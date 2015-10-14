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
    using System.IO;
    using System.Reflection;

    internal sealed class SymbolTable
    {
        private ArrayList a;
        private int array_class_id;
        private int array_of_object_class_id;
        private int br_id;
        private int card;
        public int DATETIME_CLASS_id;
        private int delegate_class_id;
        private const int DELTA_SYMBOL_CARD = 0x3e8;
        private int false_id;
        private const int FIRST_SYMBOL_CARD = 0x3e8;
        private int icloneable_class_id;
        private int null_id;
        private int object_class_id;
        public int RESET_COMPILE_STAGE_CARD = 0;
        private int root_namespace_id;
        private BaseScripter scripter;
        private IntegerStack state_stack;
        public int SYSTEM_COLLECTIONS_ID;
        private int system_namespace_id;
        private int true_id;
        private int type_class_id;
        private int valuetype_class_id;

        public SymbolTable(BaseScripter scripter)
        {
            this.scripter = scripter;
            this.state_stack = new IntegerStack();
            this.a = new ArrayList();
            for (int i = 0; i < 0x3e8; i++)
            {
                this.a.Add(new SymbolRec(scripter, i));
            }
        }

        public int AppBooleanConst(bool value)
        {
            int num = this.AppVar();
            SymbolRec rec = (SymbolRec) this.a[num];
            rec.Kind = MemberKind.Const;
            rec.Value = value;
            rec.Level = 0;
            rec.TypeId = 2;
            if (value)
            {
                rec.Name = "true";
                return num;
            }
            rec.Name = "false";
            return num;
        }

        public int AppCharacterConst(char value)
        {
            int num = this.AppVar();
            SymbolRec rec = (SymbolRec) this.a[num];
            rec.Name = value.ToString();
            rec.Kind = MemberKind.Const;
            rec.Value = value;
            rec.Level = 0;
            rec.TypeId = 4;
            return num;
        }

        public int AppConst(object value, int type_id)
        {
            int num = this.AppVar();
            SymbolRec rec = this[num];
            if ((value == null) || (value == DBNull.Value))
            {
                rec.Name = "null";
            }
            else
            {
                rec.Name = value.ToString();
            }
            rec.Kind = MemberKind.Const;
            rec.Value = value;
            rec.Level = 0;
            rec.TypeId = type_id;
            return num;
        }

        public int AppConst(string name, object value, StandardType type_id)
        {
            int num = this.AppVar();
            SymbolRec rec = this[num];
            rec.Name = name;
            rec.Kind = MemberKind.Const;
            rec.Value = value;
            rec.Level = 0;
            rec.TypeId = (int) type_id;
            return num;
        }

        public int AppIntegerConst(int value)
        {
            int num = this.AppVar();
            SymbolRec rec = (SymbolRec) this.a[num];
            rec.Name = value.ToString();
            rec.Kind = MemberKind.Const;
            rec.Value = value;
            rec.Level = 0;
            rec.TypeId = 8;
            return num;
        }

        public int AppLabel()
        {
            int num = this.AppVar();
            this[num].Level = 0;
            this[num].Kind = MemberKind.Label;
            return num;
        }

        public int AppStringConst(string value)
        {
            int num = this.AppVar();
            SymbolRec rec = (SymbolRec) this.a[num];
            rec.Name = value;
            rec.Kind = MemberKind.Const;
            rec.Value = value.Substring(1, value.Length - 2);
            rec.Level = 0;
            rec.TypeId = 12;
            return num;
        }

        private int AppType(string type_name)
        {
            this.Card++;
            SymbolRec rec = (SymbolRec) this.a[this.Card];
            rec.Kind = MemberKind.Type;
            rec.Name = type_name;
            return this.Card;
        }

        public int AppVar()
        {
            this.Card++;
            SymbolRec rec = (SymbolRec) this.a[this.Card];
            rec.Kind = MemberKind.Var;
            rec.TypeId = 0;
            return this.Card;
        }

        public void DumpClasses(string FileName)
        {
        }

        public void DumpSymbolTable(string FileName)
        {
        }

        private int FindMethod(MethodInfo info)
        {
            for (int i = this.card; i >= 1; i--)
            {
                if ((this[i].Kind == MemberKind.Method) && (this.scripter.GetFunctionObject(i).Method_Info == info))
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetMethodId(string full_name)
        {
            for (int i = 1; i <= this.Card; i++)
            {
                if ((this[i].Kind == MemberKind.Method) && (this[i].FullName == full_name))
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetMethodId(string full_name, string signature)
        {
            for (int i = 1; i <= this.Card; i++)
            {
                if (((this[i].Kind == MemberKind.Method) && (this[i].FullName == full_name)) && (this.scripter.GetFunctionObject(i).Signature == signature))
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetResultId(int sub_id)
        {
            return (sub_id + 2);
        }

        public int GetThisId(int sub_id)
        {
            return (sub_id + 3);
        }

        public void Init()
        {
            this.card = -1;
            this.root_namespace_id = this.RegisterNamespace("");
            this.system_namespace_id = this.scripter.StandardTypes.Count + 1;
            this.card = this.system_namespace_id - 1;
            this.RegisterNamespace("System");
            this.card = 0;
            for (int i = 0; i < this.scripter.StandardTypes.Count; i++)
            {
                this.RegisterType((Type) this.scripter.StandardTypes.Items.Objects[i], false);
            }
            this.card = this.system_namespace_id;
            this.valuetype_class_id = this.RegisterType(typeof(ValueType), false);
            this.array_class_id = this.RegisterType(typeof(Array), false);
            this.delegate_class_id = this.RegisterType(typeof(Delegate), false);
            this.icloneable_class_id = this.RegisterType(typeof(ICloneable), false);
            this.array_of_object_class_id = this.RegisterType(typeof(object[]), false);
            this.DATETIME_CLASS_id = this.RegisterType(typeof(DateTime), false);
            this.true_id = this.AppBooleanConst(true);
            this.false_id = this.AppBooleanConst(false);
            this.null_id = this.AppVar();
            this[this.null_id].Kind = MemberKind.Const;
            this[this.null_id].TypeId = 0x10;
            this[this.null_id].Name = "null";
            string str = '"' + "\n" + '"';
            this.br_id = this.AppStringConst(str);
            for (int j = 0; j < this.scripter.StandardTypes.Count; j++)
            {
                ClassObject classObject = this.scripter.GetClassObject(j);
                this.RegisterMemberTypes((Type) this.scripter.StandardTypes.Items.Objects[j], classObject);
            }
        }

        public void LoadFromStream(BinaryReader br,  Module m, int ds, int dp)
        {
            bool flag = (ds != 0) || (dp != 0);
            for (int i = m.S1; i <= m.S2; i++)
            {
                this.Card++;
                SymbolRec rec = this[this.Card];
                rec.LoadFromStream(br);
                if (flag)
                {
                    if (m.IsInternalId(rec.Level))
                    {
                        rec.Level += ds;
                    }
                    if (m.IsInternalId(rec.TypeId))
                    {
                        rec.TypeId += ds;
                    }
                    if (rec.Kind == MemberKind.Label)
                    {
                        int val = (int) rec.Val;
                        val += dp;
                        rec.Val = val;
                    }
                }
                if (rec.Kind == MemberKind.Label)
                {
                    int num3 = (int) rec.Value;
                    rec.CodeProgRec = this.scripter.code[num3];
                }
            }
        }

        public int LookupForwardDeclaration(int id, bool upcase)
        {
            string signature = this[id].GetSignature();
            string name = this[id].Name;
            int level = this[id].Level;
            for (int i = id - 1; i >= 0; i--)
            {
                if ((this[i].IsForward && (this[i].Level == level)) && (this[i].Kind == MemberKind.Method))
                {
                    if (upcase)
                    {
                        if (CSLite_System.StrEql(this[i].Name, name) && CSLite_System.StrEql(this[i].GetSignature(), signature))
                        {
                            return i;
                        }
                    }
                    else if ((this[i].Name == name) && (this[i].GetSignature() == signature))
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        public int LookupFullName(string full_name, bool upcase)
        {
            string str = null;
            if (upcase)
            {
                str = full_name.ToUpper();
            }
            for (int i = this.Card; i >= 1; i--)
            {
                SymbolRec rec = (SymbolRec) this.a[i];
                if (upcase)
                {
                    if (rec.FullName.ToUpper() == str)
                    {
                        return i;
                    }
                }
                else if (rec.FullName == full_name)
                {
                    return i;
                }
            }
            return 0;
        }

        public int LookupID(string name, int level, bool upcase)
        {
            string str = null;
            if (upcase)
            {
                str = name.ToUpper();
            }
            for (int i = this.Card; i >= 1; i--)
            {
                SymbolRec rec = this[i];
                MemberKind kind = rec.Kind;
                if (((kind != MemberKind.Const) && (kind != MemberKind.None)) && ((kind != MemberKind.Ref) && (rec.Level == level)))
                {
                    if (upcase)
                    {
                        if (rec.Name.ToUpper() == str)
                        {
                            return i;
                        }
                    }
                    else if (rec.Name == name)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        public int LookupIDLocal(string name, int level, bool upcase)
        {
            string str = null;
            if (upcase)
            {
                str = name.ToUpper();
            }
            for (int i = this.Card; i >= 1; i--)
            {
                if (i < level)
                {
                    return 0;
                }
                SymbolRec rec = this[i];
                MemberKind kind = rec.Kind;
                if (((kind != MemberKind.Const) && (kind != MemberKind.None)) && ((kind != MemberKind.Ref) && (rec.Level == level)))
                {
                    if (upcase)
                    {
                        if (rec.Name.ToUpper() == str)
                        {
                            return i;
                        }
                    }
                    else if (rec.Name == name)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        public int LookupTypeByFullName(string full_name, bool upcase)
        {
            string str = null;
            if (upcase)
            {
                str = full_name.ToUpper();
            }
            for (int i = this.Card; i >= 1; i--)
            {
                SymbolRec rec = (SymbolRec) this.a[i];
                if (rec.Kind == MemberKind.Type)
                {
                    if (upcase)
                    {
                        if (rec.FullName.ToUpper() == str)
                        {
                            return i;
                        }
                    }
                    else if (rec.FullName == full_name)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        public int LookupTypeByName(string name, bool upcase)
        {
            string str = null;
            if (upcase)
            {
                str = name.ToUpper();
            }
            for (int i = this.Card; i >= 1; i--)
            {
                SymbolRec rec = (SymbolRec) this.a[i];
                if (rec.Kind == MemberKind.Type)
                {
                    if (upcase)
                    {
                        if (rec.Name.ToUpper() == str)
                        {
                            return i;
                        }
                    }
                    else if (rec.Name == name)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        public int RegisterConstructor(ConstructorInfo info, int type_id)
        {
            int num = this.AppVar();
            this[num].Name = info.Name;
            this[num].Level = type_id;
            this[num].Kind = MemberKind.Constructor;
            FunctionObject m = new FunctionObject(this.scripter, num, type_id);
            if (info.IsPublic)
            {
                m.Modifiers.Add(Modifier.Public);
            }
            if (info.IsStatic)
            {
                m.Modifiers.Add(Modifier.Static);
            }
            if (info.IsAbstract)
            {
                m.Modifiers.Add(Modifier.Abstract);
            }
            if (info.IsVirtual)
            {
                m.Modifiers.Add(Modifier.Virtual);
            }
            m.Imported = true;
            m.Constructor_Info = info;
            this[num].Value = m;
            int num2 = this.AppVar();
            int num3 = this.AppVar();
            if (!info.IsStatic)
            {
                this[num3].Name = "this";
            }
            foreach (ParameterInfo info2 in info.GetParameters())
            {
                int id = this.AppVar();
                this[id].Level = num;
                this[id].TypeId = this.RegisterType(info2.ParameterType, false);
                object defaultValue = null;
                if ((info2.DefaultValue != null) && (info2.DefaultValue != DBNull.Value))
                {
                    defaultValue = info2.DefaultValue;
                }
                m.AddParam(id, ParamMod.None);
            }
            m.SetupParameters();
            ((ClassObject) this[type_id].Val).AddMember(m);
            return num;
        }

        public int RegisterEvent(EventInfo info, int type_id)
        {
            int num = this.AppVar();
            this[num].Name = info.Name;
            this[num].Level = type_id;
            this[num].Kind = MemberKind.Event;
            this[num].TypeId = this.RegisterType(info.EventHandlerType, false);
            EventObject m = new EventObject(this.scripter, num, type_id);
            m.Imported = true;
            m.Event_Info = info;
            this[num].Val = m;
            ClassObject val = (ClassObject) this[type_id].Val;
            val.AddMember(m);
            m.OwnerType = val.ImportedType;
            MethodInfo addMethod = info.GetAddMethod();
            if (addMethod != null)
            {
                m.AddId = this.RegisterMethod(addMethod, type_id);
                if (addMethod.IsPublic)
                {
                    m.Modifiers.Add(Modifier.Public);
                }
                if (addMethod.IsStatic)
                {
                    m.Modifiers.Add(Modifier.Static);
                }
            }
            MethodInfo removeMethod = info.GetRemoveMethod();
            if (removeMethod != null)
            {
                m.RemoveId = this.RegisterMethod(removeMethod, type_id);
                if (removeMethod.IsPublic)
                {
                    m.Modifiers.Add(Modifier.Public);
                }
                if (removeMethod.IsStatic)
                {
                    m.Modifiers.Add(Modifier.Static);
                }
            }
            return num;
        }

        public int RegisterField(FieldInfo info, int type_id)
        {
            int num = this.AppVar();
            this[num].Name = info.Name;
            this[num].Level = type_id;
            this[num].Kind = MemberKind.Field;
            this[num].TypeId = this.RegisterType(info.FieldType, false);
            FieldObject m = new FieldObject(this.scripter, num, type_id);
            if (info.IsPublic)
            {
                m.Modifiers.Add(Modifier.Public);
            }
            if (info.IsStatic)
            {
                m.Modifiers.Add(Modifier.Static);
            }
            m.Imported = true;
            m.Field_Info = info;
            this[num].Val = m;
            ClassObject val = (ClassObject) this[type_id].Val;
            val.AddMember(m);
            m.OwnerType = val.ImportedType;
            return num;
        }

        public void RegisterInstance(string full_name, object instance, bool need_check)
        {
            if (!need_check || (this.LookupFullName(full_name, true) <= 0))
            {
                char ch;
                string str = CSLite_System.ExtractName(full_name);
                string str2 = CSLite_System.ExtractOwner(full_name, out ch);
                Type t = instance.GetType();
                int num = this.RegisterType(t, true);
                int num2 = this.RegisterNamespace(str2);
                int id = this.AppVar();
                this[id].Name = str;
                this[id].Level = num2;
                this[id].Kind = MemberKind.Var;
                this[id].TypeId = this.RegisterType(t, false);
                this[id].Val = instance;
                MemberObject m = new MemberObject(this.scripter, id, num2);
                m.Modifiers.Add(Modifier.Public);
                m.Modifiers.Add(Modifier.Static);
                ((ClassObject) this[num2].Val).AddMember(m);
            }
        }

        public void RegisterMemberTypes(Type t, ClassObject c)
        {
            foreach (ConstructorInfo info in t.GetConstructors())
            {
                foreach (ParameterInfo info2 in info.GetParameters())
                {
                    this.RegisterType(info2.ParameterType, true);
                }
            }
            foreach (MethodInfo info3 in t.GetMethods())
            {
                this.RegisterType(info3.ReturnType, false);
                foreach (ParameterInfo info4 in info3.GetParameters())
                {
                    this.RegisterType(info4.ParameterType, false);
                }
            }
            foreach (FieldInfo info5 in t.GetFields())
            {
                this.RegisterType(info5.FieldType, false);
            }
            foreach (Type type in t.GetNestedTypes(BindingFlags.Public))
            {
                int id = this.RegisterType(type, true);
                this[id].Level = c.Id;
                c.AddMember(this.scripter.GetClassObject(id));
            }
            foreach (Type type2 in t.GetInterfaces())
            {
                int avalue = this.RegisterType(type2, false);
                c.AncestorIds.Add(avalue);
            }
            if (t.IsEnum)
            {
                Type underlyingType = Enum.GetUnderlyingType(t);
                int num3 = this.RegisterType(underlyingType, false);
                c.UnderlyingType = this.scripter.GetClassObject(num3);
            }
            Type baseType = t.BaseType;
            if ((baseType != null) && (baseType != typeof(object)))
            {
                int num4 = this.RegisterType(baseType, true);
                c.AncestorIds.Add(num4);
            }
        }

        public int RegisterMethod(MethodInfo info, int type_id)
        {
            int num = this.FindMethod(info);
            if (num <= 0)
            {
                num = this.AppVar();
                this[num].Name = info.Name;
                this[num].Level = type_id;
                this[num].Kind = MemberKind.Method;
                this[num].TypeId = this.RegisterType(info.ReturnType, false);
                FunctionObject m = new FunctionObject(this.scripter, num, type_id);
                if (info.IsPublic)
                {
                    m.Modifiers.Add(Modifier.Public);
                }
                if (info.IsStatic)
                {
                    m.Modifiers.Add(Modifier.Static);
                }
                if (info.IsAbstract)
                {
                    m.Modifiers.Add(Modifier.Abstract);
                }
                if (info.IsVirtual)
                {
                    m.Modifiers.Add(Modifier.Virtual);
                }
                m.Imported = true;
                m.Method_Info = info;
                this[num].Value = m;
                this.AppVar();
                int num2 = this.AppVar();
                this[num2].Level = num;
                this[num2].TypeId = this.RegisterType(info.ReturnType, false);
                int num3 = this.AppVar();
                this[num3].Level = num;
                if (!info.IsStatic)
                {
                    this[num3].Name = "this";
                }
                bool flag = false;
                int id = 0;
                foreach (ParameterInfo info2 in info.GetParameters())
                {
                    id = this.AppVar();
                    this[id].Level = num;
                    this[id].TypeId = this.RegisterType(info2.ParameterType, false);
                    m.AddParam(id, ParamMod.None);
                    if (flag || ((info2.DefaultValue != null) && (info2.DefaultValue != DBNull.Value)))
                    {
                        int typeId = this[id].TypeId;
                        object defaultValue = info2.DefaultValue;
                        if (info2.ParameterType.IsEnum)
                        {
                            defaultValue = ConvertHelper.ToEnum(info2.ParameterType, defaultValue);
                        }
                        int num6 = this.AppConst(defaultValue, typeId);
                        m.AddDefaultValueId(id, num6);
                        flag = true;
                    }
                }
                if (id != 0)
                {
                    int num7 = this[id].TypeId;
                    if (CSLite_System.GetRank(this[num7].Name) == 1)
                    {
                        m.ParamsId = id;
                        int num8 = this[m.ParamsId].TypeId;
                        string elementTypeName = CSLite_System.GetElementTypeName(this[num8].Name);
                        int num9 = this.scripter.GetTypeId(elementTypeName);
                        m.ParamsElementId = this.AppVar();
                        this[m.ParamsElementId].TypeId = num9;
                    }
                }
                m.SetupParameters();
                ((ClassObject) this[type_id].Val).AddMember(m);
            }
            return num;
        }

        public int RegisterNamespace(string namespace_name)
        {
            int num2;
            char ch;
            int index = this.scripter.RegisteredNamespaces.IndexOf(namespace_name);
            if (index >= 0)
            {
                return (int) this.scripter.RegisteredNamespaces.Objects[index];
            }
            string str = CSLite_System.ExtractOwner(namespace_name, out ch);
            if (namespace_name == "System")
            {
                num2 = 0;
            }
            else if (str == "")
            {
                num2 = 0;
            }
            else
            {
                num2 = this.RegisterNamespace(str);
            }
            namespace_name = CSLite_System.ExtractName(namespace_name);
            int num = this.AppType(namespace_name);
            ClassObject m = new ClassObject(this.scripter, num, num2, ClassKind.Namespace);
            m.Imported = true;
            this[num].Level = num2;
            m.Modifiers.Add(Modifier.Public);
            m.Modifiers.Add(Modifier.Static);
            this[num].Value = m;
            if (namespace_name != "")
            {
                ((ClassObject) this[num2].Val).AddMember(m);
            }
            this.scripter.RegisteredNamespaces.AddObject(m.FullName, num);
            if (m.FullName == "System.Collections")
            {
                this.SYSTEM_COLLECTIONS_ID = num;
            }
            return num;
        }

        public int RegisterProperty(PropertyInfo info, int type_id)
        {
            int num = this.AppVar();
            this[num].Name = info.Name;
            this[num].Level = type_id;
            this[num].Kind = MemberKind.Property;
            this[num].TypeId = this.RegisterType(info.PropertyType, false);
            ParameterInfo[] indexParameters = info.GetIndexParameters();
            PropertyObject m = new PropertyObject(this.scripter, num, type_id, indexParameters.Length);
            m.Imported = true;
            m.Property_Info = info;
            this[num].Val = m;
            ClassObject val = (ClassObject) this[type_id].Val;
            val.AddMember(m);
            m.OwnerType = val.ImportedType;
            MethodInfo getMethod = info.GetGetMethod();
            if ((getMethod == null) && this.scripter.SearchProtected)
            {
                getMethod = info.GetGetMethod(true);
            }
            if (getMethod != null)
            {
                m.ReadId = this.RegisterMethod(getMethod, type_id);
                if (getMethod.IsPublic)
                {
                    m.Modifiers.Add(Modifier.Public);
                }
                if (getMethod.IsStatic)
                {
                    m.Modifiers.Add(Modifier.Static);
                }
            }
            MethodInfo setMethod = info.GetSetMethod();
            if ((setMethod == null) && this.scripter.SearchProtected)
            {
                setMethod = info.GetSetMethod(true);
            }
            if (setMethod != null)
            {
                m.WriteId = this.RegisterMethod(setMethod, type_id);
                if (setMethod.IsPublic)
                {
                    m.Modifiers.Add(Modifier.Public);
                }
                if (setMethod.IsStatic)
                {
                    m.Modifiers.Add(Modifier.Static);
                }
            }
            return num;
        }

        public int RegisterType(Type t, bool recursive)
        {
            int num = this.scripter.RegisteredTypes.FindRegisteredTypeId(t);
            if (num <= 0)
            {
                char ch;
                int typeByName;
                string fullName = t.FullName;
                if (fullName == null)
                {
                    return 0x10;
                }
                string str2 = CSLite_System.ExtractOwner(fullName, out ch);
                if (str2 == "System")
                {
                    typeByName = this.system_namespace_id;
                }
                else if (str2 == "")
                {
                    typeByName = this.root_namespace_id;
                }
                else if (ch == '.')
                {
                    typeByName = this.RegisterNamespace(str2);
                }
                else
                {
                    typeByName = this.LookupTypeByName(str2, false);
                    if (typeByName == 0)
                    {
                        Type type = Type.GetType(str2);
                        if (type != null)
                        {
                            typeByName = this.RegisterType(type, false);
                        }
                    }
                }
                ClassKind ck = ClassKind.Class;
                if (t.IsArray)
                {
                    ck = ClassKind.Array;
                }
                else if (t.IsEnum)
                {
                    ck = ClassKind.Enum;
                }
                else if (t.IsInterface)
                {
                    ck = ClassKind.Interface;
                }
                else if (t.IsValueType)
                {
                    ck = ClassKind.Struct;
                }
                else if (t.BaseType == typeof(MulticastDelegate))
                {
                    ck = ClassKind.Delegate;
                }
                num = this.AppType(t.Name);
                if (t == typeof(Type))
                {
                    this.type_class_id = num;
                }
                else if (t == typeof(object))
                {
                    this.object_class_id = num;
                }
                ClassObject m = new ClassObject(this.scripter, num, typeByName, ck);
                m.Modifiers.Add(Modifier.Public);
                m.Modifiers.Add(Modifier.Static);
                if (t.IsSealed)
                {
                    m.Modifiers.Add(Modifier.Sealed);
                }
                if (t.IsAbstract)
                {
                    m.Modifiers.Add(Modifier.Abstract);
                }
                m.Imported = true;
                m.ImportedType = t;
                this[num].Value = m;
                this[num].Level = typeByName;
                ((ClassObject) this[typeByName].Val).AddMember(m);
                if (t.BaseType == typeof(MulticastDelegate))
                {
                    int num3 = this.AppVar();
                    FunctionObject obj4 = new FunctionObject(this.scripter, num3, m.Id);
                    this[num3].Kind = MemberKind.Method;
                    this[num3].Level = m.Id;
                    this[num3].Val = obj4;
                    int num4 = this.AppLabel();
                    this[num4].Level = num3;
                    num4 = this.AppVar();
                    this[num4].Level = num3;
                    num4 = this.AppVar();
                    this[num4].Level = num3;
                    m.AddMember(obj4);
                    m.PatternMethod = obj4;
                }
                this.scripter.RegisteredTypes.RegisterType(t, num);
                if (recursive)
                {
                    this.RegisterMemberTypes(t, m);
                }
            }
            return num;
        }

        public void RegisterVariable(string full_name, Type t, bool need_check)
        {
            if (!need_check || (this.LookupFullName(full_name, true) <= 0))
            {
                char ch;
                string str = CSLite_System.ExtractName(full_name);
                string str2 = CSLite_System.ExtractOwner(full_name, out ch);
                int num = this.RegisterType(t, true);
                int num2 = this.RegisterNamespace(str2);
                int id = this.AppVar();
                this[id].Name = str;
                this[id].Level = num2;
                this[id].Kind = MemberKind.Var;
                this[id].TypeId = this.RegisterType(t, false);
                MemberObject m = new MemberObject(this.scripter, id, num2);
                m.Modifiers.Add(Modifier.Public);
                m.Modifiers.Add(Modifier.Static);
                ((ClassObject) this[num2].Val).AddMember(m);
            }
        }

        public void Reset()
        {
            this.state_stack.Clear();
            while (this.Card > 0)
            {
                this[this.Card] = new SymbolRec(this.scripter, this.Card);
                this.Card--;
            }
        }

        public void ResetCompileStage()
        {
            this.state_stack.Clear();
            while (this.Card > this.RESET_COMPILE_STAGE_CARD)
            {
                this[this.Card] = new SymbolRec(this.scripter, this.Card);
                this.Card--;
            }
            for (int i = this.Card; i >= 0; i--)
            {
                if (this[i].Kind == MemberKind.Type)
                {
                    this.scripter.GetClassObject(i).Members.ResetCompileStage();
                }
            }
        }

        public void RestoreState()
        {
            this.Card = this.state_stack.Pop();
        }

        public void SaveState()
        {
            this.state_stack.Push(this.Card);
        }

        public void SaveToStream(BinaryWriter bw,  Module m)
        {
            for (int i = m.S1; i <= m.S2; i++)
            {
                this[i].SaveToStream(bw);
            }
        }

        public void SetLabel(int label_id, int instruction_number)
        {
            this[label_id].Value = instruction_number;
            this[label_id].Value = this.scripter.code[instruction_number];
        }

        public void SetupFastAccessRecords()
        {
            for (int i = 1; i <= this.Card; i++)
            {
                if ((this[i].Kind == MemberKind.Const) && (this[i].TypeId == 8))
                {
                    this[i] = new SymbolRecConstInt(this.scripter, i);
                }
                else if (this[i].Kind == MemberKind.Var)
                {
                    if (this[i].TypeId == 8)
                    {
                        this[i] = new SymbolRecVarInt(this.scripter, i);
                    }
                    else if (this[i].TypeId == 2)
                    {
                        this[i] = new SymbolRecVarBool(this.scripter, i);
                    }
                    else if (this[i].TypeId == 9)
                    {
                        this[i] = new SymbolRecVarLong(this.scripter, i);
                    }
                    else if (this[i].TypeId == 7)
                    {
                        this[i] = new SymbolRecVarFloat(this.scripter, i);
                    }
                    else if (this[i].TypeId == 6)
                    {
                        this[i] = new SymbolRecVarDouble(this.scripter, i);
                    }
                    else if (this[i].TypeId == 5)
                    {
                        this[i] = new SymbolRecVarDecimal(this.scripter, i);
                    }
                    else if (this[i].TypeId == 12)
                    {
                        this[i] = new SymbolRecVarString(this.scripter, i);
                    }
                }
            }
        }

        public int ARRAY_CLASS_id
        {
            get
            {
                return this.array_class_id;
            }
        }

        public int ARRAY_OF_OBJECT_CLASS_id
        {
            get
            {
                return this.array_of_object_class_id;
            }
        }

        public int BR_id
        {
            get
            {
                return this.br_id;
            }
        }

        public int Card
        {
            get
            {
                return this.card;
            }
            set
            {
                while (value >= this.a.Count)
                {
                    int card = this.card;
                    for (int i = 0; i < 0x3e8; i++)
                    {
                        card++;
                        this.a.Add(new SymbolRec(this.scripter, card));
                    }
                }
                this.card = value;
            }
        }

        public int DELEGATE_CLASS_id
        {
            get
            {
                return this.delegate_class_id;
            }
        }

        public int FALSE_id
        {
            get
            {
                return this.false_id;
            }
        }

        public int ICLONEABLE_CLASS_id
        {
            get
            {
                return this.icloneable_class_id;
            }
        }

        public SymbolRec this[int id]
        {
            get
            {
                return (SymbolRec) this.a[id];
            }
            set
            {
                this.a[id] = value;
            }
        }

        public int NULL_id
        {
            get
            {
                return this.null_id;
            }
        }

        public int OBJECT_CLASS_id
        {
            get
            {
                return this.object_class_id;
            }
        }

        public int ROOT_NAMESPACE_id
        {
            get
            {
                return this.root_namespace_id;
            }
        }

        public int SYSTEM_NAMESPACE_id
        {
            get
            {
                return this.system_namespace_id;
            }
        }

        public int TRUE_id
        {
            get
            {
                return this.true_id;
            }
        }

        public int TYPE_CLASS_id
        {
            get
            {
                return this.type_class_id;
            }
        }

        public int VALUETYPE_CLASS_id
        {
            get
            {
                return this.valuetype_class_id;
            }
        }
    }
}

