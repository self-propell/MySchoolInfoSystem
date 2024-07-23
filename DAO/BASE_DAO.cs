using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace SchPeoManageWeb.DAO
{
    /// <summary>
    /// 基础数据库转换操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BASE_DAO<T> where T:new()
    {
        /// <summary>
        /// 将数据转换为单个对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 将数据转换为List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 自动创建插入语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GenerateInsertQuery(T entity, out List<SqlParameter> parameters,string dbName)
        {
            var type = entity.GetType();
            var properties = type.GetProperties();
            var columns = new List<string>();
            var values = new List<string>();
            parameters = new List<SqlParameter>();

            foreach (var property in properties)
            {
                // 获取属性上的 ColumnAttribute
                var columnAttr = property.GetCustomAttribute<ColumnAttribute>();
                // 获取属性上的 DatabaseGeneratedAttribute
                var dbGeneratedAttr = property.GetCustomAttribute<DatabaseGeneratedAttribute>();
                // 获取属性上的 IgnoreForInsertAttribute
                var ignoreAttr = property.GetCustomAttribute<IgnoreForInsertAttribute>();

                // 排除标记为 IgnoreForInsertAttribute 的字段以及自增主键字段
                if (columnAttr != null && ignoreAttr == null &&
                    (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity))
                {
                    columns.Add(columnAttr.Name);
                    values.Add($"@{property.Name}");
                    parameters.Add(new SqlParameter($"@{property.Name}", property.GetValue(entity) ?? DBNull.Value));
                }
            }

            string columnsPart = string.Join(", ", columns);
            string valuesPart = string.Join(", ", values);
            string sqlstr = $"INSERT INTO {dbName} ({columnsPart}) VALUES ({valuesPart})";

            return sqlstr;
        }

        /// <summary>
        /// 自动创建更新语句
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parameters"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GenerateUpdateQuery(T entity, out List<SqlParameter> parameters, string dbName)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var columns = new List<string>();
            parameters = new List<SqlParameter>();

            // 获取主键属性
            var keyProperty = properties.FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);
            if (keyProperty == null)
                throw new InvalidOperationException("Entity does not have a primary key.");

            // 主键值
            var keyValue = keyProperty.GetValue(entity);

            // 构建 SQL 更新语句
            foreach (var property in properties)
            {
                var columnAttr = property.GetCustomAttribute<ColumnAttribute>();
                var dbGeneratedAttr = property.GetCustomAttribute<DatabaseGeneratedAttribute>();
                var ignoreAttr = property.GetCustomAttribute<IgnoreForInsertAttribute>();

                // 排除主键和标记为忽略的字段
                if (columnAttr != null && property != keyProperty && ignoreAttr == null &&
                    (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity))
                {
                    columns.Add($"{columnAttr.Name} = @{property.Name}");
                    parameters.Add(new SqlParameter($"@{property.Name}", property.GetValue(entity) ?? DBNull.Value));
                }
            }

            // 添加主键参数
            parameters.Add(new SqlParameter("@Key", keyValue));

            // 构建 SQL 更新语句
            string setClause = string.Join(", ", columns);
            string keyColumn = keyProperty.GetCustomAttribute<ColumnAttribute>()?.Name ?? keyProperty.Name;
            string sqlstr = $"UPDATE {dbName} SET {setClause} WHERE {keyColumn} = @Key";

            return sqlstr;
        }
    }
}
