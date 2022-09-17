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

/* Resize Event Management */
export function addResizeEventListener() {
    window.addEventListener('resize', onResize);
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyResizeListenerAdded');
}

export function removeResizeEventListener() {
    window.removeEventListener('resize', onResize);
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyResizeListenerRemoved');
}

function onResize() {
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyResize');
}

/* Scroll Event Management */
export function addScrollEventListener() {
    window.addEventListener('scroll', onScroll);
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyScrollListenerAdded');
}

export function removeScrollEventListener() {
    window.removeEventListener('scroll', onScroll);
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyScrollListenerRemoved');
}

function onScroll() {
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyScroll');
}