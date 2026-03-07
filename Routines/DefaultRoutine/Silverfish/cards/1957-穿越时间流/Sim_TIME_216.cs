using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 萨满祭司 费用：3
	//Nascent Bolt
	//新生闪电
	//Deal $5 damage to a minion. If it survives, draw 2 cards.
	//对一个随从造成$5点伤害。如果该随从依然存活，抽两张牌。
	class Sim_TIME_216 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{
				int damage = ownplay ? p.getSpellDamageDamage(5) : p.getEnemySpellDamageDamage(5);
				p.minionGetDamageOrHeal(target, damage);
				if (target.Hp > 0)
				{
					p.drawACard(CardDB.cardIDEnum.None, ownplay);
					p.drawACard(CardDB.cardIDEnum.None, ownplay);

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
