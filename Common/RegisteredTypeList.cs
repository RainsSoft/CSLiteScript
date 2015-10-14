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
    using System.Reflection;

    public class RegisteredTypeList
    {
        private Hashtable ht = new Hashtable();
        private StringList Items = new StringList(false);

        public void Clear()
        {
            this.Items.Clear();
            this.ht.Clear();
        }

        public RegisteredTypeList Clone()
        {
            RegisteredTypeList list = new RegisteredTypeList();
            for (int i = 0; i < this.Count; i++)
            {
                list.RegisterType(this[i].T, this[i].Id);
            }
            return list;
        }

        public void Delete(int i)
        {
            RegisteredType type = this[i];
            this.ht.Remove(type.T);
            this.Items.Delete(i);
        }

        public int FindRegisteredTypeId(Type t)
        {
            object obj2 = this.ht[t];
            if (obj2 == null)
            {
                return 0;
            }
            return (int) obj2;
        }

        public void RegisterType(Type t, int type_id)
        {
            RegisteredType anObject = new RegisteredType();
            anObject.T = t;
            anObject.Id = type_id;
            this.Items.AddObject(t.FullName, anObject);
            this.ht.Add(t, type_id);
        }

        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }

        public RegisteredType this[int i]
        {
            get
            {
                return (RegisteredType) this.Items.Objects[i];
            }
        }
    }
}

