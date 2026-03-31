using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 巫妖王 费用：8
	//Chow Down
	//投喂加餐
	//[x]Summon five 5/4 UndeadDrakes. Spend 8 <b>Corpses</b>to give them <b>Rush</b>.
	//召唤五条5/4的亡灵幼龙。消耗8份<b>残骸</b>，使其获得<b>突袭</b>。
	class Sim_CATA_465 : SimTemplate
	{
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CATA_465t);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
            p.callKid(kid, pos, ownplay, false);
            p.callKid(kid, pos, ownplay);
            p.callKid(kid, pos, ownplay);
            p.callKid(kid, pos, ownplay);
            p.callKid(kid, pos, ownplay);

        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }

    }
}
