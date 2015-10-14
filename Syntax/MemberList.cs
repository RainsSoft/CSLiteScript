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

    internal class MemberList
    {
        private ArrayList items;
        private BaseScripter scripter;

        public MemberList(BaseScripter scripter)
        {
            this.scripter = scripter;
            this.items = new ArrayList();
        }

        public int Add(MemberObject m)
        {
            this.items.Add(m);
            return this.Count;
        }

        public void Delete(int i)
        {
            this.items.RemoveAt(i);
        }

        public void ResetCompileStage()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this[i].Id > this.scripter.symbol_table.RESET_COMPILE_STAGE_CARD)
                {
                    this.Delete(i);
                }
            }
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public MemberObject this[int i]
        {
            get
            {
                return (MemberObject) this.items[i];
            }
        }
    }
}
