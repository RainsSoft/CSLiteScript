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

    public class StandardTypeList
    {
        public StringList Items = new StringList(false);

        public StandardTypeList()
        {
            this.Items.AddObject("Void", typeof(void));
            this.Items.AddObject("Boolean", typeof(bool));
            this.Items.AddObject("Byte", typeof(byte));
            this.Items.AddObject("Char", typeof(char));
            this.Items.AddObject("Decimal", typeof(decimal));
            this.Items.AddObject("Double", typeof(double));
            this.Items.AddObject("Single", typeof(float));
            this.Items.AddObject("Int32", typeof(int));
            this.Items.AddObject("Int64", typeof(long));
            this.Items.AddObject("SByte", typeof(sbyte));
            this.Items.AddObject("Int16", typeof(short));
            this.Items.AddObject("String", typeof(string));
            this.Items.AddObject("UInt32", typeof(uint));
            this.Items.AddObject("UInt64", typeof(ulong));
            this.Items.AddObject("UInt16", typeof(ushort));
            this.Items.AddObject("Object", typeof(object));
        }

        public int IndexOf(string s)
        {
            return this.Items.IndexOf(s);
        }

        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }

        public string this[int i]
        {
            get
            {
                return this.Items[i];
            }
        }
    }
}

