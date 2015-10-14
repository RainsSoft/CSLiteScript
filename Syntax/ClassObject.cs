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
namespace CSLiteScript {
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal class ClassObject : MemberObject {
        private int _namespaceNameIndex;
        internal IntegerList AncestorIds;
        public ClassKind Class_Kind;
        private Hashtable ht;
        internal Type ImportedType;
        public int IndexTypeId;
        public int MaxValueId;
        public int MinValueId;
        internal FunctionObject PatternMethod;
        public int RangeTypeId;
        internal Type RType;
        internal ClassObject UnderlyingType;

        internal ClassObject(BaseScripter scripter, int class_id, int owner_id, ClassKind ck)
            : base(scripter, class_id, owner_id) {
            this.PatternMethod = null;
            this.UnderlyingType = null;
            this.RType = null;
            this._namespaceNameIndex = -1;
            this.MinValueId = 0;
            this.MaxValueId = 0;
            this.RangeTypeId = 0;
            this.IndexTypeId = 0;
            this.ht = new Hashtable();
            this.AncestorIds = new IntegerList(false);
            this.ImportedType = null;
            this.Class_Kind = ck;
            this.PatternMethod = null;
        }

        internal void AddApplicableMethod(FunctionObject f, IntegerList a, IntegerList param_mod, int res_id, ref FunctionObject best, ref IntegerList applicable_list) {
            if (best == null) {
                best = f;
            }
            if (((f.ParamCount <= a.Count) && ((f.ParamCount >= a.Count) || (f.ParamsId != 0))) && ((res_id == 0) || base.Scripter.MatchAssignment(f.ResultId, res_id))) {
                best = f;
                if (f.ParamCount == 0) {
                    applicable_list.Add(f.Id);
                }
                else {
                    bool flag = true;
                    for (int i = 0; i < a.Count; i++) {
                        int num2 = a[i];
                        int paramId = f.GetParamId(i);
                        flag = ((int)f.GetParamMod(i) == param_mod[i]);
                        if (!flag && !f.Imported) {
                            if (base.Scripter.code.GetLanguage(base.Scripter.code.n) != CSLite_Language.VB) {
                                break;
                            }
                            flag = true;
                        }
                        flag = base.Scripter.MatchAssignment(paramId, num2);
                        if (!flag) {
                            if (base.Scripter.code.GetLanguage(base.Scripter.code.n) == CSLite_Language.VB) {
                                int typeId = base.Scripter.symbol_table[paramId].TypeId;
                                int id = base.Scripter.symbol_table[num2].TypeId;
                                ClassObject classObject = base.Scripter.GetClassObject(typeId);
                                ClassObject obj3 = base.Scripter.GetClassObject(id);
                                flag = base.Scripter.MatchTypes(classObject, obj3);
                            }
                            if (!flag) {
                                break;
                            }
                        }
                    }
                    if (flag) {
                        applicable_list.Add(f.Id);
                    }
                }
            }
        }

        internal void CompressApplicableMethodList(IntegerList a, IntegerList applicable_list) {
            while (applicable_list.Count >= 2) {
                int id = applicable_list[0];
                FunctionObject functionObject = base.Scripter.GetFunctionObject(id);
                bool flag = true;
                for (int i = 1; i < applicable_list.Count; i++) {
                    int num3 = applicable_list[i];
                    FunctionObject obj3 = base.Scripter.GetFunctionObject(num3);
                    int num4 = 0;
                    int num5 = 0;
                    for (int j = 0; j < a.Count; j++) {
                        int num7 = a[j];
                        int paramId = functionObject.GetParamId(j);
                        int num9 = obj3.GetParamId(j);
                        int num10 = base.Scripter.conversion.CompareConversions(base.Scripter, num7, paramId, num9);
                        if (num10 > 0) {
                            num4++;
                        }
                        else if (num10 < 0) {
                            num5++;
                        }
                    }
                    if ((num4 > 0) && (num5 == 0)) {
                        flag = false;
                        applicable_list.DeleteValue(num3);
                        break;
                    }
                    if ((num5 > 0) && (num4 == 0)) {
                        flag = false;
                        applicable_list.DeleteValue(id);
                        break;
                    }
                }
                if (flag) {
                    break;
                }
            }
        }

        internal ObjectObject CreateObject() {
            ObjectObject obj2 = new ObjectObject(base.Scripter, this);
            ClassObject obj3 = this;
            while (true) {
                for (int i = 0; i < obj3.Members.Count; i++) {
                    MemberObject m = obj3.Members[i];
                    if ((m.Kind == MemberKind.Field) && !m.Static) {
                        InstanceProperty p = new InstanceProperty(m, obj3.Id);
                        obj2.Properties.Add(p);
                        if (m.Imported) {
                            FieldObject obj5 = (FieldObject)m;
                            p.Field_Info = obj5.Field_Info;
                        }
                    }
                }
                ClassObject ancestorClass = obj3.AncestorClass;
                if (ancestorClass == null) {
                    return obj2;
                }
                obj3 = ancestorClass;
            }
        }

        private void FindApplicableConstructorList(IntegerList a, IntegerList param_mod, ref FunctionObject best, ref IntegerList applicable_list) {
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if ((obj2.Kind == MemberKind.Constructor) && !obj2.HasModifier(Modifier.Static)) {
                    FunctionObject f = (FunctionObject)obj2;
                    this.AddApplicableMethod(f, a, param_mod, 0, ref best, ref applicable_list);
                }
            }
            ClassObject ancestorClass = this.AncestorClass;
            if (!base.Imported) {
                if ((ancestorClass != null) && (best == null)) {
                    ancestorClass.FindApplicableConstructorList(a, param_mod, ref best, ref applicable_list);
                }
            }
            else {
                IntegerList list = new IntegerList(false);
                foreach (ConstructorInfo info in this.ImportedType.GetConstructors()) {
                    list.Add(base.Scripter.symbol_table.RegisterConstructor(info, base.Id));
                }
                if (base.Scripter.SearchProtected) {
                    foreach (ConstructorInfo info2 in this.ImportedType.GetConstructors(base.Scripter.protected_binding_flags)) {
                        list.Add(base.Scripter.symbol_table.RegisterConstructor(info2, base.Id));
                    }
                }
                for (int j = 0; j < list.Count; j++) {
                    FunctionObject functionObject = base.Scripter.GetFunctionObject(list[j]);
                    this.AddApplicableMethod(functionObject, a, param_mod, 0, ref best, ref applicable_list);
                }
                if ((ancestorClass != null) && (best == null)) {
                    ancestorClass.FindApplicableConstructorList(a, param_mod, ref best, ref applicable_list);
                }
            }
        }

