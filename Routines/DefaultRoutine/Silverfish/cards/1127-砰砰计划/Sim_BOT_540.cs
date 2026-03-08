using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //* 电磁脉冲特工 E.M.P. Operative
    //<b>Battlecry:</b> Destroy a Mech.
    //<b>战吼：</b>消灭一个机械。
    class Sim_BOT_540 : SimTemplate 
	{
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                p.minionGetDestroyed(target);
            }
        }


        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE, 17),
            };
        }
	}
}
