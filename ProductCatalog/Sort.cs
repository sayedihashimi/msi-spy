using System;
using System.Collections;
using System.Windows.Forms;

namespace ProductCatalog
{
    class CatalogListViewItemComparer : IComparer
    {
        private int _col;
        private int _max;
        private bool _up;

        public CatalogListViewItemComparer()
        {
            _col = 0;
            _max = 1;
            _up = true;
        }

        public CatalogListViewItemComparer(int column, int maxColumns, bool ascending)
        {
            _col = column;
            _max = maxColumns;
            _up = ascending;
        }

        public int Compare(object x, object y)
        {
            object a = (_up) ? x : y;
            object b = (_up) ? y : x;

            switch (_col)
            {
                case CatalogForm.ColProdName:
                case CatalogForm.ColProdCode:
                case CatalogForm.ColContext:
                case CatalogForm.ColState:
                    return String.Compare(((ListViewItem)a).SubItems[_col].Text, ((ListViewItem)b).SubItems[_col].Text);
                case CatalogForm.ColVersion:
                    Version vA = new Version(((ListViewItem)a).SubItems[_col].Text);
                    Version vB = new Version(((ListViewItem)b).SubItems[_col].Text);
                    if (vA == vB) return 0;
                    return (vA > vB) ? 1 : -1;
                case CatalogForm.ColModDate:
                    DateTime dtA = DateTime.Parse(((ListViewItem)a).SubItems[_col].Text);
                    DateTime dtB = DateTime.Parse(((ListViewItem)b).SubItems[_col].Text);
                    if (dtA == dtB) return 0;
                    return (dtA > dtB) ? 1 : -1;
                default:
                    string msg = String.Format("ListViewItemComparer: column {0} specified, range = 0 - {1}", _col, _max);
                    throw (new IndexOutOfRangeException(msg));
            }
        }
    }

    class FeatureListViewItemComparer : IComparer
    {
        private int _col;
        private int _max;
        private bool _up;

        public FeatureListViewItemComparer()
        {
            _col = 0;
            _max = 1;
            _up = true;
        }

        public FeatureListViewItemComparer(int column, int maxColumns, bool ascending)
        {
            _col = column;
            _max = maxColumns;
            _up = ascending;
        }

        public int Compare(object x, object y)
        {
            object a = (_up) ? x : y;
            object b = (_up) ? y : x;

            return String.Compare(((ListViewItem)a).SubItems[_col].Text, ((ListViewItem)b).SubItems[_col].Text);
        }
    }

    class PatchListViewItemComparer : IComparer
    {
        private int _col;
        private int _max;
        private bool _up;

        public PatchListViewItemComparer()
        {
            _col = 0;
            _max = 1;
            _up = true;
        }

        public PatchListViewItemComparer(int column, int maxColumns, bool ascending)
        {
            _col = column;
            _max = maxColumns;
            _up = ascending;
        }

        public int Compare(object x, object y)
        {
            object a = (_up) ? x : y;
            object b = (_up) ? y : x;

            return String.Compare(((ListViewItem)a).SubItems[_col].Text, ((ListViewItem)b).SubItems[_col].Text);
        }
    }

}
