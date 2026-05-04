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
    // 光环与增益：场面光环重算、种族增益、相邻增益
    public partial class Playfield
    {
        /// <summary>
        /// 更新战场上的随从状态，包括处理死亡随从的亡语效果和相邻随从的增益效果。
        /// </summary>
        public void updateBoards()
        {
            // 如果没有随从状态变化，则直接返回
            if (!this.tempTrigger.ownMinionsChanged && !this.tempTrigger.enemyMininsChanged) return;

            // 用于存储需要触发亡语效果的随从
            List<Minion> deathrattleMinions = new List<Minion>();

            // 标记是否有复活的随从
            bool minionOwnReviving = false;
            bool minionEnemyReviving = false;

            // 如果己方随从发生变化
            if (this.tempTrigger.ownMinionsChanged)
            {
                this.tempTrigger.ownMinionsChanged = false;
                List<Minion> temp = new List<Minion>();
                int i = 1;
                foreach (Minion m in this.ownMinions)
                {
                    // 移除相邻的增益效果
                    this.minionGetAdjacentBuff(m, -m.AdjacentAngr, 0);
                    // m.cantBeTargetedBySpellsOrHeroPowers = false;

                    // 如果随从已死亡
                    if (m.Hp <= 0)
                    {
                        this.OwnLastDiedMinion = m.handcard.card.cardIDenum;

                        // 记录第一个复活的随从
                        if (this.revivingOwnMinion == CardDB.cardIDEnum.None)
                        {
                            this.revivingOwnMinion = m.handcard.card.cardIDenum;
                            minionOwnReviving = true;
                        }

                        // 检查随从是否有亡语或其他相关效果
                        if ((!m.silenced && m.handcard.card.deathrattle) || m.ancestralspirit >= 1 || m.desperatestand >= 1 ||
                            m.souloftheforest >= 1 || m.stegodon >= 1 || m.livingspores >= 1 || m.infest >= 1 ||
                            m.explorershat >= 1 || m.returnToHand >= 1 || m.itsnecrolit >= 1 || m.sheepmask >= 1 || m.greybud >= 1 || m.infected >= 1 || m.finalsession >= 1 || m.deathrattle2 != null)
                        {
                            deathrattleMinions.Add(m);
                        }

                        // 移除随从的光环效果
                        if (!m.silenced)
                        {
                            m.handcard.card.sim_card.onAuraEnds(this, m);
                        }
                    }
                    else
                    {
                        m.zonepos = i;
                        temp.Add(m);
                        i++;
                    }
                }
                this.ownMinions = temp;
                this.updateAdjacentBuffs(true); // 更新己方随从的相邻增益效果
            }

            // 如果敌方随从发生变化
            if (this.tempTrigger.enemyMininsChanged)
            {
                this.tempTrigger.enemyMininsChanged = false;
                List<Minion> temp = new List<Minion>();
                int i = 1;
                foreach (Minion m in this.enemyMinions)
                {
                    // 移除相邻的增益效果
                    this.minionGetAdjacentBuff(m, -m.AdjacentAngr, 0);
                    // m.cantBeTargetedBySpellsOrHeroPowers = false;

                    // 如果随从已死亡
                    if (m.Hp <= 0)
                    {
                        if (this.revivingEnemyMinion == CardDB.cardIDEnum.None)
                        {
                            this.revivingEnemyMinion = m.handcard.card.cardIDenum;
                            minionEnemyReviving = true;
                        }

                        // 检查随从是否有亡语或其他相关效果
                        if ((!m.silenced && m.handcard.card.deathrattle) || m.ancestralspirit >= 1 || m.desperatestand >= 1 ||
                            m.souloftheforest >= 1 || m.stegodon >= 1 || m.livingspores >= 1 || m.infest >= 1 ||
                            m.explorershat >= 1 || m.returnToHand >= 1 || m.greybud >= 1 || m.infected >= 1 || m.finalsession >= 1 || m.deathrattle2 != null)
                        {
                            deathrattleMinions.Add(m);
                        }

                        // 移除随从的光环效果
                        if (!m.silenced)
                        {
                            m.handcard.card.sim_card.onAuraEnds(this, m);
                        }
                    }
                    else
                    {
                        m.zonepos = i;
                        temp.Add(m);
                        i++;
                    }
                }
                this.enemyMinions = temp;
                this.updateAdjacentBuffs(false); // 更新敌方随从的相邻增益效果
            }

            // 处理武器"灵魂之爪"的效果，根据法术强度增加或减少攻击力
            handleSpiritClaws(this.ownWeapon, this.ownHero, this.spellpower, this.spellpowerStarted, ref this.ownSpiritclaws);
            handleSpiritClaws(this.enemyWeapon, this.enemyHero, this.enemyspellpower, this.enemyspellpowerStarted, ref this.enemySpiritclaws);

            // 触发所有亡语效果
            if (deathrattleMinions.Count >= 1)
            {
                this.doDeathrattles(deathrattleMinions);
            }

            // 如果有己方复活的随从，触发相关奥秘
            if (minionOwnReviving)
            {
                this.secretTrigger_MinionDied(true);
                this.revivingOwnMinion = CardDB.cardIDEnum.None;
            }

            // 如果有敌方复活的随从，触发相关奥秘
            if (minionEnemyReviving)
            {
                this.secretTrigger_MinionDied(false);
                this.revivingEnemyMinion = CardDB.cardIDEnum.None;
            }
        }

        /// <summary>
        /// 处理灵魂之爪武器的攻击力调整。
        /// </summary>
        /// <param name="weapon">武器对象</param>
        /// <param name="hero">英雄对象</param>
        /// <param name="currentSpellPower">当前法术强度</param>
        /// <param name="initialSpellPower">初始法术强度</param>
        /// <param name="spiritClawsActive">灵魂之爪激活状态</param>
        private void handleSpiritClaws(Weapon weapon, Minion hero, int currentSpellPower, int initialSpellPower, ref bool spiritClawsActive)
        {
            if (weapon.name == CardDB.cardNameEN.spiritclaws)
            {
                int dif = (currentSpellPower > 0 ? 2 : 0) - (initialSpellPower > 0 ? 2 : 0);
                if (dif > 0 && !spiritClawsActive)
                {
                    this.minionGetBuffed(hero, 2, 0);
                    weapon.Angr += 2;
                    spiritClawsActive = true;
                }
                else if (dif < 0 && spiritClawsActive)
                {
                    this.minionGetBuffed(hero, -2, 0);
                    weapon.Angr -= 2;
                    spiritClawsActive = false;
                }
            }
        }

        /// <summary>
        /// 根据光环效果为随从添加或移除属性增益。
        /// </summary>
        /// <param name="m">目标随从</param>
        /// <param name="get">是否为添加增益，如果为false则移除增益</param>
        public void minionGetOrEraseAllAreaBuffs(Minion m, bool get)
        {
            // 英雄不受影响
            if (m.isHero) return;

            int angr = 0;  // 攻击力增益
            int vert = 0;  // 生命值增益

            // 如果随从未被沉默
            if (!m.silenced)
            {
                switch (m.name)
                {
                    case CardDB.cardNameEN.raidleader:
                    case CardDB.cardNameEN.leokk:
                    case CardDB.cardNameEN.timberwolf:
                    case CardDB.cardNameEN.grimscaleoracle:
                        angr--;  // 减去这些随从带来的基础攻击力增益
                        break;

                    case CardDB.cardNameEN.vessina:
                        if (this.ueberladung > 0 || this.lockedMana > 0) angr--;  // 只有在过载时才减去Vessina的攻击力增益
                        break;

                    case CardDB.cardNameEN.stormwindchampion:
                    case CardDB.cardNameEN.southseacaptain:
                        angr--;
                        vert--;  // 减去这些随从带来的攻击力和生命值增益
                        break;

                    case CardDB.cardNameEN.murlocwarleader:
                        if (get) angr -= 2;  // Murloc Warleader 给予的攻击力增益为2
                        break;
                }
            }

            // 处理随从种族相关的增益效果
            if (m.handcard.card.race == CardDB.Race.MURLOC)
            {
                angr += m.own ? (2 * anzOwnMurlocWarleader + anzOwnGrimscaleOracle) :
                                (2 * anzEnemyMurlocWarleader + anzEnemyGrimscaleOracle);
            }

            if (m.own)
            {
                // 处理己方随从的增益效果
                angr += anzOwnRaidleader + anzOwnStormwindChamps + (this.ueberladung > 0 || this.lockedMana > 0 ? anzOwnVessina : 0);
                vert += anzOwnStormwindChamps;

                if (m.name == CardDB.cardNameEN.silverhandrecruit) angr += anzOwnWarhorseTrainer;

                handleRaceSpecificBuffs(m, get, true);
            }
            else
            {
                // 处理敌方随从的增益效果
                angr += anzEnemyRaidleader + anzEnemyStormwindChamps + (this.ueberladung > 0 || this.lockedMana > 0 ? anzEnemyVessina : 0);
                vert += anzEnemyStormwindChamps;

                if (m.name == CardDB.cardNameEN.silverhandrecruit) angr += anzEnemyWarhorseTrainer;

                handleRaceSpecificBuffs(m, get, false);
            }

            // 根据get参数决定是添加还是移除增益
            this.minionGetBuffed(m, get ? angr : -angr, get ? vert : -vert);
        }

        /// <summary>
        /// 根据随从种族处理种族相关的增益效果。
        /// </summary>
        /// <param name="m">目标随从</param>
        /// <param name="get">是否为添加增益，如果为false则移除增益</param>
        /// <param name="own">随从是否属于己方</param>
        private void handleRaceSpecificBuffs(Minion m, bool get, bool own)
        {
            int angr = 0;
            int vert = 0;

            if (m.handcard.card.race == CardDB.Race.PET)
            {
                angr += own ? anzOwnTimberWolfs : anzEnemyTimberWolfs;
                if (get) m.charge += own ? anzOwnTundrarhino : anzEnemyTundrarhino;
                else m.charge -= own ? anzOwnTundrarhino : anzEnemyTundrarhino;
            }
            if (m.handcard.card.race == CardDB.Race.PIRATE)
            {
                angr += own ? anzOwnSouthseacaptain : anzEnemySouthseacaptain;
                vert += own ? anzOwnSouthseacaptain : anzEnemySouthseacaptain;
                if (get) m.charge += own ? anzOwnMrSmite : anzEnemyMrSmite;
                else m.charge -= own ? anzOwnMrSmite : anzEnemyMrSmite;
            }
            if (m.handcard.card.race == CardDB.Race.DEMON)
            {
                angr += (own ? anzOwnMalGanis : anzEnemyMalGanis) * 2;
                vert += (own ? anzOwnMalGanis : anzEnemyMalGanis) * 2;
            }

            // 为随从添加或移除种族相关的增益
            this.minionGetBuffed(m, angr, vert);
        }

        /// <summary>
        /// 更新随从的相邻增益效果。仅在更新战场后调用。
        /// </summary>
        /// <param name="own">是否为己方随从</param>
        public void updateAdjacentBuffs(bool own)
        {
            // 获取目标随从列表
            List<Minion> temp = own ? this.ownMinions : this.enemyMinions;
            int anz = temp.Count;  // 获取随从数量

            // 遍历随从列表
            for (int i = 0; i < anz; i++)
            {
                Minion m = temp[i];

                // 如果随从未被沉默
                if (!m.silenced)
                {
                    // 根据随从名称应用特定的相邻增益效果
                    switch (m.name)
                    {
                        case CardDB.cardNameEN.weespellstopper:
                            // Weespellstopper会使相邻的随从无法被法术或英雄技能选定为目标
                            if (i > 0) temp[i - 1].Elusive = true;
                            if (i < anz - 1) temp[i + 1].Elusive = true;
                            continue;

                        case CardDB.cardNameEN.direwolfalpha:
                            // Dire Wolf Alpha为相邻的随从增加1点攻击力
                            if (i > 0) this.minionGetAdjacentBuff(temp[i - 1], 1, 0);
                            if (i < anz - 1) this.minionGetAdjacentBuff(temp[i + 1], 1, 0);
                            continue;

                        case CardDB.cardNameEN.flametonguetotem:
                            // Flametongue Totem为相邻的随从增加2点攻击力
                            if (i > 0) this.minionGetAdjacentBuff(temp[i - 1], 2, 0);
                            if (i < anz - 1) this.minionGetAdjacentBuff(temp[i + 1], 2, 0);
                            continue;
                    }
                }
            }
        }
    }
}
