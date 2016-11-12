namespace YetAnotherFileManager
{
    partial class FileManagerUi
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManagerUi));
            this.topToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewLeft = new System.Windows.Forms.ListView();
            this.NameLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastModifiedLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textBoxDirectoryPathLeft = new System.Windows.Forms.TextBox();
            this.flowPanelLeftDisks = new System.Windows.Forms.FlowLayoutPanel();
            this.listViewRight = new System.Windows.Forms.ListView();
            this.NameRight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeRight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxDirectoryPathRight = new System.Windows.Forms.TextBox();
            this.flowPanelRightDisks = new System.Windows.Forms.FlowLayoutPanel();
            this.topToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topToolStrip
            // 
            this.topToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAbout,
            this.toolStripButtonHelp});
            this.topToolStrip.Location = new System.Drawing.Point(0, 0);
            this.topToolStrip.Name = "topToolStrip";
            this.topToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.topToolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.topToolStrip.Size = new System.Drawing.Size(645, 25);
            this.topToolStrip.TabIndex = 0;
            this.topToolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(44, 22);
            this.toolStripButtonAbout.Text = "About";
            this.toolStripButtonAbout.ToolTipText = "About";
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHelp.Image")));
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonHelp.Text = "Help";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewLeft);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxDirectoryPathLeft);
            this.splitContainer1.Panel1.Controls.Add(this.flowPanelLeftDisks);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewRight);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxDirectoryPathRight);
            this.splitContainer1.Panel2.Controls.Add(this.flowPanelRightDisks);
            this.splitContainer1.Size = new System.Drawing.Size(645, 345);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 1;
            // 
            // listViewLeft
            // 
            this.listViewLeft.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewLeft.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameLeft,
            this.TypeLeft,
            this.LastModifiedLeft});
            this.listViewLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLeft.FullRowSelect = true;
            this.listViewLeft.LabelEdit = true;
            this.listViewLeft.Location = new System.Drawing.Point(0, 74);
            this.listViewLeft.Name = "listViewLeft";
            this.listViewLeft.Size = new System.Drawing.Size(302, 271);
            this.listViewLeft.SmallImageList = this.imageList1;
            this.listViewLeft.TabIndex = 2;
            this.listViewLeft.UseCompatibleStateImageBehavior = false;
            this.listViewLeft.View = System.Windows.Forms.View.Details;
            // 
            // NameLeft
            // 
            this.NameLeft.Text = "Name";
            this.NameLeft.Width = 40;
            // 
            // TypeLeft
            // 
            this.TypeLeft.Text = "Type";
            this.TypeLeft.Width = 36;
            // 
            // LastModifiedLeft
            // 
            this.LastModifiedLeft.Text = "Last Modified";
            this.LastModifiedLeft.Width = 222;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder-close.png");
            this.imageList1.Images.SetKeyName(1, "network_local.png");
            this.imageList1.Images.SetKeyName(2, "file.png");
            // 
            // textBoxDirectoryPathLeft
            // 
            this.textBoxDirectoryPathLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBoxDirectoryPathLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxDirectoryPathLeft.Location = new System.Drawing.Point(0, 54);
            this.textBoxDirectoryPathLeft.Name = "textBoxDirectoryPathLeft";
            this.textBoxDirectoryPathLeft.ReadOnly = true;
            this.textBoxDirectoryPathLeft.Size = new System.Drawing.Size(302, 20);
            this.textBoxDirectoryPathLeft.TabIndex = 1;
            // 
            // flowPanelLeftDisks
            // 
            this.flowPanelLeftDisks.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPanelLeftDisks.Location = new System.Drawing.Point(0, 0);
            this.flowPanelLeftDisks.Name = "flowPanelLeftDisks";
            this.flowPanelLeftDisks.Size = new System.Drawing.Size(302, 54);
            this.flowPanelLeftDisks.TabIndex = 0;
            // 
            // listViewRight
            // 
            this.listViewRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameRight,
            this.TypeRight,
            this.LastModified});
            this.listViewRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRight.FullRowSelect = true;
            this.listViewRight.LabelEdit = true;
            this.listViewRight.Location = new System.Drawing.Point(0, 74);
            this.listViewRight.Name = "listViewRight";
            this.listViewRight.Size = new System.Drawing.Size(339, 271);
            this.listViewRight.SmallImageList = this.imageList1;
            this.listViewRight.TabIndex = 2;
            this.listViewRight.UseCompatibleStateImageBehavior = false;
            this.listViewRight.View = System.Windows.Forms.View.Details;
            // 
            // NameRight
            // 
            this.NameRight.Text = "Name";
            // 
            // TypeRight
            // 
            this.TypeRight.Text = "Type";
            // 
            // LastModified
            // 
            this.LastModified.Text = "LastModified";
            // 
            // textBoxDirectoryPathRight
            // 
            this.textBoxDirectoryPathRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBoxDirectoryPathRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxDirectoryPathRight.Location = new System.Drawing.Point(0, 54);
            this.textBoxDirectoryPathRight.Name = "textBoxDirectoryPathRight";
            this.textBoxDirectoryPathRight.ReadOnly = true;
            this.textBoxDirectoryPathRight.Size = new System.Drawing.Size(339, 20);
            this.textBoxDirectoryPathRight.TabIndex = 1;
            // 
            // flowPanelRightDisks
            // 
            this.flowPanelRightDisks.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPanelRightDisks.Location = new System.Drawing.Point(0, 0);
            this.flowPanelRightDisks.Name = "flowPanelRightDisks";
            this.flowPanelRightDisks.Size = new System.Drawing.Size(339, 54);
            this.flowPanelRightDisks.TabIndex = 0;
            // 
            // FileManagerUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 370);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.topToolStrip);
            this.Name = "FileManagerUi";
            this.Text = "Yet Another File Manager -_-";
            this.topToolStrip.ResumeLayout(false);
            this.topToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip topToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxDirectoryPathLeft;
        private System.Windows.Forms.FlowLayoutPanel flowPanelLeftDisks;
        private System.Windows.Forms.FlowLayoutPanel flowPanelRightDisks;
        private System.Windows.Forms.TextBox textBoxDirectoryPathRight;
        private System.Windows.Forms.ListView listViewLeft;
        private System.Windows.Forms.ImageList imageList1;
        private new System.Windows.Forms.ColumnHeader NameLeft;
        private System.Windows.Forms.ColumnHeader TypeLeft;
        private System.Windows.Forms.ColumnHeader LastModifiedLeft;
        private System.Windows.Forms.ListView listViewRight;
        private System.Windows.Forms.ColumnHeader NameRight;
        private System.Windows.Forms.ColumnHeader TypeRight;
        private System.Windows.Forms.ColumnHeader LastModified;
    }
}

