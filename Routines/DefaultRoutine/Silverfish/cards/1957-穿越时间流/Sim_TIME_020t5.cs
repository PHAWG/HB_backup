using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 恶魔猎手 费用：0
	//Final Portal to Argus
	//最后一道阿古斯传送门
	//Summon a 4/1Demon for your opponent. When it dies, Broxigar reappears in your hand.
	//为你的对手召唤一个4/1的恶魔。当它死亡时，布洛克斯加会重新出现在你的手牌中。
	class Sim_TIME_020t5 : SimTemplate
	{
        // 2/1 恶魔的卡牌 ID
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.TIME_020t5t);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            // 为对手召唤随从
            int pos = ownplay ? p.enemyMinions.Count : p.ownMinions.Count;
            p.callKid(kid, pos, !ownplay);
        }

    }
}
