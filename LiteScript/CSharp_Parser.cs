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

    internal class CSharp_Parser : BaseParser
    {
        private bool ACCESSOR_SWITCH = false;
        private Hashtable additive_operators;
        private Hashtable assign_operators;
        private ModifierList class_modifiers;
        private ModifierList constant_modifiers;
        private ModifierList constructor_modifiers;
        private ModifierList delegate_modifiers;
        private ModifierList destructor_modifiers;
        private ModifierList enum_modifiers;
        private ModifierList field_modifiers;
        private bool has_constructor = false;
        private CSLite_Types integral_types;
        private ModifierList interface_modifiers;
        private IntegerList local_variables;
        private ModifierList method_modifiers;
        private Hashtable multiplicative_operators;
        private bool no_gen = false;
        private ModifierList operator_modifiers;
        private StringList overloadable_binary_operators;
        private StringList overloadable_unary_operators;
        private IntegerList param_ids;
        private IntegerList param_mods;
        private IntegerList param_type_ids;
        private Hashtable relational_operators;
        private Hashtable shift_operators;
        private CSLite_Types standard_types;
        private IntegerList static_variable_initializers;
        private ModifierList structure_modifiers;
        protected StringList total_modifier_list;
        private bool valid_this_context = false;
        private IntegerList variable_initializers;

        public CSharp_Parser()
        {
            base.language = "CSharp";
            base.scanner = new CSharp_Scanner(this);
            base.upcase = false;
            this.variable_initializers = new IntegerList(false);
            this.static_variable_initializers = new IntegerList(false);
            this.param_ids = new IntegerList(true);
            this.param_type_ids = new IntegerList(true);
            this.param_mods = new IntegerList(true);
            this.local_variables = new IntegerList(false);
            base.keywords.Add("abstract");
            base.keywords.Add("as");
            base.keywords.Add("base");
            base.keywords.Add("bool");
            base.keywords.Add("break");
            base.keywords.Add("byte");
            base.keywords.Add("case");
            base.keywords.Add("catch");
            base.keywords.Add("char");
            base.keywords.Add("checked");
            base.keywords.Add("class");
            base.keywords.Add("const");
            base.keywords.Add("continue");
            base.keywords.Add("decimal");
            base.keywords.Add("default");
            base.keywords.Add("delegate");
            base.keywords.Add("do");
            base.keywords.Add("double");
            base.keywords.Add("else");
            base.keywords.Add("enum");
            base.keywords.Add("event");
            base.keywords.Add("explicit");
            base.keywords.Add("extern");
            base.keywords.Add("false");
            base.keywords.Add("finally");
            base.keywords.Add("fixed");
            base.keywords.Add("float");
            base.keywords.Add("for");
            base.keywords.Add("foreach");
            base.keywords.Add("goto");
            base.keywords.Add("if");
            base.keywords.Add("implicit");
            base.keywords.Add("in");
            base.keywords.Add("int");
            base.keywords.Add("interface");
            base.keywords.Add("internal");
            base.keywords.Add("is");
            base.keywords.Add("lock");
            base.keywords.Add("long");
            base.keywords.Add("namespace");
            base.keywords.Add("new");
            base.keywords.Add("null");
            base.keywords.Add("object");
            base.keywords.Add("operator");
            base.keywords.Add("out");
            base.keywords.Add("override");
            base.keywords.Add("params");
            base.keywords.Add("private");
            base.keywords.Add("protected");
            base.keywords.Add("public");
            base.keywords.Add("readonly");
            base.keywords.Add("ref");
            base.keywords.Add("return");
            base.keywords.Add("sbyte");
            base.keywords.Add("sealed");
            base.keywords.Add("short");
            base.keywords.Add("sizeof");
            base.keywords.Add("stackalloc");
            base.keywords.Add("static");
            base.keywords.Add("string");
            base.keywords.Add("struct");
            base.keywords.Add("switch");
            base.keywords.Add("this");
            base.keywords.Add("throw");
            base.keywords.Add("true");
            base.keywords.Add("try");
            base.keywords.Add("typeof");
            base.keywords.Add("uint");
            base.keywords.Add("ulong");
            base.keywords.Add("unchecked");
            base.keywords.Add("unsafe");
            base.keywords.Add("ushort");
            base.keywords.Add("using");
            base.keywords.Add("virtual");
            base.keywords.Add("void");
            base.keywords.Add("while");
            base.keywords.Add("print");
            base.keywords.Add("println");
            base.keywords.Add("function");
            this.total_modifier_list = new StringList(false);
            this.total_modifier_list.AddObject("new", Modifier.New);
            this.total_modifier_list.AddObject("public", Modifier.Public);
            this.total_modifier_list.AddObject("protected", Modifier.Protected);
            this.total_modifier_list.AddObject("internal", Modifier.Internal);
            this.total_modifier_list.AddObject("private", Modifier.Private);
            this.total_modifier_list.AddObject("abstract", Modifier.Abstract);
            this.total_modifier_list.AddObject("sealed", Modifier.Sealed);
            this.total_modifier_list.AddObject("static", Modifier.Static);
            this.total_modifier_list.AddObject("readonly", Modifier.ReadOnly);
            this.total_modifier_list.AddObject("volatile", Modifier.Volatile);
            this.total_modifier_list.AddObject("override", Modifier.Override);
            this.total_modifier_list.AddObject("virtual", Modifier.Virtual);
            this.total_modifier_list.AddObject("extern", Modifier.Extern);
            this.class_modifiers = new ModifierList();
            this.class_modifiers.Add(Modifier.New);
            this.class_modifiers.Add(Modifier.Public);
            this.class_modifiers.Add(Modifier.Protected);
            this.class_modifiers.Add(Modifier.Internal);
            this.class_modifiers.Add(Modifier.Private);
            this.class_modifiers.Add(Modifier.Abstract);
            this.class_modifiers.Add(Modifier.Sealed);
            this.constant_modifiers = new ModifierList();
            this.constant_modifiers.Add(Modifier.New);
            this.constant_modifiers.Add(Modifier.Public);
            this.constant_modifiers.Add(Modifier.Protected);
            this.constant_modifiers.Add(Modifier.Internal);
            this.constant_modifiers.Add(Modifier.Private);
            this.field_modifiers = new ModifierList();
            this.field_modifiers.Add(Modifier.New);
            this.field_modifiers.Add(Modifier.Public);
            this.field_modifiers.Add(Modifier.Protected);
            this.field_modifiers.Add(Modifier.Internal);
            this.field_modifiers.Add(Modifier.Private);
            this.field_modifiers.Add(Modifier.Static);
            this.field_modifiers.Add(Modifier.ReadOnly);
            this.field_modifiers.Add(Modifier.Volatile);
            this.method_modifiers = new ModifierList();
            this.method_modifiers.Add(Modifier.New);
            this.method_modifiers.Add(Modifier.Public);
            this.method_modifiers.Add(Modifier.Protected);
            this.method_modifiers.Add(Modifier.Internal);
            this.method_modifiers.Add(Modifier.Private);
            this.method_modifiers.Add(Modifier.Static);
            this.method_modifiers.Add(Modifier.Virtual);
            this.method_modifiers.Add(Modifier.Sealed);
            this.method_modifiers.Add(Modifier.Override);
            this.method_modifiers.Add(Modifier.Abstract);
            this.method_modifiers.Add(Modifier.Extern);
            this.operator_modifiers = new ModifierList();
            this.operator_modifiers.Add(Modifier.Public);
            this.operator_modifiers.Add(Modifier.Static);
            this.operator_modifiers.Add(Modifier.Extern);
            this.constructor_modifiers = new ModifierList();
            this.constructor_modifiers.Add(Modifier.Public);
            this.constructor_modifiers.Add(Modifier.Protected);
            this.constructor_modifiers.Add(Modifier.Internal);
            this.constructor_modifiers.Add(Modifier.Private);
            this.constructor_modifiers.Add(Modifier.Extern);
            this.constructor_modifiers.Add(Modifier.Static);
            this.destructor_modifiers = new ModifierList();
            this.destructor_modifiers.Add(Modifier.Extern);
            this.structure_modifiers = new ModifierList();
            this.structure_modifiers.Add(Modifier.New);
            this.structure_modifiers.Add(Modifier.Public);
            this.structure_modifiers.Add(Modifier.Protected);
            this.structure_modifiers.Add(Modifier.Internal);
            this.structure_modifiers.Add(Modifier.Private);
            this.interface_modifiers = new ModifierList();
            this.interface_modifiers.Add(Modifier.New);
            this.interface_modifiers.Add(Modifier.Public);
            this.interface_modifiers.Add(Modifier.Protected);
            this.interface_modifiers.Add(Modifier.Internal);
            this.interface_modifiers.Add(Modifier.Private);
            this.enum_modifiers = new ModifierList();
            this.enum_modifiers.Add(Modifier.New);
            this.enum_modifiers.Add(Modifier.Public);
            this.enum_modifiers.Add(Modifier.Protected);
            this.enum_modifiers.Add(Modifier.Internal);
            this.enum_modifiers.Add(Modifier.Private);
            this.delegate_modifiers = new ModifierList();
            this.delegate_modifiers.Add(Modifier.New);
            this.delegate_modifiers.Add(Modifier.Public);
            this.delegate_modifiers.Add(Modifier.Protected);
            this.delegate_modifiers.Add(Modifier.Internal);
            this.delegate_modifiers.Add(Modifier.Private);
            this.standard_types = new CSLite_Types();
            this.standard_types.Add("", StandardType.None);
            this.standard_types.Add("void", StandardType.Void);
            this.standard_types.Add("bool", StandardType.Bool);
            this.standard_types.Add("byte", StandardType.Byte);
            this.standard_types.Add("char", StandardType.Char);
            this.standard_types.Add("decimal", StandardType.Decimal);
            this.standard_types.Add("double", StandardType.Double);
            this.standard_types.Add("float", StandardType.Float);
            this.standard_types.Add("int", StandardType.Int);
            this.standard_types.Add("long", StandardType.Long);
            this.standard_types.Add("sbyte", StandardType.Sbyte);
            this.standard_types.Add("short", StandardType.Short);
            this.standard_types.Add("string", StandardType.String);
            this.standard_types.Add("uint", StandardType.Uint);
            this.standard_types.Add("ulong", StandardType.Ulong);
            this.standard_types.Add("ushort", StandardType.Ushort);
            this.standard_types.Add("object", StandardType.Object);
            this.integral_types = new CSLite_Types();
            this.integral_types.Add("sbyte", StandardType.Sbyte);
            this.integral_types.Add("byte", StandardType.Byte);
            this.integral_types.Add("short", StandardType.Short);
            this.integral_types.Add("ushort", StandardType.Ushort);
            this.integral_types.Add("int", StandardType.Int);
            this.integral_types.Add("uint", StandardType.Uint);
            this.integral_types.Add("long", StandardType.Long);
            this.integral_types.Add("ulong", StandardType.Ulong);
            this.integral_types.Add("char", StandardType.Char);
            this.assign_operators = new Hashtable();
            this.relational_operators = new Hashtable();
            this.shift_operators = new Hashtable();
            this.additive_operators = new Hashtable();
            this.multiplicative_operators = new Hashtable();
            this.overloadable_unary_operators = new StringList(true);
            this.overloadable_binary_operators = new StringList(false);
        }

        private void Add_InstanceVariableInitializer(int id, int type_id, ModifierList ml)
        {
            int num = 0;
            if (type_id == 2)
            {
                num = base.NewConst(false);
            }
            else if (type_id == 3)
            {
                num = base.NewConst((byte) 0);
            }
            else if (type_id == 4)
            {
                num = base.NewConst(' ');
            }
            else if (type_id == 5)
            {
                num = base.NewConst(0M);
            }
            else if (type_id == 6)
            {
                num = base.NewConst(0.0);
            }
            else if (type_id == 7)
            {
                num = base.NewConst(0f);
            }
            else if (type_id == 8)
            {
                num = base.NewConst(0);
            }
            else if (type_id == 9)
            {
                num = base.NewConst(0L);
            }
            else if (type_id == 10)
            {
                num = base.NewConst((sbyte) 0);
            }
            else if (type_id == 11)
            {
                num = base.NewConst((short) 0);
            }
            else if (type_id == 12)
            {
                num = base.NewConst("");
            }
            else if (type_id == 13)
            {
                num = base.NewConst(0);
            }
            else if (type_id == 14)
            {
                num = base.NewConst((ulong) 0L);
            }
            else if (type_id == 15)
            {
                num = base.NewConst((ushort) 0);
            }
            if (num > 0)
            {
                int num2 = base.NewVar();
                this.BeginMethod(num2, MemberKind.Method, ml, 1);
                this.InitMethod(num2);
                int num3 = id;
                if (!ml.HasModifier(Modifier.Static))
                {
                    num3 = base.NewVar();
                    base.SetName(num3, base.GetName(id));
                    this.Gen(base.code.OP_EVAL, 0, 0, num3);
                    this.variable_initializers.Add(num2);
                }
                else
                {
                    this.static_variable_initializers.Add(num2);
                }
                this.Gen(base.code.OP_ASSIGN, num3, num, num3);
                this.EndMethod(num2);
            }
        }

        public override void Call_SCANNER()
        {
            base.Call_SCANNER();
            if (base.curr_token.tokenClass == TokenClass.Keyword)
            {
                int typeId = this.standard_types.GetTypeId(base.curr_token.Text);
                if (typeId >= 0)
                {
                    base.curr_token.id = typeId;
                    base.curr_token.tokenClass = TokenClass.Identifier;
                }
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
            if (this.no_gen)
            {
                if (op == base.code.OP_SEPARATOR)
                {
                    base.Gen(op, arg1, arg2, res);
                    base.SetUpcase(false);
                }
            }
            else
            {
                base.Gen(op, arg1, arg2, res);
                base.SetUpcase(false);
            }
        }

        private bool HasAccessModifier(ModifierList ml)
        {
            return ml.HasModifier(Modifier.Public);
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
            this.ACCESSOR_SWITCH = false;
            this.has_constructor = false;
            this.no_gen = false;
            this.valid_this_context = false;
            this.assign_operators.Clear();
            this.assign_operators.Add("=", base.code.OP_ASSIGN);
            this.assign_operators.Add("+=", base.code.OP_PLUS);
            this.assign_operators.Add("-=", base.code.OP_MINUS);
            this.assign_operators.Add("*=", base.code.OP_MULT);
            this.assign_operators.Add("/=", base.code.OP_DIV);
            this.assign_operators.Add("%=", base.code.OP_MOD);
            this.assign_operators.Add("&=", base.code.OP_BITWISE_AND);
            this.assign_operators.Add("|=", base.code.OP_BITWISE_OR);
            this.assign_operators.Add("^=", base.code.OP_BITWISE_XOR);
            this.assign_operators.Add("<<=", base.code.OP_LEFT_SHIFT);
            this.assign_operators.Add(">>=", base.code.OP_RIGHT_SHIFT);
            this.relational_operators.Clear();
            this.relational_operators.Add(">", base.code.OP_GT);
            this.relational_operators.Add("<", base.code.OP_LT);
            this.relational_operators.Add(">=", base.code.OP_GE);
            this.relational_operators.Add("<=", base.code.OP_LE);
            this.relational_operators.Add("is", base.code.OP_IS);
            this.relational_operators.Add("as", base.code.OP_AS);
            this.shift_operators.Clear();
            this.shift_operators.Add("<<", base.code.OP_LEFT_SHIFT);
            this.shift_operators.Add(">>", base.code.OP_RIGHT_SHIFT);
            this.additive_operators.Clear();
            this.additive_operators.Add("+", base.code.OP_PLUS);
            this.additive_operators.Add("-", base.code.OP_MINUS);
            this.multiplicative_operators.Clear();
            this.multiplicative_operators.Add("*", base.code.OP_MULT);
            this.multiplicative_operators.Add("/", base.code.OP_DIV);
            this.multiplicative_operators.Add("%", base.code.OP_MOD);
            this.overloadable_unary_operators.Clear();
            this.overloadable_unary_operators.AddObject("+", base.code.OP_UNARY_PLUS);
            this.overloadable_unary_operators.AddObject("-", base.code.OP_UNARY_MINUS);
            this.overloadable_unary_operators.AddObject("!", base.code.OP_NOT);
            this.overloadable_unary_operators.AddObject("~", base.code.OP_COMPLEMENT);
            this.overloadable_unary_operators.AddObject("++", base.code.OP_INC);
            this.overloadable_unary_operators.AddObject("--", base.code.OP_DEC);
            this.overloadable_unary_operators.AddObject("true", base.code.OP_TRUE);
            this.overloadable_unary_operators.AddObject("false", base.code.OP_TRUE);
            this.overloadable_binary_operators.Clear();
            this.overloadable_binary_operators.AddObject("+", base.code.OP_PLUS);
            this.overloadable_binary_operators.AddObject("-", base.code.OP_MINUS);
            this.overloadable_binary_operators.AddObject("*", base.code.OP_MULT);
            this.overloadable_binary_operators.AddObject("/", base.code.OP_DIV);
            this.overloadable_binary_operators.AddObject("%", base.code.OP_MOD);
            this.overloadable_binary_operators.AddObject("&", base.code.OP_BITWISE_AND);
            this.overloadable_binary_operators.AddObject("|", base.code.OP_BITWISE_OR);
            this.overloadable_binary_operators.AddObject("^", base.code.OP_BITWISE_XOR);
            this.overloadable_binary_operators.AddObject("<<", base.code.OP_LEFT_SHIFT);
            this.overloadable_binary_operators.AddObject(">>", base.code.OP_RIGHT_SHIFT);
            this.overloadable_binary_operators.AddObject("==", base.code.OP_EQ);
            this.overloadable_binary_operators.AddObject("!=", base.code.OP_NE);
            this.overloadable_binary_operators.AddObject(">", base.code.OP_GT);
            this.overloadable_binary_operators.AddObject("<", base.code.OP_LT);
            this.overloadable_binary_operators.AddObject(">=", base.code.OP_GE);
            this.overloadable_binary_operators.AddObject("<=", base.code.OP_LE);
        }

        private bool IsIdentOrStandardType()
        {
            string text = base.curr_token.Text;
            if ((base.curr_token.tokenClass == TokenClass.Identifier) || (base.curr_token.tokenClass == TokenClass.Keyword))
            {
                if (this.IsKeyword(text))
                {
                    return this.IsStandardType(text);
                }
                return true;
            }
            return false;
        }

        public override bool IsKeyword(string s)
        {
            return (base.IsKeyword(s) || (this.ACCESSOR_SWITCH && ((((s == "get") || (s == "set")) || (s == "add")) || (s == "remove"))));
        }

        public bool IsStandardType(string s)
        {
            return (this.standard_types.GetTypeId(s) != -1);
        }

        public override void Match(char c)
        {
            if (c != base.curr_token.Char)
            {
                this.Match(c.ToString());
            }
            this.Call_SCANNER();
        }

        public override void Match(string s)
        {
            if (s != base.curr_token.Text)
            {
                if (s == ";")
                {
                    base.RaiseError(true, "CS1002. ; expected.");
                }
                else if (s == ")")
                {
                    base.RaiseError(true, "CS1026. ) expected.");
                }
                else if (s == "}")
                {
                    base.RaiseError(true, "CS1513. } expected.");
                }
                else if (s == "{")
                {
                    base.RaiseError(true, "CS1514. { expected.");
                }
                else if (s == "in")
                {
                    base.RaiseError(true, "CS1515. 'in' is expected.");
                }
                else
                {
                    base.RaiseErrorEx(true, "CS1003. Syntax error, '{0}' expected.", new object[] { s });
                }
            }
            this.Call_SCANNER();
        }

        private int Parse_AdditiveExpr(int result)
        {
            result = this.Parse_MultiplicativeExpr(result);
            for (object obj2 = this.additive_operators[base.curr_token.Text]; obj2 != null; obj2 = this.additive_operators[base.curr_token.Text])
            {
                int op = (int) obj2;
                this.Call_SCANNER();
                result = base.BinOp(op, result, this.Parse_MultiplicativeExpr(0));
            }
            return result;
        }

        private int Parse_ANDExpr(int result)
        {
            result = this.Parse_EqualityExpr(result);
            while (base.IsCurrText("&"))
            {
                this.Call_SCANNER();
                result = base.BinOp(base.code.OP_BITWISE_AND, result, this.Parse_EqualityExpr(0));
            }
            return result;
        }

        private int Parse_ArgumentList(string CloseBracket, int sub_id, int object_id)
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
                if (base.IsCurrText("ref"))
                {
                    this.Match("ref");
                    none = ParamMod.RetVal;
                }
                else if (base.IsCurrText("out"))
                {
                    this.Match("out");
                    none = ParamMod.Out;
                }
                int num2 = this.Parse_Expression();
                switch (none)
                {
                    case ParamMod.RetVal:
                    case ParamMod.Out:
                        this.Gen(base.code.OP_SET_REF_TYPE, num2, 0, 0);
                        this.Gen(base.code.OP_PUSH, num2, (int) none, sub_id);
                        break;

                    default:
                        this.Gen(base.code.OP_PUSH, num2, (int) none, sub_id);
                        break;
                }
            }
            while (base.CondMatch(','));
            this.Gen(base.code.OP_PUSH, object_id, 0, 0);
            return num;
        }

        private int Parse_ArrayInitializer(int array_type_id)
        {
            return this.Parse_MultiArrayInitializer(array_type_id);
        }

        private void Parse_Assignment()
        {
            int num = this.Parse_UnaryExpr(0);
            if (!base.IsCurrText(';'))
            {
                object obj2 = this.assign_operators[base.curr_token.Text];
                if (obj2 != null)
                {
                    int op = (int) obj2;
                    this.Call_SCANNER();
                    if (op == base.code.OP_ASSIGN)
                    {
                        this.Gen(op, num, this.Parse_Expression(), num);
                    }
                    else
                    {
                        int res = base.NewVar();
                        this.Gen(op, num, this.Parse_Expression(), res);
                        this.Gen(base.code.OP_ASSIGN, num, res, num);
                    }
                }
                else
                {
                    this.Match('=');
                }
            }
        }

        private void Parse_Attribute()
        {
            this.Parse_AttributeName();
            if (base.IsCurrText('('))
            {
                this.Parse_AttributeArguments();
            }
        }

        private void Parse_AttributeArguments()
        {
            this.Match('(');
            do
            {
                int k = base.ReadToken();
                string text = base.curr_token.Text;
                base.Backup_SCANNER(k);
                if (text == "=")
                {
                    break;
                }
                this.Parse_Expression();
            }
            while (base.CondMatch(','));
            if (!base.IsCurrText(')'))
            {
                do
                {
                    this.Parse_Ident();
                    this.Match('=');
                    this.Parse_Expression();
                }
                while (base.CondMatch(','));
            }
            this.Match(')');
        }

        private void Parse_AttributeList()
        {
            do
            {
                this.Parse_Attribute();
            }
            while (base.CondMatch(','));
        }

        private void Parse_AttributeName()
        {
            this.Parse_Type();
        }

        private void Parse_Attributes()
        {
            this.no_gen = true;
            do
            {
                this.Match('[');
                if (base.IsCurrText("field"))
                {
                    this.Match("field");
                    this.Match(':');
                }
                else if (base.IsCurrText("event"))
                {
                    this.Match("event");
                    this.Match(':');
                }
                else if (base.IsCurrText("method"))
                {
                    this.Match("method");
                    this.Match(':');
                }
                else if (base.IsCurrText("param"))
                {
                    this.Match("param");
                    this.Match(':');
                }
                else if (base.IsCurrText("property"))
                {
                    this.Match("property");
                    this.Match(':');
                }
                else if (base.IsCurrText("return"))
                {
                    this.Match("return");
                    this.Match(':');
                }
                else if (base.IsCurrText("type"))
                {
                    this.Match("type");
                    this.Match(':');
                }
                this.Parse_AttributeList();
                if (base.IsCurrText(','))
                {
                    this.Match(',');
                }
                this.Match(']');
            }
            while (base.IsCurrText('['));
            this.no_gen = false;
        }

        private void Parse_Block()
        {
            base.BeginBlock();
            base.DECLARE_SWITCH = false;
            this.Match('{');
            if (!base.IsCurrText('}'))
            {
                this.Parse_StatementList();
            }
            base.EndBlock();
            this.Match('}');
        }

        private void Parse_BreakStmt()
        {
            if (base.BreakStack.Count == 0)
            {
                base.RaiseError(false, "CS0139. No enclosing loop out of which to break or continue.");
            }
            this.Gen(base.code.OP_GOTO_START, base.BreakStack.TopLabel(), 0, 0);
            this.Match("break");
            this.Match(';');
        }

        private void Parse_CheckedStmt()
        {
            this.Match("checked");
            this.Gen(base.code.OP_CHECKED, base.TRUE_id, 0, 0);
            this.Parse_Block();
            this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
        }

        private void Parse_ClassBase(int class_id)
        {
            this.Match(':');
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
            if (!base.IsCurrText('{'))
            {
                base.RaiseError(false, "CS1521. Invalid base type.");
            }
        }

        private void Parse_ClassBody(int class_id, ModifierList owner_modifiers)
        {
            this.variable_initializers.Clear();
            this.static_variable_initializers.Clear();
            this.Match('{');
            while (true)
            {
                if (base.IsCurrText('}'))
                {
                    break;
                }
                this.Parse_ClassMemberDeclaration(class_id, owner_modifiers);
            }
            this.Match('}');
        }

        private void Parse_ClassDeclaration(ModifierList ml)
        {
            base.DECLARE_SWITCH = true;
            this.has_constructor = false;
            base.CheckModifiers(ml, this.class_modifiers);
            this.Match("class");
            int num = this.Parse_Ident();
            if (ml.HasModifier(Modifier.Abstract) && ml.HasModifier(Modifier.Sealed))
            {
                base.RaiseError(false, "CS0502. The class '{0}' is abstract and sealed.");
            }
            base.BeginClass(num, ml);
            if (base.IsCurrText(':'))
            {
                this.Parse_ClassBase(num);
            }
            else
            {
                this.Gen(base.code.OP_ADD_ANCESTOR, num, base.ObjectClassId, 0);
            }
            this.Parse_ClassBody(num, ml);
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(num);
            }
            if (!this.has_constructor)
            {
                this.CreateDefaultConstructor(num, false);
            }
            base.EndClass(num);
            if (base.IsCurrText(';'))
            {
                this.Match(';');
            }
        }

        private void Parse_ClassMemberDeclaration(int class_id, ModifierList owner_modifiers)
        {
            this.Parse_MemberDeclaration(class_id, ClassKind.Class, owner_modifiers);
        }

        private void Parse_CompilationUnit()
        {
            if (base.IsCurrText("using"))
            {
                this.Parse_UsingDirectives();
            }
            if (base.IsCurrText('['))
            {
                this.Parse_GlobalAttributes();
            }
            if (!base.IsEOF())
            {
                this.Gen(base.code.OP_BEGIN_USING, base.RootNamespaceId, 0, 0);
                this.Parse_NamespaceMemberDeclarations();
                this.Gen(base.code.OP_END_USING, base.RootNamespaceId, 0, 0);
            }
        }

        private int Parse_ConditionalANDExpr(int result)
        {
            result = this.Parse_InclusiveORExpr(result);
            while (base.IsCurrText("&&"))
            {
                int num = result;
                int num2 = base.NewLabel();
                result = base.NewVar();
                this.Gen(base.code.OP_ASSIGN, result, num, result);
                this.Gen(base.code.OP_GO_FALSE, num2, result, 0);
                this.Call_SCANNER();
                this.Gen(base.code.OP_ASSIGN, result, this.Parse_InclusiveORExpr(0), result);
                base.SetLabelHere(num2);
            }
            return result;
        }

        private int Parse_ConditionalORExpr(int result)
        {
            result = this.Parse_ConditionalANDExpr(result);
            while (base.IsCurrText("||"))
            {
                int num = result;
                int num2 = base.NewLabel();
                result = base.NewVar();
                this.Gen(base.code.OP_ASSIGN, result, num, result);
                this.Gen(base.code.OP_GO_TRUE, num2, result, 0);
                this.Call_SCANNER();
                this.Gen(base.code.OP_ASSIGN, result, this.Parse_InclusiveORExpr(0), result);
                base.SetLabelHere(num2);
            }
            return result;
        }

        private int Parse_ConstantExpression()
        {
            return this.Parse_Expression();
        }

        private void Parse_ContinueStmt()
        {
            if (base.ContinueStack.Count == 0)
            {
                base.RaiseError(false, "CS0139. No enclosing loop out of which to break or continue.");
            }
            this.Gen(base.code.OP_GOTO_START, base.ContinueStack.TopLabel(), 0, 0);
            this.Match("continue");
            this.Match(';');
        }

        private void Parse_DeclarationStmt()
        {
            if (base.IsCurrText("const"))
            {
                this.Parse_LocalConstantDeclaration();
            }
            else
            {
                this.Parse_LocalVariableDeclaration();
            }
            this.Match(';');
        }

        private void Parse_DelegateDeclaration(ModifierList ml)
        {
            base.CheckModifiers(ml, this.delegate_modifiers);
            base.DECLARE_SWITCH = true;
            this.Match("delegate");
            int num = this.Parse_Type();
            int num2 = this.Parse_Ident();
            base.BeginDelegate(num2, ml);
            this.Match('(');
            int num3 = base.NewVar();
            this.BeginMethod(num3, MemberKind.Method, ml, num);
            this.Gen(base.code.OP_ADD_PATTERN, num2, num3, 0);
            if (!base.IsCurrText(')'))
            {
                this.Parse_FormalParameterList(num3, false);
            }
            this.Match(')');
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
            base.DECLARE_SWITCH = false;
            this.Match(';');
        }

        private void Parse_DoStmt()
        {
            int l = base.NewLabel();
            int label = base.NewLabel();
            base.SetLabelHere(l);
            this.Match("do");
            base.BreakStack.Push(label);
            base.ContinueStack.Push(l);
            this.Parse_EmbeddedStmt();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Match("while");
            this.Match('(');
            this.Gen(base.code.OP_GO_TRUE, l, this.Parse_Expression(), 0);
            base.SetLabelHere(label);
            this.Match(')');
            this.Match(';');
        }

        private void Parse_EmbeddedStmt()
        {
            if (base.IsCurrText('{'))
            {
                this.Parse_Block();
            }
            else if (base.IsCurrText("if"))
            {
                this.Parse_IfStmt();
            }
            else if (base.IsCurrText("switch"))
            {
                this.Parse_SwitchStmt();
            }
            else if (base.IsCurrText("do"))
            {
                this.Parse_DoStmt();
            }
            else if (base.IsCurrText("while"))
            {
                this.Parse_WhileStmt();
            }
            else if (base.IsCurrText("for"))
            {
                this.Parse_ForStmt();
            }
            else if (base.IsCurrText("foreach"))
            {
                this.Parse_ForeachStmt();
            }
            else if (base.IsCurrText("goto"))
            {
                this.Parse_GotoStmt();
            }
            else if (base.IsCurrText("break"))
            {
                this.Parse_BreakStmt();
            }
            else if (base.IsCurrText("continue"))
            {
                this.Parse_ContinueStmt();
            }
            else if (base.IsCurrText("return"))
            {
                this.Parse_ReturnStmt();
            }
            else if (base.IsCurrText("throw"))
            {
                this.Parse_ThrowStmt();
            }
            else if (base.IsCurrText("try"))
            {
                this.Parse_TryStmt();
            }
            else if (base.IsCurrText("checked"))
            {
                this.Parse_CheckedStmt();
            }
            else if (base.IsCurrText("lock"))
            {
                this.Parse_LockStmt();
            }
            else if (base.IsCurrText("using"))
            {
                this.Parse_UsingStmt();
            }
            else if (base.IsCurrText("unchecked"))
            {
                this.Parse_UncheckedStmt();
            }
            else if (base.IsCurrText("print"))
            {
                base.Parse_PrintStmt();
            }
            else if (base.IsCurrText("println"))
            {
                base.Parse_PrintlnStmt();
            }
            else if (base.IsCurrText(';'))
            {
                this.Parse_EmptyStmt();
            }
            else
            {
                this.Parse_ExpressionStmt();
            }
        }

        private void Parse_EmptyStmt()
        {
            this.Match(';');
        }

        private void Parse_EnumDeclaration(ModifierList ml)
        {
            base.CheckModifiers(ml, this.enum_modifiers);
            ml.Add(Modifier.Static);
            base.DECLARE_SWITCH = true;
            this.Match("enum");
            int num = this.Parse_Ident();
            int num2 = 8;
            if (base.IsCurrText(':'))
            {
                this.Match(':');
                num2 = this.Parse_IntegralType();
                if (num2 == 4)
                {
                    base.RaiseError(true, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
                }
            }
            base.BeginEnum(num, ml, num2);
            this.Gen(base.code.OP_ADD_UNDERLYING_TYPE, num, num2, 0);
            this.Match('{');
            if (!base.IsCurrText('}'))
            {
                int v = -1;
                this.static_variable_initializers.Clear();
                do
                {
                    int num6;
                    if (base.IsCurrText('}'))
                    {
                        break;
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
                }
                while (base.CondMatch(','));
                this.CreateDefaultStaticConstructor(num);
            }
            base.DECLARE_SWITCH = false;
            base.EndEnum(num);
            this.Match('}');
            if (base.IsCurrText(';'))
            {
                this.Match(';');
            }
        }

        private int Parse_EqualityExpr(int result)
        {
            result = this.Parse_RelationalExpr(result);
            while (base.IsCurrText("==") || base.IsCurrText("!="))
            {
                int num;
                if (base.IsCurrText("=="))
                {
                    num = base.code.OP_EQ;
                }
                else
                {
                    num = base.code.OP_NE;
                }
                this.Call_SCANNER();
                result = base.BinOp(num, result, this.Parse_RelationalExpr(0));
            }
            return result;
        }

        private void Parse_EventAccessorDeclarations(int id, int type_id, ModifierList ml)
        {
            this.ACCESSOR_SWITCH = true;
            this.Match('{');
            int num = 0;
            int num2 = 0;
        Label_0018:
            if (base.IsCurrText('['))
            {
                this.Parse_Attributes();
            }
            if (base.IsCurrText("add"))
            {
                int num3 = base.NewVar();
                base.SetName(num3, "add_" + base.GetName(id));
                this.BeginMethod(num3, MemberKind.Method, ml, type_id);
                num++;
                if (num > 1)
                {
                    base.RaiseError(true, "CS1007. Property accessor already defined.");
                }
                this.Match("add");
                int num4 = base.NewVar();
                base.SetName(num4, "value");
                this.Gen(base.code.OP_ADD_PARAM, num3, num4, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, num4, type_id, 0);
                if (base.IsCurrText(';'))
                {
                    this.Match(';');
                }
                else
                {
                    if (ml.HasModifier(Modifier.Abstract))
                    {
                        string name = base.GetName(num3);
                        base.RaiseErrorEx(true, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { name });
                    }
                    this.InitMethod(num3);
                    this.Parse_MethodBlock();
                }
                this.EndMethod(num3);
                this.Gen(base.code.OP_ADD_ADD_ACCESSOR, id, num3, 0);
                goto Label_0018;
            }
            if (base.IsCurrText("remove"))
            {
                int num5 = base.NewVar();
                base.SetName(num5, "remove_" + base.GetName(id));
                this.BeginMethod(num5, MemberKind.Method, ml, 1);
                num2++;
                if (num2 > 1)
                {
                    base.RaiseError(true, "CS1007. Property accessor already defined.");
                }
                this.Match("remove");
                int num6 = base.NewVar();
                base.SetName(num6, "value");
                this.Gen(base.code.OP_ADD_PARAM, num5, num6, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, num6, type_id, 0);
                if (base.IsCurrText(';'))
                {
                    this.Match(';');
                }
                else
                {
                    if (ml.HasModifier(Modifier.Abstract))
                    {
                        string str2 = base.GetName(num5);
                        base.RaiseErrorEx(true, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { str2 });
                    }
                    this.InitMethod(num5);
                    this.Parse_MethodBlock();
                }
                this.EndMethod(num5);
                this.Gen(base.code.OP_ADD_REMOVE_ACCESSOR, id, num5, 0);
                goto Label_0018;
            }
            if ((num == 0) && (num2 == 0))
            {
                base.RaiseError(true, "CS1055. An add or remove accessor expected.");
            }
            if ((num == 0) || (num2 == 0))
            {
                base.RaiseErrorEx(true, "CS0065. '{0}' : event property must have both add and remove accessors.", new object[] { base.GetName(id) });
            }
            this.Match('}');
            this.ACCESSOR_SWITCH = true;
        }

        private int Parse_ExclusiveORExpr(int result)
        {
            result = this.Parse_ANDExpr(result);
            while (base.IsCurrText("^"))
            {
                this.Call_SCANNER();
                result = base.BinOp(base.code.OP_BITWISE_XOR, result, this.Parse_ANDExpr(0));
            }
            return result;
        }

        public override int Parse_Expression()
        {
            int num = this.Parse_UnaryExpr(0);
            object obj2 = this.assign_operators[base.curr_token.Text];
            if (obj2 != null)
            {
                int op = (int) obj2;
                this.Call_SCANNER();
                if (op == base.code.OP_ASSIGN)
                {
                    this.Gen(op, num, this.Parse_Expression(), num);
                    return num;
                }
                int res = base.NewVar();
                this.Gen(op, num, this.Parse_Expression(), res);
                this.Gen(base.code.OP_ASSIGN, num, res, num);
                return num;
            }
            if (base.IsCurrText("?"))
            {
                this.Match("?");
                int num4 = base.NewLabel();
                int num5 = base.NewLabel();
                this.Gen(base.code.OP_GO_FALSE, num5, num, 0);
                num = base.NewVar();
                int num6 = this.Parse_Expression();
                this.Gen(base.code.OP_ASSIGN, num, num6, num);
                this.Match(':');
                this.Gen(base.code.OP_GO, num4, 0, 0);
                base.SetLabelHere(num5);
                int num7 = this.Parse_Expression();
                this.Gen(base.code.OP_ASSIGN_COND_TYPE, num6, num7, num);
                this.Gen(base.code.OP_ASSIGN, num, num7, num);
                base.SetLabelHere(num4);
                return num;
            }
            return this.Parse_ConditionalORExpr(num);
        }

        private void Parse_ExpressionStmt()
        {
            this.Parse_StatementExpression();
            this.Match(';');
        }

        private void Parse_ForeachStmt()
        {
            this.Match("foreach");
            base.BeginBlock();
            this.Match('(');
            base.DECLARE_SWITCH = true;
            base.DECLARATION_CHECK_SWITCH = true;
            int num = this.Parse_Type();
            if (base.curr_token.tokenClass != TokenClass.Identifier)
            {
                base.RaiseError(true, "CS0230. Type and identifier are both required in a foreach statement.");
            }
            int num2 = this.Parse_Ident();
            base.DECLARE_SWITCH = false;
            base.DECLARATION_CHECK_SWITCH = false;
            this.Gen(base.code.OP_ASSIGN_TYPE, num2, num, 0);
            this.Match("in");
            int num3 = this.Parse_Expression();
            this.Gen(base.code.OP_DECLARE_LOCAL_SIMPLE, num2, num3, 0);
            this.Match(')');
            int res = base.NewVar();
            int num5 = base.NewRef("GetEnumerator");
            this.Gen(base.code.OP_CREATE_REFERENCE, num3, 0, num5);
            this.Gen(base.code.OP_BEGIN_CALL, num5, 0, 0);
            this.Gen(base.code.OP_PUSH, num3, 0, 0);
            this.Gen(base.code.OP_CALL, num5, 0, res);
            int num6 = base.NewLabel();
            int l = base.NewLabel();
            base.SetLabelHere(l);
            int num8 = base.NewVar();
            int num9 = base.NewRef("MoveNext");
            this.Gen(base.code.OP_CREATE_REFERENCE, res, 0, num9);
            this.Gen(base.code.OP_BEGIN_CALL, num9, 0, 0);
            this.Gen(base.code.OP_PUSH, res, 0, 0);
            this.Gen(base.code.OP_CALL, num9, 0, num8);
            this.Gen(base.code.OP_GO_FALSE, num6, num8, 0);
            base.BreakStack.Push(num6);
            base.ContinueStack.Push(l);
            int num10 = base.NewRef("get_Current");
            this.Gen(base.code.OP_CREATE_REFERENCE, res, 0, num10);
            this.Gen(base.code.OP_BEGIN_CALL, num10, 0, 0);
            this.Gen(base.code.OP_PUSH, res, 0, 0);
            this.Gen(base.code.OP_CALL, num10, 0, num2);
            this.Parse_EmbeddedStmt();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num6);
            base.EndBlock();
        }

        private int Parse_FormalParameterList(int sub_id, bool isIndexer)
        {
            this.param_ids.Clear();
            this.param_type_ids.Clear();
            this.param_mods.Clear();
            bool flag = false;
            int num = 0;
            while (true)
            {
                int num2;
                if (flag)
                {
                    base.RaiseError(false, "CS0231. A params parameter must be the last parameter in a formal parameter list.");
                }
                num++;
                if (base.IsCurrText('['))
                {
                    this.Parse_Attributes();
                }
                ParamMod none = ParamMod.None;
                if (base.IsCurrText("ref"))
                {
                    if (isIndexer)
                    {
                        base.RaiseError(false, "CS0631. Indexers can't have ref or out parameters.");
                    }
                    this.Match("ref");
                    none = ParamMod.RetVal;
                    num2 = base.GenTypeRef(this.Parse_Type());
                }
                else if (base.IsCurrText("out"))
                {
                    if (isIndexer)
                    {
                        base.RaiseError(false, "CS0631. Indexers can't have ref or out parameters.");
                    }
                    this.Match("out");
                    none = ParamMod.Out;
                    num2 = base.GenTypeRef(this.Parse_Type());
                }
                else if (base.IsCurrText("params"))
                {
                    this.Match("params");
                    if (base.IsCurrText("ref") || base.IsCurrText("out"))
                    {
                        base.RaiseError(false, "CS1611. The params parameter cannot be declared as ref or out.");
                    }
                    num2 = this.Parse_Type();
                    if (CSLite_System.GetRank(base.GetName(num2)) != 1)
                    {
                        base.RaiseError(false, "CS0225. The params parameter must be a single dimensional array.");
                    }
                    flag = true;
                }
                else
                {
                    num2 = this.Parse_Type();
                }
                int num3 = this.Parse_Ident();
                if (base.IsCurrText('='))
                {
                    base.RaiseError(true, "CS0241. Default parameter specifiers are not permitted.");
                }
                else if (base.IsCurrText('['))
                {
                    base.RaiseError(true, "CS1552. Array type specifier, [], must appear before parameter name.");
                }
                if (!isIndexer)
                {
                    this.Gen(base.code.OP_ASSIGN_TYPE, num3, num2, 0);
                    if (flag)
                    {
                        this.Gen(base.code.OP_ADD_PARAMS, sub_id, num3, 0);
                    }
                    else
                    {
                        this.Gen(base.code.OP_ADD_PARAM, sub_id, num3, (int) none);
                    }
                }
                foreach (int num4 in this.param_ids)
                {
                    if (base.GetName(num4) == base.GetName(num3))
                    {
                        base.RaiseErrorEx(false, "CS0100. The parameter name '{0}' is a duplicate.", new object[] { base.GetName(num3) });
                    }
                }
                this.param_ids.Add(num3);
                this.param_type_ids.Add(num2);
                this.param_mods.Add((int) none);
                if (!base.CondMatch(','))
                {
                    return num;
                }
            }
        }

        private void Parse_ForStmt()
        {
            this.Match("for");
            base.BeginBlock();
            int num = base.NewLabel();
            int l = base.NewLabel();
            int num3 = base.NewLabel();
            int num4 = base.NewLabel();
            this.Match('(');
            if (!base.IsCurrText(';'))
            {
                bool flag = this.IsIdentOrStandardType();
                int k = base.ReadToken();
                while (true)
                {
                    if (!base.IsCurrText('.'))
                    {
                        break;
                    }
                    k += base.ReadToken();
                    k += base.ReadToken();
                }
                bool flag2 = this.IsIdentOrStandardType() || base.IsCurrText('[');
                if (base.IsCurrText('['))
                {
                    k += base.ReadToken();
                    if (!base.IsCurrText(']') && !base.IsCurrText(','))
                    {
                        flag2 = false;
                    }
                }
                base.Backup_SCANNER(k);
                if (flag && flag2)
                {
                    this.Parse_LocalVariableDeclaration();
                }
                else
                {
                    do
                    {
                        this.Parse_Expression();
                    }
                    while (base.CondMatch(','));
                }
            }
            this.Match(';');
            base.SetLabelHere(num3);
            if (!base.IsCurrText(';'))
            {
                this.Gen(base.code.OP_GO_FALSE, num, this.Parse_Expression(), 0);
            }
            this.Gen(base.code.OP_GO, num4, 0, 0);
            this.Match(';');
            base.SetLabelHere(l);
            if (!base.IsCurrText(')'))
            {
                do
                {
                    this.Parse_Expression();
                }
                while (base.CondMatch(','));
            }
            this.Gen(base.code.OP_GO, num3, 0, 0);
            this.Match(')');
            base.SetLabelHere(num4);
            base.BreakStack.Push(num);
            base.ContinueStack.Push(l);
            this.Parse_EmbeddedStmt();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num);
            base.EndBlock();
        }

        private int Parse_FreeArrayInitializer(int array_type_id, ref IntegerList bounds)
        {
            int num3;
            string name = base.GetName(array_type_id);
            int rank = CSLite_System.GetRank(name);
            string elementTypeName = CSLite_System.GetElementTypeName(name);
            IntegerList list = new IntegerList(true);
            int id = base.NewVar();
            base.SetName(id, elementTypeName);
            this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
            int res = base.NewVar();
            this.Gen(base.code.OP_CREATE_OBJECT, array_type_id, 0, res);
            this.Gen(base.code.OP_BEGIN_CALL, array_type_id, 0, 0);
            IntegerList list2 = new IntegerList(true);
            for (int i = 0; i < rank; i++)
            {
                num3 = base.NewVar(0);
                list2.Add(num3);
                this.Gen(base.code.OP_PUSH, num3, 0, array_type_id);
            }
            this.Gen(base.code.OP_PUSH, res, 0, 0);
            this.Gen(base.code.OP_CALL, array_type_id, rank, 0);
            int num6 = 0;
            this.Match('{');
            if (!base.IsCurrText('}'))
            {
                num6 = 0;
                do
                {
                    num6++;
                    int num7 = base.NewVar();
                    int num8 = base.NewConst(num6 - 1);
                    this.Gen(base.code.OP_CREATE_INDEX_OBJECT, res, 0, num7);
                    this.Gen(base.code.OP_ADD_INDEX, num7, num8, res);
                    this.Gen(base.code.OP_SETUP_INDEX_OBJECT, num7, 0, 0);
                    num3 = base.NewVar();
                    if (base.IsCurrText('{'))
                    {
                        num3 = this.Parse_FreeArrayInitializer(id, ref list);
                    }
                    else
                    {
                        num3 = this.Parse_Expression();
                    }
                    this.Gen(base.code.OP_ASSIGN, num7, num3, num7);
                }
                while (base.CondMatch(','));
            }
            this.Match('}');
            num3 = list2[0];
            base.PutVal(num3, num6);
            bounds.Add(num6);
            for (int j = 1; j < list2.Count; j++)
            {
                num3 = list2[j];
                num6 = list[j - 1];
                base.PutVal(num3, num6);
                bounds.Add(num6);
            }
            return res;
        }

        private void Parse_GlobalAttributes()
        {
            this.no_gen = true;
            do
            {
                this.Match('[');
                this.Parse_GlobalAttributeTargetSpecifier();
                this.Parse_AttributeList();
                if (base.IsCurrText(','))
                {
                    this.Match(',');
                }
                this.Match(']');
            }
            while (base.IsCurrText('['));
            this.no_gen = false;
        }

        private void Parse_GlobalAttributeTargetSpecifier()
        {
            this.Match("assembly");
            this.Match(':');
        }

        private void Parse_GotoStmt()
        {
            this.Match("goto");
            int num = this.Parse_Ident();
            this.Gen(base.code.OP_GOTO_START, num, 0, 0);
            if (base.IsCurrText("case"))
            {
                base.RaiseError(true, "CS0153. A goto case is only valid inside a switch statement.");
            }
            this.Match(';');
        }

        private void Parse_IfStmt()
        {
            int num = base.NewLabel();
            this.Match("if");
            this.Match('(');
            this.Gen(base.code.OP_GO_FALSE, num, this.Parse_Expression(), 0);
            this.Match(')');
            this.Parse_EmbeddedStmt();
            if (base.IsCurrText("else"))
            {
                int num2 = base.NewLabel();
                this.Gen(base.code.OP_GO, num2, 0, 0);
                base.SetLabelHere(num);
                this.Match("else");
                this.Parse_EmbeddedStmt();
                base.SetLabelHere(num2);
            }
            else
            {
                base.SetLabelHere(num);
            }
        }

        private int Parse_InclusiveORExpr(int result)
        {
            result = this.Parse_ExclusiveORExpr(result);
            while (base.IsCurrText("|"))
            {
                this.Call_SCANNER();
                result = base.BinOp(base.code.OP_BITWISE_OR, result, this.Parse_ExclusiveORExpr(0));
            }
            return result;
        }

        private void Parse_InstanceVariableInitializer(int id, int type_id, ModifierList ml)
        {
            base.DECLARE_SWITCH = false;
            this.Match('=');
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

        private int Parse_IntegralType()
        {
            int typeId = this.integral_types.GetTypeId(base.curr_token.Text);
            if (typeId == -1)
            {
                base.RaiseError(false, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
            }
            this.Parse_Ident();
            return typeId;
        }

        private void Parse_InterfaceDeclaration(ModifierList ml)
        {
            base.CheckModifiers(ml, this.interface_modifiers);
            base.DECLARE_SWITCH = true;
            this.Match("interface");
            int num = this.Parse_Ident();
            base.BeginInterface(num, ml);
            if (base.IsCurrText(':'))
            {
                this.Parse_ClassBase(num);
            }
            this.Match('{');
            while (!base.IsCurrText('}'))
            {
                if (base.IsCurrText('['))
                {
                    this.Parse_Attributes();
                }
                ModifierList list = new ModifierList();
                if (base.IsCurrText("new"))
                {
                    this.Match("new");
                    list.Add(Modifier.New);
                }
                list.Add(Modifier.Abstract);
                if (base.IsCurrText("event"))
                {
                    this.Match("event");
                    int num2 = this.Parse_Type();
                    int num3 = this.Parse_Ident();
                    base.BeginEvent(num3, list, num2, 0);
                    base.EndEvent(num3);
                    if (base.IsCurrText('='))
                    {
                        base.RaiseErrorEx(true, "CS0068. '{0}': event in interface cannot have initializer.", new object[] { base.GetName(num3) });
                    }
                    if (base.IsCurrText('{'))
                    {
                        base.RaiseErrorEx(true, "CS0069. '{0}': event in interface cannot have add or remove accessors.", new object[] { base.GetName(num3) });
                    }
                    this.Match(';');
                }
                else if ((base.IsCurrText("class") || base.IsCurrText("struct")) || (base.IsCurrText("enum") || base.IsCurrText("delegate")))
                {
                    base.RaiseErrorEx(true, "CS0524. '{0}' : interfaces cannot declare types.", new object[] { base.GetName(num) });
                }
                else
                {
                    int id = this.Parse_Type();
                    if ((base.GetName(id) == base.GetName(num)) && base.IsCurrText('('))
                    {
                        base.RaiseError(true, "CS0526. Interfaces cannot contain constructors.");
                    }
                    if (base.IsCurrText('('))
                    {
                        base.RaiseError(true, "CS1520. Class, struct, or interface method must have a return type.");
                    }
                    else if (base.IsCurrText("this"))
                    {
                        if (id == 1)
                        {
                            base.RaiseError(false, "CS0620. Indexers can't have void type.");
                        }
                        base.CheckModifiers(list, this.method_modifiers);
                        this.Match("this");
                        int num5 = base.NewVar();
                        base.SetName(num5, "Item");
                        this.Match('[');
                        int num6 = this.Parse_FormalParameterList(num5, true);
                        this.Match(']');
                        this.valid_this_context = true;
                        base.BeginProperty(num5, list, id, num6);
                        this.Gen(base.code.OP_ADD_MODIFIER, num5, 1, 0);
                        this.Parse_PropertyAccessorDeclarations(num5, id, list);
                        base.EndProperty(num5);
                        this.valid_this_context = false;
                    }
                    else if (base.IsCurrText("operator"))
                    {
                        base.RaiseError(true, "CS0567. Interfaces cannot contain operators.");
                    }
                    else
                    {
                        int num7 = this.Parse_Ident();
                        if (base.IsCurrText('('))
                        {
                            int num8 = num7;
                            this.BeginMethod(num8, MemberKind.Method, list, id);
                            this.Gen(base.code.OP_ADD_MODIFIER, num8, 1, 0);
                            this.Match('(');
                            if (!base.IsCurrText(')'))
                            {
                                this.Parse_FormalParameterList(num8, false);
                            }
                            this.Match(')');
                            this.EndMethod(num8);
                            if (base.IsCurrText('{'))
                            {
                                base.RaiseErrorEx(true, "CS0531. '{0}' : interface members cannot have a definition.", new object[] { base.GetName(num8) });
                            }
                            this.Match(';');
                        }
                        else if (base.IsCurrText('{'))
                        {
                            this.valid_this_context = true;
                            base.BeginProperty(num7, list, id, 0);
                            this.Gen(base.code.OP_ADD_MODIFIER, num7, 1, 0);
                            this.param_ids.Clear();
                            this.param_type_ids.Clear();
                            this.param_mods.Clear();
                            this.Parse_PropertyAccessorDeclarations(num7, id, list);
                            base.EndProperty(num7);
                            this.valid_this_context = false;
                        }
                        else if (base.IsCurrText('=') || base.IsCurrText(';'))
                        {
                            base.RaiseError(true, "CS0525. Interfaces cannot contain fields.");
                        }
                        else
                        {
                            this.Match('(');
                        }
                    }
                }
            }
            base.DECLARE_SWITCH = false;
            base.EndInterface(num);
            this.Match('}');
            if (base.IsCurrText(';'))
            {
                this.Match(';');
            }
        }

        private void Parse_LocalConstantDeclaration()
        {
            this.Match("const");
            base.DECLARE_SWITCH = true;
            base.DECLARATION_CHECK_SWITCH = true;
            int num = this.Parse_Type();
            do
            {
                int num2 = this.Parse_Ident();
                if (!base.IsCurrText('='))
                {
                    base.RaiseError(true, "CS0145. A const field requires a value to be provided.");
                }
                this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, num2, base.CurrSubId, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, num2, num, 0);
                base.DECLARE_SWITCH = false;
                this.Match('=');
                if (base.IsCurrText('{'))
                {
                    this.Gen(base.code.OP_ASSIGN, num2, this.Parse_ArrayInitializer(num), num2);
                }
                else
                {
                    this.Gen(base.code.OP_ASSIGN, num2, this.Parse_Expression(), num2);
                }
                base.DECLARE_SWITCH = true;
            }
            while (base.CondMatch(','));
            base.DECLARE_SWITCH = false;
            base.DECLARATION_CHECK_SWITCH = false;
        }

        private void Parse_LocalVariableDeclaration()
        {
            this.local_variables.Clear();
            base.DECLARE_SWITCH = true;
            base.DECLARATION_CHECK_SWITCH = true;
            int num = this.Parse_Type();
            do
            {
                int num2 = this.Parse_Ident();
                this.Gen(base.code.OP_DECLARE_LOCAL_VARIABLE, num2, base.CurrSubId, 0);
                this.local_variables.Add(num2);
                this.Gen(base.code.OP_ASSIGN_TYPE, num2, num, 0);
                if (base.IsCurrText('='))
                {
                    base.DECLARE_SWITCH = false;
                    this.Match('=');
                    if (base.IsCurrText('{'))
                    {
                        this.Gen(base.code.OP_ASSIGN, num2, this.Parse_ArrayInitializer(num), num2);
                    }
                    else
                    {
                        this.Gen(base.code.OP_ASSIGN, num2, this.Parse_Expression(), num2);
                    }
                    base.DECLARE_SWITCH = true;
                }
                else
                {
                    this.Gen(base.code.OP_CHECK_STRUCT_CONSTRUCTOR, num, 0, num2);
                }
            }
            while (base.CondMatch(','));
            base.DECLARE_SWITCH = false;
            base.DECLARATION_CHECK_SWITCH = false;
        }

        private void Parse_LockStmt()
        {
            this.Match("lock");
            this.Match('(');
            int num = this.Parse_Expression();
            this.Match(')');
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

        private void Parse_MemberDeclaration(int class_id, ClassKind ck, ModifierList owner_ml)
        {
            int num27;
            int num28;
            string name = base.GetName(class_id);
            base.DECLARE_SWITCH = true;
            if (base.IsCurrText('['))
            {
                this.Parse_Attributes();
            }
            ModifierList ml = this.Parse_Modifiers();
            bool flag = ml.HasModifier(Modifier.Static);
            int k = base.ReadToken();
            string text = base.curr_token.Text;
            k += base.ReadToken();
            string str3 = base.curr_token.Text;
            base.Backup_SCANNER(k);
            if ((((base.IsCurrText("enum") | base.IsCurrText("class")) | base.IsCurrText("struct")) | base.IsCurrText("interface")) | base.IsCurrText("delegate"))
            {
                this.Parse_TypeDeclaration(ml);
            }
            else if (!base.IsCurrText("const"))
            {
                ModifierList list2;
                if (!base.IsCurrText("event"))
                {
                    if (base.IsCurrText("implicit"))
                    {
                        base.CheckModifiers(ml, this.operator_modifiers);
                        this.Match("implicit");
                        this.Match("operator");
                        if (!ml.HasModifier(Modifier.Public) || !ml.HasModifier(Modifier.Static))
                        {
                            base.RaiseErrorEx(false, "CS0558. User-defined operator 'operator' must be declared static and public.", new object[] { "op_Implicit" });
                        }
                        int num11 = this.Parse_Type();
                        int num12 = base.NewVar();
                        base.SetName(num12, "op_Implicit");
                        this.BeginMethod(num12, MemberKind.Method, ml, num11);
                        this.Match('(');
                        if (this.Parse_FormalParameterList(num12, false) != 1)
                        {
                            base.RaiseErrorEx(false, "CS1535. Overloaded unary operator '{0}' only takes one parameter.", new object[] { "implicit" });
                        }
                        this.Match(')');
                        if (base.IsCurrText(';'))
                        {
                            this.Match(';');
                        }
                        else
                        {
                            this.InitMethod(num12);
                            this.Parse_MethodBlock();
                        }
                        this.EndMethod(num12);
                        goto Label_1519;
                    }
                    if (base.IsCurrText("explicit"))
                    {
                        base.CheckModifiers(ml, this.operator_modifiers);
                        this.Match("explicit");
                        this.Match("operator");
                        if (!ml.HasModifier(Modifier.Public) || !ml.HasModifier(Modifier.Static))
                        {
                            base.RaiseErrorEx(false, "CS0558. User-defined operator 'operator' must be declared static and public.", new object[] { "op_Explicit" });
                        }
                        int num14 = this.Parse_Type();
                        int num15 = base.NewVar();
                        base.SetName(num15, "op_Explicit");
                        this.BeginMethod(num15, MemberKind.Method, ml, num14);
                        this.Match('(');
                        if (this.Parse_FormalParameterList(num15, false) != 1)
                        {
                            base.RaiseErrorEx(false, "CS1535. Overloaded unary operator '{0}' only takes one parameter.", new object[] { "explicit" });
                        }
                        this.Match(')');
                        string str6 = base.GetName(num14);
                        string str7 = base.GetName(this.param_type_ids[0]);
                        if ((str6 == name) && (str7 == name))
                        {
                            base.RaiseError(false, "CS0556. User-defined conversion must convert to or from the enclosing type.");
                        }
                        if ((str6 != name) && (str7 != name))
                        {
                            base.RaiseError(false, "CS0556. User-defined conversion must convert to or from the enclosing type.");
                        }
                        if (base.IsCurrText(';'))
                        {
                            this.Match(';');
                        }
                        else
                        {
                            this.InitMethod(num15);
                            this.Parse_MethodBlock();
                        }
                        this.EndMethod(num15);
                        goto Label_1519;
                    }
                    if (base.IsCurrText("~"))
                    {
                        if (ck == ClassKind.Struct)
                        {
                            base.RaiseError(false, "CS0575. Only class types can contain destructors.");
                        }
                        base.CheckModifiers(ml, this.destructor_modifiers);
                        this.Match("~");
                        int num17 = this.Parse_Ident();
                        if (base.GetName(class_id) != base.GetName(num17))
                        {
                            base.RaiseError(false, "CS0574. Name of destructor must match name of class.");
                        }
                        this.BeginMethod(num17, MemberKind.Method, ml, 1);
                        this.Match('(');
                        if (!base.IsCurrText(')'))
                        {
                            this.Parse_FormalParameterList(num17, false);
                        }
                        this.Match(')');
                        if (base.IsCurrText(';'))
                        {
                            this.Match(';');
                        }
                        else
                        {
                            this.InitMethod(num17);
                            this.Parse_MethodBlock();
                        }
                        this.EndMethod(num17);
                        goto Label_1519;
                    }
                    if ((base.GetName(class_id) == base.curr_token.Text) && (text == "("))
                    {
                        IntegerList list3;
                        base.CheckModifiers(ml, this.constructor_modifiers);
                        if (flag && this.HasAccessModifier(ml))
                        {
                            base.RaiseErrorEx(false, "CS0515. '{0}' : access modifiers are not allowed on static constructors.", new object[] { base.GetName(class_id) });
                        }
                        int num18 = this.Parse_Ident();
                        this.valid_this_context = true;
                        this.BeginMethod(num18, MemberKind.Constructor, ml, 1);
                        this.Match('(');
                        if (!base.IsCurrText(')'))
                        {
                            if (flag)
                            {
                                base.RaiseErrorEx(false, "CS0132. '{0}' : a static constructor must be parameterless.", new object[] { base.GetName(class_id) });
                            }
                            this.Parse_FormalParameterList(num18, false);
                        }
                        else if (ck == ClassKind.Struct)
                        {
                            base.RaiseError(false, "CS0568. Structs cannot contain explicit parameterless constructors.");
                        }
                        this.Match(')');
                        this.InitMethod(num18);
                        if (flag)
                        {
                            list3 = this.static_variable_initializers;
                        }
                        else
                        {
                            list3 = this.variable_initializers;
                        }
                        for (int i = 0; i < list3.Count; i++)
                        {
                            int num20 = list3[i];
                            this.Gen(base.code.OP_BEGIN_CALL, num20, 0, 0);
                            this.Gen(base.code.OP_PUSH, base.CurrThisID, 0, 0);
                            this.Gen(base.code.OP_CALL, num20, 0, 0);
                        }
                        if (base.IsCurrText(':'))
                        {
                            this.Match(':');
                            if (flag)
                            {
                                base.RaiseErrorEx(false, "CS0514. '{0}' : static constructor cannot have an explicit this or base constructor call.", new object[] { base.GetName(class_id) });
                            }
                            if (base.IsCurrText("base"))
                            {
                                if (ck == ClassKind.Struct)
                                {
                                    base.RaiseErrorEx(false, "CS0522. '{0}' : structs cannot call base class constructors.", new object[] { base.GetName(class_id) });
                                }
                                this.Match("base");
                                int num21 = base.NewVar();
                                int num22 = base.NewVar();
                                this.Gen(base.code.OP_EVAL_BASE_TYPE, class_id, 0, num22);
                                this.Gen(base.code.OP_ASSIGN_NAME, num21, num22, num21);
                                int num23 = base.NewVar();
                                this.Gen(base.code.OP_CAST, num22, base.CurrThisID, num23);
                                this.Gen(base.code.OP_CREATE_REFERENCE, num23, 0, num21);
                                base.DECLARE_SWITCH = false;
                                this.Match('(');
                                this.Gen(base.code.OP_CALL_BASE, num21, this.Parse_ArgumentList(")", num21, num23), 0);
                                base.DECLARE_SWITCH = true;
                                this.Match(')');
                            }
                            else if (base.IsCurrText("this"))
                            {
                                this.Match("this");
                                base.DECLARE_SWITCH = false;
                                this.Match('(');
                                this.Gen(base.code.OP_CALL, num18, this.Parse_ArgumentList(")", num18, base.CurrThisID), 0);
                                base.DECLARE_SWITCH = true;
                                this.Match(')');
                            }
                            else
                            {
                                base.RaiseError(true, "CS1018. Keyword this or base expected.");
                            }
                        }
                        else if (!flag)
                        {
                            int num24 = base.NewVar();
                            int num25 = base.NewVar();
                            this.Gen(base.code.OP_EVAL_BASE_TYPE, class_id, 0, num25);
                            this.Gen(base.code.OP_ASSIGN_NAME, num24, num25, num24);
                            int num26 = base.NewVar();
                            this.Gen(base.code.OP_CAST, num25, base.CurrThisID, num26);
                            this.Gen(base.code.OP_CREATE_REFERENCE, num26, 0, num24);
                            this.Gen(base.code.OP_BEGIN_CALL, num24, 0, 0);
                            this.Gen(base.code.OP_PUSH, num26, 0, 0);
                            this.Gen(base.code.OP_CALL, num24, 0, 0);
                        }
                        if (base.IsCurrText(';'))
                        {
                            this.Match(';');
                        }
                        else
                        {
                            this.Parse_Block();
                        }
                        this.EndMethod(num18);
                        this.valid_this_context = false;
                        if (flag)
                        {
                            this.static_variable_initializers.Clear();
                        }
                        else
                        {
                            this.has_constructor = true;
                        }
                        goto Label_1519;
                    }
                    num27 = this.Parse_Type();
                    num28 = 0;
                    if (!(str3 == "."))
                    {
                        goto Label_0CD9;
                    }
                    if (ml.HasModifier(Modifier.Public))
                    {
                        base.RaiseErrorEx(false, "CS0106. The modifier '{0}' is not valid for this item.", new object[] { "public" });
                    }
                    ml.Add(Modifier.Public);
                    num28 = this.Parse_Ident();
                    this.Gen(base.code.OP_EVAL_TYPE, 0, 0, num28);
                    while (true)
                    {
                        base.REF_SWITCH = true;
                        if (!base.CondMatch('.'))
                        {
                            goto Label_0CD2;
                        }
                        int num29 = base.ReadToken();
                        string str8 = base.curr_token.Text;
                        base.Backup_SCANNER(num29);
                        if (str8 != ".")
                        {
                            goto Label_0CD2;
                        }
                        int num30 = num28;
                        num28 = this.Parse_Ident();
                        this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, num30, 0, num28);
                    }
                }
                this.Match("event");
                base.CheckModifiers(ml, this.method_modifiers);
                int num4 = this.Parse_Type();
                int id = this.Parse_Ident();
                if (ml.HasModifier(Modifier.Abstract) && !owner_ml.HasModifier(Modifier.Abstract))
                {
                    string str4 = base.GetName(id);
                    base.RaiseErrorEx(false, "CS0513. '{0}' is abstract but it is contained in nonabstract class '{1}'", new object[] { str4, name });
                }
                if (base.IsCurrText('{'))
                {
                    base.BeginEvent(id, ml, num4, 0);
                    this.Gen(base.code.OP_ADD_MODIFIER, id, 1, 0);
                    this.param_ids.Clear();
                    this.param_type_ids.Clear();
                    this.param_mods.Clear();
                    this.Parse_EventAccessorDeclarations(id, num4, ml);
                    base.EndEvent(id);
                    goto Label_1519;
                }
            Label_0249:
                list2 = ml.Clone();
                list2.Delete(Modifier.Public);
                base.BeginField(id, list2, num4);
                string str5 = base.GetName(id);
                base.SetName(id, "__" + str5);
                if (base.IsCurrText('='))
                {
                    this.Parse_InstanceVariableInitializer(id, num4, ml);
                }
                base.EndField(id);
                int num10 = base.NewVar();
                base.SetName(num10, str5);
                base.BeginEvent(num10, ml, num4, 0);
                this.Gen(base.code.OP_ADD_EVENT_FIELD, num10, id, 0);
                int num6 = base.NewVar();
                base.SetName(num6, "add_" + str5);
                this.BeginMethod(num6, MemberKind.Method, list2, 1);
                int num7 = base.NewVar();
                base.SetName(num7, "value");
                this.Gen(base.code.OP_ADD_PARAM, num6, num7, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, num7, num4, 0);
                this.InitMethod(num6);
                int num8 = base.NewVar();
                base.SetName(num8, base.GetName(id));
                int res = base.NewVar();
                this.Gen(base.code.OP_EVAL, 0, 0, num8);
                this.Gen(base.code.OP_PLUS, num8, num7, res);
                this.Gen(base.code.OP_ASSIGN, num8, res, num8);
                this.EndMethod(num6);
                this.Gen(base.code.OP_ADD_ADD_ACCESSOR, num10, num6, 0);
                num6 = base.NewVar();
                base.SetName(num6, "remove_" + str5);
                this.BeginMethod(num6, MemberKind.Method, list2, 1);
                num7 = base.NewVar();
                base.SetName(num7, "value");
                this.Gen(base.code.OP_ADD_PARAM, num6, num7, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, num7, num4, 0);
                this.InitMethod(num6);
                num8 = base.NewVar();
                base.SetName(num8, base.GetName(id));
                res = base.NewVar();
                this.Gen(base.code.OP_EVAL, 0, 0, num8);
                this.Gen(base.code.OP_MINUS, num8, num7, res);
                this.Gen(base.code.OP_ASSIGN, num8, res, num8);
                this.EndMethod(num6);
                this.Gen(base.code.OP_ADD_REMOVE_ACCESSOR, num10, num6, 0);
                base.EndEvent(num10);
                if (base.IsCurrText(','))
                {
                    this.Call_SCANNER();
                    id = this.Parse_Ident();
                    goto Label_0249;
                }
                this.Match(';');
            }
            else
            {
                this.Match("const");
                base.CheckModifiers(ml, this.constant_modifiers);
                ml.Add(Modifier.Static);
                int num2 = this.Parse_Type();
                do
                {
                    int num3 = this.Parse_Ident();
                    if (flag)
                    {
                        base.RaiseErrorEx(false, "CS0504. The constant '{0}' cannot be marked static.", new object[] { base.GetName(num3) });
                    }
                    if (!base.IsCurrText('='))
                    {
                        base.RaiseError(true, "CS0145. A const field requires a value to be provided.");
                    }
                    base.BeginField(num3, ml, num2);
                    base.DECLARE_SWITCH = false;
                    this.Parse_InstanceVariableInitializer(num3, num2, ml);
                    base.EndField(num3);
                    base.DECLARE_SWITCH = true;
                }
                while (base.CondMatch(','));
                this.Match(';');
            }
            goto Label_1519;
        Label_0CD2:
            base.REF_SWITCH = false;
        Label_0CD9:
            if (base.IsCurrText("this"))
            {
                base.CheckModifiers(ml, this.method_modifiers);
                this.Match("this");
                int num31 = base.NewVar();
                base.SetName(num31, "Item");
                if (num27 == 1)
                {
                    base.RaiseError(false, "CS0620. Indexers can't have void type.");
                }
                if (ml.HasModifier(Modifier.Abstract) && !owner_ml.HasModifier(Modifier.Abstract))
                {
                    string str9 = base.GetName(num31);
                    base.RaiseErrorEx(false, "CS0513. '{0}' is abstract but it is contained in nonabstract class '{1}'", new object[] { str9, name });
                }
                this.Match('[');
                int num32 = this.Parse_FormalParameterList(num31, true);
                this.Match(']');
                this.valid_this_context = true;
                base.BeginProperty(num31, ml, num27, num32);
                this.Parse_PropertyAccessorDeclarations(num31, num27, ml);
                base.EndProperty(num31);
                this.valid_this_context = false;
            }
            else if (!base.IsCurrText("operator"))
            {
                int num39;
                if (base.IsCurrText('('))
                {
                    base.RaiseError(true, "CS1520. Class, struct, or interface method must have a return type.");
                }
            Label_0F95:
                num39 = this.Parse_Ident();
                if (base.IsCurrText(';'))
                {
                    if (num27 == 1)
                    {
                        base.RaiseError(false, "CS0670. Field cannot have void type.");
                    }
                    base.CheckModifiers(ml, this.field_modifiers);
                    base.BeginField(num39, ml, num27);
                    this.Add_InstanceVariableInitializer(num39, num27, ml);
                    base.EndField(num39);
                    this.Match(';');
                }
                else if (base.IsCurrText(','))
                {
                    base.CheckModifiers(ml, this.field_modifiers);
                    base.BeginField(num39, ml, num27);
                    base.EndField(num39);
                    this.Match(',');
                    do
                    {
                        num39 = this.Parse_Ident();
                        base.BeginField(num39, ml, num27);
                        if (base.IsCurrText('='))
                        {
                            if (!ml.HasModifier(Modifier.Static) && (ck == ClassKind.Struct))
                            {
                                base.RaiseErrorEx(false, "CS0573. '{0}' : cannot have instance field initializers in structs.", new object[] { base.GetName(num39) });
                            }
                            this.Parse_InstanceVariableInitializer(num39, num27, ml);
                        }
                        else
                        {
                            this.Add_InstanceVariableInitializer(num39, num27, ml);
                        }
                        base.EndField(num39);
                    }
                    while (base.CondMatch(','));
                    this.Match(';');
                }
                else if (base.IsCurrText('='))
                {
                    if (num27 == 1)
                    {
                        base.RaiseError(false, "CS0670. Field cannot have void type.");
                    }
                    if (!ml.HasModifier(Modifier.Static) && (ck == ClassKind.Struct))
                    {
                        base.RaiseErrorEx(false, "CS0573. '{0}' : cannot have instance field initializers in structs.", new object[] { base.GetName(num39) });
                    }
                    base.CheckModifiers(ml, this.field_modifiers);
                    base.BeginField(num39, ml, num27);
                    this.Parse_InstanceVariableInitializer(num39, num27, ml);
                    base.EndField(num39);
                    if (base.IsCurrText(','))
                    {
                        this.Match(',');
                        goto Label_0F95;
                    }
                    this.Match(';');
                }
                else if (base.IsCurrText('('))
                {
                    base.CheckModifiers(ml, this.method_modifiers);
                    if (ml.HasModifier(Modifier.Abstract) && !owner_ml.HasModifier(Modifier.Abstract))
                    {
                        string str12 = base.GetName(num39);
                        base.RaiseErrorEx(false, "CS0513. '{0}' is abstract but it is contained in nonabstract class '{1}'", new object[] { str12, name });
                    }
                    if (flag && ((ml.HasModifier(Modifier.Abstract) || ml.HasModifier(Modifier.Virtual)) || ml.HasModifier(Modifier.Override)))
                    {
                        base.RaiseErrorEx(false, "CS0112. A static member '{0}' cannot be marked as override, virtual or abstract.", new object[] { base.GetName(num39) });
                    }
                    if (ml.HasModifier(Modifier.Override) && (ml.HasModifier(Modifier.Virtual) || ml.HasModifier(Modifier.New)))
                    {
                        base.RaiseErrorEx(false, "CS0113. A member '{0}' marked as override cannot be marked as new or virtual.", new object[] { base.GetName(num39) });
                    }
                    if (ml.HasModifier(Modifier.Extern) && ml.HasModifier(Modifier.Abstract))
                    {
                        base.RaiseErrorEx(false, "CS0180. '{0}' cannot be both extern and abstract.", new object[] { base.GetName(num39) });
                    }
                    if (ml.HasModifier(Modifier.Sealed) && !ml.HasModifier(Modifier.Override))
                    {
                        base.RaiseErrorEx(false, "CS0238. '{0}' cannot be sealed because it is not an override.", new object[] { base.GetName(num39) });
                    }
                    if (ml.HasModifier(Modifier.Abstract) && ml.HasModifier(Modifier.Virtual))
                    {
                        base.RaiseErrorEx(false, "CS0503. The abstract method '{0}' cannot be marked virtual.", new object[] { base.GetName(num39) });
                    }
                    if (ml.HasModifier(Modifier.Virtual) && owner_ml.HasModifier(Modifier.Sealed))
                    {
                        base.RaiseErrorEx(false, "CS0549. '{0}' is a new virtual member in sealed class '{1}'.", new object[] { base.GetName(num39) });
                    }
                    int num40 = num39;
                    this.valid_this_context = true;
                    this.BeginMethod(num40, MemberKind.Method, ml, num27);
                    if (num28 > 0)
                    {
                        this.Gen(base.code.OP_ADD_EXPLICIT_INTERFACE, num40, num28, 0);
                    }
                    this.Match('(');
                    if (!base.IsCurrText(')'))
                    {
                        this.Parse_FormalParameterList(num40, false);
                    }
                    this.Match(')');
                    if (base.IsCurrText(';'))
                    {
                        if (!ml.HasModifier(Modifier.Extern) && !ml.HasModifier(Modifier.Abstract))
                        {
                            base.RaiseErrorEx(false, "CS0501. '{0}' must declare a body because it is not marked abstract or extern.", new object[] { base.GetName(num40) });
                        }
                        this.Match(';');
                    }
                    else
                    {
                        if (ml.HasModifier(Modifier.Extern))
                        {
                            base.RaiseErrorEx(false, "CS0179. '{0}' cannot be extern and declare a body.", new object[] { base.GetName(num40) });
                        }
                        this.InitMethod(num40);
                        if (ml.HasModifier(Modifier.Abstract))
                        {
                            string str13 = base.GetName(num40);
                            base.RaiseErrorEx(false, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { str13 });
                        }
                        if (base.GetName(num39) == "Main")
                        {
                            this.Gen(base.code.OP_CHECKED, base.TRUE_id, 0, 0);
                        }
                        this.Parse_MethodBlock();
                        if (base.GetName(num39) == "Main")
                        {
                            this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                        }
                    }
                    this.EndMethod(num40);
                    this.valid_this_context = false;
                }
                else if (base.IsCurrText('{'))
                {
                    base.CheckModifiers(ml, this.method_modifiers);
                    if (ml.HasModifier(Modifier.Abstract) && !owner_ml.HasModifier(Modifier.Abstract))
                    {
                        string str14 = base.GetName(num39);
                        base.RaiseErrorEx(false, "CS0513. '{0}' is abstract but it is contained in nonabstract class '{1}'", new object[] { str14, name });
                    }
                    this.valid_this_context = true;
                    base.BeginProperty(num39, ml, num27, 0);
                    this.param_ids.Clear();
                    this.param_type_ids.Clear();
                    this.param_mods.Clear();
                    this.Parse_PropertyAccessorDeclarations(num39, num27, ml);
                    base.EndProperty(num39);
                    this.valid_this_context = false;
                }
                else
                {
                    this.Parse_TypeDeclaration(ml);
                }
            }
            else
            {
                base.CheckModifiers(ml, this.operator_modifiers);
                this.Match("operator");
                if (num27 == 1)
                {
                    base.RaiseError(false, "CS0590. User-defined operators cannot return void.");
                }
                if (!ml.HasModifier(Modifier.Public) || !ml.HasModifier(Modifier.Static))
                {
                    base.RaiseErrorEx(false, "CS0558. User-defined operator 'operator' must be declared static and public.", new object[] { base.curr_token.Text });
                }
                string s = base.curr_token.Text;
                this.Call_SCANNER();
                int num33 = base.NewVar();
                this.BeginMethod(num33, MemberKind.Method, ml, num27);
                this.Match('(');
                int num34 = this.Parse_FormalParameterList(num33, false);
                string str11 = "";
                switch (num34)
                {
                    case 1:
                    {
                        int index = this.overloadable_unary_operators.IndexOf(s);
                        if (index == -1)
                        {
                            base.RaiseError(true, "CS1019. Overloadable unary operator expected.");
                        }
                        int num36 = (int) this.overloadable_unary_operators.Objects[index];
                        str11 = (string) base.code.overloadable_unary_operators_str[num36];
                        break;
                    }
                    case 2:
                    {
                        int num37 = this.overloadable_binary_operators.IndexOf(s);
                        if (num37 == -1)
                        {
                            base.RaiseError(true, "CS1020. Overloadable binary operator expected.");
                        }
                        int num38 = (int) this.overloadable_binary_operators.Objects[num37];
                        str11 = (string) base.code.overloadable_binary_operators_str[num38];
                        break;
                    }
                    default:
                        base.RaiseErrorEx(true, "CS1534. Overloaded binary operator '{0}' only takes two parameters.", new object[] { s });
                        break;
                }
                base.SetName(num33, str11);
                this.Match(')');
                if (base.IsCurrText(';'))
                {
                    this.Match(';');
                }
                else
                {
                    this.InitMethod(num33);
                    this.Parse_MethodBlock();
                }
                this.EndMethod(num33);
            }
        Label_1519:
            base.DECLARE_SWITCH = false;
        }

        private void Parse_MethodBlock()
        {
            this.Parse_Block();
            if (base.IsCurrText(';'))
            {
                base.RaiseError(true, "CS1597. Semicolon after method or accessor block is not valid.");
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

        private int Parse_MultiArrayInitializer(int array_type_id)
        {
            string name = base.GetName(array_type_id);
            int rank = CSLite_System.GetRank(name);
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
        Label_0113:
            if (base.IsCurrText('{'))
            {
                IntegerList list8=null;
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
                            goto Label_0113;
                        }
                        if (list4[num6] == 0)
                        {
                            IntegerList list5=list2;
                            int num13=num6;
                            //(list5 = list2)[num13 = num6] = list5[num13] + 1;
                            list5[num13 ] = list5[num13] + 1;
                        }
                        list6 = list3;
                        num14 = num6;
                        //(list6 = list3)[num14 = num6] = list6[num14] + 1;
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
                            goto Label_0113;
                        }
                    }
                }
                if (list4[num6] == 0)
                {
                    IntegerList list7=list2;
                    int num15=num6;
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
                    IntegerList list9= list2;
                    int num17= num6;
                    //(list9 = list2)[num17 = num6] = list9[num17] + 1;
                    list9[num17]=list9[num17] + 1;
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
                list11[num19]=list11[num19] - 1;
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
            goto Label_0113;
        }

        private int Parse_MultiplicativeExpr(int result)
        {
            result = this.Parse_UnaryExpr(result);
            for (object obj2 = this.multiplicative_operators[base.curr_token.Text]; obj2 != null; obj2 = this.multiplicative_operators[base.curr_token.Text])
            {
                int op = (int) obj2;
                this.Call_SCANNER();
                result = base.BinOp(op, result, this.Parse_UnaryExpr(0));
            }
            return result;
        }

        private void Parse_NamespaceDeclaration()
        {
            this.Match("namespace");
            IntegerList list = new IntegerList(false);
            do
            {
                int avalue = this.Parse_Ident();
                list.Add(avalue);
                base.BeginNamespace(avalue);
            }
            while (base.CondMatch('.'));
            this.Match('{');
            if (base.IsCurrText("using"))
            {
                this.Parse_UsingDirectives();
            }
            if (!base.IsCurrText('}'))
            {
                this.Parse_NamespaceMemberDeclarations();
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                base.EndNamespace(list[i]);
            }
            this.Match('}');
            if (base.IsCurrText(';'))
            {
                this.Match(';');
            }
        }

        private void Parse_NamespaceMemberDeclarations()
        {
            do
            {
                if (base.IsCurrText('}'))
                {
                    base.RaiseError(true, "CS1022. Type or namespace definition, or end-of-file expected.");
                }
                else if (base.IsCurrText("using"))
                {
                    base.RaiseError(true, "CS1529. A using clause must precede all other namespace elements.");
                }
                else if (base.IsCurrText("new"))
                {
                    base.RaiseError(true, "CS1530. Keyword new not allowed on namespace elements.");
                }
                if (base.IsCurrText('['))
                {
                    this.Parse_Attributes();
                }
                ModifierList ml = this.Parse_Modifiers();
                if (base.IsCurrText("namespace"))
                {
                    this.Parse_NamespaceDeclaration();
                }
                else
                {
                    this.Parse_TypeDeclaration(ml);
                }
            }
            while (!base.IsEOF() && !base.IsCurrText('}'));
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

        private int Parse_NullLiteral()
        {
            this.Match("null");
            return base.NULL_id;
        }

        private int Parse_PrimaryExpr()
        {
            int currThisID;
            if (base.IsCurrText('('))
            {
                this.Match('(');
                currThisID = this.Parse_Expression();
                this.Match(')');
            }
            else if (base.IsCurrText("checked"))
            {
                this.Match("checked");
                this.Match('(');
                this.Gen(base.code.OP_CHECKED, base.TRUE_id, 0, 0);
                currThisID = this.Parse_Expression();
                this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                this.Match(')');
            }
            else if (base.IsCurrText("unchecked"))
            {
                this.Match("unchecked");
                this.Match('(');
                this.Gen(base.code.OP_CHECKED, base.FALSE_id, 0, 0);
                currThisID = this.Parse_Expression();
                this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                this.Match(')');
            }
            else
            {
                if (!base.IsCurrText("new"))
                {
                    if (base.IsCurrText("this"))
                    {
                        this.Match("this");
                        currThisID = base.CurrThisID;
                        if (base.GetName(currThisID) != "this")
                        {
                            base.RaiseError(false, "CS0026. Keyword this is not valid in a static property, static method, or static field initializer.");
                        }
                        if (!this.valid_this_context)
                        {
                            base.RaiseError(false, "CS0027. Keyword this is not available in the current context.");
                        }
                        if (base.IsCurrText('.'))
                        {
                            base.REF_SWITCH = true;
                            this.Match('.');
                            int num6 = currThisID;
                            currThisID = this.Parse_Ident();
                            this.Gen(base.code.OP_CREATE_REFERENCE, num6, 0, currThisID);
                        }
                        goto Label_06AB;
                    }
                    if (!base.IsCurrText("base"))
                    {
                        if (base.IsCurrText("null"))
                        {
                            currThisID = this.Parse_NullLiteral();
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
                        else if (base.IsCurrText("typeof"))
                        {
                            this.Match("typeof");
                            currThisID = base.NewVar();
                            this.Match('(');
                            this.Gen(base.code.OP_TYPEOF, this.Parse_Type(), 0, currThisID);
                            this.Match(')');
                        }
                        else
                        {
                            currThisID = this.Parse_Ident();
                        }
                        goto Label_06AB;
                    }
                    this.Match("base");
                    currThisID = base.CurrThisID;
                    if (base.GetName(currThisID) != "this")
                    {
                        base.RaiseError(false, "CS1511. Keyword base is not available in a static method.");
                    }
                    if (base.IsCurrText('.'))
                    {
                        base.REF_SWITCH = true;
                        this.Match('.');
                        int num7 = base.NewVar();
                        this.Gen(base.code.OP_EVAL_BASE_TYPE, base.CurrClassID, 0, num7);
                        int num8 = base.NewVar();
                        this.Gen(base.code.OP_CAST, num7, base.CurrThisID, num8);
                        currThisID = this.Parse_Ident();
                        this.Gen(base.code.OP_CREATE_REFERENCE, num8, 0, currThisID);
                        if (base.IsCurrText('('))
                        {
                            int num9 = currThisID;
                            currThisID = base.NewVar();
                            this.Match('(');
                            this.Gen(base.code.OP_CALL_BASE, num9, this.Parse_ArgumentList(")", num9, num9), currThisID);
                            this.Match(')');
                        }
                        goto Label_06AB;
                    }
                    this.Match('[');
                    int res = base.NewVar();
                    this.Gen(base.code.OP_EVAL_BASE_TYPE, base.CurrClassID, 0, res);
                    currThisID = base.NewVar();
                    this.Gen(base.code.OP_CAST, res, base.CurrThisID, currThisID);
                    int num11 = base.NewVar();
                    this.Gen(base.code.OP_CREATE_INDEX_OBJECT, currThisID, 0, num11);
                    while (true)
                    {
                        this.Gen(base.code.OP_ADD_INDEX, num11, this.Parse_Expression(), currThisID);
                        if (!base.CondMatch(','))
                        {
                            this.Gen(base.code.OP_SETUP_INDEX_OBJECT, num11, 0, 0);
                            this.Match(']');
                            currThisID = num11;
                            goto Label_06AB;
                        }
                    }
                }
                this.Match("new");
                int num2 = this.Parse_NonArrayType();
                if (!base.IsCurrText('['))
                {
                    if (!base.IsCurrText('('))
                    {
                        base.RaiseError(true, "CS1526. 'new' expression requires () or [] after type.");
                    }
                    currThisID = base.NewVar();
                    this.Gen(base.code.OP_CREATE_OBJECT, num2, 0, currThisID);
                    this.Match('(');
                    this.Gen(base.code.OP_CALL, num2, this.Parse_ArgumentList(")", num2, currThisID), 0);
                    this.Match(')');
                }
                else
                {
                    bool flag = true;
                    string str = "";
                    do
                    {
                        this.Match('[');
                        if (!base.IsCurrText(']') && !base.IsCurrText(','))
                        {
                            flag = false;
                            break;
                        }
                        str = str + "[";
                        if (!base.IsCurrText(']'))
                        {
                            do
                            {
                                this.Match(',');
                                str = str + ",";
                            }
                            while (base.IsCurrText(','));
                        }
                        this.Match(']');
                        str = str + "]";
                    }
                    while (base.IsCurrText('['));
                    if (flag)
                    {
                        int id = base.NewVar();
                        base.SetName(id, base.GetName(num2) + str);
                        this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
                        currThisID = this.Parse_ArrayInitializer(id);
                    }
                    else
                    {
                        int num4 = base.NewVar();
                        this.Gen(base.code.OP_EVAL_TYPE, 0, 0, num4);
                        currThisID = base.NewVar();
                        this.Gen(base.code.OP_CREATE_OBJECT, num4, 0, currThisID);
                        this.Gen(base.code.OP_BEGIN_CALL, num4, 0, 0);
                        int num5 = 0;
                        str = "[";
                        while (true)
                        {
                            num5++;
                            this.Gen(base.code.OP_PUSH, this.Parse_Expression(), 0, num4);
                            if (!base.CondMatch(','))
                            {
                                break;
                            }
                            str = str + ",";
                        }
                        this.Gen(base.code.OP_PUSH, currThisID, 0, 0);
                        this.Gen(base.code.OP_CALL, num4, num5, 0);
                        this.Match(']');
                        str = str + "]";
                        if (base.IsCurrText('['))
                        {
                            str = str + this.Parse_RankSpecifiers();
                        }
                        base.SetName(num4, base.GetName(num2) + str);
                        if (base.IsCurrText('{'))
                        {
                            currThisID = this.Parse_ArrayInitializer(num4);
                        }
                    }
                }
            }
        Label_06AB:
            while (base.IsCurrText('('))
            {
                int num12 = currThisID;
                currThisID = base.NewVar();
                this.Match('(');
                this.Gen(base.code.OP_CALL, num12, this.Parse_ArgumentList(")", num12, num12), currThisID);
                this.Match(')');
            }
            if (!base.IsCurrText('['))
            {
                if (!base.IsCurrText('.'))
                {
                    if (base.IsCurrText("++"))
                    {
                        this.Match("++");
                        int num15 = base.NewVar();
                        this.Gen(base.code.OP_ASSIGN, num15, currThisID, num15);
                        int num16 = base.NewVar();
                        this.Gen(base.code.OP_INC, currThisID, 0, num16);
                        this.Gen(base.code.OP_ASSIGN, currThisID, num16, currThisID);
                        return num15;
                    }
                    if (base.IsCurrText("--"))
                    {
                        this.Match("--");
                        int num17 = base.NewVar();
                        this.Gen(base.code.OP_ASSIGN, num17, currThisID, num17);
                        int num18 = base.NewVar();
                        this.Gen(base.code.OP_DEC, currThisID, 0, num18);
                        this.Gen(base.code.OP_ASSIGN, currThisID, num18, currThisID);
                        currThisID = num17;
                    }
                    return currThisID;
                }
                base.REF_SWITCH = true;
                this.Match('.');
                int num14 = currThisID;
                currThisID = this.Parse_Ident();
                this.Gen(base.code.OP_CREATE_REFERENCE, num14, 0, currThisID);
            }
            else
            {
                int num13 = base.NewVar();
                this.Gen(base.code.OP_CREATE_INDEX_OBJECT, currThisID, 0, num13);
                this.Match('[');
                do
                {
                    this.Gen(base.code.OP_ADD_INDEX, num13, this.Parse_Expression(), currThisID);
                }
                while (base.CondMatch(','));
                this.Gen(base.code.OP_SETUP_INDEX_OBJECT, num13, 0, 0);
                this.Match(']');
                currThisID = num13;
            }
            goto Label_06AB;
        }

        public override void Parse_Program()
        {
            this.Gen(base.code.OP_UPCASE_OFF, 0, 0, 0);
            this.Gen(base.code.OP_EXPLICIT_ON, 0, 0, 0);
            this.Gen(base.code.OP_STRICT_ON, 0, 0, 0);
            this.Parse_CompilationUnit();
        }

        private void Parse_PropertyAccessorDeclarations(int id, int type_id, ModifierList ml)
        {
            this.ACCESSOR_SWITCH = true;
            if (type_id == 1)
            {
                base.RaiseError(true, "CS0547. '{0}' : property or indexer cannot have void type.");
            }
            this.Match('{');
            int num = 0;
            int num2 = 0;
        Label_0028:
            if (base.IsCurrText('['))
            {
                this.Parse_Attributes();
            }
            if (base.IsCurrText("get"))
            {
                int num3 = base.NewVar();
                base.SetName(num3, "get_" + base.GetName(id));
                this.BeginMethod(num3, MemberKind.Method, ml, type_id);
                num++;
                if (num > 1)
                {
                    base.RaiseError(true, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
                }
                this.Match("get");
                for (int i = 0; i < this.param_ids.Count; i++)
                {
                    int num5 = base.NewVar();
                    base.SetName(num5, base.GetName(this.param_ids[i]));
                    this.Gen(base.code.OP_ASSIGN_TYPE, num5, this.param_type_ids[i], 0);
                    this.Gen(base.code.OP_ADD_PARAM, num3, num5, 0);
                }
                if (base.IsCurrText(';'))
                {
                    if (!ml.HasModifier(Modifier.Extern) && !ml.HasModifier(Modifier.Abstract))
                    {
                        base.RaiseErrorEx(false, "CS0501. '{0}' must declare a body because it is not marked abstract or extern.", new object[] { base.GetName(num3) });
                    }
                    this.Match(';');
                }
                else
                {
                    if (ml.HasModifier(Modifier.Abstract))
                    {
                        string name = base.GetName(num3);
                        base.RaiseErrorEx(true, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { name });
                    }
                    this.InitMethod(num3);
                    this.Parse_MethodBlock();
                }
                this.EndMethod(num3);
                this.Gen(base.code.OP_ADD_READ_ACCESSOR, id, num3, 0);
                goto Label_0028;
            }
            if (base.IsCurrText("set"))
            {
                int num7;
                int num6 = base.NewVar();
                base.SetName(num6, "set_" + base.GetName(id));
                this.BeginMethod(num6, MemberKind.Method, ml, 1);
                num2++;
                if (num2 > 1)
                {
                    base.RaiseError(true, "CS1008. Type byte, sbyte, short, ushort, int, uint, long, or ulong expected.");
                }
                this.Match("set");
                for (int j = 0; j < this.param_ids.Count; j++)
                {
                    num7 = base.NewVar();
                    base.SetName(num7, base.GetName(this.param_ids[j]));
                    this.Gen(base.code.OP_ASSIGN_TYPE, num7, this.param_type_ids[j], 0);
                    this.Gen(base.code.OP_ADD_PARAM, num6, num7, 0);
                }
                num7 = base.NewVar();
                base.SetName(num7, "value");
                this.Gen(base.code.OP_ADD_PARAM, num6, num7, 0);
                this.Gen(base.code.OP_ASSIGN_TYPE, num7, type_id, 0);
                if (base.IsCurrText(';'))
                {
                    if (!ml.HasModifier(Modifier.Extern) && !ml.HasModifier(Modifier.Abstract))
                    {
                        base.RaiseErrorEx(false, "CS0501. '{0}' must declare a body because it is not marked abstract or extern.", new object[] { base.GetName(num6) });
                    }
                    this.Match(';');
                }
                else
                {
                    if (ml.HasModifier(Modifier.Abstract))
                    {
                        string str2 = base.GetName(num6);
                        base.RaiseErrorEx(true, "CS0500. '{0}' cannot declare a body because it is marked abstract.", new object[] { str2 });
                    }
                    this.InitMethod(num6);
                    this.Parse_MethodBlock();
                }
                this.EndMethod(num6);
                this.Gen(base.code.OP_ADD_WRITE_ACCESSOR, id, num6, 0);
                goto Label_0028;
            }
            if ((num + num2) == 0)
            {
                base.RaiseErrorEx(true, "CS0548. '{0}' : property or indexer must have at least one accessor.", new object[] { base.GetName(id) });
            }
            this.Match('}');
            this.ACCESSOR_SWITCH = true;
        }

        private int Parse_QualifiedIdent()
        {
            int id = this.Parse_Ident();
            while (true)
            {
                base.REF_SWITCH = true;
                if (!base.CondMatch('.'))
                {
                    break;
                }
                if (base.GetKind(id) != MemberKind.Type)
                {
                    this.Gen(base.code.OP_EVAL_TYPE, 0, 0, id);
                }
                int num2 = id;
                id = this.Parse_Ident();
                this.Gen(base.code.OP_CREATE_TYPE_REFERENCE, num2, 0, id);
            }
            base.REF_SWITCH = false;
            return id;
        }

        private string Parse_RankSpecifiers()
        {
            string str = "";
            while (true)
            {
                this.Match('[');
                str = str + "[";
                if (!base.IsCurrText(']'))
                {
                    do
                    {
                        this.Match(',');
                        str = str + ",";
                    }
                    while (base.IsCurrText(','));
                }
                this.Match(']');
                str = str + "]";
                if (!base.IsCurrText('['))
                {
                    return str;
                }
            }
        }

        private int Parse_RelationalExpr(int result)
        {
            result = this.Parse_ShiftExpr(result);
            for (object obj2 = this.relational_operators[base.curr_token.Text]; obj2 != null; obj2 = this.relational_operators[base.curr_token.Text])
            {
                int op = (int) obj2;
                this.Call_SCANNER();
                result = base.BinOp(op, result, this.Parse_ShiftExpr(0));
            }
            return result;
        }

        private void Parse_ResourceAcquisition()
        {
            bool flag = this.IsIdentOrStandardType();
            int k = base.ReadToken();
            while (true)
            {
                if (!base.IsCurrText('.'))
                {
                    break;
                }
                k += base.ReadToken();
                k += base.ReadToken();
            }
            bool flag2 = this.IsIdentOrStandardType() || base.IsCurrText('[');
            if (base.IsCurrText('['))
            {
                k += base.ReadToken();
                if (!base.IsCurrText(']') && !base.IsCurrText(','))
                {
                    flag2 = false;
                }
            }
            if (flag && flag2)
            {
                base.Backup_SCANNER(k);
                this.Parse_LocalVariableDeclaration();
            }
            else
            {
                base.Backup_SCANNER(k);
                int avalue = this.Parse_Expression();
                this.local_variables.Clear();
                this.local_variables.Add(avalue);
            }
        }

        private void Parse_ReturnStmt()
        {
            this.Match("return");
            if (!base.IsCurrText(';'))
            {
                int currLevel = base.CurrLevel;
                int resultId = base.GetResultId(currLevel);
                this.Gen(base.code.OP_ASSIGN, resultId, this.Parse_Expression(), resultId);
            }
            this.Gen(base.code.OP_EXIT_SUB, 0, 0, 0);
            this.Match(';');
        }

        private int Parse_ShiftExpr(int result)
        {
            result = this.Parse_AdditiveExpr(result);
            for (object obj2 = this.shift_operators[base.curr_token.Text]; obj2 != null; obj2 = this.shift_operators[base.curr_token.Text])
            {
                int op = (int) obj2;
                this.Call_SCANNER();
                result = base.BinOp(op, result, this.Parse_AdditiveExpr(0));
            }
            return result;
        }

        private void Parse_StatementExpression()
        {
            this.Parse_Assignment();
        }

        private void Parse_StatementList()
        {
            while ((!base.IsCurrText('.') && !base.IsCurrText('}')) && !base.IsEOF())
            {
                this.Parse_Stmt();
            }
        }

        private void Parse_Stmt()
        {
            if (base.IsCurrText("const"))
            {
                this.Parse_LocalConstantDeclaration();
            }
            else
            {
                bool flag = this.IsIdentOrStandardType();
                int k = base.ReadToken();
                if (base.IsCurrText(':'))
                {
                    base.Backup_SCANNER(k);
                    int l = base.Parse_NewLabel();
                    base.SetLabelHere(l);
                    this.Match(':');
                    this.Parse_Stmt();
                }
                else
                {
                    while (true)
                    {
                        if (!base.IsCurrText('.'))
                        {
                            break;
                        }
                        k += base.ReadToken();
                        k += base.ReadToken();
                    }
                    bool flag2 = this.IsIdentOrStandardType() || base.IsCurrText('[');
                    if (base.IsCurrText('['))
                    {
                        k += base.ReadToken();
                        if (!base.IsCurrText(']') && !base.IsCurrText(','))
                        {
                            flag2 = false;
                        }
                    }
                    if (flag && flag2)
                    {
                        base.Backup_SCANNER(k);
                        this.Parse_DeclarationStmt();
                    }
                    else
                    {
                        base.Backup_SCANNER(k);
                        this.Parse_EmbeddedStmt();
                    }
                }
            }
        }

        private void Parse_StructBody(int struct_id, ModifierList owner_modifiers)
        {
            this.variable_initializers.Clear();
            this.static_variable_initializers.Clear();
            this.Match('{');
            while (true)
            {
                if (base.IsCurrText('}'))
                {
                    break;
                }
                this.Parse_StructMemberDeclaration(struct_id, owner_modifiers);
            }
            this.Match('}');
        }

        private void Parse_StructDeclaration(ModifierList ml)
        {
            base.CheckModifiers(ml, this.structure_modifiers);
            this.Match("struct");
            int num = this.Parse_Ident();
            base.BeginStruct(num, ml);
            if (base.IsCurrText(':'))
            {
                this.Parse_ClassBase(num);
            }
            else
            {
                this.Gen(base.code.OP_ADD_ANCESTOR, num, base.ObjectClassId, 0);
            }
            this.Parse_StructBody(num, ml);
            if (this.static_variable_initializers.Count > 0)
            {
                this.CreateDefaultStaticConstructor(num);
            }
            this.CreateDefaultConstructor(num, true);
            base.EndStruct(num);
            if (base.IsCurrText(';'))
            {
                this.Match(';');
            }
        }

        private void Parse_StructMemberDeclaration(int struct_id, ModifierList owner_modifiers)
        {
            this.Parse_MemberDeclaration(struct_id, ClassKind.Struct, owner_modifiers);
        }

        private void Parse_SwitchStmt()
        {
            int label = base.NewLabel();
            int num2 = base.NewLabel();
            IntegerList list = new IntegerList(true);
            IntegerList list2 = new IntegerList(true);
            IntegerStack stack = new IntegerStack();
            int num3 = base.NewVar();
            this.Gen(base.code.OP_ASSIGN, num3, base.TRUE_id, num3);
            base.BreakStack.Push(label);
            this.Match("switch");
            base.BeginBlock();
            this.Match('(');
            int num4 = this.Parse_Expression();
            this.Match(')');
            this.Match('{');
        Label_008D:
            if (base.IsCurrText("case"))
            {
                this.Match("case");
                stack.Push(base.NewLabel());
                int avalue = this.Parse_Expression();
                list.AddObject(avalue, stack.Peek());
                this.Gen(base.code.OP_EQ, num4, avalue, num3);
                this.Gen(base.code.OP_GO_TRUE, stack.Peek(), num3, 0);
            }
            else
            {
                if (!base.IsCurrText("default"))
                {
                    while (stack.Count > 0)
                    {
                        base.SetLabelHere(stack.Peek());
                        stack.Pop();
                    }
                    int num6 = base.NewLabel();
                    this.Gen(base.code.OP_GO_FALSE, num6, num3, 0);
                    while ((!base.IsCurrText("case") && !base.IsCurrText("default")) && !base.IsCurrText('}'))
                    {
                        if (base.IsCurrText("goto"))
                        {
                            this.Match("goto");
                            if (base.IsCurrText("case"))
                            {
                                this.Match("case");
                                int num7 = this.Parse_Expression();
                                this.Gen(base.code.OP_GOTO_START, 0, 0, 0);
                                list2.AddObject(num7, base.CodeCard);
                                this.Match(';');
                            }
                            else if (base.IsCurrText("default"))
                            {
                                this.Match("default");
                                this.Gen(base.code.OP_GOTO_START, num2, 0, 0);
                                this.Match(';');
                            }
                            else
                            {
                                int num8 = this.Parse_Ident();
                                this.Gen(base.code.OP_GOTO_START, num8, 0, 0);
                                this.Match(';');
                            }
                        }
                        else
                        {
                            this.Parse_Stmt();
                        }
                    }
                    base.SetLabelHere(num6);
                    if (base.IsCurrText('}'))
                    {
                        base.BreakStack.Pop();
                        base.SetLabelHere(label);
                        base.EndBlock();
                        this.Match('}');
                        for (int i = 0; i < list2.Count; i++)
                        {
                            int id = list2[i];
                            int n = (int) list2.Objects[i];
                            object val = base.GetVal(id);
                            bool flag = false;
                            for (int j = 0; j < list.Count; j++)
                            {
                                int num13 = list[j];
                                int num14 = (int) list.Objects[j];
                                object obj3 = base.GetVal(num13);
                                if ((val.GetType() == obj3.GetType()) && (val == obj3))
                                {
                                    flag = true;
                                    base.GenAt(n, base.code.OP_GOTO_START, num14, 0, 0);
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                base.CodeCard = n;
                                base.RaiseErrorEx(true, "CS0159. No such label '{0}' within the scope of the goto statement.", new object[] { val.ToString() });
                            }
                        }
                        return;
                    }
                    goto Label_008D;
                }
                this.Match("default");
                base.SetLabelHere(num2);
                this.Gen(base.code.OP_ASSIGN, num3, base.TRUE_id, num3);
            }
            this.Match(':');
            goto Label_008D;
        }

        private void Parse_ThrowStmt()
        {
            this.Match("throw");
            if (!base.IsCurrText(';'))
            {
                this.Gen(base.code.OP_THROW, this.Parse_Expression(), 0, 0);
            }
            else
            {
                this.Gen(base.code.OP_THROW, 0, 0, 0);
            }
            this.Match(';');
        }

        private void Parse_TryStmt()
        {
            this.Match("try");
            int num = base.NewLabel();
            this.Gen(base.code.OP_TRY_ON, num, 0, 0);
            bool flag = false;
            this.Parse_Block();
            int num2 = base.NewLabel();
            this.Gen(base.code.OP_GO, num2, 0, 0);
            if (base.IsCurrText("catch"))
            {
                while (base.IsCurrText("catch"))
                {
                    int num3;
                    this.Match("catch");
                    if (base.IsCurrText('('))
                    {
                        if (flag)
                        {
                            base.RaiseError(true, "CS1017. Try statement already has an empty catch block.");
                        }
                        base.DECLARE_SWITCH = true;
                        this.Match('(');
                        int num4 = this.Parse_Type();
                        if (!base.IsCurrText(')'))
                        {
                            num3 = this.Parse_Ident();
                        }
                        else
                        {
                            num3 = base.NewVar();
                        }
                        this.Gen(base.code.OP_DECLARE_LOCAL_SIMPLE, num3, base.CurrSubId, 0);
                        this.Gen(base.code.OP_ASSIGN_TYPE, num3, num4, 0);
                        base.DECLARE_SWITCH = false;
                        this.Match(')');
                    }
                    else
                    {
                        flag = true;
                        num3 = base.NewVar();
                    }
                    this.Gen(base.code.OP_CATCH, num3, 0, 0);
                    this.Parse_Block();
                    this.Gen(base.code.OP_DISCARD_ERROR, 0, 0, 0);
                    base.SetLabelHere(num2);
                    if (base.IsCurrText("finally"))
                    {
                        this.Match("finally");
                        this.Gen(base.code.OP_FINALLY, 0, 0, 0);
                        this.Parse_Block();
                        this.Gen(base.code.OP_EXIT_ON_ERROR, 0, 0, 0);
                        this.Gen(base.code.OP_GOTO_CONTINUE, 0, 0, 0);
                    }
                }
            }
            else if (base.IsCurrText("finally"))
            {
                base.SetLabelHere(num2);
                this.Match("finally");
                this.Gen(base.code.OP_FINALLY, 0, 0, 0);
                this.Parse_Block();
                this.Gen(base.code.OP_EXIT_ON_ERROR, 0, 0, 0);
                this.Gen(base.code.OP_GOTO_CONTINUE, 0, 0, 0);
            }
            else
            {
                base.RaiseError(true, "CS1524. Expected catch or finally.");
            }
            base.SetLabelHere(num);
            this.Gen(base.code.OP_TRY_OFF, 0, 0, 0);
        }

        private int Parse_Type()
        {
            int id = this.Parse_NonArrayType();
            if (base.IsCurrText('['))
            {
                int num2 = id;
                int num3 = base.NewVar();
                base.SetName(num3, base.GetName(id) + this.Parse_RankSpecifiers());
                this.Gen(base.code.OP_EVAL_TYPE, num2, 0, num3);
                return num3;
            }
            return id;
        }

        private void Parse_TypeDeclaration(ModifierList ml)
        {
            if (base.IsCurrText("class"))
            {
                this.Parse_ClassDeclaration(ml);
            }
            else if (base.IsCurrText("struct"))
            {
                this.Parse_StructDeclaration(ml);
            }
            else if (base.IsCurrText("interface"))
            {
                this.Parse_InterfaceDeclaration(ml);
            }
            else if (base.IsCurrText("enum"))
            {
                this.Parse_EnumDeclaration(ml);
            }
            else if (base.IsCurrText("delegate"))
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
                this.Parse_StatementList();
                this.EndMethod(num2);
                base.EndClass(id);
            }
            else
            {
                this.Match("class");
            }
        }

        private int Parse_UnaryExpr(int result)
        {
            if (result != 0)
            {
                return result;
            }
            if (base.IsCurrText('+'))
            {
                this.Call_SCANNER();
                result = base.UnaryOp(base.code.OP_UNARY_PLUS, this.Parse_UnaryExpr(0));
                return result;
            }
            if (base.IsCurrText('-'))
            {
                this.Call_SCANNER();
                result = base.UnaryOp(base.code.OP_UNARY_MINUS, this.Parse_UnaryExpr(0));
                return result;
            }
            if (base.IsCurrText("++"))
            {
                this.Call_SCANNER();
                result = this.Parse_UnaryExpr(0);
                int res = base.NewVar();
                this.Gen(base.code.OP_INC, result, 0, res);
                this.Gen(base.code.OP_ASSIGN, result, res, result);
                return result;
            }
            if (base.IsCurrText("--"))
            {
                this.Call_SCANNER();
                result = this.Parse_UnaryExpr(0);
                int num2 = base.NewVar();
                this.Gen(base.code.OP_DEC, result, 0, num2);
                this.Gen(base.code.OP_ASSIGN, result, num2, result);
                return result;
            }
            if (base.IsCurrText('!'))
            {
                this.Call_SCANNER();
                result = base.UnaryOp(base.code.OP_NOT, this.Parse_UnaryExpr(0));
                return result;
            }
            if (base.IsCurrText('~'))
            {
                this.Call_SCANNER();
                result = base.UnaryOp(base.code.OP_COMPLEMENT, this.Parse_UnaryExpr(0));
                return result;
            }
            if (base.IsCurrText('*'))
            {
                base.RaiseError(true, "Not implemented");
                this.Call_SCANNER();
                result = base.UnaryOp(0, this.Parse_UnaryExpr(0));
                return result;
            }
            bool flag = base.IsCurrText('(');
            if (!flag)
            {
                flag = false;
                goto Label_027E;
            }
            int k = base.ReadToken();
            bool flag2 = this.IsIdentOrStandardType();
            k += base.ReadToken();
            while (true)
            {
                if (!base.IsCurrText('.'))
                {
                    break;
                }
                k += base.ReadToken();
                k += base.ReadToken();
            }
            if (base.IsCurrText(')'))
            {
                k += base.ReadToken();
            }
            else
            {
                bool flag3 = this.IsIdentOrStandardType() || base.IsCurrText('[');
                if (base.IsCurrText('['))
                {
                    k += base.ReadToken();
                    if (!base.IsCurrText(']') && !base.IsCurrText(','))
                    {
                        flag3 = false;
                    }
                }
                flag = flag2 && flag3;
                if (base.IsCurrText(')'))
                {
                    k += base.ReadToken();
                }
                goto Label_0273;
            }
            if (flag)
            {
                flag = (base.IsCurrText('(') || base.IsIdentifier()) || base.IsConstant();
            }
        Label_0273:
            base.Backup_SCANNER(k);
        Label_027E:
            if (flag)
            {
                this.Match('(');
                int num4 = this.Parse_Type();
                this.Match(')');
                result = base.NewVar();
                this.Gen(base.code.OP_CAST, num4, this.Parse_UnaryExpr(0), result);
            }
            else
            {
                result = this.Parse_PrimaryExpr();
            }
            return result;
        }

        private void Parse_UncheckedStmt()
        {
            this.Match("unchecked");
            this.Gen(base.code.OP_CHECKED, base.FALSE_id, 0, 0);
            this.Parse_Block();
            this.Gen(base.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
        }

        private void Parse_UsingDirective()
        {
            base.DECLARE_SWITCH = true;
            this.Match("using");
            base.DECLARE_SWITCH = false;
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
            this.Match(';');
        }

        private void Parse_UsingDirectives()
        {
            do
            {
                this.Parse_UsingDirective();
            }
            while (base.IsCurrText("using"));
        }

        private void Parse_UsingStmt()
        {
            this.Match("using");
            base.BeginBlock();
            this.Match('(');
            this.Parse_ResourceAcquisition();
            this.Match(')');
            IntegerList list = this.local_variables.Clone();
            int num = base.NewLabel();
            this.Gen(base.code.OP_TRY_ON, num, 0, 0);
            this.Parse_EmbeddedStmt();
            this.Gen(base.code.OP_FINALLY, 0, 0, 0);
            foreach (int num2 in list)
            {
                this.Gen(base.code.OP_DISPOSE, num2, 0, 0);
            }
            this.Gen(base.code.OP_EXIT_ON_ERROR, 0, 0, 0);
            this.Gen(base.code.OP_GOTO_CONTINUE, 0, 0, 0);
            base.SetLabelHere(num);
            this.Gen(base.code.OP_TRY_OFF, 0, 0, 0);
            base.EndBlock();
        }

        private void Parse_WhileStmt()
        {
            int num = base.NewLabel();
            int l = base.NewLabel();
            base.SetLabelHere(l);
            this.Match("while");
            this.Match('(');
            this.Gen(base.code.OP_GO_FALSE, num, this.Parse_Expression(), 0);
            this.Match(')');
            base.BreakStack.Push(num);
            base.ContinueStack.Push(l);
            this.Parse_EmbeddedStmt();
            base.BreakStack.Pop();
            base.ContinueStack.Pop();
            this.Gen(base.code.OP_GO, l, 0, 0);
            base.SetLabelHere(num);
        }
    }
}

