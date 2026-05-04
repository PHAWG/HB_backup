using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using log4net;
using Newtonsoft.Json;
using Triton.Bot.Settings;
using Triton.Common;
using Triton.Game.Mapping;
using Logger1 = Triton.Common.LogUtilities.Logger;

namespace Monitor
{
    /// <summary>
    /// 监控插件设置类
    /// <para>存储监控插件的所有配置和运行时数据</para>
    /// <para>继承自 JsonSettings，支持 JSON 序列化/反序列化和属性变更通知</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>记录游戏胜负统计（Wins/Losses/Concedes）</description></item>
    ///     <item><description>显示战令等级和经验值（Level/Xp/XpNeeded）</description></item>
    ///     <item><description>计算累计经验、每小时经验效率</description></item>
    ///     <item><description>显示各模式天梯排名信息</description></item>
    ///     <item><description>显示账号金币/粉尘/门票信息</description></item>
    /// </list>
    /// </remarks>
    public class MonitorSettings : JsonSettings
    {
        /// <summary>日志记录器实例</summary>
        private static readonly ILog Log = Logger1.GetLoggerInstanceForType();

        /// <summary>单例实例</summary>
        private static MonitorSettings _instance;

        /// <summary>
        /// 获取单例实例
        /// <para>使用延迟初始化，首次访问时创建实例</para>
        /// </summary>
        public static MonitorSettings Instance
        {
            get { return _instance ?? (_instance = new MonitorSettings()); }
        }

