using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 无效的 费用：3
	//Eternal Firebolt
	//永时火焰箭
	//<b>Lifesteal</b>Deal $3 damage to a minion. If it dies, return this to your hand at the end of your turn.
	//<b>吸血</b>。对一个随从造成$3点伤害。如果该随从死亡，在你的回合结束时将本牌移回你的手牌。
	class Sim_END_025 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{
				int damage = ownplay ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
				p.minionGetDamageOrHeal(target, damage);
				if (target.Hp < 0)
				{
					Minion hero = ownplay ? p.ownHero : p.enemyHero;
					hero.enchs.Add(CardDB.cardIDEnum.END_025e);
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
