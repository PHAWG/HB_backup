using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using log4net;
using Newtonsoft.Json;
using Triton.Bot.Settings;
using Triton.Common;
using Triton.Game;
using Triton.Game.Mapping;
using Logger = Triton.Common.LogUtilities.Logger;

namespace AutoStop
{
    /// <summary>
    /// 自动停止插件设置类
    /// <para>存储自动停止插件的所有配置和运行时数据</para>
    /// <para>继承自 JsonSettings，支持 JSON 序列化/反序列化和属性变更通知</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>达到指定场数后自动停止</description></item>
    ///     <item><description>达到指定胜场后自动停止</description></item>
    ///     <item><description>达到指定败场后自动停止</description></item>
    ///     <item><description>超时自动投降</description></item>
    ///     <item><description>动态打脸惩罚调整</description></item>
    /// </list>
    /// </remarks>
    public class AutoStopSettings : JsonSettings
    {
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();

        private static AutoStopSettings _instance;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static AutoStopSettings Instance
        {
            get { return _instance ?? (_instance = new AutoStopSettings()); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoStopSettings()
            : base(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "AutoStop")))
        {
        }

        /// <summary>
        /// 重新加载设置文件
        /// </summary>
        public void ReloadFile()
        {
            Reload(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "AutoStop" + GetMyHashCode())));
        }

        #region 私有字段 - 停止条件

        /// <summary>是否启用场数停止</summary>
        private bool _stopAfterXGames;

        /// <summary>是否启用胜场停止</summary>
        private bool _stopAfterXWins;

        /// <summary>是否启用败场停止</summary>
        private bool _stopAfterXLosses;

        /// <summary>停止场数阈值</summary>
        private int _stopGameCount;

        /// <summary>停止胜场阈值</summary>
        private int _stopWinCount;

        /// <summary>停止败场阈值</summary>
        private int _stopLossCount;

        #endregion

        #region 私有字段 - 超时投降

        /// <summary>是否启用超时投降</summary>
        private bool _concedeAfterXMinutes;

        /// <summary>投降时间阈值（分钟）</summary>
        private int _concedeMinutesCount;

        #endregion

        #region 私有字段 - 动态打脸惩罚

        /// <summary>是否启用动态打脸惩罚</summary>
        private bool _dynamicFacePenaltyEnabled;

        /// <summary>切换时间（分钟）</summary>
        private int _dynamicFacePenaltyMinutes;

        /// <summary>时间前打脸惩罚值</summary>
        private int _facePenaltyBeforeTime;

        /// <summary>时间后打脸惩罚值</summary>
        private int _facePenaltyAfterTime;

        #endregion

        #region 私有字段 - 当前统计

        /// <summary>当前胜场数</summary>
        private int _wins;

        /// <summary>当前败场数</summary>
        private int _losses;

        #endregion

        #region 公共方法

        /// <summary>
        /// 重置统计数据
        /// </summary>
        public void Reset()
        {
            Wins = 0;
            Losses = 0;
        }

        #endregion

        #region 公共属性 - 停止条件

        /// <summary>
        /// 获取或设置是否启用场数停止
        /// <para>启用后达到 StopGameCount 场数会自动停止 Bot</para>
        /// </summary>
        [DefaultValue(false)]
        public bool StopAfterXGames
        {
            get { return _stopAfterXGames; }
            set
            {
                if (!value.Equals(_stopAfterXGames))
                {
                    _stopAfterXGames = value;
                    NotifyPropertyChanged(() => StopAfterXGames);
                }
            }
        }

        /// <summary>
        /// 获取或设置是否启用胜场停止
        /// <para>启用后达到 StopWinCount 胜场会自动停止 Bot</para>
        /// </summary>
        [DefaultValue(false)]
        public bool StopAfterXWins
        {
            get { return _stopAfterXWins; }
            set
            {
                if (!value.Equals(_stopAfterXWins))
                {
                    _stopAfterXWins = value;
                    NotifyPropertyChanged(() => StopAfterXWins);
                }
            }
        }

        /// <summary>
        /// 获取或设置是否启用败场停止
        /// <para>启用后达到 StopLossCount 败场会自动停止 Bot</para>
        /// </summary>
        [DefaultValue(false)]
        public bool StopAfterXLosses
        {
            get { return _stopAfterXLosses; }
            set
            {
                if (!value.Equals(_stopAfterXLosses))
                {
                    _stopAfterXLosses = value;
                    NotifyPropertyChanged(() => StopAfterXLosses);
                }
            }
        }

        /// <summary>
        /// 获取或设置停止场数阈值
        /// <para>当 StopAfterXGames 启用时，达到此场数停止</para>
        /// </summary>
        [DefaultValue(1)]
        public int StopGameCount
        {
            get { return _stopGameCount; }
            set
            {
                if (!value.Equals(_stopGameCount))
                {
                    _stopGameCount = value;
                    NotifyPropertyChanged(() => StopGameCount);
                }
            }
        }

        /// <summary>
        /// 获取或设置停止胜场阈值
        /// <para>当 StopAfterXWins 启用时，达到此胜场停止</para>
        /// </summary>
        [DefaultValue(1)]
        public int StopWinCount
        {
            get { return _stopWinCount; }
            set
            {
                if (!value.Equals(_stopWinCount))
                {
                    _stopWinCount = value;
                    NotifyPropertyChanged(() => StopWinCount);
                }
            }
        }

        /// <summary>
        /// 获取或设置停止败场阈值
        /// <para>当 StopAfterXLosses 启用时，达到此败场停止</para>
        /// </summary>
        [DefaultValue(1)]
        public int StopLossCount
        {
            get { return _stopLossCount; }
            set
            {
                if (!value.Equals(_stopLossCount))
                {
                    _stopLossCount = value;
                    NotifyPropertyChanged(() => StopLossCount);
                }
            }
        }

        #endregion

        #region 公共属性 - 超时投降

        /// <summary>
        /// 获取或设置是否启用超时投降
        /// <para>启用后游戏进行 ConcedeMinutesCount 分钟会自动投降</para>
        /// </summary>
        [DefaultValue(true)]
        public bool ConcedeAfterXMinutes
        {
            get { return _concedeAfterXMinutes; }
            set
            {
                if (!value.Equals(_concedeAfterXMinutes))
                {
                    _concedeAfterXMinutes = value;
                    NotifyPropertyChanged(() => ConcedeAfterXMinutes);
                }
            }
        }

        /// <summary>
        /// 获取或设置投降时间阈值（分钟）
        /// <para>游戏进行超过此时间后自动投降</para>
        /// </summary>
        [DefaultValue(32)]
        public int ConcedeMinutesCount
        {
            get { return _concedeMinutesCount; }
            set
            {
                if (!value.Equals(_concedeMinutesCount))
                {
                    _concedeMinutesCount = value;
                    NotifyPropertyChanged(() => ConcedeMinutesCount);
                }
            }
        }

        #endregion

        #region 公共属性 - 动态打脸惩罚

        /// <summary>
        /// 获取或设置是否启用动态打脸惩罚
        /// <para>启用后根据运行时间调整 AI 的打脸惩罚值</para>
        /// <para>用于控制 AI 的打脸倾向，避免被检测</para>
        /// </summary>
        [DefaultValue(false)]
        public bool DynamicFacePenaltyEnabled
        {
            get { return _dynamicFacePenaltyEnabled; }
            set
            {
                if (!value.Equals(_dynamicFacePenaltyEnabled))
                {
                    _dynamicFacePenaltyEnabled = value;
                    NotifyPropertyChanged(() => DynamicFacePenaltyEnabled);
                }
                Log.InfoFormat("[自动停止设置] 动态打脸惩罚启用 = {0}", _dynamicFacePenaltyEnabled);
            }
        }

        /// <summary>
        /// 获取或设置切换时间（分钟）
        /// <para>运行时间超过此值后，打脸惩罚从 FacePenaltyBeforeTime 切换到 FacePenaltyAfterTime</para>
        /// </summary>
        [DefaultValue(30)]
        public int DynamicFacePenaltyMinutes
        {
            get { return _dynamicFacePenaltyMinutes; }
            set
            {
                if (!value.Equals(_dynamicFacePenaltyMinutes))
                {
                    _dynamicFacePenaltyMinutes = value;
                    NotifyPropertyChanged(() => DynamicFacePenaltyMinutes);
                }
                Log.InfoFormat("[自动停止设置] 打脸切换时间 = {0} 分钟", _dynamicFacePenaltyMinutes);
            }
        }

        /// <summary>
        /// 获取或设置时间前打脸惩罚值
        /// <para>运行时间未超过 DynamicFacePenaltyMinutes 时使用</para>
        /// <para>负值表示更倾向于打脸，正值表示更倾向于解场</para>
        /// </summary>
        [DefaultValue(-2000)]
        public int FacePenaltyBeforeTime
        {
            get { return _facePenaltyBeforeTime; }
            set
            {
                if (!value.Equals(_facePenaltyBeforeTime))
                {
                    _facePenaltyBeforeTime = value;
                    NotifyPropertyChanged(() => FacePenaltyBeforeTime);
                }
                Log.InfoFormat("[自动停止设置] 时间前打脸惩罚 = {0}", _facePenaltyBeforeTime);
            }
        }

        /// <summary>
        /// 获取或设置时间后打脸惩罚值
        /// <para>运行时间超过 DynamicFacePenaltyMinutes 后使用</para>
        /// <para>正值表示更倾向于解场，降低打脸频率</para>
        /// </summary>
        [DefaultValue(1000)]
        public int FacePenaltyAfterTime
        {
            get { return _facePenaltyAfterTime; }
            set
            {
                if (!value.Equals(_facePenaltyAfterTime))
                {
                    _facePenaltyAfterTime = value;
                    NotifyPropertyChanged(() => FacePenaltyAfterTime);
                }
                Log.InfoFormat("[自动停止设置] 时间后打脸惩罚 = {0}", _facePenaltyAfterTime);
            }
        }

        #endregion

        #region 公共属性 - 当前统计

        /// <summary>
        /// 获取或设置当前胜场数
        /// </summary>
        [DefaultValue(0)]
        public int Wins
        {
            get { return _wins; }
            set
            {
                if (value.Equals(_wins)) return;
                _wins = value;
                NotifyPropertyChanged(() => Wins);
            }
        }

        /// <summary>
        /// 获取或设置当前败场数
        /// </summary>
        [DefaultValue(0)]
        public int Losses
        {
            get { return _losses; }
            set
            {
                if (value.Equals(_losses)) return;
                _losses = value;
                NotifyPropertyChanged(() => Losses);
            }
        }

        #endregion
    }
}
