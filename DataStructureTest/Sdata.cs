using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureTest
{   
/// <summary>
/// 讓每個row 成為Sdata Class
/// </summary>
    public class Sdata
    {

        public int DealDate { get; set; }

        public string StockID { get; set; }

        public string StockName { get; set; }

        public string SecBrokerID { get; set; }

        public string SecBrokerName { get; set; }

        public decimal Price { get; set; }

        public int BuyQty { get; set; }

        public int CellQty { get; set; }

        public static Sdata FromCsv(string csvLine)
        {
            Sdata sdata = new Sdata();
            string[] line = csvLine.Split(',');
            sdata.DealDate = Convert.ToInt32(line[0]);
            sdata.StockID = Convert.ToString(line[1]);
            sdata.StockName = Convert.ToString(line[2]);
            sdata.SecBrokerID = Convert.ToString(line[3]);
            sdata.SecBrokerName = Convert.ToString(line[4]);
            sdata.Price = Convert.ToDecimal(line[5]);
            sdata.BuyQty = Convert.ToInt32(line[6]);
            sdata.CellQty = Convert.ToInt32(line[7]);
            return sdata;
        }
    }
}
