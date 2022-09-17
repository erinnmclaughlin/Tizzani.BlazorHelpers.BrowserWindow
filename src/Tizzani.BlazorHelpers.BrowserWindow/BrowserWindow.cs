namespace Tizzani.BlazorHelpers.BrowserWindow;

public sealed record BrowserWindow
(
    float DevicePixelRatio,
    BrowserWindowDimensions InnerDimensions,
    BrowserWindowDimensions OuterDimensions,
    PageOffset PageOffset
);
