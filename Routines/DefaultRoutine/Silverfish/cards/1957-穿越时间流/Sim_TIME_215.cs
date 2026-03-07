using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 萨满祭司 费用：2
	//Thunderquake
	//雷霆动地
	//[x]Deal $1 damageto all minions.Get a Static Shock.
	//对所有随从造成$1点伤害。获取一张静电震击。
	class Sim_TIME_215 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
		{
			if (target != null)
			{
				int damage = ownplay ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
				p.allMinionsGetDamage(damage);
				p.drawACard(CardDB.cardIDEnum.TIME_218,ownplay);

			}
		}
		
	}
}
