using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public class ChartData
    {
        public string[] Labels              { get; set; }
        public string[] DatasetLabels       { get; set; }
        public List<int[]> DatasetDatas     { get; set; }
    }
}