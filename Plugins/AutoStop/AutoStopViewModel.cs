using System.Windows.Input;
using Triton.Common.Mvvm;

namespace AutoStop
{
    public class AutoStopViewModel : ViewModelBase
    {
        private readonly AutoStopSettings _settings;

        public AutoStopViewModel(AutoStopSettings settings)
        {
            _settings = settings;
            ResetCommand = new RelayCommand(ExecuteReset);
            _settings.PropertyChanged += Settings_PropertyChanged;
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public bool StopAfterXGames { get => _settings.StopAfterXGames; set { _settings.StopAfterXGames = value; } }
        public bool StopAfterXWins { get => _settings.StopAfterXWins; set { _settings.StopAfterXWins = value; } }
        public bool StopAfterXLosses { get => _settings.StopAfterXLosses; set { _settings.StopAfterXLosses = value; } }
        public int StopGameCount { get => _settings.StopGameCount; set { _settings.StopGameCount = value; } }
        public int StopWinCount { get => _settings.StopWinCount; set { _settings.StopWinCount = value; } }
        public int StopLossCount { get => _settings.StopLossCount; set { _settings.StopLossCount = value; } }
        public int Wins { get => _settings.Wins; set { _settings.Wins = value; } }
        public int Losses { get => _settings.Losses; set { _settings.Losses = value; } }
        public bool ConcedeAfterXMinutes { get => _settings.ConcedeAfterXMinutes; set { _settings.ConcedeAfterXMinutes = value; } }
        public int ConcedeMinutesCount { get => _settings.ConcedeMinutesCount; set { _settings.ConcedeMinutesCount = value; } }
        public bool DynamicFacePenaltyEnabled { get => _settings.DynamicFacePenaltyEnabled; set { _settings.DynamicFacePenaltyEnabled = value; } }
        public int DynamicFacePenaltyMinutes { get => _settings.DynamicFacePenaltyMinutes; set { _settings.DynamicFacePenaltyMinutes = value; } }
        public int FacePenaltyBeforeTime { get => _settings.FacePenaltyBeforeTime; set { _settings.FacePenaltyBeforeTime = value; } }
        public int FacePenaltyAfterTime { get => _settings.FacePenaltyAfterTime; set { _settings.FacePenaltyAfterTime = value; } }

        public ICommand ResetCommand { get; }
        public event System.Action RequestReset;
        private void ExecuteReset() => RequestReset?.Invoke();
    }
}
