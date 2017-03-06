angular
.module('fitDiary')
.service('FitDaysService', class FitDaysService {
    constructor() {
        this.fitDays = [{
            Date: '2017-02-06T00:00:00',
            MealsCount: 1,
            TotalKCal: 1500,
            RealizationPercent: 0,
            Macros: []
        }, {
            id: 2,
            name: 'Tester',
            age: 99,
            email: 'randomemail@com.pl'
        }];
    }
});