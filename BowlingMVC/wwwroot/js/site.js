// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// menu hover
function IsActive(action) {
    var url = window.location.href.toLowerCase();
    if (url.indexOf(action.toLowerCase()) !== -1) {
        return "active";
    }
    return "";
}