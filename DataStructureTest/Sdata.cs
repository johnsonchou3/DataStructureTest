using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureTest
{   
/// <summary>
/// 於dgv1 顯示, 最基本資料
/// </summary>
    public class Sdata
    {
        /// <summary>
        /// 日期
        /// </summary>
        public int DealDate { get; set; }

        /// <summary>
        /// 股票代號
        /// </summary>
        public string StockID { get; set; }

        /// <summary>
        /// 股票名稱
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// 券商代號
        /// </summary>
        public string SecBrokerID { get; set; }

        /// <summary>
        /// 券商名稱
        /// </summary>
        public string SecBrokerName { get; set; }

        /// <summary>
        /// 成交價
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 買進張數
        /// </summary>
        public int BuyQty { get; set; }

        /// <summary>
        /// 賣出張數
        /// </summary>
        public int CellQty { get; set; }

        /// <summary>
        /// 將CSV 每一行文字轉成Sdata物件
        /// </summary>
        /// <param name="csvLine">要有8個欄位的string</param>
        /// <returns>回傳Sdata物件</returns>
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
