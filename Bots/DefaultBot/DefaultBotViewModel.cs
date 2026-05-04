using System.Collections.ObjectModel;
using System.Windows.Input;
using Triton.Common.Mvvm;
using Triton.Game.Mapping;

namespace Triton.Bot.Logic.Bots.DefaultBot
{
    /// <summary>
    /// 默认机器人 ViewModel
    /// <para>负责管理 DefaultBot 设置界面的数据绑定和交互逻辑</para>
    /// <para>作为 Settings 和 View 之间的桥梁，实现 MVVM 模式</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>构建模式规则选择</description></item>
    ///     <item><description>自定义卡组管理</description></item>
    ///     <item><description>自动问候设置</description></item>
    ///     <item><description>自动投降设置</description></item>
    ///     <item><description>窗口位置限制设置</description></item>
    /// </list>
    /// </remarks>
    public class DefaultBotViewModel : ViewModelBase
    {
        /// <summary>
        /// 设置实例引用
        /// <para>所有属性值都从该实例读取和写入</para>
        /// </summary>
        private readonly DefaultBotSettings _settings;

        /// <summary>
        /// 初始化 DefaultBotViewModel 的新实例
        /// </summary>
        /// <param name="settings">DefaultBot 设置实例</param>
        public DefaultBotViewModel(DefaultBotSettings settings)
        {
            _settings = settings;
            RecacheCommand = new RelayCommand(ExecuteRecache);
        }

        /// <summary>
        /// 获取所有构建模式规则列表
        /// <para>包括：标准、狂野、经典等游戏模式</para>
        /// </summary>
        public ObservableCollection<VisualsFormatType> AllConstructedRules => _settings.AllConstructedRules;

        /// <summary>
        /// 获取或设置当前选中的构建模式规则
        /// </summary>
        public VisualsFormatType ConstructedGameRule
        {
            get => _settings.ConstructedGameRule;
            set { _settings.ConstructedGameRule = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置自定义卡组代码
        /// <para>用于导入卡组代码进行游戏</para>
        /// </summary>
        public string ConstructedCustomDeck
        {
            get => _settings.ConstructedCustomDeck;
            set { _settings.ConstructedCustomDeck = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用自动问候
        /// <para>游戏开始时自动发送问候语</para>
        /// </summary>
        public bool AutoGreet
        {
            get => _settings.AutoGreet;
            set { _settings.AutoGreet = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否需要缓存自定义卡组
        /// </summary>
        public bool NeedsToCacheCustomDecks
        {
            get => _settings.NeedsToCacheCustomDecks;
            set { _settings.NeedsToCacheCustomDecks = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用窗口位置限制
        /// <para>启用后游戏窗口将被限制在指定区域内</para>
        /// </summary>
        public bool ReleaseLimit
        {
            get => _settings.ReleaseLimit;
            set { _settings.ReleaseLimit = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置窗口限制区域的宽度
        /// </summary>
        public int ReleaseLimitW
        {
            get => _settings.ReleaseLimitW;
            set { _settings.ReleaseLimitW = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置窗口限制区域的高度
        /// </summary>
        public int ReleaseLimitH
        {
            get => _settings.ReleaseLimitH;
            set { _settings.ReleaseLimitH = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否在构建模式胜利后自动投降
        /// <para>用于控制胜率，避免被检测</para>
        /// </summary>
        public bool AutoConcedeAfterConstructedWin
        {
            get => _settings.AutoConcedeAfterConstructedWin;
            set { _settings.AutoConcedeAfterConstructedWin = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置自动投降前的胜场数阈值
        /// <para>达到该胜场数后触发自动投降</para>
        /// </summary>
        public int AutoConcedeNumberOfWins
        {
            get => _settings.AutoConcedeNumberOfWins;
            set { _settings.AutoConcedeNumberOfWins = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置自动投降前的败场数阈值
        /// <para>达到该败场数后停止自动投降</para>
        /// </summary>
        public int AutoConcedeNumberOfLosses
        {
            get => _settings.AutoConcedeNumberOfLosses;
            set { _settings.AutoConcedeNumberOfLosses = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否启用普通投降模式
        /// </summary>
        public bool NormalConcede
        {
            get => _settings.NormalConcede;
            set { _settings.NormalConcede = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否在换牌阶段强制投降
        /// </summary>
        public bool ForceConcedeAtMulligan
        {
            get => _settings.ForceConcedeAtMulligan;
            set { _settings.ForceConcedeAtMulligan = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置是否根据对手名称判断是否投降
        /// </summary>
        public bool JudgmentOpponentNameConcede
        {
            get => _settings.JudgmentOpponentNameConcede;
            set { _settings.JudgmentOpponentNameConcede = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置自动投降的最小延迟（毫秒）
        /// </summary>
        public int AutoConcedeMinDelayMs
        {
            get => _settings.AutoConcedeMinDelayMs;
            set { _settings.AutoConcedeMinDelayMs = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取或设置自动投降的最大延迟（毫秒）
        /// </summary>
        public int AutoConcedeMaxDelayMs
        {
            get => _settings.AutoConcedeMaxDelayMs;
            set { _settings.AutoConcedeMaxDelayMs = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取重新缓存命令
        /// <para>点击"重新缓存"按钮时执行</para>
        /// </summary>
        public ICommand RecacheCommand { get; }

        /// <summary>
        /// 请求重新缓存事件
        /// </summary>
        public event System.Action RequestRecache;

        /// <summary>
        /// 执行重新缓存命令
        /// </summary>
        private void ExecuteRecache()
        {
            RequestRecache?.Invoke();
        }
    }
}
