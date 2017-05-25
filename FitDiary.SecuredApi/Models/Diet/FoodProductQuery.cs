using System.Data.SqlClient;

namespace FitDiary.SecuredApi.Models.Diet
{
    public class FoodProductQueryParams
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public double? MaxSugar { get; set; }
        public SortOrder SortOrder { get; set; }
        public SortColumn SortColumn { get; set; }
    }

    public enum SortColumn
    {
        SortByName = 0,
        SortByKCal
    }
}