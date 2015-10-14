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
    using System.IO;

    public sealed class ProgRec
    {
        public int arg1 = 0;
        public int arg2 = 0;
        public int FinalNumber = 0;
        public int op = 0;
        public int res = 0;
        public int tag;
        public Upcase upcase;

        internal ProgRec Clone()
        {
            ProgRec rec = new ProgRec();
            rec.op = this.op;
            rec.arg1 = this.arg1;
            rec.arg2 = this.arg2;
            rec.res = this.res;
            rec.upcase = this.upcase;
            return rec;
        }

        internal void LoadFromStream(BinaryReader br)
        {
            this.op = br.ReadInt32();
            this.arg1 = br.ReadInt32();
            this.arg2 = br.ReadInt32();
            this.res = br.ReadInt32();
            this.upcase = (Upcase) br.ReadInt32();
        }

        internal void Reset()
        {
            this.op = 0;
            this.arg1 = 0;
            this.arg2 = 0;
            this.res = 0;
            this.FinalNumber = 0;
            this.tag = 0;
            this.upcase = Upcase.None;
        }

        internal void SaveToStream(BinaryWriter bw)
        {
            bw.Write(this.op);
            bw.Write(this.arg1);
            bw.Write(this.arg2);
            bw.Write(this.res);
            bw.Write((int) this.upcase);
        }
    }
}

