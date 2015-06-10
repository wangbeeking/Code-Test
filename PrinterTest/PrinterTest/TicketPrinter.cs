using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System;

namespace PrinterTest
{
    public class TicketSet
    {
        #region 字段

        #region 头部信息
        private List<keyAndValue> _KeyAndValueListTop = new List<keyAndValue>();
        public List<keyAndValue> KeyAndValueListTop
        {
            get { return _KeyAndValueListTop; }
            set { _KeyAndValueListTop = value; }
        }
        #endregion
        #region 中部信息
        private List<keyAndValue> _KeyAndValueListMid = new List<keyAndValue>();
        public List<keyAndValue> KeyAndValueListMid
        {
            get { return _KeyAndValueListMid; }
            set { _KeyAndValueListMid = value; }
        }
        #endregion
        #region 尾部信息
        private List<keyAndValue> _KeyAndValueListFoot = new List<keyAndValue>();
        public List<keyAndValue> KeyAndValueListFoot
        {
            get { return _KeyAndValueListFoot; }
            set { _KeyAndValueListFoot = value; }
        }
        #endregion
        #region 小票顶部签名
        private string _TicketSignature="";
        public string TicketSignature
        {
            get { return _TicketSignature; }
            set { _TicketSignature = value; }
        }
        #endregion
        #region 小票的标题
        private string _TicketTitle="";
        public string TicketTitle
        {
            get { return _TicketTitle; }
            set { _TicketTitle = value; }
        }
        #endregion
        #region 小票底部签名
        private string _TicketFooter = "";
        public string TicketFooter
        {
            get { return _TicketFooter; }
            set { _TicketFooter = value; }
        }
        #endregion
        #region 商品列表信息
        private DataTable _DtGoodsList = new DataTable();
        public DataTable DtGoodsList
        {
            get { return _DtGoodsList; }
            set { _DtGoodsList = value; }
        }
        #endregion
        #region 小票宽度,按字符数计算
        private int _TicketWidth;
        public int TicketWidth
        {
            get { return _TicketWidth; }
            set { _TicketWidth = value; }
        }
        #endregion
        #region 宽度百分比
        private Decimal _Colper1 = 0.4M;
        public Decimal Colper1
        {
            get { return _Colper1; }
            set { _Colper1 = value; }
        }
        private Decimal _Colper2 = 0.2M;
        public Decimal Colper2
        {
            get { return _Colper2; }
            set { _Colper2 = value; }
        }
        private Decimal _Colper3 = 0.2M;
        public Decimal Colper3
        {
            get { return _Colper3; }
            set { _Colper3 = value; }
        }
        private Decimal _Colper4 = 0.2M;
        public Decimal Colper4
        {
            get { return _Colper4; }
            set { _Colper4 = value; }
        }
        #endregion
        #region 重要分隔符的样式
        private Char _SignWeight = '*';
        public Char SignWeight
        {
            get { return _SignWeight; }
            set { _SignWeight = value; }
        }
        #endregion
        #region 一般分隔符的样式
        private Char _SignLight = ' ';
        public Char SignLight
        {
            get { return _SignLight; }
            set { _SignLight = value; }
        }
        #endregion

        #endregion

        #region 函数

