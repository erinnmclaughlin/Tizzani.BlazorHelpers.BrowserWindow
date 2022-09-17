using Microsoft.JSInterop;

namespace Tizzani.BlazorHelpers.BrowserWindow;

public sealed record BrowserWindow
(
    float DevicePixelRatio,
    BrowserWindowDimensions Dimensions,
    BrowserWindowPageOffset PageOffset
)
{
    public static event Func<ValueTask>? OnResize;
    public static event Func<ValueTask>? OnScroll;

    public float InnerHeight => Dimensions.InnerDimensions.Height;
    public float InnerWidth => Dimensions.InnerDimensions.Width;
    public float OuterHeight => Dimensions.OuterDimensions.Height;
    public float OuterWidth => Dimensions.OuterDimensions.Width;

    [JSInvokable]
    public static async Task NotifyResize()
    {
        if (OnResize != null)
            await OnResize.Invoke();
    }

    [JSInvokable]
    public static async Task NotifyScroll()
    {
        if (OnScroll != null)
            await OnScroll.Invoke();
    }
}
