namespace Tizzani.BlazorHelpers.BrowserWindow;

public class BrowserWindowSettings
{
    public bool ListenForResizeEvent { get; set; }
    public bool ListenForScrollEvent { get; set; }

    public static readonly BrowserWindowSettings Default = new()
    {
        ListenForResizeEvent = true,
        ListenForScrollEvent = true
    };
}
