using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using log4net;
using Newtonsoft.Json;
using Triton.Bot.Settings;
using Triton.Common;
using Logger = Triton.Common.LogUtilities.Logger;

namespace Stats
{
    /// <summary>
    /// 统计插件设置类
    /// <para>存储统计插件的所有配置和运行时数据</para>
    /// <para>继承自 JsonSettings，支持 JSON 序列化/反序列化和属性变更通知</para>
    /// </summary>
    /// <remarks>
    /// 主要功能：
    /// <list type="bullet">
    ///     <item><description>记录总胜败场数（Wins/Losses）</description></item>
    ///     <item><description>按职业记录胜败场数（Wins1-11/Losses1-11）</description></item>
    ///     <item><description>计算总胜率和各职业胜率（Winrate/Winrate1-11）</description></item>
    ///     <item><description>统计对战环境分布（environment/environment1-11）</description></item>
    /// </list>
    /// 职业编号对应：
    /// <list type="bullet">
    ///     <item><description>1=战士, 2=萨满, 3=盗贼, 4=帕拉丁, 5=猎人</description></item>
    ///     <item><description>6=德鲁伊, 7=术士, 8=法师, 9=牧师, 10=恶魔猎手, 11=死亡骑士</description></item>
    /// </list>
    /// </remarks>
    public class StatsSettings : JsonSettings
    {
        private static readonly ILog Log = Logger.GetLoggerInstanceForType();

        private static StatsSettings _instance;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static StatsSettings Instance
        {
            get { return _instance ?? (_instance = new StatsSettings()); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public StatsSettings()
            : base(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "Stats")))
        {
        }

        /// <summary>
        /// 重新加载设置文件
        /// </summary>
        public void ReloadFile()
        {
            Reload(GetSettingsFilePath(Configuration.Instance.Name,
                string.Format("{0}.json", "Stats" + GetMyHashCode())));
        }

        #region 私有字段 - 总胜败统计

        /// <summary>总胜场数</summary>
        private int _wins;
        /// <summary>总败场数</summary>
        private int _losses;

        #endregion

        #region 私有字段 - 各职业胜场

        /// <summary>战士胜场</summary>
        private int _wins1;
        /// <summary>萨满胜场</summary>
        private int _wins2;
        /// <summary>盗贼胜场</summary>
        private int _wins3;
        /// <summary>帕拉丁胜场</summary>
        private int _wins4;
        /// <summary>猎人胜场</summary>
        private int _wins5;
        /// <summary>德鲁伊胜场</summary>
        private int _wins6;
        /// <summary>术士胜场</summary>
        private int _wins7;
        /// <summary>法师胜场</summary>
        private int _wins8;
        /// <summary>牧师胜场</summary>
        private int _wins9;
        /// <summary>恶魔猎手胜场</summary>
        private int _wins10;
        /// <summary>死亡骑士胜场</summary>
        private int _wins11;

        #endregion

        #region 私有字段 - 各职业败场

        /// <summary>战士败场</summary>
        private int _losses1;
        /// <summary>萨满败场</summary>
        private int _losses2;
        /// <summary>盗贼败场</summary>
        private int _losses3;
        /// <summary>帕拉丁败场</summary>
        private int _losses4;
        /// <summary>猎人败场</summary>
        private int _losses5;
        /// <summary>德鲁伊败场</summary>
        private int _losses6;
        /// <summary>术士败场</summary>
        private int _losses7;
        /// <summary>法师败场</summary>
        private int _losses8;
        /// <summary>牧师败场</summary>
        private int _losses9;
        /// <summary>恶魔猎手败场</summary>
        private int _losses10;
        /// <summary>死亡骑士败场</summary>
        private int _losses11;

        #endregion

        #region 私有字段 - 胜率

        /// <summary>总胜率（格式如 "65.5%"）</summary>
        private string _winrate;
        /// <summary>对战战士胜率</summary>
        private string _winrate1;
        /// <summary>对战萨满胜率</summary>
        private string _winrate2;
        /// <summary>对战盗贼胜率</summary>
        private string _winrate3;
        /// <summary>对战帕拉丁胜率</summary>
        private string _winrate4;
        /// <summary>对战猎人胜率</summary>
        private string _winrate5;
        /// <summary>对战德鲁伊胜率</summary>
        private string _winrate6;
        /// <summary>对战术士胜率</summary>
        private string _winrate7;
        /// <summary>对战法师胜率</summary>
        private string _winrate8;
        /// <summary>对战牧师胜率</summary>
        private string _winrate9;
        /// <summary>对战恶魔猎手胜率</summary>
        private string _winrate10;
        /// <summary>对战死亡骑士胜率</summary>
        private string _winrate11;

