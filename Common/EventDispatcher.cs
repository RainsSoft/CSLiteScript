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
    using System.Reflection;

    internal class EventDispatcher
    {
        private ArrayList event_rec_list;
        private ArrayList registered_event_handlers;
        private BaseScripter scripter;
        private string type_name;

        internal EventDispatcher(BaseScripter scripter, string type_name)
        {
            this.scripter = scripter;
            this.type_name = type_name;
            this.registered_event_handlers = new ArrayList();
            this.event_rec_list = new ArrayList();
            this.RegisterStandardHandlers();
        }

        public Delegate CreateDelegate(object instance, EventInfo event_info, FunctionObject f, object script_delegate)
        {
            Type eventHandlerType = event_info.EventHandlerType;
            string name = event_info.Name;
            for (int i = 0; i < this.registered_event_handlers.Count; i++)
            {
                EventRec rec = this.registered_event_handlers[i] as EventRec;
                Delegate hostDelegate = rec.HostDelegate;
                if ((eventHandlerType == rec.EventHandlerType) && (name == rec.EventName))
                {
                    EventRec rec2 = new EventRec();
                    rec2.Sender = instance;
                    rec2.EventHandlerType = eventHandlerType;
                    rec2.ScriptDelegate = script_delegate;
                    rec2.HostDelegate = hostDelegate;
                    rec2.EventName = name;
                    this.event_rec_list.Add(rec2);
                    return hostDelegate;
                }
            }
            return null;
        }

        public void CreateDispatchType()
        {
        }

        public void DefineEventHandler(EventInfo event_info, FunctionObject f)
        {
        }

        public object LookupScriptDelegate(object sender, string event_name)
        {
            for (int i = 0; i < this.event_rec_list.Count; i++)
            {
                EventRec rec = this.event_rec_list[i] as EventRec;
                if (((sender == rec.Sender) && (event_name == rec.EventName)) && !rec.Processed)
                {
                    rec.Processed = true;
                    return rec.ScriptDelegate;
                }
            }
            return null;
        }

        private void OnClickHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("Click", new object[] { sender, x });
        }

        private void OnCursorChangedHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("CursorChanged", new object[] { sender, x });
        }

        private void OnDockChangedHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("DockChanged", new object[] { sender, x });
        }

        private void OnDoubleClickHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("DoubleClick", new object[] { sender, x });
        }

        private void OnEnabledChangedHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("EnabledChanged", new object[] { sender, x });
        }

        private void OnEnterHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("Enter", new object[] { sender, x });
        }

        private void OnGotFocusHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("GotFocus", new object[] { sender, x });
        }

        private void OnLeaveHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("Leave", new object[] { sender, x });
        }

        private void OnLostFocusHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("LostFocus", new object[] { sender, x });
        }

        private void OnMouseEnterHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("MouseEnter", new object[] { sender, x });
        }

        private void OnMouseLeaveHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("MouseLeave", new object[] { sender, x });
        }

        private void OnResizeHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("Resize", new object[] { sender, x });
        }

        private void OnTextChangedHandler(object sender, EventArgs x)
        {
            this.scripter.ApplyDelegateHost("TextChanged", new object[] { sender, x });
        }

        public void RegisterEventHandler(Type event_handler_type, string event_name, Delegate d)
        {
            EventRec rec = new EventRec();
            rec.EventHandlerType = event_handler_type;
            rec.EventName = event_name;
            rec.HostDelegate = d;
            this.registered_event_handlers.Add(rec);
        }

        private void RegisterStandardHandlers()
        {
            this.RegisterEventHandler(typeof(EventHandler), "Click", new EventHandler(this.OnClickHandler));
            this.RegisterEventHandler(typeof(EventHandler), "CursorChanged", new EventHandler(this.OnCursorChangedHandler));
            this.RegisterEventHandler(typeof(EventHandler), "DockChanged", new EventHandler(this.OnDockChangedHandler));
            this.RegisterEventHandler(typeof(EventHandler), "DoubleClick", new EventHandler(this.OnDoubleClickHandler));
            this.RegisterEventHandler(typeof(EventHandler), "EnabledChanged", new EventHandler(this.OnEnabledChangedHandler));
            this.RegisterEventHandler(typeof(EventHandler), "Enter", new EventHandler(this.OnEnterHandler));
            this.RegisterEventHandler(typeof(EventHandler), "Leave", new EventHandler(this.OnLeaveHandler));
            this.RegisterEventHandler(typeof(EventHandler), "MouseEnter", new EventHandler(this.OnMouseEnterHandler));
            this.RegisterEventHandler(typeof(EventHandler), "MouseLeave", new EventHandler(this.OnMouseLeaveHandler));
            this.RegisterEventHandler(typeof(EventHandler), "Resize", new EventHandler(this.OnResizeHandler));
            this.RegisterEventHandler(typeof(EventHandler), "TextChanged", new EventHandler(this.OnTextChangedHandler));
        }

        public void Reset()
        {
            this.registered_event_handlers.Clear();
            this.event_rec_list.Clear();
            this.RegisterStandardHandlers();
        }

        public void ResetProcessedState(object sender, string event_name)
        {
            for (int i = 0; i < this.event_rec_list.Count; i++)
            {
                EventRec rec = this.event_rec_list[i] as EventRec;
                if ((sender == rec.Sender) && (event_name == rec.EventName))
                {
                    rec.Processed = false;
                }
            }
        }

        public void UnregisterEventHandler(Type event_handler_type, string event_name)
        {
            for (int i = 0; i < this.registered_event_handlers.Count; i++)
            {
                EventRec rec = this.registered_event_handlers[i] as EventRec;
                if ((event_handler_type == rec.EventHandlerType) && (event_name == rec.EventName))
                {
                    this.registered_event_handlers.RemoveAt(i);
                }
            }
        }
    }
}

