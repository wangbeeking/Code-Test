using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrinterTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window ,INotifyPropertyChanged
    {
        #region 绑定定义
        public   event PropertyChangedEventHandler  PropertyChanged;
        protected void Notify(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
        #region 初始化
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        string get()
         {
             DataTable dt = new DataTable();
             dt.Columns.Add("商品编码");
             dt.Columns.Add("商品名称");
             dt.Columns.Add("数量");
             dt.Columns.Add("单价");
             dt.Columns.Add("金额");

            for (int i = 0; i < 5; i++)
             {
                 DataRow dr = dt.NewRow();
                 dr["商品编码"] = "1234567891111";
                 dr["商品名称"] = "西湖龙景茶叶新茶";
                 dr["数量"] = "5";
                 dr["单价"] = "500.8";
                 dr["金额"] = "2504";

                dt.Rows.Add(dr);
             }
             TicketSet ts = new TicketSet();
             ts.TicketSignature = "XXXXX收银凭据";
             ts.TicketTitle = "消12费aa小ee票dfd sas";
             ts.TicketFooter = "谢谢惠顾！";
             ts.TicketWidth = lineSize;
             ts.DtGoodsList = dt;
             ts.AddKeyAndValueTop("消费单号：", "2009090911");
             ts.AddKeyAndValueTop("结算时间：", "2009-09-09");
             ts.AddKeyAndValueTop("包    间：", "5号");
             ts.AddKeyAndValueTop("会员卡号：", "0000088");
             ts.AddKeyAndValueTop("操 作 员：", "1001");
             ts.AddKeyAndValueTop("服 务 员：", "0001");
             ts.AddKeyAndValueFoot("桌台费用：", "5000.98");



             return ts.Ticket();
         }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //PrintDocument printer = new PrintDocument();
            //foreach (string str in PrinterSettings.InstalledPrinters)
            //    Debug.WriteLine(str);

            //PrintService a = new PrintService();

            String s = get();
            textList = s.Split('\n').ToList();

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(Print_Content);
            //纸张设置默认 
            PaperSize pageSize = new PaperSize("自定义纸张", fontSize * lineSize, (textList.Count * lineHeight));
            pd.DefaultPageSettings.PaperSize = pageSize;
            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败." + ex.Message);
            }
        }
        private List<string> textList = new List<string>();       //打印内容行 
        private static int fontSize = 10;                //字大小 
        private static int lineHeight = 2 * fontSize;              //打印行高 
        private static int lineSize = (132/fontSize)*2;                //每行打印字数 
        private void Print_Content(object sender, PrintPageEventArgs e)
        {
            var mark = 0;
            foreach (var item in textList)
            {
                e.Graphics.DrawString(item, new Font(new System.Drawing.FontFamily("宋体"), (float)fontSize), System.Drawing.Brushes.Black, 0, mark * lineHeight);
                mark++;
            }
        } 
    }
}
