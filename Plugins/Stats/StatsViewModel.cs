using System.Windows.Input;
using Triton.Common.Mvvm;

namespace Stats
{
    /// <summary>
    /// 统计插件 ViewModel
    /// <para>负责管理 Stats 插件设置界面的数据绑定和交互逻辑</para>
    /// <para>用于显示和统计游戏对局数据</para>
    /// </summary>
    public class StatsViewModel : ViewModelBase
    {
        private readonly StatsSettings _settings;

        public StatsViewModel(StatsSettings settings)
        {
            _settings = settings;
            ResetCommand = new RelayCommand(ExecuteReset);
            _settings.PropertyChanged += Settings_PropertyChanged;
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public int Wins { get => _settings.Wins; set { _settings.Wins = value; } }
        public int Wins1 { get => _settings.Wins1; set { _settings.Wins1 = value; } }
        public int Wins2 { get => _settings.Wins2; set { _settings.Wins2 = value; } }
        public int Wins3 { get => _settings.Wins3; set { _settings.Wins3 = value; } }
        public int Wins4 { get => _settings.Wins4; set { _settings.Wins4 = value; } }
        public int Wins5 { get => _settings.Wins5; set { _settings.Wins5 = value; } }
        public int Wins6 { get => _settings.Wins6; set { _settings.Wins6 = value; } }
        public int Wins7 { get => _settings.Wins7; set { _settings.Wins7 = value; } }
        public int Wins8 { get => _settings.Wins8; set { _settings.Wins8 = value; } }
        public int Wins9 { get => _settings.Wins9; set { _settings.Wins9 = value; } }
        public int Wins10 { get => _settings.Wins10; set { _settings.Wins10 = value; } }
        public int Wins11 { get => _settings.Wins11; set { _settings.Wins11 = value; } }

        public int Losses { get => _settings.Losses; set { _settings.Losses = value; } }
        public int Losses1 { get => _settings.Losses1; set { _settings.Losses1 = value; } }
        public int Losses2 { get => _settings.Losses2; set { _settings.Losses2 = value; } }
        public int Losses3 { get => _settings.Losses3; set { _settings.Losses3 = value; } }
        public int Losses4 { get => _settings.Losses4; set { _settings.Losses4 = value; } }
        public int Losses5 { get => _settings.Losses5; set { _settings.Losses5 = value; } }
        public int Losses6 { get => _settings.Losses6; set { _settings.Losses6 = value; } }
        public int Losses7 { get => _settings.Losses7; set { _settings.Losses7 = value; } }
        public int Losses8 { get => _settings.Losses8; set { _settings.Losses8 = value; } }
        public int Losses9 { get => _settings.Losses9; set { _settings.Losses9 = value; } }
        public int Losses10 { get => _settings.Losses10; set { _settings.Losses10 = value; } }
        public int Losses11 { get => _settings.Losses11; set { _settings.Losses11 = value; } }

        public string Winrate { get => _settings.Winrate; set { _settings.Winrate = value; } }
        public string Winrate1 { get => _settings.Winrate1; set { _settings.Winrate1 = value; } }
        public string Winrate2 { get => _settings.Winrate2; set { _settings.Winrate2 = value; } }
        public string Winrate3 { get => _settings.Winrate3; set { _settings.Winrate3 = value; } }
        public string Winrate4 { get => _settings.Winrate4; set { _settings.Winrate4 = value; } }
        public string Winrate5 { get => _settings.Winrate5; set { _settings.Winrate5 = value; } }
        public string Winrate6 { get => _settings.Winrate6; set { _settings.Winrate6 = value; } }
        public string Winrate7 { get => _settings.Winrate7; set { _settings.Winrate7 = value; } }
        public string Winrate8 { get => _settings.Winrate8; set { _settings.Winrate8 = value; } }
        public string Winrate9 { get => _settings.Winrate9; set { _settings.Winrate9 = value; } }
        public string Winrate10 { get => _settings.Winrate10; set { _settings.Winrate10 = value; } }
        public string Winrate11 { get => _settings.Winrate11; set { _settings.Winrate11 = value; } }

        public string Environment { get => _settings.environment; set { _settings.environment = value; } }
        public string Environment1 { get => _settings.environment1; set { _settings.environment1 = value; } }
        public string Environment2 { get => _settings.environment2; set { _settings.environment2 = value; } }
        public string Environment3 { get => _settings.environment3; set { _settings.environment3 = value; } }
        public string Environment4 { get => _settings.environment4; set { _settings.environment4 = value; } }
        public string Environment5 { get => _settings.environment5; set { _settings.environment5 = value; } }
        public string Environment6 { get => _settings.environment6; set { _settings.environment6 = value; } }
        public string Environment7 { get => _settings.environment7; set { _settings.environment7 = value; } }
        public string Environment8 { get => _settings.environment8; set { _settings.environment8 = value; } }
        public string Environment9 { get => _settings.environment9; set { _settings.environment9 = value; } }
        public string Environment10 { get => _settings.environment10; set { _settings.environment10 = value; } }
        public string Environment11 { get => _settings.environment11; set { _settings.environment11 = value; } }

        public ICommand ResetCommand { get; }
        public event System.Action RequestReset;

        private void ExecuteReset() => RequestReset?.Invoke();
    }
}
