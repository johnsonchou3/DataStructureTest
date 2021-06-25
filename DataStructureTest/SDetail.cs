using System.Collections.Generic;
using System.Linq;

namespace DataStructureTest
{
    public class Sdetail
    {
        public string StockID { get; set; }

        public string StockName { get; set; }

        public int BuyTotal { get; set; }

        public int CellTotal { get; set; }

        public decimal AvgPrice { get; set; }

        public int BuyCellOver { get; set; }

        public decimal SetBrokerCnt { get; set; }

        public static Sdetail ComputeDetails(List<Sdata> selecteddata)
        {
            Sdetail Sdetail = new Sdetail();
            Sdetail.StockID = selecteddata.Select(row => row.StockID).FirstOrDefault().ToString();
            Sdetail.StockName = selecteddata.Select(row => row.StockName).FirstOrDefault().ToString();
            Sdetail.BuyTotal = selecteddata.Sum(row => row.BuyQty);
            Sdetail.CellTotal = selecteddata.Sum(row => row.CellQty);
            Sdetail.AvgPrice = selecteddata.Sum(row => GetRevenue(row.Price, row.BuyQty, row.CellQty))
                               / (Sdetail.BuyTotal + Sdetail.CellTotal);
            Sdetail.BuyCellOver = Sdetail.BuyTotal - Sdetail.CellTotal;
            //lambda
            decimal GetRevenue(decimal price, int buyqty, int cellqty)
            {
                return price * (buyqty + cellqty);
            }
            return Sdetail;
        }
    }
}
