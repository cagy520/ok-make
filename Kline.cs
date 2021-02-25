using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cagy_okex
{
    public class DataItem
    {
        public string tm { get; set; }
        public string kai { get; set; }
        public string gao { get; set; }
        public string di { get; set; }
        public string shou { get; set; }
        public string vm { get; set; }
        public string vmb { get; set; }
    }
    public class KLine
    {
        public List<DataItem> data { get; set; }
    }
}
