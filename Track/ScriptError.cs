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

    public class ScriptError
    {
        public Exception E;
        private string line;
        private int line_number;
        private string message;
        private string module_name;
        private int pcode_line;
        public BaseScripter scripter;

        internal ScriptError(BaseScripter scripter, string message)
        {
            this.message = message;
            this.pcode_line = scripter.code.n;
            if (this.pcode_line == 0)
            {
                this.pcode_line = scripter.code.Card;
            }
            Module module = scripter.code.GetModule(this.pcode_line);
            if (module == null)
            {
                this.module_name = "";
                this.line_number = 0;
                this.line = "";
            }
            else
            {
                this.module_name = module.Name;
                this.line_number = scripter.code.GetErrorLineNumber(this.pcode_line);
                this.line = module.GetLine(this.line_number);
                while (this.IsEmptyLine(this.line))
                {
                    this.pcode_line++;
                    this.line_number++;
                    this.line = module.GetLine(this.line_number);
                    if (this.pcode_line >= scripter.code.Card)
                    {
                        break;
                    }
                }
            }
            this.E = null;
        }

        private bool IsEmptyLine(string s)
        {
            for (int i = 0; i < (s.Length - 1); i++)
            {
                char ch = s[i];
                if (((((ch != '\n') && (ch != '\r')) && ((ch != '\x0085') && (ch != '\u2028'))) && (((ch != '\u2029') && (ch != '\t')) && ((ch != '\v') && (ch != '\f')))) && (ch != ' '))
                {
                    return false;
                }
            }
            return true;
        }

        public string Line
        {
            get
            {
                return this.line;
            }
        }

        public int LineNumber
        {
            get
            {
                return this.line_number;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public string ModuleName
        {
            get
            {
                return this.module_name;
            }
        }

        public int PCodeLineNumber
        {
            get
            {
                return this.pcode_line;
            }
        }
    }
}

