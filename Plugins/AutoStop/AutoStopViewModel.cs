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
        }

        public bool StopAfterXGames
        {
            get => _settings.StopAfterXGames;
            set { _settings.StopAfterXGames = value; OnPropertyChanged(); }
        }

        public bool StopAfterXWins
        {
            get => _settings.StopAfterXWins;
            set { _settings.StopAfterXWins = value; OnPropertyChanged(); }
        }

        public bool StopAfterXLosses
        {
            get => _settings.StopAfterXLosses;
            set { _settings.StopAfterXLosses = value; OnPropertyChanged(); }
        }

        public int StopGameCount
        {
            get => _settings.StopGameCount;
            set { _settings.StopGameCount = value; OnPropertyChanged(); }
        }

        public int StopWinCount
        {
            get => _settings.StopWinCount;
            set { _settings.StopWinCount = value; OnPropertyChanged(); }
        }

        public int StopLossCount
        {
            get => _settings.StopLossCount;
            set { _settings.StopLossCount = value; OnPropertyChanged(); }
        }

        public int Wins
        {
            get => _settings.Wins;
            set { _settings.Wins = value; OnPropertyChanged(); }
        }

        public int Losses
        {
            get => _settings.Losses;
            set { _settings.Losses = value; OnPropertyChanged(); }
        }

        public bool ConcedeAfterXMinutes
        {
            get => _settings.ConcedeAfterXMinutes;
            set { _settings.ConcedeAfterXMinutes = value; OnPropertyChanged(); }
        }

        public int ConcedeMinutesCount
        {
            get => _settings.ConcedeMinutesCount;
            set { _settings.ConcedeMinutesCount = value; OnPropertyChanged(); }
        }

        public bool DynamicFacePenaltyEnabled
        {
            get => _settings.DynamicFacePenaltyEnabled;
            set { _settings.DynamicFacePenaltyEnabled = value; OnPropertyChanged(); }
        }

        public int DynamicFacePenaltyMinutes
        {
            get => _settings.DynamicFacePenaltyMinutes;
            set { _settings.DynamicFacePenaltyMinutes = value; OnPropertyChanged(); }
        }

        public int FacePenaltyBeforeTime
        {
            get => _settings.FacePenaltyBeforeTime;
            set { _settings.FacePenaltyBeforeTime = value; OnPropertyChanged(); }
        }

        public int FacePenaltyAfterTime
        {
            get => _settings.FacePenaltyAfterTime;
            set { _settings.FacePenaltyAfterTime = value; OnPropertyChanged(); }
        }

        public ICommand ResetCommand { get; }

        public event System.Action RequestReset;

        private void ExecuteReset()
        {
            RequestReset?.Invoke();
        }
    }
}
