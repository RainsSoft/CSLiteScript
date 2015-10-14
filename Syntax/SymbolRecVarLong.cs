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

    internal class SymbolRecVarLong : SymbolRec
    {
        private long[] v;

        internal SymbolRecVarLong(BaseScripter scripter, int id) : base(scripter, id)
        {
            this.v = new long[20];
            if (scripter.symbol_table[id].Value != null)
            {
                this.v[base.value_level] = scripter.symbol_table[id].ValueAsLong;
            }
            base.Level = scripter.symbol_table[id].Level;
            base.TypeId = scripter.symbol_table[id].TypeId;
            base.Kind = scripter.symbol_table[id].Kind;
            base.NameIndex = scripter.symbol_table[id].NameIndex;
            base.is_static = scripter.symbol_table[id].is_static;
        }

        public override void IncValueLevel()
        {
            base.value_level++;
        }

        public override object Val
        {
            get
            {
                return this.v[base.value_level];
            }
            set
            {
                this.v[base.value_level] = ConvertHelper.ToLong(value);
            }
        }

        public override object Value
        {
            get
            {
                return this.v[base.value_level];
            }
            set
            {
                this.v[base.value_level] = ConvertHelper.ToLong(value);
            }
        }

        public override long ValueAsLong
        {
            get
            {
                return this.v[base.value_level];
            }
            set
            {
                this.v[base.value_level] = value;
            }
        }
    }
}

