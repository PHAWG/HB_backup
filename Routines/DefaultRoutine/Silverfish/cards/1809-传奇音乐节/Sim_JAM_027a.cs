using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 潜行者 费用：2
	//Zok Rocks!
	//佐克无敌！
	//Give a friendly minion +2 Attack and <b>Rush</b>.
	//使一个友方随从获得+2攻击力和<b>突袭</b>。
	class Sim_JAM_027a : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{

				p.minionGetBuffed(target, 2, 0);
				p.minionGetRush(target);
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
