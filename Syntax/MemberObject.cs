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

    internal class MemberObject : ScriptObject
    {
        public int Id;
        public int ImplementsId;
        public bool Imported;
        public MemberKind Kind;
        public MemberList Members;
        public ModifierList Modifiers;
        public int NameIndex;
        public int OwnerId;
        public int PCodeLine;

        internal MemberObject(BaseScripter scripter, int id, int owner_id) : base(scripter)
        {
            this.ImplementsId = 0;
            this.PCodeLine = 0;
            this.Modifiers = new ModifierList();
            this.Members = new MemberList(scripter);
            this.Id = id;
            this.OwnerId = owner_id;
            this.Kind = scripter.symbol_table[this.Id].Kind;
            this.NameIndex = scripter.symbol_table[this.Id].NameIndex;
            this.Imported = false;
        }

        public void AddMember(MemberObject m)
        {
            this.Members.Add(m);
        }

        public void AddModifier(Modifier modifier)
        {
            this.Modifiers.Add(modifier);
        }

        public static int CompareAccessibility(MemberObject m1, MemberObject m2)
        {
            if (CSLite_System.PosCh('[', m1.Name) < 0)
            {
                if (CSLite_System.PosCh('[', m2.Name) >= 0)
                {
                    return 0;
                }
                if (m1.HasModifier(Modifier.Public))
                {
                    if (m2.HasModifier(Modifier.Public) || m2.HasModifier(Modifier.Friend))
                    {
                        return 0;
                    }
                    return 1;
                }
                if (m1.HasModifier(Modifier.Friend))
                {
                    if (m2.HasModifier(Modifier.Public) || m2.HasModifier(Modifier.Friend))
                    {
                        return 0;
                    }
                    return 1;
                }
                if (m1.HasModifier(Modifier.Protected))
                {
                    if (m2.HasModifier(Modifier.Public))
                    {
                        return -1;
                    }
                    if (m2.HasModifier(Modifier.Protected))
                    {
                        return 0;
                    }
                    return 1;
                }
                if (m2.HasModifier(Modifier.Public))
                {
                    return -1;
                }
                if (m2.HasModifier(Modifier.Protected))
                {
                    return -1;
                }
            }
            return 0;
        }

        public virtual MemberObject GetInstanceMemberByNameIndex(int name_index, bool upcase)
        {
            for (int i = 0; i < this.Members.Count; i++)
            {
                MemberObject obj2 = this.Members[i];
                if ((obj2.NameIndex == name_index) && !obj2.HasModifier(Modifier.Static))
                {
                    return obj2;
                }
            }
            if (upcase)
            {
                string upcaseNameByNameIndex = base.Scripter.GetUpcaseNameByNameIndex(name_index);
                for (int j = 0; j < this.Members.Count; j++)
                {
                    MemberObject obj3 = this.Members[j];
                    if (!obj3.HasModifier(Modifier.Static) && (obj3.UpcaseName == upcaseNameByNameIndex))
                    {
                        return obj3;
                    }
                }
            }
            return null;
        }

        public MemberObject GetMember(int id)
        {
            for (int i = 0; i < this.Members.Count; i++)
            {
                MemberObject obj2 = this.Members[i];
                if (obj2.Id == id)
                {
                    return obj2;
                }
            }
            return null;
        }

        internal virtual MemberObject GetMemberByNameIndex(int name_index, bool upcase)
        {
            for (int i = 0; i < this.Members.Count; i++)
            {
                MemberObject obj2 = this.Members[i];
                if (obj2.NameIndex == name_index)
                {
                    return obj2;
                }
            }
            if (upcase)
            {
                string upcaseNameByNameIndex = base.Scripter.GetUpcaseNameByNameIndex(name_index);
                for (int j = 0; j < this.Members.Count; j++)
                {
                    MemberObject obj3 = this.Members[j];
                    if (obj3.UpcaseName == upcaseNameByNameIndex)
                    {
                        return obj3;
                    }
                }
            }
            return null;
        }

        public virtual MemberObject GetStaticMemberByNameIndex(int name_index, bool upcase)
        {
            for (int i = 0; i < this.Members.Count; i++)
            {
                MemberObject obj2 = this.Members[i];
                if ((obj2.NameIndex == name_index) && obj2.HasModifier(Modifier.Static))
                {
                    return obj2;
                }
            }
            if (upcase)
            {
                string upcaseNameByNameIndex = base.Scripter.GetUpcaseNameByNameIndex(name_index);
                for (int j = 0; j < this.Members.Count; j++)
                {
                    MemberObject obj3 = this.Members[j];
                    if (obj3.HasModifier(Modifier.Static) && (obj3.UpcaseName == upcaseNameByNameIndex))
                    {
                        return obj3;
                    }
                }
            }
            return null;
        }

        public bool HasModifier(Modifier m)
        {
            return this.Modifiers.HasModifier(m);
        }

        public string FullName
        {
            get
            {
                return base.Scripter.symbol_table[this.Id].FullName;
            }
        }

        public bool IsSub
        {
            get
            {
                return (((this.Kind == MemberKind.Method) || (this.Kind == MemberKind.Constructor)) || (this.Kind == MemberKind.Destructor));
            }
        }

        public string Name
        {
            get
            {
                return base.Scripter.symbol_table[this.Id].Name;
            }
        }

        public bool Private
        {
            get
            {
                return ((!this.HasModifier(Modifier.Protected) && !this.HasModifier(Modifier.Public)) && !this.HasModifier(Modifier.Friend));
            }
        }

        public bool Protected
        {
            get
            {
                return this.HasModifier(Modifier.Protected);
            }
        }

        public bool Public
        {
            get
            {
                return this.HasModifier(Modifier.Public);
            }
        }

        public bool Static
        {
            get
            {
                return this.HasModifier(Modifier.Static);
            }
        }

        public string UpcaseName
        {
            get
            {
                return base.Scripter.GetUpcaseNameByNameIndex(this.NameIndex);
            }
        }
    }
}

