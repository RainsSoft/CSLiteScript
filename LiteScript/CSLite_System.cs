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
    using System.Runtime.InteropServices;

    public class CSLite_System
    {
        public static bool CompareStrings(string s1, string s2, bool upcase)
        {
            if (upcase)
            {
                return (s1.ToUpper() == s2.ToUpper());
            }
            return (s1 == s2);
        }

        public static string ExtractName(string s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '.')
                {
                    return s.Substring(i + 1);
                }
            }
            return s;
        }

        public static string ExtractOwner(string s, out char c)
        {
            c = '.';
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if ((s[i] == '.') || (s[i] == '+'))
                {
                    c = s[i];
                    return s.Substring(0, i);
                }
            }
            return "";
        }

        public static string ExtractPath(string s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '\\')
                {
                    return s.Substring(0, i + 1);
                }
            }
            return "";
        }

        public static string ExtractPrefixName(string s, out int p)
        {
            for (int i = 0; i < (s.Length - 1); i++)
            {
                char ch = s[i];
                if (ch == '[')
                {
                    p = i;
                    return s.Substring(0, i);
                }
            }
            p = -1;
            return s;
        }

        public static string GetElementTypeName(string array_type_name)
        {
            if (array_type_name == "String")
            {
                return "Char";
            }
            int length = PosCh('[', array_type_name);
            int num2 = PosCh(']', array_type_name);
            if ((length == -1) || (num2 == -1))
            {
                return "";
            }
            string str = array_type_name.Substring(0, length);
            if (num2 < (array_type_name.Length - 1))
            {
                str = str + array_type_name.Substring(num2 + 1);
            }
            return str;
        }

        public static int GetRank(string array_type_name)
        {
            if (PosCh('[', array_type_name) == -1)
            {
                return 0;
            }
            bool flag = false;
            int num = 1;
            for (int i = 0; i < array_type_name.Length; i++)
            {
                char ch = array_type_name[i];
                if (ch == '[')
                {
                    flag = true;
                }
                else if (ch == ']')
                {
                    return num;
                }
                if ((ch == ',') && flag)
                {
                    num++;
                }
            }
            return num;
        }

        public static string Norm(object s, int l)
        {
            string str = s.ToString();
            while (str.Length < l)
            {
                str = " " + str;
            }
            if (str.Length > l)
            {
                str = str.Substring(0, l);
            }
            return str;
        }

        public static int PosCh(char c, string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c)
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool StrEql(string s1, string s2)
        {
            return CompareStrings(s1, s2, true);
        }
    }
}

