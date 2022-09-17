using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

public class BrowserScrollListener
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private static Action? OnSubscriptionAdded { get; set; }
    private static Action? OnSubscriptionRemoved { get; set; }
    private static bool IsSubscribed { get; set; }
    private static int SubscriptionCount => _onScroll?.GetInvocationList().Length ?? 0;
    private static bool ShouldSubscribe => SubscriptionCount > 0;

    private static event Func<ValueTask>? _onScroll;
    public static event Func<ValueTask>? OnScroll
    {
        add
        {
            _onScroll += value;
            OnSubscriptionAdded?.Invoke();

        }
        remove
        {
            _onScroll -= value;
            OnSubscriptionRemoved?.Invoke();
        }
    }

    public BrowserScrollListener(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Tizzani.BlazorHelpers.BrowserWindow/main.js").AsTask());

        OnSubscriptionAdded += HandleSubscriptionCountChanged;
        OnSubscriptionRemoved += HandleSubscriptionCountChanged;
    }

    public async ValueTask DisposeAsync()
    {
        OnSubscriptionAdded -= HandleSubscriptionCountChanged;
        OnSubscriptionRemoved -= HandleSubscriptionCountChanged;

        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }

    [JSInvokable]
    public static async Task NotifyScroll()
    {
        if (_onScroll != null)
            await _onScroll.Invoke();
    }

    [JSInvokable]
    public static void NotifyScrollListenerAdded()
    {
        IsSubscribed = true;
    }

    [JSInvokable]
    public static void NotifyScrollListenerRemoved()
    {
        IsSubscribed = false;
    }

    private async void HandleSubscriptionCountChanged()
    {
        if (ShouldSubscribe == IsSubscribed)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync(ShouldSubscribe ? "addScrollEventListener" : "removeScrollEventListener");
    }
}
