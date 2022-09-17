export function getBrowserWindow() {
    return {
        DevicePixelRatio: window.devicePixelRatio,
        InnerDimensions: {
            Height: window.innerHeight,
            Width: window.innerWidth
        },
        OuterDimensions: {
            Height: window.outerHeight,
            Width: window.outerWidth
        },
        PageOffset: {
            X: window.pageXOffset,
            Y: window.pageYOffset
        }
    };
}

export function getDevicePixelRatio() {
    return window.devicePixelRatio;
}

export function getInnerDimensions() {
    return {
        Height: window.innerHeight,
        Width: window.innerWidth
    };
}

export function getOuterDimensions() {
    return {
        Height: window.outerHeight,
        Width: window.outerWidth
    };
}

export function getPageOffset() {
    return {
        X: window.pageXOffset,
        Y: window.pageYOffset
    };
}

export function subscribeToResize() {
    window.addEventListener('resize', onResize);
}

export function subscribeToScroll() {
    window.addEventListener('scroll', onScroll);
}

function onResize() {
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'OnBrowserResize');
}

function onScroll() {
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'OnBrowserScroll');
}