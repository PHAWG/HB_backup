using System.Windows.Input;
using Triton.Common.Mvvm;

namespace AutoStop
{
    /// <summary>
    /// 自动停止插件 ViewModel
    /// <para>负责管理 AutoStop 插件设置界面的数据绑定和交互逻辑</para>
    /// <para>用于在满足特定条件时自动停止 Bot 运行</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>达到指定游戏场数后停止</description></item>
    ///     <item><description>达到指定胜场数后停止</description></item>
    ///     <item><description>达到指定败场数后停止</description></item>
    ///     <item><description>运行指定时间后投降</description></item>
    ///     <item><description>动态打脸惩罚设置</description></item>
    /// </list>
    /// </remarks>
    public class AutoStopViewModel : ViewModelBase
    {
        /// <summary>
        /// 设置实例引用
        /// <para>所有属性值都从该实例读取和写入</para>
        /// </summary>
        private readonly AutoStopSettings _settings;

        /// <summary>
        /// 初始化 AutoStopViewModel 的新实例
        /// </summary>
        /// <param name="settings">AutoStop 设置实例</param>
        public AutoStopViewModel(AutoStopSettings settings)
        {
            _settings = settings;
            ResetCommand = new RelayCommand(ExecuteReset);
        }

        /// <summary>
        /// 获取或设置是否启用游戏场数停止
        /// <para>启用后达到指定场数会自动停止 Bot</para>
        /// </summary>
        public bool StopAfterXGames
        {
            get => _settings.StopAfterXGames;
            set { _settings.StopAfterXGames = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用胜场数停止
        /// <para>启用后达到指定胜场会自动停止 Bot</para>
        /// </summary>
        public bool StopAfterXWins
        {
            get => _settings.StopAfterXWins;
            set { _settings.StopAfterXWins = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用败场数停止
        /// <para>启用后达到指定败场会自动停止 Bot</para>
        /// </summary>
        public bool StopAfterXLosses
        {
            get => _settings.StopAfterXLosses;
            set { _settings.StopAfterXLosses = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置停止的游戏场数阈值
        /// </summary>
        public int StopGameCount
        {
            get => _settings.StopGameCount;
            set { _settings.StopGameCount = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置停止的胜场数阈值
        /// </summary>
        public int StopWinCount
        {
            get => _settings.StopWinCount;
            set { _settings.StopWinCount = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置停止的败场数阈值
        /// </summary>
        public int StopLossCount
        {
            get => _settings.StopLossCount;
            set { _settings.StopLossCount = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置当前胜场数
        /// <para>用于显示当前统计数据</para>
        /// </summary>
        public int Wins
        {
            get => _settings.Wins;
            set { _settings.Wins = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置当前败场数
        /// <para>用于显示当前统计数据</para>
        /// </summary>
        public int Losses
        {
            get => _settings.Losses;
            set { _settings.Losses = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用超时投降
        /// <para>启用后运行指定时间会自动投降</para>
        /// </summary>
        public bool ConcedeAfterXMinutes
        {
            get => _settings.ConcedeAfterXMinutes;
            set { _settings.ConcedeAfterXMinutes = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置投降的分钟数阈值
        /// </summary>
        public int ConcedeMinutesCount
        {
            get => _settings.ConcedeMinutesCount;
            set { _settings.ConcedeMinutesCount = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用动态打脸惩罚
        /// <para>启用后根据时间段调整打脸惩罚值</para>
        /// </summary>
        public bool DynamicFacePenaltyEnabled
        {
            get => _settings.DynamicFacePenaltyEnabled;
            set { _settings.DynamicFacePenaltyEnabled = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置动态惩罚的时间阈值（分钟）
        /// </summary>
        public int DynamicFacePenaltyMinutes
        {
            get => _settings.DynamicFacePenaltyMinutes;
            set { _settings.DynamicFacePenaltyMinutes = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置时间前的打脸惩罚值
        /// </summary>
        public int FacePenaltyBeforeTime
        {
            get => _settings.FacePenaltyBeforeTime;
            set { _settings.FacePenaltyBeforeTime = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置时间后的打脸惩罚值
        /// </summary>
        public int FacePenaltyAfterTime
        {
            get => _settings.FacePenaltyAfterTime;
            set { _settings.FacePenaltyAfterTime = value; OnPropertyChanged(); }
        }

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
