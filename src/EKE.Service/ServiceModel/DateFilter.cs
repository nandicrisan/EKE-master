using System.Collections.Generic;

namespace EKE.Service.ServiceModel
{
    public class DateFilter
    {
        public int Year { get; set; }
        public List<MonthFilter> Months { get; set; }
    }

    public class MonthFilter
    {
        public int Month { get; set; }
        public int Items {get;set;}
        public string MonthName { get; set; }
}
}
