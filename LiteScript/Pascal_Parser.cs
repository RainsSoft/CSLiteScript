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

    internal class Pascal_Parser : BaseParser
    {
        private ModifierList class_modifiers;
        private ModifierList constructor_modifiers;
        private ModifierList delegate_modifiers;
        private ModifierList enum_modifiers;
        private ModifierList event_modifiers;
        private bool has_constructor = false;
        private CSLite_Types integral_types;
        private ModifierList interface_modifiers;
        private IntegerList local_variables;
        private ModifierList method_modifiers;
        private IntegerList param_ids;
        private IntegerList param_mods;
        private IntegerList param_type_ids;
        private bool prefix = false;
        private ModifierList property_modifiers;
        private IntegerList static_variable_initializers;
        private ModifierList structure_modifiers;
        private StringList total_modifier_list;
        private bool valid_this_context = false;
        private IntegerList variable_initializers;

        public Pascal_Parser()
        {
            base.language = "Pascal";
            base.scanner = new Pascal_Scanner(this);
            base.upcase = true;
            base.AllowKeywordsInMemberAccessExpressions = true;
            this.variable_initializers = new IntegerList(false);
            this.static_variable_initializers = new IntegerList(false);
            this.param_ids = new IntegerList(true);
            this.param_type_ids = new IntegerList(true);
            this.param_mods = new IntegerList(true);
            this.local_variables = new IntegerList(false);
            this.total_modifier_list = new StringList(false);
            this.total_modifier_list.AddObject("Public", Modifier.Public);
            this.total_modifier_list.AddObject("Protected", Modifier.Protected);
            this.total_modifier_list.AddObject("Internal", Modifier.Internal);
            this.total_modifier_list.AddObject("Private", Modifier.Private);
            this.total_modifier_list.AddObject("Shared", Modifier.Static);
            this.total_modifier_list.AddObject("Override", Modifier.Override);
            this.total_modifier_list.AddObject("Overloads", Modifier.Overloads);
            this.total_modifier_list.AddObject("ReadOnly", Modifier.ReadOnly);
            this.total_modifier_list.AddObject("Shadows", Modifier.Shadows);
            base.keywords.Add("and");
            base.keywords.Add("as");
            base.keywords.Add("begin");
            base.keywords.Add("break");
            base.keywords.Add("case");
            base.keywords.Add("char");
            base.keywords.Add("class");
            base.keywords.Add("const");
            base.keywords.Add("constructor");
            base.keywords.Add("continue");
            base.keywords.Add("decimal");
            base.keywords.Add("default");
            base.keywords.Add("delegate");
            base.keywords.Add("destructor");
            base.keywords.Add("do");
            base.keywords.Add("double");
            base.keywords.Add("downto");
            base.keywords.Add("each");
            base.keywords.Add("else");
            base.keywords.Add("end");
            base.keywords.Add("exit");
            base.keywords.Add("false");
            base.keywords.Add("finally");
            base.keywords.Add("for");
            base.keywords.Add("forward");
            base.keywords.Add("function");
            base.keywords.Add("goto");
            base.keywords.Add("if");
            base.keywords.Add("in");
            base.keywords.Add("integer");
            base.keywords.Add("interface");
            base.keywords.Add("implementation");
            base.keywords.Add("initialization");
            base.keywords.Add("finalization");
            base.keywords.Add("is");
            base.keywords.Add("mod");
            base.keywords.Add("namespace");
            base.keywords.Add("nil");
            base.keywords.Add("not");
            base.keywords.Add("object");
            base.keywords.Add("of");
            base.keywords.Add("on");
            base.keywords.Add("or");
            base.keywords.Add("else");
            base.keywords.Add("override");
            base.keywords.Add("private");
            base.keywords.Add("program");
            base.keywords.Add("procedure");
            base.keywords.Add("property");
            base.keywords.Add("protected");
            base.keywords.Add("public");
            base.keywords.Add("read");
            base.keywords.Add("record");
            base.keywords.Add("repeat");
            base.keywords.Add("set");
            base.keywords.Add("short");
            base.keywords.Add("single");
            base.keywords.Add("static");
            base.keywords.Add("string");
            base.keywords.Add("then");
            base.keywords.Add("to");
            base.keywords.Add("true");
            base.keywords.Add("try");
            base.keywords.Add("type");
            base.keywords.Add("uses");
            base.keywords.Add("unit");
            base.keywords.Add("until");
            base.keywords.Add("var");
            base.keywords.Add("variant");
            base.keywords.Add("while");
            base.keywords.Add("with");
            base.keywords.Add("write");
            base.keywords.Add("xor");
            base.keywords.Add("print");
            base.keywords.Add("println");
            this.enum_modifiers = new ModifierList();
            this.enum_modifiers.Add(Modifier.New);
            this.enum_modifiers.Add(Modifier.Public);
            this.enum_modifiers.Add(Modifier.Protected);
            this.enum_modifiers.Add(Modifier.Internal);
            this.enum_modifiers.Add(Modifier.Private);
            this.class_modifiers = new ModifierList();
            this.class_modifiers.Add(Modifier.New);
            this.class_modifiers.Add(Modifier.Public);
            this.class_modifiers.Add(Modifier.Protected);
            this.class_modifiers.Add(Modifier.Internal);
            this.class_modifiers.Add(Modifier.Private);
            this.class_modifiers.Add(Modifier.Abstract);
            this.class_modifiers.Add(Modifier.Sealed);
            this.class_modifiers.Add(Modifier.Friend);
            this.structure_modifiers = new ModifierList();
            this.structure_modifiers.Add(Modifier.Public);
            this.structure_modifiers.Add(Modifier.Protected);
            this.structure_modifiers.Add(Modifier.Internal);
            this.structure_modifiers.Add(Modifier.Private);
            this.structure_modifiers.Add(Modifier.Friend);
            this.structure_modifiers.Add(Modifier.New);
            this.interface_modifiers = new ModifierList();
            this.interface_modifiers.Add(Modifier.New);
            this.interface_modifiers.Add(Modifier.Public);
            this.interface_modifiers.Add(Modifier.Protected);
            this.interface_modifiers.Add(Modifier.Internal);
            this.interface_modifiers.Add(Modifier.Private);
            this.interface_modifiers.Add(Modifier.Friend);
            this.event_modifiers = new ModifierList();
            this.event_modifiers.Add(Modifier.Public);
            this.event_modifiers.Add(Modifier.Protected);
            this.event_modifiers.Add(Modifier.Internal);
            this.event_modifiers.Add(Modifier.Private);
            this.event_modifiers.Add(Modifier.New);
            this.event_modifiers.Add(Modifier.Static);
            this.method_modifiers = new ModifierList();
            this.method_modifiers.Add(Modifier.Public);
            this.method_modifiers.Add(Modifier.Protected);
            this.method_modifiers.Add(Modifier.Internal);
            this.method_modifiers.Add(Modifier.Private);
            this.method_modifiers.Add(Modifier.New);
            this.method_modifiers.Add(Modifier.Static);
            this.method_modifiers.Add(Modifier.Virtual);
            this.method_modifiers.Add(Modifier.Sealed);
            this.method_modifiers.Add(Modifier.Abstract);
            this.method_modifiers.Add(Modifier.Override);
            this.method_modifiers.Add(Modifier.Overloads);
            this.method_modifiers.Add(Modifier.Friend);
            this.method_modifiers.Add(Modifier.Shadows);
            this.property_modifiers = this.method_modifiers.Clone();
            this.property_modifiers.Add(Modifier.Default);
            this.property_modifiers.Add(Modifier.ReadOnly);
            this.property_modifiers.Add(Modifier.WriteOnly);
            this.constructor_modifiers = new ModifierList();
            this.constructor_modifiers.Add(Modifier.Public);
            this.constructor_modifiers.Add(Modifier.Protected);
            this.constructor_modifiers.Add(Modifier.Internal);
            this.constructor_modifiers.Add(Modifier.Private);
            this.constructor_modifiers.Add(Modifier.Static);
            this.constructor_modifiers.Add(Modifier.Friend);
            this.delegate_modifiers = new ModifierList();
            this.delegate_modifiers.Add(Modifier.Public);
            this.delegate_modifiers.Add(Modifier.Protected);
            this.delegate_modifiers.Add(Modifier.Internal);
            this.delegate_modifiers.Add(Modifier.Private);
            this.delegate_modifiers.Add(Modifier.Static);
            this.delegate_modifiers.Add(Modifier.Friend);
            this.delegate_modifiers.Add(Modifier.Shadows);
            this.integral_types = new CSLite_Types();
            this.integral_types.Add("Byte", StandardType.Byte);
            this.integral_types.Add("Short", StandardType.Short);
            this.integral_types.Add("Integer", StandardType.Int);
            this.integral_types.Add("Long", StandardType.Long);
        }

        public virtual int BeginMethod(int sub_id, MemberKind k, ModifierList ml, int res_type_id)
        {
            if (base.IsCurrText('.'))
            {
                int typeID = base.LookupTypeID(base.GetName(sub_id));
                if (typeID == 0)
                {
                    base.RaiseErrorEx(true, "Undeclared identifier '{0}'", new object[] { base.GetName(sub_id) });
                }
                this.Gen(base.code.OP_BEGIN_USING, typeID, 0, 0);
                base.level_stack.Push(typeID);
                this.Match('.');
                sub_id = this.Parse_Ident();
                this.prefix = true;
            }
            base.BeginMethod(sub_id, k, ml, res_type_id);
            return sub_id;
        }

        public override void Call_SCANNER()
        {
            base.Call_SCANNER();
            if (base.IsCurrText("true"))
            {
                base.curr_token.tokenClass = TokenClass.BooleanConst;
                base.curr_token.id = base.TRUE_id;
            }
            else if (base.IsCurrText("false"))
            {
                base.curr_token.tokenClass = TokenClass.BooleanConst;
                base.curr_token.id = base.FALSE_id;
            }
        }

        private void CreateDefaultConstructor(int class_id, bool is_struct)
        {
            ModifierList ml = new ModifierList();
            ml.Add(Modifier.Public);
            int id = base.NewVar();
            base.SetName(id, base.GetName(class_id));
            this.BeginMethod(id, MemberKind.Constructor, ml, 1);
            this.InitMethod(id);
            for (int i = 0; i < this.variable_initializers.Count; i++)
            {
                int num3 = this.variable_initializers[i];
                this.Gen(base.code.OP_BEGIN_CALL, num3, 0, 0);
                this.Gen(base.code.OP_PUSH, base.CurrThisID, 0, 0);
                this.Gen(base.code.OP_CALL, num3, 0, 0);
            }
            if (!is_struct)
            {
                int num4 = base.NewVar();
                int res = base.NewVar();
                this.Gen(base.code.OP_EVAL_BASE_TYPE, class_id, num4, res);
                int num6 = base.NewVar();
                this.Gen(base.code.OP_CAST, res, base.CurrThisID, num6);
                this.Gen(base.code.OP_BEGIN_CALL, num4, 0, 0);
                this.Gen(base.code.OP_PUSH, num6, 0, 0);
                this.Gen(base.code.OP_CALL, num4, 0, 0);
            }
            this.EndMethod(id);
        }

        private void CreateDefaultStaticConstructor(int class_id)
        {
            ModifierList ml = new ModifierList();
            ml.Add(Modifier.Static);
            int num = base.NewVar();
            this.BeginMethod(num, MemberKind.Constructor, ml, 1);
            this.InitMethod(num);
            for (int i = 0; i < this.static_variable_initializers.Count; i++)
            {
                int num3 = this.static_variable_initializers[i];
                this.Gen(base.code.OP_BEGIN_CALL, num3, 0, 0);
                this.Gen(base.code.OP_PUSH, class_id, 0, 0);
                this.Gen(base.code.OP_CALL, num3, 0, 0);
            }
            this.EndMethod(num);
        }

        public virtual  void EndMethod(int sub_id)
        {
            base.EndMethod(sub_id);
            if (this.prefix)
            {
                this.Gen(base.code.OP_END_USING, base.level_stack.Peek(), 0, 0);
                base.level_stack.Pop();
            }
            this.prefix = false;
        }

        internal override void Init(BaseScripter scripter, Module m)
        {
            base.Init(scripter, m);
            this.variable_initializers.Clear();
            this.static_variable_initializers.Clear();
            this.param_ids.Clear();
            this.param_type_ids.Clear();
            this.param_mods.Clear();
            this.local_variables.Clear();
            this.has_constructor = false;
            this.valid_this_context = false;
        }

        public override void InitMethod(int sub_id)
        {
            base.ReplaceForwardDeclaration(sub_id);
            base.InitMethod(sub_id);
        }

        private int NewTempVar()
        {
            return base.NewVar();
        }

        private bool NotMatch(char s)
        {
            if (!base.IsCurrText(s))
            {
                return true;
            }
            this.Call_SCANNER();
            return false;
        }

        private bool NotMatch(string s)
        {
            if (!base.IsCurrText(s))
            {
                return true;
            }
            this.Call_SCANNER();
            return false;
        }

        private int Parse_ArgumentList(char CloseBracket, int sub_id, int object_id)
        {
            this.Gen(base.code.OP_BEGIN_CALL, sub_id, 0, 0);
            if (base.IsCurrText(CloseBracket))
            {
                this.Gen(base.code.OP_PUSH, object_id, 0, 0);
                return 0;
            }
            int num = 0;
            do
            {
                num++;
                ParamMod none = ParamMod.None;
                int num2 = this.Parse_Expression();
                this.Gen(base.code.OP_PUSH, num2, (int) none, sub_id);
            }
            while (base.CondMatch(','));
            this.Gen(base.code.OP_PUSH, object_id, 0, 0);
            return num;
        }

        private int Parse_ArrayInitializer()
        {
            int arrayOfObjectClassId = base.ArrayOfObjectClassId;
            string name = base.GetName(arrayOfObjectClassId);
            int rank = CSLite_System.GetRank(name);
            string elementTypeName = CSLite_System.GetElementTypeName(name);
            int id = base.NewVar();
            base.SetName(id, elementTypeName);
            this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
            int res = base.NewVar();
            this.Gen(base.code.OP_CREATE_OBJECT, arrayOfObjectClassId, 0, res);
            this.Gen(base.code.OP_BEGIN_CALL, arrayOfObjectClassId, 0, 0);
            IntegerList list = new IntegerList(true);
            IntegerList list2 = new IntegerList(true);
            IntegerList list3 = new IntegerList(true);
            IntegerList list4 = new IntegerList(true);
            for (int i = 0; i < rank; i++)
            {
                int avalue = base.NewVar(-1);
                list.Add(avalue);
                list2.Add(-1);
                list3.Add(-1);
                list4.Add(0);
                this.Gen(base.code.OP_PUSH, avalue, 0, arrayOfObjectClassId);
            }
            this.Gen(base.code.OP_PUSH, res, 0, 0);
            this.Gen(base.code.OP_CALL, arrayOfObjectClassId, rank, 0);
            int num7 = -1;
        Label_011D:
            if (base.IsCurrText('['))
            {
                IntegerList list8=null;
                int num17;
                this.Match('[');
                num7++;
                if (num7 == (rank - 1))
                {
                    list3[num7] = -1;
                    while (true)
                    {
                        IntegerList list6=null;
                        int num15;
                        if (base.IsCurrText(']'))
                        {
                            goto Label_011D;
                        }
                        if (list4[num7] == 0)
                        {
                            IntegerList list5=list2;
                            int num14 = num7;
                            //(list5 = list2)[num14 = num7] = list5[num14] + 1;
                            list5[num14 ] = list5[num14] + 1;
                        }
                        list6 = list3;
                        num15 = num7;
                        //(list6 = list3)[num15 = num7] = list6[num15] + 1;
                        list6[num15] = list6[num15] + 1;
                        int num8 = base.NewVar();
                        this.Gen(base.code.OP_CREATE_INDEX_OBJECT, res, 0, num8);
                        for (int j = 0; j < rank; j++)
                        {
                            int num10 = base.NewConst(list3[j]);
                            this.Gen(base.code.OP_ADD_INDEX, num8, num10, res);
                        }
                        this.Gen(base.code.OP_SETUP_INDEX_OBJECT, num8, 0, 0);
                        this.Gen(base.code.OP_ASSIGN, num8, this.Parse_Expression(), num8);
                        if (!base.CondMatch(','))
                        {
                            goto Label_011D;
                        }
                    }
                }
                if (list4[num7] == 0)
                {
                    IntegerList list7=list2;
                    int num16= num7;
                    //(list7 = list2)[num16 = num7] = list7[num16] + 1;
                    list7[num16 ] = list7[num16] + 1;
                }
                list8 = list3;
                num17 = num7;
                //(list8 = list3)[num17 = num7] = list8[num17] + 1;
                list8[num17 ] = list8[num17] + 1;
            }
            else if (base.IsCurrText(','))
            {
                IntegerList list10=null;
                int num19;
                this.Match(',');
                if (list4[num7] == 0)
                {
                    IntegerList list9;
                    int num18;
                    list9 = list2;
                    num18 = num7;
                    //(list9 = list2)[num18 = num7] = list9[num18] + 1;
                    list9[num18] = list9[num18] + 1;
                }
                list10 = list3;
                num19 = num7;
                //(list10 = list3)[num19 = num7] = list10[num19] + 1;
                list10[num19] = list10[num19] + 1;
                for (int k = num7 + 1; k < rank; k++)
                {
                    list3[k] = -1;
                }
            }
            else if (base.IsCurrText(']'))
            {
                IntegerList list11=null;
                int num20;
                this.Match(']');
                if (list2[num7] != list3[num7])
                {
                    base.RaiseError(true, "CS0178. Incorrectly structured array initializer.");
                }
                list4[num7] = 1;
                list11 = list3;
                num20 = num7;
                //(list11 = list3)[num20 = num7] = list11[num20] - 1;
                list11[num20 ] = list11[num20] - 1;
                num7--;
                if (num7 == -1)
                {
                    for (int m = 0; m < rank; m++)
                    {
                        base.PutVal(list[m], list2[m] + 1);
                    }
                    return res;
                }
            }
            else
            {
                this.Match('[');
            }
            goto Label_011D;
        }

        private string Parse_ArrayNameModifier(IntegerList bounds)
        {
            string str = "";
            this.Match('[');
            str = str + "[";
            if (!base.IsCurrText(']'))
            {
                while (true)
                {
                    if (!base.IsCurrText(','))
                    {
                        int avalue = this.Parse_Expression();
                        bounds.Add(avalue);
                    }
                    if (!base.CondMatch(','))
                    {
                        break;
                    }
                    str = str + ",";
                }
            }
            this.Match(']');
            return (str + "]");
        }

        private void Parse_ArrayTypeDeclaration(int array_id, ModifierList ml)
        {
            base.CheckModifiers(ml, this.structure_modifiers);
            base.BeginArray(array_id, ml);
            this.Match("array");
            IntegerList list = new IntegerList(false);
            list.Add(array_id);
            if (!base.IsCurrText('['))
            {
                this.Gen(base.code.OP_ADD_ARRAY_RANGE, array_id, 8, 0);
                goto Label_00AA;
            }
            this.Match('[');
        Label_0043:
            this.Gen(base.code.OP_ADD_ARRAY_RANGE, array_id, this.Parse_OrdinalType(), 0);
            if (base.IsCurrText(','))
            {
                this.Match(',');
                array_id = base.NewVar();
                base.BeginArray(array_id, ml);
                list.Add(array_id);
                goto Label_0043;
            }
            this.Match(']');
        Label_00AA:
            this.Match("of");
            list.Add(this.Parse_Type());
            for (int i = list.Count - 1; i > 0; i--)
            {
                this.Gen(base.code.OP_ADD_ARRAY_INDEX, list[i - 1], list[i], 0);
                base.EndArray(list[i - 1]);
            }
            this.CreateDefaultConstructor(list[0], false);
        }

        private string Parse_ArrayTypeModifier()
        {
            string str = "";
            this.Match('(');
            str = str + "[";
            while (true)
            {
                if (!base.CondMatch(','))
                {
                    break;
                }
                str = str + ",";
            }
            this.Match(')');
            str = str + "]";
            return "";
        }

        private void Parse_AssignmentStatement()
        {
            int id = this.Parse_SimpleExpression();
            if (base.GetName(id) == base.GetName(base.CurrSubId))
            {
                id = base.CurrResultId;
            }
            if (base.IsCurrText(":="))
            {
                this.Call_SCANNER();
                this.Gen(base.code.OP_ASSIGN, id, this.Parse_Expression(), id);
            }
            else if (base.TopInstruction.op != base.code.OP_CALL)
            {
                this.Gen(base.code.OP_BEGIN_CALL, id, 0, 0);
                this.Gen(base.code.OP_PUSH, base.CurrThisID, 0, 0);
                this.Gen(base.code.OP_CALL, id, 0, 0);
            }
        }

        private void Parse_Attributes()
        {
        }

        private void Parse_BreakStatement()
        {
            if (base.BreakStack.Count == 0)
            {
                base.RaiseError(false, "CS0139. No enclosing loop out of which to break or continue.");
            }
            this.Gen(base.code.OP_GOTO_START, base.BreakStack.TopLabel(), 0, 0);
            this.Match("break");
        }

        private void Parse_CaseStatement()
        {
            this.Match("case");
            int num = base.NewLabel();
            int res = this.NewTempVar();
            int num5 = this.Parse_Expression();
            this.Match("of");
            do
            {
                int num3 = base.NewLabel();
                int num2 = base.NewLabel();
                do
                {
                    int num4 = base.NewLabel();
                    int num6 = this.Parse_ConstantExpression();
                    if (base.IsCurrText(".."))
                    {
                        this.Gen(base.code.OP_GE, num5, num6, res);
                        this.Gen(base.code.OP_GO_FALSE, num4, res, 0);
                        this.Match("..");
                        this.Gen(base.code.OP_LE, num5, this.Parse_ConstantExpression(), res);
                        this.Gen(base.code.OP_GO_FALSE, num4, res, 0);
                    }
                    else
                    {
                        this.Gen(base.code.OP_EQ, num5, num6, res);
                    }
                    this.Gen(base.code.OP_GO_TRUE, num3, res, 0);
                    base.SetLabelHere(num4);
                }
                while (!this.NotMatch(','));
                this.Match(':');
                this.Gen(base.code.OP_GO, num2, 0, 0);
                base.SetLabelHere(num3);
                this.Parse_Statement();
                this.Gen(base.code.OP_GO, num, 0, 0);
                base.SetLabelHere(num2);
            }
            while ((!this.NotMatch(';') && !base.IsCurrText("else")) && !base.IsCurrText("end"));
            if (base.IsCurrText("else"))
            {
                this.Match("else");
                this.Parse_Statement();
            }
            if (base.IsCurrText(';'))
            {
                this.Match(';');
            }
            this.Match("end");
            base.SetLabelHere(num);
        }

        private void Parse_ClassBase(int class_id)
        {
            this.Match('(');
            do
            {
                int res = this.Parse_Ident();
                this.Gen(base.code.OP_EVAL_TYPE, 0, 0, res);
                while (true)
                {
                    base.REF_SWITCH = true;
                    if (!base.CondMatch('.'))
                    {
                        break;
                    }
                    int num2 = res;
                    res = this.Parse_Ident();
                    this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, num2, 0, res);
                }
                this.Gen(base.code.OP_ADD_ANCESTOR, class_id, res, 0);
            }
            while (base.CondMatch(','));
            this.Match(')');
        }

        private void Parse_ClassBody(int class_id, ModifierList owner_modifiers, bool IsModule)
        {
            this.variable_initializers.Clear();
            while (!base.IsCurrText("Begin") && !base.IsCurrText("End"))
            {
                if (base.IsEOF())
                {
                    this.Match("End");
                }
                this.Parse_ClassMemberDeclaration(class_id, owner_modifiers, IsModule, ClassKind.Class);
            }
        }

        private void Parse_ClassMemberDeclaration(int class_id, ModifierList owner_modifiers, bool IsModule, ClassKind ck)
        {
            this.Parse_Attributes();
            ModifierList ml = new ModifierList();
            ml.Add(Modifier.Public);
            if (base.IsCurrText("private"))
            {
                this.Call_SCANNER();
            }
            else if (base.IsCurrText("protected"))
            {
                this.Call_SCANNER();
            }
            if (base.IsCurrText("public"))
            {
                this.Call_SCANNER();
            }
            if (owner_modifiers.HasModifier(Modifier.Public) && !ml.HasModifier(Modifier.Private))
            {
                ml.Add(Modifier.Public);
            }
            if (base.IsCurrText("type"))
            {
                this.Parse_TypeDeclaration(class_id, owner_modifiers);
            }
            else if (base.IsCurrText("var"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_VariableMemberDeclaration(class_id, ml, owner_modifiers, IsModule);
            }
            else if (base.IsCurrText("const"))
            {
                ml.Add(Modifier.Static);
                this.Parse_ConstantMemberDeclaration(class_id, ml, owner_modifiers, IsModule);
            }
            else if (base.IsCurrText("constructor"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_MethodMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else if (base.IsCurrText("destructor"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_MethodMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else if (base.IsCurrText("procedure"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_MethodMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else if (base.IsCurrText("function"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_MethodMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else if (base.IsCurrText("property"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_PropertyMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_VariableMemberDeclaration(class_id, ml, owner_modifiers, IsModule);
            }
        }

        private void Parse_ClassTypeDeclaration(int class_id, ModifierList ml)
        {
            base.DECLARE_SWITCH = true;
            this.has_constructor = false;
            base.CheckModifiers(ml, this.class_modifiers);
            if (ml.HasModifier(Modifier.Abstract) && ml.HasModifier(Modifier.Sealed))
            {
                base.RaiseError(false, "CS0502. The class '{0}' is abstract and sealed.");
            }
            base.BeginClass(class_id, ml);
            this.Match("class");
            if (base.IsCurrText('('))
            {
                this.Parse_ClassBase(class_id);
            }
            else
            {
                this.Gen(base.code.OP_ADD_ANCESTOR, class_id, base.ObjectClassId, 0);
            }
            this.Parse_ClassBody(class_id, ml, false);
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(class_id);
            }
            if (!this.has_constructor)
            {
                this.CreateDefaultConstructor(class_id, false);
            }
            base.EndClass(class_id);
            this.Match("end");
        }

        private void Parse_CompoundStatement()
        {
            base.DECLARE_SWITCH = false;
            this.Match("begin");
            this.Parse_Statements();
            this.Match("end");
        }

        private void Parse_ConstantDeclarator(ModifierList ml, bool IsModule)
        {
            int num = this.Parse_Ident();
            int objectClassId = base.ObjectClassId;
            if (base.IsCurrText(":"))
            {
                this.Match(":");
                objectClassId = this.Parse_Type();
            }
            this.Gen(base.code.OP_ASSIGN_TYPE, num, objectClassId, 0);
            base.BeginField(num, ml, objectClassId);
            int num3 = base.NewVar();
            this.BeginMethod(num3, MemberKind.Method, ml, 1);
            this.InitMethod(num3);
            base.DECLARE_SWITCH = false;
            this.Match('=');
            int num4 = this.Parse_ConstantExpression();
            this.static_variable_initializers.Add(num3);
            this.Gen(base.code.OP_ASSIGN, num, num4, num);
            this.EndMethod(num3);
            base.DECLARE_SWITCH = true;
            base.EndField(num);
        }

        private int Parse_ConstantExpression()
        {
            return this.Parse_Expression();
        }

        private void Parse_ConstantMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_modifiers, bool IsModule)
        {
            base.DECLARE_SWITCH = true;
            this.Match("const");
            do
            {
                this.Parse_ConstantDeclarator(ml, IsModule);
                this.Match(";");
            }
            while (base.curr_token.tokenClass != TokenClass.Keyword);
        }

        private void Parse_ConstructorDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            this.Match("constructor");
            this.valid_this_context = true;
            bool flag = ml.HasModifier(Modifier.Static);
            int num2 = this.BeginMethod(this.Parse_Ident(), MemberKind.Constructor, ml, 1);
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    if (flag)
                    {
                        base.RaiseErrorEx(false, "CS0132. '{0}' : a static constructor must be parameterless.", new object[] { base.GetName(class_id) });
                    }
                    this.Parse_ParameterList(num2, false);
                }
                this.Match(')');
            }
            this.Match(";");
            if (!this.Parse_ForwardMethodDeclaration(num2))
            {
                IntegerList list;
                this.InitMethod(num2);
                this.Parse_LocalDeclarationPart();
                if (flag)
                {
                    list = this.static_variable_initializers;
                }
                else
                {
                    list = this.variable_initializers;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    int num4 = list[i];
                    this.Gen(base.code.OP_BEGIN_CALL, num4, 0, 0);
                    this.Gen(base.code.OP_PUSH, base.CurrThisID, 0, 0);
                    this.Gen(base.code.OP_CALL, num4, 0, 0);
                }
                base.DECLARE_SWITCH = false;
                this.Parse_CompoundStatement();
                this.Match(";");
                this.EndMethod(num2);
                this.valid_this_context = false;
                if (flag)
                {
                    this.static_variable_initializers.Clear();
                }
                else
                {
                    this.has_constructor = true;
                }
                base.DECLARE_SWITCH = true;
            }
        }

        private void Parse_ContinueStatement()
        {
            if (base.ContinueStack.Count == 0)
            {
                base.RaiseError(false, "CS0139. No enclosing loop out of which to break or continue.");
            }
            this.Gen(base.code.OP_GOTO_START, base.ContinueStack.TopLabel(), 0, 0);
            this.Match("continue");
        }

        private void Parse_DestructorDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            this.Match("destructor");
            int num = 1;
            this.valid_this_context = true;
            int num2 = this.BeginMethod(this.Parse_Ident(), MemberKind.Method, ml, num);
            this.Match(";");
            if (!this.Parse_ForwardMethodDeclaration(num2))
            {
                if (ck != ClassKind.Interface)
                {
                    this.InitMethod(num2);
                    this.Parse_LocalDeclarationPart();
                    if (ml.HasModifier(Modifier.Abstract))
                    {
                        string name = base.GetName(num2);
                        base.RaiseErrorEx(false, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { name });
                    }
                    base.DECLARE_SWITCH = false;
                    this.Parse_CompoundStatement();
                    this.Match(";");
                }
                this.EndMethod(num2);
                this.valid_this_context = false;
                base.DECLARE_SWITCH = true;
            }
        }

        private IntegerList Parse_DirectiveList()
        {
            IntegerList list = new IntegerList(false);
            while (true)
            {
                while (base.IsCurrText("overload"))
                {
                    this.Call_SCANNER();
                    list.Add(0);
                    this.Match(';');
                }
                if (!base.IsCurrText("forward"))
                {
                    return list;
                }
                this.Call_SCANNER();
                list.Add(1);
                this.Match(';');
            }
        }

        private void Parse_EnumTypeDeclaration(int enum_id)
        {
            int currLevel = base.CurrLevel;
            int id = base.NewVar();
            ModifierList ml = new ModifierList();
            ml.Add(Modifier.Public);
            ml.Add(Modifier.Static);
            base.DECLARE_SWITCH = true;
            int num3 = 8;
            base.BeginEnum(enum_id, ml, num3);
            this.Gen(base.code.OP_ADD_UNDERLYING_TYPE, enum_id, num3, 0);
            int v = -1;
            this.static_variable_initializers.Clear();
            this.Match("(");
            do
            {
                int num7;
                if (base.IsEOF())
                {
                    this.Match(")");
                }
                int num5 = this.Parse_Ident();
                base.SetName(id, base.GetName(num5));
                base.BeginField(num5, ml, enum_id);
                int num6 = base.NewVar();
                this.BeginMethod(num6, MemberKind.Method, ml, 1);
                this.InitMethod(num6);
                this.static_variable_initializers.Add(num6);
                if (base.IsCurrText('='))
                {
                    base.DECLARE_SWITCH = false;
                    this.Match('=');
                    num7 = this.Parse_ConstantExpression();
                    base.DECLARE_SWITCH = true;
                    object val = base.GetVal(num7);
                    if (val != null)
                    {
                        v = (int) val;
                    }
                    this.Gen(base.code.OP_ASSIGN, num5, num7, num5);
                    base.SetTypeId(num7, num3);
                    this.Gen(base.code.OP_ASSIGN, id, num7, id);
                }
                else
                {
                    v++;
                    num7 = base.NewConst(v);
                    this.Gen(base.code.OP_ASSIGN, num5, num7, num5);
                    base.SetTypeId(num7, num3);
                    this.Gen(base.code.OP_ASSIGN, id, num7, id);
                }
                this.EndMethod(num6);
                base.EndField(num5);
                base.EndField(id);
                base.DECLARE_SWITCH = true;
            }
            while (!this.NotMatch(","));
            this.CreateDefaultStaticConstructor(enum_id);
            base.DECLARE_SWITCH = true;
            base.EndEnum(enum_id);
            this.Match(")");
        }

        private void Parse_ExitStatement()
        {
            this.Match("exit");
            this.Gen(base.code.OP_EXIT_SUB, 0, 0, 0);
        }

        public override int Parse_Expression()
        {
            int num = this.Parse_SimpleExpression();
            while (((base.IsCurrText('=') || base.IsCurrText("<>")) || (base.IsCurrText('>') || base.IsCurrText(">="))) || (base.IsCurrText('<') || base.IsCurrText("<=")))
            {
                if (base.IsCurrText('='))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_EQ, num, this.Parse_SimpleExpression());
                }
                else
                {
                    if (base.IsCurrText("<>"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_NE, num, this.Parse_SimpleExpression());
                        continue;
                    }
                    if (base.IsCurrText('>'))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_GT, num, this.Parse_SimpleExpression());
                        continue;
                    }
                    if (base.IsCurrText(">="))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_GE, num, this.Parse_SimpleExpression());
                        continue;
                    }
                    if (base.IsCurrText('<'))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_LT, num, this.Parse_SimpleExpression());
                        continue;
                    }
                    if (base.IsCurrText("<="))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_LE, num, this.Parse_SimpleExpression());
                    }
                }
            }
            return num;
        }

        private int Parse_Factor()
        {
            int currThisID;
            if (base.IsCurrText('('))
            {
                this.Match('(');
                currThisID = this.Parse_Expression();
                this.Match(')');
            }
            else if (base.IsCurrText("Self"))
            {
                this.Match("Self");
                currThisID = base.CurrThisID;
                if (base.GetName(currThisID) != "this")
                {
                    base.RaiseError(false, "CS0026. Keyword this is not valid in a static property, static method, or static field initializer.");
                }
                if (!this.valid_this_context)
                {
                    base.RaiseError(false, "CS0027. Keyword this is not available in the current context.");
                }
            }
            else if (base.IsCurrText("true"))
            {
                currThisID = this.Parse_BooleanLiteral();
            }
            else if (base.IsCurrText("false"))
            {
                currThisID = this.Parse_BooleanLiteral();
            }
            else if (base.curr_token.tokenClass == TokenClass.StringConst)
            {
                currThisID = this.Parse_StringLiteral();
            }
            else if (base.curr_token.tokenClass == TokenClass.CharacterConst)
            {
                currThisID = this.Parse_CharacterLiteral();
            }
            else if (base.curr_token.tokenClass == TokenClass.IntegerConst)
            {
                currThisID = this.Parse_IntegerLiteral();
            }
            else if (base.curr_token.tokenClass == TokenClass.RealConst)
            {
                currThisID = this.Parse_RealLiteral();
            }
            else if (base.IsCurrText("["))
            {
                currThisID = this.Parse_ArrayInitializer();
            }
            else
            {
                currThisID = this.Parse_SimpleNameExpression();
            }
            while (true)
            {
                while (base.IsCurrText('('))
                {
                    int num2 = currThisID;
                    currThisID = base.NewVar();
                    this.Match('(');
                    this.Gen(base.code.OP_CALL, num2, this.Parse_ArgumentList(')', num2, base.CurrThisID), currThisID);
                    this.Match(')');
                }
                if (base.IsCurrText('['))
                {
                    int num3 = currThisID;
                    currThisID = base.NewVar();
                    this.Match('[');
                    this.Gen(base.code.OP_CALL, num3, this.Parse_ArgumentList(']', num3, base.CurrThisID), currThisID);
                    this.Match(']');
                }
                else
                {
                    if (!base.IsCurrText('.'))
                    {
                        return currThisID;
                    }
                    base.REF_SWITCH = true;
                    this.Match('.');
                    int num4 = currThisID;
                    currThisID = this.Parse_Ident();
                    this.Gen(base.code.OP_CREATE_REFERENCE, num4, 0, currThisID);
                }
            }
        }

        private void Parse_ForStatement()
        {
            bool flag;
            this.Match("for");
            int l = base.NewLabel();
            int num7 = base.NewLabel();
            int res = this.NewTempVar();
            int num5 = this.NewTempVar();
            int num = this.Parse_Ident();
            this.Match(":=");
            int num2 = this.Parse_Expression();
            this.Gen(base.code.OP_ASSIGN, num, num2, num);
            if (base.IsCurrText("downto"))
            {
                this.Match("downto");
                flag = false;
            }
            else
            {
                this.Match("to");
                flag = true;
            }
            int num3 = this.Parse_Expression();
            this.Gen(base.code.OP_GT, num2, num3, res);
            this.Gen(base.code.OP_GO_TRUE, num7, res, 0);
            this.Match("do");
            base.SetLabelHere(l);
            base.BreakStack.Push(num7);
            base.ContinueStack.Push(l);
            this.Parse_Statement();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            if (flag)
            {
                this.Gen(base.code.OP_PLUS, num, base.NewConst(1), num);
            }
            else
            {
                this.Gen(base.code.OP_MINUS, num, base.NewConst(1), num);
            }
            this.Gen(base.code.OP_GT, num, num3, num5);
            this.Gen(base.code.OP_GO_FALSE, l, num5, 0);
            base.SetLabelHere(num7);
        }

        private bool Parse_ForwardMethodDeclaration(int sub_id)
        {
            if (!this.prefix)
            {
                if (this.Parse_DirectiveList().IndexOf(1) >= 0)
                {
                    base.SetForward(sub_id, true);
                    this.EndMethod(sub_id);
                    return true;
                }
                if (((base.IsCurrText("end") || base.IsCurrText("procedure")) || (base.IsCurrText("function") || base.IsCurrText("constructor"))) || (base.IsCurrText("property") || base.IsCurrText("destructor")))
                {
                    base.SetForward(sub_id, true);
                    this.EndMethod(sub_id);
                    return true;
                }
            }
            return false;
        }

        private void Parse_FunctionDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            this.Match("function");
            int num = 0x10;
            this.valid_this_context = true;
            int num2 = this.BeginMethod(this.Parse_Ident(), MemberKind.Method, ml, num);
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    this.Parse_ParameterList(num2, false);
                }
                this.Match(')');
            }
            this.Match(":");
            this.Parse_Attributes();
            num = this.Parse_Type();
            this.Match(";");
            if (!this.Parse_ForwardMethodDeclaration(num2))
            {
                base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, num2, -1, -1);
                base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, base.CurrResultId, -1, -1);
                this.Gen(base.code.OP_ASSIGN_TYPE, num2, num, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, base.CurrResultId, num, 0);
                if (this.Parse_DirectiveList().IndexOf(1) >= 0)
                {
                    base.SetForward(num2, true);
                    this.EndMethod(num2);
                }
                else
                {
                    base.SetName(base.CurrResultId, "result");
                    this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, base.CurrResultId, base.CurrSubId, 0);
                    if (ml.HasModifier(Modifier.Extern))
                    {
                        base.RaiseErrorEx(false, "CS0179. '{0}' cannot be extern and declare a body.", new object[] { base.GetName(num2) });
                    }
                    if (ck != ClassKind.Interface)
                    {
                        this.InitMethod(num2);
                        this.Parse_LocalDeclarationPart();
                        if (ml.HasModifier(Modifier.Abstract))
                        {
                            string name = base.GetName(num2);
                            base.RaiseErrorEx(false, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { name });
                        }
                        if (base.GetName(num2) == "Main")
                        {
                            this.Gen(base.code.OP_CHECKED, base.TRUE_id, 0, 0);
                        }
                        base.DECLARE_SWITCH = false;
                        this.Parse_CompoundStatement();
                        this.Match(";");
                        if (base.GetName(num2) == "Main")
                        {
                            this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                        }
                    }
                    this.EndMethod(num2);
                    this.valid_this_context = false;
                    base.DECLARE_SWITCH = true;
                }
            }
        }

        private void Parse_FunctionHeading(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            this.Match("function");
            int num = 0x10;
            int num2 = this.BeginMethod(this.Parse_Ident(), MemberKind.Method, ml, num);
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    this.Parse_ParameterList(num2, false);
                }
                this.Match(')');
            }
            this.Match(":");
            this.Parse_Attributes();
            num = this.Parse_Type();
            this.Match(";");
            base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, num2, -1, -1);
            base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, base.CurrResultId, -1, -1);
            this.Gen(base.code.OP_ASSIGN_TYPE, num2, num, 0);
            this.Gen(base.code.OP_ASSIGN_TYPE, base.CurrResultId, num, 0);
            this.Parse_DirectiveList();
            base.SetForward(num2, true);
            this.EndMethod(num2);
            base.DECLARE_SWITCH = true;
        }

        private void Parse_GotoStatement()
        {
            this.Match("Goto");
            int id = this.Parse_Ident();
            base.PutKind(id, MemberKind.Label);
            this.Gen(base.code.OP_GOTO_START, id, 0, 0);
        }

        private int Parse_IdentOrType()
        {
            if (base.IsCurrText("Boolean"))
            {
                this.Call_SCANNER();
                return 2;
            }
            if (base.IsCurrText("Date"))
            {
                base.RaiseError(true, "CS0001. Internal compiler error.");
                return 0;
            }
            if (base.IsCurrText("Char"))
            {
                this.Call_SCANNER();
                return 4;
            }
            if (base.IsCurrText("String"))
            {
                this.Call_SCANNER();
                return 12;
            }
            if (base.IsCurrText("Byte"))
            {
                this.Call_SCANNER();
                return 3;
            }
            if (base.IsCurrText("Short"))
            {
                this.Call_SCANNER();
                return 11;
            }
            if (base.IsCurrText("Integer"))
            {
                this.Call_SCANNER();
                return 8;
            }
            if (base.IsCurrText("Long"))
            {
                this.Call_SCANNER();
                return 9;
            }
            if (base.IsCurrText("Single"))
            {
                this.Call_SCANNER();
                return 7;
            }
            if (base.IsCurrText("Double"))
            {
                this.Call_SCANNER();
                return 6;
            }
            if (base.IsCurrText("Decimal"))
            {
                this.Call_SCANNER();
                return 5;
            }
            if (base.IsCurrText("Object"))
            {
                this.Call_SCANNER();
                return 0x10;
            }
            if (base.IsCurrText("Nil"))
            {
                this.Call_SCANNER();
                return base.NULL_id;
            }
            return this.Parse_Ident();
        }

        private void Parse_IfStatement()
        {
            this.Match("if");
            int num = base.NewLabel();
            this.Gen(base.code.OP_GO_FALSE, num, this.Parse_Expression(), 0);
            this.Match("then");
            this.Parse_Statement();
            if (base.IsCurrText("else"))
            {
                int num2 = base.NewLabel();
                this.Gen(base.code.OP_GO, num2, 0, 0);
                base.SetLabelHere(num);
                this.Match("else");
                this.Parse_Statement();
                base.SetLabelHere(num2);
            }
            else
            {
                base.SetLabelHere(num);
            }
        }

        private void Parse_ImplementationSection(int namespace_id)
        {
            bool flag;
            this.Match("implementation");
            while (base.IsCurrText("Uses"))
            {
                this.Parse_UsesStatement();
            }
            ModifierList ml = new ModifierList();
            ml.Add(Modifier.Static);
            ModifierList list2 = new ModifierList();
            do
            {
                flag = false;
                if (base.IsCurrText("var"))
                {
                    this.Parse_VariableMemberDeclaration(namespace_id, ml, list2, true);
                    flag = true;
                }
                else if (base.IsCurrText("const"))
                {
                    this.Parse_ConstantMemberDeclaration(namespace_id, ml, list2, true);
                    flag = true;
                }
                else if (base.IsCurrText("procedure"))
                {
                    this.Parse_ProcedureDeclaration(namespace_id, ml, list2, ClassKind.Class);
                    flag = true;
                }
                else if (base.IsCurrText("function"))
                {
                    this.Parse_FunctionDeclaration(namespace_id, ml, list2, ClassKind.Class);
                    flag = true;
                }
                else if (base.IsCurrText("type"))
                {
                    this.Parse_TypeDeclaration(namespace_id, list2);
                    flag = true;
                }
            }
            while (flag);
        }

        private void Parse_InitSection(int namespace_id)
        {
            if (base.IsCurrText("initialization"))
            {
                this.Call_SCANNER();
                this.Parse_Statements();
                if (base.IsCurrText("finalization"))
                {
                    this.Call_SCANNER();
                    this.Parse_Statements();
                }
                this.Match("end");
            }
            else if (base.IsCurrText("begin"))
            {
                this.Call_SCANNER();
                this.Parse_Statements();
                this.Match("end");
            }
            else if (base.IsCurrText("end"))
            {
                this.Call_SCANNER();
            }
            else
            {
                this.Match("end");
            }
        }

        private int Parse_IntegralType()
        {
            int typeId = this.integral_types.GetTypeId(base.curr_token.Text);
            if (typeId == -1)
            {
                base.RaiseError(false, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
            }
            this.Call_SCANNER();
            return typeId;
        }

        private void Parse_InterfaceSection(int namespace_id)
        {
            bool flag;
            ModifierList ml = new ModifierList();
            ml.Add(Modifier.Static);
            ModifierList list2 = new ModifierList();
            this.Match("interface");
            while (base.IsCurrText("Uses"))
            {
                this.Parse_UsesStatement();
            }
            base.BeginClass(namespace_id, ml);
            this.Gen(base.code.OP_ADD_ANCESTOR, namespace_id, base.ObjectClassId, 0);
            do
            {
                flag = false;
                if (base.IsCurrText("var"))
                {
                    this.Parse_VariableMemberDeclaration(namespace_id, ml, list2, true);
                    flag = true;
                }
                else if (base.IsCurrText("const"))
                {
                    this.Parse_ConstantMemberDeclaration(namespace_id, ml, list2, true);
                    flag = true;
                }
                else if (base.IsCurrText("procedure"))
                {
                    this.Parse_ProcedureHeading(namespace_id, ml, list2, ClassKind.Class);
                    flag = true;
                }
                else if (base.IsCurrText("function"))
                {
                    this.Parse_FunctionHeading(namespace_id, ml, list2, ClassKind.Class);
                    flag = true;
                }
                else if (base.IsCurrText("type"))
                {
                    this.Parse_TypeDeclaration(namespace_id, list2);
                    flag = true;
                }
            }
            while (flag);
        }

        private void Parse_LocalDeclarationPart()
        {
            bool flag;
            do
            {
                flag = false;
                if (base.IsCurrText("var"))
                {
                    this.Parse_LocalDeclarationStatement(LocalModifier.Var);
                    flag = true;
                }
                else if (base.IsCurrText("const"))
                {
                    this.Parse_LocalDeclarationStatement(LocalModifier.Const);
                    flag = true;
                }
            }
            while (flag);
        }

        public void Parse_LocalDeclarationStatement(LocalModifier m)
        {
            this.local_variables.Clear();
            base.DECLARE_SWITCH = true;
            base.DECLARATION_CHECK_SWITCH = true;
            this.Call_SCANNER();
            do
            {
                int num = 0x10;
                do
                {
                    int num2 = this.Parse_Ident();
                    this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, num2, base.CurrSubId, 0);
                    this.local_variables.Add(num2);
                }
                while (base.CondMatch(','));
                base.DECLARE_SWITCH = false;
                if (m == LocalModifier.Var)
                {
                    this.Match(":");
                    num = this.Parse_Type();
                }
                else if (base.IsCurrText(":"))
                {
                    this.Match(":");
                    num = this.Parse_Type();
                }
                for (int i = 0; i < this.local_variables.Count; i++)
                {
                    int num4 = this.local_variables[i];
                    this.Gen(base.code.OP_ASSIGN_TYPE, num4, num, 0);
                }
                if (base.IsCurrText('='))
                {
                    base.DECLARE_SWITCH = false;
                    this.Match('=');
                    if (this.local_variables.Count == 1)
                    {
                        int num5 = this.local_variables[0];
                        this.Gen(base.code.OP_ASSIGN, num5, this.Parse_Expression(), num5);
                    }
                    else
                    {
                        base.RaiseError(true, "VB00002. Explicit initialization is not permitted with multiple variables declared with a single type specifier.");
                    }
                    base.DECLARE_SWITCH = true;
                }
                else
                {
                    for (int j = 0; j < this.local_variables.Count; j++)
                    {
                        int res = this.local_variables[j];
                        this.Gen(base.code.OP_CHECK_STRUCT_CONSTRUCTOR, num, 0, res);
                    }
                }
                base.DECLARE_SWITCH = true;
            }
            while (base.CondMatch(','));
            base.DECLARE_SWITCH = false;
            base.DECLARATION_CHECK_SWITCH = false;
            this.Match(";");
        }

        private void Parse_MethodMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            base.DECLARE_SWITCH = true;
            if (base.IsCurrText("procedure"))
            {
                this.Parse_ProcedureDeclaration(class_id, ml, owner_ml, ck);
            }
            else if (base.IsCurrText("function"))
            {
                this.Parse_FunctionDeclaration(class_id, ml, owner_ml, ck);
            }
            else if (base.IsCurrText("constructor"))
            {
                this.Parse_ConstructorDeclaration(class_id, ml, owner_ml, ck);
            }
            else if (base.IsCurrText("destructor"))
            {
                this.Parse_DestructorDeclaration(class_id, ml, owner_ml, ck);
            }
        }

        private ModifierList Parse_Modifiers()
        {
            int num2;
            ModifierList list = new ModifierList();
            int num = 0;
        Label_000D:
            num2 = this.total_modifier_list.IndexOf(base.curr_token.Text);
            switch (base.curr_token.Text)
            {
                case "private":
                case "protected":
                case "public":
                case "internal":
                    num++;
                    break;
            }
            if (num2 >= 0)
            {
                Modifier m = (Modifier) this.total_modifier_list.Objects[num2];
                if (list.HasModifier(m))
                {
                    string str2 = this.total_modifier_list[(int) m];
                    base.RaiseErrorEx(false, "CS1004. Duplicate '{0}' modifier.", new object[] { str2 });
                }
                list.Add(m);
                this.Call_SCANNER();
                goto Label_000D;
            }
            if (num > 1)
            {
                base.RaiseError(false, "CS0107. More than one protection modifier.");
            }
            if (list.HasModifier(Modifier.Private) && (list.HasModifier(Modifier.Virtual) || list.HasModifier(Modifier.Abstract)))
            {
                base.RaiseError(false, "CS0621. Virtual or abstract members cannot be private.");
            }
            if (!list.HasModifier(Modifier.Private))
            {
                list.Add(Modifier.Public);
            }
            return list;
        }

        private void Parse_NamespaceDeclaration()
        {
            this.Match("Namespace");
            IntegerList list = new IntegerList(false);
            do
            {
                int avalue = this.Parse_Ident();
                list.Add(avalue);
                base.BeginNamespace(avalue);
            }
            while (base.CondMatch('.'));
        Label_003A:
            this.Parse_NamespaceMemberDeclaration();
            if (!base.IsCurrText("End"))
            {
                if (base.IsEOF())
                {
                    this.Match("End");
                }
                goto Label_003A;
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                base.EndNamespace(list[i]);
            }
            this.Match("End");
            this.Match(';');
        }

        private void Parse_NamespaceMemberDeclaration()
        {
            if (base.IsCurrText("Namespace"))
            {
                this.Parse_NamespaceDeclaration();
            }
            else
            {
                this.Parse_TypeDeclaration();
            }
        }

        private int Parse_NamespaceOrTypeName()
        {
            int res = this.Parse_Ident();
            this.Gen(base.code.OP_EVAL_TYPE, 0, 0, res);
            while (true)
            {
                base.REF_SWITCH = true;
                if (!base.CondMatch('.'))
                {
                    break;
                }
                int num2 = res;
                res = this.Parse_Ident();
                this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, num2, 0, res);
            }
            base.REF_SWITCH = false;
            return res;
        }

        private int Parse_NonArrayType()
        {
            int res = this.Parse_IdentOrType();
            this.Gen(base.code.OP_EVAL_TYPE, 0, 0, res);
            while (true)
            {
                base.REF_SWITCH = true;
                if (!base.CondMatch('.'))
                {
                    break;
                }
                int num2 = res;
                res = this.Parse_Ident();
                this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, num2, 0, res);
            }
            base.REF_SWITCH = false;
            return res;
        }

        private void Parse_NonProgramDeclaration(ModifierList ml)
        {
            if (base.scripter.SIGN_BRIEF_SYNTAX)
            {
                base.DECLARE_SWITCH = false;
                ml.Add(Modifier.Public);
                int id = base.NewVar();
                base.SetName(id, "__Main");
                base.BeginClass(id, ml);
                this.Gen(base.code.OP_ADD_ANCESTOR, id, base.ObjectClassId, 0);
                ml.Add(Modifier.Static);
                int num2 = base.NewVar();
                base.SetName(num2, "Main");
                this.BeginMethod(num2, MemberKind.Method, ml, 1);
                this.InitMethod(num2);
                base.MoveSeparator();
                this.Parse_Statements();
                this.EndMethod(num2);
                base.EndClass(id);
            }
        }

        private int Parse_OrdinalType()
        {
            int num;
            if (base.IsCurrText('('))
            {
                num = base.NewVar();
                this.Parse_EnumTypeDeclaration(num);
                return num;
            }
            if (base.curr_token.tokenClass == TokenClass.IntegerConst)
            {
                num = base.NewVar();
                this.Parse_SubrangeTypeDeclaration(num, StandardType.Int);
                return num;
            }
            if (base.curr_token.tokenClass == TokenClass.CharacterConst)
            {
                num = base.NewVar();
                this.Parse_SubrangeTypeDeclaration(num, StandardType.Char);
                return num;
            }
            if (base.curr_token.tokenClass == TokenClass.BooleanConst)
            {
                num = base.NewVar();
                this.Parse_SubrangeTypeDeclaration(num, StandardType.Bool);
                return num;
            }
            return this.Parse_NonArrayType();
        }

        private int Parse_ParameterList(int sub_id, bool isIndexer)
        {
            this.param_ids.Clear();
            this.param_type_ids.Clear();
            this.param_mods.Clear();
            int num = 0;
            while (true)
            {
                num++;
                this.Parse_Attributes();
                ParamMod none = ParamMod.None;
                int objectClassId = base.ObjectClassId;
                if (base.IsCurrText("var"))
                {
                    if (isIndexer)
                    {
                        base.RaiseError(false, "CS0631. Indexers can't have ref or out parameters.");
                    }
                    this.Match("var");
                    none = ParamMod.RetVal;
                }
                do
                {
                    this.param_ids.Add(this.Parse_Ident());
                }
                while (!this.NotMatch(","));
                this.Match(":");
                objectClassId = this.Parse_Type();
                int res = 0;
                if (base.IsCurrText("="))
                {
                    this.Match('=');
                    res = this.Parse_Expression();
                }
                while (this.param_type_ids.Count < this.param_ids.Count)
                {
                    int count = this.param_type_ids.Count;
                    int num5 = this.param_ids[count];
                    this.Gen(base.code.OP_ASSIGN_TYPE, num5, objectClassId, 0);
                    this.Gen(base.code.OP_ADD_PARAM, sub_id, num5, (int) none);
                    if (res != 0)
                    {
                        this.Gen(base.code.OP_ADD_DEFAULT_VALUE, sub_id, num5, res);
                    }
                    this.param_type_ids.Add(objectClassId);
                    this.param_mods.Add((int) none);
                }
                if (!base.CondMatch(';'))
                {
                    return num;
                }
            }
        }

        public void Parse_PrintlnStatement()
        {
            this.Parse_PrintStatement();
            this.Gen(base.code.OP_PRINT, base.BR_id, 0, 0);
        }

        public void Parse_PrintStatement()
        {
            this.Call_SCANNER();
            do
            {
                this.Gen(base.code.OP_PRINT, this.Parse_Expression(), 0, 0);
            }
            while (base.CondMatch(','));
        }

        private void Parse_ProcedureDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            this.Match("procedure");
            int num2 = 1;
            this.valid_this_context = true;
            int num = this.BeginMethod(this.Parse_Ident(), MemberKind.Method, ml, num2);
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    this.Parse_ParameterList(num, false);
                }
                this.Match(')');
            }
            this.Match(";");
            if (!this.Parse_ForwardMethodDeclaration(num))
            {
                if (ck != ClassKind.Interface)
                {
                    this.InitMethod(num);
                    this.Parse_LocalDeclarationPart();
                    if (ml.HasModifier(Modifier.Abstract))
                    {
                        string name = base.GetName(num);
                        base.RaiseErrorEx(false, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { name });
                    }
                    if (base.GetName(num) == "Main")
                    {
                        this.Gen(base.code.OP_CHECKED, base.TRUE_id, 0, 0);
                    }
                    base.DECLARE_SWITCH = false;
                    this.Parse_CompoundStatement();
                    this.Match(";");
                    if (base.GetName(num) == "Main")
                    {
                        this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                    }
                }
                this.EndMethod(num);
                this.valid_this_context = false;
                base.DECLARE_SWITCH = true;
            }
        }

        private void Parse_ProcedureHeading(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            this.Match("procedure");
            int num2 = 1;
            int num = this.BeginMethod(this.Parse_Ident(), MemberKind.Method, ml, num2);
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    this.Parse_ParameterList(num, false);
                }
                this.Match(')');
            }
            this.Match(";");
            this.Parse_DirectiveList();
            base.SetForward(num, true);
            this.EndMethod(num);
            base.DECLARE_SWITCH = true;
        }

        public override void Parse_Program()
        {
            base.DECLARE_SWITCH = false;
            this.Gen(base.code.OP_UPCASE_ON, 0, 0, 0);
            this.Gen(base.code.OP_EXPLICIT_ON, 0, 0, 0);
            this.Gen(base.code.OP_STRICT_OFF, 0, 0, 0);
            int id = base.NewVar();
            base.SetName(id, "System");
            int res = base.NewRef("Math");
            this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
            this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, id, 0, res);
            this.Gen(base.code.OP_BEGIN_USING, res, 0, 0);
            this.Parse_Start();
        }

        private void Parse_ProgramDeclaration(ModifierList ml)
        {
            this.Match("program");
            base.DECLARE_SWITCH = true;
            this.has_constructor = false;
            base.CheckModifiers(ml, this.class_modifiers);
            int num = this.Parse_Ident();
            this.Match(";");
            while (base.IsCurrText("Uses"))
            {
                this.Parse_UsesStatement();
            }
            base.BeginClass(num, ml);
            this.Gen(base.code.OP_ADD_ANCESTOR, num, base.ObjectClassId, 0);
            this.Parse_ClassBody(num, ml, true);
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(num);
            }
            ml.Add(Modifier.Static);
            int id = base.NewVar();
            base.SetName(id, "Main");
            this.BeginMethod(id, MemberKind.Method, ml, 1);
            this.InitMethod(id);
            base.MoveSeparator();
            this.Gen(base.code.OP_INSERT_STRUCT_CONSTRUCTORS, num, 0, 0);
            this.Parse_CompoundStatement();
            this.EndMethod(id);
            base.EndClass(num);
            this.Match(".");
        }

        private void Parse_PropertyMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_modifiers, ClassKind ck)
        {
            base.DECLARE_SWITCH = true;
            this.Match("property");
            int num = this.Parse_Ident();
            this.param_ids.Clear();
            this.param_type_ids.Clear();
            this.param_mods.Clear();
            IntegerList list = new IntegerList(true);
            if (base.IsCurrText('['))
            {
                this.Match('[');
                if (!base.IsCurrText(']'))
                {
                    this.Parse_ParameterList(num, false);
                }
                this.Match(']');
            }
            base.DECLARE_SWITCH = false;
            this.Match(":");
            int num2 = this.Parse_Type();
            base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, num, -1, -1);
            this.Gen(base.code.OP_ASSIGN_TYPE, num, num2, 0);
            int id = 0;
            int iD = 0;
            if (base.IsCurrText("read"))
            {
                this.Call_SCANNER();
                id = this.Parse_Ident();
                string name = base.GetName(id);
                base.DiscardInstruction(base.code.OP_EVAL, 0, 0, id);
                base.SetName(id, "");
                id = base.LookupID(name);
                if (id == 0)
                {
                    base.RaiseErrorEx(true, "Undeclared identifier '{0}'", new object[] { name });
                }
            }
            if (base.IsCurrText("write"))
            {
                this.Call_SCANNER();
                iD = this.Parse_Ident();
                string s = base.GetName(iD);
                base.DiscardInstruction(base.code.OP_EVAL, 0, 0, iD);
                base.SetName(iD, "");
                iD = base.LookupID(s);
                if (iD == 0)
                {
                    base.RaiseErrorEx(true, "Undeclared identifier '{0}'", new object[] { s });
                }
            }
            base.BeginProperty(num, ml, num2, 0);
            if (id > 0)
            {
                this.valid_this_context = true;
                int num5 = base.NewVar();
                base.SetName(num5, "get_" + base.GetName(num));
                this.BeginMethod(num5, MemberKind.Method, ml, num2);
                list.Clear();
                for (int i = 0; i < this.param_ids.Count; i++)
                {
                    base.DiscardInstruction(base.code.OP_ADD_PARAM, num, -1, -1);
                    int num7 = base.NewVar();
                    base.SetName(num7, base.GetName(this.param_ids[i]));
                    this.Gen(base.code.OP_ASSIGN_TYPE, num7, this.param_type_ids[i], 0);
                    this.Gen(base.code.OP_ADD_PARAM, num5, num7, 0);
                    list.Add(num7);
                }
                this.InitMethod(num5);
                if (base.GetKind(id) == MemberKind.Method)
                {
                    int res = base.NewRef(base.GetName(id));
                    this.Gen(base.code.OP_CREATE_REFERENCE, base.CurrThisID, 0, res);
                    this.Gen(base.code.OP_BEGIN_CALL, res, 0, 0);
                    for (int j = 0; j < this.param_ids.Count; j++)
                    {
                        this.Gen(base.code.OP_PUSH, list[j], 0, res);
                    }
                    this.Gen(base.code.OP_PUSH, base.CurrThisID, 0, 0);
                    this.Gen(base.code.OP_CALL, res, this.param_ids.Count, base.CurrResultId);
                }
                else if (base.GetKind(id) == MemberKind.Field)
                {
                    if (this.param_ids.Count > 0)
                    {
                        base.RaiseError(false, "PAS0002. Incompatible types");
                    }
                    int num10 = base.NewRef(base.GetName(id));
                    this.Gen(base.code.OP_CREATE_REFERENCE, base.CurrThisID, 0, num10);
                    this.Gen(base.code.OP_ASSIGN, base.CurrResultId, num10, base.CurrResultId);
                }
                else
                {
                    base.RaiseError(false, "PAS0001. Field or method identifier expected");
                }
                this.EndMethod(num5);
                this.Gen(base.code.OP_ADD_READ_ACCESSOR, num, num5, 0);
                this.valid_this_context = false;
            }
            if (iD > 0)
            {
                int num12;
                this.valid_this_context = true;
                int num11 = base.NewVar();
                base.SetName(num11, "set_" + base.GetName(num));
                this.BeginMethod(num11, MemberKind.Method, ml, num2);
                list.Clear();
                for (int k = 0; k < this.param_ids.Count; k++)
                {
                    base.DiscardInstruction(base.code.OP_ADD_PARAM, num, -1, -1);
                    num12 = base.NewVar();
                    base.SetName(num12, base.GetName(this.param_ids[k]));
                    this.Gen(base.code.OP_ASSIGN_TYPE, num12, this.param_type_ids[k], 0);
                    this.Gen(base.code.OP_ADD_PARAM, num11, num12, 0);
                    list.Add(num12);
                }
                num12 = base.NewVar();
                base.SetName(num12, "value");
                this.Gen(base.code.OP_ADD_PARAM, num11, num12, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, num12, num2, 0);
                list.Add(num12);
                this.InitMethod(num11);
                if (base.GetKind(iD) == MemberKind.Method)
                {
                    int num14 = base.NewRef(base.GetName(iD));
                    this.Gen(base.code.OP_CREATE_REFERENCE, base.CurrThisID, 0, num14);
                    this.Gen(base.code.OP_BEGIN_CALL, num14, 0, 0);
                    for (int m = 0; m < list.Count; m++)
                    {
                        this.Gen(base.code.OP_PUSH, list[m], 0, num14);
                    }
                    this.Gen(base.code.OP_PUSH, base.CurrThisID, 0, 0);
                    this.Gen(base.code.OP_CALL, num14, list.Count, base.CurrResultId);
                }
                else if (base.GetKind(iD) == MemberKind.Field)
                {
                    if (this.param_ids.Count > 0)
                    {
                        base.RaiseError(false, "PAS0002. Incompatible types");
                    }
                    int num16 = base.NewRef(base.GetName(iD));
                    this.Gen(base.code.OP_CREATE_REFERENCE, base.CurrThisID, 0, num16);
                    this.Gen(base.code.OP_ASSIGN, num16, num12, num16);
                }
                else
                {
                    base.RaiseError(false, "PAS0001. Field or method identifier expected");
                }
                this.EndMethod(num11);
                this.Gen(base.code.OP_ADD_WRITE_ACCESSOR, num, num11, 0);
                this.valid_this_context = false;
            }
            this.Match(";");
            if (base.IsCurrText("default"))
            {
                this.Call_SCANNER();
                if (this.param_ids.Count == 0)
                {
                    base.RaiseError(false, "VB00004. Properties with no required parameters cannot be declared 'Default'.");
                }
                else
                {
                    this.Gen(base.code.OP_SET_DEFAULT, num, 0, 0);
                }
                this.Match(";");
            }
            base.EndProperty(num);
            base.DECLARE_SWITCH = true;
        }

        private void Parse_RecordTypeDeclaration(int struct_id, ModifierList ml)
        {
            base.CheckModifiers(ml, this.structure_modifiers);
            base.BeginStruct(struct_id, ml);
            this.Match("record");
            this.Gen(base.code.OP_ADD_ANCESTOR, struct_id, base.ObjectClassId, 0);
            this.Parse_ClassBody(struct_id, ml, false);
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(struct_id);
            }
            this.CreateDefaultConstructor(struct_id, true);
            base.EndStruct(struct_id);
            this.Match("end");
        }

        private void Parse_RepeatStatement()
        {
            this.Match("repeat");
            int l = base.NewLabel();
            int label = base.NewLabel();
            base.SetLabelHere(l);
            do
            {
                if (base.IsCurrText("until") || base.IsEOF())
                {
                    break;
                }
                base.BreakStack.Push(label);
                base.ContinueStack.Push(l);
                this.Parse_Statement();
                base.BreakStack.Pop();
                base.ContinueStack.Pop();
            }
            while (!this.NotMatch(';'));
            this.Match("until");
            this.Gen(base.code.OP_GO_FALSE, l, this.Parse_Expression(), 0);
            base.SetLabelHere(label);
        }

        public int Parse_SimpleExpression()
        {
            int num = this.Parse_Term();
            while ((base.IsCurrText('+') || base.IsCurrText('-')) || (base.IsCurrText("or") || base.IsCurrText("xor")))
            {
                if (base.IsCurrText('+'))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_PLUS, num, this.Parse_Term());
                }
                else
                {
                    if (base.IsCurrText('-'))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_MINUS, num, this.Parse_Term());
                        continue;
                    }
                    if (base.IsCurrText("or"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_BITWISE_OR, num, this.Parse_Term());
                        continue;
                    }
                    if (base.IsCurrText("xor"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_BITWISE_XOR, num, this.Parse_Term());
                    }
                }
            }
            return num;
        }

        private int Parse_SimpleNameExpression()
        {
            return this.Parse_IdentOrType();
        }

        private void Parse_Start()
        {
            do
            {
                this.Parse_NamespaceMemberDeclaration();
            }
            while (!base.IsEOF());
        }

        private void Parse_Statement()
        {
            int k = base.ReadToken();
            if (base.IsCurrText(':'))
            {
                base.Backup_SCANNER(k);
                int num2 = base.Parse_NewLabel();
                this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, num2, base.CurrSubId, 0);
                base.SetLabelHere(num2);
                this.Match(':');
            }
            else
            {
                base.Backup_SCANNER(k);
            }
            if (base.IsCurrText("begin"))
            {
                this.Parse_CompoundStatement();
            }
            else if (base.IsCurrText("if"))
            {
                this.Parse_IfStatement();
            }
            else if (base.IsCurrText("case"))
            {
                this.Parse_CaseStatement();
            }
            else if (base.IsCurrText("goto"))
            {
                this.Parse_GotoStatement();
            }
            else if (base.IsCurrText("break"))
            {
                this.Parse_BreakStatement();
            }
            else if (base.IsCurrText("continue"))
            {
                this.Parse_ContinueStatement();
            }
            else if (base.IsCurrText("exit"))
            {
                this.Parse_ExitStatement();
            }
            else if (base.IsCurrText("while"))
            {
                this.Parse_WhileStatement();
            }
            else if (base.IsCurrText("repeat"))
            {
                this.Parse_RepeatStatement();
            }
            else if (base.IsCurrText("for"))
            {
                this.Parse_ForStatement();
            }
            else if (base.IsCurrText("print"))
            {
                this.Parse_PrintStatement();
            }
            else if (base.IsCurrText("println"))
            {
                this.Parse_PrintlnStatement();
            }
            else
            {
                this.Parse_AssignmentStatement();
            }
        }

        private void Parse_Statements()
        {
            do
            {
                if (base.IsEOF() || base.IsCurrText("End"))
                {
                    break;
                }
                this.Parse_Statement();
            }
            while (!this.NotMatch(';'));
        }

        private void Parse_SubrangeTypeDeclaration(int type_id, StandardType type_base)
        {
            base.BeginSubrange(type_id, type_base);
            this.Gen(base.code.OP_ADD_MIN_VALUE, type_id, this.Parse_Expression(), 0);
            this.Match("..");
            this.Gen(base.code.OP_ADD_MAX_VALUE, type_id, this.Parse_Expression(), 0);
            base.EndSubrange(type_id);
        }

        public int Parse_Term()
        {
            int num = this.Parse_Factor();
            while (((base.IsCurrText('*') || base.IsCurrText('/')) || (base.IsCurrText("div") || base.IsCurrText("mod"))) || ((base.IsCurrText("and") || base.IsCurrText("shl")) || base.IsCurrText("shr")))
            {
                if (base.IsCurrText('*'))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_MULT, num, this.Parse_Factor());
                }
                else
                {
                    if (base.IsCurrText('/'))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_DIV, num, this.Parse_Factor());
                        continue;
                    }
                    if (base.IsCurrText("div"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_DIV, num, this.Parse_Factor());
                        continue;
                    }
                    if (base.IsCurrText("mod"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_MOD, num, this.Parse_Factor());
                        continue;
                    }
                    if (base.IsCurrText("and"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_BITWISE_AND, num, this.Parse_Factor());
                        continue;
                    }
                    if (base.IsCurrText("shl"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_LEFT_SHIFT, num, this.Parse_Factor());
                        continue;
                    }
                    if (base.IsCurrText("shr"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_RIGHT_SHIFT, num, this.Parse_Factor());
                    }
                }
            }
            return num;
        }

        private int Parse_Type()
        {
            int num;
            ModifierList ml = new ModifierList();
            if (base.IsCurrText("packed"))
            {
                this.Call_SCANNER();
            }
            if (base.IsCurrText('('))
            {
                num = base.NewVar();
                this.Parse_EnumTypeDeclaration(num);
                return num;
            }
            if (base.curr_token.tokenClass == TokenClass.IntegerConst)
            {
                num = base.NewVar();
                this.Parse_SubrangeTypeDeclaration(num, StandardType.Int);
                return num;
            }
            if (base.curr_token.tokenClass == TokenClass.CharacterConst)
            {
                num = base.NewVar();
                this.Parse_SubrangeTypeDeclaration(num, StandardType.Char);
                return num;
            }
            if (base.curr_token.tokenClass == TokenClass.BooleanConst)
            {
                num = base.NewVar();
                this.Parse_SubrangeTypeDeclaration(num, StandardType.Bool);
                return num;
            }
            if (base.IsCurrText("record"))
            {
                num = this.NewTempVar();
                base.DECLARE_SWITCH = true;
                this.Parse_RecordTypeDeclaration(num, ml);
                base.DECLARE_SWITCH = false;
                return num;
            }
            if (base.IsCurrText("array"))
            {
                num = this.NewTempVar();
                base.DECLARE_SWITCH = true;
                this.Parse_ArrayTypeDeclaration(num, ml);
                base.DECLARE_SWITCH = false;
                return num;
            }
            num = this.Parse_NonArrayType();
            if (base.IsCurrText('['))
            {
                string str = this.Parse_TypeNameModifier();
                string str2 = base.GetName(num) + str;
                num = base.NewVar();
                base.SetName(num, str2);
                this.Gen(base.code.OP_EVAL_TYPE, 0, 0, num);
            }
            return num;
        }

        private void Parse_TypeDeclaration()
        {
            ModifierList ml = new ModifierList();
            if (base.IsCurrText("Program"))
            {
                this.Parse_ProgramDeclaration(ml);
            }
            else if (base.IsCurrText("Unit"))
            {
                this.Parse_UnitDeclaration(ml);
            }
            else
            {
                this.Parse_NonProgramDeclaration(ml);
            }
        }

        private void Parse_TypeDeclaration(int class_id, ModifierList owner_modifiers)
        {
            bool flag;
            ModifierList ml = new ModifierList();
            ml.Add(Modifier.Public);
            base.DECLARE_SWITCH = true;
            this.Match("type");
            do
            {
                flag = false;
                int num = this.Parse_Ident();
                base.DECLARE_SWITCH = false;
                this.Match('=');
                if (base.IsCurrText('('))
                {
                    base.DECLARE_SWITCH = true;
                    this.Parse_EnumTypeDeclaration(num);
                    base.DECLARE_SWITCH = true;
                    this.Match(';');
                    flag = true;
                }
                else if (base.curr_token.tokenClass == TokenClass.IntegerConst)
                {
                    this.Parse_SubrangeTypeDeclaration(num, StandardType.Int);
                    base.DECLARE_SWITCH = true;
                    this.Match(';');
                    flag = true;
                }
                else if (base.curr_token.tokenClass == TokenClass.CharacterConst)
                {
                    this.Parse_SubrangeTypeDeclaration(num, StandardType.Char);
                    base.DECLARE_SWITCH = true;
                    this.Match(';');
                    flag = true;
                }
                else if (base.curr_token.tokenClass == TokenClass.BooleanConst)
                {
                    this.Parse_SubrangeTypeDeclaration(num, StandardType.Bool);
                    base.DECLARE_SWITCH = true;
                    this.Match(';');
                    flag = true;
                }
                else if (base.IsCurrText("class"))
                {
                    base.DECLARE_SWITCH = true;
                    this.Parse_ClassTypeDeclaration(num, ml);
                    base.DECLARE_SWITCH = true;
                    this.Match(';');
                    flag = true;
                }
                else if (base.IsCurrText("record"))
                {
                    base.DECLARE_SWITCH = true;
                    this.Parse_RecordTypeDeclaration(num, ml);
                    base.DECLARE_SWITCH = true;
                    this.Match(';');
                    flag = true;
                }
                else if (base.IsCurrText("array"))
                {
                    base.DECLARE_SWITCH = true;
                    this.Parse_ArrayTypeDeclaration(num, ml);
                    base.DECLARE_SWITCH = true;
                    this.Match(';');
                    flag = true;
                }
            }
            while (flag && (base.curr_token.tokenClass != TokenClass.Keyword));
        }

        private string Parse_TypeNameModifier()
        {
            string str = "";
            this.Match('[');
            str = str + "[";
            if (!base.IsCurrText(']'))
            {
                while (true)
                {
                    if (!base.IsCurrText(','))
                    {
                        this.Parse_Expression();
                    }
                    if (!base.CondMatch(','))
                    {
                        break;
                    }
                    str = str + ",";
                }
            }
            this.Match(']');
            return (str + "]");
        }

        private void Parse_UnitDeclaration(ModifierList ml)
        {
            base.DECLARE_SWITCH = true;
            this.Match("Unit");
            int num = this.Parse_Ident();
            this.Match(';');
            this.Parse_InterfaceSection(num);
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(num);
            }
            this.Parse_ImplementationSection(num);
            this.Parse_InitSection(num);
            base.EndClass(num);
            this.Match('.');
        }

        private void Parse_UsesClause()
        {
            int id = this.Parse_Ident();
            if (base.IsCurrText('='))
            {
                this.Match('=');
                int res = this.Parse_NamespaceOrTypeName();
                int level = base.GetLevel(id);
                this.Gen(base.code.OP_CREATE_USING_ALIAS, id, level, res);
                this.Gen(base.code.OP_BEGIN_USING, id, 0, 0);
            }
            else
            {
                string val;
                this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
                while (true)
                {
                    base.REF_SWITCH = true;
                    if (!base.CondMatch('.'))
                    {
                        break;
                    }
                    int num4 = id;
                    id = this.Parse_Ident();
                    this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, num4, 0, id);
                }
                base.REF_SWITCH = false;
                this.Gen(base.code.OP_BEGIN_USING, id, 0, 0);
                if (base.IsCurrText("in"))
                {
                    this.Call_SCANNER();
                    int num5 = this.Parse_StringLiteral();
                    val = (string) base.GetVal(num5);
                }
                else
                {
                    val = base.GetName(id) + ".pas";
                }
                base.AddModuleFromFile(val);
            }
        }

        private void Parse_UsesStatement()
        {
            this.Match("Uses");
            do
            {
                this.Parse_UsesClause();
            }
            while (base.CondMatch(','));
            base.DECLARE_SWITCH = false;
            this.Match(';');
        }

        private void Parse_VariableDeclarator(ModifierList ml, bool IsModule)
        {
            base.DECLARE_SWITCH = false;
            ArrayList list = new ArrayList();
            ArrayList sl = new ArrayList();
            IntegerList list3 = this.Parse_VariableIdentifiers(list, sl);
            this.Match(":");
            int num = this.Parse_Type();
            for (int i = 0; i < list3.Count; i++)
            {
                int num3 = list3[i];
                if (((string) sl[i]) != "")
                {
                    base.BeginField(num3, ml, num);
                    int num4 = base.NewVar();
                    this.BeginMethod(num4, MemberKind.Method, ml, 1);
                    this.InitMethod(num4);
                    int id = num3;
                    if (!ml.HasModifier(Modifier.Static))
                    {
                        id = base.NewVar();
                        base.SetName(id, base.GetName(num3));
                        this.Gen(base.code.OP_EVAL, 0, 0, id);
                        this.variable_initializers.Add(num4);
                    }
                    else
                    {
                        this.static_variable_initializers.Add(num4);
                    }
                    string str = (string) sl[i];
                    int num6 = num;
                    int num7 = base.NewVar();
                    base.SetName(num7, base.GetName(num6) + str);
                    this.Gen(base.code.OP_EVAL_TYPE, num6, 0, num7);
                    this.Gen(base.code.OP_ASSIGN_TYPE, id, num7, 0);
                    this.Gen(base.code.OP_ASSIGN_TYPE, num3, num7, 0);
                    IntegerList list4 = (IntegerList) list[i];
                    this.Gen(base.code.OP_CREATE_OBJECT, num7, 0, id);
                    int count = list4.Count;
                    if (count > 0)
                    {
                        this.Gen(base.code.OP_BEGIN_CALL, num7, 0, 0);
                        for (int j = 0; j < count; j++)
                        {
                            this.Gen(base.code.OP_INC, list4[j], 0, list4[j]);
                            this.Gen(base.code.OP_PUSH, list4[j], 0, num7);
                        }
                        this.Gen(base.code.OP_PUSH, id, 0, 0);
                        this.Gen(base.code.OP_CALL, num7, count, 0);
                    }
                    num = num7;
                    this.EndMethod(num4);
                    base.EndField(num3);
                }
                else
                {
                    this.Gen(base.code.OP_ASSIGN_TYPE, num3, num, 0);
                    base.BeginField(num3, ml, num);
                    base.EndField(num3);
                }
            }
            if (base.IsCurrText("="))
            {
                this.Match('=');
                if (list3.Count == 1)
                {
                    int num10 = list3[0];
                    this.Gen(base.code.OP_ASSIGN_TYPE, num10, num, 0);
                    this.Parse_VariableInitializer(num10, num, ml);
                }
                else
                {
                    base.RaiseError(true, "VB00002. Explicit initialization is not permitted with multiple variables declared with a single type specifier.");
                }
            }
        }

        private int Parse_VariableIdentifier(IntegerList bounds, out string s)
        {
            int num = this.Parse_Ident();
            if (base.IsCurrText('['))
            {
                s = this.Parse_ArrayNameModifier(bounds);
                return num;
            }
            s = "";
            return num;
        }

        private IntegerList Parse_VariableIdentifiers(ArrayList bounds_list, ArrayList sl)
        {
            IntegerList list = new IntegerList(false);
            while (true)
            {
                string str;
                IntegerList bounds = new IntegerList(true);
                int avalue = this.Parse_VariableIdentifier(bounds, out str);
                list.Add(avalue);
                bounds_list.Add(bounds);
                sl.Add(str);
                if (!base.CondMatch(','))
                {
                    return list;
                }
            }
        }

        private void Parse_VariableInitializer(int id, int type_id, ModifierList ml)
        {
            base.DECLARE_SWITCH = false;
            int num = base.NewVar();
            this.BeginMethod(num, MemberKind.Method, ml, 1);
            this.InitMethod(num);
            int num2 = id;
            if (!ml.HasModifier(Modifier.Static))
            {
                num2 = base.NewVar();
                base.SetName(num2, base.GetName(id));
                this.Gen(base.code.OP_EVAL, 0, 0, num2);
                this.variable_initializers.Add(num);
            }
            else
            {
                this.static_variable_initializers.Add(num);
            }
            if (base.IsCurrText('['))
            {
                this.Gen(base.code.OP_ASSIGN, num2, this.Parse_ArrayInitializer(), num2);
            }
            else
            {
                this.Gen(base.code.OP_ASSIGN, num2, this.Parse_Expression(), num2);
            }
            this.EndMethod(num);
            base.DECLARE_SWITCH = true;
        }

        private void Parse_VariableMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_modifiers, bool IsModule)
        {
            if (base.IsCurrText("var"))
            {
                this.Match("var");
            }
            do
            {
                this.Parse_VariableDeclarator(ml, IsModule);
                this.Match(";");
            }
            while (base.curr_token.tokenClass == TokenClass.Identifier);
        }

        private void Parse_WhileStatement()
        {
            this.Match("while");
            int num = base.NewLabel();
            int l = base.NewLabel();
            base.SetLabelHere(l);
            this.Gen(base.code.OP_GO_FALSE, num, this.Parse_Expression(), 0);
            this.Match("do");
            base.BreakStack.Push(num);
            base.ContinueStack.Push(l);
            this.Parse_Statement();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num);
        }

        public enum Directive
        {
            Overload,
            Forward
        }

        public enum LocalModifier
        {
            Var,
            Const
        }
    }
}

