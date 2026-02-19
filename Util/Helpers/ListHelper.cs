using System.Data.Common;
using System.Text;

namespace Util.Helpers
{
    public static class ListHelper
    {
        public static (int Index, string Value, string Key) GetColumnMappingByIndex(
            List<(int Index, string Value, string Key)> columnMappings, int index, int defaultIndex)
        {
            var colSelected = columnMappings.FirstOrDefault(w => w.Index == index);
            if (colSelected.Equals(default((int, string, string))))
                return columnMappings.FirstOrDefault(w => w.Index == defaultIndex);
            return colSelected;
        }

        public static string GetOrderBy(
            List<(int Index, string Value, string Key)> columnMappings, int index, int defaultIndex, bool isAsc)
        {
            try
            {
                var colSelected = GetColumnMappingByIndex(columnMappings, index, defaultIndex);
                return $@"order by {colSelected.Value} {(isAsc ? "asc" : "desc")}";
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetSelectFields(List<(int Index, string Value, string Key)> columnMappings)
        {
            var fields = new StringBuilder();
            foreach (var column in columnMappings)
            {
                fields.Append(column.Value)
                  .Append(" as ")
                  .Append(column.Key)
                  .Append(", ");
            }

            if (fields.Length > 0)
            {
                fields.Length -= 2;
            }
            return fields.ToString();    
        }
    }
}
