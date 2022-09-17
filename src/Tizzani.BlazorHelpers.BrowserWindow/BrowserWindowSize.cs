namespace Tizzani.BlazorHelpers.BrowserWindow;

public sealed record BrowserWindowSize
(
    BrowserWindowDimensions InnerDimensions,
    BrowserWindowDimensions OuterDimensions
);