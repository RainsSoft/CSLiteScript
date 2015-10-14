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

    internal class IndexObject : ScriptObject
    {
        private Array array_instance;
        private int index1;
        private ArrayList indexes;
        internal int MinValue;
        private int[] p;
        private int param_count;
        private string string_instance;
        private int what;

        internal IndexObject(BaseScripter scripter, ObjectObject o) : base(scripter)
        {
            this.MinValue = 0;
            this.indexes = new ArrayList();
            if (o.Instance.GetType() == typeof(string))
            {
                this.string_instance = (string) o.Instance;
                this.what = 1;
            }
            else
            {
                this.array_instance = (Array) o.Instance;
                this.what = 2;
            }
        }

        public void AddIndex(object v)
        {
            if (this.what == 1)
            {
                this.index1 = (int) v;
            }
            else
            {
                this.indexes.Add(v);
            }
        }

        public void Setup()
        {
            if (this.what > 1)
            {
                this.param_count = this.indexes.Count;
                this.p = new int[this.param_count];
                for (int i = 0; i < this.indexes.Count; i++)
                {
                    this.p[i] = ((int) ConvertHelper.ChangeType(this.indexes[i], typeof(int))) - this.MinValue;
                }
            }
        }

        public object Value
        {
            get
            {
                if (this.what == 1)
                {
                    return this.string_instance[this.index1];
                }
                return this.array_instance.GetValue(this.p);
            }
            set
            {
                if (this.what == 2)
                {
                    if (value.GetType() == typeof(ObjectObject))
                    {
                        if ((value as ObjectObject).Imported)
                        {
                            this.array_instance.SetValue(((ObjectObject) value).Instance, this.p);
                        }
                        else
                        {
                            this.array_instance.SetValue(value, this.p);
                        }
                    }
                    else
                    {
                        this.array_instance.SetValue(value, this.p);
                    }
                }
            }
        }
    }
}

