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
    using System.IO;

    public class Module
    {
        private MemoryStream buff_stream = null;
        private string file_name = "";
        private string language;
        private string name;
        private int name_index;
        public int P1 = 0;
        public int P2 = 0;
        public int S1 = 0;
        public int S2 = 0;
        private BaseScripter scripter;
        private string text = "";

        internal Module(BaseScripter scripter, string name, string language)
        {
            this.name = name;
            this.language = language;
            this.scripter = scripter;
            this.name_index = scripter.names.Add(name);
        }

        internal void AfterCompile()
        {
            this.S2 = this.scripter.symbol_table.Card;
            this.P2 = this.scripter.code.Card;
        }

        internal void BeforeCompile()
        {
            this.S1 = this.scripter.symbol_table.Card + 1;
            this.P1 = this.scripter.code.Card + 1;
        }

        internal string GetLine(int line_number)
        {
            StringList list = new StringList(true);
            list.text = this.text;
            if ((line_number >= 0) && (line_number < list.Count))
            {
                return list[line_number];
            }
            return "";
        }

        internal bool IsInternalId(int id)
        {
            return ((id >= this.S1) && (id <= this.S2));
        }

        internal void LoadFromStream()
        {
            this.buff_stream.Position = 0L;
            BinaryReader br = new BinaryReader(this.buff_stream);
            this.language = br.ReadString();
            this.file_name = br.ReadString();
            this.S1 = br.ReadInt32();
            this.S2 = br.ReadInt32();
            this.P1 = br.ReadInt32();
            this.P2 = br.ReadInt32();
            int ds = (this.scripter.symbol_table.Card - this.S1) + 1;
            int dp = (this.scripter.code.Card - this.P1) + 1;
            this.scripter.code.LoadFromStream(br, this, ds, dp);
            this.scripter.symbol_table.LoadFromStream(br, this, ds, dp);
            br.Close();
            this.buff_stream.Close();
        }

        internal void PreLoadFromStream(Stream s)
        {
            this.buff_stream = new MemoryStream();
            for (int i = 0; i < s.Length; i++)
            {
                int num2 = s.ReadByte();
                this.buff_stream.WriteByte((byte) num2);
            }
        }

        internal void SaveToStream(Stream s)
        {
            BinaryWriter bw = new BinaryWriter(s);
            bw.Write(this.language);
            bw.Write(this.file_name);
            bw.Write(this.S1);
            bw.Write(this.S2);
            bw.Write(this.P1);
            bw.Write(this.P2);
            this.scripter.code.SaveToStream(bw, this);
            this.scripter.symbol_table.SaveToStream(bw, this);
            bw.Close();
        }

        public string FileName
        {
            get
            {
                return this.file_name;
            }
            set
            {
                this.file_name = value;
            }
        }

        internal bool IsSourceCodeModule
        {
            get
            {
                return (this.buff_stream != null);
            }
        }

        public  CSLite_Language Language
        {
            get
            {
                if (CSLite_System.StrEql(this.language, "CSharp"))
                {
                    return  CSLite_Language.CSharp;
                }
                if (CSLite_System.StrEql(this.language, "Pascal"))
                {
                    return  CSLite_Language.Pascal;
                }
                return  CSLite_Language.VB;
            }
        }

        public string LanguageName
        {
            get
            {
                return this.language;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        internal int NameIndex
        {
            get
            {
                return this.name_index;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

