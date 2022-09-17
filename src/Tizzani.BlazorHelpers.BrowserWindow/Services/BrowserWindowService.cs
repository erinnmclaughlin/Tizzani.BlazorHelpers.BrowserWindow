using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

internal class BrowserWindowService : IAsyncDisposable, IBrowserWindowService
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public BrowserWindowService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Tizzani.BlazorHelpers.BrowserWindow/main.js").AsTask());
    }

    public async ValueTask<BrowserWindow> GetBrowserWindow()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<BrowserWindow>("getBrowserWindow");
    }

    public async ValueTask<float> GetDevicePixelRatio()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<float>("getDevicePixelRatio");
    }

    public async ValueTask<BrowserWindowInnerDimensions> GetInnerDimensions()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<BrowserWindowInnerDimensions>("getInnerDimensions");
    }

    public async ValueTask<BrowserWindowOuterDimensions> GetOuterDimensions()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<BrowserWindowOuterDimensions>("getOuterDimensions");
    }

    public async ValueTask<BrowserWindowPageOffset> GetPageOffset()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<BrowserWindowPageOffset>("getPageOffset");
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
