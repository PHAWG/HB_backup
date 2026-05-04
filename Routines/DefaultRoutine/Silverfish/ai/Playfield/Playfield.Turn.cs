using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Triton.Common.LogUtilities;

namespace HREngine.Bots
{
    // 回合管理：回合开始/结束、法力解锁、回合触发器、激励
    public partial class Playfield
    {
        //伞降咒符实现,添加一个全局光环检查
        public void onOwnTurnStart(Playfield p)
        {
            // 检查光环是否结束，召唤三个1/1海盗
            if (p.sigilsToTriggerOnOwnTurnStart.Contains(CardDB.cardIDEnum.VAC_925))
            {
                for (int i = 0; i < 3; i++)
                {
                    summonPirate(p);
                }
                // 移除触发标记
                p.sigilsToTriggerOnOwnTurnStart.Remove(CardDB.cardIDEnum.VAC_925);
            }
        }
        /// <summary>
        /// 在敌方回合开始时调用
        /// </summary>
        public void onEnemyTurnStart()
        {
            if (loatheb)
            {
                // 应用洛欧塞布效果：增大敌方法术的法力值消耗
                applyLoathebEffect();
            }
        }

        /// <summary>
        /// 在敌方回合结束时调用
        /// </summary>
        public void onEnemyTurnEnd()
        {
            // 重置洛欧塞布效果
            loatheb = false;
            loathebEffect = 0;
        }

        /// <summary>
        /// 应用洛欧塞布效果
        /// </summary>
        private void applyLoathebEffect()
        {
            // 这个方法应当在敌方使用法术时调整法力值消耗，具体实现会依赖游戏的规则
        }

        /// <summary>
        /// 处理回合结束的逻辑，包括更新回合计数、元素计数、触发回合结束事件等。
        /// </summary>
        public void endTurn()
        {
            // 如果是第一回合，记录回合结束时的法力值
            if (this.turnCounter == 0)
            {
                this.manaTurnEnd = this.mana;  // 记录回合结束时的法力值
            }

            this.turnCounter++;  // 增加回合计数
            this.pIdHistory.Add(0);  // 将回合历史记录中添加一个0（可能用于记录玩家行动）

            if (isOwnTurn)  // 如果是己方的回合结束
            {
                this.value = int.MinValue;  // 将当前的评分值重置为最小值

                // 检查是否破坏了连击，并增加相应的惩罚值
                this.evaluatePenality += ComboBreaker.Instance.checkIfComboWasPlayed(this);

                if (this.complete) return;  // 如果回合已完成，直接返回

                // 更新上回合和本回合使用的元素随从数量
                this.anzOwnElementalsLastTurn = this.anzOwnElementalsThisTurn;  // 将本回合使用的元素数量赋值给上回合
                this.anzOwnElementalsThisTurn = 0;  // 重置本回合使用的元素数量
            }
            else  // 如果是敌方的回合结束
            {
                simulateTrapsEndEnemyTurn();  // 模拟敌方回合结束时的陷阱触发
            }

            this.triggerEndTurn(this.isOwnTurn);  // 触发回合结束时的相关事件
            this.isOwnTurn = !this.isOwnTurn;  // 切换回合控制权

            // 移除手牌中所有快枪状态
            RemoveQuickDrawStatus(this.owncards);
            RemoveQuickDrawStatus(this.enemyHand);

            //重置本回合对敌方英雄造成的伤害
            this.damageDealtToEnemyHeroThisTurn = 0;
            //重置本回合下一张元素随从牌的法力值消耗减少量
            this.thisTurnNextElementalReduction = 0;
            //重置连续使用元素牌的回合数
            if (!this.playedElementalThisTurn) Hrtprozis.Instance.ownConsecutiveElementalTurns = 0;
            //更新上个回合玩家使用的元素牌数量
            Hrtprozis.Instance.ownElementalsPlayedLastTurn = this.ownElementalsPlayedThisTurn;
        }

        /// <summary>
        /// 处理回合开始的逻辑，包括触发回合开始事件、模拟敌方回合开始的陷阱触发等。
        /// </summary>
        public void startTurn()
        {
            // 触发回合开始时的相关事件
            this.triggerStartTurn(this.isOwnTurn);

            if (!this.isOwnTurn) // 如果是敌方的回合开始
            {
                //敌方战士任务
                this.enemyQuest.trigger_startTurn();
                simulateTrapsStartEnemyTurn(); // 模拟敌方回合开始时触发的陷阱（奥秘）
                guessHeroDamage(); // 估算敌方英雄可能造成的伤害
            }
            else // 如果是己方的回合开始
            {
                //己方战士任务
                this.ownQuest.trigger_startTurn();
                this.enemyHeroPowerCostLessOnce = 0; // 重置敌方英雄技能一次性费用减少的标记
            }
        }

