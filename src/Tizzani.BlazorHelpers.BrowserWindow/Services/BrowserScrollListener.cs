using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

public class BrowserScrollListener
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private static Action? OnSubscriptionChanged;
    private static Action? OnSubscriptionCountChanged;

    private static bool _isSubscribed;
    private static bool IsSubscribed
    {
        get => _isSubscribed;
        set
        {
            _isSubscribed = value;
            OnSubscriptionChanged?.Invoke();
        }
    }

    private static event Func<ValueTask>? _onScroll;
    public static event Func<ValueTask>? OnScroll
    {
        add
        {
            _onScroll += value;
            OnSubscriptionCountChanged?.Invoke();

        }
        remove
        {
            _onScroll -= value;
            OnSubscriptionCountChanged?.Invoke();
        }
    }

    public BrowserScrollListener(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Tizzani.BlazorHelpers.BrowserWindow/main.js").AsTask());

        OnSubscriptionChanged += VerifySubscriptionState;
        OnSubscriptionCountChanged += VerifySubscriptionState;
    }

    public async ValueTask DisposeAsync()
    {
        OnSubscriptionChanged -= VerifySubscriptionState;
        OnSubscriptionCountChanged -= VerifySubscriptionState;

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

    private async void VerifySubscriptionState()
    {
        var count = _onScroll?.GetInvocationList().Length ?? 0;
        var shouldSubscribe = count > 0;

        if (shouldSubscribe == IsSubscribed)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync(shouldSubscribe ? "addScrollEventListener" : "removeScrollEventListener");
    }
}
