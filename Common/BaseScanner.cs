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

    internal class BaseScanner
    {
        public string buff;
        public const char CHAR_EOF = '\x001a';
        public char DecimalCharacter = 'm';
        public string DecimalSeparator;
        private IntegerStack def_stack;
        public char DoubleCharacter = 'd';
        private IntegerStack history;
        public const int HISTORY_REC_LENGTH = 7;
        private int line_number;
        public int p;
        private BaseParser parser;
        private int pos;
        private const int ppELIF = 2;
        private const int ppELSE = 3;
        private const int ppENDIF = 4;
        private const int ppIF = 1;
        private BaseScripter scripter;
        public char SingleCharacter = 'f';
        public Token token;
        public bool Upcase = false;

        public BaseScanner(BaseParser parser)
        {
            this.token = new Token(this);
            this.history = new IntegerStack();
            this.def_stack = new IntegerStack();
            this.parser = parser;
            NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
            this.DecimalSeparator = currentInfo.NumberDecimalSeparator;
        }

        public void BackUp()
        {
            this.token.tokenClass = (TokenClass) this.history.Pop();
            this.token.id = this.history.Pop();
            this.token.length = this.history.Pop();
            this.token.position = this.history.Pop();
            this.token.atext = this.token.Text;
            this.pos = this.history.Pop();
            this.line_number = this.history.Pop();
            this.p = this.history.Pop();
        }

        public bool ConditionalDirectivesAreCompleted()
        {
            return (this.def_stack.Count == 0);
        }

        public char GetNextChar()
        {
            this.p++;
            this.pos++;
            return this.buff[this.p];
        }

        public void IncLineNumber()
        {
            this.line_number++;
            this.pos = 0;
            this.parser.GenSeparator();
            this.token.position = this.p + 1;
        }

        internal void Init(BaseScripter scripter, string code)
        {
            this.scripter = scripter;
            this.buff = string.Concat(new object[] { code, '\x001a', '\x001a', '\x001a' });
            this.p = -1;
            this.line_number = 0;
            this.pos = 0;
            this.history.Clear();
            this.def_stack.Clear();
        }

        public static bool IsAlpha(char c)
        {
            return ((((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z'))) || (c == '_'));
        }

        public static bool IsDigit(char c)
        {
            return ((c >= '0') && (c <= '9'));
        }

        public bool IsEOF()
        {
            return (this.LA(0) == '\x001a');
        }

        public bool IsEOF(char c)
        {
            return (c == '\x001a');
        }

        public static bool IsHexDigit(char c)
        {
            return ((IsDigit(c) || ((c >= 'a') && (c <= 'f'))) || ((c >= 'A') && (c <= 'F')));
        }

        public bool IsNewLine(char c)
        {
            return ((((c == '\n') || (c == '\r')) || ((c == '\x0085') || (c == '\u2028'))) || (c == '\u2029'));
        }

        public bool IsWhitespace(char c)
        {
            return ((((c == '\t') || (c == '\v')) || (c == '\f')) || (c == ' '));
        }

        public char LA(int n)
        {
            return this.buff[this.p + n];
        }

        public virtual string ParseString(string s)
        {
            int num = 0;
            string str = "";
        Label_000D:
            if (num >= s.Length)
            {
                return str;
            }
            char ch = s[num];
            if (ch != '\\')
            {
                str = str + ch;
                num++;
            }
            else
            {
                int num2;
                switch (s[num + 1])
                {
                    case '0':
                        num++;
                        str = str + '\0';
                        num++;
                        goto Label_000D;

                    case 'U':
                        num++;
                        num2 = num;
                        while (IsHexDigit(s[num + 1]))
                        {
                            num++;
                            if ((num - num2) == 8)
                            {
                                break;
                            }
                        }
                        if ((num - num2) < 8)
                        {
                            this.RaiseError("CS1009. Unrecognized escape sequence.");
                        }
                        ch = (char) int.Parse(s.Substring(num2 + 1, num - num2), NumberStyles.AllowHexSpecifier);
                        str = str + ch;
                        num++;
                        goto Label_000D;

                    case '"':
                        num++;
                        str = str + '"';
                        num++;
                        goto Label_000D;

                    case '\'':
                    case '\\':
                        num++;
                        str = str + s[num++];
                        goto Label_000D;

                    case 'a':
                        num++;
                        str = str + '\a';
                        num++;
                        goto Label_000D;

                    case 'b':
                        num++;
                        str = str + '\b';
                        num++;
                        goto Label_000D;

                    case 'n':
                        num++;
                        str = str + '\n';
                        num++;
                        goto Label_000D;

                    case 'r':
                        num++;
                        str = str + '\r';
                        num++;
                        goto Label_000D;

                    case 't':
                        num++;
                        str = str + '\t';
                        num++;
                        goto Label_000D;

                    case 'u':
                        num++;
                        num2 = num;
                        while (IsHexDigit(s[num + 1]))
                        {
                            num++;
                            if ((num - num2) == 4)
                            {
                                break;
                            }
                        }
                        break;

                    case 'v':
                        num++;
                        str = str + '\v';
                        num++;
                        goto Label_000D;

                    case 'x':
                        num++;
                        num2 = num;
                        while (IsHexDigit(s[num + 1]))
                        {
                            num++;
                        }
                        if ((num - num2) == 0)
                        {
                            this.RaiseError("CS1009. Unrecognized escape sequence.");
                        }
                        else if ((num - num2) > 4)
                        {
                            this.RaiseError("CS1012. Too many characters in character literal.");
                        }
                        ch = (char) int.Parse(s.Substring(num2 + 1, num - num2), NumberStyles.AllowHexSpecifier);
                        str = str + ch;
                        num++;
                        goto Label_000D;

                    case 'f':
                        num++;
                        str = str + '\f';
                        num++;
                        goto Label_000D;

                    default:
                        this.RaiseError("CS1009. Unrecognized escape sequence.");
                        goto Label_000D;
                }
                if ((num - num2) < 4)
                {
                    this.RaiseError("CS1009. Unrecognized escape sequence.");
                }
                ch = (char) int.Parse(s.Substring(num2 + 1, num - num2), NumberStyles.AllowHexSpecifier);
                str = str + ch;
                num++;
            }
            goto Label_000D;
        }

        public string ParseVerbatimString(string s)
        {
            s = s.Substring(1);
            string str = "\"";
            bool flag = false;
            for (int i = 1; i < (s.Length - 1); i++)
            {
                if (s[i] == '"')
                {
                    if (flag)
                    {
                        flag = false;
                        str = str + s[i];
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    str = str + s[i];
                }
            }
            return (str + "\"");
        }

        public void RaiseError(string message)
        {
            this.scripter.Dump();
            this.scripter.code.n = this.scripter.code.Card;
            this.parser.RaiseError(true, message);
        }

        public void RaiseErrorEx(string message, params object[] p)
        {
            this.scripter.Dump();
            this.scripter.code.n = this.scripter.code.Card;
            this.parser.RaiseErrorEx(true, message, p);
        }

        public virtual void ReadCustomToken()
        {
        }

        public Token ReadToken()
        {
            this.history.Push(this.p);
            this.history.Push(this.line_number);
            this.history.Push(this.pos);
            this.history.Push(this.token.position);
            this.history.Push(this.token.length);
            this.history.Push(this.token.id);
            this.history.Push((int) this.token.tokenClass);
            this.token.position = this.p + 1;
            this.token.tokenClass = TokenClass.None;
            this.ReadCustomToken();
            this.token.length = (this.p - this.token.position) + 1;
            return this.token;
        }

        public void ScanCharLiteral()
        {
            this.token.tokenClass = TokenClass.CharacterConst;
            char nextChar = this.GetNextChar();
            if (nextChar == '\\')
            {
                if (this.LA(1) == 'x')
                {
                    this.ScanHexadecimalEscapeSequence();
                }
                else if ((this.LA(1) == 'u') || (this.LA(1) == 'U'))
                {
                    this.ScanUnicodeEscapeSequence(false);
                }
                else
                {
                    this.ScanSimpleEscapeSequence();
                }
            }
            else if (nextChar == '\'')
            {
                this.RaiseError("CS1011. Empty character literal.");
            }
            else
            {
                this.GetNextChar();
            }
        }

        public string ScanConditionalSymbol()
        {
            this.token.position = this.p;
            this.ScanIdentifier();
            this.token.length = (this.p - this.token.position) + 1;
            this.GetNextChar();
            return this.token.Text;
        }

        private void ScanDigits()
        {
            while (IsDigit(this.LA(1)))
            {
                this.GetNextChar();
            }
        }

        public void ScanEOF()
        {
            this.token.tokenClass = TokenClass.Special;
        }

        private void ScanHexadecimalEscapeSequence()
        {
            this.GetNextChar();
            int p = this.p;
            this.ScanHexDigits();
            if ((this.p - p) == 0)
            {
                this.RaiseError("CS1009. Unrecognized escape sequence.");
            }
            else if ((this.p - p) > 4)
            {
                this.RaiseError("CS1012. Too many characters in character literal.");
            }
            this.GetNextChar();
        }

        private void ScanHexDigits()
        {
            while (IsHexDigit(this.LA(1)))
            {
                this.GetNextChar();
            }
        }

        public void ScanIdentifier()
        {
            if (this.LA(0) == '@')
            {
                this.GetNextChar();
            }
            while ((IsAlpha(this.LA(1)) || IsDigit(this.LA(1))) || (this.LA(1) == '\\'))
            {
                if (this.GetNextChar() == '\\')
                {
                    if (this.LA(1) == 'x')
                    {
                        this.ScanHexadecimalEscapeSequence();
                    }
                    else
                    {
                        if (this.LA(1) == 'u')
                        {
                            this.ScanUnicodeEscapeSequence(true);
                            continue;
                        }
                        if (this.LA(1) == 'U')
                        {
                            this.ScanUnicodeEscapeSequence(true);
                            continue;
                        }
                        this.ScanSimpleEscapeSequence();
                    }
                }
            }
            this.token.tokenClass = TokenClass.Identifier;
        }

        private void ScanIntegerTypeSuffix()
        {
            if ((this.LA(1) == 'u') || (this.LA(1) == 'U'))
            {
                this.GetNextChar();
                if (this.LA(1) == 'l')
                {
                    this.scripter.CreateWarningObject("CS0077. The 'l' suffix is easily confused with the digit '1' -- use 'L' for clarity.");
                    this.GetNextChar();
                }
                else if (this.LA(1) == 'L')
                {
                    this.GetNextChar();
                }
            }
            else if ((this.LA(1) == 'l') || (this.LA(1) == 'L'))
            {
                if (this.LA(1) == 'l')
                {
                    this.scripter.CreateWarningObject("CS0077. The 'l' suffix is easily confused with the digit '1' -- use 'L' for clarity.");
                }
                this.GetNextChar();
                if ((this.LA(1) == 'u') || (this.LA(1) == 'U'))
                {
                    this.GetNextChar();
                }
            }
        }

        public virtual void ScanNewLine()
        {
            int n = this.TestNewLine();
            if (n > 0)
            {
                this.SkipChars(n);
                this.IncLineNumber();
                if (!this.IsNewLine(this.LA(0)))
                {
                    this.p--;
                }
                else
                {
                    this.ScanNewLine();
                }
            }
            this.token.position = this.p + 1;
        }

        public void ScanNumberLiteral()
        {
            if (this.LA(0) == '.')
            {
                this.ScanDigits();
                if ((this.LA(1) == 'e') || (this.LA(1) == 'E'))
                {
                    this.GetNextChar();
                    if ((this.LA(1) == '+') || (this.LA(1) == '-'))
                    {
                        this.GetNextChar();
                    }
                    this.ScanDigits();
                }
                if ((((this.LA(1) == this.SingleCharacter) || (this.LA(1) == this.UpSingleCharacter)) || ((this.LA(1) == this.DoubleCharacter) || (this.LA(1) == this.UpDoubleCharacter))) || ((this.LA(1) == this.DecimalCharacter) || (this.LA(1) == this.UpDecimalCharacter)))
                {
                    this.GetNextChar();
                }
                this.token.tokenClass = TokenClass.RealConst;
            }
            else
            {
                this.ScanDigits();
                if (((this.LA(1) == 'u') || (this.LA(1) == 'U')) || ((this.LA(1) == 'l') || (this.LA(1) == 'L')))
                {
                    this.ScanIntegerTypeSuffix();
                    this.token.tokenClass = TokenClass.IntegerConst;
                }
                else if (((this.LA(1) == 'x') || (this.LA(1) == 'X')) && (this.LA(0) == '0'))
                {
                    this.GetNextChar();
                    this.ScanHexDigits();
                    if (((this.LA(1) == 'u') || (this.LA(1) == 'U')) || ((this.LA(1) == 'l') || (this.LA(1) == 'L')))
                    {
                        this.ScanIntegerTypeSuffix();
                    }
                    this.token.tokenClass = TokenClass.IntegerConst;
                }
                else if (this.LA(1) == '.')
                {
                    if (!IsDigit(this.LA(2)))
                    {
                        this.token.tokenClass = TokenClass.IntegerConst;
                    }
                    else
                    {
                        this.GetNextChar();
                        this.ScanDigits();
                        if ((this.LA(1) == 'e') || (this.LA(1) == 'E'))
                        {
                            this.GetNextChar();
                            if ((this.LA(1) == '+') || (this.LA(1) == '-'))
                            {
                                this.GetNextChar();
                            }
                            this.ScanDigits();
                        }
                        if ((((this.LA(1) == this.SingleCharacter) || (this.LA(1) == this.UpSingleCharacter)) || ((this.LA(1) == this.DoubleCharacter) || (this.LA(1) == this.UpDoubleCharacter))) || ((this.LA(1) == this.DecimalCharacter) || (this.LA(1) == this.UpDecimalCharacter)))
                        {
                            this.GetNextChar();
                        }
                        this.token.tokenClass = TokenClass.RealConst;
                    }
                }
                else if ((this.LA(1) == 'e') || (this.LA(1) == 'E'))
                {
                    this.GetNextChar();
                    if ((this.LA(1) == '+') || (this.LA(1) == '-'))
                    {
                        this.GetNextChar();
                    }
                    this.ScanDigits();
                    if ((((this.LA(1) == this.SingleCharacter) || (this.LA(1) == this.UpSingleCharacter)) || ((this.LA(1) == this.DoubleCharacter) || (this.LA(1) == this.UpDoubleCharacter))) || ((this.LA(1) == this.DecimalCharacter) || (this.LA(1) == this.UpDecimalCharacter)))
                    {
                        this.GetNextChar();
                    }
                    this.token.tokenClass = TokenClass.RealConst;
                }
                else if ((((this.LA(1) == this.SingleCharacter) || (this.LA(1) == this.UpSingleCharacter)) || ((this.LA(1) == this.DoubleCharacter) || (this.LA(1) == this.UpDoubleCharacter))) || ((this.LA(1) == this.DecimalCharacter) || (this.LA(1) == this.UpDecimalCharacter)))
                {
                    this.GetNextChar();
                    this.token.tokenClass = TokenClass.RealConst;
                }
                else
                {
                    this.token.tokenClass = TokenClass.IntegerConst;
                }
            }
        }

        private bool ScanPPAndExpression()
        {
            bool flag = this.ScanPPEqualityExpression();
            this.ScanWhiteSpaces();
            while ((this.LA(0) == '&') && (this.LA(1) == '&'))
            {
                this.GetNextChar();
                this.GetNextChar();
                this.GetNextChar();
                this.ScanWhiteSpaces();
                flag = flag && this.ScanPPEqualityExpression();
            }
            return flag;
        }

        public virtual void ScanPPDirective()
        {
            this.GetNextChar();
            this.ScanWhiteSpaces();
            string str = this.ScanPPDirectiveName();
            if (CSLite_System.CompareStrings(str, "end", this.Upcase))
            {
                this.GetNextChar();
                this.ScanWhiteSpaces();
                str = str + this.ScanPPDirectiveName();
            }
            if (CSLite_System.CompareStrings(str, "define", this.Upcase))
            {
                this.GetNextChar();
                this.ScanWhiteSpaces();
                str = this.ScanConditionalSymbol();
                this.scripter.PPDirectiveList.Add(str);
                this.parser.GenDefine(str);
                this.ScanPPNewLine();
                this.token.tokenClass = TokenClass.None;
                return;
            }
            if (CSLite_System.CompareStrings(str, "undef", this.Upcase))
            {
                this.GetNextChar();
                this.ScanWhiteSpaces();
                str = this.ScanConditionalSymbol();
                int index = this.scripter.PPDirectiveList.IndexOf(str);
                if (index != -1)
                {
                    this.scripter.PPDirectiveList.RemoveAt(index);
                }
                this.parser.GenUndef(str);
                this.ScanPPNewLine();
                this.token.tokenClass = TokenClass.None;
                return;
            }
            if (CSLite_System.CompareStrings(str, "if", this.Upcase))
            {
                this.GetNextChar();
                this.ScanWhiteSpaces();
                bool flag = this.ScanPPExpression();
                this.def_stack.PushObject(1, flag);
                if (!flag)
                {
                    this.ScanSkippedSection();
                    if (this.IsEOF())
                    {
                        this.RaiseError("CS1027. #endif directive expected.");
                    }
                    else
                    {
                        this.ScanPPDirective();
                    }
                }
                else
                {
                    this.ScanPPNewLine();
                    this.token.tokenClass = TokenClass.None;
                }
                return;
            }
            if (CSLite_System.CompareStrings(str, "elif", this.Upcase))
            {
                this.GetNextChar();
                this.ScanWhiteSpaces();
                bool flag2 = this.ScanPPExpression();
                if (this.def_stack.Count == 0)
                {
                    this.RaiseError("CS1028. Unexpected preprocessor directive.");
                }
                int num2 = this.def_stack.Peek();
                flag2 &= !((bool) this.def_stack.PeekObject());
                if ((num2 != 1) && (num2 != 2))
                {
                    this.RaiseError("CS1028. Unexpected preprocessor directive.");
                }
                this.def_stack.PushObject(2, flag2);
                if (!flag2)
                {
                    this.ScanSkippedSection();
                    if (this.IsEOF())
                    {
                        this.RaiseError("CS1027. #endif directive expected.");
                    }
                    else
                    {
                        this.ScanPPDirective();
                    }
                }
                else
                {
                    this.ScanPPNewLine();
                    this.token.tokenClass = TokenClass.None;
                }
                return;
            }
            if (!CSLite_System.CompareStrings(str, "else", this.Upcase))
            {
                if (!CSLite_System.CompareStrings(str, "endif", this.Upcase))
                {
                    if (CSLite_System.CompareStrings(str, "region", this.Upcase))
                    {
                        this.GetNextChar();
                        this.ScanWhiteSpaces();
                        str = this.ScanPPMessage();
                        this.parser.GenStartRegion(str);
                        this.token.tokenClass = TokenClass.None;
                        return;
                    }
                    if (CSLite_System.CompareStrings(str, "endregion", this.Upcase))
                    {
                        this.GetNextChar();
                        this.ScanWhiteSpaces();
                        str = this.ScanPPMessage();
                        this.parser.GenEndRegion(str);
                        this.token.tokenClass = TokenClass.None;
                        return;
                    }
                    if (CSLite_System.CompareStrings(str, "warning", this.Upcase))
                    {
                        this.GetNextChar();
                        this.ScanWhiteSpaces();
                        str = this.ScanPPMessage();
                        this.scripter.CreateWarningObjectEx("CS1030. #warning: '{0}'.", new object[] { str });
                        this.token.tokenClass = TokenClass.None;
                        return;
                    }
                    if (CSLite_System.CompareStrings(str, "error", this.Upcase))
                    {
                        this.GetNextChar();
                        this.ScanWhiteSpaces();
                        str = this.ScanPPMessage();
                        this.scripter.CreateErrorObjectEx("CS1029. #error: '{0}'.", new object[] { str });
                        this.token.tokenClass = TokenClass.None;
                        return;
                    }
                    if (!CSLite_System.CompareStrings(str, "line", this.Upcase))
                    {
                        this.RaiseError("CS1024. Preprocessor directive expected.");
                        return;
                    }
                    this.GetNextChar();
                    this.ScanWhiteSpaces();
                    str = "";
                    if (!IsDigit(this.LA(1)))
                    {
                        while (IsAlpha(this.LA(1)))
                        {
                            str = str + this.GetNextChar();
                        }
                        if (!CSLite_System.CompareStrings(str, "default", this.Upcase))
                        {
                            this.RaiseError("CS1576. The line number specified for #line directive is missing or invalid.");
                        }
                        goto Label_066B;
                    }
                    while (IsDigit(this.LA(1)))
                    {
                        str = str + this.GetNextChar();
                    }
                    if (str == "")
                    {
                        this.RaiseError("CS1576. The line number specified for #line directive is missing or invalid.");
                    }
                    string str2 = "";
                    while (true)
                    {
                        char nextChar = this.GetNextChar();
                        if (this.IsNewLine(nextChar))
                        {
                            this.ScanWhiteSpaces();
                            goto Label_066B;
                        }
                        str2 = str2 + nextChar;
                    }
                }
                this.GetNextChar();
                this.ScanWhiteSpaces();
                int num5 = 0;
                int num6 = 0;
                for (int i = 0; i < this.def_stack.Count; i++)
                {
                    switch (this.def_stack[i])
                    {
                        case 1:
                            num5++;
                            break;

                        case 4:
                            num6++;
                            break;
                    }
                }
                if (num6 >= num5)
                {
                    this.RaiseError("CS1028. Unexpected preprocessor directive.");
                }
                for (int j = this.def_stack.Count - 1; j >= 0; j--)
                {
                    if (this.def_stack[j] == 1)
                    {
                        while (this.def_stack.Count > j)
                        {
                            this.def_stack.Pop();
                        }
                        break;
                    }
                }
                this.ScanPPNewLine();
                this.token.tokenClass = TokenClass.None;
                return;
            }
            this.GetNextChar();
            this.ScanWhiteSpaces();
            if (this.def_stack.Count == 0)
            {
                this.RaiseError("CS1028. Unexpected preprocessor directive.");
            }
            bool anObject = false;
            int num3 = this.def_stack.Count - 1;
            do
            {
                switch (this.def_stack[num3])
                {
                    case 1:
                        if ((bool) this.def_stack.Objects[num3])
                        {
                            anObject = true;
                        }
                        goto Label_0318;

                    case 2:
                        if (!((bool) this.def_stack.Objects[num3]))
                        {
                            break;
                        }
                        anObject = true;
                        goto Label_0318;

                    default:
                        this.RaiseError("CS1028. Unexpected preprocessor directive.");
                        break;
                }
                num3--;
            }
            while (num3 >= 0);
        Label_0318:
            anObject = !anObject;
            this.def_stack.PushObject(3, anObject);
            if (!anObject)
            {
                this.ScanSkippedSection();
                if (this.IsEOF())
                {
                    this.RaiseError("CS1027. #endif directive expected.");
                }
                else
                {
                    this.ScanPPDirective();
                }
            }
            else
            {
                this.ScanPPNewLine();
                this.token.tokenClass = TokenClass.None;
            }
            return;
        Label_066B:
            this.token.tokenClass = TokenClass.None;
        }

        public string ScanPPDirectiveName()
        {
            this.token.position = this.p;
            this.ScanIdentifier();
            this.token.length = (this.p - this.token.position) + 1;
            return this.token.Text;
        }

        private bool ScanPPEqualityExpression()
        {
            bool flag = this.ScanPPUnaryExpression();
            this.ScanWhiteSpaces();
            while (((this.LA(0) == '=') && (this.LA(1) == '=')) || ((this.LA(0) == '!') && (this.LA(1) == '=')))
            {
                bool flag2 = (this.LA(0) == '=') && (this.LA(1) == '=');
                this.GetNextChar();
                this.GetNextChar();
                this.GetNextChar();
                this.ScanWhiteSpaces();
                if (flag2)
                {
                    flag = flag == this.ScanPPUnaryExpression();
                }
                else
                {
                    flag = flag != this.ScanPPUnaryExpression();
                }
            }
            return flag;
        }

        private bool ScanPPExpression()
        {
            this.ScanWhiteSpaces();
            bool flag = this.ScanPPOrExpression();
            this.ScanWhiteSpaces();
            return flag;
        }

        private string ScanPPMessage()
        {
            string str = "";
            while (true)
            {
                char nextChar = this.GetNextChar();
                if (this.IsNewLine(nextChar))
                {
                    break;
                }
                str = str + nextChar;
            }
            this.ScanWhiteSpaces();
            return str;
        }

        private void ScanPPNewLine()
        {
            if (!this.IsNewLine(this.LA(0)))
            {
                char nextChar;
                do
                {
                    nextChar = this.GetNextChar();
                }
                while (!this.IsNewLine(nextChar));
                this.ScanNewLine();
            }
            this.token.position = this.p;
            this.p--;
        }

        private bool ScanPPOrExpression()
        {
            bool flag = this.ScanPPAndExpression();
            this.ScanWhiteSpaces();
            while ((this.LA(0) == '|') && (this.LA(1) == '|'))
            {
                this.GetNextChar();
                this.GetNextChar();
                this.GetNextChar();
                this.ScanWhiteSpaces();
                flag = flag || this.ScanPPAndExpression();
            }
            return flag;
        }

        private bool ScanPPPrimaryExpression()
        {
            if (this.LA(0) == '(')
            {
                this.GetNextChar();
                this.ScanWhiteSpaces();
                bool flag = this.ScanPPExpression();
                this.ScanWhiteSpaces();
                if (this.LA(0) != ')')
                {
                    this.RaiseErrorEx("CS1003. Syntax error, '{0}' expected.", new object[] { ")" });
                }
                this.GetNextChar();
                return flag;
            }
            string s = this.ScanConditionalSymbol();
            switch (s)
            {
                case "true":
                    return true;

                case "false":
                    return false;
            }
            return (this.scripter.PPDirectiveList.IndexOf(s) != -1);
        }

        private bool ScanPPUnaryExpression()
        {
            if (this.LA(0) == '!')
            {
                this.GetNextChar();
                this.ScanWhiteSpaces();
                return !this.ScanPPUnaryExpression();
            }
            return this.ScanPPPrimaryExpression();
        }

        public void ScanRegularStringLiteral(char ch)
        {
            do
            {
                char nextChar = this.GetNextChar();
                if (this.IsNewLine(nextChar))
                {
                    this.RaiseError("CS1010. Newline in constant.");
                }
                if (nextChar == '\\')
                {
                    if (this.LA(1) == 'x')
                    {
                        this.ScanHexadecimalEscapeSequence();
                    }
                    else if (this.LA(1) == 'u')
                    {
                        this.ScanUnicodeEscapeSequence(true);
                    }
                    else if (this.LA(1) == 'U')
                    {
                        this.ScanUnicodeEscapeSequence(true);
                    }
                    else
                    {
                        this.ScanSimpleEscapeSequence();
                    }
                }
            }
            while (this.LA(0) != ch);
            this.token.tokenClass = TokenClass.StringConst;
        }

        private void ScanSimpleEscapeSequence()
        {
            switch (this.GetNextChar())
            {
                case '\'':
                case '\\':
                case '"':
                case '0':
                case 'a':
                case 'b':
                case 'f':
                case 'n':
                case 'r':
                case 't':
                case 'v':
                    this.GetNextChar();
                    break;

                default:
                    this.RaiseError("CS1009. Unrecognized escape sequence.");
                    break;
            }
        }

        public void ScanSingleLineComment()
        {
        Label_0002:
            this.GetNextChar();
            if (!this.IsEOF())
            {
                if (this.TestNewLine() <= 0)
                {
                    goto Label_0002;
                }
                this.ScanNewLine();
            }
            this.token.position = this.p + 1;
        }

        private void ScanSkippedSection()
        {
            do
            {
                if (this.GetNextChar() == '#')
                {
                    return;
                }
                int n = this.TestNewLine();
                if (n > 0)
                {
                    this.SkipChars(n);
                    this.IncLineNumber();
                }
            }
            while (!this.IsEOF(this.LA(1)));
            this.GetNextChar();
            this.ScanEOF();
        }

        public void ScanSpecial()
        {
            this.token.tokenClass = TokenClass.Special;
        }

        private void ScanUnicodeEscapeSequence(bool in_string)
        {
            int num2;
            char nextChar = this.GetNextChar();
            int p = this.p;
            if (in_string)
            {
                if (nextChar == 'u')
                {
                    num2 = 4;
                }
                else
                {
                    num2 = 8;
                }
            }
            else
            {
                num2 = -1;
            }
            int num3 = 0;
            while (IsHexDigit(this.LA(1)))
            {
                this.GetNextChar();
                num3++;
                if (num3 == num2)
                {
                    break;
                }
            }
            switch (nextChar)
            {
                case 'u':
                    if ((this.p - p) < 4)
                    {
                        this.RaiseError("CS1009. Unrecognized escape sequence.");
                    }
                    else if ((this.p - p) > 4)
                    {
                        this.RaiseError("CS1012. Too many characters in character literal.");
                    }
                    break;

                case 'U':
                    if ((this.p - p) < 8)
                    {
                        this.RaiseError("CS1009. Unrecognized escape sequence.");
                    }
                    else if ((this.p - p) > 8)
                    {
                        this.RaiseError("CS1012. Too many characters in character literal.");
                    }
                    break;
            }
            this.GetNextChar();
        }

        public virtual void ScanVerbatimStringLiteral(char ch)
        {
            int num2;
            int position = this.token.position;
            this.GetNextChar();
        Label_0015:
            num2 = this.TestNewLine();
            if (num2 > 0)
            {
                this.SkipChars(num2);
                this.IncLineNumber();
            }
            char nextChar = this.GetNextChar();
            if (this.IsEOF(nextChar))
            {
                this.RaiseError("CS1039. Unterminated string literal.");
            }
            else
            {
                if (nextChar != ch)
                {
                    goto Label_0015;
                }
                if (this.LA(1) == ch)
                {
                    this.GetNextChar();
                    goto Label_0015;
                }
                this.token.tokenClass = TokenClass.StringConst;
                this.token.position = position;
            }
        }

        public void ScanWhiteSpace()
        {
            this.token.position = this.p + 1;
        }

        public void ScanWhiteSpaces()
        {
            while (this.IsWhitespace(this.LA(0)))
            {
                this.GetNextChar();
            }
        }

        public void SkipChars(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                this.GetNextChar();
            }
        }

        public int TestNewLine()
        {
            if (((this.LA(0) == '\n') || (this.LA(0) == '\x0085')) || ((this.LA(0) == '\u2028') || (this.LA(0) == '\u2029')))
            {
                return 1;
            }
            if (this.LA(0) == '\r')
            {
                if (this.LA(1) == '\n')
                {
                    return 1;
                }
                return 1;
            }
            return 0;
        }

        public char CurrChar
        {
            get
            {
                return this.buff[this.p];
            }
        }

        public int CurrCharCode
        {
            get
            {
                return this.buff[this.p];
            }
        }

        public int LineNumber
        {
            get
            {
                return this.line_number;
            }
        }

        public int Pos
        {
            get
            {
                return this.pos;
            }
        }

        public char UpDecimalCharacter
        {
            get
            {
                return char.ToUpper(this.DecimalCharacter);
            }
        }

        public char UpDoubleCharacter
        {
            get
            {
                return char.ToUpper(this.DoubleCharacter);
            }
        }

        public char UpSingleCharacter
        {
            get
            {
                return char.ToUpper(this.SingleCharacter);
            }
        }
    }
}

