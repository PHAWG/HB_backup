using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 潜行者 费用：4 攻击力：2 生命值：3
	//Cleansing Lightspawn
	//净化的光耀之子
	//[x]<b>Lifesteal</b><b>Battlecry:</b> Deal damageto an enemy minion equal___to this minion's Health.
	//<b>吸血</b>。<b>战吼：</b>对一个敌方随从造成等同于本随从生命值的伤害。
	class Sim_TIME_427 : SimTemplate
	{
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                p.minionGetDamageOrHeal(target, own.Hp);
            }
        }


        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
            };
        }

    }
}
