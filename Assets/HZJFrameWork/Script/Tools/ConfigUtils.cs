//=====================================================
// - FileName:      ConfigUtils.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/10/14 16:46:42
// - Description:   配置表工具类
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace HZJFrameWork
{
    public class ConfigUtils 
    {
        public static Type GetStringType(string type)
        {
            return type switch
            {
                "byte" => typeof(byte),
                "short" => typeof(short),
                "int" => typeof(int),
                "long" => typeof(long),
                "float" => typeof(float),
                "double" => typeof(double),
                "bool" => typeof(bool),
                "date" => typeof(DateTime),
                "string" => typeof(string),
                "byte[]" => typeof(List<byte>),
                "short[]" => typeof(List<short>),
                "int[]" => typeof(List<int>),
                "long[]" => typeof(List<long>),
                "float[]" => typeof(List<float>),
                "double[]" => typeof(List<double>),
                "bool[]" => typeof(List<bool>),
                "string[]" => typeof(List<string>),
                "list<int[]>" => typeof(List<int[]>),
                "list<string[]>" => typeof(List<string[]>),
                "list<list<int[]>>" => typeof(List<List<int[]>>),
                _ => typeof(string),
            };
        }


        public static object GetObjectValue(string type, string value, int fieldCount = 1)
        {
            double floatV = 0.0;
            bool isSetDef = fieldCount > 1;
            switch (type)
            {
                case "byte":
                    {
                        byte byteVal = 0;
                        byte.TryParse(value, out byteVal);
                        return byteVal;
                    }
                case "short":
                    {
                        double.TryParse(value, out floatV);
                        short shortVal = Convert.ToInt16(floatV);
                        return shortVal;
                    }
                case "int":
                    {
                        double.TryParse(value, out floatV);
                        int intVal = Convert.ToInt32(floatV);
                        return intVal;
                    }
                case "long":
                    {
                        long longVal = 0L;
                        long.TryParse(value, out longVal);
                        return longVal;
                    }
                case "float":
                    {
                        float floatVal = 0f;
                        float.TryParse(value, out floatVal);
                        return floatVal;
                    }
                case "double":
                    {
                        double doubleVal = 0.0;
                        double.TryParse(value, out doubleVal);
                        return doubleVal;
                    }
                case "bool":
                    {
                        if (value == "1")
                        {
                            return true;
                        }
                        bool boolVal = false;
                        bool.TryParse(value, out boolVal);
                        return boolVal;
                    }
                case "date":
                    {
                        DateTime dateVal = DateTime.MinValue;
                        DateTime.TryParse(value, out dateVal);
                        return dateVal;
                    }
                case "string":
                    return value;
                case "byte[]":
                    return SplitToArr<byte>(value, isSetDef);
                case "short[]":
                    return SplitToArr<short>(value, isSetDef);
                case "int[]":
                    return SplitToArr<int>(value, isSetDef);
                case "long[]":
                    return SplitToArr<long>(value, isSetDef);
                case "float[]":
                    return SplitToArr<float>(value, isSetDef);
                case "double[]":
                    return SplitToArr<double>(value, isSetDef);
                case "bool[]":
                    return SplitToArr<bool>(value, isSetDef);
                case "string[]":
                    return SplitToArr<string>(value, isSetDef);
                case "list<int[]>":
                    return SplitToItems(SplitToArr<string>(value));
                case "list<list<int[]>>":
                    {
                        List<List<int[]>> list = new List<List<int[]>>();
                        list.Add(SplitToItems(SplitToArr<string>(value)));
                        return list;
                    }
                case "list<string[]>":
                    return SplitToItemsStr(SplitToArr<string>(value));
                default:
                    return null;
            }
        }

        private static List<string[]> SplitToItemsStr(List<string> itemsStr)
        {
            List<string[]> items = new List<string[]>();
            foreach (string itstr in itemsStr)
            {
                List<string> iteminfo = SplitToArr<string>(itstr, isSetDef: false, '_');
                items.Add(iteminfo.ToArray());
            }
            return items;
        }

        private static List<T> SplitToArr<T>(string str, bool isSetDef = false, char separator = ';')
        {
            if (str == string.Empty || str == null)
            {
                if (isSetDef)
                {
                    return new List<T> { default(T) };
                }
                return new List<T>();
            }
            string[] objarr = str.Split(new char[1] { separator }, StringSplitOptions.RemoveEmptyEntries);
            List<T> tArr = new List<T>();
            for (int i = 0; i < objarr.Length; i++)
            {
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        objarr[i] = ((objarr[i] == "1") ? "true" : objarr[i]);
                    }
                    if (typeof(T) == typeof(int))
                    {
                        double.TryParse(objarr[i], out var floatV);
                        tArr.Add((T)Convert.ChangeType(floatV, typeof(T)));
                    }
                    else
                    {
                        tArr.Add((T)Convert.ChangeType(objarr[i], typeof(T)));
                    }
                }
                catch (Exception e)
                {
                    tArr.Add(default(T));
                    Debug.LogError(e.Message);
                }
            }
            return tArr;
        }

        private static List<int[]> SplitToItems(List<string> itemsStr)
        {
            List<int[]> items = new List<int[]>();
            foreach (string itstr in itemsStr)
            {
                List<int> iteminfo = SplitToArr<int>(itstr, isSetDef: false, '_');
                items.Add(iteminfo.ToArray());
            }
            return items;
        }

        public static string GetCSStringType(string type, bool isNull = true)
        {
            return type switch
            {
                "date" => isNull ? "DateTime?" : "DateTime",
                "list<int[]>" => "List<int[]>",
                "list<list<int[]>>" => "List<List<int[]>>",
                "list<string[]>" => "List<string[]>",

                "List<int[]>" => "List<int[]>",
                "List<list<int[]>>" => "List<List<int[]>>",
                "List<string[]>" => "List<string[]>",
                _ => type,
            };
        }


    }
}

