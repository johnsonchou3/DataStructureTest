using System.Collections.Generic;

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
        public decimal SecBrokerCnt { get; set; }

        /// <summary>
        /// 輸入已用stockid過濾的Sdata list, 會回傳出sdetail物件(只有一行)
        /// </summary>
        /// <param name="selecteddata">輸入sdata list, 需要單一ID</param>
        /// <returns>回傳Sdetail物件</returns>
        public static Sdetail ComputeDetails(List<Sdata> selecteddata)
        {
            int buytotal = 0;
            int celltotal = 0;
            decimal revenue = 0;
            HashSet<string> secbrokerset = new HashSet<string>();
            Sdetail sdetail = new Sdetail();
            sdetail.StockID = selecteddata[0].StockID;
            sdetail.StockName = selecteddata[0].StockName;
            foreach (Sdata sdata in selecteddata)
            {
                int buyqty = sdata.BuyQty;
                int cellqty = sdata.CellQty;
                decimal price = sdata.Price;
                buytotal += buyqty;
                celltotal += cellqty;
                revenue += price * (buyqty + cellqty);
                secbrokerset.Add(sdata.SecBrokerID);
            }
            sdetail.BuyTotal = buytotal;
            sdetail.CellTotal = celltotal;
            if (buytotal + celltotal != 0)
            {
                sdetail.AvgPrice = revenue / (buytotal + celltotal);
            }
            else
            {
                sdetail.AvgPrice = 0;
            }
            sdetail.BuyCellOver = buytotal - celltotal;
            sdetail.SecBrokerCnt = secbrokerset.Count;
            return sdetail;
        }
    }
}