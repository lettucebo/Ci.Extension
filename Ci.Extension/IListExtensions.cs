using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Ci.Extension
{
    public static class IListExtensions
    {
        public static DataTable ToDataTable<T>(this IList<T> datas) where T : class
        {
            var properties = typeof(T).GetProperties();
            var table = new DataTable();
            var instance = datas.FirstOrDefault();
            if (instance != null)
            {
                foreach (var property in properties)
                {
                    var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    var name = property.Name;
                    var annotation = instance.GetAttributeFrom<DisplayNameAttribute>(property.Name);
                    var displayName = annotation?.DisplayName;
                    table.Columns.Add(displayName ?? name, type);
                }
            }
            foreach (var entity in datas)
            {
                table.Rows.Add(properties.Select(p => p.GetValue(entity, null)).ToArray());
            }
            return table;
        }
    }
}
