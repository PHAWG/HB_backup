using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Logger = Triton.Common.LogUtilities.Logger;
using log4net;

namespace HREngine.Bots
{   
    /// <summary>
    /// CardDB辅助方法，根据不同参数获取CardDB.Card
    /// </summary>
    partial class CardDB
    {
        /// <summary>
        /// 输入卡牌id，输出cardIDEnum枚举对象
        /// </summary>
        /// <param name="cardId">卡牌id字符串</param>
        /// <returns>cardIDEnum枚举对象</returns>
        public CardDB.cardIDEnum cardIdstringToEnum(string cardId)
        {
            CardDB.cardIDEnum CardEnum;
            if (Enum.TryParse<cardIDEnum>(cardId, false, out CardEnum)) return CardEnum;
            else
            {
                return CardDB.cardIDEnum.None;
            }
        }


        /// <summary>
        /// 输入卡牌中文名，输出CardDB.Card类对象，多个同名，返回第一个
        /// </summary>
        /// <param name="chnName">卡牌中文名</param>
        /// <returns>CardDB.Card类对象</returns>
        public CardDB.Card chnNameToCard(string chnName)
        {
            CardDB.Card c;
            CardDB.cardNameCN enumCn;
            if (Enum.TryParse<CardDB.cardNameCN>(chnName, out enumCn) && cardNameCNToCardList.TryGetValue(enumCn, out c))
            {
                return c;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 输入卡牌英文名，输出cardNameEN枚举对象
        /// </summary>
        /// <param name="cardNameEn">卡牌英文名</param>
        /// <returns>CardDB.cardNameEN枚举对象</returns>
        public CardDB.cardNameEN cardNameENstringToEnum(string cardNameEn)
        {
            CardDB.cardNameEN NameEnum;
            if (Enum.TryParse<CardDB.cardNameEN>(cardNameEn, false, out NameEnum)) return NameEnum;
            else return CardDB.cardNameEN.unknown;
        }

        /// <summary>
        /// 输入卡牌英文名，输出cardNameCN枚举对象
        /// </summary>
        /// <param name="cardNameEn">卡牌英文名</param>
        /// <returns>CardDB.cardNameCN枚举对象</returns>
        public CardDB.cardNameCN cardNameENstringToEnumCN(string cardNameEn)
        {
            CardDB.cardNameEN NameEnum;
            if (Enum.TryParse<CardDB.cardNameEN>(cardNameEn, false, out NameEnum))
            {
                CardDB.Card card = getCardData(NameEnum);
                return card.nameCN;
            }
            else
            {
                return CardDB.cardNameCN.未知;
            }
            ;
        }

        /// <summary>
        /// 输入卡牌中文名，输出cardNameCN枚举对象
        /// </summary>
        /// <param name="cardNameCn"></param>
        /// <returns>CardDB.cardNameCN枚举对象</returns>
        public cardNameCN cardNameCNstringToEnum(string cardNameCn)
        {
            CardDB.cardNameCN NameEnum;
            if (Enum.TryParse<CardDB.cardNameCN>(cardNameCn, false, out NameEnum)) return NameEnum;
            else return CardDB.cardNameCN.未知;
        }

        /// <summary>
        /// 输入种族英文名,输出CardDB.Race枚举对象
        /// </summary>
        /// <param name="raceNameEn">种族英文名</param>
        /// <returns>CardDB.Race枚举对象</returns>
        public CardDB.Race raceNameStringToEnum(string raceNameEn)
        {
            CardDB.Race RaceEnum;
            if (Enum.TryParse<CardDB.Race>(raceNameEn, false, out RaceEnum)) return RaceEnum;
            else return CardDB.Race.BLANK;
        }

        /// <summary>
        /// 输入法术派系英文名,输出CardDB.SpellSchool枚举对象
        /// </summary>
        /// <param name="spellSchoolNameEn">法术派系英文名</param>
        /// <returns>CardDB.SpellSchool枚举对象</returns>
        public CardDB.SpellSchool spellSchoolNameStringToEnum(string spellSchoolNameEn)
        {
            CardDB.SpellSchool SpellSchoolEnum;
            if (Enum.TryParse<CardDB.SpellSchool>(spellSchoolNameEn, false, out SpellSchoolEnum)) return SpellSchoolEnum;
            else return CardDB.SpellSchool.PHYSICAL_COMBAT;
        }
    }
}
