namespace ProductCatalog
{
    partial class FeatureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatureForm));
            this.featureList = new System.Windows.Forms.ListView();
            this.featureHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // featureList
            // 
            this.featureList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.featureList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.featureList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.featureHeader,
            this.stateHeader});
            this.featureList.FullRowSelect = true;
            this.featureList.GridLines = true;
            this.featureList.Location = new System.Drawing.Point(0, 1);
            this.featureList.Name = "featureList";
            this.featureList.Size = new System.Drawing.Size(294, 247);
            this.featureList.TabIndex = 0;
            this.featureList.UseCompatibleStateImageBehavior = false;
            this.featureList.View = System.Windows.Forms.View.Details;
            this.featureList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.featureList_ColumnClick);
            this.featureList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.featureList_KeyDown);
            this.featureList.Resize += new System.EventHandler(this.featureList_Resize);
            // 
            // featureHeader
            // 
            this.featureHeader.Text = "Feature";
            this.featureHeader.Width = 200;
            // 
            // stateHeader
            // 
            this.stateHeader.Text = "State";
            this.stateHeader.Width = 70;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 251);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(292, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsCount
            // 
            this.tsCount.Name = "tsCount";
            this.tsCount.Size = new System.Drawing.Size(58, 17);
            this.tsCount.Text = "No items.";
            // 
            // FeatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.featureList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeatureForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Features";
            this.Load += new System.EventHandler(this.FeatureForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView featureList;
        private System.Windows.Forms.ColumnHeader featureHeader;
        private System.Windows.Forms.ColumnHeader stateHeader;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsCount;
    }
}