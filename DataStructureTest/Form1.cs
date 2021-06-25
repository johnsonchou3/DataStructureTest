using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DataStructureTest
{
    /// <summary>
    /// ReadCSV檔的WinForm 程式
    /// </summary>
    public partial class Form1 : Form
    {
        List<Sdata> Datalist = new List<Sdata>();
        /// <summary>
        /// 開啟Winform
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "csv files (*.csv)|*.csv";
                label1.Text = "讀檔中";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog1.FileName;
                    BindDataCSV(txtFilePath.Text);
                    label1.Text = "讀檔完成";
                }
            }

        }
        private void BindDataCSV(string filePath)
        {
            Stopwatch loadtime = new Stopwatch();
            Stopwatch combotime = new Stopwatch();
            //讀檔計時
            loadtime.Start();
            Datalist = System.IO.File.ReadAllLines(filePath)
                                               .Skip(1)
                                               .Select(v => Sdata.FromCsv(v))
                                               .ToList();
            dgv1.DataSource = Datalist;
            loadtime.Stop();
            richTextBox1.Text += "讀取時間 : " + ShowTime(loadtime) + "\n";
            combotime.Start();
            comboBox1.Items.Add("All");
            var comboinfo = Datalist.Select(row => new { row.StockID, row.StockName }).Distinct().ToList();
            for (int i = 0; i < comboinfo.Count; i++)
            {
                comboBox1.Items.Add(comboinfo[i].StockID + " - " + comboinfo[i].StockName);
            }
            combotime.Stop();
            richTextBox1.Text += "ComboBox產生時間 : " + ShowTime(combotime) + "\n";
        }
        private string ShowTime(Stopwatch stopwatch)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds);
            return elapsedTime;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<Sdata> searchdata = new List<Sdata>();
            List<Sdetail> searchdetail = new List<Sdetail>();
            bool combohastxt = !String.IsNullOrEmpty(comboBox1.Text);
            string comboinput = comboBox1.Text;
            string[] selectedstock = GetTextArray(comboinput); 
            Stopwatch searchtime = new Stopwatch();

            if (combohastxt)
            {
                searchtime.Start();
                foreach (string stock in selectedstock)
                {
                    
                    searchdata.AddRange(Datalist.Where(row => row.StockID == stock));
                    searchdetail.Add(Sdetail.ComputeDetails(searchdata));
                }
                dgv1.DataSource = searchdata;
                dgv2.DataSource = searchdetail;
                searchtime.Stop();
                richTextBox1.Text += "查詢時間 : " + ShowTime(searchtime) + "\n";
            }
        }
        private string[] GetListOfStockIds(IEnumerable<Sdata> alldatas)
        {
            return alldatas.Select(row => row.StockID).Distinct().ToArray();
        }
        private string[] GetTextArray(string combotext)
        {
            bool inputtype1 = combotext == "All";
            bool inputtype2 = combotext.Contains('-');
            bool inputtype3 = combotext.Contains(',');
            if (inputtype1)
            {
                return GetListOfStockIds(Datalist);
            }
            else if (inputtype2)
            {
                int index = combotext.LastIndexOf("-");
                return new[] { combotext.Substring(0, index - 1) };
            }
            else if (inputtype3)
            {
                return combotext.Split(',');
            }
            else
            {
                return new[] {combotext};
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch top50timer = new Stopwatch();
            List<Sdata> searchdata = new List<Sdata>();
            List<Top50> Top50list = new List<Top50>();
            bool combohastxt = !String.IsNullOrEmpty(comboBox1.Text);
            string comboinput = comboBox1.Text;
            string[] selectedstock = GetTextArray(comboinput);

            if (combohastxt)
            {
                top50timer.Start();
                foreach (string stock in selectedstock)
                {
                    Top50list.AddRange(Top50.GetTop50(stock, Datalist));
                }
                dgv3.DataSource = Top50list;
                top50timer.Stop();
                richTextBox1.Text += "Top50 產生時間 : " + ShowTime(top50timer) + "\n";
            }
        }


    }
}
