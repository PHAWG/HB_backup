using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 巫妖王 费用：3 攻击力：3 生命值：4
	//Liferender
	//生命撕裂者
	//<b>Battlecry:</b> If your hero's Health changed this turn, deal 6 damage to an enemy minion.
	//<b>战吼：</b>在本回合中，如果你的英雄的生命值发生变化，对一个敌方随从造成6点伤害。
	class Sim_TIME_614 : SimTemplate
	{
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                p.minionGetDamageOrHeal(target, 6);
            }
        }


        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_PLAYER_HEALTH_CHANGED_THIS_TURN),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
            };
        }

    }
}