        /// <summary>
        /// 解锁法力水晶，将锁定的法力水晶转化为可用的法力水晶，并重置过载状态。
        /// </summary>
        public void unlockMana()
        {
            this.ueberladung = 0; // 重置过载值，表示本回合没有过载的法力水晶
            this.mana += lockedMana; // 将锁定的法力水晶加回到当前的法力值中
            this.lockedMana = 0; // 重置锁定的法力水晶数为0
        }

        /// <summary>
        /// 处理回合结束时的所有相关逻辑，包括随从效果触发、特殊状态重置等。
        /// </summary>
        /// <param name="ownturn">指示当前回合是否为己方回合。如果为 true，则表示是己方回合结束，否则为敌方回合结束。</param>
        public void triggerEndTurn(bool ownturn)
        {
            // 处理己方随从的回合结束效果，包括双重触发和随从销毁逻辑
            HandleEndTurnForMinions(this.ownMinions, ownturn, true, this.ownTurnEndEffectsTriggerTwice);
            // 处理敌方随从的回合结束效果，包括双重触发和随从销毁逻辑
            HandleEndTurnForMinions(this.enemyMinions, ownturn, false, this.enemyTurnEndEffectsTriggerTwice);

            // 处理己方手牌的回合结束效果
            HandleEndTurnForHandcard(this.owncards, ownturn, true);

            // 触发所有伤害触发器，如流血效果、火焰伤害等
            this.doDmgTriggers();

            // 处理暗影狂乱（Shadow Madness）效果，将受影响的随从归还给原控制者
            HandleShadowMadnessEffect(ownturn);

            // 重置本回合中特定卡牌效果的状态
            ResetSpecialCardEffects();

            // 移除所有随从的临时攻击力加成和免疫效果
            RemoveTemporaryBuffsAndImmunity(this.ownMinions);
            RemoveTemporaryBuffsAndImmunity(this.enemyMinions);

            //将回合结束时会返回到手牌的卡牌添加到手牌，如巅峰无限、甜筒殡淇淋、神性圣契
            HandlecardsToReturnAtEndOfTurn();
            removeTemporaryCards(ownturn);

            // 如果没有马尔加尼斯，移除英雄的免疫效果
            if (this.anzOwnMalGanis < 1) this.ownHero.immune = false;
            if (this.anzEnemyMalGanis < 1) this.enemyHero.immune = false;

            // 移除武器的免疫效果
            this.ownWeapon.immune = false;
            this.enemyWeapon.immune = false;
        }
        private void HandlecardsToReturnAtEndOfTurn()
        {
            if (this.cardsToReturnAtEndOfTurn.Count > 0)
            {
                foreach (CardDB.cardIDEnum cardIDEnum in this.cardsToReturnAtEndOfTurn.ToArray())
                {
                    this.drawACard(cardIDEnum, true, true);
                    this.cardsToReturnAtEndOfTurn.Remove(cardIDEnum);
                }
            }
        }
        /// <summary>
        /// 处理手牌回合结束效果
        /// </summary>
        /// <param name="owncards">手牌集合</param>
        /// <param name="ownturn">是否为我方回合结束</param>
        /// <param name="v"></param>
        private void HandleEndTurnForHandcard(List<Handmanager.Handcard> owncards, bool ownturn, bool v)
        {
            foreach (Handmanager.Handcard hc in owncards.ToArray())
            {
                hc.card.sim_card.onTurnStartTrigger(this, hc, ownturn);
            }

            foreach (Handmanager.Handcard hc in owncards.ToArray())
            {
                if (hc.enchs.Count > 1)
                {

                    if (hc.enchs.Contains(CardDB.cardIDEnum.GIL_000))
                    {
                        removeCard(hc);
                    }
                    if (hc.enchs.Contains(CardDB.cardIDEnum.GBL_999e))
                    {
                        //这里定义回合结束弃牌
                    }
                }
            }

        }

        /// <summary>
        /// 处理手牌回合开始效果
        /// </summary>
        /// <param name="owncards">手牌集合</param>
        /// <param name="ownturn">是否为我方回合开始</param>
        /// <param name="v"></param>
        private void HandleStartTurnForHandcard(List<Handmanager.Handcard> owncards, bool ownturn, bool v)
        {
            foreach (Handmanager.Handcard hc in owncards.ToArray())
            {
                hc.card.sim_card.onTurnStartTrigger(this, hc, ownturn);
            }

        }

