(function (angular) {
    'use strict';
    angular.module('import', [])
        .controller('importController', ['$scope', importController]);

    function importController($scope) {
        $scope.hammerPreset = function() {
            console.log("Test");
        }
        $scope.primaryWeapon = 'John Smith';
        $scope.contacts = [
        ];
        
        $scope.console.log("test");
        $scope.greet = function () {
            alert($scope.name);
        };

        $scope.addContact = function () {
            $scope.contacts.push({ type: 'email', value: 'yourname@example.org' });
        };

        $scope.removeContact = function (contactToRemove) {
            var index = $scope.contacts.indexOf(contactToRemove);
            $scope.contacts.splice(index, 1);
        };

        $scope.clearContact = function (contact) {
            contact.type = 'phone';
            contact.value = '';
        };
    }
})(window.angular);

var presets = require("./presets.js");

var checkboxValues = JSON.parse(localStorage.getItem('checkboxValues')) || {},
    $checkboxes = $('#openingShot, #exposed, #headCdr, #waistCdr');

$checkboxes.on("change", function () {
    $checkboxes.each(function () {
        checkboxValues[this.id] = this.checked;
    });

    localStorage.setItem("checkboxValues", JSON.stringify(checkboxValues));
});

console.log("test");

$("#presetHammer").click(function () {
    alert("Handler for .click() called.");
});