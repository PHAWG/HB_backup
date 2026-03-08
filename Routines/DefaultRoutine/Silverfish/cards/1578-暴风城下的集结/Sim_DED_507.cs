using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DED_507 : SimTemplate //* 鸦巢观察员 Crow's Nest Lookout
    {
        //战吼：对最左边和最右边的敌方随从造成2点伤害。
        //Battlecry: Deal 2 damage to the left and right-most enemy minions.
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {   
            List<Minion> minions = own.own ? p.enemyMinions : p.ownMinions;
            if(minions.Count >0){
                //for (int i = 0; i < minions.Count; i++)
                //{
                //    Minion minion = minions[i];
                //    if (minion.zonepos == 1)
                //        p.minionGetDamageOrHeal(minion, 2);
                //    if (minion.zonepos == minions.Count)
                //        p.minionGetDamageOrHeal(minion, 2);
                //}
                foreach (Minion minion in minions.ToArray()){
                    if(minion.zonepos == 1)
                        p.minionGetDamageOrHeal(minion, 2);
                    if (minion.zonepos == minions.Count)
                        p.minionGetDamageOrHeal(minion, 2);
                }
            }

        }

    }
}
