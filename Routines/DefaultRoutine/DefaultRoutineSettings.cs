using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using log4net;
using Newtonsoft.Json;
using Triton.Bot.Settings;
using Triton.Common;
using Triton.Game.Mapping;
using Logger = Triton.Common.LogUtilities.Logger;

using Triton.Bot;
using Triton.Game;
using Triton.Game.Data;

namespace HREngine.Bots
{
    public class DefaultRoutineSettings : JsonSettings
    {
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();

        private static DefaultRoutineSettings _instance;

        public static DefaultRoutineSettings Instance
        {
            get { return _instance ?? (_instance = new DefaultRoutineSettings()); }
        }

        public DefaultRoutineSettings()
            : base(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "DefaultRoutine")))
        {
            Ai.Instance.setMaxWide(_maxWide);
            Ai.Instance.setMaxDeep(_maxDeep);
            Ai.Instance.setMaxCal(_maxCal);
            Helpfunctions.Instance.writelogg = !_setLog;
            printUtils.emoteMode = _defaultEmote;
            printUtils.printNextMove = _usePrintNextMove;
            printUtils.printPentity = _usePrintPenalties;
            printUtils.enfaceReward = _enfaceReward;
        }

        public void ReloadFile()
        {
            Reload(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "DefaultRoutine" + GetMyHashCode())));
            Ai.Instance.setMaxWide(_maxWide);
            Ai.Instance.setMaxDeep(_maxDeep);
            Ai.Instance.setMaxCal(_maxCal);
            Helpfunctions.Instance.writelogg = !_setLog;
            printUtils.emoteMode = _defaultEmote;
            printUtils.printNextMove = _usePrintNextMove;
            printUtils.printPentity = _usePrintPenalties;
            printUtils.enfaceReward = _enfaceReward;
            OnPropertyChanged(string.Empty);
            if (CommandLine.Arguments.Exists("behavior"))
            {
                string[] name =
                {
                    "丨通用丨不设惩罚",
                    "丨通用丨暗牧",
                    "丨通用丨酸鱼人萨",
                    "丨标准丨快攻DK",
                    "丨标准丨酸快攻德",
                    "丨标准丨元素法",
                    "丨标准丨元素萨",
                    "丨狂野丨奥秘法",
                    "丨狂野丨剑鱼贼",
                    "丨狂野丨偶数萨",
                    "丨狂野丨快攻暗牧",
                    "丨狂野丨锁喉剑鱼贼",
                    "丨过时丨任务海盗战",
                    "丨狂野丨宇宙兽王猎",
                };
                DefaultBehavior = name[int.Parse(CommandLine.Arguments.Single("behavior"))];
                Log.ErrorFormat("[中控设置] 天梯对战策略 = {0}.", DefaultBehavior);
            }
        }

        private TAG_CLASS _arenaPreferredClass1;
        private TAG_CLASS _arenaPreferredClass2;
        private TAG_CLASS _arenaPreferredClass3;
        private TAG_CLASS _arenaPreferredClass4;
        private TAG_CLASS _arenaPreferredClass5;
        private string _defaultBehavior;

        [DefaultValue(TAG_CLASS.HUNTER)]
        public TAG_CLASS ArenaPreferredClass1
        {
            get { return _arenaPreferredClass1; }
            set
            {
                if (!value.Equals(_arenaPreferredClass1))
                {
                    _arenaPreferredClass1 = value;
                    NotifyPropertyChanged(() => ArenaPreferredClass1);

                }
            }
        }

        [DefaultValue(TAG_CLASS.WARLOCK)]
        public TAG_CLASS ArenaPreferredClass2
        {
            get { return _arenaPreferredClass2; }
            set
            {
                if (!value.Equals(_arenaPreferredClass2))
                {
                    _arenaPreferredClass2 = value;
                    NotifyPropertyChanged(() => ArenaPreferredClass2);
                }
            }
        }

        [DefaultValue(TAG_CLASS.PRIEST)]
        public TAG_CLASS ArenaPreferredClass3
        {
            get { return _arenaPreferredClass3; }
            set
            {
                if (!value.Equals(_arenaPreferredClass3))
                {
                    _arenaPreferredClass3 = value;
                    NotifyPropertyChanged(() => ArenaPreferredClass3);
                }
            }
        }

        [DefaultValue(TAG_CLASS.ROGUE)]
        public TAG_CLASS ArenaPreferredClass4
        {
            get { return _arenaPreferredClass4; }
            set
            {
                if (!value.Equals(_arenaPreferredClass4))
                {
                    _arenaPreferredClass4 = value;
                    NotifyPropertyChanged(() => ArenaPreferredClass4);
                }
            }
        }

        [DefaultValue(TAG_CLASS.WARRIOR)]
        public TAG_CLASS ArenaPreferredClass5
        {
            get { return _arenaPreferredClass5; }
            set
            {
                if (!value.Equals(_arenaPreferredClass5))
                {
                    _arenaPreferredClass5 = value;
                    NotifyPropertyChanged(() => ArenaPreferredClass5);
                }
            }
        }

        private ObservableCollection<TAG_CLASS> _allClasses;

        [JsonIgnore]
        public ObservableCollection<TAG_CLASS> AllClasses
        {
            get
            {
                return _allClasses ?? (_allClasses = new ObservableCollection<TAG_CLASS>
                {
                    TAG_CLASS.DEATHKNIGHT,
                    TAG_CLASS.DRUID,
                    TAG_CLASS.HUNTER,
                    TAG_CLASS.MAGE,
                    TAG_CLASS.PALADIN,
                    TAG_CLASS.PRIEST,
                    TAG_CLASS.ROGUE,
                    TAG_CLASS.SHAMAN,
                    TAG_CLASS.WARLOCK,
                    TAG_CLASS.WARRIOR,
                    TAG_CLASS.DEMONHUNTER,
                });
            }
        }

        [DefaultValue("丨通用丨不设惩罚")]
        public string DefaultBehavior
        {
            get { return _defaultBehavior; }
            set
            {
                if (!value.Equals(_defaultBehavior))
                {
                    _defaultBehavior = value;
                    NotifyPropertyChanged(() => DefaultBehavior);
                }
                Log.InfoFormat("[默认策略设置] 天梯对战策略 = {0}.", _defaultBehavior);
            }
        }

        private ObservableCollection<string> _allBehav;

        [JsonIgnore]
        public ObservableCollection<string> AllBehav
        {
            get
            {
                return _allBehav ?? (_allBehav = new ObservableCollection<string>(Silverfish.Instance.BehaviorDB.Keys));
            }
        }

        private string _defaultEmote = "无";
        [DefaultValue("无")]
        public string DefaultEmote
        {
            get { return _defaultEmote; }
            set
            {
                if (!value.Equals(_defaultEmote))
                {
                    _defaultEmote = value;
                    printUtils.emoteMode = value;
                    NotifyPropertyChanged(() => DefaultEmote);
                }
            }
        }

        private ObservableCollection<string> AllEmotes;

        [JsonIgnore]
        public ObservableCollection<string> AllEmote
        {
            get
            {
                return AllEmotes ?? (AllEmotes = new ObservableCollection<string>() { "无", "友善模式", "嘴臭模式", "乞讨模式", "摊牌了我是脚本", "精神污染模式", "抱歉" });
            }
        }

        private readonly List<int> _questIdsToCancel = new List<int>();

        [JsonIgnore]
        public List<int> QuestIdsToCancel
        {
            get { return _questIdsToCancel; }
        }

        private int _maxWide = 3000;

        [DefaultValue(3000)]
        public int MaxWide
        {
            get { return _maxWide; }
            set
            {
                if (!value.Equals(_maxWide))
                {
                    _maxWide = value;
                    Ai.Instance.setMaxWide(value);
                    NotifyPropertyChanged(() => MaxWide);
                }
                Log.InfoFormat("[默认策略设置] AI值 = {0}.", _maxWide);
            }
        }

        private int _maxDeep = 12;

        [DefaultValue(12)]
        public int MaxDeep
        {
            get { return _maxDeep; }
            set
            {
                if (!value.Equals(_maxDeep))
                {
                    _maxDeep = value;
                    Ai.Instance.setMaxDeep(value);
                    NotifyPropertyChanged(() => MaxDeep);
                }
                Log.InfoFormat("[默认策略设置] 最大考虑步数 = {0}.", _maxDeep);
            }
        }

        private int _maxCal = 60;

        [DefaultValue(60)]
        public int MaxCal
        {
            get { return _maxCal; }
            set
            {
                if (!value.Equals(_maxCal))
                {
                    _maxCal = value;
                    Ai.Instance.setMaxCal(value);
                    NotifyPropertyChanged(() => MaxCal);
                }
                Log.InfoFormat("[默认策略设置] 每步最大保留场面数 = {0}.", _maxCal);
            }
        }

        private bool _useSecretsPlayAround;

        [DefaultValue(false)]
        public bool UseSecretsPlayAround
        {
            get { return _useSecretsPlayAround; }
            set
            {
                if (!value.Equals(_useSecretsPlayAround))
                {
                    _useSecretsPlayAround = value;
                    NotifyPropertyChanged(() => UseSecretsPlayAround);

                }
                Log.InfoFormat("[默认策略设置] 防奥秘 = {0}.", _useSecretsPlayAround);
            }
        }

        private bool _setLog = true;

        [DefaultValue(true)]
        public bool SetLog
        {
            get { return _setLog; }
            set
            {
                if (!value.Equals(_setLog))
                {
                    _setLog = value;
                    Helpfunctions.Instance.writelogg = !value;
                    NotifyPropertyChanged(() => SetLog);
                }
                Log.InfoFormat("[默认策略设置] 不生成对战日志 = {0}.", !_setLog);
            }
        }

        private bool _usePrintNextMove;

        [DefaultValue(false)]
        public bool UsePrintNextMove
        {
            get { return _usePrintNextMove; }
            set
            {
                if (!value.Equals(_usePrintNextMove))
                {
                    _usePrintNextMove = value;
                    printUtils.printNextMove = value;
                    NotifyPropertyChanged(() => UsePrintNextMove);
                }
                Log.InfoFormat("[默认策略设置] 打印出牌惩罚 = {0}.", _usePrintNextMove);
            }
        }

        private bool _usePrintPenalties;

        [DefaultValue(false)]
        public bool UsePrintPenalties
        {
            get { return _usePrintPenalties; }
            set
            {
                if (!value.Equals(_usePrintPenalties))
                {
                    _usePrintPenalties = value;
                    printUtils.printPentity = value;
                    NotifyPropertyChanged(() => UsePrintPenalties);
                }
                Log.InfoFormat("[默认策略设置] 是否打印自定义惩罚 = {0}.", _usePrintPenalties);
            }
        }

        private bool _berserkIfCanFinishNextTour;

        [DefaultValue(false)]
        public bool BerserkIfCanFinishNextTour
        {
            get { return _berserkIfCanFinishNextTour; }
            set
            {
                if (!value.Equals(_berserkIfCanFinishNextTour))
                {
                    _berserkIfCanFinishNextTour = value;
                    NotifyPropertyChanged(() => BerserkIfCanFinishNextTour);

                }
                Log.InfoFormat("[默认策略设置] 下回合斩杀本回合打脸 = {0}.", _berserkIfCanFinishNextTour);
            }
        }

        private int _enfacehp = 27;

        [DefaultValue(27)]
        public int Enfacehp
        {
            get { return _enfacehp; }
            set
            {
                if (!value.Equals(_enfacehp))
                {
                    _enfacehp = value;
                    NotifyPropertyChanged(() => Enfacehp);

                }
                Log.InfoFormat("[默认策略设置] 打脸阈值 = {0}.", _enfacehp);
            }
        }

        private int _enfaceReward;

        [DefaultValue(0)]
        public int EnfaceReward
        {
            get { return _enfaceReward; }
            set
            {
                if (!value.Equals(_enfaceReward))
                {
                    _enfaceReward = value;
                    printUtils.enfaceReward = value;
                    NotifyPropertyChanged(() => EnfaceReward);

                }
                Log.InfoFormat("[默认策略设置] 打脸奖励 = {0}.", _enfaceReward);
            }
        }
    }
}
