using System.Data;

namespace Dummyy.Services.Convertors
{
    public class Convertor
    {
        public  List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (var item in temp.GetProperties())
                {
                    if (item.Name == column.ColumnName)
                        try
                        {
                            item.SetValue(obj, dr[column.ColumnName], null);

                        }
                        catch (ArgumentException)
                        {

                            item.SetValue(obj, dr[column.ColumnName].ToString(), null);
                        }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
