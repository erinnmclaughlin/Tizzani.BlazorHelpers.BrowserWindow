using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

public class ResizeListener : IAsyncDisposable
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

    private static event Func<ValueTask>? _onResize;
    public static event Func<ValueTask>? OnResize
    {
        add
        {
            _onResize += value;
            OnSubscriptionCountChanged?.Invoke();

        }
        remove
        {
            _onResize -= value;
            OnSubscriptionCountChanged?.Invoke();
        }
    }

    public ResizeListener(IJSRuntime jsRuntime)
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
    public static async Task NotifyResize()
    {
        if (_onResize != null)
            await _onResize.Invoke();
    }

    [JSInvokable]
    public static void NotifyResizeListenerAdded()
    {
        IsSubscribed = true;
    }

    [JSInvokable]
    public static void NotifyResizeListenerRemoved()
    {
        IsSubscribed = false;
    }

    private async void VerifySubscriptionState()
    {
        var count = _onResize?.GetInvocationList().Length ?? 0;
        var shouldSubscribe = count > 0;

        if (shouldSubscribe == IsSubscribed)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync(shouldSubscribe ? "addResizeEventListener" : "removeResizeEventListener");
    }
}
