namespace iPem.Keys {
    partial class DbCleanKey {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbCleanKey));
            this.GetKeyButton = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.gviewport = new System.Windows.Forms.GroupBox();
            this.viewport = new System.Windows.Forms.TableLayoutPanel();
            this.notice = new System.Windows.Forms.Label();
            this.tips = new System.Windows.Forms.Label();
            this.gviewport.SuspendLayout();
            this.viewport.SuspendLayout();
            this.SuspendLayout();
            // 
            // GetKeyButton
            // 
            this.GetKeyButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GetKeyButton.Location = new System.Drawing.Point(62, 35);
            this.GetKeyButton.Margin = new System.Windows.Forms.Padding(0);
            this.GetKeyButton.Name = "GetKeyButton";
            this.GetKeyButton.Size = new System.Drawing.Size(120, 30);
            this.GetKeyButton.TabIndex = 0;
            this.GetKeyButton.Text = "获取密码(&G)";
            this.GetKeyButton.UseVisualStyleBackColor = true;
            this.GetKeyButton.Click += new System.EventHandler(this.GetKeyButton_Click);
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.Color.White;
            this.Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Password.Location = new System.Drawing.Point(0, 0);
            this.Password.Margin = new System.Windows.Forms.Padding(0);
            this.Password.Name = "Password";
            this.Password.ReadOnly = true;
            this.Password.Size = new System.Drawing.Size(244, 21);
            this.Password.TabIndex = 1;
            this.Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gviewport
            // 
            this.gviewport.Controls.Add(this.viewport);
            this.gviewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gviewport.Location = new System.Drawing.Point(10, 10);
            this.gviewport.Name = "gviewport";
            this.gviewport.Padding = new System.Windows.Forms.Padding(10);
            this.gviewport.Size = new System.Drawing.Size(264, 162);
            this.gviewport.TabIndex = 2;
            this.gviewport.TabStop = false;
            this.gviewport.Text = "确认密码";
            // 
            // viewport
            // 
            this.viewport.ColumnCount = 1;
            this.viewport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.viewport.Controls.Add(this.Password, 0, 0);
            this.viewport.Controls.Add(this.GetKeyButton, 0, 2);
            this.viewport.Controls.Add(this.notice, 0, 4);
            this.viewport.Controls.Add(this.tips, 0, 6);
            this.viewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewport.Location = new System.Drawing.Point(10, 24);
            this.viewport.Name = "viewport";
            this.viewport.RowCount = 7;
            this.viewport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.viewport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.viewport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.viewport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.viewport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.viewport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.viewport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.viewport.Size = new System.Drawing.Size(244, 128);
            this.viewport.TabIndex = 2;
            // 
            // notice
            // 
            this.notice.AutoSize = true;
            this.notice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notice.Location = new System.Drawing.Point(3, 83);
            this.notice.Name = "notice";
            this.notice.Size = new System.Drawing.Size(238, 20);
            this.notice.TabIndex = 1;
            this.notice.Text = "特别提醒：";
            this.notice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tips
            // 
            this.tips.AutoSize = true;
            this.tips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tips.Location = new System.Drawing.Point(3, 108);
            this.tips.Name = "tips";
            this.tips.Size = new System.Drawing.Size(238, 20);
            this.tips.TabIndex = 1;
            this.tips.Text = "密码仅当天有效，过期后请重新获取。";
            this.tips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DbCleanKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 182);
            this.Controls.Add(this.gviewport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbCleanKey";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码生成器";
            this.gviewport.ResumeLayout(false);
            this.viewport.ResumeLayout(false);
            this.viewport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetKeyButton;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.GroupBox gviewport;
        private System.Windows.Forms.TableLayoutPanel viewport;
        private System.Windows.Forms.Label notice;
        private System.Windows.Forms.Label tips;
    }
}