using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 潜行者 费用：2
	//Poison Bloom!
	//毒花少年！
	//[x]Give a friendly minion+2 Health and <b>Lifesteal</b>.
	//使一个友方随从获得+2生命值和<b>吸血</b>。
	class Sim_JAM_027b : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{
				p.minionGetBuffed(target, 0, 2);
				p.minionGetLifesteal(target);
			}
		}
		public override PlayReq[] GetPlayReqs()
		{
			return new PlayReq[]
			{
				new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
				new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
				new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
			};
		}
	}
}
