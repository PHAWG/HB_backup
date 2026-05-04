using System.Windows.Input;
using Triton.Common.Mvvm;

namespace Quest
{
    /// <summary>
    /// 任务插件 ViewModel
    /// <para>负责管理 Quest 插件设置界面的数据绑定和交互逻辑</para>
    /// <para>用于显示和管理游戏内的每日/每周任务</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>显示每日任务进度和奖励</description></item>
    ///     <item><description>显示每周任务进度和奖励</description></item>
    ///     <item><description>支持刷新任务功能</description></item>
    ///     <item><description>自动任务管理设置</description></item>
    /// </list>
    /// </remarks>
    public class QuestViewModel : ViewModelBase
    {
        /// <summary>
        /// 设置实例引用
        /// <para>所有属性值都从该实例读取和写入</para>
        /// </summary>
        private readonly QuestSettings _settings;

        /// <summary>
        /// 每日任务1是否可刷新
        /// </summary>
        private bool _canRefreshDay1;

        /// <summary>
        /// 每日任务2是否可刷新
        /// </summary>
        private bool _canRefreshDay2;

        /// <summary>
        /// 每日任务3是否可刷新
        /// </summary>
        private bool _canRefreshDay3;

        /// <summary>
        /// 每周任务1是否可刷新
        /// </summary>
        private bool _canRefreshWeek1;

        /// <summary>
        /// 每周任务2是否可刷新
        /// </summary>
        private bool _canRefreshWeek2;

        /// <summary>
        /// 每周任务3是否可刷新
        /// </summary>
        private bool _canRefreshWeek3;

