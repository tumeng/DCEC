using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace Rmes.Pub.Data1
{
    public static class PublicClass1
    {
        //public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        //{
        //    //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    //DataTable dt = new DataTable();
        //    //for (int i = 0; i < properties.Count; i++)
        //    //{
        //    //    PropertyDescriptor property = properties[i];
        //    //    dt.Columns.Add(property.Name, property.PropertyType);
        //    //}
        //    //object[] values = new object[properties.Count];
        //    //foreach (T item in data)
        //    //{
        //    //    for (int i = 0; i < values.Length; i++)
        //    //    {
        //    //        values[i] = properties[i].GetValue(item);
        //    //    }
        //    //    dt.Rows.Add(values);
        //    //}
        //    //创建属性的集合    
        //    List<PropertyInfo> pList = new List<PropertyInfo>();
        //    //获得反射的入口    

        //    Type type = typeof(T);
        //    DataTable dt = new DataTable();
        //    //把所有的public属性加入到集合 并添加DataTable的列    
        //    Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
        //    foreach (var item in list)
        //    {
        //        //创建一个DataRow实例    
        //        DataRow row = dt.NewRow();
        //        //给row 赋值    
        //        pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
        //        //加入到DataTable    
        //        dt.Rows.Add(row);
        //    } 
        //    //DataTable dt = new DataTable();
        //    //if (_list != null)
        //    //{
        //    //    //通过反射获取list中的字段 
        //    //    System.Reflection.PropertyInfo[] p = _list[0].GetType().GetProperties();
        //    //    foreach (System.Reflection.PropertyInfo pi in p)
        //    //    {
        //    //        dt.Columns.Add(pi.Name, System.Type.GetType(pi.PropertyType.ToString()));
        //    //    }
        //    //    for (int i = 0; i < _list.Count; i++)
        //    //    {
        //    //        IList TempList = new ArrayList();
        //    //        //将IList中的一条记录写入ArrayList
        //    //        foreach (System.Reflection.PropertyInfo pi in p)
        //    //        {
        //    //            object oo = pi.GetValue(_list[i], null);
        //    //            TempList.Add(oo);
        //    //        }
        //    //        object[] itm = new object[p.Length];
        //    //        for (int j = 0; j < TempList.Count; j++)
        //    //        {
        //    //            itm.SetValue(TempList[j], j);
        //    //        }
        //    //        dt.LoadDataRow(itm, true);
        //    //    }
        //    //}
        //    return dt;
        //}

        public static DataTable ListToDataTable<T>(this IList<T> data, string tableName)
        {
            DataTable table = new DataTable(tableName);

            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {

                DataColumn dc = new DataColumn("Value");
                table.Columns.Add(dc);
                foreach (T item in data)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = item;
                    table.Rows.Add(dr);
                }
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name,
                    Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch (Exception ex)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}
