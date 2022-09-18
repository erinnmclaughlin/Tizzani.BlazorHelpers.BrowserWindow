# Tizzani.BlazorHelpers.BrowserWindow

Some helper methods for getting resize and scroll events from the browser.

[![Nuget](https://img.shields.io/nuget/v/Tizzani.BlazorHelpers.BrowserWindow)](https://www.nuget.org/packages/Tizzani.BlazorHelpers.BrowserWindow/1.0.0)

## Setup

### Register Services
```csharp
builder.Services.AddBrowserWindowService();
```

### Add Usings

##### \_Imports.razor
```csharp
@using Tizzani.BlazorHelpers.BrowserWindow
```

## Example Usage

### Option 1: Use EventCallbacks

```html
@using Tizzani.BlazorHelpers.BrowserWindow.Models

<WindowDimensionsInfo OnResize="UpdateDimensions" />
<PageOffsetInfo OnScroll="UpdatePageOffset" />

@if (Dimensions == null || PageOffset == null)
{
    <p>Loading browser window info...</p>
}
else
{
    <dl>
        <dt>Page Offset</dt>
        <dd>@PageOffset.X, @PageOffset.Y</dd>
        <dt>Inner Height</dt>
        <dd>@(Dimensions.InnerHeight)px</dd>
        <dt>Inner Width</dt>
        <dd>@(Dimensions.InnerWidth)px</dd>
        <dt>Outer Height</dt>
        <dd>@(Dimensions.OuterHeight)px</dd>
        <dt>Outer Width</dt>
        <dd>@(Dimensions.OuterWidth)px</dd>
    </dl>
}

@code {

    private WindowDimensions? Dimensions { get; set; }
    private PageOffset? PageOffset { get; set; }

    private void UpdateDimensions(WindowDimensions dimensions)
    {
        Dimensions = dimensions;
    }

    private void UpdatePageOffset(PageOffset pageOffset)
    {
        PageOffset = pageOffset;
    }
}
```

### Option 2: Use Component Context
```html
<WindowDimensionsInfo Context="dimensions">
    <PageOffsetInfo Context="offset">
        <dl>
            <dt>Page Offset</dt>
            <dd>@offset.X, @offset.Y</dd>
            <dt>Inner Height</dt>
            <dd>@(dimensions.InnerHeight)px</dd>
            <dt>Inner Width</dt>
            <dd>@(dimensions.InnerWidth)px</dd>
            <dt>Outer Height</dt>
            <dd>@(dimensions.OuterHeight)px</dd>
            <dt>Outer Width</dt>
            <dd>@(dimensions.OuterWidth)px</dd>
        </dl>
    </PageOffsetInfo>
</WindowDimensionsInfo>

```

### Option 3: Use Cascading Values
##### App.razor
```html
<CascadingWindowDimensions>
    <CascadingPageOffset>
        <Router AppAssembly="@typeof(App).Assembly">
            <Found Context="routeData">
                <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
                <FocusOnNavigate RouteData="@routeData" Selector="h1" />
            </Found>
            <NotFound>
                <PageTitle>Not found</PageTitle>
                <LayoutView Layout="@typeof(MainLayout)">
                    <p role="alert">Sorry, there's nothing at this address.</p>
                </LayoutView>
            </NotFound>
        </Router>
    </CascadingPageOffset>
</CascadingWindowDimensions>
```

##### BrowserInfo.razor
```html
@using Tizzani.BlazorHelpers.BrowserWindow.Models

@if (Dimensions == null || PageOffset == null)
{
    <p>Loading browser window info...</p>
}
else
{
    <dl>
        <dt>Page Offset</dt>
        <dd>@PageOffset.X, @PageOffset.Y</dd>
        <dt>Inner Height</dt>
        <dd>@(Dimensions.InnerHeight)px</dd>
        <dt>Inner Width</dt>
        <dd>@(Dimensions.InnerWidth)px</dd>
        <dt>Outer Height</dt>
        <dd>@(Dimensions.OuterHeight)px</dd>
        <dt>Outer Width</dt>
        <dd>@(Dimensions.OuterWidth)px</dd>
    </dl>
}

@code {
    [CascadingValue] private WindowDimensions? Dimensions { get; set; }
    [CascadingValue] private PageOffset? PageOffset { get; set; }
}
```
