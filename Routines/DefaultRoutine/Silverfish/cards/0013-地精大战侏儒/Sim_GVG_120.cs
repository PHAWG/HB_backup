using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //* 赫米特·奈辛瓦里 Hemet Nesingwary
    //<b>Battlecry:</b> Destroy a Beast.
    //<b>战吼：</b>消灭一个野兽。 
    class Sim_GVG_120 : SimTemplate
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
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE, 20),
            };
        }
    }

}