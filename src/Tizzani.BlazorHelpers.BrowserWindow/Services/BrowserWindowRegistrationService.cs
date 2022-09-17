using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

internal sealed class BrowserWindowRegistrationService : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    private bool IsListeningForResize { get; set; }
    private bool IsListeningForScroll { get; set; }

    public BrowserWindowSettings Settings { get; }

    public BrowserWindowRegistrationService(IJSRuntime jsRuntime, BrowserWindowSettings settings)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Tizzani.BlazorHelpers.BrowserWindow/main.js").AsTask());

        Settings = settings;
    }

    public async ValueTask AddEventListeners()
    {
        if (Settings.ListenForResizeEvent)
            await AddResizeEventListener();

        if (Settings.ListenForScrollEvent)
            await AddScrollEventListener();
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
