using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 萨满祭司 费用：0
	//Static Shock
	//静电震击
	//Deal $1 damage to a minion. Give your hero +1 Attack this turn.
	//对一个随从造成$1点伤害。在本回合中，使你的英雄获得+1攻击力。
	class Sim_TIME_218 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{
				int damage = ownplay ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
				Minion hero = ownplay ? p.ownHero : p.enemyHero;
				p.minionGetDamageOrHeal(target, damage);
				p.minionGetTempBuff(hero,1,0);
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
