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

    internal sealed class ConvertHelper
    {
        private Contact_Integers implicit_numeric_conversions = new Contact_Integers(100);

        public ConvertHelper()
        {
            this.implicit_numeric_conversions.Add(10, 11);
            this.implicit_numeric_conversions.Add(10, 8);
            this.implicit_numeric_conversions.Add(10, 9);
            this.implicit_numeric_conversions.Add(10, 7);
            this.implicit_numeric_conversions.Add(10, 6);
            this.implicit_numeric_conversions.Add(10, 5);
            this.implicit_numeric_conversions.Add(3, 11);
            this.implicit_numeric_conversions.Add(3, 15);
            this.implicit_numeric_conversions.Add(3, 8);
            this.implicit_numeric_conversions.Add(3, 13);
            this.implicit_numeric_conversions.Add(3, 9);
            this.implicit_numeric_conversions.Add(3, 14);
            this.implicit_numeric_conversions.Add(3, 7);
            this.implicit_numeric_conversions.Add(3, 6);
            this.implicit_numeric_conversions.Add(3, 5);
            this.implicit_numeric_conversions.Add(11, 8);
            this.implicit_numeric_conversions.Add(11, 9);
            this.implicit_numeric_conversions.Add(11, 7);
            this.implicit_numeric_conversions.Add(11, 6);
            this.implicit_numeric_conversions.Add(11, 5);
            this.implicit_numeric_conversions.Add(15, 8);
            this.implicit_numeric_conversions.Add(15, 13);
            this.implicit_numeric_conversions.Add(15, 9);
            this.implicit_numeric_conversions.Add(15, 14);
            this.implicit_numeric_conversions.Add(15, 7);
            this.implicit_numeric_conversions.Add(15, 6);
            this.implicit_numeric_conversions.Add(15, 5);
            this.implicit_numeric_conversions.Add(8, 9);
            this.implicit_numeric_conversions.Add(8, 7);
            this.implicit_numeric_conversions.Add(8, 6);
            this.implicit_numeric_conversions.Add(8, 5);
            this.implicit_numeric_conversions.Add(13, 9);
            this.implicit_numeric_conversions.Add(13, 14);
            this.implicit_numeric_conversions.Add(13, 7);
            this.implicit_numeric_conversions.Add(13, 6);
            this.implicit_numeric_conversions.Add(13, 5);
            this.implicit_numeric_conversions.Add(9, 7);
            this.implicit_numeric_conversions.Add(9, 6);
            this.implicit_numeric_conversions.Add(9, 5);
            this.implicit_numeric_conversions.Add(14, 7);
            this.implicit_numeric_conversions.Add(14, 6);
            this.implicit_numeric_conversions.Add(14, 5);
            this.implicit_numeric_conversions.Add(4, 15);
            this.implicit_numeric_conversions.Add(4, 8);
            this.implicit_numeric_conversions.Add(4, 13);
            this.implicit_numeric_conversions.Add(4, 9);
            this.implicit_numeric_conversions.Add(4, 14);
            this.implicit_numeric_conversions.Add(4, 7);
            this.implicit_numeric_conversions.Add(4, 6);
            this.implicit_numeric_conversions.Add(4, 5);
            this.implicit_numeric_conversions.Add(7, 6);
        }

        public static object ChangeType(object v, Type t)
        {
            return Convert.ChangeType(v, t);
        }

        public int CompareConversions(BaseScripter scripter, int id, int id1, int id2)
        {
            int typeId = scripter.symbol_table[id].TypeId;
            int num2 = scripter.symbol_table[id1].TypeId;
            int num3 = scripter.symbol_table[id2].TypeId;
            if (num2 != num3)
            {
                if (typeId == num2)
                {
                    return 1;
                }
                if (typeId == num3)
                {
                    return -1;
                }
                if ((num2 == 10) && (((num3 == 3) || (num3 == 15)) || ((num3 == 13) || (num3 == 14))))
                {
                    return 1;
                }
                if ((num3 == 10) && (((num2 == 3) || (num2 == 15)) || ((num2 == 13) || (num2 == 14))))
                {
                    return -1;
                }
                if ((num2 == 11) && (((num3 == 15) || (num3 == 13)) || (num3 == 14)))
                {
                    return 1;
                }
                if ((num3 == 11) && (((num2 == 15) || (num2 == 13)) || (num2 == 14)))
                {
                    return -1;
                }
                if ((num2 == 8) && ((num3 == 13) || (num3 == 14)))
                {
                    return 1;
                }
                if ((num3 == 8) && ((num2 == 13) || (num2 == 14)))
                {
                    return -1;
                }
                if ((num2 == 9) && (num3 == 14))
                {
                    return 1;
                }
                if ((num3 == 9) && (num2 == 14))
                {
                    return -1;
                }
                if (this.ExistsImplicitConversion(scripter, id1, id2))
                {
                    if (this.ExistsImplicitConversion(scripter, id2, id1))
                    {
                        return 0;
                    }
                    return 1;
                }
                if (this.ExistsImplicitConversion(scripter, id2, id1))
                {
                    if (this.ExistsImplicitConversion(scripter, id1, id2))
                    {
                        return 0;
                    }
                    return -1;
                }
            }
            return 0;
        }

        public bool ExistsImplicitBoxingConversion(ClassObject c1, ClassObject c2)
        {
            BaseScripter scripter = c1.Scripter;
            if (c1.IsValueType)
            {
                if (c2.Id == scripter.symbol_table.OBJECT_CLASS_id)
                {
                    return true;
                }
                if (c2.Id == scripter.symbol_table.VALUETYPE_CLASS_id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ExistsImplicitConversion(BaseScripter scripter, int id1, int id2)
        {
            int typeId = scripter.symbol_table[id1].TypeId;
            int num2 = scripter.symbol_table[id2].TypeId;
            if (typeId == num2)
            {
                return true;
            }
            if (this.ExistsImplicitNumericConstConversion(scripter, id1, id2))
            {
                return true;
            }
            if (this.ExistsImplicitReferenceConversion(scripter, id1, id2))
            {
                return true;
            }
            if (this.ExistsImplicitEnumerationConversion(scripter, id1, id2))
            {
                return true;
            }
            if (this.ExistsImplicitNumericConversion(typeId, num2))
            {
                return true;
            }
            ClassObject classObject = scripter.GetClassObject(typeId);
            ClassObject obj3 = scripter.GetClassObject(num2);
            return this.ExistsImplicitBoxingConversion(classObject, obj3);
        }

        public bool ExistsImplicitEnumerationConversion(BaseScripter scripter, int id1, int id2)
        {
            int typeId = scripter.symbol_table[id1].TypeId;
            ClassObject classObject = scripter.GetClassObject(typeId);
            string name = scripter.symbol_table[id2].Name;
            return (classObject.IsEnum && (name == "0"));
        }

        public bool ExistsImplicitNumericConstConversion(BaseScripter scripter, int id1, int id2)
        {
            if (scripter.symbol_table[id1].Kind == MemberKind.Const)
            {
                int typeId = scripter.symbol_table[id1].TypeId;
                int num2 = scripter.symbol_table[id2].TypeId;
                if (typeId == 8)
                {
                    switch (num2)
                    {
                        case 8:
                            return true;

                        case 13:
                            return true;

                        case 9:
                            return true;

                        case 14:
                            return true;

                        case 3:
                        {
                            int valueAsInt = scripter.symbol_table[id1].ValueAsInt;
                            if ((valueAsInt >= 0) && (valueAsInt <= 0xff))
                            {
                                return true;
                            }
                            break;
                        }
                        case 10:
                        {
                            int num4 = scripter.symbol_table[id1].ValueAsInt;
                            if ((num4 >= -128) && (num4 <= 0x7f))
                            {
                                return true;
                            }
                            break;
                        }
                        case 11:
                        {
                            int num5 = scripter.symbol_table[id1].ValueAsInt;
                            if ((num5 >= -32768) && (num5 <= 0x7fff))
                            {
                                return true;
                            }
                            break;
                        }
                        case 15:
                        {
                            int num6 = scripter.symbol_table[id1].ValueAsInt;
                            if ((num6 >= 0) && (num6 <= 0xffff))
                            {
                                return true;
                            }
                            break;
                        }
                    }
                }
            }
            return false;
        }

        public bool ExistsImplicitNumericConversion(int type_id1, int type_id2)
        {
            for (int i = 0; i < this.implicit_numeric_conversions.Count; i++)
            {
                int num2 = this.implicit_numeric_conversions.Items1[i];
                int num3 = this.implicit_numeric_conversions.Items2[i];
                if ((num2 == type_id1) && (num3 == type_id2))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ExistsImplicitReferenceConversion(ClassObject c1, ClassObject c2)
        {
            BaseScripter scripter = c1.Scripter;
            if (c1.IsReferenceType && (c2.Id == scripter.symbol_table.OBJECT_CLASS_id))
            {
                return true;
            }
            if (c1.InheritsFrom(c2))
            {
                return true;
            }
            if (c1.IsArray && (c2.Id == scripter.symbol_table.ARRAY_CLASS_id))
            {
                return true;
            }
            if (c1.IsDelegate && (c2.Id == scripter.symbol_table.DELEGATE_CLASS_id))
            {
                return true;
            }
            if ((c1.IsArray || c1.IsDelegate) && (c2.Id == scripter.symbol_table.ICLONEABLE_CLASS_id))
            {
                return true;
            }
            if (c1.IsArray && c2.IsArray)
            {
                int rank = CSLite_System.GetRank(c1.Name);
                int num2 = CSLite_System.GetRank(c2.Name);
                if (rank == num2)
                {
                    string elementTypeName = CSLite_System.GetElementTypeName(c1.Name);
                    string str2 = CSLite_System.GetElementTypeName(c2.Name);
                    int typeId = scripter.GetTypeId(elementTypeName);
                    int id = scripter.GetTypeId(str2);
                    ClassObject classObject = scripter.GetClassObject(typeId);
                    ClassObject obj3 = scripter.GetClassObject(id);
                    if (classObject.IsReferenceType && obj3.IsReferenceType)
                    {
                        return this.ExistsImplicitReferenceConversion(classObject, obj3);
                    }
                }
                return false;
            }
            return false;
        }

        public bool ExistsImplicitReferenceConversion(BaseScripter scripter, int id1, int id2)
        {
            int typeId = scripter.symbol_table[id1].TypeId;
            int id = scripter.symbol_table[id2].TypeId;
            ClassObject classObject = scripter.GetClassObject(typeId);
            ClassObject obj3 = scripter.GetClassObject(id);
            return (this.ExistsImplicitReferenceConversion(classObject, obj3) || ((id1 == scripter.symbol_table.NULL_id) && obj3.IsReferenceType));
        }

        public static bool ToBoolean(object v)
        {
            try
            {
                if (v.GetType() == typeof(bool))
                {
                    return (bool) v;
                }
                return (bool) ChangeType(ToPrimitive(v), typeof(bool));
            }
            catch
            {
                return false;
            }
        }

        public static byte ToByte(object v)
        {
            try
            {
                int num = (int) ChangeType(ToPrimitive(v), typeof(int));
                return (byte) num;
            }
            catch
            {
                return 0;
            }
        }

        public static char ToChar(object v)
        {
            try
            {
                if (v.GetType() == typeof(char))
                {
                    return (char) v;
                }
                return (char) ChangeType(ToPrimitive(v), typeof(char));
            }
            catch
            {
                return ' ';
            }
        }

        public static decimal ToDecimal(object v)
        {
            try
            {
                if (v.GetType() == typeof(decimal))
                {
                    return (decimal) v;
                }
                return (decimal) ChangeType(ToPrimitive(v), typeof(decimal));
            }
            catch
            {
                return 0M;
            }
        }

        public static double ToDouble(object v)
        {
            try
            {
                if (v.GetType() == typeof(double))
                {
                    return (double) v;
                }
                return (double) ChangeType(ToPrimitive(v), typeof(double));
            }
            catch
            {
                return 0.0;
            }
        }

        public static object ToEnum(Type t, object v)
        {
            Array values = Enum.GetValues(t);
            for (int i = 0; i < values.Length; i++)
            {
                if (((int) values.GetValue(i)) == ((int) v))
                {
                    return values.GetValue(i);
                }
            }
            return v;
        }

        public static float ToFloat(object v)
        {
            try
            {
                if (v.GetType() == typeof(float))
                {
                    return (float) v;
                }
                return (float) ChangeType(ToPrimitive(v), typeof(float));
            }
            catch
            {
                return 0f;
            }
        }

        public static int ToInt(object v)
        {
            try
            {
                if (v.GetType() == typeof(int))
                {
                    return (int) v;
                }
                return (int) ChangeType(ToPrimitive(v), typeof(int));
            }
            catch
            {
                return 0;
            }
        }

        public static long ToLong(object v)
        {
            try
            {
                if (v.GetType() == typeof(long))
                {
                    return (long) v;
                }
                return (long) ChangeType(ToPrimitive(v), typeof(long));
            }
            catch
            {
                return 0L;
            }
        }

        public static object ToPrimitive(object v)
        {
            if (v is ObjectObject)
            {
                return (v as ObjectObject).Instance;
            }
            return v;
        }

        public static short ToShort(object v)
        {
            try
            {
                int num = (int) ChangeType(ToPrimitive(v), typeof(int));
                return (short) num;
            }
            catch
            {
                return 0;
            }
        }

        public static string ToString(object v)
        {
            if (v == null)
            {
                return null;
            }
            try
            {
                return (string) ChangeType(ToPrimitive(v), typeof(string));
            }
            catch
            {
                return "";
            }
        }
    }
}

