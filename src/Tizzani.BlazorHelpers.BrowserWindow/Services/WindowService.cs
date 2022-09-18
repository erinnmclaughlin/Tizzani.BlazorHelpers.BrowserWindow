using Microsoft.JSInterop;
using Tizzani.BlazorHelpers.BrowserWindow.Models;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

internal class WindowService : IWindowService
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public WindowService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Tizzani.BlazorHelpers.BrowserWindow/main.js").AsTask());
    }

    public async ValueTask<WindowDimensions> GetDimensions()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<WindowDimensions>("getDimensions");
    }

    public async ValueTask<PageOffset> GetPageOffset()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<PageOffset>("getPageOffset");
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
