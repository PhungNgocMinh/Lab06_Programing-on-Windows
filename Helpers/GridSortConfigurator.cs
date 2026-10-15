using System.Windows.Forms;

namespace ProductClass.Helpers
{
    public static class GridSortConfigurator
    {
        public static void Apply(DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
            {
                bool sortable = col.DataPropertyName == "DonGia"
                                || col.DataPropertyName == "SoLuong";

                col.SortMode = sortable
                    ? DataGridViewColumnSortMode.Automatic
                    : DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}