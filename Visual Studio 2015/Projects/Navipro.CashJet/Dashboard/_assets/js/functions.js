﻿function fullscreen() {
    var a = document.getElementsByTagName("a");
    for (var i = 0; i < a.length; i++) {
        if (a[i].className.match("noeffect")) {
        }
        else {
            a[i].onclick = function() {
                window.location = this.getAttribute("href");
                return false;
            };
        }
    }
}

function hideURLbar() {
    window.scrollTo(0, 0.9);
}


window.onload = function() {
    fullscreen();
    hideURLbar();
};

