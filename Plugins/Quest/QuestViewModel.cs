using System.Windows.Input;
using Triton.Common.Mvvm;

namespace Quest
{
    public class QuestViewModel : ViewModelBase
    {
        private readonly QuestSettings _settings;

        public QuestViewModel(QuestSettings settings)
        {
            _settings = settings;
            _settings.PropertyChanged += Settings_PropertyChanged;
            RefreshDay1Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(1, "day"), () => CanRefreshDay1);
            RefreshDay2Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(2, "day"), () => CanRefreshDay2);
            RefreshDay3Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(3, "day"), () => CanRefreshDay3);
            RefreshWeek1Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(1, "week"), () => CanRefreshWeek1);
            RefreshWeek2Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(2, "week"), () => CanRefreshWeek2);
            RefreshWeek3Command = new RelayCommand(() => RequestRefreshQuest?.Invoke(3, "week"), () => CanRefreshWeek3);
            ResetCommand = new RelayCommand(() => RequestReset?.Invoke());
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public bool CanRefreshDay1 { get => _canRefreshDay1; set => SetProperty(ref _canRefreshDay1, value); }
        public bool CanRefreshDay2 { get => _canRefreshDay2; set => SetProperty(ref _canRefreshDay2, value); }
        public bool CanRefreshDay3 { get => _canRefreshDay3; set => SetProperty(ref _canRefreshDay3, value); }
        public bool CanRefreshWeek1 { get => _canRefreshWeek1; set => SetProperty(ref _canRefreshWeek1, value); }
        public bool CanRefreshWeek2 { get => _canRefreshWeek2; set => SetProperty(ref _canRefreshWeek2, value); }
        public bool CanRefreshWeek3 { get => _canRefreshWeek3; set => SetProperty(ref _canRefreshWeek3, value); }

        private bool _canRefreshDay1;
        private bool _canRefreshDay2;
        private bool _canRefreshDay3;
        private bool _canRefreshWeek1;
        private bool _canRefreshWeek2;
        private bool _canRefreshWeek3;

        public int ProgressDay1 { get => _settings.ProgressDay1; set { _settings.ProgressDay1 = value; } }
        public int ProgressDay2 { get => _settings.ProgressDay2; set { _settings.ProgressDay2 = value; } }
        public int ProgressDay3 { get => _settings.ProgressDay3; set { _settings.ProgressDay3 = value; } }
        public int ProgressWeek1 { get => _settings.ProgressWeek1; set { _settings.ProgressWeek1 = value; } }
        public int ProgressWeek2 { get => _settings.ProgressWeek2; set { _settings.ProgressWeek2 = value; } }
        public int ProgressWeek3 { get => _settings.ProgressWeek3; set { _settings.ProgressWeek3 = value; } }

        public int QuotaDay1 { get => _settings.QuotaDay1; set { _settings.QuotaDay1 = value; } }
        public int QuotaDay2 { get => _settings.QuotaDay2; set { _settings.QuotaDay2 = value; } }
        public int QuotaDay3 { get => _settings.QuotaDay3; set { _settings.QuotaDay3 = value; } }
        public int QuotaWeek1 { get => _settings.QuotaWeek1; set { _settings.QuotaWeek1 = value; } }
        public int QuotaWeek2 { get => _settings.QuotaWeek2; set { _settings.QuotaWeek2 = value; } }
        public int QuotaWeek3 { get => _settings.QuotaWeek3; set { _settings.QuotaWeek3 = value; } }

        public int XpDay1 { get => _settings.XpDay1; set { _settings.XpDay1 = value; } }
        public int XpDay2 { get => _settings.XpDay2; set { _settings.XpDay2 = value; } }
        public int XpDay3 { get => _settings.XpDay3; set { _settings.XpDay3 = value; } }
        public int XpWeek1 { get => _settings.XpWeek1; set { _settings.XpWeek1 = value; } }
        public int XpWeek2 { get => _settings.XpWeek2; set { _settings.XpWeek2 = value; } }
        public int XpWeek3 { get => _settings.XpWeek3; set { _settings.XpWeek3 = value; } }

        public string DesDay1 { get => _settings.DesDay1; set { _settings.DesDay1 = value; } }
        public string DesDay2 { get => _settings.DesDay2; set { _settings.DesDay2 = value; } }
        public string DesDay3 { get => _settings.DesDay3; set { _settings.DesDay3 = value; } }
        public string DesWeek1 { get => _settings.DesWeek1; set { _settings.DesWeek1 = value; } }
        public string DesWeek2 { get => _settings.DesWeek2; set { _settings.DesWeek2 = value; } }
        public string DesWeek3 { get => _settings.DesWeek3; set { _settings.DesWeek3 = value; } }

        public bool NeedAuto { get => _settings.NeedAuto; set { _settings.NeedAuto = value; } }
        public string RunTime { get => _settings.RunTime; set { _settings.RunTime = value; } }
        public int Interval { get => _settings.Interval; set { _settings.Interval = value; } }

        public ICommand RefreshDay1Command { get; }
        public ICommand RefreshDay2Command { get; }
        public ICommand RefreshDay3Command { get; }
        public ICommand RefreshWeek1Command { get; }
        public ICommand RefreshWeek2Command { get; }
        public ICommand RefreshWeek3Command { get; }
        public ICommand ResetCommand { get; }

        public event System.Action<int, string> RequestRefreshQuest;
        public event System.Action RequestReset;
    }
}
