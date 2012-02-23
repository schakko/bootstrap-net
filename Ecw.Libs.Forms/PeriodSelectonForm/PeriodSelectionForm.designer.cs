namespace Ecw.Libs.Forms
{
    partial class PeriodSelectionForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.nextYearButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.yearButton = new System.Windows.Forms.Button();
            this.previousYearButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // nextYearButton
            // 
            this.nextYearButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nextYearButton.BackColor = System.Drawing.SystemColors.Control;
            this.nextYearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.nextYearButton.Location = new System.Drawing.Point(362, 2);
            this.nextYearButton.Name = "nextYearButton";
            this.nextYearButton.Size = new System.Drawing.Size(44, 26);
            this.nextYearButton.TabIndex = 4;
            this.nextYearButton.Text = "→";
            this.nextYearButton.UseVisualStyleBackColor = true;
            this.nextYearButton.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.BackColor = System.Drawing.SystemColors.Control;
            this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptButton.Location = new System.Drawing.Point(426, 600);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(5);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(89, 26);
            this.acceptButton.TabIndex = 0;
            this.acceptButton.Text = "OK";
            this.acceptButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(525, 600);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(88, 26);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Abbrechen";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // yearButton
            // 
            this.yearButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.yearButton.BackColor = System.Drawing.SystemColors.Control;
            this.yearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.yearButton.Location = new System.Drawing.Point(268, 2);
            this.yearButton.Name = "yearButton";
            this.yearButton.Size = new System.Drawing.Size(88, 26);
            this.yearButton.TabIndex = 3;
            this.yearButton.UseVisualStyleBackColor = true;
            this.yearButton.Click += new System.EventHandler(this.yearButton_Click);
            // 
            // previousYearButton
            // 
            this.previousYearButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.previousYearButton.BackColor = System.Drawing.SystemColors.Control;
            this.previousYearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.previousYearButton.Location = new System.Drawing.Point(218, 2);
            this.previousYearButton.Name = "previousYearButton";
            this.previousYearButton.Size = new System.Drawing.Size(44, 26);
            this.previousYearButton.TabIndex = 2;
            this.previousYearButton.Text = "←";
            this.previousYearButton.UseVisualStyleBackColor = true;
            this.previousYearButton.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(624, 553);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // PeriodSelectionForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(624, 637);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.previousYearButton);
            this.Controls.Add(this.yearButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.nextYearButton);
            this.DecorationMargin = new System.Windows.Forms.Padding(0, 0, 0, 49);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.GlassMargin = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PeriodSelectionForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(84)))), ((int)(((byte)(254)))));
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button nextYearButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button yearButton;
        private System.Windows.Forms.Button previousYearButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

