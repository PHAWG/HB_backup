using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 潜行者 费用：2 攻击力：2 生命值：2
	//Fanboy
	//饭圈迷弟
	//[x]<b>Choose One -</b> Give a friendlyminion +2 Attack and <b>Rush</b>;or +2 Health and <b>Lifesteal</b>.
	//<b>抉择：</b>使一个友方随从获得+2攻击力和<b>突袭</b>；或者+2生命值和<b>吸血</b>。
	class Sim_JAM_027 : SimTemplate
	{
        public override void onCardPlay(Playfield p, Minion own, Minion target, int choice, Handmanager.Handcard hc)
        {
			if (target != null)
			{
				if (choice == 1 || p.ownFandralStaghelm > 0)
				{
					p.minionGetBuffed(target, 2, 0);
                    p.minionGetRush(target);

                }

                if (choice == 2 || p.ownFandralStaghelm > 0)
				{
                    p.minionGetBuffed(target, 0, 2);
					p.minionGetLifesteal(target);

                }
            }
        }
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }

    }
}
