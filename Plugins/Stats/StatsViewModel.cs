using System.Windows.Input;
using Triton.Common.Mvvm;

namespace Stats
{
    /// <summary>
    /// 统计插件 ViewModel
    /// <para>负责管理 Stats 插件设置界面的数据绑定和交互逻辑</para>
    /// <para>用于显示和统计游戏对局数据</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>显示各职业的胜场/败场统计</description></item>
    ///     <item><description>计算并显示胜率</description></item>
    ///     <item><description>显示对战环境分布</description></item>
    ///     <item><description>支持重置统计数据</description></item>
    /// </list>
    /// </remarks>
    public class StatsViewModel : ViewModelBase
    {
        /// <summary>
        /// 设置实例引用
        /// <para>所有属性值都从该实例读取和写入</para>
        /// </summary>
        private readonly StatsSettings _settings;

        /// <summary>
        /// 初始化 StatsViewModel 的新实例
        /// </summary>
        /// <param name="settings">Stats 设置实例</param>
        public StatsViewModel(StatsSettings settings)
        {
            _settings = settings;
            ResetCommand = new RelayCommand(ExecuteReset);
        }

        /// <summary>
        /// 获取或设置总胜场数
        /// </summary>
        public int Wins { get => _settings.Wins; set { _settings.Wins = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业1胜场数
        /// </summary>
        public int Wins1 { get => _settings.Wins1; set { _settings.Wins1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业2胜场数
        /// </summary>
        public int Wins2 { get => _settings.Wins2; set { _settings.Wins2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业3胜场数
        /// </summary>
        public int Wins3 { get => _settings.Wins3; set { _settings.Wins3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业4胜场数
        /// </summary>
        public int Wins4 { get => _settings.Wins4; set { _settings.Wins4 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业5胜场数
        /// </summary>
        public int Wins5 { get => _settings.Wins5; set { _settings.Wins5 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业6胜场数
        /// </summary>
        public int Wins6 { get => _settings.Wins6; set { _settings.Wins6 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业7胜场数
        /// </summary>
        public int Wins7 { get => _settings.Wins7; set { _settings.Wins7 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业8胜场数
        /// </summary>
        public int Wins8 { get => _settings.Wins8; set { _settings.Wins8 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业9胜场数
        /// </summary>
        public int Wins9 { get => _settings.Wins9; set { _settings.Wins9 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业10胜场数
        /// </summary>
        public int Wins10 { get => _settings.Wins10; set { _settings.Wins10 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业11胜场数
        /// </summary>
        public int Wins11 { get => _settings.Wins11; set { _settings.Wins11 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置总败场数
        /// </summary>
        public int Losses { get => _settings.Losses; set { _settings.Losses = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业1败场数
        /// </summary>
        public int Losses1 { get => _settings.Losses1; set { _settings.Losses1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业2败场数
        /// </summary>
        public int Losses2 { get => _settings.Losses2; set { _settings.Losses2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业3败场数
        /// </summary>
        public int Losses3 { get => _settings.Losses3; set { _settings.Losses3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业4败场数
        /// </summary>
        public int Losses4 { get => _settings.Losses4; set { _settings.Losses4 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业5败场数
        /// </summary>
        public int Losses5 { get => _settings.Losses5; set { _settings.Losses5 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业6败场数
        /// </summary>
        public int Losses6 { get => _settings.Losses6; set { _settings.Losses6 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业7败场数
        /// </summary>
        public int Losses7 { get => _settings.Losses7; set { _settings.Losses7 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业8败场数
        /// </summary>
        public int Losses8 { get => _settings.Losses8; set { _settings.Losses8 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业9败场数
        /// </summary>
        public int Losses9 { get => _settings.Losses9; set { _settings.Losses9 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业10败场数
        /// </summary>
        public int Losses10 { get => _settings.Losses10; set { _settings.Losses10 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业11败场数
        /// </summary>
        public int Losses11 { get => _settings.Losses11; set { _settings.Losses11 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置总胜率
        /// </summary>
        public string Winrate { get => _settings.Winrate; set { _settings.Winrate = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业1胜率
        /// </summary>
        public string Winrate1 { get => _settings.Winrate1; set { _settings.Winrate1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业2胜率
        /// </summary>
        public string Winrate2 { get => _settings.Winrate2; set { _settings.Winrate2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业3胜率
        /// </summary>
        public string Winrate3 { get => _settings.Winrate3; set { _settings.Winrate3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业4胜率
        /// </summary>
        public string Winrate4 { get => _settings.Winrate4; set { _settings.Winrate4 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业5胜率
        /// </summary>
        public string Winrate5 { get => _settings.Winrate5; set { _settings.Winrate5 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业6胜率
        /// </summary>
        public string Winrate6 { get => _settings.Winrate6; set { _settings.Winrate6 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业7胜率
        /// </summary>
        public string Winrate7 { get => _settings.Winrate7; set { _settings.Winrate7 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业8胜率
        /// </summary>
        public string Winrate8 { get => _settings.Winrate8; set { _settings.Winrate8 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业9胜率
        /// </summary>
        public string Winrate9 { get => _settings.Winrate9; set { _settings.Winrate9 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业10胜率
        /// </summary>
        public string Winrate10 { get => _settings.Winrate10; set { _settings.Winrate10 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战职业11胜率
        /// </summary>
        public string Winrate11 { get => _settings.Winrate11; set { _settings.Winrate11 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置对战环境分布
        /// </summary>
        public string Environment { get => _settings.environment; set { _settings.environment = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业1环境占比
        /// </summary>
        public string Environment1 { get => _settings.environment1; set { _settings.environment1 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业2环境占比
        /// </summary>
        public string Environment2 { get => _settings.environment2; set { _settings.environment2 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业3环境占比
        /// </summary>
        public string Environment3 { get => _settings.environment3; set { _settings.environment3 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业4环境占比
        /// </summary>
        public string Environment4 { get => _settings.environment4; set { _settings.environment4 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业5环境占比
        /// </summary>
        public string Environment5 { get => _settings.environment5; set { _settings.environment5 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业6环境占比
        /// </summary>
        public string Environment6 { get => _settings.environment6; set { _settings.environment6 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业7环境占比
        /// </summary>
        public string Environment7 { get => _settings.environment7; set { _settings.environment7 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业8环境占比
        /// </summary>
        public string Environment8 { get => _settings.environment8; set { _settings.environment8 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业9环境占比
        /// </summary>
        public string Environment9 { get => _settings.environment9; set { _settings.environment9 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业10环境占比
        /// </summary>
        public string Environment10 { get => _settings.environment10; set { _settings.environment10 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取或设置职业11环境占比
        /// </summary>
        public string Environment11 { get => _settings.environment11; set { _settings.environment11 = value; OnPropertyChanged(); } }

        /// <summary>
        /// 获取重置统计命令
        /// </summary>
        public ICommand ResetCommand { get; }

        /// <summary>
        /// 请求重置统计事件
        /// </summary>
        public event System.Action RequestReset;

        /// <summary>
        /// 执行重置统计命令
        /// </summary>
        private void ExecuteReset()
        {
            RequestReset?.Invoke();
        }
    }
}
