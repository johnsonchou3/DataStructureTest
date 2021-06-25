﻿using System.Collections.Generic;
using System.Linq;

namespace DataStructureTest
{
    /// <summary>
    /// 展示在Dgv3 的物件,顯示Top50 +-超買賣
    /// </summary>
    public class Top50
    {
        /// <summary>
        /// 股票名稱
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// 卷商名稱
        /// </summary>
        public string SecBrokerName { get; set; }

        /// <summary>
        /// 超買賣數
        /// </summary>
        public int BuyCellOver { get; set; }

        /// <summary>
        /// 將輸入進的ID的sdata list 轉成Top50物件的list
        /// </summary>
        /// <param name="stockid">參數1-所選之ID</param>
        /// <param name="datalist">參數2-整張Datatable</param>
        /// <returns>回傳top50物件的list, 可直接輸出到dgv3</returns>
        public static List<Top50> GetTop50(string stockid, List<Sdata> datalist)
        {
            List<Top50> Top50list = new List<Top50>();
            List<Top50> Top50listpos = new List<Top50>();
            List<Top50> Top50listneg = new List<Top50>();
            string StockName;
            StockName = datalist.Where(row => row.StockID == stockid).Select(row => row.StockName).FirstOrDefault().ToString();
            var secidgroup = datalist.Where(row => row.StockID == stockid).GroupBy(x => x.SecBrokerName);
            foreach (var secid in secidgroup)
            {
                Top50 top50 = new Top50();
                top50.StockName = StockName;
                top50.SecBrokerName = secid.Key;
                top50.BuyCellOver = secid.Sum(x => x.BuyQty) - secid.Sum(x => x.CellQty);
                if (top50.BuyCellOver > 0)
                {
                    Top50listpos.Add(top50);
                }
                else if (top50.BuyCellOver < 0)
                {
                    Top50listneg.Add(top50);
                }
            }
            Top50list.AddRange(Top50listpos.OrderByDescending(row => row.BuyCellOver).ToList().Take(50));
            Top50list.AddRange(Top50listneg.OrderBy(data => data.BuyCellOver).Take(50).ToList().Take(50));
            return Top50list;
        }
    }
}