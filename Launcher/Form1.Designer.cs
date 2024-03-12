namespace Launcher
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Add = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.Cover = new System.Windows.Forms.PictureBox();
            this.Play = new System.Windows.Forms.Button();
            this.Description = new System.Windows.Forms.Label();
            this.NameLauncher = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Close = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cover)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.37892F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.62108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.NameLauncher, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.908686F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.09132F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1280, 721);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.Add, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.listBox1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 66);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(269, 653);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.Black;
            this.Add.Image = global::Launcher.Properties.Resources.plus;
            this.Add.Location = new System.Drawing.Point(3, 612);
            this.Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(261, 39);
            this.Add.TabIndex = 0;
            this.Add.Text = "Add";
            this.Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(3, 28);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox1.Size = new System.Drawing.Size(263, 580);
            this.listBox1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 2);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 22);
            this.textBox1.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.Cover, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.Play, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.Description, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(443, 66);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.14484F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.85515F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(519, 653);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // Cover
            // 
            this.Cover.Location = new System.Drawing.Point(4, 3);
            this.Cover.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cover.Name = "Cover";
            this.Cover.Size = new System.Drawing.Size(511, 254);
            this.Cover.TabIndex = 3;
            this.Cover.TabStop = false;
            this.Cover.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Play
            // 
            this.Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.Play.BackColor = System.Drawing.Color.ForestGreen;
            this.Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play.Image = global::Launcher.Properties.Resources.play;
            this.Play.Location = new System.Drawing.Point(91, 592);
            this.Play.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(336, 58);
            this.Play.TabIndex = 2;
            this.Play.Text = "Play";
            this.Play.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Play.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Play.UseVisualStyleBackColor = false;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Description
            // 
            this.Description.AutoSize = true;
            this.Description.Location = new System.Drawing.Point(4, 426);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(44, 16);
            this.Description.TabIndex = 4;
            this.Description.Text = "label1";
            // 
            // NameLauncher
            // 
            this.NameLauncher.AutoSize = true;
            this.NameLauncher.BackColor = System.Drawing.Color.Transparent;
            this.NameLauncher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NameLauncher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameLauncher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLauncher.ForeColor = System.Drawing.SystemColors.Window;
            this.NameLauncher.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NameLauncher.ImageKey = "(none)";
            this.NameLauncher.Location = new System.Drawing.Point(3, 0);
            this.NameLauncher.Name = "NameLauncher";
            this.NameLauncher.Size = new System.Drawing.Size(434, 64);
            this.NameLauncher.TabIndex = 5;
            this.NameLauncher.Text = "Launcher";
            this.NameLauncher.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.21085F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.789157F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.Close, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.button4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.button5, 2, 0);
            this.tableLayoutPanel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(443, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.4138F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.58621F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(183, 41);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // Close
            // 
            this.Close.AutoSize = true;
            this.Close.BackColor = System.Drawing.Color.Transparent;
            this.Close.FlatAppearance.BorderSize = 0;
            this.Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Close.Image = global::Launcher.Properties.Resources.x1;
            this.Close.Location = new System.Drawing.Point(123, 2);
            this.Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(53, 37);
            this.Close.TabIndex = 0;
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.Quit);
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::Launcher.Properties.Resources.maximize_21;
            this.button4.Location = new System.Drawing.Point(63, 2);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(53, 37);
            this.button4.TabIndex = 1;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Maximize);
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = global::Launcher.Properties.Resources.minus;
            this.button5.Location = new System.Drawing.Point(3, 2);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(53, 37);
            this.button5.TabIndex = 2;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Minimized);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cover)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label NameLauncher;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.PictureBox Cover;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label Description;
    }
    }

