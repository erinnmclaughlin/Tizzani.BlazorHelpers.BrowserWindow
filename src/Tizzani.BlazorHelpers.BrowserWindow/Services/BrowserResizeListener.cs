using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

public class BrowserResizeListener : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private static Action? OnSubscriptionAdded { get; set; }
    private static Action? OnSubscriptionRemoved { get; set; }
    private static bool IsSubscribed { get; set; }
    private static int SubscriptionCount => _onResize?.GetInvocationList().Length ?? 0;
    private static bool ShouldSubscribe => SubscriptionCount > 0;

    private static event Func<ValueTask>? _onResize;
    public static event Func<ValueTask>? OnResize
    {
        add
        {
            _onResize += value;
            OnSubscriptionAdded?.Invoke();

        }
        remove
        {
            _onResize -= value;
            OnSubscriptionRemoved?.Invoke();
        }
    }

    public BrowserResizeListener(IJSRuntime jsRuntime)
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

    private async void HandleSubscriptionCountChanged()
    {
        if (ShouldSubscribe == IsSubscribed)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync(ShouldSubscribe ? "addResizeEventListener" : "removeResizeEventListener");
    }
}
