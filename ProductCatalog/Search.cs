using System;
using System.Windows.Forms;

namespace ProductCatalog
{
    public class ProductSearchSettings
    {
        public bool SearchProductName { get; set; }
        public bool SearchProductCode { get; set; }
        public bool SearchVersion { get; set; }
        public bool SearchModDate { get; set; }
        public bool SearchAll { get; set; }
        public string SearchString { get; set; }
        public int SearchStartingRow { get; set; }

        public ProductSearchSettings()
        {
            SearchProductName = true;
            SearchProductCode = true;
            SearchVersion = true;
            SearchModDate = false;
            SearchString = "";
            SearchStartingRow = 0;
        }

        public bool IsValid {
            get
            {
                return SearchString.Length > 0 && (SearchProductName || SearchProductCode || SearchVersion || SearchModDate);
            }
        }
    }

    public class Search
    {
        static public bool FindInProductList(ListView list, ProductSearchSettings settings)
        {
            if (settings.SearchAll)
                return FindAllInProductList(list, settings);
            else
                return FindFirstInProductList(list, settings);
        }

        static private bool FindFirstInProductList(ListView list, ProductSearchSettings settings)
        {
            foreach (ListViewItem lvi in list.Items) lvi.Selected = false;

            if (settings.SearchStartingRow >= list.Items.Count)
                return false;

            for (int i = settings.SearchStartingRow; i < list.Items.Count; i++)
            {
                ListViewItem lvi = list.Items[i];
                if (FindInItem(lvi, settings))
                {
                    lvi.Selected = true;
                    settings.SearchStartingRow = i + 1;
                    list.EnsureVisible(i);
                    return true;
                }
            }
            return false;
        }

        static private bool FindAllInProductList(ListView list, ProductSearchSettings settings)
        {
            bool found = false;
            foreach (ListViewItem lvi in list.Items)
            {
                lvi.Selected = FindInItem(lvi, settings);
                if (lvi.Selected) found = true;
            }
            return found;
        }

        static private bool FindInItem(ListViewItem lvi, ProductSearchSettings settings)
        {
            if (settings.SearchProductName)
                if (lvi.SubItems[CatalogForm.ColProdName].Text.IndexOf(settings.SearchString, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return true;
            if (settings.SearchProductCode)
                if (lvi.SubItems[CatalogForm.ColProdCode].Text.IndexOf(settings.SearchString, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return true;
            if (settings.SearchVersion)
                if (lvi.SubItems[CatalogForm.ColVersion].Text.IndexOf(settings.SearchString, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return true;
            if (settings.SearchModDate)
                if (lvi.SubItems[CatalogForm.ColModDate].Text.IndexOf(settings.SearchString, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return true;
            return false;
        }
    }
}
