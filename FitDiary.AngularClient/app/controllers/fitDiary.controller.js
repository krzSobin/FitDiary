angular
.module('fitDiary')
.controller('FitDiaryCtrl', class FitDiaryCtrl {
    constructor(FitDaysService) {
        this.FitDaysService = FitDaysService;
    }

    $onInit() {
        this.fitDays = this.FitDaysService.fitDays;
    }
});