        /// <summary>
        /// 构造函数
        /// /// <para>初始化设置文件路径为 "Monitor.json"</para>
        /// </summary>
        public MonitorSettings()
            : base(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "Monitor")))
        {
        }

        /// <summary>
        /// 重新加载设置文件
        /// <para>从磁盘重新读取配置，用于多账号场景</para>
        /// </summary>
        public void ReloadFile()
        {
            Reload(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "Monitor" + GetMyHashCode())));
        }

        #region 私有字段 - 对战统计

        /// <summary>总胜场数</summary>
        private int _wins;

        /// <summary>总败场数（不含投降）</summary>
        private int _losses;

        /// <summary>总投降次数</summary>
        private int _concedes;

        #endregion

        #region 私有字段 - 战令经验

        /// <summary>当前战令等级</summary>
        private int _level;

        /// <summary>当前战令经验值</summary>
        private int _xp;

        /// <summary>升级所需经验值</summary>
        private int _xpNeeded;

        /// <summary>当前累计总经验值（从1级到当前的经验总和）</summary>
        private int _allXp;

        /// <summary>满级所需总经验值（固定值 602200）</summary>
        private int _allXpNeeded;

        #endregion

        #region 私有字段 - 运行统计

        /// <summary>总运行时间（秒）</summary>
        private long _allRunTime;

        /// <summary>累计获得的总经验值</summary>
        private long _allGetXp;

        /// <summary>每小时经验值（数值）</summary>
        private int _perHourXp;

        /// <summary>每小时经验值（字符串格式，如 "500/小时"）</summary>
        private string _perHourXpStr;

        /// <summary>距离满级还差的经验值</summary>
        private int _fullXpNeeded;

        /// <summary>预估满级所需时间</summary>
        private string _fullTimeNeeded;

        #endregion

        #region 私有字段 - 账户信息

        /// <summary>收藏品信息（金币/粉尘/门票）</summary>
        private string _collection;

        /// <summary>旅店通票到期时间</summary>
        private string _passportEnd;

        #endregion

        #region 私有字段 - 天梯排名

        /// <summary>经典模式排名信息</summary>
        private string _classicInfo;

        /// <summary>标准模式排名信息</summary>
        private string _standardInfo;

        /// <summary>狂野模式排名信息</summary>
        private string _wildInfo;

        /// <summary>幻变模式排名信息</summary>
        private string _twistInfo;

        #endregion

        #region 公共属性 - 对战统计

        /// <summary>
        /// 获取或设置总胜场数
        /// <para>每场胜利后 +1</para>
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
        /// 获取或设置总败场数
        /// <para>每场失败且非投降后 +1</para>
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

        /// <summary>
        /// 获取或设置总投降次数
        /// <para>每场投降后 +1</para>
        /// </summary>
        [DefaultValue(0)]
        public int Concedes
        {
            get { return _concedes; }
            set
            {
                if (value.Equals(_concedes)) return;
                _concedes = value;
                NotifyPropertyChanged(() => Concedes);
            }
        }

        #endregion

        #region 公共属性 - 战令经验

        /// <summary>
        /// 获取或设置当前战令等级
        /// <para>从游戏内 RewardTrack 数据获取</para>
        /// </summary>
        [DefaultValue(0)]
        public int Level
        {
            get { return _level; }
            set
            {
                if (value.Equals(_level)) return;
                _level = value;
                NotifyPropertyChanged(() => Level);
            }
        }

        /// <summary>
        /// 获取或设置当前战令经验值
        /// <para>从游戏内 RewardTrack.TrackDataModel.Xp 获取</para>
        /// </summary>
        [DefaultValue(0)]
        public int Xp
        {
            get { return _xp; }
            set
            {
                if (value.Equals(_xp)) return;
                _xp = value;
                NotifyPropertyChanged(() => Xp);
            }
        }

        /// <summary>
        /// 获取或设置升级所需经验值
        /// <para>从游戏内 RewardTrack.TrackDataModel.XpNeeded 获取</para>
        /// </summary>
        [DefaultValue(0)]
        public int XpNeeded
        {
            get { return _xpNeeded; }
            set
            {
                if (value.Equals(_xpNeeded)) return;
                _xpNeeded = value;
                NotifyPropertyChanged(() => XpNeeded);
            }
        }

        /// <summary>
        /// 获取或设置累计总经验值
        /// <para>根据等级查表计算：前(level-1)级所需经验 + 当前经验</para>
        /// </summary>
        [DefaultValue(0)]
        public int AllXp
        {
            get { return _allXp; }
            set
            {
                if (value.Equals(_allXp)) return;
                _allXp = value;
                NotifyPropertyChanged(() => AllXp);
            }
        }

        /// <summary>
        /// 获取或设置满级所需总经验值
        /// <para>默认 602200（199级满级所需总经验）</para>
        /// </summary>
        [DefaultValue(602200)]
        public int AllXpNeeded
        {
            get { return _allXpNeeded; }
            set
            {
                if (value.Equals(_allXpNeeded)) return;
                _allXpNeeded = value;
                NotifyPropertyChanged(() => AllXpNeeded);
            }
        }

        #endregion

        #region 公共属性 - 运行统计

        /// <summary>
        /// 获取或设置总运行时间（秒）
        /// <para>设置时自动更新以下派生属性：</para>
        /// <list type="bullet">
        ///     <item><description>AllRunTimeText: 格式化的时间文本</description></item>
        ///     <item><description>PerHourXp: 每小时经验值</description></item>
        /// </list>
        /// </summary>
        [DefaultValue(0)]
        public long AllRunTime
        {
            get { return _allRunTime; }
            set
            {
                if (value.Equals(_allRunTime)) return;
                _allRunTime = value;
                NotifyPropertyChanged(() => AllRunTime);

                // 自动计算派生值
                float t = ((float)_allRunTime / 3600);
                AllRunTimeText = t.ToString("F2");
                if (t > 0) PerHourXp = (int)(AllGetXp / t);
                else PerHourXp = 0;

                // 限制每小时经验在合理范围（300-700），超出则使用默认值
                if (PerHourXp <= 300 || PerHourXp >= 700) PerHourXp = 500;
            }
        }

        /// <summary>
        /// 获取或设置总运行时间文本
        /// <para>格式如 "25.50" 表示 25.5 小时</para>
        /// </summary>
        [DefaultValue("0.0")]
        public string AllRunTimeText
        {
            get { return allRunTimeText; }
            set
            {
                if (!value.Equals(allRunTimeText))
                {
                    allRunTimeText = value;
                    NotifyPropertyChanged(() => AllRunTimeText);
                }
            }
        }

        /// <summary>
        /// 获取或设置累计获得的总经验值
        /// <para>设置时自动更新 PerHourXp</para>
        /// </summary>
        [DefaultValue(0)]
        public long AllGetXp
        {
            get { return _allGetXp; }
            set
            {
                if (value.Equals(_allGetXp)) return;
                _allGetXp = value;
                NotifyPropertyChanged(() => AllGetXp);

                // 自动计算每小时经验
                float t = ((float)AllRunTime / 3600);
                if (t > 0) PerHourXp = (int)(value / t);
                else PerHourXp = 0;

                if (PerHourXp <= 300 || PerHourXp >= 700) PerHourXp = 500;
            }
        }

        /// <summary>
        /// 获取或设置每小时经验值
        /// <para>范围 300-700，超出自动设为 500</para>
        /// <para>设置时自动更新 PerHourXpStr 字符串</para>
        /// </summary>
        [DefaultValue(500)]
        public int PerHourXp
        {
            get { return _perHourXp; }
            set
            {
                if (value.Equals(_perHourXp)) return;
                _perHourXp = value;
                PerHourXpStr = string.Format("{0}/小时", _perHourXp);
                NotifyPropertyChanged(() => PerHourXp);
            }
        }

        /// <summary>
        /// 获取或设置每小时经验字符串
        /// <para>格式如 "500/小时"</para>
        /// </summary>
        [DefaultValue("500/小时")]
        public string PerHourXpStr
        {
            get { return _perHourXpStr; }
            set
            {
                if (value.Equals(_perHourXpStr)) return;
                _perHourXpStr = value;
                NotifyPropertyChanged(() => PerHourXpStr);
            }
        }

        /// <summary>
        /// 获取或设置距离满级还差的经验值
        /// <para>计算公式：AllXpNeeded - AllXp</para>
        /// </summary>
        [DefaultValue(602200)]
        public int FullXpNeeded
        {
            get { return _fullXpNeeded; }
            set
            {
                if (value.Equals(_fullXpNeeded)) return;
                _fullXpNeeded = value;
                NotifyPropertyChanged(() => FullXpNeeded);
            }
        }

        /// <summary>
        /// 获取或设置预估满级所需时间
        /// <para>格式如 "25天3小时" 或 "恭喜满级"</para>
        /// </summary>
        [DefaultValue("未知")]
        public string FullTimeNeeded
        {
            get { return _fullTimeNeeded; }
            set
            {
                if (value.Equals(_fullTimeNeeded)) return;
                _fullTimeNeeded = value;
                NotifyPropertyChanged(() => FullTimeNeeded);
            }
        }

        #endregion

        #region 公共属性 - 账户信息

        /// <summary>
        /// 获取或设置收藏品信息
        /// <para>格式如 "金币(9340) 粉尘(49052) 门票(0)"</para>
        /// </summary>
        [DefaultValue("未知")]
        public string Collection
        {
            get { return _collection; }
            set
            {
                if (value.Equals(_collection)) return;
                _collection = value;
                NotifyPropertyChanged(() => Collection);
            }
        }

        /// <summary>
        /// 获取或设置旅店通票到期时间
        /// <para>格式如 "3.26-7.26 已过xx天xx小时 还剩xx天"</para>
        /// </summary>
        [DefaultValue("未知")]
        public string PassportEnd
        {
            get { return _passportEnd; }
            set
            {
                if (value.Equals(_passportEnd)) return;
                _passportEnd = value;
                NotifyPropertyChanged(() => PassportEnd);
            }
        }

        #endregion

        #region 公共属性 - 天梯排名

        /// <summary>
        /// 获取或设置经典模式天梯排名信息
        /// <para>格式如 "青铜10 0星 0/0(胜0.00%)"</para>
        /// </summary>
        [DefaultValue("未知")]
        public string ClassicInfo
        {
            get { return _classicInfo; }
            set
            {
                if (value.Equals(_classicInfo)) return;
                _classicInfo = value;
                NotifyPropertyChanged(() => ClassicInfo);
            }
        }

        /// <summary>
        /// 获取或设置标准模式天梯排名信息
        /// </summary>
        [DefaultValue("未知")]
        public string StandardInfo
        {
            get { return _standardInfo; }
            set
            {
                if (value.Equals(_standardInfo)) return;
                _standardInfo = value;
                NotifyPropertyChanged(() => StandardInfo);
            }
        }

        /// <summary>
        /// 获取或设置狂野模式天梯排名信息
        /// </summary>
        [DefaultValue("未知")]
        public string WildInfo
        {
            get { return _wildInfo; }
            set
            {
                if (value.Equals(_wildInfo)) return;
                _wildInfo = value;
                NotifyPropertyChanged(() => WildInfo);
            }
        }

        /// <summary>
        /// 获取或设置幻变模式天梯排名信息
        /// </summary>
        [DefaultValue("未知")]
        public string TwistInfo
        {
            get { return _twistInfo; }
            set
            {
                if (value.Equals(_twistInfo)) return;
                _twistInfo = value;
                NotifyPropertyChanged(() => TwistInfo);
            }
        }

        #endregion
    }
}
