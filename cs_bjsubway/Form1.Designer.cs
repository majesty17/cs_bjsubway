namespace cs_bjsubway
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.listView_lines = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_reset = new System.Windows.Forms.Button();
            this.button_select_all = new System.Windows.Forms.Button();
            this.button_select_no = new System.Windows.Forms.Button();
            this.button_scale_up = new System.Windows.Forms.Button();
            this.button_scale_down = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.WindowText;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "绘图";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView_lines
            // 
            this.listView_lines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView_lines.BackColor = System.Drawing.SystemColors.WindowText;
            this.listView_lines.CheckBoxes = true;
            this.listView_lines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_lines.FullRowSelect = true;
            this.listView_lines.GridLines = true;
            this.listView_lines.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_lines.HideSelection = false;
            this.listView_lines.Location = new System.Drawing.Point(12, 179);
            this.listView_lines.MultiSelect = false;
            this.listView_lines.Name = "listView_lines";
            this.listView_lines.Size = new System.Drawing.Size(195, 640);
            this.listView_lines.TabIndex = 2;
            this.listView_lines.UseCompatibleStateImageBehavior = false;
            this.listView_lines.View = System.Windows.Forms.View.Details;
            this.listView_lines.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_lines_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Line";
            this.columnHeader1.Width = 190;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Location = new System.Drawing.Point(213, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1050, 807);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button_reset
            // 
            this.button_reset.BackColor = System.Drawing.SystemColors.WindowText;
            this.button_reset.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_reset.Location = new System.Drawing.Point(12, 74);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(195, 56);
            this.button_reset.TabIndex = 4;
            this.button_reset.Text = "重置视角";
            this.button_reset.UseVisualStyleBackColor = false;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_select_all
            // 
            this.button_select_all.BackColor = System.Drawing.SystemColors.WindowText;
            this.button_select_all.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_select_all.Location = new System.Drawing.Point(12, 138);
            this.button_select_all.Name = "button_select_all";
            this.button_select_all.Size = new System.Drawing.Size(95, 35);
            this.button_select_all.TabIndex = 5;
            this.button_select_all.Text = "全选";
            this.button_select_all.UseVisualStyleBackColor = false;
            this.button_select_all.Click += new System.EventHandler(this.button_select_all_Click);
            // 
            // button_select_no
            // 
            this.button_select_no.BackColor = System.Drawing.SystemColors.WindowText;
            this.button_select_no.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_select_no.Location = new System.Drawing.Point(112, 138);
            this.button_select_no.Name = "button_select_no";
            this.button_select_no.Size = new System.Drawing.Size(95, 35);
            this.button_select_no.TabIndex = 6;
            this.button_select_no.Text = "反选";
            this.button_select_no.UseVisualStyleBackColor = false;
            this.button_select_no.Click += new System.EventHandler(this.button_select_no_Click);
            // 
            // button_scale_up
            // 
            this.button_scale_up.BackColor = System.Drawing.SystemColors.WindowText;
            this.button_scale_up.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_scale_up.Location = new System.Drawing.Point(228, 22);
            this.button_scale_up.Name = "button_scale_up";
            this.button_scale_up.Size = new System.Drawing.Size(36, 36);
            this.button_scale_up.TabIndex = 7;
            this.button_scale_up.Text = "+";
            this.button_scale_up.UseVisualStyleBackColor = false;
            this.button_scale_up.Click += new System.EventHandler(this.button_scale_up_Click);
            // 
            // button_scale_down
            // 
            this.button_scale_down.BackColor = System.Drawing.SystemColors.WindowText;
            this.button_scale_down.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_scale_down.Location = new System.Drawing.Point(228, 64);
            this.button_scale_down.Name = "button_scale_down";
            this.button_scale_down.Size = new System.Drawing.Size(36, 36);
            this.button_scale_down.TabIndex = 8;
            this.button_scale_down.Text = "-";
            this.button_scale_down.UseVisualStyleBackColor = false;
            this.button_scale_down.Click += new System.EventHandler(this.button_scale_down_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1275, 831);
            this.Controls.Add(this.button_scale_down);
            this.Controls.Add(this.button_scale_up);
            this.Controls.Add(this.button_select_no);
            this.Controls.Add(this.button_select_all);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listView_lines);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView_lines;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Button button_select_all;
        private System.Windows.Forms.Button button_select_no;
        private System.Windows.Forms.Button button_scale_up;
        private System.Windows.Forms.Button button_scale_down;
    }
}

