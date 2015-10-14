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
    using System.Xml;

    public sealed class BaseScripter
    {
        internal StringList available_types;
        internal Code code;
        private Hashtable conv_list = new Hashtable();
        internal ConvertHelper conversion = new ConvertHelper();
        internal bool DefaultInstanceMethods = false;
        internal int EntryId;
        internal ErrorList Error_List;
        private EventDispatcher event_dispatcher;
        internal StringList ForbiddenNamespaces;
        internal StringList ForbiddenTypes;
        internal ModuleList module_list;
        internal StringList names = new StringList(false);
        internal Hashtable OperatorHelpers;
        internal CSLite_Scripter Owner;
        internal ParserList parser_list;
        internal StringList PPDirectiveList;
        public BindingFlags protected_binding_flags = (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        internal StringList RegisteredNamespaces;
        internal RegisteredTypeList RegisteredTypes;
        private bool RESET_COMPILE_STAGE_SWITCH = false;
        public bool SearchProtected = true;
        internal bool SIGN_BRIEF_SYNTAX = true;
        internal bool SIGN_REFLECTION = true;
        internal StandardTypeList StandardTypes = new StandardTypeList();
        internal bool SwappedArguments = false;
        internal SymbolTable symbol_table;
        internal bool TerminatedFlag = false;
        internal Hashtable UserInstances;
        internal StringList UserNamespaces;
        internal Hashtable UserTypes;
        internal Hashtable UserVariables;
        internal ErrorList Warning_List;

        internal BaseScripter(CSLite_Scripter owner)
        {
            this.Owner = owner;
            this.symbol_table = new SymbolTable(this);
            this.code = new Code(this);
            this.module_list = new ModuleList(this);
            this.parser_list = new ParserList();
            this.Error_List = new ErrorList(this);
            this.Warning_List = new ErrorList(this);
            this.PPDirectiveList = new StringList(false);
            this.RegisteredTypes = new RegisteredTypeList();
            this.RegisteredNamespaces = new StringList(false);
            this.UserTypes = new Hashtable();
            this.UserNamespaces = new StringList(false);
            this.ForbiddenNamespaces = new StringList(false);
            this.UserInstances = new Hashtable();
            this.UserVariables = new Hashtable();
            this.OperatorHelpers = new Hashtable();
            this.available_types = new StringList(false);
            this.ForbiddenTypes = new StringList(false);
            this.EntryId = 0;
            this.event_dispatcher = new EventDispatcher(this, "CSLiteEventDispatcher");
            if (CSLite_Scripter.AUTO_IMPORTING_SWITCH)
            {
                this.RegisterAvailableNamespaces();
            }
        }

        internal void AddBDSProj(string xmlfile_name)
        {
            string str = CSLite_System.ExtractPath(xmlfile_name);
            XmlDocument document = new XmlDocument();
            document.Load(xmlfile_name);
            for (XmlNode node = document.FirstChild; node != null; node = node.NextSibling)
            {
                if (node.Name == "BorlandProject")
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if (node2.Name == "CSharp.Personality")
                        {
                            for (XmlNode node3 = node2.FirstChild; node3 != null; node3 = node3.NextSibling)
                            {
                                if (node3.Name == "FileList")
                                {
                                    for (XmlNode node4 = node3.FirstChild; node4 != null; node4 = node4.NextSibling)
                                    {
                                        foreach (XmlAttribute attribute in node4.Attributes)
                                        {
                                            if (attribute.Name == "FileName")
                                            {
                                                switch (Path.GetExtension(attribute.Value))
                                                {
                                                    case ".cs":
                                                        this.AddModule(attribute.Value, "CSharp");
                                                        this.AddCodeFromFile(attribute.Value, str + attribute.Value);
                                                        break;

                                                    case ".dll":
                                                        this.RegisterAssembly(attribute.Value);
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal Breakpoint AddBreakpoint(string module_name, int line_number)
        {
            Breakpoint bp = new Breakpoint(this, module_name, line_number);
            this.code.AddBreakpoint(bp);
            return bp;
        }

        internal  Module AddCode(string module_name, string text)
        {
            int index = this.module_list.IndexOf(module_name);
            if (index == -1)
            {
                this.RaiseException("CSLite0001. Module not found.");
            }
             Module module = this.module_list[index];
            module.Text = module.Text + text;
            return module;
        }

        internal  Module AddCodeFromFile(string module_name, string path)
        {
            if (File.Exists(path))
            {
                int index = this.module_list.IndexOf(module_name);
                if (index == -1)
                {
                    this.RaiseException("CSLite0001. Module not found.");
                }
                 Module module = this.module_list[index];
                using (StreamReader reader = new StreamReader(path))
                {
                    module.Text = reader.ReadToEnd();
                }
                module.FileName = path;
                return module;
            }
            this.CreateErrorObjectEx("CS0014. Required file '{0}' could not be found.", new object[] { path });
            return null;
        }

        internal  Module AddCodeLine(string module_name, string text)
        {
            int index = this.module_list.IndexOf(module_name);
            if (index == -1)
            {
                this.RaiseException("CSLite0001. Module not found.");
            }
             Module module = this.module_list[index];
            object obj2 = module.Text;
            module.Text = string.Concat(new object[] { obj2, text, '\r', '\n' });
            return module;
        }

        internal void AddCSProj(string file_name)
        {
            string str = CSLite_System.ExtractPath(file_name);
            XmlDocument document = new XmlDocument();
            document.Load(file_name);
            for (XmlNode node = document.FirstChild; node != null; node = node.NextSibling)
            {
                if (node.Name == "VisualStudioProject")
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if (node2.Name == "CSHARP")
                        {
                            for (XmlNode node3 = node2.FirstChild; node3 != null; node3 = node3.NextSibling)
                            {
                                if (node3.Name == "Build")
                                {
                                    for (XmlNode node4 = node3.FirstChild; node4 != null; node4 = node4.NextSibling)
                                    {
                                        if (node4.Name == "References")
                                        {
                                            for (XmlNode node5 = node4.FirstChild; node5 != null; node5 = node5.NextSibling)
                                            {
                                                if (node5.Name == "Reference")
                                                {
                                                    foreach (XmlAttribute attribute in node5.Attributes)
                                                    {
                                                        if (attribute.Name == "HintPath")
                                                        {
                                                            this.RegisterAssembly(attribute.Value);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (node3.Name == "Files")
                                {
                                    for (XmlNode node6 = node3.FirstChild; node6 != null; node6 = node6.NextSibling)
                                    {
                                        if (node6.Name == "Include")
                                        {
                                            for (XmlNode node7 = node6.FirstChild; node7 != null; node7 = node7.NextSibling)
                                            {
                                                if (node7.Name == "File")
                                                {
                                                    foreach (XmlAttribute attribute2 in node7.Attributes)
                                                    {
                                                        if (attribute2.Name == "RelPath")
                                                        {
                                                            this.AddModule(attribute2.Value, "CSharp");
                                                            this.AddCodeFromFile(attribute2.Value, str + attribute2.Value);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal ObjectObject AddDelegates(ObjectObject d1, ObjectObject d2)
        {
            object obj4;
            FunctionObject obj5;
            bool flag;
            ObjectObject obj3 = d1.Class_Object.CreateObject();
            for (flag = d1.FindFirstInvocation(out obj4, out obj5); flag; flag = d1.FindNextInvocation(out obj4, out obj5))
            {
                obj3.AddInvocation(obj4, obj5);
            }
            for (flag = d2.FindFirstInvocation(out obj4, out obj5); flag; flag = d2.FindNextInvocation(out obj4, out obj5))
            {
                obj3.AddInvocation(obj4, obj5);
            }
            return obj3;
        }

        internal  Module AddModule(string module_name, string language_name)
        {
            int index = this.module_list.IndexOf(module_name);
            if (index == -1)
            {
                 Module m = new  Module(this, module_name, language_name);
                this.module_list.Add(m);
                return m;
            }
            return this.module_list[index];
        }

        private void ApplyDel(ObjectObject d, params object[] p)
        {
            if (d != null)
            {
                object obj2;
                FunctionObject obj3;
                d.FindFirstInvocation(out obj2, out obj3);
                while (obj3 != null)
                {
                    this.code.CallMethodEx(RunMode.Run, obj2, obj3.Id, p);
                    d.FindNextInvocation(out obj2, out obj3);
                }
            }
        }

        public static void ApplyDelegate(object scripter, object d, params object[] p)
        {
            BaseScripter scripter2 = scripter as BaseScripter;
            scripter2.ApplyDel(scripter2.ToScriptObject(d), p);
        }

        public void ApplyDelegateHost(string event_name, params object[] p)
        {
            object sender = p[0];
            object scriptDelegate = this.event_dispatcher.LookupScriptDelegate(sender, event_name);
            if (scriptDelegate == null)
            {
                this.event_dispatcher.ResetProcessedState(sender, event_name);
                scriptDelegate = this.event_dispatcher.LookupScriptDelegate(sender, event_name);
            }
            this.ApplyDel(this.ToScriptObject(scriptDelegate), p);
        }

        internal object CallMain(RunMode rm, params object[] p)
        {
            return this.CallMethod(rm, null, this.EntryId, p);
        }

        internal object CallMethod(RunMode rm, object target, int sub_id, params object[] p)
        {
            return this.code.CallMethod(rm, target, sub_id, p);
        }

        internal void CallStaticConstructors()
        {
            this.code.CallStaticConstructors();
            if (!this.IsError())
            {
            }
        }

        internal bool CheckForbiddenNamespace(string s)
        {
            char ch;
            if (this.ForbiddenNamespaces.IndexOf(s) >= 0)
            {
                return true;
            }
            string str = CSLite_System.ExtractOwner(s, out ch);
            if (str.Length == 0)
            {
                return false;
            }
            return this.CheckForbiddenNamespace(str);
        }

        internal void CheckForbiddenType(int id)
        {
            if (this.symbol_table[id].Kind == MemberKind.Type)
            {
                ClassObject classObject = this.GetClassObject(id);
                if (classObject.Imported && (classObject.ImportedType != null))
                {
                    foreach (Attribute attribute in Attribute.GetCustomAttributes(classObject.ImportedType))
                    {
                        if (attribute is CSLite_ScriptForbid)
                        {
                            this.CreateErrorObjectEx("CSLite0007. Use of forbidden type '{0}'.", new object[] { classObject.FullName });
                            return;
                        }
                    }
                }
                if (this.ForbiddenTypes.IndexOf(classObject.FullName) >= 0)
                {
                    this.CreateErrorObjectEx("CSLite0007. Use of forbidden type '{0}'.", new object[] { classObject.FullName });
                }
                else
                {
                    char ch;
                    string s = CSLite_System.ExtractOwner(classObject.FullName, out ch);
                    if (this.CheckForbiddenNamespace(s))
                    {
                        this.CreateErrorObjectEx("CSLite0007. Use of forbidden type '{0}'.", new object[] { classObject.FullName });
                    }
                }
            }
        }

        internal void Compile()
        {
            int num;
            if (this.RESET_COMPILE_STAGE_SWITCH)
            {
                foreach (string str in this.UserInstances.Keys)
                {
                    object instance = this.UserInstances[str];
                    this.symbol_table.RegisterInstance(str, instance, true);
                }
                foreach (string str2 in this.UserVariables.Keys)
                {
                    Type t = this.UserVariables[str2] as Type;
                    this.symbol_table.RegisterVariable(str2, t, true);
                }
            }
            else
            {
                this.RegisteredTypes.Clear();
                this.RegisteredNamespaces.Clear();
                this.symbol_table.Init();
                for (num = 0; num < this.UserNamespaces.Count; num++)
                {
                    string str3 = this.UserNamespaces[num];
                    this.symbol_table.RegisterNamespace(str3);
                }
                foreach (Type type2 in this.UserTypes.Keys)
                {
                    bool recursive = (bool) this.UserTypes[type2];
                    this.symbol_table.RegisterType(type2, recursive);
                }
                foreach (string str4 in this.UserInstances.Keys)
                {
                    object obj3 = this.UserInstances[str4];
                    this.symbol_table.RegisterInstance(str4, obj3, false);
                }
                foreach (string str5 in this.UserVariables.Keys)
                {
                    Type type3 = this.UserVariables[str5] as Type;
                    this.symbol_table.RegisterVariable(str5, type3, false);
                }
            }
            this.symbol_table.RESET_COMPILE_STAGE_CARD = this.symbol_table.Card;
            for (num = 0; num < this.module_list.Count; num++)
            {
                 Module m = this.module_list[num];
                BaseParser p = this.parser_list.FindParser(m.LanguageName);
                this.CompileModule(m, p);
                if (this.IsError())
                {
                    break;
                }
            }
        }

        private void CompileModule( Module m, BaseParser p)
        {
            m.BeforeCompile();
            if (m.IsSourceCodeModule)
            {
                try
                {
                    m.LoadFromStream();
                    if (File.Exists(m.FileName))
                    {
                        this.AddCodeFromFile(m.Name, m.FileName);
                    }
                }
                catch (Exception exception)
                {
                    this.CreateErrorObject(exception.Message);
                    this.CreateErrorObjectEx("CSLite0003. Cannot load compiled module '{0}' from '{1}'", new object[] { m.Name, m.FileName });
                }
            }
            else if (p == null)
            {
                this.CreateErrorObjectEx("CSLite0002. Unknown language '{0}'.", new object[] { m.LanguageName });
            }
            else
            {
                try
                {
                    p.Init(this, m);
                    p.Gen(this.code.OP_BEGIN_MODULE, m.NameIndex, (int) m.Language, 0);
                    p.Gen(this.code.OP_SEPARATOR, m.NameIndex, 0, 0);
                    p.Gen(this.code.OP_BEGIN_USING, p.RootNamespaceId, 0, 0);
                    p.Gen(this.code.OP_BEGIN_USING, p.SystemNamespaceId, 0, 0);
                    p.Gen(this.code.OP_CHECKED, this.symbol_table.TRUE_id, 0, 0);
                    if (m.Language == CSLite_Language.VB)
                    {
                        p.Gen(this.code.OP_UPCASE_ON, 0, 0, 0);
                    }
                    else
                    {
                        p.Gen(this.code.OP_UPCASE_OFF, 0, 0, 0);
                    }
                    p.Gen(this.code.OP_EXPLICIT_ON, 0, 0, 0);
                    p.Call_SCANNER();
                    p.Parse_Program();
                    p.Gen(this.code.OP_RESTORE_CHECKED_STATE, 0, 0, 0);
                    p.Gen(this.code.OP_END_USING, p.RootNamespaceId, 0, 0);
                    p.Gen(this.code.OP_END_USING, p.SystemNamespaceId, 0, 0);
                    p.Gen(this.code.OP_HALT, 0, 0, 0);
                    p.Gen(this.code.OP_END_MODULE, m.NameIndex, 0, 0);
                    if (!p.ConditionalDirectivesAreCompleted())
                    {
                        p.RaiseError(true, "CS1027. #endif directive expected.");
                    }
                }
                catch (Errors.CSLiteScriptException)
                {
                }
                catch (Exception exception2)
                {
                    this.Error_List.Add(new ScriptError(this, exception2.Message));
                    this.LastError.E = exception2;
                }
            }
            m.AfterCompile();
        }

        internal Delegate CreateDelegate(object instance, EventInfo e, FunctionObject pattern_method, object script_delegate)
        {
            return this.event_dispatcher.CreateDelegate(instance, e, pattern_method, script_delegate);
        }

        internal void CreateDispatchType()
        {
            this.event_dispatcher.CreateDispatchType();
        }

        internal void CreateErrorObject(string message)
        {
            int n = this.code.n;
            if (n == 0)
            {
                n = this.code.Card;
            }
            this.Dump();
            if (!this.Error_List.HasError(message, n))
            {
                ScriptError e = new ScriptError(this, message);
                this.Error_List.Add(e);
            }
        }

        internal void CreateErrorObjectEx(string message, params object[] p)
        {
            this.CreateErrorObject(string.Format(message, p));
        }

        internal void CreateWarningObject(string message)
        {
            int n = this.code.n;
            if (n == 0)
            {
                n = this.code.Card;
            }
            if (!this.Warning_List.HasError(message, n))
            {
                this.Warning_List.Add(new ScriptError(this, message));
            }
        }

        internal void CreateWarningObjectEx(string message, params object[] p)
        {
            this.CreateWarningObject(string.Format(message, p));
        }

        internal void DefineEventHandler(EventInfo e, FunctionObject pattern_method)
        {
            this.event_dispatcher.DefineEventHandler(e, pattern_method);
        }

        internal void DiscardError()
        {
            this.Error_List.Clear();
            this.Warning_List.Clear();
        }

        internal void Dump()
        {
        }

        internal object Eval(string expr)
        {
             Module m = this.GetModule(this.code.n);
            if (m == null)
            {
                return null;
            }
            BaseParser parser = this.parser_list.FindParser(m.LanguageName);
            if (parser == null)
            {
                this.CreateErrorObjectEx("CSLite0002. Unknown language '{0}'.", new object[] { m.LanguageName });
                return null;
            }
            int subId = this.code.GetSubId(this.code.n);
            if (subId == -1)
            {
                return null;
            }
            this.code.SaveState();
            this.symbol_table.SaveState();
            int card = this.code.Card;
            int n = this.code.n;
            int num4 = this.symbol_table.AppVar();
            int num5 = 0;
            try
            {
                parser.InitExpression(this, m, subId, expr);
                if (m.Language == CSLite_Language.VB)
                {
                    parser.Gen(this.code.OP_UPCASE_ON, 0, 0, 0);
                }
                else
                {
                    parser.Gen(this.code.OP_UPCASE_OFF, 0, 0, 0);
                }
                parser.Call_SCANNER();
                num5 = parser.Parse_Expression();
            }
            catch (Errors.CSLiteScriptException)
            {
            }
            catch (Exception exception)
            {
                this.Error_List.Add(new ScriptError(this, exception.Message));
                this.LastError.E = exception;
            }
            if (this.IsError())
            {
                return null;
            }
            if (card == this.code.Card)
            {
                num4 = num5;
            }
            else
            {
                IntegerStack stack = this.code.RecreateLevelStack(n);
                if (stack.Count == 0)
                {
                    this.code.RestoreState();
                    this.symbol_table.RestoreState();
                    return null;
                }
                IntegerStack stack2 = this.code.RecreateClassStack(n);
                parser.Gen(this.code.OP_ASSIGN, num4, num5, num4);
                parser.Gen(this.code.OP_HALT, 0, 0, 0);
                this.code.RemoveEvalOpEx(card, stack);
                this.code.SetTypesEx(card);
                this.code.CheckTypesEx(card, stack.Peek(), stack2);
                this.code.AdjustCallsEx(card);
                this.code.LinkGoToEx(card);
                this.code.n = card;
                this.code.Run(RunMode.Run);
            }
            object obj2 = this.GetValue(num4);
            this.code.RestoreState();
            this.symbol_table.RestoreState();
            return obj2;
        }

        internal Type FindAvailableType(string full_name, bool upcase)
        {
            int index = this.available_types.IndexOf(full_name);
            if ((index == -1) && upcase)
            {
                index = this.available_types.UpcaseIndexOf(full_name);
            }
            if (index == -1)
            {
                return null;
            }
            return (this.available_types.Objects[index] as Type);
        }

        internal MemberObject FindImportedNamespaceMember(ClassObject n, string member_name, bool upcase)
        {
            string str = n.FullName + "." + member_name;
            Type t = this.FindAvailableType(str, upcase);
            if (t != null)
            {
                int id = this.symbol_table.RegisterType(t, false);
                return this.GetMemberObject(id);
            }
            string str2 = str + ".";
            for (int i = 0; i < this.UserNamespaces.Count; i++)
            {
                string str3 = this.UserNamespaces[i] + ".";
                if (str2.Length < str3.Length)
                {
                    str3 = str3.Substring(0, str2.Length - 1);
                }
                if (str2 == str3)
                {
                    int num3 = this.symbol_table.RegisterNamespace(str);
                    return this.GetMemberObject(num3);
                }
            }
            return null;
        }

        internal ClassObject FindOriginalType(ClassObject c)
        {
            if (!c.IsRefType)
            {
                return c;
            }
            string name = c.Name.Substring(0, c.Name.Length - 1);
            int ownerId = c.OwnerId;
            int id = this.symbol_table.LookupID(name, ownerId, false);
            if (id > 0)
            {
                return (ClassObject) this.GetVal(id);
            }
            return null;
        }

        internal void ForbidNamespace(string namespace_name)
        {
            this.ForbiddenNamespaces.Add(namespace_name);
        }

        internal void ForbidType(Type t)
        {
            this.ForbiddenTypes.Add(t.FullName);
        }

        internal ClassObject GetClassObject(int id)
        {
            return (ClassObject) this.symbol_table[id].Val;
        }

        internal ClassObject GetClassObjectEx(int id)
        {
            ClassObject classObject = this.GetClassObject(id);
            if (classObject.IsRefType)
            {
                classObject = this.FindOriginalType(classObject);
            }
            return classObject;
        }

        internal ProgRec GetCurrentIstruction()
        {
            return this.code.GetCurrentIstruction();
        }

        internal int GetEntryId()
        {
            IntegerList list = new IntegerList(false);
            for (int i = 0; i < this.symbol_table.Card; i++)
            {
                if ((this.symbol_table[i].Kind == MemberKind.Method) && (this.symbol_table[i].Name == "Main"))
                {
                    for (int j = 1; j < this.code.Card; j++)
                    {
                        if ((this.code[j].op == this.code.OP_CREATE_METHOD) && (this.code[j].arg1 == i))
                        {
                            for (int k = j + 1; this.code[k].op == this.code.OP_ADD_MODIFIER; k++)
                            {
                                if (this.code[k].arg2 == 7)
                                {
                                    list.Add(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (list.Count == 0)
            {
                return 0;
            }
            if (list.Count == 1)
            {
                return list[0];
            }
            return -1;
        }

        internal EventObject GetEventObject(int id)
        {
            return (EventObject) this.symbol_table[id].Val;
        }

        internal FunctionObject GetFunctionObject(int id)
        {
            return (FunctionObject) this.symbol_table[id].Val;
        }

        internal IndexObject GetIndexObject(int id)
        {
            return (IndexObject) this.symbol_table[id].Val;
        }

        internal int GetLineNumber(int n)
        {
            return this.code.GetLineNumber(n);
        }

        internal MemberObject GetMemberObject(int id)
        {
            return (MemberObject) this.symbol_table[id].Val;
        }

        internal int GetMethodId(string full_name)
        {
            return this.symbol_table.GetMethodId(full_name);
        }

        internal int GetMethodId(string full_name, string signature)
        {
            return this.symbol_table.GetMethodId(full_name, signature);
        }

        internal  Module GetModule(int n)
        {
            return this.code.GetModule(n);
        }

        internal PropertyObject GetPropertyObject(int id)
        {
            return (PropertyObject) this.symbol_table[id].Val;
        }

        internal int GetTypeId(int id)
        {
            return this.symbol_table[id].TypeId;
        }

        internal int GetTypeId(string type_name)
        {
            for (int i = this.symbol_table.Card; i >= 1; i--)
            {
                if ((this.symbol_table[i].Kind == MemberKind.Type) && (this.symbol_table[i].Name == type_name))
                {
                    return i;
                }
            }
            return 0;
        }

        internal string GetUpcaseNameByNameIndex(int name_index)
        {
            return this.names[name_index].ToUpper();
        }

        internal object GetVal(int id)
        {
            return this.symbol_table[id].Val;
        }

        internal object GetValue(int id)
        {
            return this.symbol_table[id].Value;
        }

        internal bool HasPredefinedNamespace(string full_name)
        {
            return (this.UserNamespaces.IndexOf(full_name) >= 0);
        }

        internal bool IsError()
        {
            return (this.Error_List.Count > 0);
        }

        internal bool IsStandardType(int id)
        {
            return ((id >= 1) && (id <= this.StandardTypes.Count));
        }

        internal void Link()
        {
            this.Dump();
            this.code.CreateClassObjects();
            if (!this.IsError())
            {
                this.code.RemoveEvalOp();
                if (!this.IsError())
                {
                    this.code.SetTypes();
                    if (!this.IsError())
                    {
                        this.code.CheckTypes();
                        if (!this.IsError())
                        {
                            this.code.InsertEventHandlers();
                            if (!this.IsError())
                            {
                                this.code.AdjustCalls();
                                if (!this.IsError())
                                {
                                    foreach (string str in this.UserInstances.Keys)
                                    {
                                        object obj2 = this.UserInstances[str];
                                        if (obj2 is Delegate)
                                        {
                                            this.code.CreatePatternMethod(obj2 as Delegate);
                                            this.code.Card++;
                                            this.code.SetInstruction(this.code.Card, this.code.OP_HALT, 0, 0, 0);
                                        }
                                    }
                                    this.EntryId = this.GetEntryId();
                                    if (this.EntryId == 0)
                                    {
                                        this.CreateErrorObjectEx("CS5001. Program '{0}' does not have an entry point defined.", new object[] { "" });
                                    }
                                    else if (this.EntryId < 0)
                                    {
                                        this.CreateErrorObject("CS0017. Program has more than one entry point defined.");
                                    }
                                    else
                                    {
                                        this.code.Optimization();
                                        if (!this.IsError())
                                        {
                                            this.code.LinkGoTo();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal  Module LoadCompiledModule(string module_name, Stream s)
        {
             Module module;
            int index = this.module_list.IndexOf(module_name);
            if (index == -1)
            {
                module = new  Module(this, module_name, "");
                this.module_list.Add(module);
            }
            else
            {
                module = this.module_list[index];
            }
            module.PreLoadFromStream(s);
            return module;
        }

        internal  Module LoadCompiledModuleFromFile(string module_name, string file_name)
        {
            using (FileStream stream = new FileStream(file_name, FileMode.Open))
            {
                return this.LoadCompiledModule(module_name, stream);
            }
        }

        internal bool MatchAssignment(int id1, int id2)
        {
            if (this.conversion.ExistsImplicitConversion(this, id2, id1))
            {
                return true;
            }
            int typeId = this.symbol_table[id1].TypeId;
            int id = this.symbol_table[id2].TypeId;
            ClassObject classObject = this.GetClassObject(typeId);
            ClassObject obj3 = this.GetClassObject(id);
            if (classObject.Name == obj3.Name)
            {
                return true;
            }
            if (classObject.Class_Kind == ClassKind.Enum)
            {
                if (obj3.Class_Kind == ClassKind.Enum)
                {
                    return false;
                }
                return this.MatchTypes(classObject.UnderlyingType, obj3);
            }
            if (obj3.Class_Kind == ClassKind.Enum)
            {
                if (classObject.Class_Kind == ClassKind.Enum)
                {
                    return false;
                }
                return this.MatchTypes(classObject, obj3.UnderlyingType);
            }
            return false;
        }

        internal bool MatchTypes(ClassObject c1, ClassObject c2)
        {
            if (c1.IsRefType)
            {
                c1 = this.FindOriginalType(c1);
                if (c1 == null)
                {
                    return false;
                }
            }
            if (c2.IsRefType)
            {
                c2 = this.FindOriginalType(c2);
                if (c2 == null)
                {
                    return false;
                }
            }
            if (c1 == c2)
            {
                return true;
            }
            if (this.conversion.ExistsImplicitNumericConversion(c1.Id, c2.Id))
            {
                return true;
            }
            if (c1.Class_Kind == ClassKind.Enum)
            {
                if (c2.Class_Kind == ClassKind.Enum)
                {
                    return false;
                }
                return this.MatchTypes(c1.UnderlyingType, c2);
            }
            if (c2.Class_Kind == ClassKind.Enum)
            {
                if (c1.Class_Kind == ClassKind.Enum)
                {
                    return false;
                }
                return this.MatchTypes(c1, c2.UnderlyingType);
            }
            return false;
        }

        internal void PutVal(int id, object value)
        {
            this.symbol_table[id].Val = value;
        }

        internal void PutValue(int id, object value)
        {
            this.symbol_table[id].Value = value;
        }

        internal void RaiseException(string message)
        {
            Errors.RaiseException(message);
        }

        internal void RaiseExceptionEx(string message, params object[] p)
        {
            Errors.RaiseException(string.Format(message, p));
        }

        internal void RegisterAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("BaseScripter.RegisterAssembly: 'assembly' parameter must not be null.");
            }
            foreach (Type type in assembly.GetTypes())
            {
                if (type != null)
                {
                    this.available_types.AddObject(type.FullName, type);
                    string avalue = type.Namespace;
                    if ((avalue != null) && (avalue != ""))
                    {
                        this.UserNamespaces.Add(avalue);
                    }
                }
            }
        }

        internal void RegisterAssembly(string path)
        {
            //
            AssemblyName assemblyRef = new AssemblyName();
            assemblyRef.CodeBase = path;
            this.RegisterAssembly(Assembly.Load(assemblyRef));
        }

        internal void RegisterAssemblyWithPartialName(string name)
        {
            Assembly assembly = Assembly.Load(name);//LoadWithPartialName(name);
            this.RegisterAssembly(assembly);
        }

        internal void RegisterAvailableNamespaces()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                this.RegisterAssembly(assembly);
            }
        }

        internal void RegisterAvailableType(Type t)
        {
            this.available_types.AddObject(t.FullName, t);
        }

        public void RegisterEventHandler(Type event_handler_type, string event_name, Delegate d)
        {
            this.event_dispatcher.RegisterEventHandler(event_handler_type, event_name, d);
        }

        internal void RegisterInstance(string name, object instance)
        {
            if (instance == null)
            {
                this.CreateErrorObjectEx("CSLite0009. Error in RegisterInstance method. You have tried to register object which is not initialized.", new object[] { name });
            }
            else
            {
                if (this.UserVariables.ContainsKey(name))
                {
                    this.UserVariables.Remove(name);
                }
                if (this.UserInstances.ContainsKey(name))
                {
                    this.UserInstances[name] = instance;
                }
                else
                {
                    this.UserInstances.Add(name, instance);
                }
            }
        }

        internal void RegisterNamespace(string name)
        {
            this.UserNamespaces.Add(name);
        }

        internal void RegisterOperatorHelper(string name, MethodInfo m)
        {
            this.OperatorHelpers.Add(name, m);
        }

        internal void RegisterParser(BaseParser p)
        {
            this.parser_list.Add(p);
        }

        internal void RegisterType(Type t, bool recursive)
        {
            if (this.UserTypes.ContainsKey(t))
            {
                this.UserTypes[t] = recursive;
            }
            else
            {
                this.UserTypes.Add(t, recursive);
            }
        }

        internal void RegisterVariable(string name, Type type)
        {
            if (this.UserInstances.ContainsKey(name))
            {
                this.UserInstances.Remove(name);
            }
            if (this.UserVariables.ContainsKey(name))
            {
                this.UserVariables[name] = type;
            }
            else
            {
                this.UserVariables.Add(name, type);
            }
        }

        internal void RemoveAllBreakpoints()
        {
            this.code.RemoveAllBreakpoints();
        }

        internal void RemoveBreakpoint(Breakpoint bp)
        {
            this.code.RemoveBreakpoint(bp);
        }

        internal void RemoveBreakpoint(string module_name, int line_number)
        {
            this.code.RemoveBreakpoint(module_name, line_number);
        }

        internal void Reset()
        {
            this.RESET_COMPILE_STAGE_SWITCH = false;
            this.TerminatedFlag = false;
            this.EntryId = 0;
            this.DiscardError();
            this.symbol_table.Reset();
            this.code.Reset();
            this.module_list.Clear();
            this.PPDirectiveList.Clear();
            this.RegisteredTypes.Clear();
            this.RegisteredNamespaces.Clear();
            this.UserInstances.Clear();
            this.UserVariables.Clear();
            this.OperatorHelpers.Clear();
            this.event_dispatcher.Reset();
            this.conv_list.Clear();
            this.ForbiddenNamespaces.Clear();
            this.ForbiddenTypes.Clear();
        }

        internal void ResetCompileStage()
        {
            this.RESET_COMPILE_STAGE_SWITCH = true;
            this.EntryId = 0;
            this.DiscardError();
            this.symbol_table.ResetCompileStage();
            this.code.Reset();
            this.module_list.Clear();
            this.PPDirectiveList.Clear();
            for (int i = this.RegisteredTypes.Count - 1; i >= 0; i--)
            {
                RegisteredType type = this.RegisteredTypes[i];
                if (type.Id > this.symbol_table.RESET_COMPILE_STAGE_CARD)
                {
                    this.RegisteredTypes.Delete(i);
                }
            }
            for (int j = this.RegisteredNamespaces.Count - 1; j >= 0; j--)
            {
                int num3 = (int) this.RegisteredNamespaces.Objects[j];
                if (num3 > this.symbol_table.RESET_COMPILE_STAGE_CARD)
                {
                    this.RegisteredNamespaces.Delete(j);
                }
            }
        }

        internal void ResetRunStage()
        {
            this.conv_list.Clear();
            this.code.ResetRunStageStructs();
        }

        internal void Resume(RunMode rm)
        {
            this.code.Run(rm);
        }

        internal  Module SaveCompiledModule(string module_name, Stream s)
        {
            int index = this.module_list.IndexOf(module_name);
            if (index != -1)
            {
                 Module module = this.module_list[index];
                module.SaveToStream(s);
                return module;
            }
            return null;
        }

        internal  Module SaveCompiledModuleToFile(string module_name, string file_name)
        {
            using (FileStream stream = new FileStream(file_name, FileMode.Create))
            {
                return this.SaveCompiledModule(module_name, stream);
            }
        }

        internal void SetTypeId(int id, int type_id)
        {
            this.symbol_table[id].TypeId = type_id;
        }

        internal void ShowErrors()
        {
            foreach (ScriptError error in this.Error_List)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(error.Message);
                Console.WriteLine("Module: " + error.ModuleName);
                Console.WriteLine("LineNumber: " + error.LineNumber.ToString());
                Console.WriteLine("PCodeLineNumber: " + error.PCodeLineNumber.ToString());
                Console.WriteLine(error.Line);
            }
        }

        internal void ShowType(Type t)
        {
            foreach (ConstructorInfo info in t.GetConstructors())
            {
                Console.WriteLine("constructor:");
                Console.WriteLine(info.ToString());
            }
            foreach (FieldInfo info2 in t.GetFields())
            {
                Console.WriteLine("field:");
                Console.WriteLine(info2.ToString());
            }
            foreach (MethodInfo info3 in t.GetMethods())
            {
                Console.WriteLine("method:");
                Console.WriteLine(info3.ToString());
            }
            foreach (PropertyInfo info4 in t.GetProperties())
            {
                Console.WriteLine("property:");
                Console.WriteLine(info4.ToString());
            }
            foreach (EventInfo info5 in t.GetEvents())
            {
                Console.WriteLine("event:");
                Console.WriteLine(info5.ToString());
            }
        }

        internal void ShowWarnings()
        {
            foreach (ScriptError error in this.Warning_List)
            {
                Console.WriteLine("Warning:");
                Console.WriteLine(error.Message);
                Console.WriteLine("Module: " + error.ModuleName);
                Console.WriteLine("LineNumber: " + error.LineNumber.ToString());
                Console.WriteLine("PCodeLineNumber: " + error.PCodeLineNumber.ToString());
                Console.WriteLine(error.Line);
            }
        }

        internal int SourceLineToPCodeLine(string module_name, int line_number)
        {
            int index = this.module_list.IndexOf(module_name);
            if (index != -1)
            {
                 Module module = this.module_list[index];
                for (int i = module.P1; i <= module.P2; i++)
                {
                    if ((this.code[i].op == this.code.OP_SEPARATOR) && (this.code[i].arg2 == line_number))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        internal ObjectObject ToScriptObject(object v)
        {
            if (v == null)
            {
                return null;
            }
            Type t = v.GetType();
            if (t == typeof(ObjectObject))
            {
                return (ObjectObject) v;
            }
            if (t == typeof(IndexObject))
            {
                return this.ToScriptObject((v as IndexObject).Value);
            }
            ObjectObject obj2 = (ObjectObject) this.conv_list[v];
            if (obj2 == null)
            {
                int id = this.RegisteredTypes.FindRegisteredTypeId(t);
                obj2 = ((ClassObject) this.GetVal(id)).CreateObject();
                obj2.Instance = v;
                this.conv_list.Add(v, obj2);
            }
            return obj2;
        }

        public void UnregisterEventHandler(Type event_handler_type, string event_name)
        {
            this.event_dispatcher.UnregisterEventHandler(event_handler_type, event_name);
        }

        internal BreakpointList Breakpoint_List
        {
            get
            {
                return this.code.Breakpoints;
            }
        }

        internal CallStack Call_Stack
        {
            get
            {
                return this.code.Call_Stack;
            }
        }

        internal string CurrentLine
        {
            get
            {
                return this.code.CurrentLine;
            }
        }

        internal int CurrentLineNumber
        {
            get
            {
                return this.code.CurrentLineNumber;
            }
        }

        internal string CurrentModule
        {
            get
            {
                return this.code.CurrentModule;
            }
        }

        internal ScriptError LastError
        {
            get
            {
                if (this.Error_List.Count == 0)
                {
                    return null;
                }
                return this.Error_List[this.Error_List.Count - 1];
            }
        }

        internal ModuleList Modules
        {
            get
            {
                return this.module_list;
            }
        }

        internal bool Paused
        {
            get
            {
                return this.code.Paused;
            }
        }

        internal bool Terminated
        {
            get
            {
                return this.code.Terminated;
            }
        }
    }
}

