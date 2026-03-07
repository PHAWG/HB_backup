using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 无效的 费用：2
	//Press the Advantage
	//发挥优势
	//Deal $1 damage. Give your hero +1 Attack this turn. Draw 1 card. Gain 1 Armor.
	//造成$1点伤害。在本回合中，使你的英雄获得+1攻击力。抽1张牌。获得1点护甲值。
	class Sim_END_007 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{
				int damage = ownplay ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
				Minion hero = ownplay ? p.ownHero : p.enemyHero;
				p.minionGetDamageOrHeal(target, damage);
				p.minionGetTempBuff(hero,1,0);
				p.minionGetArmor(hero,1);
				p.drawACard(CardDB.cardIDEnum.None,ownplay);
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
