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

    public class StringList : CSLite_TypedList
    {
        private Hashtable ht;

        public StringList(bool dupyes) : base(dupyes)
        {
            this.ht = new Hashtable();
        }

        public int Add(string avalue)
        {
            if (base.DupYes)
            {
                base.Items.Add(avalue);
                base.Objects.Add(null);
                return (base.Items.Count - 1);
            }
            int index = this.IndexOf(avalue);
            if (index == -1)
            {
                this.ht.Add(avalue, base.Items.Count);
                base.Items.Add(avalue);
                base.Objects.Add(null);
                return (base.Items.Count - 1);
            }
            return index;
        }

        public int AddObject(string avalue, object anObject)
        {
            int num = this.Add(avalue);
            base.Objects[num] = anObject;
            return num;
        }

        public void Clear()
        {
            base.Clear();
            this.ht.Clear();
        }

        public StringList Clone()
        {
            StringList list = new StringList(base.DupYes);
            for (int i = 0; i < base.Count; i++)
            {
                list.AddObject(base.Items[i], base.Objects[i]);
            }
            return list;
        }

        public void Delete(int index)
        {
            if ((index >= 0) && (index < base.Count))
            {
                base.Items.RemoveAt(index);
                base.Objects.RemoveAt(index);
                this.ht.Clear();
                for (int i = 0; i < base.Count; i++)
                {
                    this.ht.Add(base.Items[i], i);
                }
            }
        }

        public void Dump(string path)
        {
        }

        public int IndexOf(string s)
        {
            if (base.DupYes)
            {
                for (int i = 0; i < base.Items.Count; i++)
                {
                    if (base.Items[i].ToString() == s)
                    {
                        return i;
                    }
                }
                return -1;
            }
            object obj2 = this.ht[s];
            if (obj2 == null)
            {
                return -1;
            }
            return (int) obj2;
        }

        public void LoadFromFile(string path)
        {
            this.Clear();
            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.Peek() >= 0)
                {
                    string avalue = reader.ReadLine();
                    this.Add(avalue);
                }
            }
        }

        public void SaveToFile(string path)
        {
            StreamWriter writer = new StreamWriter(path,false,System.Text.Encoding.Default); //File.CreateText(path);
            for (int i = 0; i < base.Count; i++)
            {
                writer.WriteLine(base.Items[i].ToString());
            }
            writer.Close();
        }

        public int UpcaseIndexOf(string s)
        {
            string str = s.ToUpper();
            for (int i = 0; i < base.Items.Count; i++)
            {
                if (base.Items[i].ToString().ToUpper() == str)
                {
                    return i;
                }
            }
            return -1;
        }

        public string this[int index]
        {
            get
            {
                if (base.Items[index] == null)
                {
                    return "*null";
                }
                return base.Items[index].ToString();
            }
        }

        public string text
        {
            get
            {
                string str = "";
                for (int i = 0; i < base.Count; i++)
                {
                    str = str + base.Items[i].ToString() + "\n\r";
                }
                return str;
            }
            set
            {
                string str;
                this.Clear();
                int length = value.Length;
                if (length == 0)
                {
                    return;
                }
                int num2 = 0;
                int startIndex = num2;
                do
                {
                    switch (value[num2])
                    {
                        case '\r':
                        case '\n':
                            str = value.Substring(startIndex, num2 - startIndex);
                            this.Add(str);
                            num2++;
                            if (num2 >= length)
                            {
                                goto Label_0083;
                            }
                            if (value[num2] == '\r')
                            {
                                num2++;
                            }
                            else if (value[num2] == '\n')
                            {
                                num2++;
                                this.Add("");
                            }
                            startIndex = num2;
                            break;

                        default:
                            num2++;
                            break;
                    }
                }
                while (num2 < length);
            Label_0083:
                if (startIndex < num2)
                {
                    str = value.Substring(startIndex, num2 - startIndex);
                    this.Add(str);
                }
            }
        }
    }
}

