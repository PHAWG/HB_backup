using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 巫妖王 费用：3
    //What Befell Zandalar
    //赞达拉的惨象
    //[x]Deal $2 damage to allenemies. Choose a Boonto give to Bwonsamdi.
    //对所有敌人造成$2点伤害。选择并使邦桑迪获得一项恩泽。
    class Sim_TIME_619t2 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            int damage = ownplay ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);
            p.allCharsOfASideGetDamage(!ownplay, damage);

        }

    }
}
