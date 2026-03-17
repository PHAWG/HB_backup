namespace HREngine.Bots
{
    partial class CardDB
    {

        #region 卡牌类型
        /// <summary>
        /// <value> 卡牌类型 </value>
        /// </summary>
        public enum cardtype
        {
            NONE = 0,
            GAME = 1,
            PLAYER = 2,
            HERO = 3,//英雄
            MOB = 4,//随从
            SPELL = 5,//法术
            ENCHANTMENT = 6,//增幅（例如：变形术，救赎，力量的代价，自然之力的附加效果）
            WEAPON = 7,//武器
            ITEM = 8,
            TOKEN = 9,
            HEROPWR = 10,//英雄技能
            BLANK = 11,
            GAME_MODE_BUTTON = 12,
            MOVE_MINION_HOVER_TARGET = 22,
            LETTUCE_ABILITY,
            BATTLEGROUND_HERO_BUDDY,//战旗伙伴
            LOCATION = 39,//地标
            BATTLEGROUND_QUEST_REWARD,//战旗奖励
            BATTLEGROUND_ANOMALY = 43,//战旗畸变
            BATTLEGROUND_SPELL = 42,//战旗法术
            BATTLEGROUND_TRINKET = 44,//战旗饰品
        }

        #endregion


        #region 卡牌效果

        /// <summary>
        /// 卡片效果
        /// </summary>
        public enum cardtrigers
        {
            /// <summary>
            /// 新触发
            /// </summary>
            newtriger,
            /// <summary>
            /// 战吼效果
            /// </summary>
            getBattlecryEffect,
            /// <summary>
            /// 一个英雄受到伤害触发
            /// </summary>
            onAHeroGotHealedTrigger,
            /// <summary>
            /// 随从受到伤害触发
            /// </summary>
            onAMinionGotHealedTrigger,//随从受到伤害触发
            /// <summary>
            /// 光环消失
            /// </summary>
            onAuraEnds,
            /// <summary>
            ///  光环开始
            /// </summary>
            onAuraStarts,
            /// <summary>
            /// 卡片即将使用
            /// </summary>
            onCardIsGoingToBePlayed,
            /// <summary>
            /// 卡片使用
            /// </summary>
            onCardPlay,
            /// <summary>
            /// 卡片使用后
            /// </summary>
            onCardWasPlayed,
            /// <summary>
            /// 亡语
            /// </summary>
            onDeathrattle,
            /// <summary>
            /// 激怒开始
            /// </summary>
            onEnrageStart,
            /// <summary>
            /// 激怒结束
            /// </summary>
            onEnrageStop,
            /// <summary>
            /// 随从死亡触发
            /// </summary>
            onMinionDiedTrigger,
            /// <summary>
            /// 随从受到伤害触发
            /// </summary>
            onMinionGotDmgTrigger,
            /// <summary>
            /// 随从被召唤
            /// </summary>
            onMinionIsSummoned,
            /// <summary>
            /// 随从召唤过
            /// </summary>
            onMinionWasSummoned,
            /// <summary>
            /// 奥秘使用
            /// </summary>
            onSecretPlay,
            /// <summary>
            /// 回合结束触发
            /// </summary>
            onTurnEndsTrigger,
            /// <summary>
            /// 回合开始触发
            /// </summary>
            onTurnStartTrigger,
            /// <summary>
            /// 触发激发
            /// </summary>
            triggerInspire,
            /// <summary>
            /// 超杀
            /// </summary>
            OnOverkill,
            /// <summary>
            /// 荣耀击杀
            /// </summary>
            OnHonorableKill,
            /// <summary>
            /// 消灭
            /// </summary>
            xiaomie,
            /// <summary>
            /// 回合开始
            /// </summary>
            onTurnStart,
            /// <summary>
            /// 回合结束
            /// </summary>
            onTurnEnd,
            /// <summary>
            /// 使用地标
            /// </summary>
            useLocation,
            /// <summary>
            /// 使用泰坦技能
            /// </summary>
            useTitanAbility,
            /// <summary>
            /// 当本随从攻击时
            /// </summary>
            onMinionAttack,
            /// <summary>
            /// 当本随从攻击后
            /// </summary>
            afterMinionAttack,
            /// <summary>
            /// 当随从牌打出后
            /// </summary>
            onCardIsAfterToBePlayed,
        }
        #endregion

        #region 法术派系
        /// <summary>
        /// <value> 法术派系 </value>
        /// </summary>
        public enum SpellSchool
        {   /// <summary>
            /// <value> 法术派系 </value>
            /// </summary>
            NONE = 0,
            /// <summary>
            /// <value> 奥术 </value>
            /// </summary>
            ARCANE = 1,
            /// <summary>
            /// <value> 火焰 </value>
            /// </summary>
            FIRE = 2,
            /// <summary>
            /// <value> 冰霜 </value>
            /// </summary>
            FROST = 3,
            /// <summary>
            /// <value> 自然 </value>
            /// </summary>
            NATURE = 4,
            /// <summary>
            /// <value> 神圣 </value>
            /// </summary>
            HOLY = 5,
            /// <summary>
            /// <value> 暗影 </value>
            /// </summary>
            SHADOW = 6,
            /// <summary>
            /// <value> 邪能 </value>
            /// </summary>
            FEL = 7,
            /// <summary>
            /// <value> 无法术派系 </value>
            /// </summary>
            PHYSICAL_COMBAT = 8,
        }
        #endregion

        #region 种族
        /// <summary>
        /// <value> 种族 </value>
        /// </summary>
        public enum Race
        {
            INVALID = 0,
            /// <summary>
            /// <value> 血精灵 </value>
            /// </summary>
            BLOODELF = 1,
            /// <summary>
            /// <value> 德莱尼 </value>
            /// </summary>
            DRAENEI = 2,
            /// <summary>
            /// <value> 矮人 </value>
            /// </summary>
            DWARF = 3,
            /// <summary>
            /// <value> 侏儒 </value>
            /// </summary>
            GNOME = 4,
            /// <summary>
            /// <value> 地精 </value>
            /// </summary>
            GOBLIN = 5,
            /// <summary>
            /// <value> 人类 </value>
            /// </summary>
            HUMAN = 6,
            /// <summary>
            /// <value> 暗夜精灵 </value>
            /// </summary>
            NIGHTELF = 7,
            /// <summary>
            /// <value> 兽人 </value>
            /// </summary>
            ORC = 8,
            /// <summary>
            /// <value> 牛头人 </value>
            /// </summary>
            TAUREN = 9,
            /// <summary>
            /// <value> 巨魔 </value>
            /// </summary>
            TROLL = 10,
            /// <summary>
            /// <value> 亡灵 </value>
            /// </summary>
            UNDEAD = 11,
            /// <summary>
            /// <value> 狼人 </value>
            /// </summary>
            WORGEN = 12,
            /// <summary>
            /// <value> 地精2 </value>
            /// </summary>
            GOBLIN2 = 13,
            /// <summary>
            /// <value> 鱼人 </value>
            /// </summary>
            MURLOC = 14,
            /// <summary>
            /// <value> 恶魔 </value>
            /// </summary>
            DEMON = 15,
            /// <summary>
            /// <value> 天灾 </value>
            /// </summary>
            SCOURGE = 16,
            /// <summary>
            /// <value> 机械 </value>
            /// </summary>
            MECHANICAL = 17,
            /// <summary>
            /// <value> 元素 </value>
            /// </summary>
            ELEMENTAL = 18,
            /// <summary>
            /// <value> 食人魔 </value>
            /// </summary>
            OGRE = 19,
            /// <summary>
            /// <value> 野兽 </value>
            /// </summary>
            PET = 20,
            /// <summary>
            /// <value> 图腾 </value>
            /// </summary>
            TOTEM = 21,
            /// <summary>
            /// <value> 蛛魔 </value>
            /// </summary>
            NERUBIAN = 22,
            /// <summary>
            /// <value> 海盗 </value>
            /// </summary>
            PIRATE = 23,
            /// <summary>
            /// <value> 龙 </value>
            /// </summary>
            DRAGON = 24,
            /// <summary>
            /// <value> 无种族 </value>
            /// </summary>
            BLANK = 25,
            /// <summary>
            /// <value> 全部 </value>
            /// </summary>
            ALL = 26,
            /// <summary>
            /// <value> 蛋  </value>
            /// </summary>
            EGG = 38,
            /// <summary>
            /// <value> 野猪人 </value>
            /// </summary>
            QUILBOAR = 43,
            /// <summary>
            /// <value> 纳迦 </value>
            /// </summary>
            NAGA = 92,
        }
        #endregion



        #region 特殊标签

        /// <summary>
        /// 特殊标签
        /// </summary>
        public enum Specialtags
        {
            /// <summary>具有种族类型</summary>
            CardRace = 200,
            /// <summary>跟班</summary>
            markOfEvil = 994,
            /// <summary>军情七处</summary>
            SI_7 = 1678,
            /// <summary>树人</summary>
            Treant = 2831,
            /// <summary>古树</summary>
            Ancient = 2871,
            /// <summary>小鬼</summary>
            IMP = 1965,
            /// <summary>雏龙</summary>
            Whelp = 2355,
            /// <summary>白银之手新兵</summary>
            SilverHandRecruit = 3444,
            /// <summary>星舰组件</summary>
            StarshipPiece = 3631,
            /// <summary>星舰</summary>
            Starship = 3555,
            /// <summary>星灵</summary>
            Protoss = 3469,
            /// <summary>小精灵</summary>
            Wisp = 3881,
            /// <summary>拉法姆</summary>
            Rafaam = 3928,
            /// <summary>伊瑟拉</summary>
            Ysera = 4392,
        };
        #endregion


        #region 异常类型
        /// <summary>
        /// <value> 异常类型 </value>
        /// </summary>
        /// <summary>
        /// 定义了一个枚举类型 ErrorType2，用于表示各种错误或条件类型。
        /// 每个枚举值代表一种特定的错误或条件，并附带简要说明。
        /// </summary>
        public enum ErrorType2
        {
            /// <summary>
            /// 表示无效的状态或操作。
            /// </summary>
            INVALID = -1,

            /// <summary>
            /// 表示无异常或默认状态。
            /// </summary>
            NONE = 0,

            /// <summary>
            /// 要求目标必须是随从。
            /// </summary>
            REQ_MINION_TARGET = 1,

            /// <summary>
            /// 要求目标必须是友方单位。
            /// </summary>
            REQ_FRIENDLY_TARGET = 2,

            /// <summary>
            /// 要求目标必须是敌方单位。
            /// </summary>
            REQ_ENEMY_TARGET = 3,

            /// <summary>
            /// 要求目标必须是受伤的随从。
            /// </summary>
            REQ_DAMAGED_TARGET = 4,

            /// <summary>
            /// 要求达到最大奥秘数量限制。
            /// </summary>
            REQ_MAX_SECRETS = 5,

            /// <summary>
            /// 要求目标必须是被冻结的随从。
            /// </summary>
            REQ_FROZEN_TARGET = 6,

            /// <summary>
            /// 要求目标具有冲锋能力。
            /// </summary>
            REQ_CHARGE_TARGET = 7,

            /// <summary>
            /// 要求目标的攻击力小于等于指定值。
            /// </summary>
            REQ_TARGET_MAX_ATTACK = 8,

            /// <summary>
            /// 要求目标不能是自身英雄。
            /// </summary>
            REQ_NONSELF_TARGET = 9,

            /// <summary>
            /// 要求目标必须是指定种族的单位。
            /// </summary>
            REQ_TARGET_WITH_RACE = 10,

            /// <summary>
            /// 要求必须有目标才能执行操作。
            /// </summary>
            REQ_TARGET_TO_PLAY = 11,

            /// <summary>
            /// 要求随从槽位满足一定数量。
            /// </summary>
            REQ_NUM_MINION_SLOTS = 12,

            /// <summary>
            /// 要求必须装备武器才能使用。
            /// </summary>
            REQ_WEAPON_EQUIPPED = 13,

            /// <summary>
            /// 要求拥有足够的法力值。
            /// </summary>
            REQ_ENOUGH_MANA = 14,

            /// <summary>
            /// 要求必须在自己的回合。
            /// </summary>
            REQ_YOUR_TURN = 15,

            /// <summary>
            /// 要求目标不能处于潜行状态。
            /// </summary>
            REQ_NONSTEALTH_ENEMY_TARGET = 16,

            /// <summary>
            /// 要求目标必须是英雄。
            /// </summary>
            REQ_HERO_TARGET = 17,

            /// <summary>
            /// 要求奥秘区域未达到上限。
            /// </summary>
            REQ_SECRET_ZONE_CAP = 18,

            /// <summary>
            /// 要求随从数量未达到上限（如果有目标）。
            /// </summary>
            REQ_MINION_CAP_IF_TARGET_AVAILABLE = 19,

            /// <summary>
            /// 要求随从数量满足指定条件。
            /// </summary>
            REQ_MINION_CAP = 20,

            /// <summary>
            /// 要求目标在本回合内进行过攻击。
            /// </summary>
            REQ_TARGET_ATTACKED_THIS_TURN = 21,

            /// <summary>
            /// 允许在无目标时使用（如抉择卡牌）。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE = 22,

            /// <summary>
            /// 要求敌方场上至少有指定数量的随从。
            /// </summary>
            REQ_MINIMUM_ENEMY_MINIONS = 23,

            /// <summary>
            /// 连击效果需要指定目标。
            /// </summary>
            REQ_TARGET_FOR_COMBO = 24,

            /// <summary>
            /// 要求剩余法力水晶满足条件。
            /// </summary>
            REQ_NOT_EXHAUSTED_ACTIVATE = 25,

            /// <summary>
            /// 要求控制一个唯一的奥秘或任务。
            /// </summary>
            REQ_UNIQUE_SECRET_OR_QUEST = 26,

            /// <summary>
            /// 要求目标具有嘲讽能力。
            /// </summary>
            REQ_TARGET_TAUNTER = 27,

            /// <summary>
            /// 要求目标可以被攻击。
            /// </summary>
            REQ_CAN_BE_ATTACKED = 28,

            /// <summary>
            /// 要求英雄技能可以使用。
            /// </summary>
            REQ_ACTION_PWR_IS_MASTER_PWR = 29,

            /// <summary>
            /// 要求目标具有磁力特性。
            /// </summary>
            REQ_TARGET_MAGNET = 30,

            /// <summary>
            /// 要求目标攻击力大于0。
            /// </summary>
            REQ_ATTACK_GREATER_THAN_0 = 31,

            /// <summary>
            /// 要求攻击者未被冻结。
            /// </summary>
            REQ_ATTACKER_NOT_FROZEN = 32,

            /// <summary>
            /// 要求目标是英雄或随从。
            /// </summary>
            REQ_HERO_OR_MINION_TARGET = 33,

            /// <summary>
            /// 要求目标可以被法术指定。
            /// </summary>
            REQ_CAN_BE_TARGETED_BY_SPELLS = 34,

            /// <summary>
            /// 当前用途暂不明确。
            /// </summary>
            REQ_SUBCARD_IS_PLAYABLE = 35,

            /// <summary>
            /// 连击效果无需指定目标。
            /// </summary>
            REQ_TARGET_FOR_NO_COMBO = 36,

            /// <summary>
            /// 要求未控制其他随从。
            /// </summary>
            REQ_NOT_MINION_JUST_PLAYED = 37,

            /// <summary>
            /// 要求未使用英雄技能。
            /// </summary>
            REQ_NOT_EXHAUSTED_HERO_POWER = 38,

            /// <summary>
            /// 允许对手指定目标。
            /// </summary>
            REQ_CAN_BE_TARGETED_BY_OPPONENTS = 39,

            /// <summary>
            /// 要求攻击者可以发起攻击。
            /// </summary>
            REQ_ATTACKER_CAN_ATTACK = 40,

            /// <summary>
            /// 要求目标攻击力大于等于指定值。
            /// </summary>
            REQ_TARGET_MIN_ATTACK = 41,

            /// <summary>
            /// 要求目标可以被英雄技能指定。
            /// </summary>
            REQ_CAN_BE_TARGETED_BY_HERO_POWERS = 42,

            /// <summary>
            /// 要求敌方目标不具有免疫能力。
            /// </summary>
            REQ_ENEMY_TARGET_NOT_IMMUNE = 43,

            /// <summary>
            /// 要求全场没有随从存在。
            /// </summary>
            REQ_ENTIRE_ENTOURAGE_NOT_IN_PLAY = 44,

            /// <summary>
            /// 要求总随从数量至少达到指定值。
            /// </summary>
            REQ_MINIMUM_TOTAL_MINIONS = 45,

            /// <summary>
            /// 要求目标必须是嘲讽随从。
            /// </summary>
            REQ_MUST_TARGET_TAUNTER = 46,

            /// <summary>
            /// 要求目标未受伤。
            /// </summary>
            REQ_UNDAMAGED_TARGET = 47,

            /// <summary>
            /// 要求目标可以被战吼效果指定。
            /// </summary>
            REQ_CAN_BE_TARGETED_BY_BATTLECRIES = 48,

            /// <summary>
            /// 猎人英雄技能“稳固射击”的特殊要求。
            /// </summary>
            REQ_STEADY_SHOT = 49,

            /// <summary>
            /// 要求目标是随从或敌方英雄。
            /// </summary>
            REQ_MINION_OR_ENEMY_HERO = 50,

            /// <summary>
            /// 要求手中有龙牌才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND = 51,

            /// <summary>
            /// 要求目标必须是传说随从。
            /// </summary>
            REQ_LEGENDARY_TARGET = 52,

            /// <summary>
            /// 要求本回合有友方随从死亡。
            /// </summary>
            REQ_FRIENDLY_MINION_DIED_THIS_TURN = 53,

            /// <summary>
            /// 要求本局游戏中有友方随从死亡。
            /// </summary>
            REQ_FRIENDLY_MINION_DIED_THIS_GAME = 54,

            /// <summary>
            /// 要求敌方英雄已装备武器。
            /// </summary>
            REQ_ENEMY_WEAPON_EQUIPPED = 55,

            /// <summary>
            /// 要求友方随从数量满足最低要求才可使用。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS = 56,

            /// <summary>
            /// 要求目标必须是战吼随从。
            /// </summary>
            REQ_TARGET_WITH_BATTLECRY = 57,

            /// <summary>
            /// 要求目标必须是亡语随从。
            /// </summary>
            REQ_TARGET_WITH_DEATHRATTLE = 58,

            /// <summary>
            /// 要求奥秘数量满足最低要求才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS = 59,

            /// <summary>
            /// 要求奥秘数未达到区域上限。
            /// </summary>
            REQ_SECRET_ZONE_CAP_FOR_NON_SECRET = 60,

            /// <summary>
            /// 要求目标费用等于指定值。
            /// </summary>
            REQ_TARGET_EXACT_COST = 61,

            /// <summary>
            /// 要求目标处于潜行状态。
            /// </summary>
            REQ_STEALTHED_TARGET = 62,

            /// <summary>
            /// 要求场上可以放置随从且法力水晶未满。
            /// </summary>
            REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT = 63,

            /// <summary>
            /// 要求任务数量满足最低要求。
            /// </summary>
            REQ_MAX_QUESTS = 64,

            /// <summary>
            /// 要求上一回合打出过元素牌才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN = 65,

            /// <summary>
            /// 要求目标不是吸血鬼。
            /// </summary>
            REQ_TARGET_NOT_VAMPIRE = 66,

            /// <summary>
            /// 要求目标只能受到武器伤害。
            /// </summary>
            REQ_TARGET_NOT_DAMAGEABLE_ONLY_BY_WEAPONS = 67,

            /// <summary>
            /// 要求英雄技能未被禁用。
            /// </summary>
            REQ_NOT_DISABLED_HERO_POWER = 68,

            /// <summary>
            /// 要求必须先使用其他卡牌。
            /// </summary>
            REQ_MUST_PLAY_OTHER_CARD_FIRST = 69,

            /// <summary>
            /// 要求手牌未满。
            /// </summary>
            REQ_HAND_NOT_FULL = 70,

            /// <summary>
            /// 要求必须有目标才能使用。
            /// </summary>
            REQ_TARGET_TO_PLAY2 = 75,

            /// <summary>
            /// 要求目标法术不含自然属性。
            /// </summary>
            REQ_TARGET_NO_NATURE = 77,

            /// <summary>
            /// 要求目标不可播放。
            /// </summary>
            REQ_LITERALLY_UNPLAYABLE,

            /// <summary>
            /// 如果目标可用且英雄有攻击力，则请求目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_HERO_HAS_ATTACK,

            /// <summary>
            /// 要求本回合有对应种族的友方随从死亡。
            /// </summary>
            REQ_FRIENDLY_MINION_OF_RACE_DIED_THIS_TURN,

            /// <summary>
            /// 要求本回合打出过足够数量的法术才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_SPELLS_PLAYED_THIS_TURN,

            /// <summary>
            /// 要求手牌中有同种族友方随从。
            /// </summary>
            REQ_FRIENDLY_MINION_OF_RACE_IN_HAND,

            /// <summary>
            /// 要求本局游戏有友方亡语随从死亡。
            /// </summary>
            REQ_FRIENDLY_DEATHRATTLE_MINION_DIED_THIS_GAME = 86,

            /// <summary>
            /// 要求本局游戏有友方复生随从死亡。
            /// </summary>
            REQ_FRIENDLY_REBORN_MINION_DIED_THIS_GAME = 89,

            /// <summary>
            /// 要求本局游戏有随从死亡。
            /// </summary>
            REQ_MINION_DIED_THIS_GAME,

            /// <summary>
            /// 要求战场未完全满员。
            /// </summary>
            REQ_BOARD_NOT_COMPLETELY_FULL = 92,

            /// <summary>
            /// 要求有过载的法力水晶才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_HAS_OVERLOADED_MANA,

            /// <summary>
            /// 要求英雄本回合已攻击才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_HERO_ATTACKED_THIS_TURN,

            /// <summary>
            /// 要求本回合抽过牌才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_DRAWN_THIS_TURN,

            /// <summary>
            /// 要求本回合未抽过牌才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_NOT_DRAWN_THIS_TURN,

            /// <summary>
            /// 要求目标不是三倍复制的随从。
            /// </summary>
            REQ_TARGET_NON_TRIPLED_MINION,

            /// <summary>
            /// 要求本回合购买过随从。
            /// </summary>
            REQ_BOUGHT_MINION_THIS_TURN,

            /// <summary>
            /// 要求本回合出售过随从。
            /// </summary>
            REQ_SOLD_MINION_THIS_TURN,

            /// <summary>
            /// 要求玩家生命值本回合发生变化才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_PLAYER_HEALTH_CHANGED_THIS_TURN,

            /// <summary>
            /// 要求牌库中有灵魂碎片才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_SOUL_FRAGMENT_IN_DECK,

            /// <summary>
            /// 要求目标未受伤（除非连击）。
            /// </summary>
            REQ_DAMAGED_TARGET_UNLESS_COMBO,

            /// <summary>
            /// 要求目标不处于休眠状态。
            /// </summary>
            REQ_NOT_MINION_DORMANT,

            /// <summary>
            /// 要求目标不可被触碰。
            /// </summary>
            REQ_TARGET_NOT_UNTOUCHABLE,

            /// <summary>
            /// 要求本回合购买过指定种族的随从才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_BOUGHT_RACE_THIS_TURN,

            /// <summary>
            /// 要求本回合出售过指定种族的随从才可指定目标。
            /// </summary>
            REQ_TARGET_IF_AVAILABLE_AND_SOLD_RACE_THIS_TURN,

            /// <summary>
            /// 要求不在冷却时间内。
            /// </summary>
            REQ_NOT_IN_COOLDOWN,

            /// <summary>
            /// 要求目标是雇佣兵。
            /// </summary>
            REQ_TARGET_IS_MERC,

            /// <summary>
            /// 要求目标不是雇佣兵。
            /// </summary>
            REQ_TARGET_IS_NON_MERC,

            /// <summary>
            /// 要求两个不同种族的目标。
            /// </summary>
            REQ_TWO_OF_A_KIND,

            /// <summary>
            /// 要求存在法力过载。
            /// </summary>
            REQ_HAS_OVERLOADED_MANA,

            /// <summary>
            /// 要求目标不能是拥有者的莱特切能力。
            /// </summary>
            REQ_LETTUCE_ABILITY_CANNOT_TARGET_OWNER,

            /// <summary>
            /// 要求目标不具有指定标签。
            /// </summary>
            REQ_TARGET_NOT_HAVE_TAG = 116,

            /// <summary>
            /// 要求目标必须具有指定标签。
            /// </summary>
            REQ_TARGET_MUST_HAVE_TAG,

            /// <summary>
            /// 要求目标可交易。
            /// </summary>
            REQ_TRADEABLE = 119,

            /// <summary>
            /// 要求目标不是传说随从。
            /// </summary>
            REQ_NOT_LEGENDARY_TARGET = 123,

            /// <summary>
            /// 要求酒馆等级满足最低要求才可使用。
            /// </summary>
            REQ_MINIMUM_TAVERN_TIER_LEVEL_TO_PLAY = 128,

            /// <summary>
            /// 要求卡牌酒馆等级满足条件才可使用。
            /// </summary>
            REQ_CARD_TAVERN_TIER_LEVEL_TO_PLAY,

            /// <summary>
            /// 要求地点未处于疲劳状态。
            /// </summary>
            REQ_NOT_EXHAUSTED_LOCATION,

            /// <summary>
            /// 要求目标是地标。
            /// </summary>
            REQ_LOCATION_TARGET,

            /// <summary>
            /// 要求目标是白银之手新兵。
            /// </summary>
            REQ_TARGET_SILVER_HAND_RECRUIT,

            /// <summary>
            /// 要求残骸数量满足最低要求。
            /// </summary>
            REQ_MINIMUM_CORPSES,

            /// <summary>
            /// 要求目标是地点或随从。
            /// </summary>
            REQ_LOCATION_OR_MINION_TARGET,

            /// <summary>
            /// 要求目标可以被地点指定。
            /// </summary>
            REQ_CAN_BE_TARGETED_BY_LOCATIONS,

            /// <summary>
            /// 要求锻造条件满足。
            /// </summary>
            REQ_FORGE,

            /// <summary>
            /// 要求目标费用不超过指定值。
            /// </summary>
            REQ_TARGET_MAX_COST,

            /// <summary>
            /// 要求本局游戏打出过法术。
            /// </summary>
            REQ_HAS_PLAYED_SPELL_THIS_GAME,

            /// <summary>
            /// 要求目标不是泰坦。
            /// </summary>
            REQ_TARGET_IS_NON_TITAN = 141,

            /// <summary>
            /// 要求双人通行证可用。
            /// </summary>
            REQ_BACON_DUO_PASSABLE,

            /// <summary>
            /// 要求目标攻击力精确匹配指定值。
            /// </summary>
            REQ_TARGET_EXACT_ATTACK,

            /// <summary>
            /// 要求敌方非金色随从数量满足最低要求。
            /// </summary>
            REQ_MINIMUM_NON_GOLDEN_ENEMY_MINIONS = 146,

            /// <summary>
            /// 要求本局游戏有友方嘲讽随从死亡。
            /// </summary>
            REQ_FRIENDLY_TAUNT_MINION_DIED_THIS_GAME = 148,

            /// <summary>
            /// 要求目标本回合触发过回合结束效果。
            /// </summary>
            REQ_TARGET_HAS_END_OF_TURN_POWER_TO_TRIGGER_THIS_TURN = 152,

            /// <summary>
            /// 要求游戏回合数满足最低要求。
            /// </summary>
            REQ_MINIMUM_GAME_TURN,

            /// <summary>
            /// 要求敌方场上有指定种族的随从。
            /// </summary>
            REQ_ENEMY_MINION_OF_RACE_IN_PLAY,

            /// <summary>
            /// 要求目标不在战场上。
            /// </summary>
            REQ_TARGET_NOT_IN_PLAY,

            /// <summary>
            /// 要求敌方目标不受火焰法术免疫。
            /// </summary>
            REQ_ENEMY_TARGET_NOT_IMMUNE_TO_FIRE_SPELLS,

            /// <summary>
            /// 要求来源必须具有指定标签。
            /// </summary>
            REQ_SOURCE_MUST_HAVE_TAG = 158,

            /// <summary>
            /// 要求回溯界面未显示时才可使用。
            /// </summary>
            REQ_CANNOT_USE_WHILE_REWIND_UI_DISPLAYED,

            /// <summary>
            /// 要求通过拖拽方式使用卡牌。
            /// </summary>
            REQ_DRAG_TO_PLAY,
        }

        #endregion


    }
}