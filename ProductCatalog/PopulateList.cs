using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ProductCatalog
{
    class PopulateList
    {
        #region Interop Declarations

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern int MsiGetProductInfo(string product, string property, [Out] StringBuilder valueBuf, ref int len);
        
        [DllImport("msi.dll", SetLastError = true)]
        static extern int MsiEnumProducts(int iProductIndex, StringBuilder lpProductBuf);
        
        [DllImport("msi.dll", SetLastError = true)]
        internal static extern int MsiEnumProductsEx(
                string szProductCode,
                string szUserSid,
                int dwContext,
                int dwIndex,
                [Out] StringBuilder szInstalledProductCode,
                out int pdwInstalledContext,
                [Out] StringBuilder szSid,
                ref int pcchSid);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern INSTALLSTATE MsiQueryProductState(string product);

        [DllImport("msi.dll", SetLastError = true)]
        internal static extern int MsiEnumFeatures(
                string szProductCode,
                int dwIndex,
                [Out] StringBuilder lpFeatureBuf,
                [Out] StringBuilder lpParentBuf);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        static extern INSTALLSTATE MsiQueryFeatureState(string product, string feature);

        [DllImport("msi.dll", SetLastError = true)]
        internal static extern int MsiEnumPatches(
                string szProductCode,
                int dwIndex,
                [Out] StringBuilder lpPatchBuf,
                [Out] StringBuilder lpTransformsBuf,
                ref int pcchTransformsBuf);

        [DllImport("msi.dll", SetLastError = true)]
        internal static extern int MsiGetPatchInfoEx(
                string PatchCode,
                string ProductCode,
                string UserID,
                int dwContext,
                string Property,
                [Out] StringBuilder Value,
                ref int pcchValue);

        private const int MSIINSTALLCONTEXT_USERMANAGED = 1;    // Patch under managed context.
        private const int MSIINSTALLCONTEXT_USER = 2;           // Patch under unmanaged context.
        private const int MSIINSTALLCONTEXT_MACHINE = 4;        // Patch under machine context.

        private enum INSTALLSTATE
        {
            INSTALLSTATE_NOTUSED = -7,  // component disabled
            INSTALLSTATE_BADCONFIG = -6,  // configuration data corrupt
            INSTALLSTATE_INCOMPLETE = -5,  // installation suspended or in progress
            INSTALLSTATE_SOURCEABSENT = -4,  // run from source, source is unavailable
            INSTALLSTATE_MOREDATA = -3,  // return buffer overflow
            INSTALLSTATE_INVALIDARG = -2,  // invalid function argument
            INSTALLSTATE_UNKNOWN = -1,  // unrecognized product or feature
            INSTALLSTATE_BROKEN = 0,  // broken
            INSTALLSTATE_ADVERTISED = 1,  // advertised feature
            INSTALLSTATE_REMOVED = 1,  // component being removed (action state, not settable)
            INSTALLSTATE_ABSENT = 2,  // uninstalled (or action state absent but clients remain)
            INSTALLSTATE_LOCAL = 3,  // installed on local drive
            INSTALLSTATE_SOURCE = 4,  // run from source, CD or net
            INSTALLSTATE_DEFAULT = 5,  // use default, local or source
        }

        #endregion

        static private string InstallStateString(INSTALLSTATE installState)
        {
            string state;
            switch (installState)
            {
                case INSTALLSTATE.INSTALLSTATE_NOTUSED: state = "Disabled"; break;
                case INSTALLSTATE.INSTALLSTATE_BADCONFIG: state = "Corrupt"; break;
                case INSTALLSTATE.INSTALLSTATE_INCOMPLETE: state = "Incomplete"; break;
                case INSTALLSTATE.INSTALLSTATE_SOURCEABSENT: state = "Absent"; break;
                case INSTALLSTATE.INSTALLSTATE_MOREDATA: state = "Buffer Overflow"; break;
                case INSTALLSTATE.INSTALLSTATE_INVALIDARG: state = "Invalid"; break;
                case INSTALLSTATE.INSTALLSTATE_UNKNOWN: state = "Unknown"; break;
                case INSTALLSTATE.INSTALLSTATE_BROKEN: state = "Broken"; break;
                case INSTALLSTATE.INSTALLSTATE_ADVERTISED: state = "Advertised"; break;
                case INSTALLSTATE.INSTALLSTATE_ABSENT: state = "Absent"; break;
                case INSTALLSTATE.INSTALLSTATE_LOCAL: state = "Local"; break;
                case INSTALLSTATE.INSTALLSTATE_SOURCE: state = "Source"; break;
                case INSTALLSTATE.INSTALLSTATE_DEFAULT: state = "Installed"; break;
                default: state = installState.ToString(); break;
            }
            return state;
        }

        static private string ContextString(int installedContext)
        {
            StringBuilder sbContext = new StringBuilder();
            if ((installedContext & MSIINSTALLCONTEXT_USERMANAGED) > 0)
            {
                sbContext.Append("Managed");
            }
            if ((installedContext & MSIINSTALLCONTEXT_USER) > 0)
            {
                if (sbContext.Length > 0)
                    sbContext.Append(", ");
                sbContext.Append("User");
            }
            if ((installedContext & MSIINSTALLCONTEXT_MACHINE) > 0)
            {
                if (sbContext.Length > 0)
                    sbContext.Append(", ");
                sbContext.Append("Machine");
            }
            return sbContext.ToString();
        }

        static private string InstallDateString(string installDate)
        {
            // InstalledDate is a string in the format YYYYMMDD.
            DateTime dt = new DateTime(
                Convert.ToInt32(installDate.Substring(0, 4)),   // Year
                Convert.ToInt32(installDate.Substring(4, 2)),   // Month
                Convert.ToInt32(installDate.Substring(6, 2)));  // Day
            return dt.ToShortDateString();
        }

        static public void PopulateProductList(ListView list)
        {
            list.Items.Clear();
            StringBuilder sbProductCode = new StringBuilder(39);
            int i = 0;
            int installedContext = 0;
            int cchSid = 0;
            INSTALLSTATE installState = 0;

            while (0 == MsiEnumProductsEx(null, null, MSIINSTALLCONTEXT_USERMANAGED + MSIINSTALLCONTEXT_USER + MSIINSTALLCONTEXT_MACHINE,
                i++, sbProductCode, out installedContext, null, ref cchSid))
            //while (0 == MsiEnumProducts(i++, sbProductCode))
            {
                String strProductCode = sbProductCode.ToString();
                int productNameLen = 512;
                int versionStringLen = 256;
                int installDateLen = 16;

                StringBuilder sbProductName = new StringBuilder(productNameLen);
                StringBuilder sbVersionString = new StringBuilder(versionStringLen);
                StringBuilder sbInstallDateString = new StringBuilder(installDateLen);

                MsiGetProductInfo(strProductCode, "ProductName", sbProductName, ref productNameLen);
                MsiGetProductInfo(strProductCode, "VersionString", sbVersionString, ref versionStringLen);
                MsiGetProductInfo(strProductCode, "InstallDate", sbInstallDateString, ref installDateLen);
                installState = MsiQueryProductState(strProductCode);

                ListViewItem lvi = new ListViewItem(sbProductName.ToString(), 0);
                lvi.SubItems.Add(strProductCode);
                lvi.SubItems.Add(sbVersionString.ToString());
                lvi.SubItems.Add(ContextString(installedContext));
                lvi.SubItems.Add(InstallStateString(installState));
                lvi.SubItems.Add(InstallDateString(sbInstallDateString.ToString()));
                list.Items.Add(lvi);
            }
        }

        static public void PopulateFeatureList(ListView list, string productCode)
        {
            list.Items.Clear();
            StringBuilder sbFeature = new StringBuilder(39);
            StringBuilder sbParent = new StringBuilder(39);
            int i = 0;
            while (0 == MsiEnumFeatures(productCode, i++, sbFeature, sbParent))
            {
                ListViewItem lvi = new ListViewItem(sbFeature.ToString(), 0);
                lvi.SubItems.Add(InstallStateString(MsiQueryFeatureState(productCode, sbFeature.ToString())));
                list.Items.Add(lvi);
            }
        }

        static public void PopulatePatchList(ListView list, string productCode)
        {
            list.Items.Clear();
            StringBuilder sPatchCode = new StringBuilder(39);
            StringBuilder sbTransforms = new StringBuilder(1025);
            StringBuilder sbPatchName = new StringBuilder(1025);
            int i = 0;
            int buffSize = 1024;
            while (0 == MsiEnumPatches(productCode, i++, sPatchCode, sbTransforms, ref buffSize))
            {
                string patchCode = sPatchCode.ToString();
                buffSize = 1024;
                MsiGetPatchInfoEx(patchCode, productCode, null, MSIINSTALLCONTEXT_MACHINE, "DisplayName", sbPatchName, ref buffSize);
                ListViewItem lvi = new ListViewItem(sbPatchName.ToString(), 0);
                lvi.SubItems.Add(patchCode);
                list.Items.Add(lvi);
                buffSize = 1024;
            }
        }
    }
}
