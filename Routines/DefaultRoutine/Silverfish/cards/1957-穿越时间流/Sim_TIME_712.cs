using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 牧师 费用：7
    //Dethrone
    //诛灭暴君
    //Destroy a minion. <b>Combo:</b> Summon a random 8-Cost minion.
    //消灭一个随从。<b>连击：</b>随机召唤一个法力值消耗为（8）的随从。
    class Sim_TIME_712 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            if (target != null)
            {
                p.minionGetDestroyed(target);
                if (p.cardsPlayedThisTurn > 0)
                {
                    int pos = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;
                    p.callKid(p.getRandomCardForManaMinion(8), pos, ownplay);
                }
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }

    }
}