        /// <summary>
        /// 处理随从的回合结束效果，包括双重触发效果和随从销毁逻辑。
        /// </summary>
        /// <param name="minions">随从列表，包括己方或敌方随从。</param>
        /// <param name="ownturn">当前回合是否为己方回合。</param>
        /// <param name="isOwnMinions">指示该随从列表是否为己方随从。</param>
        /// <param name="triggerTwice">指示是否需要双重触发回合结束效果。</param>
        private void HandleEndTurnForMinions(List<Minion> minions, bool ownturn, bool isOwnMinions, int triggerTwice)
        {
            // 遍历当前所有随从
            foreach (Minion m in minions.ToArray())
            {
                // 重置随从的不能攻击英雄状态
                m.cantAttackHeroes = false;

                // 如果随从未被沉默，则触发回合结束效果
                if (!m.silenced)
                {
                    // 根据条件确定触发效果的次数
                    int triggers = (isOwnMinions == ownturn) ? 1 + triggerTwice : 1;

                    // 触发指定次数的回合结束效果
                    for (int i = 0; i < triggers; i++)
                    {
                        m.handcard.card.sim_card.onTurnEndsTrigger(this, m, ownturn);
                    }
                }
                //处理地标冷却
                if (isOwnMinions == ownturn)
                {
                    if (m.handcard.card.type == CardDB.cardtype.LOCATION && m.CooldownTurn > 0)
                    {
                        m.CooldownTurn -= 1;
                        m.updateReadyness();
                        Helpfunctions.Instance.logg("卡牌名称 - " + m.handcard.card.nameCN + " 地标冷却回合 - " + m.CooldownTurn);
                    }
                }

                // 判断随从是否在回合结束时被销毁
                if ((isOwnMinions && ownturn && m.destroyOnOwnTurnEnd) ||
                    (!isOwnMinions && !ownturn && m.destroyOnEnemyTurnEnd))
                {
                    // 销毁该随从
                    this.minionGetDestroyed(m);
                }
            }

        }

        /// <summary>
        /// 处理暗影狂乱效果，归还受影响的随从给原控制者。
        /// </summary>
        /// <param name="ownturn">当前回合是否为己方回合。</param>
        private void HandleShadowMadnessEffect(bool ownturn)
        {
            // 如果有随从受到暗影狂乱影响
            if (this.shadowmadnessed >= 1)
            {
                // 根据当前回合选择己方或敌方随从列表
                List<Minion> ownm = (ownturn) ? this.ownMinions : this.enemyMinions;

                // 遍历受到暗影狂乱影响的随从
                foreach (Minion m in ownm.ToArray())
                {
                    if (m.shadowmadnessed)
                    {
                        // 取消暗影狂乱效果
                        m.shadowmadnessed = false;

                        // 将随从控制权归还给原控制者
                        this.minionGetControlled(m, !m.own, false);
                    }
                }

                // 重置暗影狂乱的影响计数
                this.shadowmadnessed = 0;

                // 更新场面状态
                updateBoards();
            }
        }

        /// <summary>
        /// 重置特定卡牌效果的状态，这些效果只在本回合有效。
        /// </summary>
        private void ResetSpecialCardEffects()
        {
            // 重置本回合内的秘密法术费用、下一个法术费用等状态
            this.nextSecretThisTurnCost0 = false;
            this.nextSpellThisTurnCost0 = false;
            this.nextMurlocThisTurnCostHealth = false;
            this.nextSpellThisTurnCostHealth = false;
            this.nextAnyCardThisTurnCostEnemyHealth = false;

            // 重置其他特殊效果状态
            this.lockandload = 0;
            this.stampede = 0;
            this.embracetheshadow = 0;
            this.playedPreparation = false;
        }

        /// <summary>
        /// 移除所有随从的临时攻击力加成和免疫效果，并重置随从的某些特殊状态。
        /// </summary>
        /// <param name="minions">随从列表。</param>
        private void RemoveTemporaryBuffsAndImmunity(List<Minion> minions)
        {
            // 遍历每个随从，移除临时攻击加成和免疫效果
            foreach (Minion m in minions)
            {
                // 移除临时攻击加成
                this.minionGetTempBuff(m, -m.tempAttack, 0);

                // 移除随从的免疫效果
                m.immune = false;

                // 重置随从的血量不能降至1点以下的状态
                m.cantLowerHPbelowONE = false;
            }
        }

