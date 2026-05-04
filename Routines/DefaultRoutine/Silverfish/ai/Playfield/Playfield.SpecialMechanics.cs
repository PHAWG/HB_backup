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
    public partial class Playfield
    {
        /// <summary>
        /// 增加尸体
        /// </summary>
        /// <param name="count"></param>
        public void addCorpses(int count)
        {
            if (!this.ownGraveyard.ContainsKey(CardDB.cardIDEnum.CS2_122))
                this.ownGraveyard.Add(CardDB.cardIDEnum.CS2_122, count);
            else
                this.ownGraveyard[CardDB.cardIDEnum.CS2_122] += count;
        }
        //伞降咒符 海盗帕奇斯降落伞 惊险悬崖 sim实现方法
        public void summonPirate(Playfield p)
        {
            // 冲锋海盗卡牌ID为 "VAC_926t'"
            CardDB.Card pirateCard = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.VAC_926t);
            int position = p.ownMinions.Count; // 召唤的位置在己方随从的最后
            p.callKid(pirateCard, position, true);
        }
        /// <summary>
        /// 消耗尸体
        /// </summary>
        /// <param name="count"></param>
        public void corpseConsumption(int count)
        {
            Dictionary<CardDB.cardIDEnum, int> tempOwnGraveyard = new Dictionary<CardDB.cardIDEnum, int>();
            foreach (var item in this.ownGraveyard)
            {
                if (CardDB.Instance.getCardDataFromID(item.Key).type == CardDB.cardtype.MOB)
                {
                    if (item.Value > count)
                    {
                        int temp = item.Value - count;
                        this.ownGraveyard[item.Key] = temp;
                        break;
                    }
                    else if (item.Value == count)
                    {
                        count = count - item.Value;
                        tempOwnGraveyard.Add(item.Key, item.Value);
                        break;
                    }
                    else
                    {
                        count = count - item.Value;
                        tempOwnGraveyard.Add(item.Key, item.Value);
                    }
                }
            }

            foreach (var item in tempOwnGraveyard)
            {
                this.ownGraveyard.Remove(item.Key);
            }

        }

        /// <summary>
        /// 获取尸体数量
        /// </summary>
        /// <returns></returns>
        public int getCorpseCount()
        {
            int retCount = 0;
            foreach (var item in this.ownGraveyard)
            {
                if (CardDB.Instance.getCardDataFromID(item.Key).type == CardDB.cardtype.MOB)
                {
                    retCount += item.Value;
                }
            }
            return retCount;
        }

        /// <summary>
        /// 更换英雄技能（英雄技能在游戏中被称为Hero Power）。
        /// </summary>
        /// <param name="newHeroPower">新的英雄技能卡牌ID</param>
        /// <param name="own">是否为己方英雄</param>
        public void setNewHeroPower(CardDB.cardIDEnum newHeroPower, bool own)
        {
            if (own)
            {
                // 设置己方英雄的新技能
                this.ownHeroAblility.card = CardDB.Instance.getCardDataFromID(newHeroPower);
                this.ownAbilityReady = true; // 确保英雄技能可以使用
            }
            else
            {
                // 设置敌方英雄的新技能
                this.enemyHeroAblility.card = CardDB.Instance.getCardDataFromID(newHeroPower);
                this.enemyAbilityReady = true; // 确保英雄技能可以使用
            }
        }

        /// <summary>
        /// 用于处理发掘进度并返回对应品质的随机卡牌
        /// </summary>
        /// <returns></returns>
        public CardDB.Card handleExcavation()
        {
            this.excavationCount++;
            this.allExcavationCount++;

            CardDB.Card resultCard = null;

            if (this.excavationCount == 1)
            {
                // 普通宝藏池
                resultCard = getRandomCardFromPool("common");
            }
            else if (this.excavationCount == 2)
            {
                // 稀有宝藏池
                resultCard = getRandomCardFromPool("rare");
            }
            else if (this.excavationCount == 3)
            {
                // 史诗宝藏池
                resultCard = getRandomCardFromPool("epic");

                // 如果当前英雄没有专属传说级宝藏，则重置发掘进度
                if (!hasLegendaryTreasure())
                {
                    this.excavationCount = 0;
                }
            }
            else if (this.excavationCount == 4)
            {
                // 处理传说级宝藏
                resultCard = getLegendaryTreasure();

                // 重置发掘进度
                this.excavationCount = 0;
            }
            return resultCard;
        }

        /// <summary>
        /// 获取指定品质的宝藏池卡牌列表
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public List<CardDB.cardIDEnum> getTreasurePool(string quality)
        {
            if (CardDB.Instance.treasurePools.ContainsKey(quality))
            {
                return CardDB.Instance.treasurePools[quality];
            }
            return new List<CardDB.cardIDEnum>();
        }

        /// <summary>
        /// 根据英雄类型返回特定的传说级宝藏卡牌
        /// </summary>
        /// <returns></returns>
        private CardDB.Card getLegendaryTreasure()
        {
            // 获取当前英雄的类型
            TAG_CLASS heroClass = this.ownHero.cardClass;

            // 根据英雄职业返回对应的传说级宝藏卡牌
            CardDB.cardIDEnum legendaryTreasureId = CardDB.cardIDEnum.None;

            switch (heroClass)
            {
                case TAG_CLASS.DEATHKNIGHT:
                    legendaryTreasureId = CardDB.cardIDEnum.WW_001t26; // 艾泽里特鼠
                    break;
                case TAG_CLASS.MAGE:
                    legendaryTreasureId = CardDB.cardIDEnum.WW_001t24; // 艾泽里特鹰
                    break;
                case TAG_CLASS.PALADIN:
                    legendaryTreasureId = CardDB.cardIDEnum.DEEP_999t4; // 艾泽里特龙
                    break;
                case TAG_CLASS.ROGUE:
                    legendaryTreasureId = CardDB.cardIDEnum.WW_001t23; // 艾泽里特蝎
                    break;
                case TAG_CLASS.SHAMAN:
                    legendaryTreasureId = CardDB.cardIDEnum.DEEP_999t5; // 艾泽里特鱼人
                    break;
                case TAG_CLASS.WARLOCK:
                    legendaryTreasureId = CardDB.cardIDEnum.WW_001t25; // 艾泽里特蛇
                    break;
                case TAG_CLASS.WARRIOR:
                    legendaryTreasureId = CardDB.cardIDEnum.WW_001t27; // 艾泽里特牛
                    break;
                default:
                    // 如果没有对应的职业宝藏卡牌，则保持为空（或根据需求处理）
                    break;
            }

            // 返回对应的卡牌对象
            return CardDB.Instance.getCardDataFromID(legendaryTreasureId);
        }

        /// <summary>
        /// 从指定品质的宝藏池中随机获取一张卡牌
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        private CardDB.Card getRandomCardFromPool(string quality)
        {
            // 假设有一个方法可以根据宝藏池的品质获取相应的卡牌池
            List<CardDB.cardIDEnum> pool = getTreasurePool(quality);
            if (pool.Count == 0) return null;

            int randomIndex = getRandomNumber(0, pool.Count - 1);
            return CardDB.Instance.getCardDataFromID(pool[randomIndex]);
        }

        /// <summary>
        /// 基于当前英雄的类型判断是否有专属传说级宝藏
        /// </summary>
        /// <returns></returns>
        private bool hasLegendaryTreasure()
        {
            // 获取当前英雄的类型
            TAG_CLASS heroClass = this.ownHero.cardClass;

            // 判断是否有专属的传说级宝藏
            switch (heroClass)
            {
                case TAG_CLASS.DEATHKNIGHT: // 艾泽里特鼠
                case TAG_CLASS.MAGE:        // 艾泽里特鹰
                case TAG_CLASS.PALADIN:     // 艾泽里特龙
                case TAG_CLASS.ROGUE:       // 艾泽里特蝎
                case TAG_CLASS.SHAMAN:      // 艾泽里特鱼人
                case TAG_CLASS.WARLOCK:     // 艾泽里特蛇
                case TAG_CLASS.WARRIOR:     // 艾泽里特牛
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 使用地标卡牌
        /// </summary>
        /// <param name="own">地标卡牌所属的随从对象</param>
        /// <param name="target">目标随从或英雄</param>
        public void useLocation(Minion own, Minion target)
        {
            // 获取当前要使用的地标卡牌
            CardDB.Card locationCard = own.handcard.card;

            if (locationCard.CooldownTurn > 0) return;

            // 触发地标的使用效果
            locationCard.sim_card.useLocation(this, own, target);



            // 如果地标能冻结目标，进行冻结操作
            // if (target != null && locationCard.cardIDenum == CardDB.cardIDEnum.REV_602)
            // {
            //     minionGetFrozen(target);
            // }

            // 特殊处理对于 VAC_334 的逻辑
            if (own.handcard.card.cardIDenum == CardDB.cardIDEnum.VAC_334) // 检查是否为"小玩物小屋"卡牌
            {
                if (this.owncards.Count > 0)
                {
                    // 记录手牌中的最后一张牌的实体ID
                    int lastCardEntityID = this.owncards[this.owncards.Count - 1].entity;

                    // 假设你要记录这个实体ID，可以保存到一个新的属性
                    this.lastDrawnCardEntityID = lastCardEntityID; // 需要在类中定义 lastDrawnCardEntityID 变量
                }
            }

            if (own.Hp <= 0)
            {
                own.handcard.card.sim_card.onDeathrattle(this, own);
            }
        }

        /// <summary>
        /// 使用泰坦技能
        /// </summary>
        /// <param name="own">泰坦所属的随从对象</param>
        /// <param name="titanAbilityNO">泰坦的技能编号，1、2、3</param>
        /// <param name="target">目标随从</param>
        /// 
        public void useTitanAbility(Minion own, int titanAbilityNO, Minion target)
        {

            // 获取当前要使用技能的泰坦卡牌
            // CardDB.Card useAbilityTitan = own.handcard.card;
            //是否继续执行
            bool flag = true;
            switch (titanAbilityNO)
            {
                case 1:
                    if (own.TitanAbilityUsed1) flag = false;
                    break;
                case 2:
                    if (own.TitanAbilityUsed2) flag = false;
                    break;
                case 3:
                    if (own.TitanAbilityUsed3) flag = false;
                    break;
            }
            if (flag)
            {
                // 触发技能的使用效果
                own.handcard.card.sim_card.useTitanAbility(this, own, titanAbilityNO, target);
            }
        }

        public CardDB.Card getNextJadeGolem(bool ownplay)
        {
            int jadeGolemCount = ownplay ? this.anzOwnJadeGolem : this.anzEnemyJadeGolem;
            jadeGolemCount = System.Math.Min(jadeGolemCount, 29);
            jadeGolemCount = System.Math.Max(jadeGolemCount, 0);
            CardDB.cardIDEnum jadeGolemId = CardDB.cardIDEnum.CFM_712_t01;
            switch (jadeGolemCount + 1)
            {
                case 1: jadeGolemId = CardDB.cardIDEnum.CFM_712_t01; break;
                case 2: jadeGolemId = CardDB.cardIDEnum.CFM_712_t02; break;
                case 3: jadeGolemId = CardDB.cardIDEnum.CFM_712_t03; break;
                case 4: jadeGolemId = CardDB.cardIDEnum.CFM_712_t04; break;
                case 5: jadeGolemId = CardDB.cardIDEnum.CFM_712_t05; break;
                case 6: jadeGolemId = CardDB.cardIDEnum.CFM_712_t06; break;
                case 7: jadeGolemId = CardDB.cardIDEnum.CFM_712_t07; break;
                case 8: jadeGolemId = CardDB.cardIDEnum.CFM_712_t08; break;
                case 9: jadeGolemId = CardDB.cardIDEnum.CFM_712_t09; break;
                case 10: jadeGolemId = CardDB.cardIDEnum.CFM_712_t10; break;
                case 11: jadeGolemId = CardDB.cardIDEnum.CFM_712_t11; break;
                case 12: jadeGolemId = CardDB.cardIDEnum.CFM_712_t12; break;
                case 13: jadeGolemId = CardDB.cardIDEnum.CFM_712_t13; break;
                case 14: jadeGolemId = CardDB.cardIDEnum.CFM_712_t14; break;
                case 15: jadeGolemId = CardDB.cardIDEnum.CFM_712_t15; break;
                case 16: jadeGolemId = CardDB.cardIDEnum.CFM_712_t16; break;
                case 17: jadeGolemId = CardDB.cardIDEnum.CFM_712_t17; break;
                case 18: jadeGolemId = CardDB.cardIDEnum.CFM_712_t18; break;
                case 19: jadeGolemId = CardDB.cardIDEnum.CFM_712_t19; break;
                case 20: jadeGolemId = CardDB.cardIDEnum.CFM_712_t20; break;
                case 21: jadeGolemId = CardDB.cardIDEnum.CFM_712_t21; break;
                case 22: jadeGolemId = CardDB.cardIDEnum.CFM_712_t22; break;
                case 23: jadeGolemId = CardDB.cardIDEnum.CFM_712_t23; break;
                case 24: jadeGolemId = CardDB.cardIDEnum.CFM_712_t24; break;
                case 25: jadeGolemId = CardDB.cardIDEnum.CFM_712_t25; break;
                case 26: jadeGolemId = CardDB.cardIDEnum.CFM_712_t26; break;
                case 27: jadeGolemId = CardDB.cardIDEnum.CFM_712_t27; break;
                case 28: jadeGolemId = CardDB.cardIDEnum.CFM_712_t28; break;
                case 29: jadeGolemId = CardDB.cardIDEnum.CFM_712_t29; break;
                case 30: jadeGolemId = CardDB.cardIDEnum.CFM_712_t30; break;
            }
            return CardDB.Instance.getCardDataFromID(jadeGolemId);
        }
    }
}
