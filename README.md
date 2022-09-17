### Option 1: Use EventCallbacks

```html

<BrowserDimensionsInfo OnResize="UpdateDimensions" />
<BrowserPageOffsetInfo OnScroll="UpdatePageOffset" />

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
        <dd>@(Dimensions.InnerDimensions.Height)px</dd>
        <dt>Inner Width</dt>
        <dd>@(Dimensions.InnerDimensions.Width)px</dd>
        <dt>Outer Height</dt>
        <dd>@(Dimensions.OuterDimensions.Height)px</dd>
        <dt>Outer Width</dt>
        <dd>@(Dimensions.OuterDimensions.Width)px</dd>
    </dl>
}

@code {

    private BrowserWindowDimensions? Dimensions { get; set; }
    private BrowserWindowPageOffset? PageOffset { get; set; }

    private void UpdateDimensions(BrowserWindowDimensions dimensions)
    {
        Dimensions = dimensions;
    }

    private void UpdatePageOffset(BrowserWindowPageOffset pageOffset)
    {
        PageOffset = pageOffset;
    }
}
```

### Option 2: Use Component Context
```html
<BrowserDimensionsInfo Context="dimensions">
    <BrowserPageOffsetInfo Context="offset">
        <dl>
            <dt>Page Offset</dt>
            <dd>@offset.X, @offset.Y</dd>
            <dt>Inner Height</dt>
            <dd>@(dimensions.InnerDimensions.Height)px</dd>
            <dt>Inner Width</dt>
            <dd>@(dimensions.InnerDimensions.Width)px</dd>
            <dt>Outer Height</dt>
            <dd>@(dimensions.OuterDimensions.Height)px</dd>
            <dt>Outer Width</dt>
            <dd>@(dimensions.OuterDimensions.Width)px</dd>
        </dl>
    </BrowserPageOffsetView>
</BrowserDimensionsView>
```

### Option 3: Use Cascading Values
###### App.razor
```html
<CascadingBrowserDimensions>
<CascadingBrowserPageOffset>
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
</CascadingBrowserDimensions>
</CascadingBrowserPageOffset>
```

###### BrowserInfo.razor
```html
<dl>
    <dt>Page Offset</dt>
    <dd>@offset.X, @offset.Y</dd>
    <dt>Inner Height</dt>
    <dd>@(dimensions.InnerDimensions.Height)px</dd>
    <dt>Inner Width</dt>
    <dd>@(dimensions.InnerDimensions.Width)px</dd>
    <dt>Outer Height</dt>
    <dd>@(dimensions.OuterDimensions.Height)px</dd>
    <dt>Outer Width</dt>
    <dd>@(dimensions.OuterDimensions.Width)px</dd>
</dl>

@code {
    [CascadingValue] private BrowserWindowDimensions? Dimensions { get; set; }
    [CascadingValue] private BrowserWindowPageOffset? PageOffset { get; set; }
}
```
