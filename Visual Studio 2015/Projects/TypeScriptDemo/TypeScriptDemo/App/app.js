var myApp;
(function (myApp) {
    $(function () {
        var contentBoxDiv = $("#content-body");
        contentBoxDiv
            .text("Hello TypeScript")
            .css({
            "padding": "12px",
            "color": "Blue",
            "font-size": "32px"
        });
    });
})(myApp || (myApp = {}));
//# sourceMappingURL=app.js.map