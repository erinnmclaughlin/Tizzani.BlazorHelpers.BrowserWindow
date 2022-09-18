using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

public class ScrollListener
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private static Action? OnSubscriptionAdded;
    private static Action? OnSubscriptionRemoved;

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

    public ScrollListener(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Tizzani.BlazorHelpers.BrowserWindow/main.js").AsTask());

        OnSubscriptionAdded += HandleSubscriptionAdded;
        OnSubscriptionRemoved += HandleSubscriptionRemoved;
    }

    public async ValueTask DisposeAsync()
    {
        OnSubscriptionAdded -= HandleSubscriptionAdded;
        OnSubscriptionRemoved -= HandleSubscriptionRemoved;

        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    [JSInvokable]
    public static async Task NotifyScroll()
    {
        if (_onScroll != null)
            await _onScroll.Invoke();
    }

    private async void HandleSubscriptionAdded()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("addScrollEventListener");
    }

    private async void HandleSubscriptionRemoved()
    {
        var count = _onScroll?.GetInvocationList().Length ?? 0;

        if (count > 0)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("removeScrollEventListener");
    }
}
