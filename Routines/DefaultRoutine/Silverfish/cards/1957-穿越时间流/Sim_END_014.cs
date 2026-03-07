using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 无效的 费用：4
    //Synchronized Spark
    //协作火花
    //[x]Deal $3 damage toan enemy. If it dies,give a random friendlyminion +3/+3.
    //对一个敌人造成$3点伤害。如果该角色死亡，随机使一个友方随从获得+3/+3。
    class Sim_END_014 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            if (target != null)
            {
                int damage = ownplay ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
                p.minionGetDamageOrHeal(target, damage);
                if (target.Hp <= 0)
                {
                    foreach (Minion minion in ownplay ? p.ownMinions : p.enemyMinions)
                    {
                        p.minionGetBuffed(minion, 3, 3);
                        break;

                    }
                }
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
            };
        }

    }
}
