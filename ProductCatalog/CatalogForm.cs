using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ProductCatalog
{
    public partial class CatalogForm : Form
    {
        #region Declarations

        private int _lastColumnClicked = 0;
        private bool _ascending = true;
        private ProductSearchSettings _findSettings = new ProductSearchSettings();
        private bool _userChangedSelection = false;

        public const int ColProdName = 0;
        public const int ColProdCode = 1;
        public const int ColVersion = 2;
        public const int ColContext = 3;
        public const int ColState = 4;
        public const int ColModDate = 5;

        #endregion

        public CatalogForm()
        {
            InitializeComponent();
        }

        #region Form Events

        private void CatalogForm_Load(object sender, EventArgs e)
        {
            PopulateList.PopulateProductList(productList);
            productList.Sorting = SortOrder.Ascending;
            Version ver = new Version(Application.ProductVersion);
            tsVersion.Text = "v" + ver.ToString(2);
        }

        #endregion

        #region List Events

        private void productList_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            int wid = control.Size.Width;
            for (int i = 1; i < productList.Columns.Count; i++)
            {
                ColumnHeader ch = productList.Columns[i];
                wid -= ch.Width;
            }
            productList.Columns[ColProdName].Width = wid - (16 + 3);    // 16 for scrollbar width
        }

        private void productList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool diffCol = _lastColumnClicked != e.Column;
            _ascending = (diffCol) ? true : !_ascending;
            this.productList.ListViewItemSorter = new CatalogListViewItemComparer(e.Column, productList.Columns.Count - 1, _ascending);
            _lastColumnClicked = e.Column;
        }

        // Track user changes in selection
        private void productList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _userChangedSelection = true;
            int count = productList.SelectedItems.Count;
            switch (count)
            {
                case 0: tsSelCount.Text = "No items selected."; break;
                case 1: tsSelCount.Text = "1 item selected."; break;
                default: tsSelCount.Text = String.Format("{0} items selected.", count); break;
            }
            copyToolStripMenuItem.Enabled = (count > 0);
            patchesToolStripMenuItem.Enabled = (count == 1);
            featuresToolStripMenuItem.Enabled = (count == 1);
            //componentsToolStripMenuItem.Enabled = (count == 1);
        }

        // Crude command router
        private void productList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                Find(true);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SelectAllListItems(false);
                e.Handled = true;
            }
        }

        #endregion

        #region Menu Commands

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: It would be nice to preserve selection and improve redraw.
            productList.SelectedItems.Clear();  // /To reset selection count
            PopulateList.PopulateProductList(productList);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAllListItems(true);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedListItemsToClipboard();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find(true);
        }

        private void findAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find(false);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        #endregion    

        #region Context Menu Commands

        private void patchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(productList.SelectedItems.Count == 1)
            {
                ListViewItem lvi = productList.SelectedItems[0];
                string productCode = lvi.SubItems[CatalogForm.ColProdCode].Text;
                PatchForm dlg = new PatchForm(productCode);
                dlg.ShowDialog(this);
            }
        }

        private void featuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(productList.SelectedItems.Count == 1)
            {
                ListViewItem lvi = productList.SelectedItems[0];
                string productCode = lvi.SubItems[CatalogForm.ColProdCode].Text;
                FeatureForm dlg = new FeatureForm(productCode);
                dlg.ShowDialog(this);
            }
        }

        private void componentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void SelectAllListItems(bool select)
        {
            foreach (ListViewItem lvi in productList.Items)
            {
                lvi.Selected = select;
            }
        }

        private void CopySelectedListItemsToClipboard()
        {
            StringBuilder sbClip = new StringBuilder();
            foreach (ListViewItem lvi in productList.Items)
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
            sb.Append(lvi.SubItems[ColProdName].Text);
            sb.Append("\t");
            sb.Append(lvi.SubItems[ColProdCode].Text);
            sb.Append("\t");
            sb.Append(lvi.SubItems[ColVersion].Text);
            sb.Append("\t");
            sb.Append(lvi.SubItems[ColContext].Text);
            sb.Append("\t");
            sb.Append(lvi.SubItems[ColState].Text);
            sb.Append("\t");
            sb.Append(lvi.SubItems[ColModDate].Text);
            sb.Append("\r\n");
        }

        private void Find(bool withUI)
        {
            // If the user has changed the selection since the last search,
            // Set the starting row at the row after the current first selection,
            // or at 0 if no selection of the last line is selected
            if (_userChangedSelection)
            {
                _findSettings.SearchStartingRow = 0;
                int row = 0;
                while (row < productList.Items.Count)
                {
                    if (productList.Items[row].Selected)
                    {
                        _findSettings.SearchStartingRow = row + 1;
                        break;
                    }
                    row++;
                }
                _userChangedSelection = false;
            }

            if (withUI)
            {
                FindProductForm dlg = new FindProductForm(_findSettings);
                dlg.Settings = _findSettings;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _findSettings = dlg.Settings;
                    // If nothing was found, reset search next time.
                    // If something was found, ignore selection changes made by finding.
                    bool result = Search.FindInProductList(productList, _findSettings);
                    _userChangedSelection = !result;
                    if (!result) productList.EnsureVisible(0);
                    findAgainToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                if (_findSettings.IsValid)
                {
                    bool result = Search.FindInProductList(productList, _findSettings);
                    _userChangedSelection = !result;
                    if (!result) productList.EnsureVisible(0);
                }
            }
        }

        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            Stream strm;

            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if ((strm = sfd.OpenFile()) != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ListViewItem lvi in productList.Items)
                        AppendListViewItemToStringBuilder(lvi, ref sb);
                    StreamWriter sw = new StreamWriter(strm);
                    sw.Write(sb.ToString());
                    sw.Close();
                    strm.Close();
                }
            }
        }
    }
}