        /// <summary>
        /// 处理回合开始时的触发效果，根据是己方回合还是敌方回合分别进行处理。
        /// </summary>
        /// <param name="ownturn">是否为己方回合。</param>
        public void triggerStartTurn(bool ownturn)
        {
            // 重置死亡随从列表和回合内死亡随从计数
            if (this.diedMinions != null)
            {
                this.ownMinionsDiedTurn = 0;
                this.enemyMinionsDiedTurn = 0;
                if (!this.print) this.diedMinions.Clear(); // 仅包含本回合内死亡的随从
            }

            // 处理己方或敌方随从
            List<Minion> ownm = (ownturn) ? this.ownMinions : this.enemyMinions;
            foreach (Minion m in ownm.ToArray())
            {
                // 更新随从状态
                m.playedPrevTurn = m.playedThisTurn;
                m.playedThisTurn = false;
                m.numAttacksThisTurn = 0;
                m.justBuffed = 0;
                m.updateReadyness();

                // 处理随从苏醒效果（休眠结束）
                if (m.dormant > 0 && ownturn == m.own)
                {
                    m.dormant--;
                    if (m.dormant == 0)
                    {
                        m.handcard.card.sim_card.onDormantEndsTrigger(this, m);
                    }
                }

                // 处理潜行状态的取消
                if (m.conceal)
                {
                    m.conceal = false;
                    m.stealth = false;
                }

                // 触发随从的回合开始效果（非沉默状态）
                if (!m.silenced)
                {
                    m.handcard.card.sim_card.onTurnStartTrigger(this, m, ownturn);
                }

                // 处理随从在己方或敌方回合开始时被摧毁
                if (ownturn && m.destroyOnOwnTurnStart) this.minionGetDestroyed(m);
                if (!ownturn && m.destroyOnEnemyTurnStart) this.minionGetDestroyed(m);
            }

            // 处理敌方或己方随从
            List<Minion> enemm = (ownturn) ? this.enemyMinions : this.ownMinions;
            foreach (Minion m in enemm.ToArray())
            {
                m.frozen = false;
                m.justBuffed = 0;

                // 触发随从的回合开始效果（非沉默状态）
                if (!m.silenced)
                {
                    m.handcard.card.sim_card.onTurnStartTrigger(this, m, ownturn);
                }

                // 处理随从在己方或敌方回合开始时被摧毁
                if (ownturn && m.destroyOnOwnTurnStart) this.minionGetDestroyed(m);
                if (!ownturn && m.destroyOnEnemyTurnStart) this.minionGetDestroyed(m);

                // 处理随从在回合开始时改变控制权
                if (m.changeOwnerOnTurnStart)
                {
                    this.minionGetControlled(m, ownturn, true);
                }
            }

            // 处理英雄和英雄技能的状态
            Minion hero = ownturn ? this.ownHero : this.enemyHero;
            Handmanager.Handcard heroAblility = ownturn ? this.ownHeroAblility : this.enemyHeroAblility;

            // 取消英雄的潜行状态
            if (hero.conceal)
            {
                hero.conceal = false;
                hero.stealth = false;
            }

            // 触发英雄技能的回合开始效果

            heroAblility.card.sim_card.onTurnStartTrigger(this, heroAblility, ownturn);

            HandleStartTurnForHandcard(this.owncards, ownturn, true);

            // 处理回合开始的常规效果
            this.doDmgTriggers();
            this.drawACard(CardDB.cardNameEN.unknown, ownturn);
            this.doDmgTriggers();

            // 重置回合相关的计数器和标志位
            this.cardsPlayedThisTurn = 0;
            this.mobsplayedThisTurn = 0;
            this.nextSecretThisTurnCost0 = false;
            this.nextSpellThisTurnCost0 = false;
            this.nextMurlocThisTurnCostHealth = false;
            this.nextSpellThisTurnCostHealth = false;
            this.nextAnyCardThisTurnCostEnemyHealth = false;
            this.optionsPlayedThisTurn = 0;
            this.enemyOptionsDoneThisTurn = 0;
            this.anzUsedOwnHeroPower = 0;
            this.anzUsedEnemyHeroPower = 0;

            // 根据回合调整法力值和英雄状态
            if (ownturn)
            {
                this.ownMaxMana = Math.Min(10, this.ownMaxMana + 1);
                this.mana = this.ownMaxMana - this.ueberladung;
                this.lockedMana = this.ueberladung;
                this.ueberladung = 0;

                this.enemyHero.frozen = false;
                this.ownHero.Angr = this.ownWeapon.Angr;
                this.ownHero.numAttacksThisTurn = 0;
                this.ownAbilityReady = true;
                this.ownHero.updateReadyness();
                this.owncarddraw = 0;
            }
            else
            {
                this.enemyMaxMana = Math.Min(10, this.enemyMaxMana + 1);
                this.mana = this.enemyMaxMana;
                this.ownHero.frozen = false;
                this.enemyHero.Angr = this.enemyWeapon.Angr;
                this.enemyHero.numAttacksThisTurn = 0;
                this.enemyAbilityReady = true;
                this.enemyHero.updateReadyness();
            }

            // 重置回合状态
            this.complete = false;
            this.value = int.MinValue;
        }

