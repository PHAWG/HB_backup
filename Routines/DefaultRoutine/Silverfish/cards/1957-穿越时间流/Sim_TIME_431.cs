using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 潜行者 费用：2 攻击力：1 生命值：4
	//Amber Priestess
	//琥珀女祭司
	//[x]<b>Taunt</b><b>Battlecry:</b> Restore Healthto a character equal tothis minion's Health.
	//<b>嘲讽</b>。<b>战吼：</b>为一个角色恢复等同于本随从生命值的生命值。
	class Sim_TIME_431 : SimTemplate
	{
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                p.minionGetDamageOrHeal(target, -own.Hp);
            }
        }


        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
    }
}
