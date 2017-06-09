using System.Data.SqlClient;

namespace FitDiary.SecuredApi.Models.Training
{
    public class ExcerciseQueryParams
    {
        public string Name { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}