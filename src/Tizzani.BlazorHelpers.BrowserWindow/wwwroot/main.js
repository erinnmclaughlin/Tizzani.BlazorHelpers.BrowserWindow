export function getBrowserWindow() {
    return {
        DevicePixelRatio: getDevicePixelRatio(),
        Dimensions: getDimensions(),
        PageOffset: getPageOffset()
    };
}

export function getDevicePixelRatio() {
    return window.devicePixelRatio;
}

export function getDimensions() {
    return {
        InnerDimensions: getInnerDimensions(),
        OuterDimensions: getOuterDimensions()
    };
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

export function addResizeEventListener() {
    window.addEventListener('resize', onResize);
}

export function addScrollEventListener() {
    window.addEventListener('scroll', onScroll);
}

function onResize() {
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyResize');
}

function onScroll() {
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyScroll');
}