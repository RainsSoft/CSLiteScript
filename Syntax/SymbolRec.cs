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

    internal class SymbolRec
    {
        private int block;
        public ProgRec CodeProgRec = null;
        public int Count = 0;
        private int id;
        private bool is_forward = false;
        public bool is_static = false;
        private MemberKind kind;
        private int level;
        private int name_index;
        private BaseScripter scripter;
        private int type_id;
        private ArrayList value;
        internal int value_level = 0;

        internal SymbolRec(BaseScripter scripter, int id)
        {
            this.scripter = scripter;
            this.id = id;
            this.value = new ArrayList();
            this.value.Add(null);
            this.value_level = 0;
            this.level = 0;
            this.block = 0;
        }

        public virtual void DecValueLevel()
        {
            this.value_level--;
        }

        private int GetParamId(int index)
        {
            int num = -1;
            for (int i = this.scripter.symbol_table.GetThisId(this.Id) + 1; i < this.scripter.symbol_table.Card; i++)
            {
                if (this.scripter.symbol_table[i].Level == this.Id)
                {
                    num++;
                    if (num == index)
                    {
                        return this.id;
                    }
                }
            }
            return 0;
        }

        public string GetSignature()
        {
            string str = "";
            if (this.Kind != MemberKind.Method)
            {
                return str;
            }
            str = "(";
            for (int i = 0; i < this.Count; i++)
            {
                int paramId = this.GetParamId(i);
                int typeId = this.scripter.symbol_table[paramId].TypeId;
                str = str + this.scripter.symbol_table[typeId].Name;
                if (i < (this.Count - 1))
                {
                    str = str + ",";
                }
            }
            return (str + ")");
        }

        public virtual void IncValueLevel()
        {
            this.value_level++;
            while (this.value_level >= this.value.Count)
            {
                this.value.Add(null);
            }
        }

        public void LoadFromStream(BinaryReader br)
        {
            this.id = br.ReadInt32();
            this.Name = br.ReadString();
            this.level = br.ReadInt32();
            this.type_id = br.ReadInt32();
            this.kind = (MemberKind) br.ReadInt32();
            if (this.kind == MemberKind.Label)
            {
                this.Val = br.ReadInt32();
            }
            else if (((this.kind == MemberKind.Const) || (this.kind == MemberKind.Var)) && br.ReadBoolean())
            {
                if (this.type_id == 2)
                {
                    this.Val = br.ReadBoolean();
                }
                else if (this.type_id == 3)
                {
                    this.Val = br.ReadByte();
                }
                else if (this.type_id == 4)
                {
                    this.Val = br.ReadChar();
                }
                else if (this.type_id != 5)
                {
                    if (this.type_id == 6)
                    {
                        this.Val = br.ReadDouble();
                    }
                    else if (this.type_id == 7)
                    {
                        this.Val = br.ReadSingle();
                    }
                    else if (this.type_id == 8)
                    {
                        this.Val = br.ReadInt32();
                    }
                    else if (this.type_id == 9)
                    {
                        this.Val = br.ReadInt64();
                    }
                    else if (this.type_id == 10)
                    {
                        this.Val = br.ReadSByte();
                    }
                    else if (this.type_id == 11)
                    {
                        this.Val = br.ReadInt16();
                    }
                    else if (this.type_id == 12)
                    {
                        this.Val = br.ReadString();
                    }
                    else if (this.type_id == 13)
                    {
                        this.Val = br.ReadUInt32();
                    }
                    else if (this.type_id == 14)
                    {
                        this.Val = br.ReadUInt64();
                    }
                    else if (this.type_id == 15)
                    {
                        this.Val = br.ReadUInt16();
                    }
                }
            }
        }

        public void SaveToStream(BinaryWriter bw)
        {
            bw.Write(this.id);
            bw.Write(this.Name);
            bw.Write(this.level);
            bw.Write(this.type_id);
            bw.Write((int) this.kind);
            if (this.kind == MemberKind.Label)
            {
                bw.Write((int) this.Val);
            }
            else if ((this.kind == MemberKind.Const) || (this.kind == MemberKind.Var))
            {
                bool flag = this.Val != null;
                bw.Write(flag);
                if (flag)
                {
                    if (this.type_id == 2)
                    {
                        bw.Write(this.ValueAsBool);
                    }
                    else if (this.type_id == 3)
                    {
                        bw.Write(this.ValueAsByte);
                    }
                    else if (this.type_id == 4)
                    {
                        bw.Write(this.ValueAsChar);
                    }
                    else if (this.type_id != 5)
                    {
                        if (this.type_id == 6)
                        {
                            bw.Write(this.ValueAsDouble);
                        }
                        else if (this.type_id == 7)
                        {
                            bw.Write(this.ValueAsFloat);
                        }
                        else if (this.type_id == 8)
                        {
                            bw.Write(this.ValueAsInt);
                        }
                        else if (this.type_id == 9)
                        {
                            bw.Write(this.ValueAsLong);
                        }
                        else if (this.type_id == 10)
                        {
                            bw.Write(this.ValueAsByte);
                        }
                        else if (this.type_id == 11)
                        {
                            bw.Write(this.ValueAsShort);
                        }
                        else if (this.type_id == 12)
                        {
                            bw.Write(this.ValueAsString);
                        }
                        else if (this.type_id == 13)
                        {
                            bw.Write(this.ValueAsInt);
                        }
                        else if (this.type_id == 14)
                        {
                            bw.Write(this.ValueAsLong);
                        }
                        else if (this.type_id == 15)
                        {
                            bw.Write(this.ValueAsShort);
                        }
                    }
                }
            }
        }

        public int Block
        {
            get
            {
                return this.block;
            }
            set
            {
                this.block = value;
            }
        }

        public string FullName
        {
            get
            {
                if (this.Name != "System")
                {
                    if (this.kind == MemberKind.Type)
                    {
                        if (this.id == 0)
                        {
                            return "";
                        }
                        if (this.id == this.scripter.symbol_table.ROOT_NAMESPACE_id)
                        {
                            return this.Name;
                        }
                        if (this.id == this.scripter.symbol_table.SYSTEM_NAMESPACE_id)
                        {
                            return this.Name;
                        }
                        string fullName = this.scripter.symbol_table[this.level].FullName;
                        if (fullName == "")
                        {
                            return this.Name;
                        }
                        return (fullName + "." + this.Name);
                    }
                    if ((this.kind == MemberKind.Method) || (this.kind == MemberKind.Ref))
                    {
                        string str2 = this.scripter.symbol_table[this.level].FullName;
                        if (str2 == "")
                        {
                            return this.Name;
                        }
                        return (str2 + "." + this.Name);
                    }
                }
                return this.Name;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public bool IsForward
        {
            get
            {
                return this.is_forward;
            }
            set
            {
                this.is_forward = value;
            }
        }

        public MemberKind Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        public string Name
        {
            get
            {
                return this.scripter.names[this.name_index];
            }
            set
            {
                this.name_index = this.scripter.names.Add(value);
            }
        }

        public int NameIndex
        {
            get
            {
                return this.name_index;
            }
            set
            {
                this.name_index = value;
            }
        }

        public int TypeId
        {
            get
            {
                return this.type_id;
            }
            set
            {
                this.type_id = value;
            }
        }

        public virtual object Val
        {
            get
            {
                return this.value[this.value_level];
            }
            set
            {
                this.value[this.value_level] = value;
            }
        }

        public virtual object Value
        {
            get
            {
                switch (this.kind)
                {
                    case MemberKind.Field:
                    {
                        FieldObject obj2 = (FieldObject) this.value[this.value_level];
                        if (obj2 != null)
                        {
                            return obj2.Value;
                        }
                        return null;
                    }
                    case MemberKind.Property:
                    {
                        PropertyObject obj3 = (PropertyObject) this.value[this.value_level];
                        if (obj3 != null)
                        {
                            return obj3.Value;
                        }
                        return null;
                    }
                    case MemberKind.Index:
                    {
                        IndexObject obj5 = (IndexObject) this.value[this.value_level];
                        if (obj5 != null)
                        {
                            return obj5.Value;
                        }
                        return null;
                    }
                    case MemberKind.Label:
                        return this.value[this.value_level];

                    case MemberKind.Ref:
                    {
                        ObjectObject obj4 = (ObjectObject) this.value[this.value_level];
                        if (obj4 == null)
                        {
                            return null;
                        }
                        int typeId = this.scripter.symbol_table[this.Level].TypeId;
                        return obj4.GetProperty(this.name_index, typeId);
                    }
                }
                return this.value[this.value_level];
            }
            set
            {
                switch (this.kind)
                {
                    case MemberKind.Field:
                    {
                        FieldObject obj2 = (FieldObject) this.value[this.value_level];
                        obj2.Value = value;
                        return;
                    }
                    case MemberKind.Property:
                    {
                        PropertyObject obj3 = (PropertyObject) this.value[this.value_level];
                        obj3.Value = value;
                        return;
                    }
                    case MemberKind.Index:
                    {
                        IndexObject obj5 = (IndexObject) this.value[this.value_level];
                        obj5.Value = value;
                        return;
                    }
                    case MemberKind.Label:
                        if (value.GetType() != typeof(ProgRec))
                        {
                            this.value[this.value_level] = value;
                            return;
                        }
                        this.CodeProgRec = (ProgRec) value;
                        return;

                    case MemberKind.Ref:
                    {
                        ObjectObject obj4 = (ObjectObject) this.value[this.value_level];
                        int typeId = this.scripter.symbol_table[this.Level].TypeId;
                        obj4.PutProperty(this.name_index, typeId, value);
                        return;
                    }
                }
                this.value[this.value_level] = value;
            }
        }

        public virtual bool ValueAsBool
        {
            get
            {
                return ConvertHelper.ToBoolean(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual byte ValueAsByte
        {
            get
            {
                return ConvertHelper.ToByte(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual char ValueAsChar
        {
            get
            {
                return ConvertHelper.ToChar(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public ClassObject ValueAsClassObject
        {
            get
            {
                return (ClassObject) this.Val;
            }
        }

        public virtual decimal ValueAsDecimal
        {
            get
            {
                return ConvertHelper.ToDecimal(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual double ValueAsDouble
        {
            get
            {
                return ConvertHelper.ToDouble(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual float ValueAsFloat
        {
            get
            {
                return ConvertHelper.ToFloat(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual int ValueAsInt
        {
            get
            {
                return ConvertHelper.ToInt(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual long ValueAsLong
        {
            get
            {
                return ConvertHelper.ToLong(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual short ValueAsShort
        {
            get
            {
                return ConvertHelper.ToShort(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual string ValueAsString
        {
            get
            {
                return ConvertHelper.ToString(this.Value);
            }
            set
            {
                this.Value = value;
            }
        }
    }
}

