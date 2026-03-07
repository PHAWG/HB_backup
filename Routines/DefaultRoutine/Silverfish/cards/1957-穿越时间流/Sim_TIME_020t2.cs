using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 恶魔猎手 费用：0
	//First Portal to Argus
	//第一道阿古斯传送门
	//[x]Summon a 1/1 Demon foryour opponent. When it dies,draw a card and shuffle thenext Portal into your deck.
	//为你的对手召唤一个1/1的恶魔。当它死亡时，抽一张牌，并将下一道传送门洗入你的牌库。
	class Sim_TIME_020t2 : SimTemplate
	{
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TIME_020t2t);
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            int pos = ownplay ? p.enemyMinions.Count : p.ownMinions.Count;
            p.callKid(kid, pos,ownplay);
        }
        //public override PlayReq[] GetPlayReqs()
        //{
        //    return new PlayReq[]
        //    {
        //        new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS),
        //    };
        //} 
	}
}
