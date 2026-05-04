using System.Collections.ObjectModel;
using System.Windows.Input;
using Triton.Common.Mvvm;

namespace HREngine.Bots
{
    /// <summary>
    /// 默认策略 ViewModel
    /// <para>负责管理 DefaultRoutine（Silverfish AI）设置界面的数据绑定和交互逻辑</para>
    /// <para>作为 Settings 和 View 之间的桥梁，实现 MVVM 模式</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>AI 行为模式选择</description></item>
    ///     <item><description>自动表情选择</description></item>
    ///     <item><description>搜索深度和宽度设置</description></item>
    ///     <item><description>斩杀奖励计算设置</description></item>
    ///     <item><description>日志和调试选项</description></item>
    /// </list>
    /// </remarks>
    public class DefaultRoutineViewModel : ViewModelBase
    {
        /// <summary>
        /// 设置实例引用
        /// <para>所有属性值都从该实例读取和写入</para>
        /// </summary>
        private readonly DefaultRoutineSettings _settings;

        /// <summary>
        /// 初始化 DefaultRoutineViewModel 的新实例
        /// </summary>
        /// <param name="settings">DefaultRoutine 设置实例</param>
        public DefaultRoutineViewModel(DefaultRoutineSettings settings)
        {
            _settings = settings;
            OpenLastMatchCommand = new RelayCommand(ExecuteOpenLastMatch);
            ClearLogCommand = new RelayCommand(ExecuteClearLog);
            ResetCommand = new RelayCommand(ExecuteReset);
        }

        /// <summary>
        /// 获取所有行为模式列表
        /// <para>包括：控制、节奏、打脸等 AI 行为模式</para>
        /// </summary>
        public ObservableCollection<string> AllBehaviors => _settings.AllBehav;

        /// <summary>
        /// 获取或设置默认行为模式
        /// <para>决定 AI 的整体游戏风格</para>
        /// </summary>
        public string DefaultBehavior
        {
            get => _settings.DefaultBehavior;
            set { _settings.DefaultBehavior = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取所有表情选项列表
        /// <para>包括：问候、威胁、称赞等游戏内表情</para>
        /// </summary>
        public ObservableCollection<string> AllEmotes => _settings.AllEmote;

        /// <summary>
        /// 获取或设置默认表情
        /// <para>AI 在游戏中自动发送的表情</para>
        /// </summary>
        public string DefaultEmote
        {
            get => _settings.DefaultEmote;
            set { _settings.DefaultEmote = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置搜索最大宽度
        /// <para>控制 AI 每层搜索考虑的动作数量</para>
        /// <para>值越大，AI 考虑的选项越多，但计算时间越长</para>
        /// </summary>
        public int MaxWide
        {
            get => _settings.MaxWide;
            set { _settings.MaxWide = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置搜索最大深度
        /// <para>控制 AI 向前模拟的回合数</para>
        /// <para>值越大，AI 看得越远，但计算时间越长</para>
        /// </summary>
        public int MaxDeep
        {
            get => _settings.MaxDeep;
            set { _settings.MaxDeep = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置最大计算次数
        /// <para>限制 AI 每回合的最大计算量</para>
        /// </summary>
        public int MaxCal
        {
            get => _settings.MaxCal;
            set { _settings.MaxCal = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置斩杀奖励值
        /// <para>影响 AI 对打脸伤害的偏好程度</para>
        /// <para>值越高，AI 越倾向于打脸而非解场</para>
        /// </summary>
        public int EnfaceReward
        {
            get => _settings.EnfaceReward;
            set { _settings.EnfaceReward = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用详细日志
        /// <para>启用后会输出更多的调试信息</para>
        /// </summary>
        public bool SetLog
        {
            get => _settings.SetLog;
            set { _settings.SetLog = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否使用奥秘规避
        /// <para>启用后 AI 会尝试规避对手的奥秘</para>
        /// </summary>
        public bool UseSecretsPlayAround
        {
            get => _settings.UseSecretsPlayAround;
            set { _settings.UseSecretsPlayAround = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否打印惩罚值
        /// <para>用于调试，显示每个动作的惩罚值计算</para>
        /// </summary>
        public bool UsePrintPenalties
        {
            get => _settings.UsePrintPenalties;
            set { _settings.UsePrintPenalties = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否打印下一步动作
        /// <para>用于调试，显示 AI 计划执行的动作</para>
        /// </summary>
        public bool UsePrintNextMove
        {
            get => _settings.UsePrintNextMove;
            set { _settings.UsePrintNextMove = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否在下一回合能斩杀时激进攻击
        /// <para>启用后 AI 会在可能斩杀时更激进地打脸</para>
        /// </summary>
        public bool BerserkIfCanFinishNextTour
        {
            get => _settings.BerserkIfCanFinishNextTour;
            set { _settings.BerserkIfCanFinishNextTour = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置斩杀线血量阈值
        /// <para>对手血量低于此值时，AI 会优先考虑打脸</para>
        /// </summary>
        public int Enfacehp
        {
            get => _settings.Enfacehp;
            set { _settings.Enfacehp = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取打开上一场比赛记录命令
        /// </summary>
        public ICommand OpenLastMatchCommand { get; }

        /// <summary>
        /// 获取清除日志命令
        /// </summary>
        public ICommand ClearLogCommand { get; }

        /// <summary>
        /// 获取重置设置命令
        /// </summary>
        public ICommand ResetCommand { get; }

        /// <summary>
        /// 请求打开上一场比赛记录事件
        /// </summary>
        public event System.Action RequestOpenLastMatch;

        /// <summary>
        /// 请求清除日志事件
        /// </summary>
        public event System.Action RequestClearLog;

        /// <summary>
        /// 请求重置设置事件
        /// </summary>
        public event System.Action RequestReset;

        /// <summary>
        /// 执行打开上一场比赛记录命令
        /// </summary>
        private void ExecuteOpenLastMatch()
        {
            RequestOpenLastMatch?.Invoke();
        }

        /// <summary>
        /// 执行清除日志命令
        /// </summary>
        private void ExecuteClearLog()
        {
            RequestClearLog?.Invoke();
        }

        /// <summary>
        /// 执行重置设置命令
        /// </summary>
        private void ExecuteReset()
        {
            RequestReset?.Invoke();
        }
    }
}
