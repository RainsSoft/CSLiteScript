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

    internal sealed class TryStack
    {
        private ArrayList fItems = new ArrayList();

        public void Clear()
        {
            this.fItems.Clear();
        }

        public bool Legal(int n)
        {
            TryStackRec rec = (TryStackRec) this.fItems[this.Count - 1];
            return ((n >= rec.Bound1) || (n <= rec.Bound2));
        }

        public void Pop()
        {
            this.fItems.RemoveAt(this.Count - 1);
        }

        public void Push(int b1, int b2)
        {
            TryStackRec rec = new TryStackRec();
            rec.Bound1 = b1;
            rec.Bound2 = b2;
            this.fItems.Add(rec);
        }

        public int Count
        {
            get
            {
                return this.fItems.Count;
            }
        }

        private class TryStackRec
        {
            public int Bound1;
            public int Bound2;
        }
    }
}