        #endregion

        #region 私有字段 - 环境分布

        /// <summary>总对战环境分布（格式如 "战士(15%) 萨满(10%)..."）</summary>
        private string _environment;
        /// <summary>战士占比</summary>
        private string _environment1;
        /// <summary>萨满占比</summary>
        private string _environment2;
        /// <summary>盗贼占比</summary>
        private string _environment3;
        /// <summary>帕拉丁占比</summary>
        private string _environment4;
        /// <summary>猎人占比</summary>
        private string _environment5;
        /// <summary>德鲁伊占比</summary>
        private string _environment6;
        /// <summary>术士占比</summary>
        private string _environment7;
        /// <summary>法师占比</summary>
        private string _environment8;
        /// <summary>牧师占比</summary>
        private string _environment9;
        /// <summary>恶魔猎手占比</summary>
        private string _environment10;
        /// <summary>死亡骑士占比</summary>
        private string _environment11;

        #endregion

        #region 公共属性 - 总胜败统计

        /// <summary>
        /// 获取或设置总胜场数
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

        #endregion

        #region 公共属性 - 各职业胜场

        /// <summary>获取或设置战士胜场</summary>
        [DefaultValue(0)]
        public int Wins1 { get { return _wins1; } set { if (value.Equals(_wins1)) return; _wins1 = value; NotifyPropertyChanged(() => Wins1); } }

        /// <summary>获取或设置萨满胜场</summary>
        [DefaultValue(0)]
        public int Wins2 { get { return _wins2; } set { if (value.Equals(_wins2)) return; _wins2 = value; NotifyPropertyChanged(() => Wins2); } }

        /// <summary>获取或设置盗贼胜场</summary>
        [DefaultValue(0)]
        public int Wins3 { get { return _wins3; } set { if (value.Equals(_wins3)) return; _wins3 = value; NotifyPropertyChanged(() => Wins3); } }

        /// <summary>获取或设置帕拉丁胜场</summary>
        [DefaultValue(0)]
        public int Wins4 { get { return _wins4; } set { if (value.Equals(_wins4)) return; _wins4 = value; NotifyPropertyChanged(() => Wins4); } }

        /// <summary>获取或设置猎人胜场</summary>
        [DefaultValue(0)]
        public int Wins5 { get { return _wins5; } set { if (value.Equals(_wins5)) return; _wins5 = value; NotifyPropertyChanged(() => Wins5); } }

        /// <summary>获取或设置德鲁伊胜场</summary>
        [DefaultValue(0)]
        public int Wins6 { get { return _wins6; } set { if (value.Equals(_wins6)) return; _wins6 = value; NotifyPropertyChanged(() => Wins6); } }

        /// <summary>获取或设置术士胜场</summary>
        [DefaultValue(0)]
        public int Wins7 { get { return _wins7; } set { if (value.Equals(_wins7)) return; _wins7 = value; NotifyPropertyChanged(() => Wins7); } }

        /// <summary>获取或设置法师胜场</summary>
        [DefaultValue(0)]
        public int Wins8 { get { return _wins8; } set { if (value.Equals(_wins8)) return; _wins8 = value; NotifyPropertyChanged(() => Wins8); } }

        /// <summary>获取或设置牧师胜场</summary>
        [DefaultValue(0)]
        public int Wins9 { get { return _wins9; } set { if (value.Equals(_wins9)) return; _wins9 = value; NotifyPropertyChanged(() => Wins9); } }

        /// <summary>获取或设置恶魔猎手胜场</summary>
        [DefaultValue(0)]
        public int Wins10 { get { return _wins10; } set { if (value.Equals(_wins10)) return; _wins10 = value; NotifyPropertyChanged(() => Wins10); } }

        /// <summary>获取或设置死亡骑士胜场</summary>
        [DefaultValue(0)]
        public int Wins11 { get { return _wins11; } set { if (value.Equals(_wins11)) return; _wins11 = value; NotifyPropertyChanged(() => Wins11); } }

        #endregion

        #region 公共属性 - 各职业败场

        /// <summary>获取或设置总败场数</summary>
        [DefaultValue(0)]
        public int Losses { get { return _losses; } set { if (value.Equals(_losses)) return; _losses = value; NotifyPropertyChanged(() => Losses); } }

        /// <summary>获取或设置战士败场</summary>
        [DefaultValue(0)]
        public int Losses1 { get { return _losses1; } set { if (value.Equals(_losses1)) return; _losses1 = value; NotifyPropertyChanged(() => Losses1); } }

