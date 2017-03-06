angular
.module('fitDiary')
.component('fitDayItem', {
    bindings: {
        'fitDay': '<'
    },
    templateUrl: `fitDayItem.html`
});