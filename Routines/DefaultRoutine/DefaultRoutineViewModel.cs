using System.Collections.ObjectModel;
using System.Windows.Input;
using Triton.Common.Mvvm;

namespace HREngine.Bots
{
    public class DefaultRoutineViewModel : ViewModelBase
    {
        private readonly DefaultRoutineSettings _settings;

        public DefaultRoutineViewModel(DefaultRoutineSettings settings)
        {
            _settings = settings;
            OpenLastMatchCommand = new RelayCommand(ExecuteOpenLastMatch);
            ClearLogCommand = new RelayCommand(ExecuteClearLog);
            ResetCommand = new RelayCommand(ExecuteReset);
        }

        public ObservableCollection<string> AllBehaviors => _settings.AllBehav;

        public string DefaultBehavior
        {
            get => _settings.DefaultBehavior;
            set { _settings.DefaultBehavior = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> AllEmotes => _settings.AllEmote;

        public string DefaultEmote
        {
            get => _settings.DefaultEmote;
            set { _settings.DefaultEmote = value; OnPropertyChanged(); }
        }

        public int MaxWide
        {
            get => _settings.MaxWide;
            set { _settings.MaxWide = value; OnPropertyChanged(); }
        }

        public int MaxDeep
        {
            get => _settings.MaxDeep;
            set { _settings.MaxDeep = value; OnPropertyChanged(); }
        }

        public int MaxCal
        {
            get => _settings.MaxCal;
            set { _settings.MaxCal = value; OnPropertyChanged(); }
        }

        public int EnfaceReward
        {
            get => _settings.EnfaceReward;
            set { _settings.EnfaceReward = value; OnPropertyChanged(); }
        }

        public bool SetLog
        {
            get => _settings.SetLog;
            set { _settings.SetLog = value; OnPropertyChanged(); }
        }

        public bool UseSecretsPlayAround
        {
            get => _settings.UseSecretsPlayAround;
            set { _settings.UseSecretsPlayAround = value; OnPropertyChanged(); }
        }

        public bool UsePrintPenalties
        {
            get => _settings.UsePrintPenalties;
            set { _settings.UsePrintPenalties = value; OnPropertyChanged(); }
        }

        public bool UsePrintNextMove
        {
            get => _settings.UsePrintNextMove;
            set { _settings.UsePrintNextMove = value; OnPropertyChanged(); }
        }

        public bool BerserkIfCanFinishNextTour
        {
            get => _settings.BerserkIfCanFinishNextTour;
            set { _settings.BerserkIfCanFinishNextTour = value; OnPropertyChanged(); }
        }

        public int Enfacehp
        {
            get => _settings.Enfacehp;
            set { _settings.Enfacehp = value; OnPropertyChanged(); }
        }

        public ICommand OpenLastMatchCommand { get; }
        public ICommand ClearLogCommand { get; }
        public ICommand ResetCommand { get; }

        public event System.Action RequestOpenLastMatch;
        public event System.Action RequestClearLog;
        public event System.Action RequestReset;

        private void ExecuteOpenLastMatch()
        {
            RequestOpenLastMatch?.Invoke();
        }

        private void ExecuteClearLog()
        {
            RequestClearLog?.Invoke();
        }

        private void ExecuteReset()
        {
            RequestReset?.Invoke();
        }
    }
}
