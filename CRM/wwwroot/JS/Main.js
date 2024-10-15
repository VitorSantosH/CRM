function GetUrl() {
    var url = window.location.href;
    var baseUrl = url.substring(0, url.indexOf(window.location.pathname) + 1);
    return baseUrl;
}