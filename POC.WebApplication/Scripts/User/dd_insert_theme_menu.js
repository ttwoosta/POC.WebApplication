"use strict";

// run the windowLoaded when the page loads
window.addEventListener("load", windowLoaded);

function windowLoaded() {
    console.log("window is loaded");

    DevExpress.ui.themes.ready(function() {
        //ko.applyBindings(viewModel);
        console.log("themes is ready");
    });
    
    var theme_icon = "https://js.devexpress.com/ThemeBuilder/Content/Images/themes/generic-dark.svg";

    var themeObj = [
        { text: "Generic Light", value: "generic.light", image: theme_icon },
        { text: "Generic Dark", value: "generic.dark", image: theme_icon },
        { text: "Blue Sky", value: "bluesky.light", image: theme_icon },
        { text: "Blue Sky (dark)", value: "bluesky.dark", image: theme_icon },
        { text: "Green Mist", value: "greenmist", image: theme_icon },
        { text: "Carmine", value: "carmine", image: theme_icon },
        { text: "Contrast", value: "contrast", image: theme_icon },
        { text: "Dark Moon", value: "darkmoon", image: theme_icon },
        { text: "Dark Violet", value: "darkviolet", image: theme_icon },
        { text: "Blue (dark)", value: "blue.dark", image: theme_icon },
        { text: "Blue", value: "blue.light", image: theme_icon },
        { text: "Lime", value: "lime.light", image: theme_icon },
        { text: "Lime (dark)", value: "lime.dark", image: theme_icon },
        { text: "Orange", value: "orange.light", image: theme_icon },
        { text: "Orange (dark)", value: "orange.dark", image: theme_icon },
        { text: "Purple", value: "purple.light", image: theme_icon },
        { text: "Purple (dark)", value: "purple.dark", image: theme_icon },
        { text: "Teal", value: "teal.light", image: theme_icon },
        { text: "Teal (dark)", value: "teal.dark", image: theme_icon },
    ];

    var currentThemeKey = localStorage.getItem("my_current_theme");
    if (currentThemeKey == null)
        currentThemeKey = themeObj[0].value;

    // apply current theme
    DevExpress.ui.themes.current(currentThemeKey);

    $("#themeDropDownBoxContainer").attr("style", "width: 170px; margin: 10px").dxDropDownBox({

        value: currentThemeKey,
        valueExpr: "value",
        displayExpr: "text",
        dataSource: new DevExpress.data.DataSource({
            store: new DevExpress.data.ArrayStore({ data: themeObj, key: "value" }),
        }),
        dropDownOptions: {
            title: "Themes",
            showTitle: true,
            fullScreen: false,
            showCloseButton: true
        },
        contentTemplate: function (e) {
            var $list = $("<div>").dxList({
                dataSource: e.component.option("dataSource"),
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    e.component.option("value", arg.addedItems[0].value);
                    e.component.close();

                    var strTheme = arg.addedItems[0].value.toLowerCase();

                    $("#body-container").fadeOut("slow", function () {
                        DevExpress.ui.themes.current(strTheme);
                    }).fadeIn("slow");

                    localStorage.setItem("my_current_theme", strTheme);
                }
            });
            return $list;
        }
    });

}