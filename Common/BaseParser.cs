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
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class BaseParser
    {
        public bool AllowKeywordsInMemberAccessExpressions = false;
        private int block_count;
        private IntegerList block_list = new IntegerList(true);
        private IntegerStack block_stack = new IntegerStack();
        protected EntryStack BreakStack = new EntryStack();
        internal Code code;
        protected EntryStack ContinueStack = new EntryStack();
        private int curr_module;
        public Token curr_token;
        protected bool DECLARATION_CHECK_SWITCH = false;
        protected bool DECLARE_SWITCH = false;
        protected StringList keywords = new StringList(false);
        public string language;
        protected IntegerStack level_stack = new IntegerStack();
        protected bool REF_SWITCH = false;
        protected BaseScanner scanner;
        internal BaseScripter scripter;
        internal SymbolTable symbol_table;
        private int temp_count;
        protected bool upcase = false;

        public void AddModuleFromFile(string file_name)
        {
            if ((this.scripter.Modules.IndexOf(file_name) < 0) && File.Exists(file_name))
            {
                this.scripter.AddModule(file_name, this.language);
                this.scripter.AddCodeFromFile(file_name, file_name);
            }
        }

        public void Backup_SCANNER(int k)
        {
            for (int i = 0; i < k; i++)
            {
                this.scanner.BackUp();
            }
            int lineNumber = this.scanner.LineNumber;
            while ((this.code[this.code.Card].op == this.code.OP_SEPARATOR) && (this.code[this.code.Card].arg2 > lineNumber))
            {
                this.code.Card--;
            }
        }

        public void BeginArray(int array_id, ModifierList ml)
        {
            int level = this.symbol_table[array_id].Level;
            this.symbol_table[array_id].Kind = MemberKind.Type;
            this.level_stack.Push(array_id);
            this.Gen(this.code.OP_CREATE_CLASS, array_id, level, 2);
            this.Gen(this.code.OP_ADD_ANCESTOR, array_id, this.ObjectClassId, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, array_id, 7, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, array_id, 6, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, array_id, 1, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, array_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_BEGIN_USING, array_id, 0, 0);
        }

        public void BeginBlock()
        {
            this.block_count++;
            this.block_stack.Push(this.block_count);
            this.block_list.Add(this.CurrBlock);
        }

        public void BeginClass(int class_id, ModifierList ml)
        {
            int level = this.symbol_table[class_id].Level;
            this.symbol_table[class_id].Kind = MemberKind.Type;
            this.level_stack.Push(class_id);
            this.Gen(this.code.OP_CREATE_CLASS, class_id, level, 1);
            this.Gen(this.code.OP_ADD_MODIFIER, class_id, 7, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, class_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_BEGIN_USING, class_id, 0, 0);
        }

        public void BeginDelegate(int delegate_id, ModifierList ml)
        {
            int level = this.symbol_table[delegate_id].Level;
            this.symbol_table[delegate_id].Kind = MemberKind.Type;
            this.level_stack.Push(delegate_id);
            this.Gen(this.code.OP_CREATE_CLASS, delegate_id, level, 6);
            this.Gen(this.code.OP_ADD_MODIFIER, delegate_id, 7, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, delegate_id, 6, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, delegate_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_BEGIN_USING, delegate_id, 0, 0);
        }

        public void BeginEnum(int enum_id, ModifierList ml, int type_base)
        {
            int level = this.symbol_table[enum_id].Level;
            this.symbol_table[enum_id].Kind = MemberKind.Type;
            this.level_stack.Push(enum_id);
            this.Gen(this.code.OP_CREATE_CLASS, enum_id, level, 4);
            this.Gen(this.code.OP_ADD_MODIFIER, enum_id, 6, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, enum_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_BEGIN_USING, enum_id, 0, 0);
        }

        public void BeginEvent(int event_id, ModifierList ml, int type_id, int param_count)
        {
            int level = this.symbol_table[event_id].Level;
            this.symbol_table[event_id].Kind = MemberKind.Event;
            this.Gen(this.code.OP_CREATE_EVENT, event_id, level, param_count);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, event_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_ASSIGN_TYPE, event_id, type_id, 0);
        }

        public void BeginField(int field_id, ModifierList ml, int type_id)
        {
            int level = this.symbol_table[field_id].Level;
            this.symbol_table[field_id].Kind = MemberKind.Field;
            this.Gen(this.code.OP_CREATE_FIELD, field_id, level, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, field_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_ASSIGN_TYPE, field_id, type_id, 0);
        }

        public void BeginInterface(int interface_id, ModifierList ml)
        {
            int level = this.symbol_table[interface_id].Level;
            this.symbol_table[interface_id].Kind = MemberKind.Type;
            this.level_stack.Push(interface_id);
            this.Gen(this.code.OP_CREATE_CLASS, interface_id, level, 3);
            this.Gen(this.code.OP_ADD_MODIFIER, interface_id, 7, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, interface_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_BEGIN_USING, interface_id, 0, 0);
        }

        public virtual int BeginMethod(int sub_id, MemberKind k, ModifierList ml, int res_type_id)
        {
            int level = this.symbol_table[sub_id].Level;
            this.symbol_table[sub_id].Kind = k;
            this.NewLabel();
            this.level_stack.Push(sub_id);
            int num2 = this.NewVar();
            this.Gen(this.code.OP_ASSIGN_TYPE, num2, res_type_id, 0);
            this.Gen(this.code.OP_ASSIGN_TYPE, sub_id, res_type_id, 0);
            int num3 = this.NewVar();
            if (!ml.HasModifier(Modifier.Static))
            {
                this.symbol_table[num3].TypeId = level;
                this.symbol_table[num3].Name = "this";
            }
            this.Gen(this.code.OP_CREATE_METHOD, sub_id, level, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, sub_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_ASSIGN_TYPE, sub_id, res_type_id, 0);
            this.BeginBlock();
            return sub_id;
        }

        public void BeginNamespace(int namespace_id)
        {
            int level = this.symbol_table[namespace_id].Level;
            this.symbol_table[namespace_id].Kind = MemberKind.Type;
            this.level_stack.Push(namespace_id);
            this.Gen(this.code.OP_CREATE_NAMESPACE, namespace_id, level, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, namespace_id, 7, 0);
            this.Gen(this.code.OP_BEGIN_USING, namespace_id, 0, 0);
        }

        public void BeginProperty(int property_id, ModifierList ml, int type_id, int param_count)
        {
            int level = this.symbol_table[property_id].Level;
            this.symbol_table[property_id].Kind = MemberKind.Property;
            this.Gen(this.code.OP_CREATE_PROPERTY, property_id, level, param_count);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, property_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_ASSIGN_TYPE, property_id, type_id, 0);
        }

        public void BeginStruct(int struct_id, ModifierList ml)
        {
            int level = this.symbol_table[struct_id].Level;
            this.symbol_table[struct_id].Kind = MemberKind.Type;
            this.level_stack.Push(struct_id);
            this.Gen(this.code.OP_CREATE_CLASS, struct_id, level, 2);
            this.Gen(this.code.OP_ADD_MODIFIER, struct_id, 7, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, struct_id, 6, 0);
            for (int i = 0; i < ml.Count; i++)
            {
                this.Gen(this.code.OP_ADD_MODIFIER, struct_id, (int) ml[i], 0);
            }
            this.Gen(this.code.OP_BEGIN_USING, struct_id, 0, 0);
        }

        public void BeginSubrange(int type_id, StandardType type_base)
        {
            int level = this.symbol_table[type_id].Level;
            this.symbol_table[type_id].Kind = MemberKind.Type;
            this.level_stack.Push(type_id);
            this.Gen(this.code.OP_CREATE_CLASS, type_id, level, 7);
            this.Gen(this.code.OP_ADD_ANCESTOR, type_id, (int) type_base, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, type_id, 7, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, type_id, 6, 0);
            this.Gen(this.code.OP_ADD_MODIFIER, type_id, 1, 0);
            this.Gen(this.code.OP_BEGIN_USING, type_id, 0, 0);
        }

        public int BinOp(int op, int arg1, int arg2)
        {
            int res = this.NewVar();
            this.Gen(op, arg1, arg2, res);
            return res;
        }

        public virtual void Call_SCANNER()
        {
            int block;
            bool flag;
            this.curr_token = this.scanner.ReadToken();
            this.curr_token.atext = this.curr_token.Text;
            switch (this.curr_token.tokenClass)
            {
                case TokenClass.Identifier:
                    if (!this.IsKeyword(this.curr_token.Text))
                    {
                        if (this.REF_SWITCH)
                        {
                            this.curr_token.id = this.NewRef(this.curr_token.Text);
                            this.REF_SWITCH = false;
                            return;
                        }
                        if (!this.DECLARE_SWITCH)
                        {
                            this.curr_token.id = this.LookupID(this.curr_token.Text);
                            if (this.curr_token.id != 0)
                            {
                                int level = this.symbol_table[this.curr_token.id].Level;
                                if (this.symbol_table[level].Kind == MemberKind.Type)
                                {
                                    if (this.CurrLevel != 0)
                                    {
                                        this.curr_token.id = 0;
                                    }
                                }
                                else if (!this.IsDeclaredLocalVar(this.curr_token.id, this.CurrLevel))
                                {
                                    this.curr_token.id = 0;
                                }
                            }
                            if (this.curr_token.id == 0)
                            {
                                this.curr_token.id = this.symbol_table.AppVar();
                                SymbolRec rec2 = this.symbol_table[this.curr_token.id];
                                rec2.Name = this.curr_token.Text;
                                rec2.Level = this.CurrLevel;
                                rec2.Block = this.CurrBlock;
                                rec2.Kind = MemberKind.Var;
                                this.Gen(this.code.OP_EVAL, 0, 0, this.curr_token.id);
                            }
                            return;
                        }
                        if (!this.DECLARATION_CHECK_SWITCH)
                        {
                            goto Label_0234;
                        }
                        int iD = this.LookupID(this.curr_token.Text);
                        block = this.symbol_table[iD].Block;
                        if (((iD == 0) || (block == 0)) || (this.symbol_table[iD].Level != this.CurrLevel))
                        {
                            goto Label_0234;
                        }
                        flag = false;
                        for (int i = this.block_list.Count - 1; i >= 0; i--)
                        {
                            int num4 = this.block_list[i];
                            if ((num4 < block) && (num4 < this.CurrBlock))
                            {
                                flag = true;
                                break;
                            }
                            if (num4 == block)
                            {
                                break;
                            }
                        }
                        break;
                    }
                    if (!this.REF_SWITCH || !this.AllowKeywordsInMemberAccessExpressions)
                    {
                        this.curr_token.id = 0;
                        this.curr_token.tokenClass = TokenClass.Keyword;
                        return;
                    }
                    this.curr_token.id = this.NewRef(this.curr_token.Text);
                    this.REF_SWITCH = false;
                    return;

                case TokenClass.Keyword:
                case TokenClass.BooleanConst:
                    return;

                case TokenClass.IntegerConst:
                {
                    ulong num6;
                    string text = this.curr_token.Text;
                    if ((CSLite_System.PosCh('u', text) < 0) && (CSLite_System.PosCh('U', text) < 0))
                    {
                        if ((CSLite_System.PosCh('l', text) >= 0) || (CSLite_System.PosCh('L', text) >= 0))
                        {
                            if ((CSLite_System.PosCh('u', text) >= 0) || (CSLite_System.PosCh('U', text) >= 0))
                            {
                                ulong num8;
                                if ((CSLite_System.PosCh('x', text) >= 0) || (CSLite_System.PosCh('X', text) >= 0))
                                {
                                    text = text.Substring(2);
                                    num8 = ulong.Parse(text.Substring(0, text.Length - 2), NumberStyles.AllowHexSpecifier);
                                }
                                else
                                {
                                    num8 = ulong.Parse(text.Substring(0, text.Length - 2));
                                }
                                this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num8, StandardType.Ulong);
                            }
                            else
                            {
                                long num9;
                                if ((CSLite_System.PosCh('x', text) >= 0) || (CSLite_System.PosCh('X', text) >= 0))
                                {
                                    text = text.Substring(2);
                                    num9 = long.Parse(text.Substring(0, text.Length - 1), NumberStyles.AllowHexSpecifier);
                                }
                                else
                                {
                                    num9 = long.Parse(text.Substring(0, text.Length - 1));
                                }
                                this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num9, StandardType.Long);
                            }
                        }
                        else
                        {
                            ulong num10;
                            if ((CSLite_System.PosCh('x', text) >= 0) || (CSLite_System.PosCh('X', text) >= 0))
                            {
                                num10 = ulong.Parse(text.Substring(2), NumberStyles.AllowHexSpecifier);
                            }
                            else
                            {
                                num10 = ulong.Parse(text);
                            }
                            if (num10 <= 0x7fffffffL)
                            {
                                this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, (int) num10, StandardType.Int);
                            }
                            else if (num10 <= 0xffffffffL)
                            {
                                this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, (uint) num10, StandardType.Uint);
                            }
                            else if (num10 <= 0x7fffffffffffffffL)
                            {
                                this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, (long) num10, StandardType.Long);
                            }
                            else
                            {
                                this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num10, StandardType.Ulong);
                            }
                        }
                        return;
                    }
                    if ((CSLite_System.PosCh('l', text) < 0) && (CSLite_System.PosCh('L', text) < 0))
                    {
                        uint num7;
                        if ((CSLite_System.PosCh('x', text) >= 0) || (CSLite_System.PosCh('X', text) >= 0))
                        {
                            text = text.Substring(2);
                            num7 = uint.Parse(text.Substring(0, text.Length - 1), NumberStyles.AllowHexSpecifier);
                        }
                        else
                        {
                            num7 = uint.Parse(text.Substring(0, text.Length - 1));
                        }
                        this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num7, StandardType.Uint);
                        return;
                    }
                    if ((CSLite_System.PosCh('x', text) < 0) && (CSLite_System.PosCh('X', text) < 0))
                    {
                        num6 = ulong.Parse(text.Substring(0, text.Length - 2));
                    }
                    else
                    {
                        text = text.Substring(2);
                        num6 = ulong.Parse(text.Substring(0, text.Length - 2), NumberStyles.AllowHexSpecifier);
                    }
                    this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num6, StandardType.Ulong);
                    return;
                }
                case TokenClass.StringConst:
                {
                    if ((CSLite_System.PosCh('@', this.curr_token.Text) == -1) || !(this.language != "VB"))
                    {
                        string str5 = this.scanner.ParseString(this.curr_token.Text);
                        this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, str5.Substring(1, str5.Length - 2), StandardType.String);
                        return;
                    }
                    string str4 = this.scanner.ParseVerbatimString(this.curr_token.Text);
                    this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, str4.Substring(1, str4.Length - 2), StandardType.String);
                    return;
                }
                case TokenClass.CharacterConst:
                {
                    char ch = this.scanner.ParseString(this.curr_token.Text)[1];
                    this.curr_token.id = this.symbol_table.AppCharacterConst(ch);
                    return;
                }
                case TokenClass.RealConst:
                {
                    string s = this.curr_token.Text;
                    if ((CSLite_System.PosCh(this.SingleCharacter, s) < 0) && (CSLite_System.PosCh(this.UpSingleCharacter, s) < 0))
                    {
                        if ((CSLite_System.PosCh(this.DoubleCharacter, s) >= 0) || (CSLite_System.PosCh(this.UpDoubleCharacter, s) >= 0))
                        {
                            s = s.Substring(0, s.Length - 1);
                            if (this.scanner.DecimalSeparator == ",")
                            {
                                s = s.Replace('.', ',');
                            }
                            double num12 = double.Parse(s, NumberStyles.Any);
                            this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num12, StandardType.Double);
                        }
                        else if ((CSLite_System.PosCh(this.DecimalCharacter, s) >= 0) || (CSLite_System.PosCh(this.UpDecimalCharacter, s) >= 0))
                        {
                            s = s.Substring(0, s.Length - 1);
                            if (this.scanner.DecimalSeparator == ",")
                            {
                                s = s.Replace('.', ',');
                            }
                            decimal num13 = decimal.Parse(s, NumberStyles.Any);
                            this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num13, StandardType.Decimal);
                        }
                        else
                        {
                            if (this.scanner.DecimalSeparator == ",")
                            {
                                s = s.Replace('.', ',');
                            }
                            double num14 = double.Parse(s);
                            this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num14, StandardType.Double);
                        }
                        return;
                    }
                    s = s.Substring(0, s.Length - 1);
                    if (this.scanner.DecimalSeparator == ",")
                    {
                        s = s.Replace('.', ',');
                    }
                    float num11 = float.Parse(s, NumberStyles.Any);
                    this.curr_token.id = this.symbol_table.AppConst(this.curr_token.Text, num11, StandardType.Float);
                    return;
                }
                default:
                    return;
            }
            if (!flag)
            {
                if (this.CurrBlock == block)
                {
                    this.RaiseErrorEx(false, "CS0128. A local variable named '{0}' is already defined in this scope.", new object[] { this.curr_token.Text });
                }
                else if (this.CurrBlock > block)
                {
                    this.RaiseErrorEx(false, "CS0136. A local variable named '{0}' cannot be declared in this scope because it would give a different meaning to '{0}', which is already used in a '{1}' scope to denote something else", new object[] { this.curr_token.Text, "parent or current" });
                }
                else
                {
                    this.RaiseErrorEx(false, "CS0136. A local variable named '{0}' cannot be declared in this scope because it would give a different meaning to '{0}', which is already used in a '{1}' scope to denote something else", new object[] { this.curr_token.Text, "child" });
                }
            }
        Label_0234:
            this.curr_token.id = this.symbol_table.AppVar();
            SymbolRec rec = this.symbol_table[this.curr_token.id];
            rec.Name = this.curr_token.Text;
            rec.Level = this.CurrLevel;
            rec.Block = this.CurrBlock;
        }

        internal void CheckModifiers(ModifierList ml, ModifierList true_list)
        {
            for (int i = 0; i < ml.Count; i++)
            {
                Modifier m = ml[i];
                if (!true_list.HasModifier(m))
                {
                    this.RaiseErrorEx(false, "CS0106. The modifier '{0}' is not valid for this item.", new object[] { m.ToString() });
                }
            }
        }

        public bool ConditionalDirectivesAreCompleted()
        {
            return this.scanner.ConditionalDirectivesAreCompleted();
        }

        public bool CondMatch(char c)
        {
            if (c == this.curr_token.Char)
            {
                this.Call_SCANNER();
                return true;
            }
            return false;
        }

        public void DiscardInstruction(int op, int arg1, int arg2, int res)
        {
            for (int i = this.code.Card; i >= 1; i--)
            {
                if ((((this.code[i].op == op) && ((this.code[i].arg1 == arg1) || (arg1 == -1))) && ((this.code[i].arg2 == arg2) || (arg2 == -1))) && ((this.code[i].res == res) || (res == -1)))
                {
                    this.code[i].op = this.code.OP_NOP;
                    break;
                }
            }
        }

        public void EndArray(int array_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, array_id, 0, 0);
            this.Gen(this.code.OP_END_CLASS, array_id, 0, 0);
        }

        public void EndBlock()
        {
            this.block_stack.Pop();
            this.block_list.Add(this.CurrBlock);
        }

        public void EndClass(int class_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, class_id, 0, 0);
            this.Gen(this.code.OP_END_CLASS, class_id, 0, 0);
        }

        public void EndDelegate(int delegate_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, delegate_id, 0, 0);
            this.Gen(this.code.OP_END_CLASS, delegate_id, 0, 0);
        }

        public void EndEnum(int enum_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, enum_id, 0, 0);
            this.Gen(this.code.OP_END_CLASS, enum_id, 0, 0);
        }

        public void EndEvent(int event_id)
        {
        }

        public void EndField(int field_id)
        {
        }

        public void EndInterface(int interface_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, interface_id, 0, 0);
            this.Gen(this.code.OP_END_CLASS, interface_id, 0, 0);
        }

        public virtual void EndMethod(int sub_id)
        {
            this.Gen(this.code.OP_END_METHOD, sub_id, this.symbol_table.Card, 0);
            int l = sub_id + 1;
            this.Gen(this.code.OP_RET, sub_id, 0, 0);
            this.level_stack.Pop();
            this.SetLabelHere(l);
            this.EndBlock();
        }

        public void EndNamespace(int namespace_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, namespace_id, 0, 0);
        }

        public void EndProperty(int property_id)
        {
        }

        public void EndStruct(int struct_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, struct_id, 0, 0);
            this.Gen(this.code.OP_END_CLASS, struct_id, 0, 0);
        }

        public void EndSubrange(int type_id)
        {
            this.level_stack.Pop();
            this.Gen(this.code.OP_END_USING, type_id, 0, 0);
            this.Gen(this.code.OP_END_CLASS, type_id, 0, 0);
        }

        public virtual void Gen(int op, int arg1, int arg2, int res)
        {
            if ((op == this.code.OP_CREATE_REFERENCE) || (op == this.code.OP_EVAL))
            {
                this.code.Card++;
                this.code.SetInstruction(this.code.Card, this.code.OP_NOP, 0, 0, 0);
            }
            this.code.Card++;
            this.code.SetInstruction(this.code.Card, op, arg1, arg2, res);
            if (op == this.code.OP_ASSIGN)
            {
                this.code.Card++;
                this.code.SetInstruction(this.code.Card, this.code.OP_NOP, 0, 0, 0);
                this.code.Card++;
                this.code.SetInstruction(this.code.Card, this.code.OP_NOP, 0, 0, 0);
            }
            else if (op == this.code.OP_ADD_PARAM)
            {
                SymbolRec rec1 = this.symbol_table[arg2];
                rec1.Count++;
            }
        }

        public void GenAt(int n, int op, int arg1, int arg2, int res)
        {
            this.code.SetInstruction(n, op, arg1, arg2, res);
        }

        public void GenDefine(string name)
        {
            int num = this.symbol_table.AppConst(name, name, StandardType.String);
            this.Gen(this.code.OP_DEFINE, num, 0, 0);
        }

        public void GenEndRegion(string name)
        {
            int num = this.symbol_table.AppConst(name, name, StandardType.String);
            this.Gen(this.code.OP_END_REGION, num, 0, 0);
        }

        public void GenSeparator()
        {
            ProgRec rec = this.code[this.code.Card];
            if ((rec.arg2 != this.scanner.LineNumber) || (rec.op != this.code.OP_SEPARATOR))
            {
                this.Gen(this.code.OP_SEPARATOR, this.curr_module, this.scanner.LineNumber, 0);
            }
        }

        public void GenStartRegion(string name)
        {
            int num = this.symbol_table.AppConst(name, name, StandardType.String);
            this.Gen(this.code.OP_START_REGION, num, 0, 0);
        }

        public int GenTypeRef(int type_id)
        {
            int id = this.NewVar();
            this.SetName(id, this.GetName(type_id) + "&");
            this.symbol_table[id].Level = this.symbol_table[type_id].Level;
            this.Gen(this.code.OP_CREATE_REF_TYPE, type_id, 0, id);
            return id;
        }

        public void GenUndef(string name)
        {
            int num = this.symbol_table.AppConst(name, name, StandardType.String);
            this.Gen(this.code.OP_UNDEF, num, 0, 0);
        }

        public MemberKind GetKind(int id)
        {
            return this.symbol_table[id].Kind;
        }

        public int GetLevel(int id)
        {
            return this.symbol_table[id].Level;
        }

        public string GetName(int id)
        {
            return this.symbol_table[id].Name;
        }

        public int GetResultId(int sub_id)
        {
            return this.symbol_table.GetResultId(sub_id);
        }

        private SymbolRec GetSymbolRec(int id)
        {
            return this.symbol_table[id];
        }

        public int GetThisId(int sub_id)
        {
            return this.symbol_table.GetThisId(sub_id);
        }

        public int GetTypeId(int id)
        {
            return this.symbol_table[id].TypeId;
        }

        public object GetVal(int id)
        {
            return this.symbol_table[id].Val;
        }

        internal virtual void Init(BaseScripter scripter, Module m)
        {
            this.scripter = scripter;
            this.code = scripter.code;
            this.symbol_table = scripter.symbol_table;
            this.scanner.Init(scripter, m.Text);
            this.temp_count = 0;
            this.curr_module = m.NameIndex;
            this.level_stack.Clear();
            this.level_stack.Push(0);
            this.level_stack.Push(this.RootNamespaceId);
            this.block_count = 0;
            this.block_stack.Clear();
            this.block_stack.Push(0);
            this.block_list.Clear();
            this.DECLARE_SWITCH = false;
            this.DECLARATION_CHECK_SWITCH = false;
        }

        internal void InitExpression(BaseScripter scripter, Module m, int sub_id, string expr)
        {
            this.scripter = scripter;
            this.code = scripter.code;
            this.symbol_table = scripter.symbol_table;
            this.scanner.Init(scripter, expr);
            this.temp_count = 0;
            this.curr_module = m.NameIndex;
            this.level_stack.Clear();
            this.level_stack.Push(0);
            this.level_stack.Push(sub_id);
            this.block_count = 0;
            this.block_stack.Clear();
            this.block_stack.Push(0);
            this.block_list.Clear();
            this.DECLARE_SWITCH = false;
        }

        public virtual void InitMethod(int sub_id)
        {
            int num = sub_id + 1;
            this.Gen(this.code.OP_INIT_METHOD, sub_id, 0, 0);
            this.Gen(this.code.OP_GO, num, 0, 0);
            this.Gen(this.code.OP_LABEL, 0, 0, 0);
        }

        public bool IsConstant()
        {
            return ((((this.curr_token.tokenClass == TokenClass.CharacterConst) || (this.curr_token.tokenClass == TokenClass.IntegerConst)) || ((this.curr_token.tokenClass == TokenClass.RealConst) || (this.curr_token.tokenClass == TokenClass.BooleanConst))) || (this.curr_token.tokenClass == TokenClass.StringConst));
        }

        public bool IsCurrText(char c)
        {
            return ((this.curr_token.Char == c) && (this.curr_token.Len == 1));
        }

        public bool IsCurrText(string s)
        {
            if (this.upcase)
            {
                return (this.curr_token.Text.ToUpper() == s.ToUpper());
            }
            return ((s[0] == this.curr_token.Char) && (this.curr_token.Text == s));
        }

        private bool IsDeclaredLocalVar(int id, int sub_id)
        {
            for (int i = this.code.Card; i >= 1; i--)
            {
                if (this.code[i].op == this.code.OP_DECLARE_LOCAL_VARIABLE)
                {
                    if ((this.code[i].arg1 == id) && (this.code[i].arg2 == sub_id))
                    {
                        return true;
                    }
                }
                else if (this.code[i].op == this.code.OP_DECLARE_LOCAL_VARIABLE_RUNTIME)
                {
                    if ((this.code[i].arg1 == id) && (this.code[i].arg2 == sub_id))
                    {
                        return true;
                    }
                }
                else if ((this.code[i].op == this.code.OP_ADD_PARAM) || (this.code[i].op == this.code.OP_ADD_PARAMS))
                {
                    if ((this.code[i].arg2 == id) && (this.code[i].arg1 == sub_id))
                    {
                        return true;
                    }
                }
                else if (this.code[i].op == this.code.OP_DECLARE_LOCAL_SIMPLE)
                {
                    if (this.code[i].arg1 == id)
                    {
                        return true;
                    }
                }
                else if ((this.code[i].op == this.code.OP_CREATE_METHOD) && (this.code[i].arg1 == sub_id))
                {
                    return false;
                }
            }
            return false;
        }

        public bool IsEOF()
        {
            return this.scanner.IsEOF();
        }

        public bool IsIdentifier()
        {
            return (this.curr_token.tokenClass == TokenClass.Identifier);
        }

        public virtual bool IsKeyword(string s)
        {
            if (this.upcase)
            {
                string str = s.ToUpper();
                for (int i = 0; i < this.keywords.Count; i++)
                {
                    if (this.keywords[i].ToUpper() == str)
                    {
                        return true;
                    }
                }
                return false;
            }
            return (this.keywords.IndexOf(s) != -1);
        }

        public bool IsOperator(IntegerList oper_list, out int op)
        {
            op = 0;
            int index = oper_list.IndexOf(this.curr_token.id);
            if (index >= 0)
            {
                op = oper_list[index];
                this.Call_SCANNER();
                return true;
            }
            return false;
        }

        public bool IsStaticLocalVar(int id)
        {
            return this.symbol_table[id].is_static;
        }

        public int LookupID(string s)
        {
            return this.symbol_table.LookupIDLocal(s, this.CurrLevel, this.upcase);
        }

        public int LookupTypeID(string s)
        {
            return this.symbol_table.LookupTypeByName(s, this.upcase);
        }

        public virtual void Match(char c)
        {
            if (c != this.curr_token.Char)
            {
                this.RaiseErrorEx(true, "'{0}' expected but '{1}' found.", new object[] { c.ToString(), this.curr_token.Text });
            }
            this.Call_SCANNER();
        }

        public virtual void Match(string s)
        {
            if (this.upcase)
            {
                if (s.ToUpper() != this.curr_token.Text.ToUpper())
                {
                    this.RaiseErrorEx(true, "'{0}' expected but '{1}' found.", new object[] { s, this.curr_token.Text });
                }
            }
            else if (s != this.curr_token.Text)
            {
                this.RaiseErrorEx(true, "'{0}' expected but '{1}' found.", new object[] { s, this.curr_token.Text });
            }
            this.Call_SCANNER();
        }

        public void MoveSeparator()
        {
            int card = this.code.Card;
            while (this.code[card].op != this.code.OP_SEPARATOR)
            {
                card--;
            }
            this.code.Card++;
            this.code[this.code.Card].op = this.code.OP_SEPARATOR;
            this.code[this.code.Card].arg1 = this.code[card].arg1;
            this.code[this.code.Card].arg2 = this.code[card].arg2;
            this.code[this.code.Card].res = this.code[card].res;
            this.code[card].op = this.code.OP_NOP;
        }

        public int NewConst(object v)
        {
            int num = this.symbol_table.AppVar();
            this.symbol_table[num].Level = 0;
            this.symbol_table[num].Name = v.ToString();
            this.symbol_table[num].Value = v;
            this.symbol_table[num].Kind = MemberKind.Const;
            Type type = v.GetType();
            if (type == typeof(bool))
            {
                this.symbol_table[num].TypeId = 2;
                return num;
            }
            if (type == typeof(byte))
            {
                this.symbol_table[num].TypeId = 3;
                return num;
            }
            if (type == typeof(char))
            {
                this.symbol_table[num].TypeId = 4;
                return num;
            }
            if (type == typeof(decimal))
            {
                this.symbol_table[num].TypeId = 5;
                return num;
            }
            if (type == typeof(double))
            {
                this.symbol_table[num].TypeId = 6;
                return num;
            }
            if (type == typeof(float))
            {
                this.symbol_table[num].TypeId = 7;
                return num;
            }
            if (type == typeof(int))
            {
                this.symbol_table[num].TypeId = 8;
                return num;
            }
            if (type == typeof(long))
            {
                this.symbol_table[num].TypeId = 9;
                return num;
            }
            if (type == typeof(sbyte))
            {
                this.symbol_table[num].TypeId = 10;
                return num;
            }
            if (type == typeof(short))
            {
                this.symbol_table[num].TypeId = 11;
                return num;
            }
            if (type == typeof(string))
            {
                this.symbol_table[num].TypeId = 12;
                return num;
            }
            if (type == typeof(uint))
            {
                this.symbol_table[num].TypeId = 13;
                return num;
            }
            if (type == typeof(ulong))
            {
                this.symbol_table[num].TypeId = 14;
                return num;
            }
            if (type == typeof(ushort))
            {
                this.symbol_table[num].TypeId = 15;
                return num;
            }
            this.symbol_table[num].TypeId = 0x10;
            return num;
        }

        public int NewLabel()
        {
            return this.symbol_table.AppLabel();
        }

        public int NewRef(string name)
        {
            int num = this.symbol_table.AppVar();
            this.symbol_table[num].Name = name;
            this.symbol_table[num].Level = this.CurrLevel;
            this.symbol_table[num].Kind = MemberKind.Ref;
            return num;
        }

        public int NewVar()
        {
            int num = this.symbol_table.AppVar();
            this.symbol_table[num].Level = this.CurrLevel;
            this.symbol_table[num].Block = this.CurrBlock;
            this.temp_count++;
            this.symbol_table[num].Name = "$$" + this.temp_count.ToString();
            return num;
        }

        public int NewVar(object v)
        {
            int num = this.symbol_table.AppVar();
            this.symbol_table[num].Level = 0;
            this.symbol_table[num].Value = v;
            this.symbol_table[num].Kind = MemberKind.Var;
            if (v.GetType() == 1.GetType())
            {
                this.symbol_table[num].TypeId = 8;
            }
            else if (v.GetType() == "".GetType())
            {
                this.symbol_table[num].TypeId = 12;
            }
            else if (v.GetType() == 0.0.GetType())
            {
                this.symbol_table[num].TypeId = 6;
            }
            this.temp_count++;
            this.symbol_table[num].Name = "$$" + this.temp_count.ToString();
            return num;
        }

        public virtual int Parse_BooleanLiteral()
        {
            if (this.IsCurrText("true"))
            {
                this.Match("true");
                return this.TRUE_id;
            }
            this.Match("false");
            return this.FALSE_id;
        }

        public virtual int Parse_CharacterLiteral()
        {
            int id = this.curr_token.id;
            this.Call_SCANNER();
            return id;
        }

        public virtual int Parse_Expression()
        {
            return this.Parse_Ident();
        }

        public virtual int Parse_Ident()
        {
            if (((this.curr_token.tokenClass != TokenClass.Identifier) && (this.curr_token.id == 0)) && (this.curr_token.Text != "void"))
            {
                this.RaiseError(true, "CS1001. Identifier expected.");
            }
            int id = this.curr_token.id;
            this.Call_SCANNER();
            return id;
        }

        public virtual int Parse_IntegerLiteral()
        {
            int id = this.curr_token.id;
            this.Call_SCANNER();
            return id;
        }

        public int Parse_NewLabel()
        {
            int num = this.Parse_Ident();
            this.symbol_table[num].Kind = MemberKind.Label;
            return num;
        }

        public void Parse_PrintlnStmt()
        {
            this.Parse_PrintStmt();
            this.Gen(this.code.OP_PRINT, this.symbol_table.BR_id, 0, 0);
        }

        public void Parse_PrintStmt()
        {
            this.Call_SCANNER();
        Label_0008:
            this.Gen(this.code.OP_PRINT, this.Parse_Expression(), 0, 0);
            if (this.IsCurrText(","))
            {
                this.Call_SCANNER();
                goto Label_0008;
            }
            this.Match(";");
        }

        public virtual void Parse_Program()
        {
        }

        public virtual int Parse_RealLiteral()
        {
            int id = this.curr_token.id;
            this.Call_SCANNER();
            return id;
        }

        public virtual int Parse_StringLiteral()
        {
            int id = this.curr_token.id;
            this.Call_SCANNER();
            return id;
        }

        public void PutKind(int id, MemberKind value)
        {
            this.symbol_table[id].Kind = value;
        }

        public void PutLevel(int id, int value)
        {
            this.symbol_table[id].Level = value;
        }

        public void PutVal(int id, object value)
        {
            this.symbol_table[id].Val = value;
        }

        public void RaiseError(bool fatal, string message)
        {
            this.scripter.Dump();
            this.scripter.CreateErrorObject(message);
            if (fatal)
            {
                this.code.n = this.code.Card;
                this.scripter.RaiseException(message);
            }
        }

        public void RaiseErrorEx(bool fatal, string message, params object[] p)
        {
            this.RaiseError(fatal, string.Format(message, p));
        }

        public int ReadToken()
        {
            int num = 0;
            do
            {
                num++;
                this.curr_token = this.scanner.ReadToken();
            }
            while (this.curr_token.tokenClass == TokenClass.Separator);
            this.curr_token.atext = this.curr_token.Text;
            return num;
        }

        public void ReplaceForwardDeclaration(int id)
        {
            int forwardDeclaration = this.symbol_table.LookupForwardDeclaration(id, this.upcase);
            if (forwardDeclaration == 0)
            {
                return;
            }
            int typeId = this.GetSymbolRec(this.symbol_table.GetThisId(forwardDeclaration)).TypeId;
            int num3 = 1;
            while (num3 <= this.code.Card)
            {
                if ((this.code[num3].op == this.code.OP_CREATE_METHOD) && (forwardDeclaration == this.code[num3].arg1))
                {
                    break;
                }
                num3++;
            }
            bool flag = false;
            num3--;
        Label_0088:
            num3++;
            if (((this.code[num3].op == this.code.OP_ADD_MODIFIER) && (forwardDeclaration == this.code[num3].arg1)) && (this.code[num3].arg2 == 7))
            {
                flag = true;
            }
            if ((this.code[num3].op == this.code.OP_END_METHOD) && (forwardDeclaration == this.code[num3].arg1))
            {
                this.code[num3].op = this.code.OP_NOP;
            }
            else
            {
                if (this.code[num3].op != this.code.OP_SEPARATOR)
                {
                    this.code[num3].op = this.code.OP_NOP;
                }
                goto Label_0088;
            }
            this.code[num3 + 1].op = this.code.OP_NOP;
            this.symbol_table[forwardDeclaration].Kind = MemberKind.None;
            this.symbol_table[forwardDeclaration].Name = "";
            this.symbol_table[forwardDeclaration + 1].Kind = MemberKind.None;
            this.symbol_table[forwardDeclaration + 1].Name = "";
            this.symbol_table[forwardDeclaration + 2].Kind = MemberKind.None;
            this.symbol_table[forwardDeclaration + 2].Name = "";
            this.symbol_table[forwardDeclaration + 3].Kind = MemberKind.None;
            this.symbol_table[forwardDeclaration + 3].Name = "";
            this.code.ReplaceId(forwardDeclaration, id);
            if (!flag)
            {
                for (num3 = this.code.Card; num3 >= 1; num3--)
                {
                    if (((this.code[num3].op == this.code.OP_ADD_MODIFIER) && (id == this.code[num3].arg1)) && (this.code[num3].arg2 == 7))
                    {
                        this.code[num3].op = this.code.OP_NOP;
                        break;
                    }
                }
                this.GetSymbolRec(this.symbol_table.GetThisId(id)).TypeId = typeId;
                this.GetSymbolRec(this.symbol_table.GetThisId(id)).Name = "this";
            }
            this.Gen(this.code.OP_ADD_MODIFIER, id, 1, 0);
        }

        public void SetForward(int sub_id, bool value)
        {
            this.symbol_table[sub_id].IsForward = value;
        }

        public void SetLabelHere(int l)
        {
            if (this.code[this.code.Card].op == this.code.OP_EVAL)
            {
                this.symbol_table.SetLabel(l, this.code.Card);
                int card = this.code.Card;
                int res = this.code[card].res;
                this.code[card].op = this.code.OP_LABEL;
                this.code.Card++;
                card = this.code.Card;
                this.code[card].op = this.code.OP_EVAL;
                this.code[card].res = res;
            }
            else
            {
                this.Gen(this.code.OP_LABEL, 0, 0, 0);
                this.symbol_table.SetLabel(l, this.code.Card);
            }
        }

        public void SetName(int id, string value)
        {
            this.symbol_table[id].Name = value;
        }

        public void SetStaticLocalVar(int id)
        {
            this.symbol_table[id].is_static = true;
        }

        public void SetTypeId(int id, int value)
        {
            this.symbol_table[id].TypeId = value;
        }

        public void SetUpcase(bool value)
        {
            if (value)
            {
                this.code.SetUpcase(this.code.Card, Upcase.Yes);
            }
            else
            {
                this.code.SetUpcase(this.code.Card, Upcase.No);
            }
        }

        public int UnaryOp(int op, int arg1)
        {
            int res = this.NewVar();
            this.Gen(op, arg1, 0, res);
            return res;
        }

        public int ArrayOfObjectClassId
        {
            get
            {
                return this.symbol_table.ARRAY_OF_OBJECT_CLASS_id;
            }
        }

        public int BR_id
        {
            get
            {
                return this.symbol_table.BR_id;
            }
        }

        public int CodeCard
        {
            get
            {
                return this.code.Card;
            }
            set
            {
                this.code.Card = value;
            }
        }

        public int CurrBlock
        {
            get
            {
                return this.block_stack.Peek();
            }
        }

        public int CurrClassID
        {
            get
            {
                for (int i = this.level_stack.Count - 1; i >= 0; i--)
                {
                    int id = this.level_stack[i];
                    if (this.GetKind(id) == MemberKind.Type)
                    {
                        return id;
                    }
                }
                return 0;
            }
        }

        public int CurrLevel
        {
            get
            {
                return this.level_stack.Peek();
            }
        }

        public int CurrResultId
        {
            get
            {
                return this.symbol_table.GetResultId(this.CurrSubId);
            }
        }

        public int CurrSubId
        {
            get
            {
                for (int i = this.level_stack.Count - 1; i >= 0; i--)
                {
                    int id = this.level_stack[i];
                    if (this.GetKind(id) != MemberKind.Type)
                    {
                        return id;
                    }
                }
                return 0;
            }
        }

        public int CurrThisID
        {
            get
            {
                for (int i = this.level_stack.Count - 1; i >= 0; i--)
                {
                    int id = this.level_stack[i];
                    switch (this.GetKind(id))
                    {
                        case MemberKind.Method:
                        case MemberKind.Constructor:
                        case MemberKind.Destructor:
                            return (id + 3);
                    }
                }
                return 0;
            }
        }

        public int DATETIME_CLASS_id
        {
            get
            {
                return this.symbol_table.DATETIME_CLASS_id;
            }
        }

        public char DecimalCharacter
        {
            get
            {
                return this.scanner.DecimalCharacter;
            }
        }

        public char DoubleCharacter
        {
            get
            {
                return this.scanner.DoubleCharacter;
            }
        }

        public int FALSE_id
        {
            get
            {
                return this.symbol_table.FALSE_id;
            }
        }

        public int NULL_id
        {
            get
            {
                return this.symbol_table.NULL_id;
            }
        }

        public int ObjectClassId
        {
            get
            {
                return this.symbol_table.OBJECT_CLASS_id;
            }
        }

        public int RootNamespaceId
        {
            get
            {
                return this.symbol_table.ROOT_NAMESPACE_id;
            }
        }

        public char SingleCharacter
        {
            get
            {
                return this.scanner.SingleCharacter;
            }
        }

        public int SystemNamespaceId
        {
            get
            {
                return this.symbol_table.SYSTEM_NAMESPACE_id;
            }
        }

        public ProgRec TopInstruction
        {
            get
            {
                int card = this.code.Card;
                while (this.code[card].op == this.code.OP_SEPARATOR)
                {
                    card--;
                }
                return this.code[card];
            }
        }

        public int TRUE_id
        {
            get
            {
                return this.symbol_table.TRUE_id;
            }
        }

        public char UpDecimalCharacter
        {
            get
            {
                return this.scanner.UpDecimalCharacter;
            }
        }

        public char UpDoubleCharacter
        {
            get
            {
                return this.scanner.UpDoubleCharacter;
            }
        }

        public char UpSingleCharacter
        {
            get
            {
                return this.scanner.UpSingleCharacter;
            }
        }

        public int ValueTypeClassId
        {
            get
            {
                return this.symbol_table.VALUETYPE_CLASS_id;
            }
        }
    }
}

