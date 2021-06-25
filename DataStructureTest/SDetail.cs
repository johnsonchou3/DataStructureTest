using System.Collections.Generic;
using System.Linq;

namespace DataStructureTest
{
    /// <summary>
    /// 展示在dgv2 的查詢股票詳情
    /// </summary>
    public class Sdetail
    {
        /// <summary>
        /// 股票代號
        /// </summary>
        public string StockID { get; set; }

        /// <summary>
        /// 股票名稱
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// 總買進
        /// </summary>
        public int BuyTotal { get; set; }

        /// <summary>
        /// 總賣出
        /// </summary>
        public int CellTotal { get; set; }

        /// <summary>
        /// 價格加均平均
        /// </summary>
        public decimal AvgPrice { get; set; }

        /// <summary>
        /// 買賣超
        /// </summary>
        public int BuyCellOver { get; set; }

        /// <summary>
        /// 卷商數
        /// </summary>
        public decimal SetBrokerCnt { get; set; }

        /// <summary>
        /// 輸入已用stockid過濾的Sdata list, 會回傳出sdetail物件(只有一行)
        /// </summary>
        /// <param name="selecteddata">輸入sdata list, 需要單一ID</param>
        /// <returns>回傳Sdetail物件</returns>
        public static Sdetail ComputeDetails(List<Sdata> selecteddata)
        {
            Sdetail Sdetail = new Sdetail();
            Sdetail.StockID = selecteddata.Select(row => row.StockID).First().ToString();
            Sdetail.StockName = selecteddata.Select(row => row.StockName).First().ToString();
            Sdetail.BuyTotal = selecteddata.Sum(row => row.BuyQty);
            Sdetail.CellTotal = selecteddata.Sum(row => row.CellQty);
            Sdetail.AvgPrice = selecteddata.Sum(row => GetRevenue(row.Price, row.BuyQty, row.CellQty))
                               / (Sdetail.BuyTotal + Sdetail.CellTotal);
            Sdetail.BuyCellOver = Sdetail.BuyTotal - Sdetail.CellTotal;
            Sdetail.SetBrokerCnt = selecteddata.Select(row => row.SecBrokerID).Distinct().Count();
            //lambda
            decimal GetRevenue(decimal price, int buyqty, int cellqty)
            {
                return price * (buyqty + cellqty);
            }
            return Sdetail;
        }
    }
}
