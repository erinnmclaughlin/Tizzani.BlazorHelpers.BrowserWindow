using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

internal sealed class BrowserWindowRegistrationService : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private bool IsListeningForResize { get; set; }
    private bool IsListeningForScroll { get; set; }

    public BrowserWindowRegistrationService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Tizzani.BlazorHelpers.BrowserWindow/main.js").AsTask());
    }

    public async ValueTask AddResizeEventListener()
    {
        if (IsListeningForResize)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("addResizeEventListener");

        IsListeningForResize = true;
    }

    public async ValueTask AddScrollEventListener()
    {
        if (IsListeningForScroll)
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("addScrollEventListener");

        IsListeningForScroll = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }
}
