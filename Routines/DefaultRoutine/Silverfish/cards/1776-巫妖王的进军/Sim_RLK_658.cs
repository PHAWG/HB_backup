using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 德鲁伊 费用：5 攻击力：5 生命值：4
    //Elder Nadox
    //纳多克斯长老
    //<b>Battlecry:</b> Destroy a friendly Undead. Your minions gain its Attack.
    //<b>战吼：</b>消灭一个友方亡灵，你的所有随从获得其攻击力。
    class Sim_RLK_658 : SimTemplate
    {
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                int Angr = target.Angr;
                p.minionGetDestroyed(target);
                foreach (var m in own.own ? p.ownMinions : p.enemyMinions)
                {
                    p.minionGetBuffed(m,Angr, 0);
                }
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE,CardDB.Race.UNDEAD),
            };
        }

    }
}
