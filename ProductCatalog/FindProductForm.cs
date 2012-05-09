using System;
using System.Windows.Forms;

namespace ProductCatalog
{
    public partial class FindProductForm : Form
    {
        public ProductSearchSettings Settings { get; set; }

        public FindProductForm(ProductSearchSettings settings)
        {
            InitializeComponent();
            Settings = settings;
        }

        public bool IsValid { get { return (checkedListBox1.CheckedItems.Count > 0) && (tbSearchFor.Text.Length > 0); } }

        #region Form Events

        private void FindProductForm_Load(object sender, EventArgs e)
        {
            UIFromSettings();
        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            EnableUI();
        }

        private void tbSearchFor_TextChanged(object sender, EventArgs e)
        {
            EnableUI();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SettingsFromUI();
        }

        #endregion

        private void EnableUI()
        {
            // Enable the OK button if there is a seach string and at least one column checkbox checked.
            btnOK.Enabled = IsValid;
        }

        private void UIFromSettings()
        {
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.SetItemChecked(0, Settings.SearchProductName);
            checkedListBox1.SetItemChecked(1, Settings.SearchProductCode);
            checkedListBox1.SetItemChecked(2, Settings.SearchVersion);
            checkedListBox1.SetItemChecked(3, Settings.SearchModDate);
            cbSelectAll.Checked = Settings.SearchAll;
            tbSearchFor.Text = Settings.SearchString;
        }

        private void SettingsFromUI()
        {
            Settings.SearchAll = cbSelectAll.Checked;
            Settings.SearchProductName = checkedListBox1.GetItemChecked(0);
            Settings.SearchProductCode = checkedListBox1.GetItemChecked(1);
            Settings.SearchVersion = checkedListBox1.GetItemChecked(2);
            Settings.SearchModDate = checkedListBox1.GetItemChecked(3);
            Settings.SearchString = tbSearchFor.Text;
        }
    }
}
