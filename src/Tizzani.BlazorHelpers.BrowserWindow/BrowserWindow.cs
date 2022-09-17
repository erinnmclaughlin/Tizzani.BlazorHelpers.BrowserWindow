namespace Tizzani.BlazorHelpers.BrowserWindow;

public sealed record BrowserWindow
(
    float DevicePixelRatio,
    BrowserWindowSize Size,
    BrowserWindowPageOffset PageOffset
);
