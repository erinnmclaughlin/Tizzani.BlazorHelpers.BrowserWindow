namespace Tizzani.BlazorHelpers.BrowserWindow;

public interface IBrowserWindowService
{
    ValueTask<BrowserWindow> GetBrowserWindow();
    ValueTask<float> GetDevicePixelRatio();
    ValueTask<BrowserWindowDimensions> GetDimensions();
    ValueTask<BrowserWindowInnerDimensions> GetInnerDimensions();
    ValueTask<BrowserWindowOuterDimensions> GetOuterDimensions();
    ValueTask<BrowserWindowPageOffset> GetPageOffset();
}