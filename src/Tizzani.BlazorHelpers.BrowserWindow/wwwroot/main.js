/* Dimensions & Resize Events */
export function getDimensions() {
    return {
        InnerHeight: window.innerHeight,
        InnerWidth: window.innerWidth,
        OuterHeight: window.outerHeight,
        OuterWidth: window.outerWidth
    };
}

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

/* Page Offset & Scroll Events */
export function getPageOffset() {
    return {
        X: window.pageXOffset,
        Y: window.pageYOffset
    };
}

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