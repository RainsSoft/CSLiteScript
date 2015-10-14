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

    public sealed class CallStackRec
    {
        private FunctionObject f;
        private int n;
        private ArrayList p = new ArrayList();
        private BaseScripter scripter;

        internal CallStackRec(BaseScripter scripter, FunctionObject f, int n)
        {
            this.scripter = scripter;
            this.n = n;
            this.f = f;
        }

        public string CallView
        {
            get
            {
                string str = this.Name + "(";
                for (int i = 0; i < this.Parameters.Count; i++)
                {
                    str = str + this.Parameters[i].ToString();
                    if (i < (this.Parameters.Count - 1))
                    {
                        str = str + ",";
                    }
                }
                return (str + ")");
            }
        }

        public string FullName
        {
            get
            {
                return this.f.FullName;
            }
        }

        public int LineNumber
        {
            get
            {
                return this.scripter.GetLineNumber(this.n);
            }
        }

        public string ModuleName
        {
            get
            {
                Module module = this.scripter.GetModule(this.n);
                if (module == null)
                {
                    return "";
                }
                return module.Name;
            }
        }

        internal int N
        {
            get
            {
                return this.n;
            }
        }

        public string Name
        {
            get
            {
                return this.f.Name;
            }
        }

        public ArrayList Parameters
        {
            get
            {
                return this.p;
            }
        }

        internal int SubId
        {
            get
            {
                return this.f.Id;
            }
        }
    }
}

