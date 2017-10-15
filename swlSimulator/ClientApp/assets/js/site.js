var presets = require("./presets.js");

var checkboxValues = JSON.parse(localStorage.getItem('checkboxValues')) || {},
    $checkboxes = $('#openingShot, #exposed, #headCdr, #waistCdr');

$checkboxes.on("change", function () {
    $checkboxes.each(function () {
        checkboxValues[this.id] = this.checked;
    });

    localStorage.setItem("checkboxValues", JSON.stringify(checkboxValues));
});

console.log(presets);

$("#presetHammer").click(function () {
    alert("Handler for .click() called.");
});