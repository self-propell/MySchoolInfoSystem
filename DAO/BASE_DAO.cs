﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace SchPeoManageWeb.DAO
{
    public abstract class BASE_DAO<T> where T:new()
    {
        public static T ConvertToModel(DataTable dt)
        {
            //实例化泛型对象
            T t = new T();
            if (dt != null)
            {
                // 获取泛型的具体类型   
                Type type = typeof(T);
                string tempName = "";
                //只反射第一行的结果
                DataRow dr = dt.Rows[0];
                // 获得泛型的所有公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历所有公共属性的名字，查找与DataTable列名相同的属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//获取属性名
                    if (dt.Columns.Contains(tempName))// 检查DataTable是否包含该属性名    
                    {
                        //如果该属性是否可写(有setter方法)，则把列的值赋值给该属性
                        if (pi.CanWrite)
                        {
                            object value = dr[tempName];
                            if (value != DBNull.Value)
                            {
                                // 如果属性是字符串类型，则去除前后空格
                                if (pi.PropertyType == typeof(string))
                                {
                                    value = ((string)value).Trim();
                                }
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                }
            }
            return t;
        }
        public static List<T> ConvertToList(DataTable dt)
        {
            List<T> ts = new List<T>();
            if (dt != null)
            {
                // 获取泛型的具体类型   
                Type type = typeof(T);
                string tempName = "";
                foreach (DataRow dr in dt.Rows)
                {
                    //实例化泛型对象
                    T t = new T();
                    // 获得泛型的所有公共属性      
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    //遍历所有公共属性的名字，查找与DataTable列名相同的属性
                    foreach (PropertyInfo pi in propertys)
                    {
                        // 检查属性是否有 Column 特性
                        var columnAttribute = pi.GetCustomAttribute<ColumnAttribute>();
                        if (columnAttribute!=null)// 检查DataTable是否包含该属性名    
                        {
                            // 使用 Column 特性中定义的列名
                            tempName = columnAttribute.Name;
                            //如果该属性是否可写(有setter方法)，则把列的值赋值给该属性
                            if (dt.Columns.Contains(tempName))
                            {
                                // 如果该属性是否可写（有 setter 方法），
                                // 则把列的值赋值给该属性
                                if (pi.CanWrite)
                                {
                                    object value = dr[tempName];
                                    if (value != DBNull.Value)
                                    {
                                        // 尝试将值转换为属性的类型
                                        pi.SetValue(t, value, null);
                                    }
                                    else
                                    {
                                        // 处理 DBNull.Value 的情况，例如设置为 null 或默认值
                                        pi.SetValue(t, null, null);
                                    }
                                }
                            }

                        }
                    }
                    //遍历一次后，将泛型的Bean对象添加至集合
                    ts.Add(t);
                }
                return ts;
            }
            return ts;
        }
    }
}