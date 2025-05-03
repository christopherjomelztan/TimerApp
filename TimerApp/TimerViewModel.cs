using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;

public class TimerViewModel : INotifyPropertyChanged
{
    private TimerModel _timerModel;
    private DispatcherTimer _dispatcherTimer;

    public TimerViewModel()
    {
        _timerModel = new TimerModel();
        _dispatcherTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _dispatcherTimer.Tick += OnTick;
    }

    public int Minutes
    {
        get => _timerModel.Minutes;
        set
        {
            _timerModel.Minutes = value;
            OnPropertyChanged(nameof(Minutes));
            OnPropertyChanged(nameof(TimeDisplay));
        }
    }

    public int Seconds
    {
        get => _timerModel.Seconds;
        set
        {
            _timerModel.Seconds = value;
            OnPropertyChanged(nameof(Seconds));
            OnPropertyChanged(nameof(TimeDisplay));
        }
    }

    public bool IsRunning
    {
        get => _timerModel.IsRunning;
        set
        {
            _timerModel.IsRunning = value;
            OnPropertyChanged(nameof(IsRunning));
        }
    }

    public string TimeDisplay => $"{Minutes:D2}:{Seconds:D2}";
    public ICommand StartCommand => new RelayCommand(StartTimer, () => !IsRunning);
    public ICommand ClearOrStopCommand => new RelayCommand(ClearOrStopTimer);

    private void StartTimer()
    {
        if (Minutes > 0 || Seconds > 0)
        {
            IsRunning = true;
            _dispatcherTimer.Start();
            OnPropertyChanged(nameof(ButtonContent));
        }
    }

    private void ClearOrStopTimer()
    {
        if (IsRunning)
        {
            StopTimer();
        }
        else
        {
            ClearTimer();
        }
    }
    private void StopTimer()
    {
        IsRunning = false;
        _dispatcherTimer.Stop();
        ClearTimer();
        OnPropertyChanged(nameof(ButtonContent));
    }

    private void ClearTimer()
    {
        if (!IsRunning)
        {
            Minutes = 0;
            Seconds = 0;
            OnPropertyChanged(nameof(TimeDisplay));
        }
    }
    public string ButtonContent => IsRunning ? "Stop" : "Clear";

    private void AddMinutes(int minutes)
    {
        Minutes += minutes;
        OnPropertyChanged(nameof(TimeDisplay));
    }
    public ICommand AddTenMinutesCommand => new RelayCommand(() => AddMinutes(10));
    public ICommand AddFifteenMinutesCommand => new RelayCommand(() => AddMinutes(15));
    public ICommand AddThirtyMinutesCommand => new RelayCommand(() => AddMinutes(30));


    private void OnTick(object sender, EventArgs e)
    {
        if (Seconds == 0)
        {
            if (Minutes == 0)
            {
                StopTimer();
                // Send alarm and notification
                MessageBox.Show("Time's up!");
            }
            else
            {
                Minutes--;
                Seconds = 59;
            }
        }
        else
        {
            Seconds--;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
