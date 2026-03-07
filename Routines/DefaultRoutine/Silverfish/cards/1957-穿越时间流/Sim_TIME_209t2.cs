using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 萨满祭司 费用：3
	//Avatar Form
	//天神下凡形态
	//[x]Give a friendly character+2 Attack and "After thisattacks, deal 2 damage toall enemies" this turn.
	//在本回合中，使一个友方角色获得+2攻击力和“在本角色攻击后，对所有敌人造成2点伤害”。
	class Sim_TIME_209t2 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice, Handmanager.Handcard hc)
        {
            if (target != null)
            {
                p.minionGetTempBuff(target, 2, 0);
                target.enchs.Add(CardDB.cardIDEnum.TIME_209t2e);
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