        /// <summary>
        /// 当英雄获得护甲时触发的效果。
        /// </summary>
        /// <param name="ownHero">是否为己方英雄。</param>
        public void triggerAHeroGotArmor(bool ownHero, int armor)
        {
            // 获取当前操作的随从列表，根据是己方英雄还是敌方英雄选择相应的随从列表
            List<Minion> minions = ownHero ? this.ownMinions : this.enemyMinions;

            // 遍历随从列表，检查是否存在重型攻城战车，并触发相应效果
            foreach (Minion m in minions)
            {
                if (!m.silenced)
                    m.handcard.card.sim_card.onHeroGetArmor(this, m, ownHero, armor);
                // 如果随从是重型攻城战车并且未被沉默
                // if (m.name == CardDB.cardNameEN.siegeengine && !m.silenced)
                // {
                //     // 重型攻城战车获得+1攻击力
                //     this.minionGetBuffed(m, 1, 0);
                // }
            }
        }

        /// <summary>
        /// 触发卡牌数量变化的相关效果。
        /// </summary>
        /// <param name="own">是否为己方卡牌变化。</param>
        public void triggerCardsChanged(bool own)
        {
            // 定义当前玩家的随从列表和卡牌数量变量，根据是否为己方变化进行选择
            List<Minion> minions = own ? this.enemyMinions : this.ownMinions;
            int currentCards = own ? this.owncards.Count : this.enemyAnzCards;
            int previousCards = own ? this.tempanzOwnCards : this.tempanzEnemyCards;

            /*
            // 如果手牌从大于等于6张变为少于6张
            if (previousCards >= 6 && currentCards <= 5)
            {
                // 遍历敌方随从，检查是否存在地精工兵，并减少其攻击力
                foreach (Minion m in minions)
                {
                    if (m.name == CardDB.cardNameEN.goblinsapper && !m.silenced)
                    {
                        this.minionGetBuffed(m, -4, 0);
                    }
                }
            }
            // 如果手牌从少于6张变为大于等于6张
            else if (currentCards >= 6 && previousCards <= 5)
            {
                // 遍历敌方随从，检查是否存在地精工兵，并增加其攻击力
                foreach (Minion m in minions)
                {
                    if (m.name == CardDB.cardNameEN.goblinsapper && !m.silenced)
                    {
                        this.minionGetBuffed(m, 4, 0);
                    }
                }
            }
            */

            // 更新临时卡牌数量变量
            if (own)
            {
                this.tempanzOwnCards = currentCards;

                // 符文秘银杖效果：如果累计使用了4张卡牌，则降低所有手牌法力值消耗
                if (this.ownWeapon.card.nameCN == CardDB.cardNameCN.符文秘银杖 && this.ownWeapon.scriptNum1 >= 4)
                {
                    foreach (Handmanager.Handcard hc in this.owncards)
                    {
                        hc.manacost--;
                        this.evaluatePenality -= 3; // 降低惩罚值
                    }
                    this.ownWeapon.scriptNum1 = 0; // 重置计数器
                }
            }
            else
            {
                this.tempanzEnemyCards = currentCards;
            }
        }

        /// <summary>
        /// 触发随从的激励效果。
        /// </summary>
        /// <param name="ownturn">是否为己方回合。</param>
        public void triggerInspire(bool ownturn)
        {
            // 获取己方和敌方的随从列表
            List<Minion> ownMinions = this.ownMinions.ToArray().ToList();
            List<Minion> enemyMinions = this.enemyMinions.ToArray().ToList();

            // 触发己方随从的激励效果
            foreach (Minion m in ownMinions)
            {
                if (!m.silenced) // 如果随从未被沉默
                {
                    m.handcard.card.sim_card.onInspire(this, m, ownturn);
                }
            }

            // 触发敌方随从的激励效果
            foreach (Minion m in enemyMinions)
            {
                if (!m.silenced) // 如果随从未被沉默
                {
                    m.handcard.card.sim_card.onInspire(this, m, ownturn);
                }
            }
        }
    }
}
