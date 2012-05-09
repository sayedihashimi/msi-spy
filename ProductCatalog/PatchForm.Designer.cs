namespace ProductCatalog
{
    partial class PatchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatchForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.patchList = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 193);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(399, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsCount
            // 
            this.tsCount.Name = "tsCount";
            this.tsCount.Size = new System.Drawing.Size(58, 17);
            this.tsCount.Text = "No items.";
            // 
            // patchList
            // 
            this.patchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patchList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.patchList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.codeHeader});
            this.patchList.FullRowSelect = true;
            this.patchList.GridLines = true;
            this.patchList.Location = new System.Drawing.Point(0, -1);
            this.patchList.Name = "patchList";
            this.patchList.Size = new System.Drawing.Size(399, 191);
            this.patchList.TabIndex = 1;
            this.patchList.UseCompatibleStateImageBehavior = false;
            this.patchList.View = System.Windows.Forms.View.Details;
            this.patchList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.patchList_ColumnClick);
            this.patchList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.patchList_KeyDown);
            this.patchList.Resize += new System.EventHandler(this.patchList_Resize);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Patch Name";
            this.nameHeader.Width = 140;
            // 
            // codeHeader
            // 
            this.codeHeader.Text = "Patch Code";
            this.codeHeader.Width = 240;
            // 
            // PatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 215);
            this.Controls.Add(this.patchList);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatchForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Applied Patches";
            this.Load += new System.EventHandler(this.PatchForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsCount;
        private System.Windows.Forms.ListView patchList;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader codeHeader;
    }
}