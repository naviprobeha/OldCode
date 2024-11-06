
module myApp {
    
    $( () =>  {

        let contentBoxDiv: JQuery = $("#content-body");

        contentBoxDiv
            .text("Hello TypeScript")
            .css({
                "padding": "12px",
                "color": "Blue",
                "font-size": "32px"
            });

    });
}