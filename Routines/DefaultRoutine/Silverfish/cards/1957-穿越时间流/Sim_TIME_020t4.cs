using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 恶魔猎手 费用：0
	//Third Portal to Argus
	//第三道阿古斯传送门
	//[x]Summon a 3/1 Demon foryour opponent. When it dies,draw a card and shuffle thenext Portal into your deck.
	//为你的对手召唤一个3/1的恶魔。当它死亡时，抽一张牌，并将下一道传送门洗入你的牌库。
	class Sim_TIME_020t4 : SimTemplate
	{
        // 2/1 恶魔的卡牌 ID
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TIME_020t4t);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            // 为对手召唤随从
            int pos = ownplay ? p.enemyMinions.Count : p.ownMinions.Count;
            p.callKid(kid, pos, !ownplay);
        }

    }
}
