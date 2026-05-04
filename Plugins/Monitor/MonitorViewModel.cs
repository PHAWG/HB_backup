using System.Windows.Input;
using Triton.Common.Mvvm;

namespace Monitor
{
    /// <summary>
    /// 监控插件 ViewModel
    /// <para>负责管理 Monitor 插件设置界面的数据绑定和交互逻辑</para>
    /// <para>用于显示和监控游戏运行统计数据</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>显示当前等级和经验值</description></item>
    ///     <item><description>显示总运行时间和经验获取速率</description></item>
    ///     <item><description>显示收藏品信息</description></item>
    ///     <item><description>显示旅店通票到期时间</description></item>
    ///     <item><description>显示各模式排名信息</description></item>
    /// </list>
    /// </remarks>
    public class MonitorViewModel : ViewModelBase
    {
        /// <summary>
        /// 设置实例引用
        /// <para>所有属性值都从该实例读取和写入</para>
        /// </summary>
        private readonly MonitorSettings _settings;

        /// <summary>
        /// 初始化 MonitorViewModel 的新实例
        /// </summary>
        /// <param name="settings">Monitor 设置实例</param>
        public MonitorViewModel(MonitorSettings settings)
        {
            _settings = settings;
            ResetCommand = new RelayCommand(ExecuteReset);

            _settings.PropertyChanged += Settings_PropertyChanged;
        }

        /// <summary>
        /// 转发 Settings 的 PropertyChanged 事件到 UI
        /// </summary>
        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// 获取或设置当前等级
        /// </summary>
        public int Level { get => _settings.Level; set { _settings.Level = value; } }

        /// <summary>
        /// 获取或设置当前经验值
        /// </summary>
        public int Xp { get => _settings.Xp; set { _settings.Xp = value; } }

        /// <summary>
        /// 获取或设置升级所需经验值
        /// </summary>
        public int XpNeeded { get => _settings.XpNeeded; set { _settings.XpNeeded = value; } }

        /// <summary>
        /// 获取或设置累计获得的总经验值
        /// </summary>
        public int AllXp { get => _settings.AllXp; set { _settings.AllXp = value; } }

        /// <summary>
        /// 获取或设置满级所需的总经验值
        /// </summary>
        public int AllXpNeeded { get => _settings.AllXpNeeded; set { _settings.AllXpNeeded = value; } }

        /// <summary>
        /// 获取或设置总运行时间文本
        /// <para>格式化的时间字符串，如 "2小时30分钟"</para>
        /// </summary>
        public string AllRunTimeText { get => _settings.AllRunTimeText; set { _settings.AllRunTimeText = value; } }

        /// <summary>
        /// 获取或设置累计获得的经验值（长整型）
        /// </summary>
        public long AllGetXp { get => _settings.AllGetXp; set { _settings.AllGetXp = value; } }

        /// <summary>
        /// 获取或设置每小时经验值文本
        /// <para>显示经验获取速率</para>
        /// </summary>
        public string PerHourXpStr { get => _settings.PerHourXpStr; set { _settings.PerHourXpStr = value; } }

        /// <summary>
        /// 获取或设置满级所需经验值
        /// </summary>
        public int FullXpNeeded { get => _settings.FullXpNeeded; set { _settings.FullXpNeeded = value; } }

        /// <summary>
        /// 获取或设置满级预估时间
        /// </summary>
        public string FullTimeNeeded { get => _settings.FullTimeNeeded; set { _settings.FullTimeNeeded = value; } }

        /// <summary>
        /// 获取或设置收藏品信息
        /// <para>显示卡牌收藏进度</para>
        /// </summary>
        public string Collection { get => _settings.Collection; set { _settings.Collection = value; } }

        /// <summary>
        /// 获取或设置旅店通票到期时间
        /// </summary>
        public string PassportEnd { get => _settings.PassportEnd; set { _settings.PassportEnd = value; } }

        /// <summary>
        /// 获取或设置扭曲模式信息
        /// </summary>
        public string TwistInfo { get => _settings.TwistInfo; set { _settings.TwistInfo = value; } }

        /// <summary>
        /// 获取或设置标准模式信息
        /// </summary>
        public string StandardInfo { get => _settings.StandardInfo; set { _settings.StandardInfo = value; } }

        /// <summary>
        /// 获取或设置狂野模式信息
        /// </summary>
        public string WildInfo { get => _settings.WildInfo; set { _settings.WildInfo = value; } }

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
