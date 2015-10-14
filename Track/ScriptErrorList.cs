﻿/*
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
    using System.Reflection;

    public class ScriptErrorList : IEnumerator, IEnumerable
    {
        private ArrayList items = new ArrayList();
        private int pos = -1;

        public int Add(ScriptError e)
        {
            return this.items.Add(e);
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (this.pos < (this.items.Count - 1))
            {
                this.pos++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            this.pos = 0;
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public object Current
        {
            get
            {
                return this.items[this.pos];
            }
        }

        public ScriptError this[int i]
        {
            get
            {
                return (this.items[i] as ScriptError);
            }
        }
    }
}

