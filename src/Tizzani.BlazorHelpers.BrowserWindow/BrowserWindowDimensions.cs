namespace Tizzani.BlazorHelpers.BrowserWindow;
public sealed record BrowserWindowDimensions
(
    BrowserWindowInnerDimensions InnerDimensions,
    BrowserWindowOuterDimensions OuterDimensions
);