using System;
using System.Text;
using System.Windows.Forms;

namespace ProductCatalog
{
    public partial class FeatureForm : Form
    {
        private int _lastColumnClicked = 0;
        private bool _ascending = true;
        private string _productCode = null;

        public static int ColFeatureName = 0;
        public static int ColFeatureState = 1;

        public FeatureForm()
        {
            InitializeComponent();
        }
        public FeatureForm(string productCode)
        {
            _productCode = productCode;
            InitializeComponent();
        }

        private void FeatureForm_Load(object sender, EventArgs e)
        {
            if (_productCode != null)
            {
                PopulateList.PopulateFeatureList(featureList, _productCode);
                switch (featureList.Items.Count)
                {
                    case 0: tsCount.Text = "No items."; break;
                    case 1: tsCount.Text = "1 item."; break;
                    default: tsCount.Text = String.Format("{0} items.", featureList.Items.Count); break;
                }
            }
        }

        #region List Events

        private void featureList_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int wid = control.Size.Width;
            for (int i = 1; i < featureList.Columns.Count; i++)
            {
                ColumnHeader ch = featureList.Columns[i];
                wid -= ch.Width;
            }
            featureList.Columns[0].Width = wid - (16 + 2);    // 16 for scrollbar width
        }

        private void featureList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool diffCol = _lastColumnClicked != e.Column;
            _ascending = (diffCol) ? true : !_ascending;
            this.featureList.ListViewItemSorter = new FeatureListViewItemComparer(e.Column, featureList.Columns.Count - 1, _ascending);
            _lastColumnClicked = e.Column;
        }

        private void featureList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAllListItems(true);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedListItemsToClipboard();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SelectAllListItems(false);
                e.Handled = true;
            }
        }

        #endregion

        private void SelectAllListItems(bool select)
        {
            foreach (ListViewItem lvi in featureList.Items)
            {
                lvi.Selected = select;
            }
        }

        private void CopySelectedListItemsToClipboard()
        {
            StringBuilder sbClip = new StringBuilder();
            foreach (ListViewItem lvi in featureList.Items)
            {
                if (lvi.Selected)
                {
                    AppendListViewItemToStringBuilder(lvi, ref sbClip);
                }
            }
            if (sbClip.Length > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(sbClip.ToString());
            }
        }
        private void AppendListViewItemToStringBuilder(ListViewItem lvi, ref StringBuilder sb)
        {
            sb.Append(lvi.SubItems[ColFeatureName].Text);
            sb.Append("\t");
            sb.Append(lvi.SubItems[ColFeatureState].Text);
            sb.Append("\r\n");
        }

    }
}