        /// <summary>获取或设置萨满败场</summary>
        [DefaultValue(0)]
        public int Losses2 { get { return _losses2; } set { if (value.Equals(_losses2)) return; _losses2 = value; NotifyPropertyChanged(() => Losses2); } }

        /// <summary>获取或设置盗贼败场</summary>
        [DefaultValue(0)]
        public int Losses3 { get { return _losses3; } set { if (value.Equals(_losses3)) return; _losses3 = value; NotifyPropertyChanged(() => Losses3); } }

        /// <summary>获取或设置帕拉丁败场</summary>
        [DefaultValue(0)]
        public int Losses4 { get { return _losses4; } set { if (value.Equals(_losses4)) return; _losses4 = value; NotifyPropertyChanged(() => Losses4); } }

        /// <summary>获取或设置猎人败场</summary>
        [DefaultValue(0)]
        public int Losses5 { get { return _losses5; } set { if (value.Equals(_losses5)) return; _losses5 = value; NotifyPropertyChanged(() => Losses5); } }

        /// <summary>获取或设置德鲁伊败场</summary>
        [DefaultValue(0)]
        public int Losses6 { get { return _losses6; } set { if (value.Equals(_losses6)) return; _losses6 = value; NotifyPropertyChanged(() => Losses6); } }

        /// <summary>获取或设置术士败场</summary>
        [DefaultValue(0)]
        public int Losses7 { get { return _losses7; } set { if (value.Equals(_losses7)) return; _losses7 = value; NotifyPropertyChanged(() => Losses7); } }

        /// <summary>获取或设置法师败场</summary>
        [DefaultValue(0)]
        public int Losses8 { get { return _losses8; } set { if (value.Equals(_losses8)) return; _losses8 = value; NotifyPropertyChanged(() => Losses8); } }

        /// <summary>获取或设置牧师败场</summary>
        [DefaultValue(0)]
        public int Losses9 { get { return _losses9; } set { if (value.Equals(_losses9)) return; _losses9 = value; NotifyPropertyChanged(() => Losses9); } }

        /// <summary>获取或设置恶魔猎手败场</summary>
        [DefaultValue(0)]
        public int Losses10 { get { return _losses10; } set { if (value.Equals(_losses10)) return; _losses10 = value; NotifyPropertyChanged(() => Losses10); } }

        /// <summary>获取或设置死亡骑士败场</summary>
        [DefaultValue(0)]
        public int Losses11 { get { return _losses11; } set { if (value.Equals(_losses11)) return; _losses11 = value; NotifyPropertyChanged(() => Losses11); } }

        #endregion

        #region 公共属性 - 胜率

        /// <summary>获取或设置总胜率（格式如 "65.5%"）</summary>
        [DefaultValue("0")]
        public string Winrate { get { return _winrate; } set { if (value.Equals(_winrate)) return; _winrate = value; NotifyPropertyChanged(() => Winrate); } }

        /// <summary>获取或设置对战战士胜率</summary>
        [DefaultValue("0")]
        public string Winrate1 { get { return _winrate1; } set { if (value.Equals(_winrate1)) return; _winrate1 = value; NotifyPropertyChanged(() => Winrate1); } }

        /// <summary>获取或设置对战萨满胜率</summary>
        [DefaultValue("0")]
        public string Winrate2 { get { return _winrate2; } set { if (value.Equals(_winrate2)) return; _winrate2 = value; NotifyPropertyChanged(() => Winrate2); } }

        /// <summary>获取或设置对战盗贼胜率</summary>
        [DefaultValue("0")]
        public string Winrate3 { get { return _winrate3; } set { if (value.Equals(_winrate3)) return; _winrate3 = value; NotifyPropertyChanged(() => Winrate3); } }

        /// <summary>获取或设置对战帕拉丁胜率</summary>
        [DefaultValue("0")]
        public string Winrate4 { get { return _winrate4; } set { if (value.Equals(_winrate4)) return; _winrate4 = value; NotifyPropertyChanged(() => Winrate4); } }

        /// <summary>获取或设置对战猎人胜率</summary>
        [DefaultValue("0")]
        public string Winrate5 { get { return _winrate5; } set { if (value.Equals(_winrate5)) return; _winrate5 = value; NotifyPropertyChanged(() => Winrate5); } }

        /// <summary>获取或设置对战德鲁伊胜率</summary>
        [DefaultValue("0")]
        public string Winrate6 { get { return _winrate6; } set { if (value.Equals(_winrate6)) return; _winrate6 = value; NotifyPropertyChanged(() => Winrate6); } }

