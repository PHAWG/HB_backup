using System.Windows.Input;
using Triton.Common.Mvvm;

namespace Monitor
{
    public class MonitorViewModel : ViewModelBase
    {
        private readonly MonitorSettings _settings;

        public MonitorViewModel(MonitorSettings settings)
        {
            _settings = settings;
            ResetCommand = new RelayCommand(ExecuteReset);
        }

        public int Level
        {
            get => _settings.Level;
            set { _settings.Level = value; OnPropertyChanged(); }
        }

        public int Xp
        {
            get => _settings.Xp;
            set { _settings.Xp = value; OnPropertyChanged(); }
        }

        public int XpNeeded
        {
            get => _settings.XpNeeded;
            set { _settings.XpNeeded = value; OnPropertyChanged(); }
        }

        public int AllXp
        {
            get => _settings.AllXp;
            set { _settings.AllXp = value; OnPropertyChanged(); }
        }

        public int AllXpNeeded
        {
            get => _settings.AllXpNeeded;
            set { _settings.AllXpNeeded = value; OnPropertyChanged(); }
        }

        public string AllRunTimeText
        {
            get => _settings.AllRunTimeText;
            set { _settings.AllRunTimeText = value; OnPropertyChanged(); }
        }

        public long AllGetXp
        {
            get => _settings.AllGetXp;
            set { _settings.AllGetXp = value; OnPropertyChanged(); }
        }

        public string PerHourXpStr
        {
            get => _settings.PerHourXpStr;
            set { _settings.PerHourXpStr = value; OnPropertyChanged(); }
        }

        public int FullXpNeeded
        {
            get => _settings.FullXpNeeded;
            set { _settings.FullXpNeeded = value; OnPropertyChanged(); }
        }

        public string FullTimeNeeded
        {
            get => _settings.FullTimeNeeded;
            set { _settings.FullTimeNeeded = value; OnPropertyChanged(); }
        }

        public string Collection
        {
            get => _settings.Collection;
            set { _settings.Collection = value; OnPropertyChanged(); }
        }

        public string PassportEnd
        {
            get => _settings.PassportEnd;
            set { _settings.PassportEnd = value; OnPropertyChanged(); }
        }

        public string TwistInfo
        {
            get => _settings.TwistInfo;
            set { _settings.TwistInfo = value; OnPropertyChanged(); }
        }

        public string StandardInfo
        {
            get => _settings.StandardInfo;
            set { _settings.StandardInfo = value; OnPropertyChanged(); }
        }

        public string WildInfo
        {
            get => _settings.WildInfo;
            set { _settings.WildInfo = value; OnPropertyChanged(); }
        }

        public ICommand ResetCommand { get; }

        public event System.Action RequestReset;

        private void ExecuteReset()
        {
            RequestReset?.Invoke();
        }
    }
}
