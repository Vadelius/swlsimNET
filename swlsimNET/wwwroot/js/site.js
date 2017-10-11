var presets = require("./presets.js");

var checkboxValues = JSON.parse(localStorage.getItem('checkboxValues')) || {},
    $checkboxes = $('#OpeningShot, #Exposed, #HeadSignetIsCdr, #Savagery');

$checkboxes.on("change", function () {
    $checkboxes.each(function () {
        checkboxValues[this.id] = this.checked;
    });

    localStorage.setItem("checkboxValues", JSON.stringify(checkboxValues));
});