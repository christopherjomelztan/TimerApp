using System.ComponentModel;

public class TimerModel : INotifyPropertyChanged
{
    private int _minutes;
    private int _seconds;
    private bool _isRunning;

    public int Minutes
    {
        get => _minutes;
        set
        {
            _minutes = value;
            OnPropertyChanged(nameof(Minutes));
        }
    }

    public int Seconds
    {
        get => _seconds;
        set
        {
            _seconds = value;
            OnPropertyChanged(nameof(Seconds));
        }
    }

    public bool IsRunning
    {
        get => _isRunning;
        set
        {
            _isRunning = value;
            OnPropertyChanged(nameof(IsRunning));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
