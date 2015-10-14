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

    internal class InstancePropertyList
    {
        private IntegerList items = new IntegerList(true);
        private BaseScripter scripter;

        public InstancePropertyList(BaseScripter scripter)
        {
            this.scripter = scripter;
        }

        public int Add(InstanceProperty p)
        {
            this.items.AddObject(p.NameIndex, p);
            return this.Count;
        }

        public InstanceProperty FindProperty(int name_index)
        {
            for (int i = 0; i < this.Count; i++)
            {
                InstanceProperty property = (InstanceProperty) this.items.Objects[i];
                if (this.items[i] == name_index)
                {
                    return property;
                }
            }
            string upcaseNameByNameIndex = this.scripter.GetUpcaseNameByNameIndex(name_index);
            for (int j = 0; j < this.Count; j++)
            {
                InstanceProperty property2 = (InstanceProperty) this.items.Objects[j];
                if (property2.UpcaseName == upcaseNameByNameIndex)
                {
                    return property2;
                }
            }
            return null;
        }

        public InstanceProperty FindProperty(int name_index, int type_id)
        {
            bool flag = false;
            for (int i = 0; i < this.Count; i++)
            {
                InstanceProperty property = (InstanceProperty) this.items.Objects[i];
                if (property.TypeId == type_id)
                {
                    flag = true;
                }
                if (flag && (this.items[i] == name_index))
                {
                    return property;
                }
            }
            string upcaseNameByNameIndex = this.scripter.GetUpcaseNameByNameIndex(name_index);
            flag = false;
            for (int j = 0; j < this.Count; j++)
            {
                InstanceProperty property2 = (InstanceProperty) this.items.Objects[j];
                if (property2.TypeId == type_id)
                {
                    flag = true;
                }
                if (flag && (property2.UpcaseName == upcaseNameByNameIndex))
                {
                    return property2;
                }
            }
            return null;
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public InstanceProperty this[int i]
        {
            get
            {
                return (InstanceProperty) this.items.Objects[i];
            }
        }
    }
}

