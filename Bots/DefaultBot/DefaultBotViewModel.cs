using System.Collections.ObjectModel;
using System.Windows.Input;
using Triton.Common.Mvvm;
using Triton.Game.Mapping;

namespace Triton.Bot.Logic.Bots.DefaultBot
{
    public class DefaultBotViewModel : ViewModelBase
    {
        private readonly DefaultBotSettings _settings;

        public DefaultBotViewModel(DefaultBotSettings settings)
        {
            _settings = settings;
            RecacheCommand = new RelayCommand(ExecuteRecache);
        }

        public ObservableCollection<VisualsFormatType> AllConstructedRules => _settings.AllConstructedRules;

        public VisualsFormatType ConstructedGameRule
        {
            get => _settings.ConstructedGameRule;
            set { _settings.ConstructedGameRule = value; OnPropertyChanged(); }
        }

        public string ConstructedCustomDeck
        {
            get => _settings.ConstructedCustomDeck;
            set { _settings.ConstructedCustomDeck = value; OnPropertyChanged(); }
        }

        public bool AutoGreet
        {
            get => _settings.AutoGreet;
            set { _settings.AutoGreet = value; OnPropertyChanged(); }
        }

        public bool NeedsToCacheCustomDecks
        {
            get => _settings.NeedsToCacheCustomDecks;
            set { _settings.NeedsToCacheCustomDecks = value; OnPropertyChanged(); }
        }

        public bool ReleaseLimit
        {
            get => _settings.ReleaseLimit;
            set { _settings.ReleaseLimit = value; OnPropertyChanged(); }
        }

        public int ReleaseLimitW
        {
            get => _settings.ReleaseLimitW;
            set { _settings.ReleaseLimitW = value; OnPropertyChanged(); }
        }

        public int ReleaseLimitH
        {
            get => _settings.ReleaseLimitH;
            set { _settings.ReleaseLimitH = value; OnPropertyChanged(); }
        }

        public bool AutoConcedeAfterConstructedWin
        {
            get => _settings.AutoConcedeAfterConstructedWin;
            set { _settings.AutoConcedeAfterConstructedWin = value; OnPropertyChanged(); }
        }

        public int AutoConcedeNumberOfWins
        {
            get => _settings.AutoConcedeNumberOfWins;
            set { _settings.AutoConcedeNumberOfWins = value; OnPropertyChanged(); }
        }

        public int AutoConcedeNumberOfLosses
        {
            get => _settings.AutoConcedeNumberOfLosses;
            set { _settings.AutoConcedeNumberOfLosses = value; OnPropertyChanged(); }
        }

        public bool NormalConcede
        {
            get => _settings.NormalConcede;
            set { _settings.NormalConcede = value; OnPropertyChanged(); }
        }

        public bool ForceConcedeAtMulligan
        {
            get => _settings.ForceConcedeAtMulligan;
            set { _settings.ForceConcedeAtMulligan = value; OnPropertyChanged(); }
        }

        public bool JudgmentOpponentNameConcede
        {
            get => _settings.JudgmentOpponentNameConcede;
            set { _settings.JudgmentOpponentNameConcede = value; OnPropertyChanged(); }
        }

        public int AutoConcedeMinDelayMs
        {
            get => _settings.AutoConcedeMinDelayMs;
            set { _settings.AutoConcedeMinDelayMs = value; OnPropertyChanged(); }
        }

        public int AutoConcedeMaxDelayMs
        {
            get => _settings.AutoConcedeMaxDelayMs;
            set { _settings.AutoConcedeMaxDelayMs = value; OnPropertyChanged(); }
        }

        public ICommand RecacheCommand { get; }

        public event System.Action RequestRecache;

        private void ExecuteRecache()
        {
            RequestRecache?.Invoke();
        }
    }
}
