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
}

export function removeResizeEventListener() {
    window.removeEventListener('resize', onResize);
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
}

export function removeScrollEventListener() {
    window.removeEventListener('scroll', onScroll);
}

function onScroll() {
    DotNet.invokeMethodAsync('Tizzani.BlazorHelpers.BrowserWindow', 'NotifyScroll');
}