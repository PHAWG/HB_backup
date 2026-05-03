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
    /// <summary>Settings for the Stats plugin. </summary>
    public class AutoStopSettings : JsonSettings
    {
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();

        private static AutoStopSettings _instance;

        /// <summary>The current instance for this class. </summary>
        public static AutoStopSettings Instance
        {
            get { return _instance ?? (_instance = new AutoStopSettings()); }
        }


        /// <summary>The default ctor. Will use the settings path "Stats".</summary>
        public AutoStopSettings()
            : base(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "AutoStop")))
        {

        }

        public void ReloadFile()
        {
            Reload(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "AutoStop" + GetMyHashCode())));
        }

        private bool _stopAfterXGames;
        private bool _stopAfterXWins;
        private bool _stopAfterXLosses;
        private bool _concedeAfterXMinutes;

        private int _stopGameCount;
        private int _stopWinCount;
        private int _stopLossCount;
        private int _concedeMinutesCount;
        private bool _dynamicFacePenaltyEnabled;
        private int _dynamicFacePenaltyMinutes;
        private int _facePenaltyBeforeTime;
        private int _facePenaltyAfterTime;



        private int _wins;
        private int _losses;

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

        public void Reset()
        {
            Wins = 0;
            Losses = 0;
            
          //  Rank = TritonHs.MyRank;
        }


        [DefaultValue(0)]
        public int Wins
        {
            get { return _wins; }
            set
            {
                if (value.Equals(_wins))
                {
                    return;
                }
                _wins = value;
                NotifyPropertyChanged(() => Wins);
            }
        }

        /// <summary>Current stored losses.</summary>
        [DefaultValue(0)]
        public int Losses
        {
            get { return _losses; }
            set
            {
                if (value.Equals(_losses))
                {
                    return;
                }
                _losses = value;
                NotifyPropertyChanged(() => Losses);
            }
        }

        /// <summary>Current stored concedes.</summary>


        /// <summary>
        /// How many games should the bot play before stopping?
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
        /// How many games should the bot win before stopping?
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
        /// How many games should the bot lose before stopping?
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

        /// <summary>
        /// How many games should the bot concede before stopping?
        /// </summary>


        /// <summary>
        /// Should the bot stop after each game played?
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
        /// Should the bot stop after each win?
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
        /// Should the bot stop after each loss?
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
        /// 是否在游戏进行指定分钟数后自动投降
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
        /// 游戏进行多少分钟后自动投降
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

    }
}
