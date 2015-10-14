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
    using System.Reflection;

    internal class ModifierList
    {
        private int count = 0;
        private Modifier[] items = new Modifier[0x13];

        public void Add(Modifier m)
        {
            if (!this.HasModifier(m))
            {
                this.items[this.count] = m;
                this.count++;
            }
        }

        public ModifierList Clone()
        {
            ModifierList list = new ModifierList();
            for (int i = 0; i < this.Count; i++)
            {
                list.Add(this[i]);
            }
            return list;
        }

        public void Delete(Modifier m)
        {
            int num = -1;
            for (int i = 0; i < this.count; i++)
            {
                if (this.items[i] == m)
                {
                    num = i;
                    break;
                }
            }
            for (int j = this.count - 2; j >= num; j--)
            {
                this.items[j] = this.items[j + 1];
            }
            this.count--;
        }

        public bool HasModifier(Modifier m)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (this.items[i] == m)
                {
                    return true;
                }
            }
            return false;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public Modifier this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                this.items[index] = value;
            }
        }
    }
}

