using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 圣骑士 费用：6 攻击力：4 生命值：4
	//PMM Infinitizer
	//无穷永动机
	//<b>Battlecry:</b> Set a friendly minion's Attack and Health to 8. It can't attack heroes this turn.
	//<b>战吼：</b>将一个友方随从的攻击力和生命值变为8。该随从在本回合中无法攻击英雄。
	class Sim_TIME_043 : SimTemplate
	{
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                p.minionSetHealthtoX(target, 8);
                p.minionSetAttackToX(target, 8);
                target.cantAttackHeroes = true;

            }
        }


        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }

    }
}
