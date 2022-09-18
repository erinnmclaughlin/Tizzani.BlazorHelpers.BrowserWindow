using System.Timers;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

internal sealed class DebounceTimer : IDisposable
{
    private System.Timers.Timer Timer { get; set; } = new();

    public double Debounce
    {
        get => Timer.Interval;
        set
        {
            Timer.Interval = Math.Max(10, value); ;
        }
    }

    public event ElapsedEventHandler Elapsed
    {
        add => Timer.Elapsed += value;
        remove => Timer.Elapsed -= value;
    }

    public DebounceTimer(double debounce = 300)
    {
        Debounce = debounce;
        Timer.AutoReset = false;
    }

    public void Dispose() => Timer.Dispose();
    public void Stop() => Timer.Stop();
    public void Start() => Timer.Start();

    public ValueTask Reset()
    {
        Stop();
        Start();

        return ValueTask.CompletedTask;
    }

}
