using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 战士 费用：2
	//Precursory Strike
	//先行打击
	//Deal $3 damage.If you're holding a minion that costs (5) or more, draw a minion.
	//造成$3点伤害。如果你的手牌中有法力值消耗大于或等于（5）点的随从牌，抽一张随从牌。
	class Sim_TIME_750 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{
				int damage = ownplay ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
				p.minionGetDamageOrHeal(target, damage);
			}
		}

		public override PlayReq[] GetPlayReqs()
		{
			return new PlayReq[]
			{
				new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY)
			};
		}

	}
}
