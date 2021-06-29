using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Text;

namespace DataStructureTest
{
    /// <summary>
    /// ReadCSV檔的WinForm 程式
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 原CSV產出的Sdata List
        /// </summary>
        private List<Sdata> Datalist = new List<Sdata>();

        /// <summary>
        /// id 對sdata list 的dictionary
        /// </summary>
        private Dictionary<string, List<Sdata>> Datadic = new Dictionary<string, List<Sdata>>();

        /// <summary>
        /// id 對name 的dictionary
        /// </summary>
        private Dictionary<string, string> Idnamedic = new Dictionary<string, string>();

        /// <summary>
        /// winform 內容
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 讀取檔案按鈕
        /// </summary>
        /// <param name="sender">按下讀取檔案發生</param>
        /// <param name="e">事件數據</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "csv files (*.csv)|*.csv";
                label1.Text = "讀檔中";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog1.FileName;
                    Thread t = new Thread(new ParameterizedThreadStart(BindDataCSV));
                    t.Start(txtFilePath.Text);
                    label1.Text = "讀檔完成";
                }
            }
        }

        /// <summary>
        /// 根據Filepath 而產生Datalist 並產出combobox
        /// </summary>
        /// <param name="filePathobj">根據dialog選檔自動產出filepathobj以讓Thread使用, 再轉成string</param>
        private void BindDataCSV(object filePathobj)
        {
            string filePath = filePathobj as string;
            Stopwatch loadtime = new Stopwatch();
            Stopwatch combotime = new Stopwatch();
            bool IsFirstLine = true;
            //讀檔計時
            loadtime.Start();
            string[] arrayoflines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
            foreach (string line in arrayoflines)
            {
                if (IsFirstLine)
                {
                    IsFirstLine = false;
                    continue;
                }
                Datalist.Add(Sdata.FromCsvLine(line));
            }
            Datadic = DataGroupByID(Datalist);
            Idnamedic = NameGroupByID(Datalist);
            this.Invoke((MethodInvoker)delegate
            {
                dgv1.DataSource = Datalist;
            });
            loadtime.Stop();
            this.Invoke((MethodInvoker)delegate
            {
                richTextBox1.Text += "讀取時間 : " + ShowTime(loadtime) + "\n";
            });
            combotime.Start();
            this.Invoke((MethodInvoker)delegate
            {
                comboBox1.Items.Add("All");
                foreach (KeyValuePair<string, string> pair in Idnamedic)
                {
                    comboBox1.Items.Add(pair.Key + " - " + pair.Value);
                }
            });
            combotime.Stop();
            this.Invoke((MethodInvoker)delegate
            {
                richTextBox1.Text += "ComboBox產生時間 : " + ShowTime(combotime) + "\n";
            });
        }

        /// <summary>
        /// 將stopwatch 物件變成string 的function
        /// </summary>
        /// <param name="stopwatch">必須已完成start+stop 的stopwatch 物件</param>
        /// <returns></returns>
        private string ShowTime(Stopwatch stopwatch)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds);
            return elapsedTime;
        }

        /// <summary>
        /// 按鈕二, 查詢特定股並顯示於dgv1, dgv2
        /// </summary>
        /// <param name="sender">按下查詢股票發生</param>
        /// <param name="e">事件數據</param>
        private void Button2_Click(object sender, EventArgs e)
        {
            List<Sdata> searchdata = new List<Sdata>();
            List<Sdetail> searchdetail = new List<Sdetail>();
            bool combohastxt = !string.IsNullOrEmpty(comboBox1.Text);
            string comboinput = comboBox1.Text;
            List<string> selectedstock = GetTextArray(comboinput);
            Stopwatch searchtime = new Stopwatch();
            if (combohastxt & IDisValid(selectedstock))
            {
                searchtime.Start();
                foreach (string stock in selectedstock)
                {
                    List<Sdata> tempsdata = Datadic[stock];
                    Sdetail tempsdetail = Sdetail.ComputeDetails(tempsdata);
                    searchdata.AddRange(tempsdata);
                    searchdetail.Add(tempsdetail);
                }
                dgv1.DataSource = searchdata;
                dgv2.DataSource = searchdetail;
                searchtime.Stop();
                richTextBox1.Text += "查詢時間 : " + ShowTime(searchtime) + "\n";
            }
            else
            {
                MessageBox.Show("輸入錯誤", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 得到全部stockid 並組成list
        /// </summary>
        /// <param name="idnamedic">需輸入idtoname的dictionary</param>
        /// <returns></returns>
        private List<string> GetListOfStockIds(Dictionary<string, string> idnamedic)
        {
            List<string> listofids = new List<string>();
            foreach (KeyValuePair<string, string> idname in idnamedic)
            {
                listofids.Add(idname.Key);
            }
            return listofids;
        }

        /// <summary>
        /// 取得combobox 內容並取得ID, 並回傳string[ID]
        /// </summary>
        /// <param name="combotext">讀取Combotext.text的內容</param>
        /// <returns>回傳string array [ID]</returns>
        private List<string> GetTextArray(string combotext)
        {
            bool inputtype1 = combotext == "All";
            bool inputtype2 = combotext.Contains('-');
            bool inputtype3 = combotext.Contains(',');
            if (inputtype1)
            {
                return GetListOfStockIds(Idnamedic);
            }
            else if (inputtype2)
            {
                int index = combotext.LastIndexOf("-");
                return new List<string> { combotext.Substring(0, index - 1) };
            }
            else if (inputtype3)
            {
                List<string> combotextlist = new List<string>();
                foreach (string text in combotext.Split(','))
                {
                    combotextlist.Add(text);
                }
                return combotextlist;
            }
            else
            {
                return new List<string> { combotext };
            }
        }

        /// <summary>
        /// 把同樣ID 的sdata 組成一個list 並以ID 作index 建立dictionary
        /// </summary>
        /// <param name="sdatalist">輸入全部sdata 的List </param>
        /// <returns></returns>
        private Dictionary<string, List<Sdata>> DataGroupByID(List<Sdata> sdatalist)
        {
            Dictionary<string, List<Sdata>> idtolistdic = new Dictionary<string, List<Sdata>>();
            foreach (Sdata sdata in sdatalist)
            {
                string tempid = sdata.StockID;
                if (idtolistdic.ContainsKey(tempid))
                {
                    idtolistdic[tempid].Add(sdata);
                }
                else
                {
                    List<Sdata> newlist = new List<Sdata>();
                    newlist.Add(sdata);
                    idtolistdic.Add(tempid, newlist);
                }
            }
            return idtolistdic;
        }

        /// <summary>
        /// 建立ID 對name 的dictionary
        /// </summary>
        /// <param name="sdatalist">輸入全部資料的list of sdata </param>
        /// <returns></returns>
        private Dictionary<string, string> NameGroupByID(List<Sdata> sdatalist)
        {
            Dictionary<string, string> idtonamedic = new Dictionary<string, string>();
            foreach (Sdata sdata in sdatalist)
            {
                string tempid = sdata.StockID;
                string tempname = sdata.StockName;
                if (idtonamedic.ContainsKey(tempid))
                {
                    continue;
                }
                else
                {
                    List<Sdata> newlist = new List<Sdata>();
                    newlist.Add(sdata);
                    idtonamedic.Add(tempid, tempname);
                }
            }
            return idtonamedic;
        }

        /// <summary>
        /// 檢查id 是否在data 內
        /// </summary>
        /// <param name="inputids">輸入id的array, 用GetTextArray(comboboxtext)取得</param>
        /// <returns></returns>
        private bool IDisValid(List<string> inputids)
        {
            foreach (string id in inputids)
            {
                if (!Idnamedic.ContainsKey(id))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 按鈕三, 查詢Top50 +-BuyCellTotal
        /// </summary>
        /// <param name="sender">按下「買賣超Top50」發生</param>
        /// <param name="e">事件數據</param>
        private void Button3_Click(object sender, EventArgs e)
        {
            Stopwatch top50timer = new Stopwatch();
            List<Sdata> searchdata = new List<Sdata>();
            List<Top50> top50list = new List<Top50>();
            bool combohastxt = !string.IsNullOrEmpty(comboBox1.Text);
            string comboinput = comboBox1.Text;
            List<string> selectedstock = GetTextArray(comboinput);

            if (combohastxt & IDisValid(selectedstock))
            {
                top50timer.Start();
                foreach (string stock in selectedstock)
                {
                    List<Top50> tempTop50list = new List<Top50>();
                    List<Sdata> tempsdata = Datadic[stock];
                    tempTop50list = Top50.GetTop50(tempsdata);
                    top50list.AddRange(tempTop50list);
                }
                dgv3.DataSource = top50list;
                top50timer.Stop();
                richTextBox1.Text += "Top50 產生時間 : " + ShowTime(top50timer) + "\n";
            }
            else
            {
                MessageBox.Show("輸入錯誤", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
