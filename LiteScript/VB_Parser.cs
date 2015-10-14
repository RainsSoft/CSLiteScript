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

    internal class VB_Parser : BaseParser
    {
        private ModifierList class_modifiers;
        private ModifierList constructor_modifiers;
        private int curr_prop_id = 0;
        private ModifierList delegate_modifiers;
        private ModifierList enum_modifiers;
        private ModifierList event_modifiers;
        private IntegerStack exit_kind_stack;
        private int explicit_intf_id = 0;
        private ForLoopStack for_loop_stack;
        private bool has_constructor = false;
        private CSLite_Types integral_types;
        private ModifierList interface_modifiers;
        private IntegerList local_variables;
        private ModifierList method_modifiers;
        private int new_type_id = 0;
        private bool OPTION_STRICT = true;
        private IntegerList param_ids;
        private IntegerList param_mods;
        private IntegerList param_type_ids;
        private ModifierList property_modifiers;
        private bool SKIP_STATEMENT_TERMINATOR = false;
        private IntegerList static_variable_initializers;
        private ModifierList structure_modifiers;
        private StringList total_modifier_list;
        private bool typeof_expression = false;
        private bool valid_this_context = false;
        private IntegerList variable_initializers;
        private IntegerStack with_stack;

        public VB_Parser()
        {
            base.language = "VB";
            base.scanner = new VB_Scanner(this);
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
            this.total_modifier_list.AddObject("Overridable", Modifier.Virtual);
            this.total_modifier_list.AddObject("NotOverridable", Modifier.Sealed);
            this.total_modifier_list.AddObject("MustOverride", Modifier.Abstract);
            this.total_modifier_list.AddObject("Overrides", Modifier.Override);
            this.total_modifier_list.AddObject("Overloads", Modifier.Overloads);
            this.total_modifier_list.AddObject("ReadOnly", Modifier.ReadOnly);
            this.total_modifier_list.AddObject("Friend", Modifier.Friend);
            this.total_modifier_list.AddObject("Default", Modifier.Default);
            this.total_modifier_list.AddObject("MustInherit", Modifier.Abstract);
            this.total_modifier_list.AddObject("Shadows", Modifier.Shadows);
            this.total_modifier_list.AddObject("NotInheritable", Modifier.Sealed);
            this.total_modifier_list.AddObject("WithEvents", Modifier.WithEvents);
            base.keywords.Add("AddHandler");
            base.keywords.Add("AddressOf");
            base.keywords.Add("Alias");
            base.keywords.Add("And");
            base.keywords.Add("AndAlso");
            base.keywords.Add("Ansi");
            base.keywords.Add("As");
            base.keywords.Add("Assembly");
            base.keywords.Add("Auto");
            base.keywords.Add("Boolean");
            base.keywords.Add("ByRef");
            base.keywords.Add("Byte");
            base.keywords.Add("ByVal");
            base.keywords.Add("Call");
            base.keywords.Add("Case");
            base.keywords.Add("Catch");
            base.keywords.Add("CBool");
            base.keywords.Add("CByte");
            base.keywords.Add("CChar");
            base.keywords.Add("CDate");
            base.keywords.Add("CDbl");
            base.keywords.Add("CDec");
            base.keywords.Add("Char");
            base.keywords.Add("CInt");
            base.keywords.Add("Class");
            base.keywords.Add("CLng");
            base.keywords.Add("CObj");
            base.keywords.Add("Const");
            base.keywords.Add("CShort");
            base.keywords.Add("CSng");
            base.keywords.Add("CStr");
            base.keywords.Add("CType");
            base.keywords.Add("Date");
            base.keywords.Add("Decimal");
            base.keywords.Add("Declare");
            base.keywords.Add("Default");
            base.keywords.Add("Delegate");
            base.keywords.Add("Dim");
            base.keywords.Add("DirectCast");
            base.keywords.Add("Do");
            base.keywords.Add("Double");
            base.keywords.Add("Each");
            base.keywords.Add("Else");
            base.keywords.Add("ElseIf");
            base.keywords.Add("End");
            base.keywords.Add("EndIf");
            base.keywords.Add("Enum");
            base.keywords.Add("Erase");
            base.keywords.Add("Error");
            base.keywords.Add("Event");
            base.keywords.Add("Exit");
            base.keywords.Add("False");
            base.keywords.Add("Finally");
            base.keywords.Add("For");
            base.keywords.Add("Friend");
            base.keywords.Add("Function");
            base.keywords.Add("Get");
            base.keywords.Add("GoSub");
            base.keywords.Add("GoTo");
            base.keywords.Add("Handles");
            base.keywords.Add("If");
            base.keywords.Add("Implements");
            base.keywords.Add("Imports");
            base.keywords.Add("In");
            base.keywords.Add("Inherits");
            base.keywords.Add("Integer");
            base.keywords.Add("Interface");
            base.keywords.Add("Is");
            base.keywords.Add("Let");
            base.keywords.Add("Lib");
            base.keywords.Add("Like");
            base.keywords.Add("Long");
            base.keywords.Add("Loop");
            base.keywords.Add("Me");
            base.keywords.Add("Mod");
            base.keywords.Add("Module");
            base.keywords.Add("MustInherit");
            base.keywords.Add("MustOverride");
            base.keywords.Add("MyBase");
            base.keywords.Add("MyClass");
            base.keywords.Add("Namespace");
            base.keywords.Add("New");
            base.keywords.Add("Next");
            base.keywords.Add("Not");
            base.keywords.Add("Nothing");
            base.keywords.Add("NotInheritable");
            base.keywords.Add("NotOverridable");
            base.keywords.Add("Object");
            base.keywords.Add("On");
            base.keywords.Add("Option");
            base.keywords.Add("Optional");
            base.keywords.Add("Or");
            base.keywords.Add("Else");
            base.keywords.Add("Overloads");
            base.keywords.Add("Overridable");
            base.keywords.Add("Overrides");
            base.keywords.Add("ParamArray");
            base.keywords.Add("Preserve");
            base.keywords.Add("Private");
            base.keywords.Add("Property");
            base.keywords.Add("Protected");
            base.keywords.Add("Public");
            base.keywords.Add("RaiseEvent");
            base.keywords.Add("ReadOnly");
            base.keywords.Add("ReDim");
            base.keywords.Add("REM");
            base.keywords.Add("RemoveHandler");
            base.keywords.Add("Resume");
            base.keywords.Add("Return");
            base.keywords.Add("Select");
            base.keywords.Add("Set");
            base.keywords.Add("Shadows");
            base.keywords.Add("Shared");
            base.keywords.Add("Short");
            base.keywords.Add("Single");
            base.keywords.Add("Static");
            base.keywords.Add("Step");
            base.keywords.Add("Stop");
            base.keywords.Add("String");
            base.keywords.Add("Structure");
            base.keywords.Add("Sub");
            base.keywords.Add("SyncLock");
            base.keywords.Add("Then");
            base.keywords.Add("Throw");
            base.keywords.Add("To");
            base.keywords.Add("True");
            base.keywords.Add("Try");
            base.keywords.Add("TypeOf");
            base.keywords.Add("Unicode");
            base.keywords.Add("Until");
            base.keywords.Add("Variant");
            base.keywords.Add("Wend");
            base.keywords.Add("When");
            base.keywords.Add("While");
            base.keywords.Add("With");
            base.keywords.Add("WithEvents");
            base.keywords.Add("WriteOnly");
            base.keywords.Add("Xor");
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
            this.for_loop_stack = new ForLoopStack();
            this.exit_kind_stack = new IntegerStack();
            this.with_stack = new IntegerStack();
        }

        public override void Call_SCANNER()
        {
            base.Call_SCANNER();
            if (base.IsCurrText("Date"))
            {
                base.curr_token.id = base.DATETIME_CLASS_id;
                base.curr_token.tokenClass = TokenClass.Identifier;
            }
            if (base.IsCurrText('_'))
            {
                this.Call_SCANNER();
                while (this.IsLineTerminator())
                {
                    this.MatchLineTerminator();
                }
            }
        }

        private void CheckMethodModifiers(int id, int class_id, ModifierList ml, ModifierList owner_ml)
        {
            base.CheckModifiers(ml, this.method_modifiers);
            if (ml.HasModifier(Modifier.Abstract) && !owner_ml.HasModifier(Modifier.Abstract))
            {
                string name = base.GetName(id);
                string str2 = base.GetName(class_id);
                base.RaiseErrorEx(false, "CS0513. '{0}' is abstract but it is contained in nonabstract class '{1}'", new object[] { name, str2 });
            }
            if (ml.HasModifier(Modifier.Static) && ((ml.HasModifier(Modifier.Abstract) || ml.HasModifier(Modifier.Virtual)) || ml.HasModifier(Modifier.Override)))
            {
                base.RaiseErrorEx(false, "CS0112. A static member '{0}' cannot be marked as override, virtual or abstract.", new object[] { base.GetName(id) });
            }
            if (ml.HasModifier(Modifier.Override) && (ml.HasModifier(Modifier.Virtual) || ml.HasModifier(Modifier.New)))
            {
                base.RaiseErrorEx(false, "CS0113. A member '{0}' marked as override cannot be marked as new or virtual.", new object[] { base.GetName(id) });
            }
            if (ml.HasModifier(Modifier.Extern) && ml.HasModifier(Modifier.Abstract))
            {
                base.RaiseErrorEx(false, "CS0180. '{0}' cannot be both extern and abstract.", new object[] { base.GetName(id) });
            }
            if (ml.HasModifier(Modifier.Sealed) && !ml.HasModifier(Modifier.Override))
            {
                base.RaiseErrorEx(false, "CS0238. '{0}' cannot be sealed because it is not an override.", new object[] { base.GetName(id) });
            }
            if (ml.HasModifier(Modifier.Abstract) && ml.HasModifier(Modifier.Virtual))
            {
                base.RaiseErrorEx(false, "CS0503. The abstract method '{0}' cannot be marked virtual.", new object[] { base.GetName(id) });
            }
            if (ml.HasModifier(Modifier.Virtual) && owner_ml.HasModifier(Modifier.Sealed))
            {
                base.RaiseErrorEx(false, "CS0549. '{0}' is a new virtual member in sealed class '{1}'.", new object[] { base.GetName(id) });
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

        public override void Gen(int op, int arg1, int arg2, int res)
        {
            base.Gen(op, arg1, arg2, res);
            base.SetUpcase(true);
        }

        private bool HasAccessModifier(ModifierList ml)
        {
            return ((ml.HasModifier(Modifier.Public) || ml.HasModifier(Modifier.Private)) || ml.HasModifier(Modifier.Protected));
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
            this.explicit_intf_id = 0;
            this.curr_prop_id = 0;
            this.new_type_id = 0;
            this.SKIP_STATEMENT_TERMINATOR = false;
            this.OPTION_STRICT = true;
            this.typeof_expression = false;
            this.for_loop_stack.Clear();
            this.exit_kind_stack.Clear();
            this.with_stack.Clear();
        }

        private bool IsLineTerminator()
        {
            return (base.scanner.IsNewLine(base.scanner.LA(0)) || base.IsEOF());
        }

        private bool IsStatementTerminator()
        {
            return ((this.IsLineTerminator() || base.IsCurrText(':')) || base.IsEOF());
        }

        private void MatchLineTerminator()
        {
            if (!base.IsEOF())
            {
                if (!base.scanner.IsNewLine(base.scanner.LA(0)))
                {
                    base.RaiseError(true, "Line terminator expected");
                }
                while (base.scanner.IsNewLine(base.scanner.LA(0)))
                {
                    base.scanner.p++;
                    if (base.scanner.LA(0) == '\n')
                    {
                        base.scanner.p++;
                    }
                    base.scanner.IncLineNumber();
                    base.scanner.p--;
                    this.Call_SCANNER();
                }
            }
        }

        private void MatchStatementTerminator()
        {
            if (!base.IsEOF())
            {
                if (!this.SKIP_STATEMENT_TERMINATOR && !base.scanner.IsNewLine(base.scanner.LA(0)))
                {
                    base.RaiseError(true, "Statement terminator expected");
                }
                while (base.scanner.IsNewLine(base.scanner.LA(0)))
                {
                    base.scanner.p++;
                    if (base.scanner.LA(0) == '\n')
                    {
                        base.scanner.p++;
                    }
                    base.scanner.IncLineNumber();
                    base.scanner.p--;
                    this.Call_SCANNER();
                }
            }
        }

        private void Parse_AddHandlerStatement()
        {
            this.Match("AddHandler");
            int num = this.Parse_Expression();
            this.Match(',');
            int num2 = this.Parse_Expression();
            int res = base.NewVar();
            this.Gen(base.code.OP_PLUS, num, num2, res);
            this.Gen(base.code.OP_ASSIGN, num, res, num);
            this.MatchStatementTerminator();
        }

        private int Parse_AdditiveExpression()
        {
            int num = this.Parse_ModulusExpression();
            while (base.IsCurrText('+') || base.IsCurrText('-'))
            {
                if (base.IsCurrText('+'))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_PLUS, num, this.Parse_ModulusExpression());
                }
                else
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_MINUS, num, this.Parse_ModulusExpression());
                }
            }
            return num;
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

        private int Parse_ArrayInitializer(int array_type_id)
        {
            string name = base.GetName(array_type_id);
            int rank = CSLite_System.GetRank(name);
            base.scripter.Dump();
            string elementTypeName = CSLite_System.GetElementTypeName(name);
            int id = base.NewVar();
            base.SetName(id, elementTypeName);
            this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
            int res = base.NewVar();
            this.Gen(base.code.OP_CREATE_OBJECT, array_type_id, 0, res);
            this.Gen(base.code.OP_BEGIN_CALL, array_type_id, 0, 0);
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
                this.Gen(base.code.OP_PUSH, avalue, 0, array_type_id);
            }
            this.Gen(base.code.OP_PUSH, res, 0, 0);
            this.Gen(base.code.OP_CALL, array_type_id, rank, 0);
            int num6 = -1;
        Label_011E:
            if (base.IsCurrText('{'))
            {
                IntegerList list8;
                int num16;
                this.Match('{');
                num6++;
                if (num6 == (rank - 1))
                {
                    list3[num6] = -1;
                    while (true)
                    {
                        IntegerList list6=null;
                        int num14;
                        if (base.IsCurrText('}'))
                        {
                            goto Label_011E;
                        }
                        if (list4[num6] == 0)
                        {
                            IntegerList list5= list2;
                            int num13= num6;
                            //(list5 = list2)[num13 = num6] = list5[num13] + 1;
                            list5[num13] = list5[num13] + 1;
                        }
                        list6 = list3;
                        num14 = num6;
                        list6[num14] = list6[num14] + 1;
                        int num7 = base.NewVar();
                        this.Gen(base.code.OP_CREATE_INDEX_OBJECT, res, 0, num7);
                        for (int j = 0; j < rank; j++)
                        {
                            int num9 = base.NewConst(list3[j]);
                            this.Gen(base.code.OP_ADD_INDEX, num7, num9, res);
                        }
                        this.Gen(base.code.OP_SETUP_INDEX_OBJECT, num7, 0, 0);
                        this.Gen(base.code.OP_ASSIGN, num7, this.Parse_Expression(), num7);
                        if (!base.CondMatch(','))
                        {
                            goto Label_011E;
                        }
                    }
                }
                if (list4[num6] == 0)
                {
                    IntegerList list7= list2;
                    int num15= num6;
                    //(list7 = list2)[num15 = num6] = list7[num15] + 1;
                    list7[num15] = list7[num15] + 1;
                }

                list8 = list3;
                num16 = num6;
                //(list8 = list3)[num16 = num6] = list8[num16] + 1;
                list8[num16] = list8[num16] + 1;
            }
            else if (base.IsCurrText(','))
            {
                IntegerList list10=null;
                int num18;
                this.Match(',');
                if (list4[num6] == 0)
                {
                    IntegerList list9 = list2;
                    int num17 = num6;
                    //(list9 = list2)[num17 = num6] = list9[num17] + 1;
                    list9[num17] = list9[num17] + 1;
                }
                list10 = list3;
                num18 = num6;
                //(list10 = list3)[num18 = num6] = list10[num18] + 1;
                list10[num18] = list10[num18] + 1;
                for (int k = num6 + 1; k < rank; k++)
                {
                    list3[k] = -1;
                }
            }
            else if (base.IsCurrText('}'))
            {
                IntegerList list11=null;
                int num19;
                this.Match('}');
                if (list2[num6] != list3[num6])
                {
                    base.RaiseError(true, "CS0178. Incorrectly structured array initializer.");
                }
                list4[num6] = 1;
                list11 = list3;
                num19 = num6;
                //(list11 = list3)[num19 = num6] = list11[num19] - 1;
                list11[num19] = list11[num19] - 1;
                num6--;
                if (num6 == -1)
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
                this.Match('{');
            }
            goto Label_011E;
        }

        private string Parse_ArrayNameModifier(IntegerList bounds)
        {
            string str = "";
            this.Match('(');
            str = str + "[";
            if (!base.IsCurrText(')'))
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
            this.Match(')');
            return (str + "]");
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
            else if ((this.curr_prop_id > 0) && (base.GetName(id) == base.GetName(this.curr_prop_id)))
            {
                base.DiscardInstruction(base.code.OP_EVAL, -1, -1, id);
                id = base.CurrResultId;
            }
            if (base.IsCurrText('='))
            {
                this.Call_SCANNER();
                this.Gen(base.code.OP_ASSIGN, id, this.Parse_Expression(), id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText('^'))
            {
                this.Call_SCANNER();
                this.Match('=');
                int res = base.NewVar();
                this.Gen(base.code.OP_EXPONENT, id, this.Parse_Expression(), res);
                this.Gen(base.code.OP_ASSIGN, id, res, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText('*'))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num3 = base.NewVar();
                this.Gen(base.code.OP_MULT, id, this.Parse_Expression(), num3);
                this.Gen(base.code.OP_ASSIGN, id, num3, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText('/'))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num4 = base.NewVar();
                this.Gen(base.code.OP_DIV, id, this.Parse_Expression(), num4);
                this.Gen(base.code.OP_ASSIGN, id, num4, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText('\\'))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num5 = base.NewVar();
                this.Gen(base.code.OP_DIV, id, this.Parse_Expression(), num5);
                this.Gen(base.code.OP_ASSIGN, id, num5, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText('+'))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num6 = base.NewVar();
                this.Gen(base.code.OP_PLUS, id, this.Parse_Expression(), num6);
                this.Gen(base.code.OP_ASSIGN, id, num6, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText('-'))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num7 = base.NewVar();
                this.Gen(base.code.OP_MINUS, id, this.Parse_Expression(), num7);
                this.Gen(base.code.OP_ASSIGN, id, num7, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText('&'))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num8 = base.NewVar();
                this.Gen(base.code.OP_PLUS, id, this.Parse_Expression(), num8);
                this.Gen(base.code.OP_ASSIGN, id, num8, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText("<<"))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num9 = base.NewVar();
                this.Gen(base.code.OP_LEFT_SHIFT, id, this.Parse_Expression(), num9);
                this.Gen(base.code.OP_ASSIGN, id, num9, id);
                this.MatchStatementTerminator();
            }
            else if (base.IsCurrText(">>"))
            {
                this.Call_SCANNER();
                this.Match('=');
                int num10 = base.NewVar();
                this.Gen(base.code.OP_RIGHT_SHIFT, id, this.Parse_Expression(), num10);
                this.Gen(base.code.OP_ASSIGN, id, num10, id);
                this.MatchStatementTerminator();
            }
            else if (this.IsLineTerminator())
            {
                this.MatchLineTerminator();
            }
            else
            {
                this.Match('=');
            }
        }

        private void Parse_Attributes()
        {
        }

        private void Parse_Block()
        {
            base.BeginBlock();
            base.DECLARE_SWITCH = false;
            this.Parse_Statements();
            base.EndBlock();
        }

        private int Parse_BooleanExpression()
        {
            int num = base.NewVar(true);
            this.Gen(base.code.OP_ASSIGN, num, this.Parse_Expression(), num);
            return num;
        }

        private void Parse_ClassBase(int class_id)
        {
            this.Match("Inherits");
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
            this.MatchLineTerminator();
        }

        private void Parse_ClassBody(int class_id, ModifierList owner_modifiers, bool IsModule)
        {
            this.variable_initializers.Clear();
            this.static_variable_initializers.Clear();
            while (!base.IsCurrText("End"))
            {
                if (base.IsEOF())
                {
                    this.Match("End");
                }
                this.Parse_ClassMemberDeclaration(class_id, owner_modifiers, IsModule, ClassKind.Class);
            }
        }

        private void Parse_ClassDeclaration(ModifierList ml)
        {
            base.DECLARE_SWITCH = true;
            this.has_constructor = false;
            base.CheckModifiers(ml, this.class_modifiers);
            this.Match("Class");
            int num = this.Parse_Ident();
            this.MatchLineTerminator();
            if (ml.HasModifier(Modifier.Abstract) && ml.HasModifier(Modifier.Sealed))
            {
                base.RaiseError(false, "CS0502. The class '{0}' is abstract and sealed.");
            }
            base.BeginClass(num, ml);
            if (base.IsCurrText("Inherits"))
            {
                this.Parse_ClassBase(num);
            }
            else
            {
                this.Gen(base.code.OP_ADD_ANCESTOR, num, base.ObjectClassId, 0);
            }
            if (base.IsCurrText("Implements"))
            {
                this.Parse_TypeImplementsClause(num);
            }
            if (!base.IsCurrText("End"))
            {
                this.Parse_ClassBody(num, ml, false);
            }
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(num);
            }
            if (!this.has_constructor)
            {
                this.CreateDefaultConstructor(num, false);
            }
            base.EndClass(num);
            this.Match("End");
            this.Match("Class");
            this.MatchLineTerminator();
        }

        private void Parse_ClassMemberDeclaration(int class_id, ModifierList owner_modifiers, bool IsModule, ClassKind ck)
        {
            this.Parse_Attributes();
            ModifierList ml = this.Parse_Modifiers();
            if (owner_modifiers.HasModifier(Modifier.Public) && !ml.HasModifier(Modifier.Private))
            {
                ml.Add(Modifier.Public);
            }
            if (base.IsCurrText("Enum"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Structure"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Interface"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Class"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Delegate"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Event"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_EventMemberDeclaration(class_id, ml, owner_modifiers);
            }
            else if (base.IsCurrText("Dim"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_VariableMemberDeclaration(class_id, ml, owner_modifiers);
            }
            else if (base.IsCurrText("Const"))
            {
                ml.Add(Modifier.Static);
                this.Parse_ConstantMemberDeclaration(class_id, ml, owner_modifiers);
            }
            else if (base.IsCurrText("Property"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_PropertyMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else if (base.IsCurrText("Declare"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_MethodMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else if (base.IsCurrText("Sub"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_MethodMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else if (base.IsCurrText("Function"))
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_MethodMemberDeclaration(class_id, ml, owner_modifiers, ck);
            }
            else
            {
                if (IsModule)
                {
                    ml.Add(Modifier.Static);
                }
                this.Parse_VariableMemberDeclaration(class_id, ml, owner_modifiers);
            }
        }

        private int Parse_ConcatenationExpression()
        {
            int num = this.Parse_AdditiveExpression();
            while (base.IsCurrText("&"))
            {
                this.Call_SCANNER();
                num = base.BinOp(base.code.OP_PLUS, num, this.Parse_AdditiveExpression());
            }
            return num;
        }

        private void Parse_ConstantDeclarator(ModifierList ml)
        {
            int num = this.Parse_Ident();
            int objectClassId = base.ObjectClassId;
            if (base.IsCurrText("As"))
            {
                this.Match("As");
                objectClassId = this.Parse_Type();
            }
            else if (this.OPTION_STRICT)
            {
                base.RaiseError(false, "VB00006. Option Strict On requires all variable declarations to have an 'As' clause.");
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

        private void Parse_ConstantMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_modifiers)
        {
            base.DECLARE_SWITCH = true;
            this.Match("Const");
            do
            {
                this.Parse_ConstantDeclarator(ml);
            }
            while (base.CondMatch(','));
            this.MatchLineTerminator();
        }

        private void Parse_DelegateDeclaration(ModifierList ml)
        {
            this.Match("Delegate");
            base.CheckModifiers(ml, this.delegate_modifiers);
            bool flag = false;
            if (base.IsCurrText("Sub"))
            {
                this.Match("Sub");
            }
            else if (base.IsCurrText("Function"))
            {
                this.Match("Function");
                flag = true;
            }
            else
            {
                this.Match("Sub");
            }
            int num = 1;
            int num2 = this.Parse_Ident();
            base.BeginDelegate(num2, ml);
            int num3 = base.NewVar();
            this.BeginMethod(num3, MemberKind.Method, ml, num);
            this.Gen(base.code.OP_ADD_PATTERN, num2, num3, 0);
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    this.Parse_ParameterList(num3, false);
                }
                this.Match(')');
            }
            if (base.IsCurrText("As") && flag)
            {
                this.Match("As");
                this.Parse_Attributes();
                num = this.Parse_Type();
                base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, num3, -1, -1);
                base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, base.CurrResultId, -1, -1);
                this.Gen(base.code.OP_ASSIGN_TYPE, num3, num, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, base.CurrResultId, num, 0);
            }
            this.InitMethod(num3);
            int num4 = base.NewVar();
            int res = base.NewVar();
            int resultId = base.GetResultId(num3);
            int thisId = base.GetThisId(num3);
            int num8 = base.NewLabel();
            int l = base.NewLabel();
            this.Gen(base.code.OP_FIND_FIRST_DELEGATE, thisId, num4, res);
            this.Gen(base.code.OP_GO_NULL, num8, num4, 0);
            for (int i = 0; i < this.param_ids.Count; i++)
            {
                this.Gen(base.code.OP_PUSH, this.param_ids[i], this.param_mods[i], num4);
            }
            this.Gen(base.code.OP_PUSH, res, 0, 0);
            this.Gen(base.code.OP_CALL_SIMPLE, num4, this.param_ids.Count, resultId);
            base.SetLabelHere(l);
            this.Gen(base.code.OP_FIND_NEXT_DELEGATE, thisId, num4, res);
            this.Gen(base.code.OP_GO_NULL, num8, num4, 0);
            for (int j = 0; j < this.param_ids.Count; j++)
            {
                this.Gen(base.code.OP_PUSH, this.param_ids[j], this.param_mods[j], num4);
            }
            this.Gen(base.code.OP_PUSH, res, 0, 0);
            this.Gen(base.code.OP_CALL_SIMPLE, num4, this.param_ids.Count, resultId);
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num8);
            this.EndMethod(num3);
            base.EndDelegate(num2);
            base.DECLARE_SWITCH = true;
            this.MatchLineTerminator();
        }

        public void Parse_DoLoopStatement()
        {
            this.Match("Do");
            this.PushExitKind(ExitKind.Do);
            int l = base.NewLabel();
            int num2 = base.NewLabel();
            base.SetLabelHere(l);
            if (base.IsCurrText("While"))
            {
                this.Match("While");
                this.Gen(base.code.OP_GO_FALSE, num2, this.Parse_Expression(), 0);
            }
            else if (base.IsCurrText("Until"))
            {
                this.Match("Until");
                this.Gen(base.code.OP_GO_TRUE, num2, this.Parse_Expression(), 0);
            }
            this.MatchStatementTerminator();
            base.BreakStack.Push(num2, ExitKind.Do);
            base.ContinueStack.Push(l);
            this.Parse_Block();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Match("Loop");
            if (base.IsCurrText("While"))
            {
                this.Match("While");
                this.Gen(base.code.OP_GO_TRUE, l, this.Parse_Expression(), 0);
            }
            else if (base.IsCurrText("Until"))
            {
                this.Match("Until");
                this.Gen(base.code.OP_GO_FALSE, l, this.Parse_Expression(), 0);
            }
            else
            {
                this.Gen(base.code.OP_GO, l, 0, 0);
            }
            base.SetLabelHere(num2);
            this.MatchStatementTerminator();
            this.PopExitKind();
        }

        private void Parse_EndStatement()
        {
            this.Match("End");
            this.Gen(base.code.OP_HALT, 0, 0, 0);
            this.MatchStatementTerminator();
        }

        private void Parse_EnumDeclaration(ModifierList ml)
        {
            base.CheckModifiers(ml, this.enum_modifiers);
            ml.Add(Modifier.Static);
            base.DECLARE_SWITCH = true;
            this.Match("Enum");
            int num = this.Parse_Ident();
            int num2 = 8;
            if (base.IsCurrText("As"))
            {
                this.Match("As");
                num2 = this.Parse_IntegralType();
                if (num2 == 4)
                {
                    base.RaiseError(true, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
                }
            }
            base.BeginEnum(num, ml, num2);
            this.Gen(base.code.OP_ADD_UNDERLYING_TYPE, num, num2, 0);
            this.MatchLineTerminator();
            int v = -1;
            this.static_variable_initializers.Clear();
            while (true)
            {
                int num6;
                if (base.IsCurrText("End"))
                {
                    break;
                }
                if (base.IsEOF())
                {
                    this.Match("End");
                }
                if (base.IsCurrText('['))
                {
                    this.Parse_Attributes();
                }
                int num4 = this.Parse_Ident();
                base.BeginField(num4, ml, num);
                int num5 = base.NewVar();
                this.BeginMethod(num5, MemberKind.Method, ml, 1);
                this.InitMethod(num5);
                this.static_variable_initializers.Add(num5);
                if (base.IsCurrText('='))
                {
                    base.DECLARE_SWITCH = false;
                    this.Match('=');
                    num6 = this.Parse_ConstantExpression();
                    base.DECLARE_SWITCH = true;
                    object val = base.GetVal(num6);
                    if (val != null)
                    {
                        v = (int) val;
                    }
                    this.Gen(base.code.OP_ASSIGN, num4, num6, num4);
                    base.SetTypeId(num6, num2);
                }
                else
                {
                    v++;
                    num6 = base.NewConst(v);
                    this.Gen(base.code.OP_ASSIGN, num4, num6, num4);
                    base.SetTypeId(num6, num2);
                }
                this.EndMethod(num5);
                base.EndField(num4);
                base.DECLARE_SWITCH = true;
                this.MatchLineTerminator();
            }
            this.CreateDefaultStaticConstructor(num);
            base.DECLARE_SWITCH = true;
            base.EndEnum(num);
            this.Match("End");
            this.Match("Enum");
            this.MatchLineTerminator();
        }

        private void Parse_EraseStatement()
        {
            this.Match("Erase");
            do
            {
                int num = this.Parse_Expression();
            }
            while (base.CondMatch(','));
            this.MatchStatementTerminator();
        }

        private void Parse_ErrorStatement()
        {
            this.Match("Error");
            this.Gen(base.code.OP_THROW, this.Parse_Expression(), 0, 0);
            this.MatchStatementTerminator();
        }

        private void Parse_EventHandlerList()
        {
            do
            {
                this.Parse_EventMemberSpecifier();
            }
            while (base.CondMatch(','));
        }

        private void Parse_EventMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_ml)
        {
            int num2;
            int num3;
            this.Match("Event");
            base.CheckModifiers(ml, this.event_modifiers);
            int id = this.Parse_Ident();
            if (ml.HasModifier(Modifier.Abstract) && !owner_ml.HasModifier(Modifier.Abstract))
            {
                string str = base.GetName(id);
                string str2 = base.GetName(class_id);
                base.RaiseErrorEx(false, "CS0513. '{0}' is abstract but it is contained in nonabstract class '{1}'", new object[] { str, str2 });
            }
            if (base.IsCurrText('('))
            {
                int num4 = 1;
                int num5 = base.NewVar();
                base.SetName(num5, base.GetName(id) + "EventHandler");
                base.BeginDelegate(num5, ml);
                num2 = base.NewVar();
                this.BeginMethod(num2, MemberKind.Method, ml, num4);
                this.Gen(base.code.OP_ADD_PATTERN, num5, num2, 0);
                if (base.IsCurrText('('))
                {
                    this.Match('(');
                    if (!base.IsCurrText(')'))
                    {
                        this.Parse_ParameterList(num2, false);
                    }
                    this.Match(')');
                }
                if (base.IsCurrText("As"))
                {
                    this.Match("As");
                    this.Parse_Attributes();
                    num4 = this.Parse_Type();
                    this.Gen(base.code.OP_ASSIGN_TYPE, num2, num4, 0);
                    this.Gen(base.code.OP_ASSIGN_TYPE, base.CurrResultId, num4, 0);
                }
                this.InitMethod(num2);
                int num6 = base.NewVar();
                int num7 = base.NewVar();
                int resultId = base.GetResultId(num2);
                int thisId = base.GetThisId(num2);
                int num10 = base.NewLabel();
                int l = base.NewLabel();
                this.Gen(base.code.OP_FIND_FIRST_DELEGATE, thisId, num6, num7);
                this.Gen(base.code.OP_GO_NULL, num10, num6, 0);
                for (int i = 0; i < this.param_ids.Count; i++)
                {
                    this.Gen(base.code.OP_PUSH, this.param_ids[i], this.param_mods[i], num6);
                }
                this.Gen(base.code.OP_PUSH, num7, 0, 0);
                this.Gen(base.code.OP_CALL_SIMPLE, num6, this.param_ids.Count, resultId);
                base.SetLabelHere(l);
                this.Gen(base.code.OP_FIND_NEXT_DELEGATE, thisId, num6, num7);
                this.Gen(base.code.OP_GO_NULL, num10, num6, 0);
                for (int j = 0; j < this.param_ids.Count; j++)
                {
                    this.Gen(base.code.OP_PUSH, this.param_ids[j], this.param_mods[j], num6);
                }
                this.Gen(base.code.OP_PUSH, num7, 0, 0);
                this.Gen(base.code.OP_CALL_SIMPLE, num6, this.param_ids.Count, resultId);
                this.Gen(base.code.OP_GO, l, 0, 0);
                base.SetLabelHere(num10);
                this.EndMethod(num2);
                base.EndDelegate(num5);
                num3 = num5;
            }
            else
            {
                this.Match("As");
                num3 = this.Parse_Type();
            }
            ModifierList list = ml.Clone();
            list.Delete(Modifier.Public);
            base.BeginField(id, list, num3);
            string name = base.GetName(id);
            base.SetName(id, "__" + name);
            base.EndField(id);
            int num17 = base.NewVar();
            base.SetName(num17, name);
            base.BeginEvent(num17, ml, num3, 0);
            this.Gen(base.code.OP_ADD_EVENT_FIELD, num17, id, 0);
            num2 = base.NewVar();
            base.SetName(num2, "add_" + name);
            this.BeginMethod(num2, MemberKind.Method, list, 1);
            int num14 = base.NewVar();
            base.SetName(num14, "value");
            this.Gen(base.code.OP_ADD_PARAM, num2, num14, 0);
            this.Gen(base.code.OP_ASSIGN_TYPE, num14, num3, 0);
            this.InitMethod(num2);
            int num15 = base.NewVar();
            base.SetName(num15, base.GetName(id));
            int res = base.NewVar();
            this.Gen(base.code.OP_EVAL, 0, 0, num15);
            this.Gen(base.code.OP_PLUS, num15, num14, res);
            this.Gen(base.code.OP_ASSIGN, num15, res, num15);
            this.EndMethod(num2);
            this.Gen(base.code.OP_ADD_ADD_ACCESSOR, num17, num2, 0);
            num2 = base.NewVar();
            base.SetName(num2, "remove_" + name);
            this.BeginMethod(num2, MemberKind.Method, list, 1);
            num14 = base.NewVar();
            base.SetName(num14, "value");
            this.Gen(base.code.OP_ADD_PARAM, num2, num14, 0);
            this.Gen(base.code.OP_ASSIGN_TYPE, num14, num3, 0);
            this.InitMethod(num2);
            num15 = base.NewVar();
            base.SetName(num15, base.GetName(id));
            res = base.NewVar();
            this.Gen(base.code.OP_EVAL, 0, 0, num15);
            this.Gen(base.code.OP_MINUS, num15, num14, res);
            this.Gen(base.code.OP_ASSIGN, num15, res, num15);
            this.EndMethod(num2);
            this.Gen(base.code.OP_ADD_REMOVE_ACCESSOR, num17, num2, 0);
            base.EndEvent(num17);
            this.MatchLineTerminator();
        }

        private void Parse_EventMemberSpecifier()
        {
            int currThisID;
            if (base.IsCurrText("MyBase"))
            {
                this.Match("MyBase");
                currThisID = base.CurrThisID;
            }
            else
            {
                currThisID = this.Parse_Ident();
            }
            base.DECLARE_SWITCH = true;
            this.Match('.');
            int res = this.Parse_Ident();
            this.Gen(base.code.OP_ADD_HANDLES, base.CurrSubId, currThisID, res);
        }

        private void Parse_ExitStatement()
        {
            this.Match("Exit");
            ExitKind currExitKind = this.CurrExitKind;
            if (base.IsCurrText("Do"))
            {
                this.Match("Do");
                this.Gen(base.code.OP_GOTO_START, base.BreakStack.TopLabel(ExitKind.Do), 0, 0);
            }
            else if (base.IsCurrText("For"))
            {
                this.Match("For");
                this.Gen(base.code.OP_GOTO_START, base.BreakStack.TopLabel(ExitKind.For), 0, 0);
            }
            else if (base.IsCurrText("While"))
            {
                this.Match("While");
                this.Gen(base.code.OP_GOTO_START, base.BreakStack.TopLabel(ExitKind.While), 0, 0);
            }
            else if (base.IsCurrText("Select"))
            {
                this.Match("Select");
                this.Gen(base.code.OP_GOTO_START, base.BreakStack.TopLabel(ExitKind.Select), 0, 0);
            }
            else if (base.IsCurrText("Sub"))
            {
                this.Match("Sub");
                this.Gen(base.code.OP_EXIT_SUB, 0, 0, 0);
            }
            else if (base.IsCurrText("Function"))
            {
                this.Match("Function");
                this.Gen(base.code.OP_EXIT_SUB, 0, 0, 0);
            }
            else if (base.IsCurrText("Property"))
            {
                this.Match("Property");
                this.Gen(base.code.OP_EXIT_SUB, 0, 0, 0);
            }
            else if (base.BreakStack.Count == 0)
            {
                base.RaiseError(false, "CS0139. No enclosing loop out of which to break or continue.");
            }
            this.MatchStatementTerminator();
        }

        private int Parse_ExponentiationExpression()
        {
            int num = this.Parse_SimpleExpression();
            while (base.IsCurrText('^'))
            {
                this.Call_SCANNER();
                num = base.BinOp(base.code.OP_EXPONENT, num, this.Parse_SimpleExpression());
            }
            return num;
        }

        public override int Parse_Expression()
        {
            return this.Parse_LogicalXORExpression();
        }

        private void Parse_ExternalMethodDeclaration(int class_id, ModifierList ml, ModifierList owner_ml)
        {
            this.Match("Declare");
        }

        public void Parse_ForEachStatement()
        {
            this.Match("Each");
            base.BeginBlock();
            this.PushExitKind(ExitKind.For);
            int res = this.Parse_Ident();
            if (base.IsCurrText("As"))
            {
                this.Match("As");
                base.DiscardInstruction(base.code.OP_EVAL, -1, -1, res);
                this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, res, base.CurrSubId, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, res, this.Parse_Type(), 0);
            }
            this.Match("In");
            int num2 = this.Parse_Expression();
            this.MatchStatementTerminator();
            int num3 = base.NewVar();
            int num4 = base.NewRef("GetEnumerator");
            this.Gen(base.code.OP_CREATE_REFERENCE, num2, 0, num4);
            this.Gen(base.code.OP_BEGIN_CALL, num4, 0, 0);
            this.Gen(base.code.OP_PUSH, num2, 0, 0);
            this.Gen(base.code.OP_CALL, num4, 0, num3);
            int num5 = base.NewLabel();
            int l = base.NewLabel();
            base.SetLabelHere(l);
            int num7 = base.NewVar();
            int num8 = base.NewRef("MoveNext");
            this.Gen(base.code.OP_CREATE_REFERENCE, num3, 0, num8);
            this.Gen(base.code.OP_BEGIN_CALL, num8, 0, 0);
            this.Gen(base.code.OP_PUSH, num3, 0, 0);
            this.Gen(base.code.OP_CALL, num8, 0, num7);
            this.Gen(base.code.OP_GO_FALSE, num5, num7, 0);
            base.BreakStack.Push(num5, ExitKind.For);
            base.ContinueStack.Push(l);
            int num9 = base.NewRef("get_Current");
            this.Gen(base.code.OP_CREATE_REFERENCE, num3, 0, num9);
            this.Gen(base.code.OP_BEGIN_CALL, num9, 0, 0);
            this.Gen(base.code.OP_PUSH, num3, 0, 0);
            this.Gen(base.code.OP_CALL, num9, 0, res);
            this.Parse_Block();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num5);
            this.Match("Next");
            if (!this.IsStatementTerminator())
            {
                int id = this.Parse_Expression();
                if (id != res)
                {
                    base.RaiseErrorEx(true, "VB00001. Next control variable does not match For loop control variable '{0}'", new object[] { base.GetName(id) });
                }
            }
            this.PopExitKind();
            base.BeginBlock();
            this.MatchStatementTerminator();
        }

        public void Parse_ForNextStatement()
        {
            int num7;
            this.Match("For");
            if (base.IsCurrText("Each"))
            {
                this.Parse_ForEachStatement();
                return;
            }
            this.PushExitKind(ExitKind.For);
            base.BeginBlock();
            int l = base.NewLabel();
            int num2 = base.NewLabel();
            int res = this.Parse_Ident();
            if (base.IsCurrText("As"))
            {
                this.Match("As");
                base.DiscardInstruction(base.code.OP_EVAL, -1, -1, res);
                this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, res, base.CurrSubId, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, res, this.Parse_Type(), 0);
            }
            else
            {
                this.Gen(base.code.OP_ASSIGN_TYPE, res, 8, 0);
            }
            this.Match("=");
            int num4 = this.Parse_Expression();
            this.Gen(base.code.OP_ASSIGN, res, num4, res);
            base.SetLabelHere(l);
            this.Match("To");
            int num5 = this.Parse_Expression();
            int num6 = base.NewVar(true);
            this.Gen(base.code.OP_LE, res, num5, num6);
            this.Gen(base.code.OP_GO_FALSE, num2, num6, 0);
            if (base.IsCurrText("Step"))
            {
                this.Match("Step");
                num7 = this.Parse_Expression();
            }
            else
            {
                num7 = base.NewVar(0);
                this.Gen(base.code.OP_ASSIGN, num7, base.NewVar(1), num7);
            }
            this.MatchStatementTerminator();
            this.for_loop_stack.Push(res, num7, l, num2);
            base.BreakStack.Push(num2, ExitKind.For);
            base.ContinueStack.Push(l);
            base.BeginBlock();
            while (true)
            {
                if ((base.IsCurrText("Next") || base.IsEOF()) || (this.for_loop_stack.Count == 0))
                {
                    break;
                }
                this.Parse_Statement();
            }
            base.EndBlock();
            base.EndBlock();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            if (this.for_loop_stack.Count == 0)
            {
                base.SetLabelHere(num2);
                return;
            }
            this.Match("Next");
            if (!this.IsStatementTerminator())
            {
                while (true)
                {
                    int num8 = this.Parse_Expression();
                    ForLoopRec top = this.for_loop_stack.Top;
                    if (top.id != num8)
                    {
                        base.RaiseErrorEx(true, "VB00001. Next control variable does not match For loop control variable '{0}'", new object[] { base.GetName(top.id) });
                    }
                    this.Gen(base.code.OP_PLUS, top.id, top.step_id, top.id);
                    this.Gen(base.code.OP_GO, top.lg, 0, 0);
                    base.SetLabelHere(top.lf);
                    this.for_loop_stack.Pop();
                    if (!base.CondMatch(','))
                    {
                        goto Label_032D;
                    }
                }
            }
            this.Gen(base.code.OP_PLUS, res, num7, res);
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num2);
            this.for_loop_stack.Pop();
        Label_032D:
            this.PopExitKind();
            this.MatchStatementTerminator();
        }

        private void Parse_FunctionDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            this.Match("Function");
            this.PushExitKind(ExitKind.Function);
            int id = this.Parse_Ident();
            this.CheckMethodModifiers(id, class_id, ml, owner_ml);
            int num2 = 0x10;
            this.valid_this_context = true;
            this.BeginMethod(id, MemberKind.Method, ml, num2);
            if (this.explicit_intf_id > 0)
            {
                this.Gen(base.code.OP_ADD_EXPLICIT_INTERFACE, id, this.explicit_intf_id, 0);
            }
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    this.Parse_ParameterList(id, false);
                }
                this.Match(')');
            }
            if (base.IsCurrText("As"))
            {
                this.Match("As");
                this.Parse_Attributes();
                num2 = this.Parse_Type();
                if (base.IsCurrText('('))
                {
                    string str = this.Parse_TypeNameModifier();
                    string str2 = base.GetName(num2) + str;
                    num2 = base.NewVar();
                    base.SetName(num2, str2);
                    this.Gen(base.code.OP_EVAL_TYPE, 0, 0, num2);
                }
                base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, id, -1, -1);
                base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, base.CurrResultId, -1, -1);
                this.Gen(base.code.OP_ASSIGN_TYPE, id, num2, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, base.CurrResultId, num2, 0);
            }
            if (ml.HasModifier(Modifier.Extern))
            {
                base.RaiseErrorEx(false, "CS0179. '{0}' cannot be extern and declare a body.", new object[] { base.GetName(id) });
            }
            if (ck != ClassKind.Interface)
            {
                this.InitMethod(id);
                if (ml.HasModifier(Modifier.Abstract))
                {
                    string name = base.GetName(id);
                    base.RaiseErrorEx(false, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { name });
                }
                if (base.GetName(id) == "Main")
                {
                    this.Gen(base.code.OP_CHECKED, base.TRUE_id, 0, 0);
                }
                if (base.IsCurrText("Handles"))
                {
                    this.Parse_HandlesClause();
                }
                else if (base.IsCurrText("Implements"))
                {
                    this.Parse_ImplementsClause(id);
                }
                base.DECLARE_SWITCH = false;
                this.MatchLineTerminator();
                this.Parse_Block();
                if (base.GetName(id) == "Main")
                {
                    this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                }
                this.Match("End");
                this.Match("Function");
            }
            this.EndMethod(id);
            this.valid_this_context = false;
            base.DECLARE_SWITCH = true;
            this.PopExitKind();
            this.MatchLineTerminator();
        }

        private void Parse_GoToStatement()
        {
            this.Match("GoTo");
            int id = this.Parse_Ident();
            base.PutKind(id, MemberKind.Label);
            this.Gen(base.code.OP_GOTO_START, id, 0, 0);
            this.MatchStatementTerminator();
        }

        private void Parse_HandlesClause()
        {
            if (base.IsCurrText("Handles"))
            {
                base.DECLARE_SWITCH = false;
                this.Match("Handles");
                this.Parse_EventHandlerList();
                base.DECLARE_SWITCH = true;
            }
        }

        public override int Parse_Ident()
        {
            if (base.IsCurrText('.'))
            {
                if (this.with_stack.Count == 0)
                {
                    base.RaiseError(true, "CS1001. Identifier expected.");
                    return 0;
                }
                int num = this.with_stack.Peek();
                base.REF_SWITCH = true;
                this.Match('.');
                int res = this.Parse_Ident();
                this.Gen(base.code.OP_CREATE_REFERENCE, num, 0, res);
                base.REF_SWITCH = false;
                return res;
            }
            return base.Parse_Ident();
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
                this.Call_SCANNER();
                return base.DATETIME_CLASS_id;
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
            if (base.IsCurrText("Nothing"))
            {
                this.Call_SCANNER();
                return base.NULL_id;
            }
            return this.Parse_Ident();
        }

        private void Parse_IfStatement()
        {
            this.Match("If");
            int num = base.NewLabel();
            int num2 = base.NewLabel();
            int num3 = this.Parse_Expression();
            this.Gen(base.code.OP_GO_FALSE, num2, num3, 0);
            if (base.IsCurrText("Then"))
            {
                this.Match("Then");
            }
            if (this.IsStatementTerminator())
            {
                this.MatchStatementTerminator();
                this.Parse_Block();
                this.Gen(base.code.OP_GO, num, 0, 0);
                base.SetLabelHere(num2);
                while (base.IsCurrText("ElseIf"))
                {
                    int num4 = base.NewLabel();
                    this.Match("ElseIf");
                    int num5 = this.Parse_Expression();
                    this.Gen(base.code.OP_GO_FALSE, num4, num5, 0);
                    if (base.IsCurrText("Then"))
                    {
                        this.Match("Then");
                    }
                    this.MatchStatementTerminator();
                    this.Parse_Block();
                    this.Gen(base.code.OP_GO, num, 0, 0);
                    base.SetLabelHere(num4);
                }
                if (base.IsCurrText("Else"))
                {
                    this.Match("Else");
                    this.MatchStatementTerminator();
                    this.Parse_Block();
                }
                base.SetLabelHere(num);
                this.Match("End");
                this.Match("If");
                this.MatchStatementTerminator();
            }
            else
            {
                this.SKIP_STATEMENT_TERMINATOR = true;
                this.Parse_Statement();
                this.Gen(base.code.OP_GO, num, 0, 0);
                base.SetLabelHere(num2);
                if (base.IsCurrText("Else"))
                {
                    this.Match("Else");
                    this.Parse_Statements();
                }
                base.SetLabelHere(num);
                this.MatchStatementTerminator();
                this.SKIP_STATEMENT_TERMINATOR = false;
            }
        }

        private void Parse_ImplementsClause(int member_id)
        {
            this.Match("Implements");
            do
            {
                int res = this.Parse_Type();
                this.Gen(base.code.OP_ADD_IMPLEMENTS, member_id, base.CurrClassID, res);
            }
            while (base.CondMatch(','));
        }

        private void Parse_ImportsClause()
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
            }
        }

        private void Parse_ImportsStatement()
        {
            this.Match("Imports");
            do
            {
                this.Parse_ImportsClause();
            }
            while (base.CondMatch(','));
            base.DECLARE_SWITCH = false;
            this.MatchLineTerminator();
        }

        private int Parse_IntegerDivisionExpression()
        {
            int num = this.Parse_MultiplicativeExpression();
            while (base.IsCurrText('\\'))
            {
                this.Call_SCANNER();
                num = base.BinOp(base.code.OP_DIV, num, this.Parse_MultiplicativeExpression());
            }
            return num;
        }

        public override int Parse_IntegerLiteral()
        {
            int id = base.Parse_IntegerLiteral();
            if (base.IsCurrText('%'))
            {
                base.SetTypeId(id, 8);
                this.Call_SCANNER();
                return id;
            }
            if (base.IsCurrText('&'))
            {
                base.SetTypeId(id, 9);
                this.Call_SCANNER();
                return id;
            }
            if (base.IsCurrText('@'))
            {
                base.SetTypeId(id, 5);
                this.Call_SCANNER();
                return id;
            }
            if (base.IsCurrText('!'))
            {
                base.SetTypeId(id, 7);
                this.Call_SCANNER();
                return id;
            }
            if (base.IsCurrText('#'))
            {
                base.SetTypeId(id, 6);
                this.Call_SCANNER();
                return id;
            }
            if (base.IsCurrText('$'))
            {
                base.SetTypeId(id, 12);
                this.Call_SCANNER();
            }
            return id;
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

        private void Parse_InterfaceDeclaration(ModifierList ml)
        {
            base.CheckModifiers(ml, this.interface_modifiers);
            ml.Add(Modifier.Abstract);
            base.DECLARE_SWITCH = true;
            this.Match("Interface");
            int num = this.Parse_Ident();
            this.MatchLineTerminator();
            base.BeginInterface(num, ml);
            if (base.IsCurrText("Inherits"))
            {
                this.Parse_ClassBase(num);
            }
            while (!base.IsCurrText("End"))
            {
                if (base.IsEOF())
                {
                    this.Match("End");
                }
                this.Parse_InterfaceMember(num, ml);
            }
            base.DECLARE_SWITCH = false;
            base.EndInterface(num);
            this.Match("End");
            this.Match("Interface");
            this.MatchLineTerminator();
        }

        private void Parse_InterfaceMember(int interface_id, ModifierList owner_modifiers)
        {
            this.Parse_Attributes();
            ModifierList ml = this.Parse_Modifiers();
            ml.Add(Modifier.Abstract);
            if (owner_modifiers.HasModifier(Modifier.Public) && !ml.HasModifier(Modifier.Private))
            {
                ml.Add(Modifier.Public);
            }
            if (base.IsCurrText("Enum"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Structure"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Interface"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Class"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Delegate"))
            {
                this.Parse_NonModuleDeclaration(ml);
            }
            else if (base.IsCurrText("Event"))
            {
                this.Parse_EventMemberDeclaration(interface_id, ml, owner_modifiers);
            }
            else if (base.IsCurrText("Property"))
            {
                this.Parse_PropertyMemberDeclaration(interface_id, ml, owner_modifiers, ClassKind.Interface);
            }
            else if (base.IsCurrText("Sub"))
            {
                this.Parse_MethodMemberDeclaration(interface_id, ml, owner_modifiers, ClassKind.Interface);
            }
            else if (base.IsCurrText("Function"))
            {
                this.Parse_MethodMemberDeclaration(interface_id, ml, owner_modifiers, ClassKind.Interface);
            }
        }

        private void Parse_InvocationStatement()
        {
            if (base.IsCurrText("Call"))
            {
                this.Match("Call");
            }
            int num = this.Parse_SimpleExpression();
            int res = base.NewVar();
            if (base.IsCurrText('('))
            {
                this.Match('(');
                this.Gen(base.code.OP_CALL, num, this.Parse_ArgumentList(')', num, base.CurrThisID), res);
                this.Match(')');
            }
            else
            {
                this.Gen(base.code.OP_BEGIN_CALL, num, 0, 0);
                this.Gen(base.code.OP_PUSH, base.CurrThisID, 0, 0);
                this.Gen(base.code.OP_CALL, num, 0, res);
            }
        }

        public void Parse_LocalDeclarationStatement(LocalModifier m)
        {
            this.local_variables.Clear();
            base.DECLARE_SWITCH = true;
            base.DECLARATION_CHECK_SWITCH = true;
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            this.Call_SCANNER();
            do
            {
                int num = 0x10;
                do
                {
                    int num2 = this.Parse_Ident();
                    this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, num2, base.CurrSubId, 0);
                    this.local_variables.Add(num2);
                    if (m == LocalModifier.Static)
                    {
                        base.SetStaticLocalVar(num2);
                    }
                    if (base.IsCurrText('('))
                    {
                        IntegerList bounds = new IntegerList(true);
                        string str = this.Parse_ArrayNameModifier(bounds);
                        list.Add(bounds);
                        list2.Add(str);
                    }
                    else
                    {
                        list.Add(null);
                        list2.Add(null);
                    }
                }
                while (base.CondMatch(','));
                base.DECLARE_SWITCH = false;
                if (base.IsCurrText("As"))
                {
                    this.Match("As");
                    bool flag = base.curr_token.id == base.DATETIME_CLASS_id;
                    if (base.IsCurrText("New") || flag)
                    {
                        if (base.IsCurrText("New"))
                        {
                            this.Match("New");
                            flag = base.curr_token.id == base.DATETIME_CLASS_id;
                        }
                        num = this.Parse_Type();
                        if (this.local_variables.Count == 1)
                        {
                            if (list2[0] != null)
                            {
                                base.RaiseError(true, "VB00003. Arrays cannot be declared with 'New'");
                            }
                            int res = this.local_variables[0];
                            this.Gen(base.code.OP_CREATE_OBJECT, num, 0, res);
                            if (base.IsCurrText('('))
                            {
                                this.Match('(');
                                this.Gen(base.code.OP_CALL, num, this.Parse_ArgumentList(')', num, res), 0);
                                this.Match(')');
                            }
                            else
                            {
                                this.Gen(base.code.OP_BEGIN_CALL, num, 0, 0);
                                if (flag)
                                {
                                    this.Gen(base.code.OP_PUSH, base.NewConst(0), 0, num);
                                }
                                this.Gen(base.code.OP_PUSH, res, 0, 0);
                                this.Gen(base.code.OP_CALL, num, 0, 0);
                            }
                        }
                        else
                        {
                            base.RaiseError(true, "VB00002. Explicit initialization is not permitted with multiple variables declared with a single type specifier.");
                        }
                    }
                    else
                    {
                        num = this.Parse_TypeEx();
                    }
                }
                else if (this.OPTION_STRICT)
                {
                    base.RaiseError(false, "VB00006. Option Strict On requires all variable declarations to have an 'As' clause.");
                }
                for (int i = 0; i < this.local_variables.Count; i++)
                {
                    int num5 = this.local_variables[i];
                    if (list2[i] != null)
                    {
                        string str2 = (string) list2[i];
                        int id = num;
                        int num7 = base.NewVar();
                        base.SetName(num7, base.GetName(id) + str2);
                        this.Gen(base.code.OP_EVAL_TYPE, id, 0, num7);
                        this.Gen(base.code.OP_ASSIGN_TYPE, num5, num7, 0);
                        IntegerList list4 = (IntegerList) list[i];
                        this.Gen(base.code.OP_CREATE_OBJECT, num7, 0, num5);
                        int count = list4.Count;
                        if (count > 0)
                        {
                            this.Gen(base.code.OP_BEGIN_CALL, num7, 0, 0);
                            for (int j = 0; j < count; j++)
                            {
                                this.Gen(base.code.OP_INC, list4[j], 0, list4[j]);
                                this.Gen(base.code.OP_PUSH, list4[j], 0, num7);
                            }
                            this.Gen(base.code.OP_PUSH, num5, 0, 0);
                            this.Gen(base.code.OP_CALL, num7, count, 0);
                        }
                        num = num7;
                    }
                    else
                    {
                        this.Gen(base.code.OP_ASSIGN_TYPE, num5, num, 0);
                    }
                }
                if (base.IsCurrText('='))
                {
                    base.DECLARE_SWITCH = false;
                    this.Match('=');
                    if (this.local_variables.Count == 1)
                    {
                        int num10 = this.local_variables[0];
                        if (base.IsCurrText('{'))
                        {
                            this.Gen(base.code.OP_ASSIGN, num10, this.Parse_ArrayInitializer(num), num10);
                        }
                        else
                        {
                            this.Gen(base.code.OP_ASSIGN, num10, this.Parse_Expression(), num10);
                        }
                        this.Gen(base.code.OP_INIT_STATIC_VAR, num10, 0, 0);
                    }
                    else
                    {
                        base.RaiseError(true, "VB00002. Explicit initialization is not permitted with multiple variables declared with a single type specifier.");
                    }
                    base.DECLARE_SWITCH = true;
                }
                else
                {
                    for (int k = 0; k < this.local_variables.Count; k++)
                    {
                        int num12 = this.local_variables[k];
                        this.Gen(base.code.OP_CHECK_STRUCT_CONSTRUCTOR, num, 0, num12);
                    }
                }
                base.DECLARE_SWITCH = true;
            }
            while (base.CondMatch(','));
            base.DECLARE_SWITCH = false;
            base.DECLARATION_CHECK_SWITCH = false;
            this.MatchStatementTerminator();
        }

        private int Parse_LogicalANDExpression()
        {
            int num = this.Parse_LogicalNOTExpression();
            while (base.IsCurrText("And") || base.IsCurrText("AndAlso"))
            {
                if (base.IsCurrText("And"))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_BITWISE_AND, num, this.Parse_LogicalNOTExpression());
                }
                else
                {
                    int num2 = num;
                    int num3 = base.NewLabel();
                    num = base.NewVar();
                    this.Gen(base.code.OP_ASSIGN, num, num2, num);
                    this.Gen(base.code.OP_GO_FALSE, num3, num, 0);
                    this.Call_SCANNER();
                    this.Gen(base.code.OP_ASSIGN, num, this.Parse_LogicalNOTExpression(), num);
                    base.SetLabelHere(num3);
                }
            }
            return num;
        }

        private int Parse_LogicalNOTExpression()
        {
            if (base.IsCurrText("Not"))
            {
                this.Match("Not");
                return base.UnaryOp(base.code.OP_NOT, this.Parse_Expression());
            }
            return this.Parse_RelationalExpression();
        }

        private int Parse_LogicalORExpression()
        {
            int num = this.Parse_LogicalANDExpression();
            while (base.IsCurrText("Or") || base.IsCurrText("OrElse"))
            {
                if (base.IsCurrText("Or"))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_BITWISE_OR, num, this.Parse_LogicalANDExpression());
                }
                else
                {
                    int num2 = num;
                    int num3 = base.NewLabel();
                    num = base.NewVar();
                    this.Gen(base.code.OP_ASSIGN, num, num2, num);
                    this.Gen(base.code.OP_GO_TRUE, num3, num, 0);
                    this.Call_SCANNER();
                    this.Gen(base.code.OP_ASSIGN, num, this.Parse_LogicalANDExpression(), num);
                    base.SetLabelHere(num3);
                }
            }
            return num;
        }

        private int Parse_LogicalXORExpression()
        {
            int num = this.Parse_LogicalORExpression();
            while (base.IsCurrText("Xor"))
            {
                this.Call_SCANNER();
                num = base.BinOp(base.code.OP_BITWISE_XOR, num, this.Parse_LogicalORExpression());
            }
            return num;
        }

        private void Parse_MethodMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            base.DECLARE_SWITCH = true;
            if (base.IsCurrText("Sub"))
            {
                this.Parse_SubDeclaration(class_id, ml, owner_ml, ck);
            }
            else if (base.IsCurrText("Function"))
            {
                this.Parse_FunctionDeclaration(class_id, ml, owner_ml, ck);
            }
            else if (base.IsCurrText("Declare"))
            {
                this.Parse_ExternalMethodDeclaration(class_id, ml, owner_ml);
            }
            else
            {
                this.Match("Sub");
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
            return list;
        }

        private void Parse_ModuleDeclaration(ModifierList ml)
        {
            this.Match("Module");
            base.DECLARE_SWITCH = true;
            this.has_constructor = false;
            base.CheckModifiers(ml, this.class_modifiers);
            int num = this.Parse_Ident();
            this.MatchLineTerminator();
            base.BeginClass(num, ml);
            this.Gen(base.code.OP_ADD_ANCESTOR, num, base.ObjectClassId, 0);
            this.Parse_ClassBody(num, ml, true);
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(num);
            }
            if (!this.has_constructor)
            {
                this.CreateDefaultConstructor(num, false);
            }
            base.EndClass(num);
            this.Match("End");
            this.Match("Module");
            if (this.IsLineTerminator())
            {
                this.MatchLineTerminator();
            }
        }

        private int Parse_ModulusExpression()
        {
            int num = this.Parse_IntegerDivisionExpression();
            while (base.IsCurrText("Mod"))
            {
                this.Call_SCANNER();
                num = base.BinOp(base.code.OP_MOD, num, this.Parse_IntegerDivisionExpression());
            }
            return num;
        }

        private int Parse_MultiplicativeExpression()
        {
            int num = this.Parse_UnaryNegationExpression();
            while (base.IsCurrText('*') || base.IsCurrText('/'))
            {
                if (base.IsCurrText('*'))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_MULT, num, this.Parse_UnaryNegationExpression());
                }
                else
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_DIV, num, this.Parse_UnaryNegationExpression());
                }
            }
            return num;
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
            this.Match("Namespace");
            this.MatchLineTerminator();
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

        private void Parse_NonModuleDeclaration(ModifierList ml)
        {
            if (base.IsCurrText("Enum"))
            {
                this.Parse_EnumDeclaration(ml);
            }
            else if (base.IsCurrText("Structure"))
            {
                this.Parse_StructureDeclaration(ml);
            }
            else if (base.IsCurrText("Interface"))
            {
                this.Parse_InterfaceDeclaration(ml);
            }
            else if (base.IsCurrText("Class"))
            {
                this.Parse_ClassDeclaration(ml);
            }
            else if (base.IsCurrText("Delegate"))
            {
                this.Parse_DelegateDeclaration(ml);
            }
            else if (base.scripter.SIGN_BRIEF_SYNTAX)
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
            else
            {
                this.Match("Class");
            }
        }

        private void Parse_OnErrorStatement()
        {
            int num = base.NewLabel();
            this.Gen(base.code.OP_GO, num, 0, 0);
            this.Match("On");
            this.Match("Error");
            if (base.IsCurrText("GoTo"))
            {
                this.Match("GoTo");
                if (base.IsCurrText('-'))
                {
                    this.Match('-');
                    this.Match('1');
                }
                else if (base.IsCurrText('0'))
                {
                    this.Match('0');
                }
                else
                {
                    this.Gen(base.code.OP_ONERROR, 0, 0, 0);
                    this.Gen(base.code.OP_DISCARD_ERROR, 0, 0, 0);
                    int id = this.Parse_Ident();
                    base.PutKind(id, MemberKind.Label);
                    this.Gen(base.code.OP_GOTO_START, id, 0, 0);
                }
            }
            else
            {
                this.Gen(base.code.OP_ONERROR, 0, 0, 0);
                this.Gen(base.code.OP_DISCARD_ERROR, 0, 0, 0);
                this.Gen(base.code.OP_RESUME_NEXT, 0, 0, 0);
                this.Match("Resume");
                this.Match("Next");
            }
            base.SetLabelHere(num);
            this.MatchStatementTerminator();
        }

        private void Parse_OptionCompareStatement()
        {
            this.Match("Compare");
            if (base.IsCurrText("Binary"))
            {
                this.Match("Binary");
                this.MatchLineTerminator();
            }
            else if (base.IsCurrText("Text"))
            {
                this.Match("Text");
                this.MatchLineTerminator();
            }
            else
            {
                this.Match("Binary");
            }
        }

        private void Parse_OptionExplicitStatement()
        {
            this.Match("Explicit");
            if (base.IsCurrText("On"))
            {
                this.Match("On");
                this.Gen(base.code.OP_EXPLICIT_ON, 0, 0, 0);
                this.MatchLineTerminator();
            }
            else if (base.IsCurrText("Off"))
            {
                this.Match("Off");
                this.Gen(base.code.OP_EXPLICIT_OFF, 0, 0, 0);
                this.MatchLineTerminator();
            }
            else
            {
                this.Match("On");
            }
        }

        private void Parse_OptionStatement()
        {
            this.Match("Option");
            if (base.IsCurrText("Explicit"))
            {
                this.Parse_OptionExplicitStatement();
            }
            else if (base.IsCurrText("Strict"))
            {
                this.Parse_OptionStrictStatement();
            }
            else if (base.IsCurrText("Compare"))
            {
                this.Parse_OptionCompareStatement();
            }
        }

        private void Parse_OptionStrictStatement()
        {
            this.Match("Strict");
            if (base.IsCurrText("On"))
            {
                this.Match("On");
                this.Gen(base.code.OP_STRICT_ON, 0, 0, 0);
                this.MatchLineTerminator();
            }
            else if (base.IsCurrText("Off"))
            {
                this.Match("Off");
                this.Gen(base.code.OP_STRICT_OFF, 0, 0, 0);
                this.OPTION_STRICT = false;
                this.MatchLineTerminator();
            }
            else
            {
                this.Match("On");
            }
        }

        private int Parse_ParameterList(int sub_id, bool isIndexer)
        {
            this.param_ids.Clear();
            this.param_type_ids.Clear();
            this.param_mods.Clear();
            bool flag = false;
            bool flag2 = false;
            int num = 0;
            while (true)
            {
                if (flag)
                {
                    base.RaiseError(false, "CS0231. A params parameter must be the last parameter in a formal parameter list.");
                }
                num++;
                this.Parse_Attributes();
                ParamMod none = ParamMod.None;
                if (base.IsCurrText("Optional"))
                {
                    this.Match("Optional");
                    flag2 = true;
                }
                else if (flag2)
                {
                    this.Match("Optional");
                }
                int objectClassId = base.ObjectClassId;
                if (base.IsCurrText("ByRef"))
                {
                    if (isIndexer)
                    {
                        base.RaiseError(false, "CS0631. Indexers can't have ref or out parameters.");
                    }
                    this.Match("ByRef");
                    none = ParamMod.RetVal;
                }
                else if (base.IsCurrText("ParamArray"))
                {
                    this.Match("ParamArray");
                    if (base.IsCurrText("ByRef"))
                    {
                        base.RaiseError(false, "CS1611. The params parameter cannot be declared as ref or out.");
                    }
                    flag = true;
                }
                else
                {
                    this.Match("ByVal");
                }
                int num3 = this.Parse_Ident();
                string str = "";
                if (base.IsCurrText('('))
                {
                    flag = true;
                    str = this.Parse_TypeNameModifier();
                }
                if (base.IsCurrText("As"))
                {
                    this.Match("As");
                    objectClassId = this.Parse_Type();
                    if (base.IsCurrText('('))
                    {
                        string str2 = this.Parse_TypeNameModifier();
                        string str3 = base.GetName(objectClassId) + str2;
                        objectClassId = base.NewVar();
                        base.SetName(objectClassId, str3);
                        this.Gen(base.code.OP_EVAL_TYPE, 0, 0, objectClassId);
                    }
                    if (flag)
                    {
                        string str4 = base.GetName(objectClassId) + str;
                        objectClassId = base.NewVar();
                        base.SetName(objectClassId, str4);
                        this.Gen(base.code.OP_EVAL_TYPE, 0, 0, objectClassId);
                        if (CSLite_System.GetRank(base.GetName(objectClassId)) != 1)
                        {
                            base.RaiseError(false, "CS0225. The params parameter must be a single dimensional array.");
                        }
                    }
                }
                else if (this.OPTION_STRICT)
                {
                    base.RaiseError(false, "VB00006. Option Strict On requires all variable declarations to have an 'As' clause.");
                }
                this.Gen(base.code.OP_ASSIGN_TYPE, num3, objectClassId, 0);
                if (!isIndexer)
                {
                    if (flag)
                    {
                        this.Gen(base.code.OP_ADD_PARAMS, sub_id, num3, 0);
                    }
                    else
                    {
                        this.Gen(base.code.OP_ADD_PARAM, sub_id, num3, (int) none);
                    }
                }
                if (flag2)
                {
                    this.Match('=');
                    int res = this.Parse_Expression();
                    this.Gen(base.code.OP_ADD_DEFAULT_VALUE, sub_id, num3, res);
                }
                foreach (int num5 in this.param_ids)
                {
                    if (base.GetName(num5) == base.GetName(num3))
                    {
                        base.RaiseErrorEx(false, "CS0100. The parameter name '{0}' is a duplicate.", new object[] { base.GetName(num3) });
                    }
                }
                this.param_ids.Add(num3);
                this.param_type_ids.Add(objectClassId);
                this.param_mods.Add((int) none);
                if (!base.CondMatch(','))
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
            this.MatchLineTerminator();
        }

        public override void Parse_Program()
        {
            this.for_loop_stack.Clear();
            this.exit_kind_stack.Clear();
            this.PushExitKind(ExitKind.None);
            base.DECLARE_SWITCH = false;
            this.Gen(base.code.OP_UPCASE_ON, 0, 0, 0);
            this.Gen(base.code.OP_EXPLICIT_ON, 0, 0, 0);
            this.Gen(base.code.OP_STRICT_ON, 0, 0, 0);
            int id = base.NewVar();
            base.SetName(id, "System");
            int res = base.NewRef("Math");
            this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
            this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, id, 0, res);
            this.Gen(base.code.OP_BEGIN_USING, res, 0, 0);
            this.Parse_Start();
        }

        private void Parse_PropertyMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_modifiers, ClassKind ck)
        {
            this.Match("Property");
            int num = this.Parse_Ident();
            this.param_ids.Clear();
            this.param_type_ids.Clear();
            this.param_mods.Clear();
            if (base.IsCurrText('('))
            {
                this.Match('(');
                if (!base.IsCurrText(')'))
                {
                    this.Parse_ParameterList(num, false);
                }
                this.Match(')');
            }
            int num2 = 0x10;
            if (base.IsCurrText("As"))
            {
                this.Match("As");
                this.Parse_Attributes();
                num2 = this.Parse_TypeEx();
                base.DiscardInstruction(base.code.OP_ASSIGN_TYPE, num, -1, -1);
                this.Gen(base.code.OP_ASSIGN_TYPE, num, num2, 0);
            }
            base.BeginProperty(num, ml, num2, 0);
            if (ml.HasModifier(Modifier.Default))
            {
                if (this.param_ids.Count == 0)
                {
                    base.RaiseError(false, "VB00004. Properties with no required parameters cannot be declared 'Default'.");
                }
                else
                {
                    this.Gen(base.code.OP_SET_DEFAULT, num, 0, 0);
                }
            }
            if (base.IsCurrText("Implements"))
            {
                this.Parse_ImplementsClause(num);
            }
            int num3 = 0;
            int num4 = 0;
        Label_0110:
            this.MatchLineTerminator();
            this.Parse_Attributes();
            if (base.IsCurrText("Get"))
            {
                this.curr_prop_id = num;
                this.Match("Get");
                base.DECLARE_SWITCH = false;
                this.valid_this_context = true;
                this.MatchLineTerminator();
                int id = base.NewVar();
                base.SetName(id, "get_" + base.GetName(num));
                this.BeginMethod(id, MemberKind.Method, ml, num2);
                num3++;
                if (num3 > 1)
                {
                    base.RaiseError(true, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
                }
                for (int i = 0; i < this.param_ids.Count; i++)
                {
                    base.DiscardInstruction(base.code.OP_ADD_PARAM, num, -1, -1);
                    int num7 = base.NewVar();
                    base.SetName(num7, base.GetName(this.param_ids[i]));
                    this.Gen(base.code.OP_ASSIGN_TYPE, num7, this.param_type_ids[i], 0);
                    this.Gen(base.code.OP_ADD_PARAM, id, num7, 0);
                }
                this.InitMethod(id);
                this.Parse_Block();
                this.EndMethod(id);
                this.Gen(base.code.OP_ADD_READ_ACCESSOR, num, id, 0);
                base.DECLARE_SWITCH = true;
                this.valid_this_context = false;
                this.Match("End");
                this.Match("Get");
                this.curr_prop_id = 0;
                goto Label_0110;
            }
            if (base.IsCurrText("Set"))
            {
                this.valid_this_context = true;
                this.Match("Set");
                int num8 = base.NewVar();
                base.SetName(num8, "set_" + base.GetName(num));
                this.BeginMethod(num8, MemberKind.Method, ml, num2);
                num4++;
                if (num4 > 1)
                {
                    base.RaiseError(true, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
                }
                if (this.IsLineTerminator())
                {
                    int num9;
                    for (int j = 0; j < this.param_ids.Count; j++)
                    {
                        num9 = base.NewVar();
                        base.SetName(num9, base.GetName(this.param_ids[j]));
                        this.Gen(base.code.OP_ASSIGN_TYPE, num9, this.param_type_ids[j], 0);
                        this.Gen(base.code.OP_ADD_PARAM, num8, num9, 0);
                    }
                    num9 = base.NewVar();
                    base.SetName(num9, "value");
                    this.Gen(base.code.OP_ADD_PARAM, num8, num9, 0);
                    this.Gen(base.code.OP_ASSIGN_TYPE, num9, num2, 0);
                    base.DECLARE_SWITCH = false;
                    this.InitMethod(num8);
                    this.MatchLineTerminator();
                }
                else
                {
                    if (base.IsCurrText('('))
                    {
                        for (int k = 0; k < this.param_ids.Count; k++)
                        {
                            int num12 = base.NewVar();
                            base.SetName(num12, base.GetName(this.param_ids[k]));
                            this.Gen(base.code.OP_ASSIGN_TYPE, num12, this.param_type_ids[k], 0);
                            this.Gen(base.code.OP_ADD_PARAM, num8, num12, 0);
                        }
                        this.Match('(');
                        if (!base.IsCurrText(')'))
                        {
                            this.Parse_ParameterList(num8, false);
                        }
                        this.Match(')');
                    }
                    base.DECLARE_SWITCH = false;
                    this.InitMethod(num8);
                    this.MatchLineTerminator();
                }
                this.Parse_Block();
                this.EndMethod(num8);
                this.Gen(base.code.OP_ADD_WRITE_ACCESSOR, num, num8, 0);
                base.DECLARE_SWITCH = true;
                this.valid_this_context = false;
                this.Match("End");
                this.Match("Set");
                goto Label_0110;
            }
            base.EndProperty(num);
            if ((num3 + num4) == 0)
            {
                if (ml.HasModifier(Modifier.Abstract))
                {
                    return;
                }
                base.RaiseErrorEx(true, "CS0548. '{0}' : property or indexer must have at least one accessor.", new object[] { base.GetName(num) });
            }
            this.Match("End");
            this.Match("Property");
            this.MatchLineTerminator();
        }

        private void Parse_RaiseEventStatement()
        {
            this.Match("RaiseEvent");
            int num = this.Parse_SimpleNameExpression();
            this.Gen(base.code.OP_RAISE_EVENT, num, 0, 0);
            if (base.IsCurrText('('))
            {
                this.Match('(');
                int res = base.NewVar();
                this.Gen(base.code.OP_CALL, num, this.Parse_ArgumentList(')', num, num), res);
                this.Match(')');
            }
            else
            {
                int num3 = base.NewVar();
                this.Gen(base.code.OP_BEGIN_CALL, num, 0, 0);
                this.Gen(base.code.OP_PUSH, num, 0, 0);
                this.Gen(base.code.OP_CALL, num, 0, num3);
            }
            this.MatchStatementTerminator();
        }

        private void Parse_ReDimStatement()
        {
            this.Match("ReDim");
            if (base.IsCurrText("Preserve"))
            {
                this.Match("Preserve");
            }
            do
            {
                int num = this.Parse_Expression();
            }
            while (base.CondMatch(','));
            this.MatchStatementTerminator();
        }

        private int Parse_RelationalExpression()
        {
            int num = this.Parse_ShiftExpression();
            while ((((base.IsCurrText('=') || base.IsCurrText("<>")) || (base.IsCurrText('>') || base.IsCurrText(">="))) || ((base.IsCurrText('<') || base.IsCurrText("<=")) || (base.IsCurrText("Is") && !this.typeof_expression))) || base.IsCurrText("Like"))
            {
                if (base.IsCurrText('='))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_EQ, num, this.Parse_ShiftExpression());
                }
                else
                {
                    if (base.IsCurrText("<>"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_NE, num, this.Parse_ShiftExpression());
                        continue;
                    }
                    if (base.IsCurrText('>'))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_GT, num, this.Parse_ShiftExpression());
                        continue;
                    }
                    if (base.IsCurrText(">="))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_GE, num, this.Parse_ShiftExpression());
                        continue;
                    }
                    if (base.IsCurrText('<'))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_LT, num, this.Parse_ShiftExpression());
                        continue;
                    }
                    if (base.IsCurrText("<="))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_LE, num, this.Parse_ShiftExpression());
                        continue;
                    }
                    if (base.IsCurrText("Like"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_EQ, num, this.Parse_ShiftExpression());
                        continue;
                    }
                    if (base.IsCurrText("Is"))
                    {
                        this.Call_SCANNER();
                        num = base.BinOp(base.code.OP_EQ, num, this.Parse_ShiftExpression());
                    }
                }
            }
            return num;
        }

        private void Parse_RemoveHandlerStatement()
        {
            this.Match("RemoveHandler");
            int num = this.Parse_Expression();
            this.Match(',');
            int num2 = this.Parse_Expression();
            int res = base.NewVar();
            this.Gen(base.code.OP_MINUS, num, num2, res);
            this.Gen(base.code.OP_ASSIGN, num, res, num);
            this.MatchStatementTerminator();
        }

        private void Parse_ResumeStatement()
        {
            this.Match("Resume");
            if (base.IsCurrText("Next"))
            {
                this.Match("Next");
                this.Gen(base.code.OP_RESUME_NEXT, 0, 0, 0);
            }
            else if (base.curr_token.tokenClass == TokenClass.Identifier)
            {
                int id = this.Parse_Ident();
                base.PutKind(id, MemberKind.Label);
                this.Gen(base.code.OP_GOTO_START, id, 0, 0);
            }
            else
            {
                this.Gen(base.code.OP_RESUME, 0, 0, 0);
            }
            this.MatchStatementTerminator();
        }

        private void Parse_ReturnStatement()
        {
            this.Match("Return");
            if (!this.IsStatementTerminator())
            {
                int currLevel = base.CurrLevel;
                int resultId = base.GetResultId(currLevel);
                this.Gen(base.code.OP_ASSIGN, resultId, this.Parse_Expression(), resultId);
            }
            this.Gen(base.code.OP_EXIT_SUB, 0, 0, 0);
            this.MatchStatementTerminator();
        }

        private void Parse_SelectStatement()
        {
            this.Match("Select");
            if (base.IsCurrText("Case"))
            {
                this.Match("Case");
            }
            this.PushExitKind(ExitKind.Select);
            int label = base.NewLabel();
            base.BreakStack.Push(label, ExitKind.Select);
            int num2 = this.Parse_Expression();
            this.MatchStatementTerminator();
            while (base.IsCurrText("Case"))
            {
                int num3 = base.NewLabel();
                this.Match("Case");
                if (!base.IsCurrText("Else"))
                {
                    int num4 = base.NewVar(true);
                    this.Gen(base.code.OP_ASSIGN, num4, base.TRUE_id, num4);
                    do
                    {
                        if (base.IsCurrText("Is"))
                        {
                            this.Match("Is");
                        }
                        int op = 0;
                        if (base.IsCurrText('='))
                        {
                            op = base.code.OP_EQ;
                            this.Call_SCANNER();
                        }
                        else if (base.IsCurrText("<>"))
                        {
                            op = base.code.OP_NE;
                            this.Call_SCANNER();
                        }
                        else if (base.IsCurrText('>'))
                        {
                            op = base.code.OP_GT;
                            this.Call_SCANNER();
                        }
                        else if (base.IsCurrText(">="))
                        {
                            op = base.code.OP_GE;
                            this.Call_SCANNER();
                        }
                        else if (base.IsCurrText('<'))
                        {
                            op = base.code.OP_LT;
                            this.Call_SCANNER();
                        }
                        else if (base.IsCurrText("<="))
                        {
                            op = base.code.OP_LE;
                            this.Call_SCANNER();
                        }
                        if (op != 0)
                        {
                            int num6 = this.Parse_Expression();
                            int res = base.NewVar(true);
                            this.Gen(op, num2, num6, res);
                            this.Gen(base.code.OP_BITWISE_AND, num4, res, num4);
                        }
                        else
                        {
                            int num8 = this.Parse_Expression();
                            if (base.IsCurrText("To"))
                            {
                                this.Match("To");
                                int num9 = this.Parse_Expression();
                                int num10 = base.NewVar(true);
                                int num11 = base.NewVar(true);
                                this.Gen(base.code.OP_GE, num2, num8, num10);
                                this.Gen(base.code.OP_LE, num2, num9, num11);
                                this.Gen(base.code.OP_BITWISE_AND, num4, num10, num4);
                                this.Gen(base.code.OP_BITWISE_AND, num4, num11, num4);
                            }
                            else
                            {
                                int num12 = base.NewVar(true);
                                this.Gen(base.code.OP_EQ, num2, num8, num12);
                                this.Gen(base.code.OP_BITWISE_AND, num4, num12, num4);
                            }
                        }
                    }
                    while (base.CondMatch(','));
                    this.Gen(base.code.OP_GO_FALSE, num3, num4, 0);
                    this.MatchStatementTerminator();
                    this.Parse_Block();
                    this.Gen(base.code.OP_GO, label, 0, 0);
                    base.SetLabelHere(num3);
                }
                else
                {
                    this.Match("Else");
                    this.MatchStatementTerminator();
                    this.Parse_Block();
                    base.SetLabelHere(num3);
                }
            }
            base.SetLabelHere(label);
            base.BreakStack.Pop();
            this.PopExitKind();
            this.Match("End");
            this.Match("Select");
            this.MatchStatementTerminator();
        }

        private int Parse_ShiftExpression()
        {
            int num = this.Parse_ConcatenationExpression();
            while (base.IsCurrText("<<") || base.IsCurrText(">>"))
            {
                if (base.IsCurrText("<<"))
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_LEFT_SHIFT, num, this.Parse_ConcatenationExpression());
                }
                else
                {
                    this.Call_SCANNER();
                    num = base.BinOp(base.code.OP_RIGHT_SHIFT, num, this.Parse_ConcatenationExpression());
                }
            }
            return num;
        }

        private int Parse_SimpleExpression()
        {
            int currThisID;
            if (base.IsCurrText('('))
            {
                this.Match('(');
                currThisID = this.Parse_Expression();
                this.Match(')');
            }
            else if (base.IsCurrText("CBool"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_BOOLEAN, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CByte"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_BYTE, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CChar"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_CHAR, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CDate"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Match(')');
            }
            else if (base.IsCurrText("CDec"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_DECIMAL, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CDbl"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_DOUBLE, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CInt"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_INT, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CType"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                int num2 = this.Parse_Expression();
                this.Match(',');
                int num3 = this.Parse_TypeEx();
                this.Gen(base.code.OP_CAST, num3, num2, currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("DirectCast"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                int num4 = this.Parse_Expression();
                this.Match(',');
                int num5 = this.Parse_Expression();
                this.Gen(base.code.OP_CAST, num5, num4, currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CLng"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_LONG, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CObj"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_CAST, 0x10, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CShort"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_SHORT, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CSng"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_FLOAT, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("CStr"))
            {
                this.Call_SCANNER();
                this.Match('(');
                currThisID = base.NewVar();
                this.Gen(base.code.OP_TO_STRING, 0, this.Parse_Expression(), currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("Me"))
            {
                this.Match("Me");
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
            else if (base.IsCurrText("MyClass"))
            {
                this.Match("MyClass");
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
            else if (base.IsCurrText("GetType"))
            {
                base.DiscardInstruction(base.code.OP_EVAL, -1, -1, base.curr_token.id);
                this.Match("GetType");
                currThisID = base.NewVar();
                this.Match('(');
                this.Gen(base.code.OP_TYPEOF, this.Parse_TypeEx(), 0, currThisID);
                this.Match(')');
            }
            else if (base.IsCurrText("TypeOf"))
            {
                this.typeof_expression = true;
                this.Match("TypeOf");
                int num6 = this.Parse_Expression();
                this.typeof_expression = false;
                this.Match("Is");
                currThisID = base.NewVar();
                this.Gen(base.code.OP_IS, num6, this.Parse_TypeEx(), currThisID);
            }
            else if (base.IsCurrText("AddressOf"))
            {
                this.Match("AddressOf");
                currThisID = base.NewVar();
                this.Gen(base.code.OP_ADDRESS_OF, this.Parse_Expression(), 0, currThisID);
            }
            else if (!base.IsCurrText("MyBase"))
            {
                if (base.IsCurrText("New"))
                {
                    this.Match("New");
                    int num12 = this.Parse_Type();
                    this.new_type_id = num12;
                    currThisID = base.NewVar();
                    this.Gen(base.code.OP_CREATE_OBJECT, num12, 0, currThisID);
                    if (base.IsCurrText('('))
                    {
                        this.Match('(');
                        this.Gen(base.code.OP_CALL, num12, this.Parse_ArgumentList(')', num12, currThisID), 0);
                        this.Match(')');
                    }
                    else
                    {
                        this.Gen(base.code.OP_BEGIN_CALL, num12, 0, 0);
                        this.Gen(base.code.OP_PUSH, currThisID, 0, 0);
                        this.Gen(base.code.OP_CALL, num12, 0, 0);
                    }
                    if (base.IsCurrText('{'))
                    {
                        string str = base.GetName(this.new_type_id) + "[]";
                        int id = base.NewVar();
                        base.SetName(id, str);
                        this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
                        int num14 = this.Parse_ArrayInitializer(id);
                        this.Gen(base.code.OP_ASSIGN_TYPE, currThisID, id, 0);
                        this.Gen(base.code.OP_ASSIGN_TYPE, num14, id, 0);
                        this.Gen(base.code.OP_ASSIGN, currThisID, num14, currThisID);
                    }
                    return currThisID;
                }
                if (base.IsCurrText("true"))
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
                else
                {
                    currThisID = this.Parse_SimpleNameExpression();
                }
            }
            else
            {
                this.Match("MyBase");
                currThisID = base.CurrThisID;
                if (base.GetName(currThisID) != "this")
                {
                    base.RaiseError(false, "CS1511. Keyword base is not available in a static method.");
                }
                if (base.IsCurrText('.'))
                {
                    base.REF_SWITCH = true;
                    this.Match('.');
                    int res = base.NewVar();
                    this.Gen(base.code.OP_EVAL_BASE_TYPE, base.CurrClassID, 0, res);
                    int num8 = base.NewVar();
                    this.Gen(base.code.OP_CAST, res, base.CurrThisID, num8);
                    currThisID = this.Parse_Ident();
                    this.Gen(base.code.OP_CREATE_REFERENCE, num8, 0, currThisID);
                    if (base.IsCurrText('('))
                    {
                        int num9 = currThisID;
                        currThisID = base.NewVar();
                        this.Match('(');
                        this.Gen(base.code.OP_CALL_BASE, num9, this.Parse_ArgumentList(')', num9, base.CurrThisID), currThisID);
                        this.Match(')');
                    }
                }
                else
                {
                    this.Match('[');
                    int num10 = base.NewVar();
                    this.Gen(base.code.OP_EVAL_BASE_TYPE, base.CurrClassID, 0, num10);
                    currThisID = base.NewVar();
                    this.Gen(base.code.OP_CAST, num10, base.CurrThisID, currThisID);
                    int num11 = base.NewVar();
                    this.Gen(base.code.OP_CREATE_INDEX_OBJECT, currThisID, 0, num11);
                    do
                    {
                        this.Gen(base.code.OP_ADD_INDEX, num11, this.Parse_Expression(), currThisID);
                    }
                    while (base.CondMatch(','));
                    this.Gen(base.code.OP_SETUP_INDEX_OBJECT, num11, 0, 0);
                    this.Match(']');
                    currThisID = num11;
                }
            }
            while (true)
            {
                while (base.IsCurrText('('))
                {
                    int num15 = currThisID;
                    currThisID = base.NewVar();
                    this.Match('(');
                    this.Gen(base.code.OP_CALL, num15, this.Parse_ArgumentList(')', num15, base.CurrThisID), currThisID);
                    this.Match(')');
                    if (base.IsCurrText('{'))
                    {
                        string str2 = base.GetName(this.new_type_id) + "[]";
                        int num16 = base.NewVar();
                        base.SetName(num16, str2);
                        this.Gen(base.code.OP_EVAL_TYPE, 0, 0, num16);
                        int num17 = this.Parse_ArrayInitializer(num16);
                        this.Gen(base.code.OP_ASSIGN_TYPE, currThisID, num16, 0);
                        this.Gen(base.code.OP_ASSIGN_TYPE, num17, num16, 0);
                        this.Gen(base.code.OP_ASSIGN, currThisID, num17, currThisID);
                    }
                }
                if (!base.IsCurrText('['))
                {
                    if (!base.IsCurrText('.'))
                    {
                        return currThisID;
                    }
                    base.REF_SWITCH = true;
                    this.Match('.');
                    int num19 = currThisID;
                    currThisID = this.Parse_Ident();
                    this.Gen(base.code.OP_CREATE_REFERENCE, num19, 0, currThisID);
                }
                else
                {
                    int num18 = base.NewVar();
                    this.Gen(base.code.OP_CREATE_INDEX_OBJECT, currThisID, 0, num18);
                    this.Match('[');
                    do
                    {
                        this.Gen(base.code.OP_ADD_INDEX, num18, this.Parse_Expression(), currThisID);
                    }
                    while (base.CondMatch(','));
                    this.Gen(base.code.OP_SETUP_INDEX_OBJECT, num18, 0, 0);
                    this.Match(']');
                    currThisID = num18;
                }
            }
        }

        private int Parse_SimpleNameExpression()
        {
            return this.Parse_IdentOrType();
        }

        private void Parse_Start()
        {
            while (this.IsLineTerminator())
            {
                if (base.IsEOF())
                {
                    return;
                }
                this.MatchLineTerminator();
            }
            base.DECLARE_SWITCH = true;
            while (base.IsCurrText("Option"))
            {
                this.Parse_OptionStatement();
            }
            while (base.IsCurrText("Imports"))
            {
                this.Parse_ImportsStatement();
            }
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
                if (this.IsLineTerminator())
                {
                    this.MatchLineTerminator();
                    return;
                }
            }
            else
            {
                base.Backup_SCANNER(k);
            }
            if (base.IsCurrText("print"))
            {
                this.Parse_PrintStatement();
            }
            else if (base.IsCurrText("println"))
            {
                this.Parse_PrintlnStatement();
            }
            else if (base.IsCurrText("Static"))
            {
                this.Parse_LocalDeclarationStatement(LocalModifier.Static);
            }
            else if (base.IsCurrText("Dim"))
            {
                this.Parse_LocalDeclarationStatement(LocalModifier.Dim);
            }
            else if (base.IsCurrText("Const"))
            {
                this.Parse_LocalDeclarationStatement(LocalModifier.Const);
            }
            else if (base.IsCurrText("With"))
            {
                this.Parse_WithStatement();
            }
            else if (base.IsCurrText("SyncLock"))
            {
                this.Parse_SyncLockStatement();
            }
            else if (base.IsCurrText("RaiseEvent"))
            {
                this.Parse_RaiseEventStatement();
            }
            else if (base.IsCurrText("AddHandler"))
            {
                this.Parse_AddHandlerStatement();
            }
            else if (base.IsCurrText("RemoveHandler"))
            {
                this.Parse_RemoveHandlerStatement();
            }
            else if (base.IsCurrText("Call"))
            {
                this.Parse_InvocationStatement();
            }
            else if (base.IsCurrText("If"))
            {
                this.Parse_IfStatement();
            }
            else if (base.IsCurrText("Select"))
            {
                this.Parse_SelectStatement();
            }
            else if (base.IsCurrText("While"))
            {
                this.Parse_WhileStatement();
            }
            else if (base.IsCurrText("Do"))
            {
                this.Parse_DoLoopStatement();
            }
            else if (base.IsCurrText("For"))
            {
                this.Parse_ForNextStatement();
            }
            else if (base.IsCurrText("Try"))
            {
                this.Parse_TryStatement();
            }
            else if (base.IsCurrText("Throw"))
            {
                this.Parse_ThrowStatement();
            }
            else if (base.IsCurrText("Error"))
            {
                this.Parse_ErrorStatement();
            }
            else if (base.IsCurrText("Resume"))
            {
                this.Parse_ResumeStatement();
            }
            else if (base.IsCurrText("On"))
            {
                this.Parse_OnErrorStatement();
            }
            else if (base.IsCurrText("GoTo"))
            {
                this.Parse_GoToStatement();
            }
            else if (base.IsCurrText("Exit"))
            {
                this.Parse_ExitStatement();
            }
            else if (base.IsCurrText("Stop"))
            {
                this.Parse_StopStatement();
            }
            else if (base.IsCurrText("End"))
            {
                this.Parse_EndStatement();
            }
            else if (base.IsCurrText("ReDim"))
            {
                this.Parse_ReDimStatement();
            }
            else if (base.IsCurrText("Erase"))
            {
                this.Parse_EraseStatement();
            }
            else if (base.IsCurrText("Return"))
            {
                this.Parse_ReturnStatement();
            }
            else
            {
                this.Parse_AssignmentStatement();
            }
        }

        private void Parse_Statements()
        {
            while ((((!base.IsCurrText("End") && !base.IsCurrText("Else")) && (!base.IsCurrText("ElseIf") && !base.IsCurrText("Case"))) && ((!base.IsCurrText("Loop") && !base.IsCurrText("Next")) && (!base.IsCurrText("Catch") && !base.IsCurrText("Finally")))) && !base.IsEOF())
            {
                this.Parse_Statement();
            }
        }

        private void Parse_StopStatement()
        {
            this.Match("Stop");
            this.Gen(base.code.OP_HALT, 0, 0, 0);
            this.MatchStatementTerminator();
        }

        private void Parse_StructureDeclaration(ModifierList ml)
        {
            base.DECLARE_SWITCH = true;
            this.has_constructor = false;
            base.CheckModifiers(ml, this.structure_modifiers);
            this.Match("Structure");
            int num = this.Parse_Ident();
            this.MatchLineTerminator();
            base.BeginStruct(num, ml);
            this.Gen(base.code.OP_ADD_ANCESTOR, num, base.ObjectClassId, 0);
            if (!base.IsCurrText("End"))
            {
                this.Parse_ClassBody(num, ml, false);
            }
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(num);
            }
            if (!this.has_constructor)
            {
                this.CreateDefaultConstructor(num, false);
            }
            base.EndStruct(num);
            this.Match("End");
            this.Match("Structure");
            this.MatchLineTerminator();
        }

        private void Parse_SubDeclaration(int class_id, ModifierList ml, ModifierList owner_ml, ClassKind ck)
        {
            int num;
            this.Match("Sub");
            this.PushExitKind(ExitKind.Sub);
            int num2 = 1;
            this.valid_this_context = true;
            if (base.IsCurrText("New"))
            {
                IntegerList list;
                bool flag = ml.HasModifier(Modifier.Static);
                base.CheckModifiers(ml, this.constructor_modifiers);
                if (flag && this.HasAccessModifier(ml))
                {
                    base.RaiseErrorEx(false, "CS0515. '{0}' : access modifiers are not allowed on static constructors.", new object[] { base.GetName(class_id) });
                }
                num = base.NewVar();
                this.Call_SCANNER();
                this.BeginMethod(num, MemberKind.Constructor, ml, 1);
                if (base.IsCurrText('('))
                {
                    this.Match('(');
                    if (!base.IsCurrText(')'))
                    {
                        if (flag)
                        {
                            base.RaiseErrorEx(false, "CS0132. '{0}' : a static constructor must be parameterless.", new object[] { base.GetName(class_id) });
                        }
                        this.Parse_ParameterList(num, false);
                    }
                    this.Match(')');
                }
                this.InitMethod(num);
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
                this.MatchLineTerminator();
                this.Parse_Block();
                this.EndMethod(num);
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
                this.Match("End");
                this.Match("Sub");
                this.PopExitKind();
                this.MatchLineTerminator();
            }
            else
            {
                num = this.Parse_Ident();
                this.CheckMethodModifiers(num, class_id, ml, owner_ml);
                this.BeginMethod(num, MemberKind.Method, ml, num2);
                if (this.explicit_intf_id > 0)
                {
                    this.Gen(base.code.OP_ADD_EXPLICIT_INTERFACE, num, this.explicit_intf_id, 0);
                }
                if (base.IsCurrText('('))
                {
                    this.Match('(');
                    if (!base.IsCurrText(')'))
                    {
                        this.Parse_ParameterList(num, false);
                    }
                    this.Match(')');
                }
                if (ml.HasModifier(Modifier.Extern))
                {
                    base.RaiseErrorEx(false, "CS0179. '{0}' cannot be extern and declare a body.", new object[] { base.GetName(num) });
                }
                if (ck != ClassKind.Interface)
                {
                    this.InitMethod(num);
                    if (ml.HasModifier(Modifier.Abstract))
                    {
                        string name = base.GetName(num);
                        base.RaiseErrorEx(false, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { name });
                    }
                    if (base.GetName(num) == "Main")
                    {
                        this.Gen(base.code.OP_CHECKED, base.TRUE_id, 0, 0);
                    }
                    if (base.IsCurrText("Handles"))
                    {
                        this.Parse_HandlesClause();
                    }
                    else if (base.IsCurrText("Implements"))
                    {
                        this.Parse_ImplementsClause(num);
                    }
                    base.DECLARE_SWITCH = false;
                    this.MatchLineTerminator();
                    this.Parse_Block();
                    if (base.GetName(num) == "Main")
                    {
                        this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                    }
                    this.Match("End");
                    this.Match("Sub");
                }
                this.EndMethod(num);
                this.valid_this_context = false;
                base.DECLARE_SWITCH = true;
                this.PopExitKind();
                this.MatchLineTerminator();
            }
        }

        private void Parse_SyncLockStatement()
        {
            this.Match("SyncLock");
            int num = this.Parse_Expression();
            this.MatchStatementTerminator();
            if (!base.IsCurrText("End"))
            {
                this.Gen(base.code.OP_LOCK, num, 0, 0);
                int num2 = base.NewLabel();
                this.Gen(base.code.OP_TRY_ON, num2, 0, 0);
                this.Parse_Block();
                this.Gen(base.code.OP_FINALLY, 0, 0, 0);
                this.Gen(base.code.OP_UNLOCK, num, 0, 0);
                this.Gen(base.code.OP_EXIT_ON_ERROR, 0, 0, 0);
                this.Gen(base.code.OP_GOTO_CONTINUE, 0, 0, 0);
                base.SetLabelHere(num2);
                this.Gen(base.code.OP_TRY_OFF, 0, 0, 0);
            }
            this.Match("End");
            this.Match("SyncLock");
            this.MatchStatementTerminator();
        }

        private void Parse_ThrowStatement()
        {
            this.Match("Throw");
            if (!this.IsStatementTerminator())
            {
                this.Gen(base.code.OP_THROW, this.Parse_Expression(), 0, 0);
            }
            else
            {
                this.Gen(base.code.OP_THROW, 0, 0, 0);
            }
            this.MatchStatementTerminator();
        }

        private void Parse_TryStatement()
        {
            this.Match("Try");
            this.PushExitKind(ExitKind.Try);
            this.MatchStatementTerminator();
            int label = base.NewLabel();
            base.BreakStack.Push(label, ExitKind.Try);
            this.Gen(base.code.OP_TRY_ON, label, 0, 0);
            this.Parse_Block();
            int num2 = base.NewLabel();
            this.Gen(base.code.OP_GO, num2, 0, 0);
            while (base.IsCurrText("Catch"))
            {
                base.DECLARE_SWITCH = true;
                this.Match("Catch");
                int num4 = base.NewLabel();
                if (base.IsCurrText("When"))
                {
                    base.DECLARE_SWITCH = false;
                    this.Match("When");
                    int num5 = this.Parse_BooleanExpression();
                    this.Gen(base.code.OP_CATCH, 0, 0, 0);
                    this.Gen(base.code.OP_GO_FALSE, num4, num5, 0);
                }
                else
                {
                    int num3 = this.Parse_Ident();
                    base.DECLARE_SWITCH = false;
                    this.Match("As");
                    this.Gen(base.code.OP_DECLARE_LOCAL_SIMPLE, num3, base.CurrSubId, 0);
                    this.Gen(base.code.OP_ASSIGN_TYPE, num3, this.Parse_Type(), 0);
                    this.Gen(base.code.OP_CATCH, num3, 0, 0);
                    if (base.IsCurrText("When"))
                    {
                        this.Match("When");
                        int num6 = this.Parse_BooleanExpression();
                        this.Gen(base.code.OP_GO_FALSE, num4, num6, 0);
                    }
                }
                this.MatchStatementTerminator();
                this.Parse_Block();
                this.Gen(base.code.OP_DISCARD_ERROR, 0, 0, 0);
                base.SetLabelHere(num4);
                base.SetLabelHere(num2);
            }
            if (base.IsCurrText("Finally"))
            {
                this.Match("Finally");
                this.MatchStatementTerminator();
                base.SetLabelHere(num2);
                this.Gen(base.code.OP_FINALLY, 0, 0, 0);
                this.Parse_Block();
                this.Gen(base.code.OP_EXIT_ON_ERROR, 0, 0, 0);
                this.Gen(base.code.OP_GOTO_CONTINUE, 0, 0, 0);
            }
            base.SetLabelHere(label);
            this.Gen(base.code.OP_TRY_OFF, 0, 0, 0);
            this.Match("End");
            this.Match("Try");
            base.BreakStack.Pop();
            this.PopExitKind();
            this.MatchStatementTerminator();
        }

        private int Parse_Type()
        {
            int id = this.Parse_NonArrayType();
            if (!base.IsCurrText('['))
            {
                return id;
            }
            int num2 = id;
            int num3 = base.NewVar();
            string str = "";
            do
            {
                str = str + this.Parse_ArrayTypeModifier();
            }
            while (base.IsCurrText('('));
            base.SetName(num3, base.GetName(id) + str);
            this.Gen(base.code.OP_EVAL_TYPE, num2, 0, num3);
            return num3;
        }

        private void Parse_TypeDeclaration()
        {
            this.Parse_Attributes();
            ModifierList ml = this.Parse_Modifiers();
            if (base.IsCurrText("Module"))
            {
                this.Parse_ModuleDeclaration(ml);
            }
            else
            {
                this.Parse_NonModuleDeclaration(ml);
            }
        }

        private int Parse_TypeEx()
        {
            int id = this.Parse_Type();
            if (base.IsCurrText('('))
            {
                string str = this.Parse_TypeNameModifier();
                string str2 = base.GetName(id) + str;
                id = base.NewVar();
                base.SetName(id, str2);
                this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
            }
            return id;
        }

        private void Parse_TypeImplementsClause(int class_id)
        {
            this.Match("Implements");
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
            this.MatchLineTerminator();
        }

        private string Parse_TypeNameModifier()
        {
            string str = "";
            this.Match('(');
            str = str + "[";
            if (!base.IsCurrText(')'))
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
            this.Match(')');
            return (str + "]");
        }

        private int Parse_UnaryNegationExpression()
        {
            if (base.IsCurrText('+'))
            {
                this.Call_SCANNER();
                return base.UnaryOp(base.code.OP_UNARY_PLUS, this.Parse_Expression());
            }
            if (base.IsCurrText('-'))
            {
                this.Call_SCANNER();
                return base.UnaryOp(base.code.OP_UNARY_MINUS, this.Parse_Expression());
            }
            if (base.IsCurrText("Not"))
            {
                this.Call_SCANNER();
                return base.UnaryOp(base.code.OP_NOT, this.Parse_Expression());
            }
            return this.Parse_ExponentiationExpression();
        }

        private void Parse_VariableDeclarator(ModifierList ml)
        {
            ArrayList list = new ArrayList();
            ArrayList sl = new ArrayList();
            IntegerList list3 = this.Parse_VariableIdentifiers(list, sl);
            int objectClassId = base.ObjectClassId;
            if (base.IsCurrText("As"))
            {
                this.Match("As");
                bool flag = base.curr_token.id == base.DATETIME_CLASS_id;
                if (base.IsCurrText("New") || flag)
                {
                    if (base.IsCurrText("New"))
                    {
                        this.Match("New");
                        flag = base.curr_token.id == base.DATETIME_CLASS_id;
                        objectClassId = this.Parse_Type();
                    }
                    else
                    {
                        objectClassId = this.Parse_TypeEx();
                    }
                    if (list3.Count == 1)
                    {
                        string str = (string) sl[0];
                        if ((str != null) && (str != ""))
                        {
                            base.RaiseError(true, "VB00003. Arrays cannot be declared with 'New'");
                        }
                        int num2 = list3[0];
                        this.Gen(base.code.OP_ASSIGN_TYPE, num2, objectClassId, 0);
                        base.BeginField(num2, ml, objectClassId);
                        base.DECLARE_SWITCH = false;
                        int num3 = base.NewVar();
                        this.BeginMethod(num3, MemberKind.Method, ml, 1);
                        this.InitMethod(num3);
                        int id = num2;
                        if (!ml.HasModifier(Modifier.Static))
                        {
                            id = base.NewVar();
                            base.SetName(id, base.GetName(num2));
                            this.Gen(base.code.OP_EVAL, 0, 0, id);
                            this.variable_initializers.Add(num3);
                        }
                        else
                        {
                            this.static_variable_initializers.Add(num3);
                        }
                        this.Gen(base.code.OP_CREATE_OBJECT, objectClassId, 0, id);
                        if (base.IsCurrText('('))
                        {
                            this.Match('(');
                            this.Gen(base.code.OP_CALL, objectClassId, this.Parse_ArgumentList(')', objectClassId, id), 0);
                            this.Match(')');
                        }
                        else
                        {
                            this.Gen(base.code.OP_BEGIN_CALL, objectClassId, 0, 0);
                            if (flag)
                            {
                                this.Gen(base.code.OP_PUSH, base.NewConst(0), 0, objectClassId);
                            }
                            this.Gen(base.code.OP_PUSH, id, 0, 0);
                            this.Gen(base.code.OP_CALL, objectClassId, 0, 0);
                        }
                        this.EndMethod(num3);
                        base.DECLARE_SWITCH = true;
                        base.EndField(num2);
                    }
                    else
                    {
                        base.RaiseError(true, "VB00002. Explicit initialization is not permitted with multiple variables declared with a single type specifier.");
                    }
                    return;
                }
                objectClassId = this.Parse_TypeEx();
            }
            else if (this.OPTION_STRICT)
            {
                base.RaiseError(false, "VB00006. Option Strict On requires all variable declarations to have an 'As' clause.");
            }
            for (int i = 0; i < list3.Count; i++)
            {
                int num6 = list3[i];
                if (((string) sl[i]) != "")
                {
                    base.BeginField(num6, ml, objectClassId);
                    int num7 = base.NewVar();
                    this.BeginMethod(num7, MemberKind.Method, ml, 1);
                    this.InitMethod(num7);
                    int num8 = num6;
                    if (!ml.HasModifier(Modifier.Static))
                    {
                        num8 = base.NewVar();
                        base.SetName(num8, base.GetName(num6));
                        this.Gen(base.code.OP_EVAL, 0, 0, num8);
                        this.variable_initializers.Add(num7);
                    }
                    else
                    {
                        this.static_variable_initializers.Add(num7);
                    }
                    string str2 = (string) sl[i];
                    int num9 = objectClassId;
                    int num10 = base.NewVar();
                    base.SetName(num10, base.GetName(num9) + str2);
                    this.Gen(base.code.OP_EVAL_TYPE, num9, 0, num10);
                    this.Gen(base.code.OP_ASSIGN_TYPE, num8, num10, 0);
                    this.Gen(base.code.OP_ASSIGN_TYPE, num6, num10, 0);
                    IntegerList list4 = (IntegerList) list[i];
                    this.Gen(base.code.OP_CREATE_OBJECT, num10, 0, num8);
                    int count = list4.Count;
                    if (count > 0)
                    {
                        this.Gen(base.code.OP_BEGIN_CALL, num10, 0, 0);
                        for (int j = 0; j < count; j++)
                        {
                            this.Gen(base.code.OP_INC, list4[j], 0, list4[j]);
                            this.Gen(base.code.OP_PUSH, list4[j], 0, num10);
                        }
                        this.Gen(base.code.OP_PUSH, num8, 0, 0);
                        this.Gen(base.code.OP_CALL, num10, count, 0);
                    }
                    objectClassId = num10;
                    this.EndMethod(num7);
                    base.EndField(num6);
                }
                else
                {
                    this.Gen(base.code.OP_ASSIGN_TYPE, num6, objectClassId, 0);
                    base.BeginField(num6, ml, objectClassId);
                    base.EndField(num6);
                }
            }
            if (base.IsCurrText("="))
            {
                this.Match('=');
                if (list3.Count == 1)
                {
                    int num13 = list3[0];
                    this.Gen(base.code.OP_ASSIGN_TYPE, num13, objectClassId, 0);
                    this.Parse_VariableInitializer(num13, objectClassId, ml);
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
            if (base.IsCurrText('('))
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
            if (base.IsCurrText('{'))
            {
                this.Gen(base.code.OP_ASSIGN, num2, this.Parse_ArrayInitializer(type_id), num2);
            }
            else
            {
                this.Gen(base.code.OP_ASSIGN, num2, this.Parse_Expression(), num2);
            }
            this.EndMethod(num);
            base.DECLARE_SWITCH = true;
        }

        private void Parse_VariableMemberDeclaration(int class_id, ModifierList ml, ModifierList owner_modifiers)
        {
            if (base.IsCurrText("Dim"))
            {
                this.Match("Dim");
            }
            do
            {
                this.Parse_VariableDeclarator(ml);
            }
            while (base.CondMatch(','));
            this.MatchLineTerminator();
        }

        public void Parse_WhileStatement()
        {
            this.Match("While");
            this.PushExitKind(ExitKind.While);
            int num = base.NewLabel();
            int l = base.NewLabel();
            base.SetLabelHere(l);
            this.Gen(base.code.OP_GO_FALSE, num, this.Parse_Expression(), 0);
            this.MatchStatementTerminator();
            base.BreakStack.Push(num, ExitKind.While);
            base.ContinueStack.Push(l);
            this.Parse_Block();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num);
            this.Match("End");
            this.Match("While");
            this.PopExitKind();
            this.MatchStatementTerminator();
        }

        private void Parse_WithStatement()
        {
            this.Match("With");
            int i = this.Parse_Expression();
            this.with_stack.Push(i);
            this.MatchStatementTerminator();
            if (!base.IsCurrText("End"))
            {
                this.Parse_Block();
            }
            this.with_stack.Pop();
            this.Match("End");
            this.Match("With");
            this.MatchStatementTerminator();
        }

        private void PopExitKind()
        {
            this.exit_kind_stack.Pop();
        }

        private void PushExitKind(ExitKind k)
        {
            this.exit_kind_stack.Push((int) k);
        }

        private ExitKind CurrExitKind
        {
            get
            {
                return (ExitKind) this.exit_kind_stack.Peek();
            }
        }

        private class ForLoopRec
        {
            public int id;
            public int lf;
            public int lg;
            public int step_id;
        }

        private class ForLoopStack
        {
            private ObjectStack s = new ObjectStack();

            public void Clear()
            {
                this.s.Clear();
            }

            public void Pop()
            {
                this.s.Pop();
            }

            public void Push(int id, int step_id, int lg, int lf)
            {
                VB_Parser.ForLoopRec v = new VB_Parser.ForLoopRec();
                v.id = id;
                v.step_id = step_id;
                v.lg = lg;
                v.lf = lf;
                this.s.Push(v);
            }

            public int Count
            {
                get
                {
                    return this.s.Count;
                }
            }

            public VB_Parser.ForLoopRec Top
            {
                get
                {
                    return (VB_Parser.ForLoopRec) this.s.Peek();
                }
            }
        }

        public enum LocalModifier
        {
            Static,
            Dim,
            Const
        }
    }
}

