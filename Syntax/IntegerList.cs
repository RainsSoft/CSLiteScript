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
    using System.Reflection;

    public class IntegerList : CSLite_TypedList
    {
        public IntegerList(bool dupyes) : base(dupyes)
        {
        }

        public int Add(int avalue)
        {
            return base.Add(avalue);
        }

        public void AddFrom(IntegerList l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                base.AddObject(l.Items[i], l.Objects[i]);
            }
        }

        public int AddObject(int avalue, object anObject)
        {
            return base.AddObject(avalue, anObject);
        }

        public IntegerList Clone()
        {
            IntegerList list = new IntegerList(base.DupYes);
            for (int i = 0; i < base.Count; i++)
            {
                list.Add(this[i]);
            }
            return list;
        }

        public void DeleteValue(int avalue)
        {
            int index = this.IndexOf(avalue);
            if (index != -1)
            {
                base.Items.RemoveAt(index);
            }
        }

        public int IndexOf(int value)
        {
            return base.Items.IndexOf(value);
        }

        public int Insert(int index, int avalue)
        {
            return base.Insert(index, avalue);
        }

        public int this[int i]
        {
            get
            {
                return (int) base.Items[i];
            }
            set
            {
                base.Items[i] = value;
            }
        }

        public int Last
        {
            get
            {
                return this[base.Count - 1];
            }
            set
            {
                this[base.Count - 1] = value;
            }
        }
    }
}

