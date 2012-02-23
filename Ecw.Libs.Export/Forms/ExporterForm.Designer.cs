namespace Ecw.Libs.Export.Forms
{
    partial class ExporterForm<TSource>
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
            this.columnSelector = new System.Windows.Forms.CheckedListBox();
            this.captionLabel = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.exportTypeSelector = new System.Windows.Forms.ComboBox();
            this.delimiterSelector = new System.Windows.Forms.ComboBox();
            this.seperatorLabel = new System.Windows.Forms.Label();
            this.seperatorSelector = new System.Windows.Forms.ComboBox();
            this.delimiterLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.pathButton = new System.Windows.Forms.Button();
            this.pathInput = new System.Windows.Forms.TextBox();
            this.exportTypeLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.closeSelector = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // columnSelector
            // 
            this.columnSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.columnSelector.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.columnSelector.ColumnWidth = 160;
            this.columnSelector.FormattingEnabled = true;
            this.columnSelector.IntegralHeight = false;
            this.columnSelector.Location = new System.Drawing.Point(14, 42);
            this.columnSelector.Margin = new System.Windows.Forms.Padding(5);
            this.columnSelector.MultiColumn = true;
            this.columnSelector.Name = "columnSelector";
            this.columnSelector.Size = new System.Drawing.Size(324, 222);
            this.columnSelector.TabIndex = 0;
            this.columnSelector.CheckOnClick = true;
            // 
            // captionLabel
            // 
            this.captionLabel.BackColor = System.Drawing.SystemColors.Window;
            this.captionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.captionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.captionLabel.Location = new System.Drawing.Point(11, 11);
            this.captionLabel.Margin = new System.Windows.Forms.Padding(5);
            this.captionLabel.Name = "captionLabel";
            this.captionLabel.Size = new System.Drawing.Size(258, 28);
            this.captionLabel.TabIndex = 19;
            this.captionLabel.Text = "Felder die exportiert werden sollen:";
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.Location = new System.Drawing.Point(152, 425);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(5);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(88, 26);
            this.acceptButton.TabIndex = 21;
            this.acceptButton.Text = "OK";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(250, 425);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(88, 26);
            this.cancelButton.TabIndex = 22;
            this.cancelButton.Text = "Abbrechen";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // exportTypeSelector
            // 
            this.exportTypeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.exportTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exportTypeSelector.FormattingEnabled = true;
            this.exportTypeSelector.Location = new System.Drawing.Point(41, 309);
            this.exportTypeSelector.Margin = new System.Windows.Forms.Padding(6);
            this.exportTypeSelector.Name = "exportTypeSelector";
            this.exportTypeSelector.Size = new System.Drawing.Size(296, 21);
            this.exportTypeSelector.TabIndex = 24;
            this.exportTypeSelector.SelectionChangeCommitted += new System.EventHandler(this.exportTypeSelector_SelectionChangeCommitted);
            // 
            // delimiterSelector
            // 
            this.delimiterSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delimiterSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.delimiterSelector.Font = new System.Drawing.Font("Lucida Console", 9.5F);
            this.delimiterSelector.FormattingEnabled = true;
            this.delimiterSelector.Location = new System.Drawing.Point(283, 342);
            this.delimiterSelector.Margin = new System.Windows.Forms.Padding(6);
            this.delimiterSelector.Name = "delimiterSelector";
            this.delimiterSelector.Size = new System.Drawing.Size(54, 21);
            this.delimiterSelector.TabIndex = 25;
            this.delimiterSelector.SelectionChangeCommitted += new System.EventHandler(this.delimiterSelector_SelectionChangeCommitted);
            // 
            // seperatorLabel
            // 
            this.seperatorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.seperatorLabel.AutoSize = true;
            this.seperatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seperatorLabel.Location = new System.Drawing.Point(11, 344);
            this.seperatorLabel.Margin = new System.Windows.Forms.Padding(5);
            this.seperatorLabel.Name = "seperatorLabel";
            this.seperatorLabel.Size = new System.Drawing.Size(120, 13);
            this.seperatorLabel.TabIndex = 26;
            this.seperatorLabel.Text = "Dezimaltrennenzeichen:";
            // 
            // seperatorSelector
            // 
            this.seperatorSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.seperatorSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seperatorSelector.Font = new System.Drawing.Font("Lucida Console", 9.5F);
            this.seperatorSelector.FormattingEnabled = true;
            this.seperatorSelector.Location = new System.Drawing.Point(134, 342);
            this.seperatorSelector.Margin = new System.Windows.Forms.Padding(6);
            this.seperatorSelector.Name = "seperatorSelector";
            this.seperatorSelector.Size = new System.Drawing.Size(41, 21);
            this.seperatorSelector.TabIndex = 27;
            this.seperatorSelector.SelectedIndexChanged += new System.EventHandler(this.seperatorSelector_SelectedIndexChanged);
            // 
            // delimiterLabel
            // 
            this.delimiterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delimiterLabel.AutoSize = true;
            this.delimiterLabel.Location = new System.Drawing.Point(195, 344);
            this.delimiterLabel.Margin = new System.Windows.Forms.Padding(5);
            this.delimiterLabel.Name = "delimiterLabel";
            this.delimiterLabel.Size = new System.Drawing.Size(75, 13);
            this.delimiterLabel.TabIndex = 28;
            this.delimiterLabel.Text = "Trennzeichen:";
            // 
            // pathLabel
            // 
            this.pathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pathLabel.AutoSize = true;
            this.pathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathLabel.Location = new System.Drawing.Point(11, 381);
            this.pathLabel.Margin = new System.Windows.Forms.Padding(5);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(61, 13);
            this.pathLabel.TabIndex = 31;
            this.pathLabel.Text = "Dateiname:";
            // 
            // pathButton
            // 
            this.pathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathButton.Location = new System.Drawing.Point(305, 376);
            this.pathButton.Margin = new System.Windows.Forms.Padding(5);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(33, 23);
            this.pathButton.TabIndex = 32;
            this.pathButton.Text = "...";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // pathInput
            // 
            this.pathInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathInput.Location = new System.Drawing.Point(74, 378);
            this.pathInput.Name = "pathInput";
            this.pathInput.Size = new System.Drawing.Size(221, 20);
            this.pathInput.TabIndex = 33;
            // 
            // exportTypeLabel
            // 
            this.exportTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportTypeLabel.AutoSize = true;
            this.exportTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportTypeLabel.Location = new System.Drawing.Point(11, 312);
            this.exportTypeLabel.Margin = new System.Windows.Forms.Padding(5);
            this.exportTypeLabel.Name = "exportTypeLabel";
            this.exportTypeLabel.Size = new System.Drawing.Size(23, 13);
            this.exportTypeLabel.TabIndex = 34;
            this.exportTypeLabel.Text = "Art:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.label4.Location = new System.Drawing.Point(11, 274);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 18);
            this.label4.TabIndex = 35;
            this.label4.Text = "Exportziel:";
            // 
            // closeSelector
            // 
            this.closeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeSelector.AutoSize = true;
            this.closeSelector.BackColor = System.Drawing.Color.Transparent;
            this.closeSelector.Checked = true;
            this.closeSelector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeSelector.Location = new System.Drawing.Point(14, 431);
            this.closeSelector.Name = "closeSelector";
            this.closeSelector.Size = new System.Drawing.Size(109, 17);
            this.closeSelector.TabIndex = 36;
            this.closeSelector.Text = "Fenster schließen";
            this.closeSelector.UseVisualStyleBackColor = false;
            // 
            // ExporterForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(349, 462);
            this.Controls.Add(this.closeSelector);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exportTypeLabel);
            this.Controls.Add(this.pathInput);
            this.Controls.Add(this.pathButton);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.delimiterLabel);
            this.Controls.Add(this.seperatorSelector);
            this.Controls.Add(this.seperatorLabel);
            this.Controls.Add(this.delimiterSelector);
            this.Controls.Add(this.exportTypeSelector);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.captionLabel);
            this.Controls.Add(this.columnSelector);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(365, 400);
            this.Name = "ExporterForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Text = "Listenexport";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox columnSelector;
        internal System.Windows.Forms.Label captionLabel;
        internal System.Windows.Forms.Button acceptButton;
        internal System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox exportTypeSelector;
        private System.Windows.Forms.ComboBox delimiterSelector;
        private System.Windows.Forms.Label seperatorLabel;
        private System.Windows.Forms.ComboBox seperatorSelector;
        private System.Windows.Forms.Label delimiterLabel;
        private System.Windows.Forms.Label pathLabel;
        internal System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.TextBox pathInput;
        private System.Windows.Forms.Label exportTypeLabel;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox closeSelector;
    }
}