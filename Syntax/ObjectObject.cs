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
    using System.Runtime.InteropServices;

    internal class ObjectObject : ScriptObject
    {
        private ClassObject class_object;
        public object Instance;
        private int invocation_index;
        private ArrayList invocation_listF;
        private ArrayList invocation_listX;
        public InstancePropertyList Properties;

        internal ObjectObject(BaseScripter scripter, ClassObject class_object) : base(scripter)
        {
            this.class_object = class_object;
            this.Properties = new InstancePropertyList(scripter);
            this.Instance = null;
            this.invocation_listX = new ArrayList();
            this.invocation_listF = new ArrayList();
            this.invocation_index = -1;
        }

        public void AddInvocation(object x, FunctionObject f)
        {
            this.invocation_listX.Add(x);
            this.invocation_listF.Add(f);
        }

        public ObjectObject Clone()
        {
            ObjectObject obj2 = new ObjectObject(base.Scripter, this.class_object);
            obj2.Instance = this.Instance;
            for (int i = 0; i < this.Properties.Count; i++)
            {
                InstanceProperty p = this.Properties[i].Clone();
                obj2.Properties.Add(p);
            }
            return obj2;
        }

        public bool FindFirstInvocation(out object x, out FunctionObject f)
        {
            if (this.invocation_listX.Count == 0)
            {
                x = null;
                f = null;
                this.invocation_index = -1;
                return false;
            }
            this.invocation_index = 0;
            x = this.invocation_listX[this.invocation_index];
            f = (FunctionObject) this.invocation_listF[this.invocation_index];
            return true;
        }

        public bool FindNextInvocation(out object x, out FunctionObject f)
        {
            this.invocation_index++;
            if (this.invocation_index >= this.invocation_listX.Count)
            {
                x = null;
                f = null;
                this.invocation_index = -1;
                return false;
            }
            x = this.invocation_listX[this.invocation_index];
            f = (FunctionObject) this.invocation_listF[this.invocation_index];
            return true;
        }

        public InstanceProperty FindProperty(int name_index)
        {
            return this.Properties.FindProperty(name_index);
        }

        public InstanceProperty FindProperty(int name_index, int type_id)
        {
            return this.Properties.FindProperty(name_index, type_id);
        }

        public object GetProperty(int name_index, int type_id)
        {
            InstanceProperty property = this.FindProperty(name_index, type_id);
            if (property == null)
            {
                property = this.FindProperty(name_index);
                if (property == null)
                {
                    string str = base.Scripter.names[name_index];
                    base.Scripter.RaiseExceptionEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { this.class_object.Name, str });
                }
            }
            if (property.Field_Info != null)
            {
                return property.Field_Info.GetValue(this.Instance);
            }
            return property.Value;
        }

        public bool HasProperty(int name_index)
        {
            return (this.Properties.FindProperty(name_index) != null);
        }

        public void PutProperty(int name_index, int type_id, object value)
        {
            InstanceProperty property = this.FindProperty(name_index, type_id);
            if (property == null)
            {
                property = this.FindProperty(name_index);
                if (property == null)
                {
                    string str = base.Scripter.names[name_index];
                    base.Scripter.RaiseExceptionEx("CS0117. '{0}' does not contain a definition for '{1}'.", new object[] { this.class_object.Name, str });
                }
            }
            if (property.Field_Info != null)
            {
                property.Field_Info.SetValue(this.Instance, value);
            }
            else
            {
                property.Value = value;
            }
        }

        public void SubInvocation(object x, FunctionObject f)
        {
            bool flag;
            do
            {
                flag = false;
                for (int i = 0; i < this.invocation_listX.Count; i++)
                {
                    object obj2 = this.invocation_listX[i];
                    FunctionObject obj3 = (FunctionObject) this.invocation_listF[i];
                    if ((x == obj2) && (f == obj3))
                    {
                        this.invocation_listX.RemoveAt(i);
                        this.invocation_listF.RemoveAt(i);
                        flag = true;
                    }
                }
            }
            while (flag);
        }

        public ClassObject Class_Object
        {
            get
            {
                return this.class_object;
            }
        }

        public bool Imported
        {
            get
            {
                return this.class_object.Imported;
            }
        }

        public int InvocationCount
        {
            get
            {
                return this.invocation_listX.Count;
            }
        }
    }
}

