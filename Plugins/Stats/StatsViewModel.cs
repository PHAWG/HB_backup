using System.Windows.Input;
using Triton.Common.Mvvm;

namespace Stats
{
    public class StatsViewModel : ViewModelBase
    {
        private readonly StatsSettings _settings;

        public StatsViewModel(StatsSettings settings)
        {
            _settings = settings;
            ResetCommand = new RelayCommand(ExecuteReset);
        }

        public int Wins { get => _settings.Wins; set { _settings.Wins = value; OnPropertyChanged(); } }
        public int Wins1 { get => _settings.Wins1; set { _settings.Wins1 = value; OnPropertyChanged(); } }
        public int Wins2 { get => _settings.Wins2; set { _settings.Wins2 = value; OnPropertyChanged(); } }
        public int Wins3 { get => _settings.Wins3; set { _settings.Wins3 = value; OnPropertyChanged(); } }
        public int Wins4 { get => _settings.Wins4; set { _settings.Wins4 = value; OnPropertyChanged(); } }
        public int Wins5 { get => _settings.Wins5; set { _settings.Wins5 = value; OnPropertyChanged(); } }
        public int Wins6 { get => _settings.Wins6; set { _settings.Wins6 = value; OnPropertyChanged(); } }
        public int Wins7 { get => _settings.Wins7; set { _settings.Wins7 = value; OnPropertyChanged(); } }
        public int Wins8 { get => _settings.Wins8; set { _settings.Wins8 = value; OnPropertyChanged(); } }
        public int Wins9 { get => _settings.Wins9; set { _settings.Wins9 = value; OnPropertyChanged(); } }
        public int Wins10 { get => _settings.Wins10; set { _settings.Wins10 = value; OnPropertyChanged(); } }
        public int Wins11 { get => _settings.Wins11; set { _settings.Wins11 = value; OnPropertyChanged(); } }

        public int Losses { get => _settings.Losses; set { _settings.Losses = value; OnPropertyChanged(); } }
        public int Losses1 { get => _settings.Losses1; set { _settings.Losses1 = value; OnPropertyChanged(); } }
        public int Losses2 { get => _settings.Losses2; set { _settings.Losses2 = value; OnPropertyChanged(); } }
        public int Losses3 { get => _settings.Losses3; set { _settings.Losses3 = value; OnPropertyChanged(); } }
        public int Losses4 { get => _settings.Losses4; set { _settings.Losses4 = value; OnPropertyChanged(); } }
        public int Losses5 { get => _settings.Losses5; set { _settings.Losses5 = value; OnPropertyChanged(); } }
        public int Losses6 { get => _settings.Losses6; set { _settings.Losses6 = value; OnPropertyChanged(); } }
        public int Losses7 { get => _settings.Losses7; set { _settings.Losses7 = value; OnPropertyChanged(); } }
        public int Losses8 { get => _settings.Losses8; set { _settings.Losses8 = value; OnPropertyChanged(); } }
        public int Losses9 { get => _settings.Losses9; set { _settings.Losses9 = value; OnPropertyChanged(); } }
        public int Losses10 { get => _settings.Losses10; set { _settings.Losses10 = value; OnPropertyChanged(); } }
        public int Losses11 { get => _settings.Losses11; set { _settings.Losses11 = value; OnPropertyChanged(); } }

        public string Winrate { get => _settings.Winrate; set { _settings.Winrate = value; OnPropertyChanged(); } }
        public string Winrate1 { get => _settings.Winrate1; set { _settings.Winrate1 = value; OnPropertyChanged(); } }
        public string Winrate2 { get => _settings.Winrate2; set { _settings.Winrate2 = value; OnPropertyChanged(); } }
        public string Winrate3 { get => _settings.Winrate3; set { _settings.Winrate3 = value; OnPropertyChanged(); } }
        public string Winrate4 { get => _settings.Winrate4; set { _settings.Winrate4 = value; OnPropertyChanged(); } }
        public string Winrate5 { get => _settings.Winrate5; set { _settings.Winrate5 = value; OnPropertyChanged(); } }
        public string Winrate6 { get => _settings.Winrate6; set { _settings.Winrate6 = value; OnPropertyChanged(); } }
        public string Winrate7 { get => _settings.Winrate7; set { _settings.Winrate7 = value; OnPropertyChanged(); } }
        public string Winrate8 { get => _settings.Winrate8; set { _settings.Winrate8 = value; OnPropertyChanged(); } }
        public string Winrate9 { get => _settings.Winrate9; set { _settings.Winrate9 = value; OnPropertyChanged(); } }
        public string Winrate10 { get => _settings.Winrate10; set { _settings.Winrate10 = value; OnPropertyChanged(); } }
        public string Winrate11 { get => _settings.Winrate11; set { _settings.Winrate11 = value; OnPropertyChanged(); } }

        public string Environment { get => _settings.environment; set { _settings.environment = value; OnPropertyChanged(); } }
        public string Environment1 { get => _settings.environment1; set { _settings.environment1 = value; OnPropertyChanged(); } }
        public string Environment2 { get => _settings.environment2; set { _settings.environment2 = value; OnPropertyChanged(); } }
        public string Environment3 { get => _settings.environment3; set { _settings.environment3 = value; OnPropertyChanged(); } }
        public string Environment4 { get => _settings.environment4; set { _settings.environment4 = value; OnPropertyChanged(); } }
        public string Environment5 { get => _settings.environment5; set { _settings.environment5 = value; OnPropertyChanged(); } }
        public string Environment6 { get => _settings.environment6; set { _settings.environment6 = value; OnPropertyChanged(); } }
        public string Environment7 { get => _settings.environment7; set { _settings.environment7 = value; OnPropertyChanged(); } }
        public string Environment8 { get => _settings.environment8; set { _settings.environment8 = value; OnPropertyChanged(); } }
        public string Environment9 { get => _settings.environment9; set { _settings.environment9 = value; OnPropertyChanged(); } }
        public string Environment10 { get => _settings.environment10; set { _settings.environment10 = value; OnPropertyChanged(); } }
        public string Environment11 { get => _settings.environment11; set { _settings.environment11 = value; OnPropertyChanged(); } }

        public ICommand ResetCommand { get; }

        public event System.Action RequestReset;

        private void ExecuteReset()
        {
            RequestReset?.Invoke();
        }
    }
}
