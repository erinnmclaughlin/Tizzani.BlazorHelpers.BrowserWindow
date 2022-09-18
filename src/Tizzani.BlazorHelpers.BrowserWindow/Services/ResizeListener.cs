using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

public class ResizeListener : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private static Action? OnSubscriptionAdded;
    private static Action? OnSubscriptionRemoved;

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

    public ResizeListener(IJSRuntime jsRuntime)
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
    public static async Task NotifyResize()
    {
        if (_onResize != null)
            await _onResize.Invoke();
    }

    private async void HandleSubscriptionAdded()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("addResizeEventListener");
    }

    private async void HandleSubscriptionRemoved()
    {
        var count = _onResize?.GetInvocationList().Length ?? 0;

        if (count > 0)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("removeResizeEventListener");
    }
}
