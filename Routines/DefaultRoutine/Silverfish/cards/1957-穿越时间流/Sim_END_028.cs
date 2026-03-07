using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HREngine.Bots
{
	//法术 无效的 费用：4
	//For All Time
	//力敌万世
	//Destroy all minions with 4 or less Attack. <b>Overload:</b> (2)
	//消灭所有攻击力小于或等于4的随从。<b>过载：</b>（2）。
	class Sim_END_028 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            List<Minion> minions = new List<Minion>();
			minions.AddRange(p.ownMinions.ToList());
			minions.AddRange(p.enemyMinions.ToList());
            foreach (Minion minion in minions)
            {
                if(minion.Angr <= 2)
				{
					p.minionGetDestroyed(minion);
				}
            }
        }
		
	}
}
