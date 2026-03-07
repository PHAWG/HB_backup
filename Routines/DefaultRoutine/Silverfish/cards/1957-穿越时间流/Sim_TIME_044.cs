using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//地标 圣骑士 费用：2
	//Past Gnomeregan
	//过去的诺莫瑞根
	//[x]Give a minion +2/+1.<i>Advance to the present!</i>
	//使一个随从获得+2/+1。<i>推进到现在！</i>
	class Sim_TIME_044 : SimTemplate
	{
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TIME_044t1);
        public override void useLocation(Playfield p, Minion triggerMinion, Minion target)
        {
            if (target != null)
            {
                p.minionGetBuffed(target, 2, 1);
            }
            p.minionTransform(triggerMinion, kid);
            
        }

        public override PlayReq[] GetUseAbilityReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }

	}
}
