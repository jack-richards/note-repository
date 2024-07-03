window.ResizeBasedOnScrollHeight = function (id) {
    var textarea = document.getElementById(id);
    if (textarea) {
        textarea.style.height = textarea.scrollHeight + "px";
    }
}