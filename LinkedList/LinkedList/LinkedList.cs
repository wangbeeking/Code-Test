using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList<T>
    {
        #region 开始和结束
        #region RBegin
        private LinkedNode<T> _RBegin = new LinkedNode<T>();
        public LinkedNode<T> RBegin
        {
            get { return _RBegin; }
            private set { _RBegin = value; }
        }
        #endregion
        #region Begin
        private LinkedNode<T> _Begin = null;
        public LinkedNode<T> Begin
        {
            get { return _Begin; }
            private set { _Begin = value; }
        }
        #endregion
        #region End
        private LinkedNode<T> _End = null;
        public LinkedNode<T> End
        {
            get { return _End; }
            private set { _End = value; }
        }
        #endregion
        #region REnd
        private LinkedNode<T> _REnd = new LinkedNode<T>();
        public LinkedNode<T> REnd
        {
            get { return _REnd; }
            private set { _REnd = value; }
        }
        #endregion
        #endregion
        #region 方法

        #endregion
        public LinkedNode<T> Add(LinkedNode<T> Node)
        {
            throw new NotImplementedException();
        }

        public LinkedNode<T> Add(T Data)
        {
            throw new NotImplementedException();
        }

        public LinkedNode<T> Clean()
        {
            throw new NotImplementedException();
        }

        public LinkedNode<T> Delete(LinkedNode<T> Node)
        {
            throw new NotImplementedException();
        }

        public LinkedNode<T> Delete(T Data)
        {
            throw new NotImplementedException();
        }

        public LinkedNode<T> Empty()
        {
            throw new NotImplementedException();
        }
    }
    public class LinkedNode<T>
    {
        #region 全局字段
        private static ulong _Length = 0;
        public ulong Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
        #endregion

        #region 字段
        private T _Data;
        public T Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        private LinkedNode<T> _Next;
        public LinkedNode<T> Next
        {
            get { return _Next; }
            set { _Next = value; }
        }
        #endregion

        #region 构造/析构
        public LinkedNode()
        {
            ++Length;
        }
        public LinkedNode(T data)
        {
            ++Length;
            Data = data;
        }
        public LinkedNode(LinkedNode<T> Node)
        {
            ++Length;
            Data = Node.Data;
            Next = Node.Next;
        }
        ~LinkedNode()
        {
            --Length;
            Next = null;
        }
        #endregion
    }
}
