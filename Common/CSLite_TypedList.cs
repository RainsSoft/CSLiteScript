/*
    -----------------------------------------------------------------------------
    CSLiteScript is a fast easy Script Language for dotnetframework,Mono,Unity hot fix and everywhere
    This source file is part of CSLiteScript
    For the latest info, see https://github.com/RainsSoft/CSLiteScript/
    my blog:http://blog.csdn.net/andyhebear
  
   
    Copyright (c) 2014-2030 rains soft
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
    -----------------------------------------------------------------------------
    */
namespace CSLiteScript
{
    using System;
    using System.Collections;

    public class CSLite_TypedList : IEnumerator, IEnumerable
    {
        public bool DupYes;
        private ArrayList fItems;
        private ArrayList fObjects;
        private int pos = -1;

        public CSLite_TypedList(bool dupyes)
        {
            this.DupYes = dupyes;
            this.fItems = new ArrayList();
            this.fObjects = new ArrayList();
        }

        public int Add(object avalue)
        {
            if (this.DupYes)
            {
                this.fItems.Add(avalue);
                this.fObjects.Add(null);
                return (this.fItems.Count - 1);
            }
            int index = this.fItems.IndexOf(avalue);
            if (index == -1)
            {
                this.fItems.Add(avalue);
                this.fObjects.Add(null);
                return (this.fItems.Count - 1);
            }
            return index;
        }

        public int AddObject(object avalue, object anObject)
        {
            int num = this.Add(avalue);
            this.fObjects[num] = anObject;
            return num;
        }

        public void Clear()
        {
            this.fItems.Clear();
            this.fObjects.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public int IndexOf(object value)
        {
            return this.Items.IndexOf(value);
        }

        public int Insert(int index, object avalue)
        {
            if (this.DupYes)
            {
                this.fItems.Insert(index, avalue);
                this.fObjects.Insert(index, null);
                return (this.fItems.Count - 1);
            }
            int num = this.fItems.IndexOf(avalue);
            if (num == -1)
            {
                this.fItems.Insert(index, avalue);
                this.fObjects.Insert(index, null);
                return (this.fItems.Count - 1);
            }
            return num;
        }

        public bool MoveNext()
        {
            if (this.pos < (this.Items.Count - 1))
            {
                this.pos++;
                return true;
            }
            this.Reset();
            return false;
        }

        public void RemoveAt(int i)
        {
            this.fItems.RemoveAt(i);
            this.fObjects.RemoveAt(i);
        }

        public void Reset()
        {
            this.pos = -1;
        }

        public int Count
        {
            get
            {
                return this.fItems.Count;
            }
        }

        public object Current
        {
            get
            {
                return this.Items[this.pos];
            }
        }

        public ArrayList Items
        {
            get
            {
                return this.fItems;
            }
        }

        public ArrayList Objects
        {
            get
            {
                return this.fObjects;
            }
        }
    }
}