        /// <summary>获取或设置对战术士胜率</summary>
        [DefaultValue("0")]
        public string Winrate7 { get { return _winrate7; } set { if (value.Equals(_winrate7)) return; _winrate7 = value; NotifyPropertyChanged(() => Winrate7); } }

        /// <summary>获取或设置对战法师胜率</summary>
        [DefaultValue("0")]
        public string Winrate8 { get { return _winrate8; } set { if (value.Equals(_winrate8)) return; _winrate8 = value; NotifyPropertyChanged(() => Winrate8); } }

        /// <summary>获取或设置对战牧师胜率</summary>
        [DefaultValue("0")]
        public string Winrate9 { get { return _winrate9; } set { if (value.Equals(_winrate9)) return; _winrate9 = value; NotifyPropertyChanged(() => Winrate9); } }

        /// <summary>获取或设置对战恶魔猎手胜率</summary>
        [DefaultValue("0")]
        public string Winrate10 { get { return _winrate10; } set { if (value.Equals(_winrate10)) return; _winrate10 = value; NotifyPropertyChanged(() => Winrate10); } }

        /// <summary>获取或设置对战死亡骑士胜率</summary>
        [DefaultValue("0")]
        public string Winrate11 { get { return _winrate11; } set { if (value.Equals(_winrate11)) return; _winrate11 = value; NotifyPropertyChanged(() => Winrate11); } }

        #endregion

        #region 公共属性 - 环境分布

        /// <summary>获取或设置总对战环境分布</summary>
        [DefaultValue("0")]
        public string environment { get { return _environment; } set { if (value.Equals(_environment)) return; _environment = value; NotifyPropertyChanged(() => environment); } }

        /// <summary>获取或设置战士占比</summary>
        [DefaultValue("0")]
        public string environment1 { get { return _environment1; } set { if (value.Equals(_environment1)) return; _environment1 = value; NotifyPropertyChanged(() => environment1); } }

        /// <summary>获取或设置萨满占比</summary>
        [DefaultValue("0")]
        public string environment2 { get { return _environment2; } set { if (value.Equals(_environment2)) return; _environment2 = value; NotifyPropertyChanged(() => environment2); } }

        /// <summary>获取或设置盗贼占比</summary>
        [DefaultValue("0")]
        public string environment3 { get { return _environment3; } set { if (value.Equals(_environment3)) return; _environment3 = value; NotifyPropertyChanged(() => environment3); } }

        /// <summary>获取或设置帕拉丁占比</summary>
        [DefaultValue("0")]
        public string environment4 { get { return _environment4; } set { if (value.Equals(_environment4)) return; _environment4 = value; NotifyPropertyChanged(() => environment4); } }

        /// <summary>获取或设置猎人占比</summary>
        [DefaultValue("0")]
        public string environment5 { get { return _environment5; } set { if (value.Equals(_environment5)) return; _environment5 = value; NotifyPropertyChanged(() => environment5); } }

        /// <summary>获取或设置德鲁伊占比</summary>
        [DefaultValue("0")]
        public string environment6 { get { return _environment6; } set { if (value.Equals(_environment6)) return; _environment6 = value; NotifyPropertyChanged(() => environment6); } }

        /// <summary>获取或设置术士占比</summary>
        [DefaultValue("0")]
        public string environment7 { get { return _environment7; } set { if (value.Equals(_environment7)) return; _environment7 = value; NotifyPropertyChanged(() => environment7); } }

        /// <summary>获取或设置法师占比</summary>
        [DefaultValue("0")]
        public string environment8 { get { return _environment8; } set { if (value.Equals(_environment8)) return; _environment8 = value; NotifyPropertyChanged(() => environment8); } }

        /// <summary>获取或设置牧师占比</summary>
        [DefaultValue("0")]
        public string environment9 { get { return _environment9; } set { if (value.Equals(_environment9)) return; _environment9 = value; NotifyPropertyChanged(() => environment9); } }

        /// <summary>获取或设置恶魔猎手占比</summary>
        [DefaultValue("0")]
        public string environment10 { get { return _environment10; } set { if (value.Equals(_environment10)) return; _environment10 = value; NotifyPropertyChanged(() => environment10); } }

        /// <summary>获取或设置死亡骑士占比</summary>
        [DefaultValue("0")]
        public string environment11 { get { return _environment11; } set { if (value.Equals(_environment11)) return; _environment11 = value; NotifyPropertyChanged(() => environment11); } }

        #endregion
    }
}
