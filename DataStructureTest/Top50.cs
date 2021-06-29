using System.Collections.Generic;

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
        /// 輸入已用stockid過濾的Sdata list, 會回傳出Top50物件的list
        /// </summary>
        /// <param name="selecteddata">輸入sdata list, 需要單一ID</param>
        /// <returns>回傳Top50 的list </returns>
        public static List<Top50> GetTop50(List<Sdata> selecteddata)
        {
            List<Top50> top50list = new List<Top50>();
            List<Top50> top50listpos = new List<Top50>();
            List<Top50> top50listneg = new List<Top50>();
            Dictionary<string, List<Sdata>> secidgroup = new Dictionary<string, List<Sdata>>();
            string stockname;
            stockname = selecteddata[0].StockName;
            secidgroup = DataGroupBySec(selecteddata);
            foreach (KeyValuePair<string, List<Sdata>> secidlist in secidgroup)
            {
                int buycelltotal = 0;
                Top50 top50 = new Top50();
                top50.StockName = stockname;
                top50.SecBrokerName = secidlist.Key;
                foreach (Sdata sdata in secidlist.Value)
                {
                    buycelltotal += sdata.BuyQty - sdata.CellQty;
                }
                top50.BuyCellOver = buycelltotal;
                if (top50.BuyCellOver > 0)
                {
                    top50listpos.Add(top50);
                }
                else if (top50.BuyCellOver < 0)
                {
                    top50listneg.Add(top50);
                }
            }
            top50listpos.Sort((top50a, top50b) => top50b.BuyCellOver.CompareTo(top50a.BuyCellOver));
            top50listneg.Sort((top50a, top50b) => top50a.BuyCellOver.CompareTo(top50b.BuyCellOver));
            if (top50listpos.Count <= 50)
            {
                top50list.AddRange(top50listpos);
            }
            else if (top50listpos.Count > 50)
            {
                top50list.AddRange(top50listpos.GetRange(0, 50));
            }
            if (top50listpos.Count <= 50)
            {
                top50list.AddRange(top50listneg);
            }
            else if (top50listneg.Count > 50)
            {
                top50list.AddRange(top50listneg.GetRange(0, 50));
            }
            return top50list;
        }

        /// <summary>
        /// 以相同secbrokername 的sdata 組成list, 並建立secbrokername to sdata list 的dictionary
        /// </summary>
        /// <param name="selecteddata">所選擇相同ID的sdata list</param>
        /// <returns></returns>
        private static Dictionary<string, List<Sdata>> DataGroupBySec(List<Sdata> selecteddata)
        {
            Dictionary<string, List<Sdata>> sectolistdic = new Dictionary<string, List<Sdata>>();
            foreach (Sdata sdata in selecteddata)
            {
                string tempsecname = sdata.SecBrokerName;
                if (sectolistdic.ContainsKey(tempsecname))
                {
                    sectolistdic[tempsecname].Add(sdata);
                }
                else
                {
                    List<Sdata> newdiclist = new List<Sdata>();
                    newdiclist.Add(sdata);
                    sectolistdic[tempsecname] = newdiclist;
                }
            }
            return sectolistdic;
        }
    }
}