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

    internal class PropertyObject : MemberObject
    {
        public bool IsDefault;
        public Type OwnerType;
        private object[] p;
        public int ParamCount;
        public PropertyInfo Property_Info;
        public int ReadId;
        private object value;
        public int WriteId;

        internal PropertyObject(BaseScripter scripter, int property_id, int owner_id, int param_count) : base(scripter, property_id, owner_id)
        {
            this.Property_Info = null;
            this.OwnerType = null;
            this.ReadId = 0;
            this.WriteId = 0;
            this.ParamCount = 0;
            this.IsDefault = false;
            this.ParamCount = param_count;
            this.p = new object[this.ParamCount];
        }

        internal bool IsIndexer
        {
            get
            {
                return (this.ParamCount > 0);
            }
        }

        internal object Value
        {
            get
            {
                if (base.Imported && base.Static)
                {
                    return this.Property_Info.GetValue(this.OwnerType, this.p);
                }
                return this.value;
            }
            set
            {
                if (base.Imported && base.Static)
                {
                    this.Property_Info.SetValue(this.OwnerType, value, this.p);
                }
                else
                {
                    this.value = value;
                }
            }
        }
    }
}