        private void FindApplicableMethodList(int name_index, IntegerList a, IntegerList param_mod, int res_id, ref FunctionObject best, ref IntegerList applicable_list, bool upcase) {
            string upcaseNameByNameIndex = base.Scripter.GetUpcaseNameByNameIndex(name_index);
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if (obj2.Kind == MemberKind.Method) {
                    bool flag;
                    if (upcase) {
                        flag = upcaseNameByNameIndex == obj2.UpcaseName;
                    }
                    else {
                        flag = obj2.NameIndex == name_index;
                    }
                    if (flag) {
                        FunctionObject f = (FunctionObject)obj2;
                        this.AddApplicableMethod(f, a, param_mod, res_id, ref best, ref applicable_list);
                    }
                }
            }
            ClassObject ancestorClass = this.AncestorClass;
            if (!base.Imported) {
                if (ancestorClass != null) {
                    ancestorClass.FindApplicableMethodList(name_index, a, param_mod, res_id, ref best, ref applicable_list, upcase);
                }
            }
            else {
                string str2 = base.Scripter.names[name_index];
                IntegerList list = new IntegerList(false);
                foreach (MethodInfo info in this.ImportedType.GetMethods()) {
                    if (str2 == info.Name) {
                        bool flag2 = true;
                        foreach (Attribute attribute in Attribute.GetCustomAttributes(info)) {
                            if (attribute is CSLite_ScriptForbid) {
                                flag2 = false;
                            }
                        }
                        if (flag2) {
                            int avalue = base.Scripter.symbol_table.RegisterMethod(info, base.Id);
                            list.Add(avalue);
                        }
                    }
                }
                if (base.Scripter.SearchProtected) {
                    foreach (MethodInfo info2 in this.ImportedType.GetMethods(base.Scripter.protected_binding_flags)) {
                        if (str2 == info2.Name) {
                            bool flag3 = true;
                            foreach (Attribute attribute2 in Attribute.GetCustomAttributes(info2)) {
                                if (attribute2 is CSLite_ScriptForbid) {
                                    flag3 = false;
                                }
                            }
                            if (flag3) {
                                int num3 = base.Scripter.symbol_table.RegisterMethod(info2, base.Id);
                                list.Add(num3);
                            }
                        }
                    }
                }
                for (int j = 0; j < list.Count; j++) {
                    FunctionObject functionObject = base.Scripter.GetFunctionObject(list[j]);
                    this.AddApplicableMethod(functionObject, a, param_mod, res_id, ref best, ref applicable_list);
                }
                if (ancestorClass != null) {
                    ancestorClass.FindApplicableMethodList(name_index, a, param_mod, res_id, ref best, ref applicable_list, upcase);
                }
                for (int k = 0; k < this.AncestorIds.Count; k++) {
                    ClassObject classObject = base.Scripter.GetClassObject(this.AncestorIds[k]);
                    if (classObject.IsInterface) {
                        classObject.FindApplicableMethodList(name_index, a, param_mod, res_id, ref best, ref applicable_list, upcase);
                    }
                }
            }
        }

        internal int FindConstructorId() {
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if (obj2.Kind == MemberKind.Constructor) {
                    return obj2.Id;
                }
            }
            return 0;
        }

        internal int FindConstructorId(int param_count) {
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if ((obj2.Kind == MemberKind.Constructor) && (base.Scripter.GetFunctionObject(obj2.Id).ParamCount == param_count)) {
                    return obj2.Id;
                }
            }
            return 0;
        }

        internal int FindConstructorId(IntegerList a, IntegerList param_mod, out FunctionObject best) {
            if (a == null) {
                a = new IntegerList(false);
            }
            if (param_mod == null) {
                param_mod = new IntegerList(false);
            }
            IntegerList list = new IntegerList(false);
            best = null;
            this.FindApplicableConstructorList(a, param_mod, ref best, ref list);
            this.CompressApplicableMethodList(a, list);
            if (list.Count >= 1) {
                return list[0];
            }
            return 0;
        }

        internal int FindDestructorId(IntegerList a) {
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if (obj2.Kind == MemberKind.Destructor) {
                    return obj2.Id;
                }
            }
            return 0;
        }

        internal PropertyObject FindIndexer() {
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if (obj2.Kind == MemberKind.Property) {
                    PropertyObject obj3 = (PropertyObject)obj2;
                    if (obj3.IsIndexer) {
                        return obj3;
                    }
                }
            }
            foreach (PropertyInfo info in this.ImportedType.GetProperties()) {
                if (info.Name == "Item") {
                    bool flag = true;
                    foreach (Attribute attribute in Attribute.GetCustomAttributes(info)) {
                        if (attribute is CSLite_ScriptForbid) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        int num2 = base.Scripter.symbol_table.RegisterProperty(info, base.Id);
                        return (PropertyObject)base.Scripter.symbol_table[num2].Val;
                    }
                }
            }
            if (base.Scripter.SearchProtected) {
                foreach (PropertyInfo info2 in this.ImportedType.GetProperties(base.Scripter.protected_binding_flags)) {
                    if (info2.Name == "Item") {
                        bool flag2 = true;
                        foreach (Attribute attribute2 in Attribute.GetCustomAttributes(info2)) {
                            if (attribute2 is CSLite_ScriptForbid) {
                                flag2 = false;
                            }
                        }
                        if (flag2) {
                            int num3 = base.Scripter.symbol_table.RegisterProperty(info2, base.Id);
                            return (PropertyObject)base.Scripter.symbol_table[num3].Val;
                        }
                    }
                }
            }
            return null;
        }

        internal int FindMethodId(int name_index, IntegerList a, IntegerList param_mod, int res_id, out FunctionObject best, bool upcase) {
            if (a == null) {
                a = new IntegerList(false);
            }
            if (param_mod == null) {
                param_mod = new IntegerList(false);
            }
            IntegerList list = new IntegerList(false);
            best = null;
            this.FindApplicableMethodList(name_index, a, param_mod, res_id, ref best, ref list, upcase);
            if (list.Count > 1) {
                this.CompressApplicableMethodList(a, list);
            }
            if (list.Count >= 1) {
                int id = list[0];
                best = base.Scripter.GetFunctionObject(id);
                return id;
            }
            return 0;
        }

        internal int FindMethodId(string name, IntegerList a, IntegerList param_mod, int res_id, out FunctionObject best, bool upcase) {
            int num = base.Scripter.names.Add(name);
            return this.FindMethodId(num, a, param_mod, res_id, out best, upcase);
        }

        internal int FindOverloadableBinaryOperatorId(string operator_name, int arg1, int arg2) {
            if (base.Id > 0x10) {
                IntegerList list = new IntegerList(false);
                FunctionObject best = null;
                IntegerList a = new IntegerList(true);
                a.Add(arg1);
                a.Add(arg2);
                IntegerList list3 = new IntegerList(true);
                list3.Add(0);
                list3.Add(0);
                int num = base.Scripter.names.Add(operator_name);
                this.FindApplicableMethodList(num, a, list3, 0, ref best, ref list, false);
                this.CompressApplicableMethodList(a, list);
                if (list.Count >= 1) {
                    return list[0];
                }
            }
            return 0;
        }

        internal int FindOverloadableExplicitOperatorId(int dest_id) {
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if ((obj2.Kind == MemberKind.Method) && (obj2.Name == "op_Explicit")) {
                    FunctionObject obj3 = (FunctionObject)obj2;
                    if ((obj3.ParamCount == 1) && base.Scripter.MatchAssignment(obj3.ResultId, dest_id)) {
                        return obj3.Id;
                    }
                }
            }
            if (base.Imported) {
                foreach (MethodInfo info in this.ImportedType.GetMethods()) {
                    if (!("op_Explicit" == info.Name)) {
                        continue;
                    }
                    bool flag = true;
                    foreach (Attribute attribute in Attribute.GetCustomAttributes(info)) {
                        if (attribute is CSLite_ScriptForbid) {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) {
                        int num2 = base.Scripter.symbol_table.RegisterMethod(info, base.Id);
                        FunctionObject obj4 = (FunctionObject)base.Scripter.symbol_table[num2].Value;
                        if ((obj4.ParamCount == 1) && base.Scripter.MatchAssignment(obj4.ResultId, dest_id)) {
                            return obj4.Id;
                        }
                    }
                }
            }
            return 0;
        }

        internal int FindOverloadableImplicitOperatorId(int actual_id, int res_id) {
            bool upcase = false;
            int num = base.Scripter.names.Add("op_Implicit");
            IntegerList a = new IntegerList(false);
            a.Add(actual_id);
            IntegerList list2 = new IntegerList(false);
            list2.Add(0);
            IntegerList list3 = new IntegerList(false);
            FunctionObject best = null;
            this.FindApplicableMethodList(num, a, list2, res_id, ref best, ref list3, upcase);
            this.CompressApplicableMethodList(a, list3);
            if (list3.Count >= 1) {
                int id = list3[0];
                best = base.Scripter.GetFunctionObject(id);
                return id;
            }
            return 0;
        }

        internal int FindOverloadableUnaryOperatorId(string operator_name, int arg1) {
            if (base.Id > 0x10) {
                IntegerList list = new IntegerList(false);
                FunctionObject best = null;
                IntegerList a = new IntegerList(true);
                a.Add(arg1);
                IntegerList list3 = new IntegerList(true);
                list3.Add(0);
                int num = base.Scripter.names.Add(operator_name);
                this.FindApplicableMethodList(num, a, list3, 0, ref best, ref list, false);
                this.CompressApplicableMethodList(a, list);
                if (list.Count >= 1) {
                    return list[0];
                }
            }
            return 0;
        }

        internal override MemberObject GetMemberByNameIndex(int name_index, bool upcase) {
            MemberObject memberByNameIndex = (MemberObject)this.ht[name_index];
            if (memberByNameIndex == null) {
                memberByNameIndex = base.GetMemberByNameIndex(name_index, upcase);
                if (memberByNameIndex == null) {
                    if (!base.Imported) {
                        ClassObject ancestorClass = this.AncestorClass;
                        if (ancestorClass == null) {
                            return null;
                        }
                        memberByNameIndex = ancestorClass.GetMemberByNameIndex(name_index, upcase);
                    }
                    else {
                        if (this.ImportedType == null) {
                            return null;
                        }
                        string str = base.Scripter.names[name_index];
                        string upcaseNameByNameIndex = null;
                        if (upcase) {
                            upcaseNameByNameIndex = base.Scripter.GetUpcaseNameByNameIndex(name_index);
                        }
                        foreach (ConstructorInfo info in this.ImportedType.GetConstructors()) {
                            bool flag;
                            if (upcase) {
                                flag = upcaseNameByNameIndex == base.UpcaseName;
                            }
                            else {
                                flag = str == base.Name;
                            }
                            if (flag) {
                                foreach (Attribute attribute in Attribute.GetCustomAttributes(info)) {
                                    if (attribute is CSLite_ScriptForbid) {
                                        flag = false;
                                    }
                                }
                                if (flag) {
                                    int num = base.Scripter.symbol_table.RegisterConstructor(info, base.Id);
                                    memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num].Value;
                                }
                            }
                        }
                        foreach (MethodInfo info2 in this.ImportedType.GetMethods()) {
                            bool flag2;
                            if (upcase) {
                                flag2 = upcaseNameByNameIndex == info2.Name.ToUpper();
                            }
                            else {
                                flag2 = str == info2.Name;
                            }
                            if (flag2) {
                                foreach (Attribute attribute2 in Attribute.GetCustomAttributes(info2)) {
                                    if (attribute2 is CSLite_ScriptForbid) {
                                        flag2 = false;
                                    }
                                }
                                if (flag2) {
                                    int num2 = base.Scripter.symbol_table.RegisterMethod(info2, base.Id);
                                    memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num2].Value;
                                }
                            }
                        }
                        foreach (FieldInfo info3 in this.ImportedType.GetFields()) {
                            bool flag3;
                            if (upcase) {
                                flag3 = upcaseNameByNameIndex == info3.Name.ToUpper();
                            }
                            else {
                                flag3 = str == info3.Name;
                            }
                            if (flag3) {
                                foreach (Attribute attribute3 in Attribute.GetCustomAttributes(info3)) {
                                    if (attribute3 is CSLite_ScriptForbid) {
                                        flag3 = false;
                                    }
                                }
                                if (flag3) {
                                    int num3 = base.Scripter.symbol_table.RegisterField(info3, base.Id);
                                    memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num3].Val;
                                }
                            }
                        }
                        foreach (PropertyInfo info4 in this.ImportedType.GetProperties()) {
                            bool flag4;
                            if (upcase) {
                                flag4 = upcaseNameByNameIndex == info4.Name.ToUpper();
                            }
                            else {
                                flag4 = str == info4.Name;
                            }
                            if (flag4) {
                                foreach (Attribute attribute4 in Attribute.GetCustomAttributes(info4)) {
                                    if (attribute4 is CSLite_ScriptForbid) {
                                        flag4 = false;
                                    }
                                }
                                if (flag4) {
                                    int num4 = base.Scripter.symbol_table.RegisterProperty(info4, base.Id);
                                    memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num4].Val;
                                }
                            }
                        }
                        foreach (EventInfo info5 in this.ImportedType.GetEvents()) {
                            bool flag5;
                            if (upcase) {
                                flag5 = upcaseNameByNameIndex == info5.Name.ToUpper();
                            }
                            else {
                                flag5 = str == info5.Name;
                            }
                            if (flag5) {
                                foreach (Attribute attribute5 in Attribute.GetCustomAttributes(info5)) {
                                    if (attribute5 is CSLite_ScriptForbid) {
                                        flag5 = false;
                                    }
                                }
                                if (flag5) {
                                    int num5 = base.Scripter.symbol_table.RegisterEvent(info5, base.Id);
                                    memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num5].Val;
                                }
                            }
                        }
                        if ((memberByNameIndex == null) && base.Scripter.SearchProtected) {
                            foreach (MethodInfo info6 in this.ImportedType.GetMethods(base.Scripter.protected_binding_flags)) {
                                bool flag6;
                                if (upcase) {
                                    flag6 = upcaseNameByNameIndex == info6.Name.ToUpper();
                                }
                                else {
                                    flag6 = str == info6.Name;
                                }
                                if (flag6) {
                                    int num6 = base.Scripter.symbol_table.RegisterMethod(info6, base.Id);
                                    memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num6].Value;
                                }
                            }
                            foreach (FieldInfo info7 in this.ImportedType.GetFields(base.Scripter.protected_binding_flags)) {
                                bool flag7;
                                if (upcase) {
                                    flag7 = upcaseNameByNameIndex == info7.Name.ToUpper();
                                }
                                else {
                                    flag7 = str == info7.Name;
                                }
                                if (flag7) {
                                    foreach (Attribute attribute6 in Attribute.GetCustomAttributes(info7)) {
                                        if (attribute6 is CSLite_ScriptForbid) {
                                            flag7 = false;
                                        }
                                    }
                                    if (flag7) {
                                        int num7 = base.Scripter.symbol_table.RegisterField(info7, base.Id);
                                        memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num7].Val;
                                    }
                                }
                            }
                            foreach (PropertyInfo info8 in this.ImportedType.GetProperties(base.Scripter.protected_binding_flags)) {
                                bool flag8;
                                if (upcase) {
                                    flag8 = upcaseNameByNameIndex == info8.Name.ToUpper();
                                }
                                else {
                                    flag8 = str == info8.Name;
                                }
                                if (flag8) {
                                    foreach (Attribute attribute7 in Attribute.GetCustomAttributes(info8)) {
                                        if (attribute7 is CSLite_ScriptForbid) {
                                            flag8 = false;
                                        }
                                    }
                                    if (flag8) {
                                        int num8 = base.Scripter.symbol_table.RegisterProperty(info8, base.Id);
                                        memberByNameIndex = (MemberObject)base.Scripter.symbol_table[num8].Val;
                                    }
                                }
                            }
                        }
                        if (memberByNameIndex == null) {
                            foreach (Type type in this.ImportedType.GetInterfaces()) {
                                int num9 = base.Scripter.symbol_table.RegisterType(type, false);
                                if (this.AncestorIds.IndexOf(num9) == -1) {
                                    this.AncestorIds.Add(num9);
                                }
                            }
                        }
                        if (memberByNameIndex == null) {
                            ClassObject obj4 = this.AncestorClass;
                            if (obj4 != null) {
                                memberByNameIndex = obj4.GetMemberByNameIndex(name_index, upcase);
                            }
                            if (memberByNameIndex == null) {
                                for (int i = 0; i < this.AncestorIds.Count; i++) {
                                    ClassObject classObject = base.Scripter.GetClassObject(this.AncestorIds[i]);
                                    if (classObject.IsInterface) {
                                        memberByNameIndex = classObject.GetMemberByNameIndex(name_index, upcase);
                                        if (memberByNameIndex != null) {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (memberByNameIndex != null) {
                    this.ht.Add(name_index, memberByNameIndex);
                }
            }
            return memberByNameIndex;
        }

        internal IntegerList GetSupportedInterfaceListIds() {
            IntegerList list = new IntegerList(false);
            for (int i = 0; i < this.AncestorIds.Count; i++) {
                int id = this.AncestorIds[i];
                ClassObject val = (ClassObject)base.Scripter.GetVal(id);
                if (val.Class_Kind == ClassKind.Interface) {
                    list.Add(id);
                    IntegerList supportedInterfaceListIds = val.GetSupportedInterfaceListIds();
                    list.AddFrom(supportedInterfaceListIds);
                }
            }
            return list;
        }

        internal bool HasMethod(int name_index, int param_count) {
            for (int i = 0; i < base.Members.Count; i++) {
                MemberObject obj2 = base.Members[i];
                if (obj2.Kind == MemberKind.Method) {
                    FunctionObject obj3 = obj2 as FunctionObject;
                    if (obj3.ParamCount == param_count) {
                        return true;
                    }
                }
            }
            ClassObject ancestorClass = this.AncestorClass;
            if (!base.Imported && (ancestorClass != null)) {
                return ancestorClass.HasMethod(name_index, param_count);
            }
            string str = base.Scripter.names[name_index];
            foreach (MethodInfo info in this.ImportedType.GetMethods()) {
                if (CSLite_System.CompareStrings(str, info.Name, true) && (info.GetParameters().Length == param_count)) {
                    return true;
                }
            }
            if (base.Scripter.SearchProtected) {
                foreach (MethodInfo info2 in this.ImportedType.GetMethods(base.Scripter.protected_binding_flags)) {
                    if (CSLite_System.CompareStrings(str, info2.Name, true) && (info2.GetParameters().Length == param_count)) {
                        return true;
                    }
                }
            }
            return false;
        }

        internal bool Implements(ClassObject i) {
            return (this.InheritsFrom(i) && i.IsInterface);
        }

        public bool InheritsFrom(ClassObject a) {
            for (int i = 0; i < this.AncestorIds.Count; i++) {
                int id = this.AncestorIds[i];
                if (a.Id == id) {
                    return true;
                }
                if (base.Scripter.GetClassObject(id).InheritsFrom(a)) {
                    return true;
                }
            }
            return (((a.Imported && (this.ImportedType != null)) && !a.IsInterface) && this.ImportedType.IsSubclassOf(a.ImportedType));
        }

        internal bool IsBaseClassOf(ClassObject c) {
            return this.InheritsFrom(c);
        }

        internal bool IsOuterMemberId(int id) {
            if (this.OwnerClass == null) {
                return false;
            }
            if (this.OwnerClass.GetMember(id) == null) {
                return this.OwnerClass.IsOuterMemberId(id);
            }
            return true;
        }

        internal bool Abstract {
            get {
                return base.HasModifier(Modifier.Abstract);
            }
        }

        internal ClassObject AncestorClass {
            get {
                for (int i = 0; i < this.AncestorIds.Count; i++) {
                    int id = this.AncestorIds[i];
                    ClassObject classObject = base.Scripter.GetClassObject(id);
                    if (classObject.Class_Kind == ClassKind.Class) {
                        return classObject;
                    }
                }
                return null;
            }
        }

        internal PropertyObject DefaultProperty {
            get {
                for (int i = 0; i < base.Members.Count; i++) {
                    MemberObject obj2 = base.Members[i];
                    if ((obj2.Kind == MemberKind.Property) && !obj2.Static) {
                        PropertyObject obj3 = obj2 as PropertyObject;
                        if (obj3.IsDefault) {
                            return obj3;
                        }
                    }
                }
                return null;
            }
        }

        internal bool IsArray {
            get {
                return (this.Class_Kind == ClassKind.Array);
            }
        }

        internal bool IsClass {
            get {
                return (this.Class_Kind == ClassKind.Class);
            }
        }

        internal bool IsDelegate {
            get {
                return (this.Class_Kind == ClassKind.Delegate);
            }
        }

        internal bool IsEnum {
            get {
                return (this.Class_Kind == ClassKind.Enum);
            }
        }

        internal bool IsInterface {
            get {
                return (this.Class_Kind == ClassKind.Interface);
            }
        }

        internal bool IsNamespace {
            get {
                return (this.Class_Kind == ClassKind.Namespace);
            }
        }

        internal bool IsPascalArray {
            get {
                return ((this.RangeTypeId != 0) && (this.IndexTypeId != 0));
            }
        }

        internal bool IsReferenceType {
            get {
                return !this.IsStruct;
            }
        }

        internal bool IsRefType {
            get {
                return (CSLite_System.PosCh('&', base.Name) == (base.Name.Length - 1));
            }
        }

        internal bool IsStruct {
            get {
                return (this.Class_Kind == ClassKind.Struct);
            }
        }

        internal bool IsSubrange {
            get {
                return (this.Class_Kind == ClassKind.Subrange);
            }
        }

        internal bool IsValueType {
            get {
                return this.IsStruct;
            }
        }

        internal int MaxValue {
            get {
                if (this.IsSubrange) {
                    return base.Scripter.symbol_table[this.MaxValueId].ValueAsInt;
                }
                return 0;
            }
        }

        internal int MinValue {
            get {
                if (this.IsSubrange) {
                    return base.Scripter.symbol_table[this.MinValueId].ValueAsInt;
                }
                return 0;
            }
        }

        private string NamespaceName {
            get {
                char ch;
                return CSLite_System.ExtractOwner(base.FullName, out ch);
            }
        }

        public int NamespaceNameIndex {
            get {
                if (this._namespaceNameIndex == -1) {
                    char ch;
                    string avalue = CSLite_System.ExtractOwner(base.FullName, out ch);
                    this._namespaceNameIndex = base.Scripter.names.Add(avalue);
                }
                return this._namespaceNameIndex;
            }
        }

        internal ClassObject OwnerClass {
            get {
                if (base.OwnerId == 0) {
                    return null;
                }
                return base.Scripter.GetClassObject(base.OwnerId);
            }
        }

        internal bool Sealed {
            get {
                return base.HasModifier(Modifier.Sealed);
            }
        }
    }
}

