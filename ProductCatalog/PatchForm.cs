using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProductCatalog
{
    public partial class PatchForm : Form
    {
        private int _lastColumnClicked = 0;
        private bool _ascending = true;
        private string _productCode = null;

        public static int ColPatchName = 0;
        public static int ColPatchCode = 1;

        public PatchForm()
        {
            InitializeComponent();
        }
        public PatchForm(string productCode)
        {
            _productCode = productCode;
            InitializeComponent();
        }

        private void PatchForm_Load(object sender, EventArgs e)
        {
            if (_productCode != null)
            {
                PopulateList.PopulatePatchList(patchList, _productCode);
                switch (patchList.Items.Count)
                {
                    case 0: tsCount.Text = "No items."; break;
                    case 1: tsCount.Text = "1 item."; break;
                    default: tsCount.Text = String.Format("{0} items.", patchList.Items.Count); break;
                }
            }
        }

        private void patchList_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int wid = control.Size.Width;
            for (int i = 1; i < patchList.Columns.Count; i++)
            {
                ColumnHeader ch = patchList.Columns[i];
                wid -= ch.Width;
            }
            patchList.Columns[0].Width = wid - (16 + 2);    // 16 for scrollbar width
        }

        private void patchList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool diffCol = _lastColumnClicked != e.Column;
            _ascending = (diffCol) ? true : !_ascending;
            this.patchList.ListViewItemSorter = new PatchListViewItemComparer(e.Column, patchList.Columns.Count - 1, _ascending);
            _lastColumnClicked = e.Column;
        }

        private void patchList_KeyDown(object sender, KeyEventArgs e)
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

        private void SelectAllListItems(bool select)
        {
            foreach (ListViewItem lvi in patchList.Items)
            {
                lvi.Selected = select;
            }
        }

        private void CopySelectedListItemsToClipboard()
        {
            StringBuilder sbClip = new StringBuilder();
            foreach (ListViewItem lvi in patchList.Items)
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
            sb.Append(lvi.SubItems[ColPatchName].Text);
            sb.Append("\t");
            sb.Append(lvi.SubItems[ColPatchCode].Text);
            sb.Append("\r\n");
        }
    }
}
