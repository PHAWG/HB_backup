using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 恶魔猎手 费用：4 攻击力：2 生命值：6
	//Timeway Warden
	//时间流守望者
	//[x]<b>Battlecry:</b> Imprison anenemy minion. It goes<b>Dormant</b> for 10,000 turns.___<b>Deathrattle:</b> Awaken it.
	//<b>战吼：</b>囚禁一个敌方随从，使其<b>休眠</b>10000回合。<b>亡语：</b>唤醒该随从。
	class Sim_TIME_442 : SimTemplate
	{
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null)
            {
                target.dormant = 10000;

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
