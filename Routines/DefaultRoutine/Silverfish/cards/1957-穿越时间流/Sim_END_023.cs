using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 无效的 费用：5
    //Bitter End
    //苦涩结局
    //<b>Freeze</b> a minion and its neighbors. Destroy any that are damaged.
    //<b>冻结</b>一个随从及其相邻随从，并消灭其中受伤的随从。
    class Sim_END_023 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            if (target != null)
            {
                Minion[] minions = target.own ? p.ownMinions.ToArray() : p.enemyMinions.ToArray();
                p.minionGetFrozen(target);
                if (target.wounded)
                    p.minionGetDestroyed(target);
                foreach (Minion minion in minions)
                {
                    if (minion.zonepos == target.zonepos + 1 || minion.zonepos == target.zonepos - 1)
                    {

                        p.minionGetFrozen(minion);
                        if (minion.wounded)
                            p.minionGetDestroyed(minion);
                    }
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
