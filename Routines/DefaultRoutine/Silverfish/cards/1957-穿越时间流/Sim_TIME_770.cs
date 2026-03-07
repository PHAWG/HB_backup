using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 牧师 费用：4
	//Fast Forward
	//快进
	//Draw 2 cards.Pick one to have its Cost reduced by (2).
	//抽两张牌，从中选择一张，使其法力值消耗减少（2）点。
	class Sim_TIME_770 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
            p.drawACard(CardDB.cardIDEnum.None, ownplay);

        }

    }
}