        #region 添加信息
        public void AddKeyAndValueTop(String keyStr, String valueStr)
        {
            keyAndValue keyandvale = new keyAndValue();
            keyandvale.keyStr = keyStr;
            keyandvale.valueStr = valueStr;
            this.KeyAndValueListTop.Add(keyandvale);
        }
        public void AddKeyAndValueMid(String keyStr, String valueStr)
        {
            keyAndValue keyandvale = new keyAndValue();
            keyandvale.keyStr = keyStr;
            keyandvale.valueStr = valueStr;
            this.KeyAndValueListMid.Add(keyandvale);
        }
        public void AddKeyAndValueFoot(String keyStr, String valueStr)
        {
            keyAndValue keyandvale = new keyAndValue();
            keyandvale.keyStr = keyStr;
            keyandvale.valueStr = valueStr;
            this.KeyAndValueListFoot.Add(keyandvale);
        }
        #endregion
        #region 生成小票
        public string Ticket()
        {
            #region 初始化
            StringBuilder ticketStr = new StringBuilder();
            #endregion
            #region 小票头部
            ticketStr.Append(SetArgPosition(this.TicketSignature, this.TicketWidth, true));
            ticketStr.Append(SetArgPosition(this.TicketTitle, this.TicketWidth, true));
            ticketStr.Append(CreateLine(this.TicketWidth, this.SignWeight));
            #endregion
            #region 小票上部内容
            for (int i = 0; i < this.KeyAndValueListTop.Count; i++)
                ticketStr.Append(this.KeyAndValueListTop[i].keyStr + this.KeyAndValueListTop[i].valueStr + "\n");
            #endregion
            #region 商品列表
            ticketStr.Append(ItemsList());
            #endregion
            #region 小票中部内容
            for (int i = 0; i < this.KeyAndValueListMid.Count; i++)
                ticketStr.Append(this.KeyAndValueListMid[i].keyStr + this.KeyAndValueListMid[i].valueStr + "\n");
            ticketStr.Append(CreateLine(this.TicketWidth, this.SignWeight));
            #endregion
            #region 小票下部内容
            for (int i = 0; i < this.KeyAndValueListFoot.Count; i++)
                ticketStr.Append(SetArgPosition(this.KeyAndValueListFoot[i].keyStr + this.KeyAndValueListFoot[i].valueStr, this._TicketWidth, false));
            #endregion
            #region 小票底部
            ticketStr.Append(SetArgPosition(this.TicketFooter, this._TicketWidth, false));
            #endregion
            return ticketStr.ToString();
        }
        #endregion
        #region 设置分隔线
        private string CreateLine(int ticketwidth, Char signChar)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ticketwidth; i++)
                result.Append(signChar);
            result.Append("\n");
            return result.ToString();
        }
        #endregion
        #region 排列表头信息
        private String ArrangeArgPosition(String arg, int ticketwidth, Decimal colPer)
        {
            StringBuilder result = new StringBuilder(arg);
            int charNum = Convert.ToInt32(TicketWidth * colPer);
            if (0 != charNum)
            {
                int argcount = System.Text.Encoding.Default.GetByteCount(arg);
                for (int i = 0; i < charNum - argcount; i++)
                    result.Append(" ");
            }
            else
            {
                result.Remove(0, result.Length);
            }
            return result.ToString();
        }
        #endregion
        #region 设置小票头部信息
        private String SetArgPosition(String arg, int ticketwidth, bool isMiddle)
        {
            StringBuilder result = new StringBuilder();
            int argnum = System.Text.Encoding.Default.GetByteCount(arg);
            if (argnum <= ticketwidth)
            {
                if (isMiddle)
                {
                    for (int i = 0; i < (ticketwidth - argnum) / 2; i++)
                        result.Append(" ");
                }
                result.Append(arg);
                result.Append("\n");
            }
            else
            {
                for (int i = 0; i <= ticketwidth / 2; i++)
                {
                    int temp = ticketwidth / 2 + i;
                    if (ticketwidth == System.Text.Encoding.Default.GetByteCount(arg.Substring(0, temp)) || ticketwidth == System.Text.Encoding.Default.GetByteCount(arg.Substring(0, temp)) - 1)
                    {
                        result.Append(arg.Substring(0, temp));
                        result.Append("\n");
                        result.Append(arg.Substring(temp, arg.Length - (temp)));
                        result.Append("\n");
                        break;
                    }
                }
            }
            return result.ToString();
        }
        #endregion
        #region 商品列表设置
        private String ItemsList()
        {
            StringBuilder result = new StringBuilder();
            if (this.DtGoodsList != null && this.DtGoodsList.Columns.Count > 0 && this.DtGoodsList.Rows.Count > 0)
            {
                result.Append(CreateLine(this._TicketWidth, this.SignWeight));
                result.Append(ArrangeArgPosition(this.DtGoodsList.Columns[1].Caption, this._TicketWidth, this.Colper1));
                result.Append(ArrangeArgPosition(this.DtGoodsList.Columns[2].Caption, this._TicketWidth, this.Colper2));
                result.Append(ArrangeArgPosition(this.DtGoodsList.Columns[3].Caption, this._TicketWidth, this.Colper3));
                result.Append(ArrangeArgPosition(this.DtGoodsList.Columns[4].Caption, this._TicketWidth, this.Colper4));
                result.Append("\n");
                result.Append(CreateLine(this.TicketWidth, this.SignLight));
                for (int i = 0; i < this.DtGoodsList.Rows.Count; i++)
                {
                        //商品名称
                    result.Append(SetArgPosition(this.DtGoodsList.Rows[i][1].ToString(), this._TicketWidth, false));
                        //商品编码
                    result.Append(ArrangeArgPosition(this.DtGoodsList.Rows[i][0].ToString(), this._TicketWidth, this.Colper1));
                        //数量
                    result.Append(ArrangeArgPosition(this.DtGoodsList.Rows[i][2].ToString(), this._TicketWidth, this.Colper2));
                        //单价
                    result.Append(ArrangeArgPosition(this.DtGoodsList.Rows[i][3].ToString(), this._TicketWidth, this.Colper3));
                        //金额
                    result.Append(ArrangeArgPosition(this.DtGoodsList.Rows[i][4].ToString(), this._TicketWidth, this.Colper4));

                    result.Append("\n");
                }
                result.Append(CreateLine(this.TicketWidth, this.SignLight));
            }
            return result.ToString();
        }
        #endregion

        #endregion

        #region 结构体
        public struct keyAndValue
        {
            public String keyStr;
            public String valueStr;
        }
        #endregion
    }
}
