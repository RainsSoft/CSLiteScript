/*
-----------------------------------------------------------------------------
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
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using CSLiteScript;

    public class CSLite_Scripter :  MarshalByRefObject,  IDisposable {//Component {
        public static bool AUTO_IMPORTING_SWITCH = true;
        public BaseScripter scripter;
        private ScripterState state;
        public int RunCount;
        //public event ChangeStateHandler OnChangeState;
        public event CSLiteChangeStateHandler OnChangeState {
            add {
                this.ch = (CSLiteChangeStateHandler)Delegate.Combine(this.ch, value);
            }
            remove {
                this.ch = (CSLiteChangeStateHandler)Delegate.Remove(this.ch, value);
            }
        }

        //public event RunningHandler OnRunning;
        public event RunningHandler OnRunning {
            add {
                this.rh = (RunningHandler)Delegate.Combine(this.rh,value);
            }
            remove {
                this.rh = (RunningHandler)Delegate.Remove(this.rh,value);
            }
        }

        public event CSLiteExceptionHandler OnCSLiteException {
            add {
                this.CSLiteException = (CSLiteExceptionHandler)Delegate.Combine(this.CSLiteException,value);
            }
            remove {
                this.CSLiteException = (CSLiteExceptionHandler)Delegate.Remove(this.CSLiteException,value);
            }
        }
        public CSLiteExceptionHandler CSLiteException;
        private  CSLiteChangeStateHandler ch;
        internal  RunningHandler rh;

        public CSLite_Scripter() {
            this.ch = null;
            this.rh = null;
            this.CSLiteException = null;
            this.RunCount = 0;
            this.scripter = new BaseScripter(this);
            this.scripter.RegisterParser(new CSharp_Parser());
            //this.scripter.RegisterParser(new VB_Parser());
            //this.scripter.RegisterParser(new Pascal_Parser());
            this.state = ScripterState.None;
            this.SetState(ScripterState.Init);
        }

        //public CSLiteScripter(IContainer container) {
        //    this.ch = null;
        //    this.rh = null;
        //    this.CSLiteException = null;
        //    this.RunCount = 0;
        //    this.scripter = new BaseScripter(this);
        //    this.scripter.RegisterParser(new CSharp_Parser());
        //    //this.scripter.RegisterParser(new VB_Parser());
        //    //this.scripter.RegisterParser(new Pascal_Parser());
        //    this.state = ScripterState.None;
        //    this.SetState(ScripterState.Init);
        //    if (container != null) {
        //        //container.Add(this);
        //    }
        //}

        public void AddBDSProj(string file_name) {
            if (!this.HasErrors) {
                this.MatchState(ScripterState.Init);
                this.scripter.AddBDSProj(file_name);
            }
        }

        public Breakpoint AddBreakpoint(string module_name, int line_number) {
            return this.scripter.AddBreakpoint(module_name, line_number);
        }

        public void AddCode(string module_name, string text) {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                        this.scripter.AddCode(module_name, text);
                        if (!this.HasErrors) {
                            this.state = ScripterState.ReadyToCompile;
                            break;
                        }
                        this.SetState(ScripterState.Error);
                        break;

                    default:
                        this.MatchState(ScripterState.Init);
                        break;
                }
            }
        }

        public void AddCodeFromFile(string module_name, string path) {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                        this.scripter.AddCodeFromFile(module_name, path);
                        if (!this.HasErrors) {
                            this.state = ScripterState.ReadyToCompile;
                            break;
                        }
                        this.SetState(ScripterState.Error);
                        break;

                    default:
                        this.MatchState(ScripterState.Init);
                        break;
                }
            }
        }

        public void AddCodeLine(string module_name, string text) {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                        this.scripter.AddCodeLine(module_name, text);
                        if (!this.HasErrors) {
                            this.state = ScripterState.ReadyToCompile;
                            break;
                        }
                        this.SetState(ScripterState.Error);
                        break;

                    default:
                        this.MatchState(ScripterState.Init);
                        break;
                }
            }
        }

        public void AddCSProj(string file_name) {
            if (!this.HasErrors) {
                this.MatchState(ScripterState.Init);
                this.scripter.AddCSProj(file_name);
            }
        }

        public void AddModule(string module_name) {
            if (!this.HasErrors) {
                this.scripter.AddModule(module_name, "CSharp");
            }
        }

        public void AddModule(string module_name, string language_name) {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                        this.scripter.AddModule(module_name, language_name);
                        if (!this.HasErrors) {
                            this.state = ScripterState.ReadyToCompile;
                            break;
                        }
                        this.SetState(ScripterState.Error);
                        break;

                    default:
                        this.MatchState(ScripterState.Init);
                        break;
                }
            }
        }

        public void ApplyDelegate(string event_name, params object[] p) {
            this.scripter.ApplyDelegateHost(event_name, p);
        }

        public void Compile() {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                        this.SetState(ScripterState.ReadyToCompile);
                        break;
                }
                this.MatchState(ScripterState.ReadyToCompile);
                if (!this.HasErrors) {
                    this.SetState(ScripterState.Compiling);
                    this.scripter.Compile();
                    if (this.HasErrors) {
                        this.SetState(ScripterState.Error);
                    }
                    else {
                        this.SetState(ScripterState.ReadyToLink);
                    }
                }
            }
        }

        public void DiscardError() {
            this.scripter.DiscardError();
        }

       

        public object Eval(string expr) {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Running:
                    case ScripterState.Terminated:
                    case ScripterState.Paused:
                        return this.scripter.Eval(expr);
                }
                return null;
            }
            return null;
        }

        public object Eval(string expr, CSLite_Language language) {
            if (this.state == ScripterState.Init) {
                EvalHelper instance = new EvalHelper();
                this.RegisterType(typeof(EvalHelper));
                this.RegisterInstance("_eh", instance);
                this.AddModule("1", language.ToString());
                if (language == CSLite_Language.CSharp) {
                    this.AddCode("1", "_eh.result=" + expr + ";");
                }
                else {
                    this.AddCode("1", "_eh.result=" + expr);
                }
                this.Run(RunMode.Run, new object[0]);
                this.Reset();
                return instance.result;
            }
            return null;
        }

        public void ForbidNamespace(string namespace_name) {
            this.scripter.ForbidNamespace(namespace_name);
        }

        public void ForbidType(Type t) {
            this.scripter.ForbidType(t);
        }

        private int GetEntryId() {
            return this.scripter.GetEntryId();
        }

        private int GetMethodId(string full_name) {
            int methodId = this.scripter.GetMethodId(full_name);
            if (methodId <= 0) {
                this.RaiseErrorEx("CSLite0005. Error in Invoke. Method '{0}' not found.", new object[] { full_name });
            }
            return methodId;
        }

        private int GetMethodId(string full_name, string signature) {
            int methodId = this.scripter.GetMethodId(full_name, signature);
            if (methodId <= 0) {
                this.RaiseErrorEx("CSLite0005. Error in Invoke. Method '{0}' not found.", new object[] { full_name + signature });
            }
            return methodId;
        }

        public object HostObjectToScriptObject(object target) {
            return this.scripter.ToScriptObject(target);
        }

        public object Invoke(RunMode rm, object target, int method_id, params object[] parameters) {
            object obj2 = null;
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                    case ScripterState.ReadyToLink:
                        this.Link();
                        if (!this.HasErrors) {
                            this.SetState(ScripterState.Running);
                            this.scripter.CallStaticConstructors();
                            if (!this.HasErrors) {
                                if (this.HasErrors) {
                                    return obj2;
                                }
                                obj2 = this.scripter.code.CallMethodEx(rm, target, method_id, parameters);
                            }
                            break;
                        }
                        return obj2;

                    case ScripterState.ReadyToRun:
                    case ScripterState.Running:
                        this.SetState(ScripterState.Running);
                        this.scripter.CallStaticConstructors();
                        if (this.HasErrors) {
                            break;
                        }
                        if (!this.HasErrors) {
                            obj2 = this.scripter.code.CallMethodEx(rm, target, method_id, parameters);
                            break;
                        }
                        return obj2;

                    case ScripterState.Terminated:
                        this.scripter.ResetRunStage();
                        this.SetState(ScripterState.Running);
                        if (!this.HasErrors) {
                            obj2 = this.scripter.code.CallMethodEx(rm, target, method_id, parameters);
                            break;
                        }
                        return obj2;

                    case ScripterState.Paused:
                        this.SetState(ScripterState.Running);
                        this.scripter.Resume(rm);
                        break;

                    default:
                        this.MatchState(ScripterState.ReadyToRun);
                        break;
                }
                if (this.HasErrors) {
                    this.SetState(ScripterState.Error);
                }
                else if (this.scripter.Paused) {
                    this.SetState(ScripterState.Paused);
                }
                else {
                    this.SetState(ScripterState.Terminated);
                }
            }
            return obj2;
        }
        /// <summary>
        /// Execute a static method 
        /// </summary>
        /// <param name="rm"></param>
        /// <param name="target"></param>
        /// <param name="method_name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Invoke(RunMode rm, object target, string method_name, params object[] parameters) {
            object obj2 = null;
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                    case ScripterState.ReadyToLink:
                        this.Link();
                        if (!this.HasErrors) {
                            this.SetState(ScripterState.Running);
                            this.scripter.CallStaticConstructors();
                            if (!this.HasErrors) {
                                int methodId = this.GetMethodId(method_name);
                                if (this.HasErrors) {
                                    return obj2;
                                }
                                obj2 = this.scripter.code.CallMethodEx(rm, target, methodId, parameters);
                            }
                            break;
                        }
                        return obj2;

                    case ScripterState.ReadyToRun:
                        this.SetState(ScripterState.Running);
                        this.scripter.CallStaticConstructors();
                        if (!this.HasErrors) {
                            int num3 = this.GetMethodId(method_name);
                            if (this.HasErrors) {
                                return obj2;
                            }
                            obj2 = this.scripter.code.CallMethodEx(rm, target, num3, parameters);
                        }
                        break;

                    case ScripterState.Running: {
                            this.SetState(ScripterState.Running);
                            int num2 = this.GetMethodId(method_name);
                            if (!this.HasErrors) {
                                obj2 = this.scripter.code.CallMethodEx(rm, target, num2, parameters);
                                break;
                            }
                            return obj2;
                        }
                    case ScripterState.Terminated: {
                            this.scripter.ResetRunStage();
                            this.SetState(ScripterState.Running);
                            int num4 = this.GetMethodId(method_name);
                            if (!this.HasErrors) {
                                obj2 = this.scripter.code.CallMethodEx(rm, target, num4, parameters);
                                break;
                            }
                            return obj2;
                        }
                    case ScripterState.Paused:
                        this.SetState(ScripterState.Running);
                        this.scripter.Resume(rm);
                        break;

                    default:
                        this.MatchState(ScripterState.ReadyToRun);
                        break;
                }
                if (this.HasErrors) {
                    this.SetState(ScripterState.Error);
                }
                else if (this.scripter.Paused) {
                    this.SetState(ScripterState.Paused);
                }
                else {
                    this.SetState(ScripterState.Terminated);
                }
            }
            return obj2;
        }

        public void Link() {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                        this.Compile();
                        if (this.HasErrors) {
                            return;
                        }
                        break;
                }
                this.MatchState(ScripterState.ReadyToLink);
                if (!this.HasErrors) {
                    this.state = ScripterState.Linking;
                    this.scripter.Link();
                    if (this.HasErrors) {
                        this.SetState(ScripterState.Error);
                    }
                    else {
                        this.SetState(ScripterState.ReadyToRun);
                    }
                }
            }
        }

        public void LoadCompiledModule(string module_name, Stream s) {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                        this.scripter.LoadCompiledModule(module_name, s);
                        if (!this.HasErrors) {
                            this.state = ScripterState.ReadyToCompile;
                            break;
                        }
                        this.SetState(ScripterState.Error);
                        break;

                    default:
                        this.MatchState(ScripterState.Init);
                        break;
                }
            }
        }

        public void LoadCompiledModuleFromFile(string module_name, string file_name) {
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                        this.scripter.LoadCompiledModuleFromFile(module_name, file_name);
                        if (!this.HasErrors) {
                            this.state = ScripterState.ReadyToCompile;
                            break;
                        }
                        this.SetState(ScripterState.Error);
                        break;

                    default:
                        this.MatchState(ScripterState.Init);
                        break;
                }
            }
        }

        private void MatchState(ScripterState s) {
            if (this.state != s) {
                this.RaiseErrorEx("CSLite0004. Incorrect scripter state. Expected '{0}'", new object[] { s.ToString() });
            }
        }

        private void RaiseError(string message) {
            this.scripter.CreateErrorObject(message);
            this.SetState(ScripterState.Error);
        }

        private void RaiseErrorEx(string message, params object[] p) {
            this.scripter.CreateErrorObjectEx(message, p);
            this.SetState(ScripterState.Error);
        }

        public void RegisterAssembly(Assembly assembly) {
            this.scripter.RegisterAssembly(assembly);
        }

        public void RegisterAssembly(string path) {
            this.scripter.RegisterAssembly(path);
        }

        public void RegisterAssemblyWithPartialName(string name) {
            this.scripter.RegisterAssemblyWithPartialName(name);
        }

        public void RegisterEventHandler(Type event_handler_type, string event_name, Delegate d) {
            this.scripter.RegisterEventHandler(event_handler_type, event_name, d);
        }

        public void RegisterInstance(string instance_name, object instance) {
            this.scripter.RegisterInstance(instance_name, instance);
        }

        public void RegisterOperatorHelper(string name, MethodInfo m) {
            this.scripter.RegisterOperatorHelper(name, m);
        }

        public void RegisterType(Type t) {
            this.scripter.RegisterType(t, true);
        }

        public void RegisterType(Type t, bool recursive) {
            this.scripter.RegisterType(t, recursive);
        }

        public void RegisterVariable(string instance_name, Type type) {
            this.scripter.RegisterVariable(instance_name, type);
        }

        public void RemoveAllBreakpoints() {
            this.scripter.RemoveAllBreakpoints();
        }

        public void RemoveBreakpoint(Breakpoint bp) {
            this.scripter.RemoveBreakpoint(bp);
        }

        public void RemoveBreakpoint(string module_name, int line_number) {
            this.scripter.RemoveBreakpoint(module_name, line_number);
        }

        public void Reset() {
            this.scripter.Reset();
            this.SetState(ScripterState.Init);
        }

        public void ResetCompileStage() {
            this.scripter.ResetCompileStage();
            this.SetState(ScripterState.Init);
        }

        public void Run(RunMode rm, params object[] parameters) {
            this.scripter.TerminatedFlag = false;
            if (!this.HasErrors) {
                switch (this.state) {
                    case ScripterState.Init:
                    case ScripterState.ReadyToCompile:
                    case ScripterState.ReadyToLink:
                        this.Link();
                        if (!this.HasErrors) {
                            this.SetState(ScripterState.Running);
                            this.scripter.CallStaticConstructors();
                            if (!this.HasErrors) {
                                this.scripter.CallMain(rm, parameters);
                            }
                            break;
                        }
                        return;

                    case ScripterState.ReadyToRun:
                        this.SetState(ScripterState.Running);
                        this.scripter.CallStaticConstructors();
                        if (!this.HasErrors) {
                            this.scripter.CallMain(rm, parameters);
                        }
                        break;

                    case ScripterState.Terminated:
                        this.scripter.ResetRunStage();
                        this.SetState(ScripterState.Running);
                        this.scripter.CallStaticConstructors();
                        if (!this.HasErrors) {
                            this.scripter.CallMain(rm, parameters);
                        }
                        break;

                    case ScripterState.Paused:
                        this.SetState(ScripterState.Running);
                        this.scripter.Resume(rm);
                        break;

                    default:
                        this.MatchState(ScripterState.ReadyToRun);
                        break;
                }
                if (this.HasErrors) {
                    this.SetState(ScripterState.Error);
                }
                else if (this.scripter.Paused) {
                    this.SetState(ScripterState.Paused);
                }
                else {
                    this.SetState(ScripterState.Terminated);
                }
            }
        }

        public void SaveCompiledModule(string module_name, Stream s) {
            if (!this.HasErrors) {
                this.MatchState(ScripterState.ReadyToLink);
                if (!this.HasErrors) {
                    this.scripter.SaveCompiledModule(module_name, s);
                    if (this.HasErrors) {
                        this.SetState(ScripterState.Error);
                    }
                }
            }
        }

        public void SaveCompiledModuleToFile(string module_name, string file_name) {
            if (!this.HasErrors) {
                this.MatchState(ScripterState.ReadyToLink);
                if (!this.HasErrors) {
                    this.scripter.SaveCompiledModuleToFile(module_name, file_name);
                    if (this.HasErrors) {
                        this.SetState(ScripterState.Error);
                    }
                }
            }
        }

        public object ScriptObjectToHostObject(object target) {
            if (target.GetType() == typeof(ObjectObject)) {
                return (target as ObjectObject).Instance;
            }
            return target;
        }

        private void SetState(ScripterState value) {
            if ((this.ch != null) && (this.state != value)) {
                this.ch(this, new CSLiteChangeStateEventArgs(this.state, value));
            }
            this.state = value;
        }

        public void Terminate() {
            this.scripter.TerminatedFlag = true;
        }

        public void UnregisterEventHandler(Type event_handler_type, string event_name) {
            this.scripter.UnregisterEventHandler(event_handler_type, event_name);
        }

        public BreakpointList Breakpoint_List {
            get {
                return this.scripter.Breakpoint_List;
            }
        }

        public CallStack Call_Stack {
            get {
                return this.scripter.Call_Stack;
            }
        }

        public string CurrentLine {
            get {
                return this.scripter.CurrentLine;
            }
        }

        public int CurrentLineNumber {
            get {
                return this.scripter.CurrentLineNumber;
            }
        }

        public string CurrentModule {
            get {
                return this.scripter.CurrentModule;
            }
        }

        public bool DefaultInstanceMethods {
            get {
                return this.scripter.DefaultInstanceMethods;
            }
            set {
                this.scripter.DefaultInstanceMethods = value;
            }
        }

        public ErrorList Error_List {
            get {
                return this.scripter.Error_List;
            }
        }

        public bool HasErrors {
            get {
                return this.scripter.IsError();
            }
        }

        public bool HasWarnings {
            get {
                return (this.Warning_List.Count > 0);
            }
        }

        public ModuleList Module_List {
            get {
                return this.scripter.Modules;
            }
        }

        public Hashtable RegisteredInstances {
            get {
                return this.scripter.UserInstances;
            }
        }

        public ScripterState State {
            get {
                return this.state;
            }
            set {
                if (this.state != value) {
                    ScripterState state = this.state;
                    if (state == ScripterState.Init) {
                        this.SetState(value);
                    }
                    else if (state == ScripterState.Terminated) {
                        if (value == ScripterState.Running) {
                            this.SetState(value);
                        }
                        else {
                            this.MatchState(ScripterState.Terminated);
                        }
                    }
                    else {
                        this.MatchState(ScripterState.Terminated);
                    }
                }
            }
        }

        public bool SwappedArguments {
            get {
                return this.scripter.SwappedArguments;
            }
        }

        public ErrorList Warning_List {
            get {
                return this.scripter.Warning_List;
            }
        }

        
        #region IDisposable 成员
        public event EventHandler Disposed;       
        public bool IsDisposed { get { return disposedValue; } }
        private bool disposedValue;
        protected virtual void Dispose(bool disposing) {
            if (Disposed != null) {
                Disposed(this,null);
            }
            if (!this.disposedValue) {
                if (!disposing) {
                    //Console.WriteLine("~()");
                }
               
            }
            this.disposedValue = true;
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //~() {
        //    Dispose(false);
        //}
       

        #endregion
    }
}

