using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 无效的 费用：1
	//Eternal Toil
	//永时困苦
	//[x]Deal $1 damage to aminion. If it survives, drawa card. If it dies, summona random 1-Cost minion.
	//对一个随从造成$1点伤害。如果该随从依然存活，抽一张牌。如果该随从死亡，随机召唤一个法力值消耗为（1）的随从。
	class Sim_END_020 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            if (target != null)
            {
                int damage = ownplay ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
                p.minionGetDamageOrHeal(target, damage);
                if(target.Hp > 0)
                {
                    p.drawACard(CardDB.cardIDEnum.None, ownplay);
                }
                else
                {
                    int pos = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;
                    p.callKid(p.getRandomCardForManaMinion(1), pos, ownplay);
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
