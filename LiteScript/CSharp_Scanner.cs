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

    internal class CSharp_Scanner : BaseScanner
    {
        public CSharp_Scanner(BaseParser parser) : base(parser)
        {
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
            else if (base.IsNewLine(ch))
            {
                this.ScanNewLine();
            }
            else if (base.IsEOF(ch))
            {
                base.ScanEOF();
            }
            else if (BaseScanner.IsAlpha(ch))
            {
                base.ScanIdentifier();
            }
            else if (ch == '@')
            {
                if (base.LA(1) == '"')
                {
                    this.ScanVerbatimStringLiteral('"');
                }
                else
                {
                    base.ScanIdentifier();
                }
            }
            else if (ch == '"')
            {
                base.ScanRegularStringLiteral('"');
            }
            else if (BaseScanner.IsDigit(ch))
            {
                base.ScanNumberLiteral();
            }
            else if (ch == '\'')
            {
                base.ScanCharLiteral();
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
            else
            {
                switch (ch)
                {
                    case '(':
                        base.ScanSpecial();
                        goto Label_045B;

                    case ')':
                        base.ScanSpecial();
                        goto Label_045B;

                    case '[':
                        base.ScanSpecial();
                        goto Label_045B;

                    case ']':
                        base.ScanSpecial();
                        goto Label_045B;

                    case '{':
                        base.ScanSpecial();
                        goto Label_045B;

                    case '}':
                        base.ScanSpecial();
                        goto Label_045B;

                    case '=':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '.':
                        if (BaseScanner.IsDigit(base.LA(1)))
                        {
                            base.ScanNumberLiteral();
                        }
                        else
                        {
                            base.ScanSpecial();
                        }
                        goto Label_045B;

                    case '~':
                        base.ScanSpecial();
                        goto Label_045B;

                    case '+':
                        if (base.LA(1) == '+')
                        {
                            base.GetNextChar();
                        }
                        else if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '-':
                        if (base.LA(1) == '-')
                        {
                            base.GetNextChar();
                        }
                        else if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '*':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '/':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        else
                        {
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
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case ';':
                        base.ScanSpecial();
                        goto Label_045B;

                    case '%':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '&':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        else if (base.LA(1) == '&')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '|':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        else if (base.LA(1) == '|')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '^':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '<':
                        if (base.LA(1) == '<')
                        {
                            base.GetNextChar();
                        }
                        else if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

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
                        goto Label_045B;

                    case '!':
                        if (base.LA(1) == '=')
                        {
                            base.GetNextChar();
                        }
                        base.ScanSpecial();
                        goto Label_045B;

                    case '#':
                        this.ScanPPDirective();
                        goto Label_0005;
                }
                base.RaiseError("Syntax error.");
            }
        Label_045B:
            if (base.token.tokenClass == TokenClass.None)
            {
                goto Label_0005;
            }
        }
    }
}

