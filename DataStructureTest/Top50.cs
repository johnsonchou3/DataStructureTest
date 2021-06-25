using System.Collections.Generic;
using System.Linq;

namespace DataStructureTest
{
    public class Top50
    {
        public string StockName { get; set; }

        public string SecBrokerName { get; set; }

        public int BuyCellOver { get; set; }

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

