### Option 1: Use EventCallbacks

```html
<BrowserWindowInfo OnResize="UpdateDimensions" OnScroll="UpdatePageOffset" />

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

### Option 2: Use Cascading-ish Values
```html
<BrowserDimensionsView Context="dimensions">
    <BrowserPageOffsetView Context="offset">
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
