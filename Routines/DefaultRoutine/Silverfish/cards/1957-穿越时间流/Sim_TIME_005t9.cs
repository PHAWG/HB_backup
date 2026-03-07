using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 术士 费用：9 攻击力：9 生命值：9
    //Archmage Rafaam
    //大法师拉法姆
    //<b>Battlecry:</b> Transform all minions that aren't Rafaam into 1/1 Sheep.
    //<b>战吼：</b>将所有非拉法姆随从变形成为1/1的绵羊。
    class Sim_TIME_005t9 : SimTemplate
    {
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TIME_005t9t);
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            foreach (var item in p.ownMinions)
                if (!item.handcard.card.Rafaam)
                    p.minionTransform(item, kid);

            foreach (var item in p.enemyMinions)
                if (!item.handcard.card.Rafaam)
                    p.minionTransform(item, kid);

        }

    }
}