        /// <summary>
        /// 初始化 QuestViewModel 的新实例
        /// </summary>
        /// <param name="settings">Quest 设置实例</param>
        public QuestViewModel(QuestSettings settings)
        {
            _settings = settings;
            RefreshDay1Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(1, "day"), () => CanRefreshDay1);
            RefreshDay2Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(2, "day"), () => CanRefreshDay2);
            RefreshDay3Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(3, "day"), () => CanRefreshDay3);
            RefreshWeek1Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(1, "week"), () => CanRefreshWeek1);
            RefreshWeek2Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(2, "week"), () => CanRefreshWeek2);
            RefreshWeek3Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(3, "week"), () => CanRefreshWeek3);
            ResetCommand = new RelayCommand(() => RequestReset?.Invoke());
        }

        /// <summary>
        /// 获取或设置每日任务1是否可刷新
        /// </summary>
        public bool CanRefreshDay1
        {
            get => _canRefreshDay1;
            set => SetProperty(ref _canRefreshDay1, value);
        }

        /// <summary>
        /// 获取或设置每日任务2是否可刷新
        /// </summary>
        public bool CanRefreshDay2
        {
            get => _canRefreshDay2;
            set => SetProperty(ref _canRefreshDay2, value);
        }

        /// <summary>
        /// 获取或设置每日任务3是否可刷新
        /// </summary>
        public bool CanRefreshDay3
        {
            get => _canRefreshDay3;
            set => SetProperty(ref _canRefreshDay3, value);
        }

        /// <summary>
        /// 获取或设置每周任务1是否可刷新
        /// </summary>
        public bool CanRefreshWeek1
        {
            get => _canRefreshWeek1;
            set => SetProperty(ref _canRefreshWeek1, value);
        }

        /// <summary>
        /// 获取或设置每周任务2是否可刷新
        /// </summary>
        public bool CanRefreshWeek2
        {
            get => _canRefreshWeek2;
            set => SetProperty(ref _canRefreshWeek2, value);
        }

        /// <summary>
        /// 获取或设置每周任务3是否可刷新
        /// </summary>
        public bool CanRefreshWeek3
        {
            get => _canRefreshWeek3;
            set => SetProperty(ref _canRefreshWeek3, value);
        }

        /// <summary>
        /// 获取或设置每日任务1进度
        /// </summary>
        public int ProgressDay1 { get => _settings.ProgressDay1; set { _settings.ProgressDay1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务2进度
        /// </summary>
        public int ProgressDay2 { get => _settings.ProgressDay2; set { _settings.ProgressDay2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务3进度
        /// </summary>
        public int ProgressDay3 { get => _settings.ProgressDay3; set { _settings.ProgressDay3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务1进度
        /// </summary>
        public int ProgressWeek1 { get => _settings.ProgressWeek1; set { _settings.ProgressWeek1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务2进度
        /// </summary>
        public int ProgressWeek2 { get => _settings.ProgressWeek2; set { _settings.ProgressWeek2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务3进度
        /// </summary>
        public int ProgressWeek3 { get => _settings.ProgressWeek3; set { _settings.ProgressWeek3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务1配额
        /// </summary>
        public int QuotaDay1 { get => _settings.QuotaDay1; set { _settings.QuotaDay1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务2配额
        /// </summary>
        public int QuotaDay2 { get => _settings.QuotaDay2; set { _settings.QuotaDay2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务3配额
        /// </summary>
        public int QuotaDay3 { get => _settings.QuotaDay3; set { _settings.QuotaDay3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务1配额
        /// </summary>
        public int QuotaWeek1 { get => _settings.QuotaWeek1; set { _settings.QuotaWeek1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务2配额
        /// </summary>
        public int QuotaWeek2 { get => _settings.QuotaWeek2; set { _settings.QuotaWeek2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务3配额
        /// </summary>
        public int QuotaWeek3 { get => _settings.QuotaWeek3; set { _settings.QuotaWeek3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务1经验奖励
        /// </summary>
        public int XpDay1 { get => _settings.XpDay1; set { _settings.XpDay1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务2经验奖励
        /// </summary>
        public int XpDay2 { get => _settings.XpDay2; set { _settings.XpDay2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务3经验奖励
        /// </summary>
        public int XpDay3 { get => _settings.XpDay3; set { _settings.XpDay3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务1经验奖励
        /// </summary>
        public int XpWeek1 { get => _settings.XpWeek1; set { _settings.XpWeek1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务2经验奖励
        /// </summary>
        public int XpWeek2 { get => _settings.XpWeek2; set { _settings.XpWeek2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务3经验奖励
        /// </summary>
        public int XpWeek3 { get => _settings.XpWeek3; set { _settings.XpWeek3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务1描述
        /// </summary>
        public string DesDay1 { get => _settings.DesDay1; set { _settings.DesDay1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务2描述
        /// </summary>
        public string DesDay2 { get => _settings.DesDay2; set { _settings.DesDay2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每日任务3描述
        /// </summary>
        public string DesDay3 { get => _settings.DesDay3; set { _settings.DesDay3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务1描述
        /// </summary>
        public string DesWeek1 { get => _settings.DesWeek1; set { _settings.DesWeek1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务2描述
        /// </summary>
        public string DesWeek2 { get => _settings.DesWeek2; set { _settings.DesWeek2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置每周任务3描述
        /// </summary>
        public string DesWeek3 { get => _settings.DesWeek3; set { _settings.DesWeek3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置是否需要自动管理任务
        /// </summary>
        public bool NeedAuto { get => _settings.NeedAuto; set { _settings.NeedAuto = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置运行时间
        /// </summary>
        public string RunTime { get => _settings.RunTime; set { _settings.RunTime = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置检查间隔（分钟）
        /// </summary>
        public int Interval { get => _settings.Interval; set { _settings.Interval = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取刷新每日任务1命令
        /// </summary>
        public ICommand RefreshDay1Command { get; }

        /// <summary>
        /// 获取刷新每日任务2命令
        /// </summary>
        public ICommand RefreshDay2Command { get; }

        /// <summary>
        /// 获取刷新每日任务3命令
        /// </summary>
        public ICommand RefreshDay3Command { get; }

        /// <summary>
        /// 获取刷新每周任务1命令
        /// </summary>
        public ICommand RefreshWeek1Command { get; }

        /// <summary>
        /// 获取刷新每周任务2命令
        /// </summary>
        public ICommand RefreshWeek2Command { get; }

        /// <summary>
        /// 获取刷新每周任务3命令
        /// </summary>
        public ICommand RefreshWeek3Command { get; }

        /// <summary>
        /// 获取重置命令
        /// </summary>
        public ICommand ResetCommand { get; }

        /// <summary>
        /// 请求刷新任务事件
        /// <para>参数1: 任务序号 (1-3)</para>
        /// <para>参数2: 任务类型 ("day" 或 "week")</para>
        /// </summary>
        public event System.Action<int, string> RequestRefreshQuest;

        /// <summary>
        /// 请求重置事件
        /// </summary>
        public event System.Action RequestReset;
    }
}
