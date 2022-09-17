namespace Tizzani.BlazorHelpers.BrowserWindow;

public sealed record BrowserWindow
(
    float DevicePixelRatio,
    BrowserWindowDimensions Dimensions,
    BrowserWindowPageOffset PageOffset
)
{
    public float InnerHeight => Dimensions.InnerDimensions.Height;
    public float InnerWidth => Dimensions.InnerDimensions.Width;
    public float OuterHeight => Dimensions.OuterDimensions.Height;
    public float OuterWidth => Dimensions.OuterDimensions.Width;
}
