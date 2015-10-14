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

    internal class FunctionObject : MemberObject
    {
        public ConstructorInfo Constructor_Info;
        public IntegerList Default_Ids;
        public ClassObject ExplicitInterface;
        private ProgRec init;
        private int low_bound;
        public MethodInfo Method_Info;
        public IntegerList Param_Ids;
        public IntegerList Param_Mod;
        public object[] Params;
        private int params_element_id;
        private int params_id;
        private int res_id;
        public int SignatureIndex;
        private int this_id;

        internal FunctionObject(BaseScripter scripter, int sub_id, int owner_id) : base(scripter, sub_id, owner_id)
        {
            this.init = null;
            this.params_id = 0;
            this.params_element_id = 0;
            this.low_bound = 0;
            this.ExplicitInterface = null;
            this.SignatureIndex = -1;
            this.Param_Ids = new IntegerList(false);
            this.Param_Mod = new IntegerList(true);
            this.Default_Ids = new IntegerList(true);
            this.res_id = scripter.symbol_table.GetResultId(sub_id);
            this.this_id = scripter.symbol_table.GetThisId(sub_id);
            this.Constructor_Info = null;
        }

        public void AddDefaultValueId(int param_id, int default_value_id)
        {
            for (int i = 0; i < this.Param_Ids.Count; i++)
            {
                if (this.Param_Ids[i] == param_id)
                {
                    this.Default_Ids[i] = default_value_id;
                }
            }
        }

        public void AddParam(int id, ParamMod mod)
        {
            this.Param_Ids.Add(id);
            this.Param_Mod.Add(mod);
            this.Default_Ids.Add(0);
        }

        public void AllocateSub()
        {
            SymbolTable table = base.Scripter.symbol_table;
            for (int i = base.Id; i <= this.low_bound; i++)
            {
                SymbolRec rec = table[i];
                if ((rec.Level == base.Id) && !rec.is_static)
                {
                    rec.IncValueLevel();
                }
            }
        }

        public static bool CompareHeaders(FunctionObject fx, FunctionObject fy)
        {
            int resultId;
            int num2;
            int typeId;
            int num4;
            string name;
            string str2;
            if (fx.ParamCount != fy.ParamCount)
            {
                return false;
            }
            SymbolTable table = fx.Scripter.symbol_table;
            bool flag = true;
            for (int i = 0; i < fx.ParamCount; i++)
            {
                resultId = fx.Param_Ids[i];
                num2 = fy.Param_Ids[i];
                typeId = table[resultId].TypeId;
                num4 = table[num2].TypeId;
                name = table[typeId].Name;
                str2 = table[num4].Name;
                flag &= name == str2;
            }
            resultId = fx.ResultId;
            num2 = fy.ResultId;
            typeId = table[resultId].TypeId;
            num4 = table[num2].TypeId;
            name = table[typeId].Name;
            str2 = table[num4].Name;
            return (flag & (name == str2));
        }

        public void CreateSignature()
        {
            string avalue = "(";
            for (int i = 0; i < this.ParamCount; i++)
            {
                int typeId = base.Scripter.symbol_table[this.Param_Ids[i]].TypeId;
                string name = base.Scripter.symbol_table[typeId].Name;
                avalue = avalue + name;
                if (i != (this.ParamCount - 1))
                {
                    avalue = avalue + ",";
                }
            }
            avalue = avalue + ")";
            this.SignatureIndex = base.Scripter.names.Add(avalue);
        }

        public void DeallocateSub()
        {
            SymbolTable table = base.Scripter.symbol_table;
            for (int i = base.Id; i <= this.low_bound; i++)
            {
                SymbolRec rec = table[i];
                if ((rec.Level == base.Id) && !rec.is_static)
                {
                    rec.DecValueLevel();
                }
            }
        }

        public FunctionObject GetLateBindingFunction(object v, int type_id, bool upcase)
        {
            ClassObject classObject;
            FunctionObject obj2 = this;
            int nameIndex = base.NameIndex;
            if (base.Static)
            {
                classObject = (ClassObject) v;
            }
            else
            {
                classObject = base.Scripter.GetClassObject(type_id);
            }
            string str = base.Name.ToUpper();
            for (int i = 0; i < classObject.Members.Count; i++)
            {
                bool flag;
                MemberObject obj4 = classObject.Members[i];
                if (base.Static)
                {
                    flag = obj4.HasModifier(Modifier.Static);
                }
                else
                {
                    flag = !obj4.HasModifier(Modifier.Static);
                }
                flag = flag && (obj4.Kind == MemberKind.Method);
                if (upcase)
                {
                    if (str == obj4.Name.ToUpper())
                    {
                        FunctionObject obj5 = obj4 as FunctionObject;
                        if (this.SignatureIndex == obj5.SignatureIndex)
                        {
                            return obj5;
                        }
                    }
                }
                else if (nameIndex == obj4.NameIndex)
                {
                    FunctionObject obj6 = obj4 as FunctionObject;
                    if (this.SignatureIndex == obj6.SignatureIndex)
                    {
                        return obj6;
                    }
                }
            }
            return obj2;
        }

        public int GetParamId(int param_number)
        {
            if (param_number < this.ParamCount)
            {
                return this.Param_Ids[param_number];
            }
            return this.params_element_id;
        }

        public ParamMod GetParamMod(int param_number)
        {
            if (param_number < this.ParamCount)
            {
                return (ParamMod) this.Param_Mod[param_number];
            }
            return (ParamMod) this.Param_Mod[this.ParamCount - 1];
        }

        public int GetParamTypeId(int param_number)
        {
            int id = this.Param_Ids[param_number];
            return base.Scripter.GetTypeId(id);
        }

        public object GetParamValue(int param_number)
        {
            if (base.Imported)
            {
                return this.Params[param_number];
            }
            int id = this.Param_Ids[param_number];
            base.Scripter.symbol_table[id].IncValueLevel();
            object val = base.Scripter.code.GetVal(id);
            base.Scripter.symbol_table[id].DecValueLevel();
            return val;
        }

        public bool HasParameter(int id)
        {
            for (int i = 0; i < this.ParamCount; i++)
            {
                if (this.Param_Ids[i] == id)
                {
                    return true;
                }
            }
            return false;
        }

        public object InvokeConstructor()
        {
            return this.Constructor_Info.Invoke(this.Params);
        }

        public object InvokeMethod(object obj)
        {
            return this.Method_Info.Invoke(obj, this.Params);
        }

        public void PutParam(int param_number, object value)
        {
            int id = this.Param_Ids[param_number];
            base.Scripter.code.PutVal(id, value);
        }

        public void PutThis(object value)
        {
            base.Scripter.code.PutVal(this.this_id, value);
        }

        public bool RequiresLateBinding()
        {
            return ((base.Scripter.code.GetClassObject(base.OwnerId).Class_Kind == ClassKind.Interface) || (base.HasModifier(Modifier.Virtual) || base.HasModifier(Modifier.Override)));
        }

        public void SetupLowBound(int id)
        {
            this.low_bound = id;
        }

        public void SetupParameters()
        {
            this.Params = new object[this.ParamCount];
        }

        public int DefaultParamCount
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.Default_Ids.Count; i++)
                {
                    if (this.Default_Ids[i] != 0)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public ProgRec Init
        {
            get
            {
                return this.init;
            }
            set
            {
                this.init = value;
            }
        }

        public ClassObject Owner
        {
            get
            {
                return base.Scripter.GetClassObject(base.OwnerId);
            }
        }

        public int ParamCount
        {
            get
            {
                return this.Param_Ids.Count;
            }
        }

        public int ParamsElementId
        {
            get
            {
                return this.params_element_id;
            }
            set
            {
                this.params_element_id = value;
            }
        }

        public int ParamsId
        {
            get
            {
                return this.params_id;
            }
            set
            {
                this.params_id = value;
            }
        }

        public int ResultId
        {
            get
            {
                return this.res_id;
            }
        }

        public string Signature
        {
            get
            {
                if (this.SignatureIndex == -1)
                {
                    return "unknown";
                }
                return base.Scripter.names[this.SignatureIndex];
            }
        }

        public int ThisId
        {
            get
            {
                return this.this_id;
            }
        }
    }
}

