using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//地标 圣骑士 费用：2
	//Present Gnomeregan
	//现在的诺莫瑞根
	//[x]Give a minion +2/+1and "<b>Deathrattle:</b> Deal 2damage to the enemy hero."<i>Advance to the future!</i>
	//使一个随从获得+2/+1和“<b>亡语：</b>对敌方英雄造成2点伤害。”<i>推进到未来！</i>
	class Sim_TIME_044t1 : SimTemplate
	{
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TIME_044t2);
        public override void useLocation(Playfield p, Minion triggerMinion, Minion target)
        {
            if (target != null)
            {
                p.minionGetBuffed(target, 2, 1);
                target.enchs.Add(CardDB.cardIDEnum.TIME_044t1e);
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
