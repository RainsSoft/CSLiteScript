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

    internal class VB_Scanner : BaseScanner
    {
        public VB_Scanner(BaseParser parser) : base(parser)
        {
            base.SingleCharacter = 'f';
            base.DoubleCharacter = 'r';
            base.DecimalCharacter = 'd';
            base.Upcase = true;
        }

        public override string ParseString(string s)
        {
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

        public override void ReadCustomToken()
        {
            char ch;
        Label_0005:
            ch = base.GetNextChar();
            if (base.IsWhitespace(ch))
            {
                base.ScanWhiteSpace();
            }
            else
            {
                if (base.IsNewLine(ch))
                {
                    return;
                }
                if (base.IsEOF(ch))
                {
                    base.ScanEOF();
                }
                else if (BaseScanner.IsAlpha(ch))
                {
                    if ((((base.LA(0) == 'R') && (base.LA(1) == 'E')) && ((base.LA(2) == 'M') && !BaseScanner.IsAlpha(base.LA(3)))) && !BaseScanner.IsDigit(base.LA(3)))
                    {
                        base.ScanSingleLineComment();
                    }
                    else if ((base.LA(0) == '_') && !BaseScanner.IsAlpha(base.LA(1)))
                    {
                        base.ScanSpecial();
                    }
                    else
                    {
                        base.ScanIdentifier();
                    }
                }
                else if (ch == '@')
                {
                    if (base.LA(1) == '"')
                    {
                        this.ScanVerbatimStringLiteral('"');
                    }
                    else
                    {
                        base.ScanSpecial();
                    }
                }
                else if (ch == '"')
                {
                    this.ScanVerbatimStringLiteral('"');
                }
                else if (BaseScanner.IsDigit(ch))
                {
                    base.ScanNumberLiteral();
                }
                else if (ch == '\'')
                {
                    base.ScanSingleLineComment();
                }
                else if (ch == '‘')
                {
                    base.ScanSingleLineComment();
                }
                else if (ch == '’')
                {
                    base.ScanSingleLineComment();
                }
                else if (ch == '(')
                {
                    base.ScanSpecial();
                }
                else if (ch == ')')
                {
                    base.ScanSpecial();
                }
                else if (ch == ',')
                {
                    base.ScanSpecial();
                }
                else if (ch == ':')
                {
                    base.ScanSpecial();
                }
                else if (ch == '?')
                {
                    base.ScanSpecial();
                }
                else if (ch == ';')
                {
                    base.ScanSpecial();
                }
                else
                {
                    switch (ch)
                    {
                        case '(':
                            base.ScanSpecial();
                            goto Label_0409;

                        case ')':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '{':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '}':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '[':
                            base.ScanSpecial();
                            goto Label_0409;

                        case ']':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '=':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '+':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '-':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '*':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '\\':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '.':
                            if (BaseScanner.IsDigit(base.LA(1)))
                            {
                                base.ScanNumberLiteral();
                            }
                            else
                            {
                                base.ScanSpecial();
                            }
                            goto Label_0409;

                        case '/':
                            if (base.LA(1) == '*')
                            {
                                do
                                {
                                    base.GetNextChar();
                                    int n = base.TestNewLine();
                                    if (n > 0)
                                    {
                                        base.SkipChars(n);
                                        base.IncLineNumber();
                                    }
                                    if (base.IsEOF(base.LA(1)))
                                    {
                                        base.RaiseError("CS1035. End-of-file found, '*/' expected.");
                                        return;
                                    }
                                }
                                while ((base.LA(1) != '*') || (base.LA(2) != '/'));
                                base.SkipChars(2);
                                goto Label_0005;
                            }
                            if (base.LA(1) == '/')
                            {
                                base.ScanSingleLineComment();
                                goto Label_0005;
                            }
                            base.ScanSpecial();
                            goto Label_0409;

                        case '~':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '%':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '&':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '|':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '^':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '<':
                            if (base.LA(1) == '<')
                            {
                                base.GetNextChar();
                            }
                            else if (base.LA(1) == '=')
                            {
                                base.GetNextChar();
                            }
                            else if (base.LA(1) == '>')
                            {
                                base.GetNextChar();
                            }
                            base.ScanSpecial();
                            goto Label_0409;

                        case '>':
                            if (base.LA(1) == '>')
                            {
                                base.GetNextChar();
                            }
                            else if (base.LA(1) == '=')
                            {
                                base.GetNextChar();
                            }
                            base.ScanSpecial();
                            goto Label_0409;

                        case '!':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '$':
                            base.ScanSpecial();
                            goto Label_0409;

                        case '#':
                            this.ScanPPDirective();
                            goto Label_0005;
                    }
                    base.RaiseError("Syntax error.");
                }
            }
        Label_0409:
            if (base.token.tokenClass == TokenClass.None)
            {
                goto Label_0005;
            }
        }

        public override void ScanVerbatimStringLiteral(char ch)
        {
            int num2;
            int position = base.token.position;
        Label_000E:
            num2 = base.TestNewLine();
            if (num2 > 0)
            {
                base.SkipChars(num2);
                base.IncLineNumber();
            }
            char nextChar = base.GetNextChar();
            if (base.IsEOF(nextChar))
            {
                base.RaiseError("CS1039. Unterminated string literal.");
            }
            else
            {
                if (nextChar != ch)
                {
                    goto Label_000E;
                }
                if (base.LA(1) == ch)
                {
                    base.GetNextChar();
                    goto Label_000E;
                }
                base.token.tokenClass = TokenClass.StringConst;
                base.token.position = position;
            }
        }
    }
}

