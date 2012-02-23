namespace Ecw.Libs.Forms.Test
{
    partial class TestForm
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
            this.listButton4 = new Ecw.Libs.Forms.ListButton();
            this.listButton3 = new Ecw.Libs.Forms.ListButton();
            this.listButton2 = new Ecw.Libs.Forms.ListButton();
            this.listButton1 = new Ecw.Libs.Forms.ListButton();
            this.SuspendLayout();
            // 
            // listButton4
            // 
            this.listButton4.Location = new System.Drawing.Point(59, 172);
            this.listButton4.MinimumSize = new System.Drawing.Size(4, 4);
            this.listButton4.Name = "listButton4";
            this.listButton4.Padding = new System.Windows.Forms.Padding(6, 20, 0, 0);
            this.listButton4.Size = new System.Drawing.Size(165, 58);
            this.listButton4.TabIndex = 3;
            this.listButton4.Text = "Hier steht ein sehr langer Text.";
            // 
            // listButton3
            // 
            this.listButton3.Location = new System.Drawing.Point(59, 140);
            this.listButton3.MinimumSize = new System.Drawing.Size(4, 4);
            this.listButton3.Name = "listButton3";
            this.listButton3.Padding = new System.Windows.Forms.Padding(6);
            this.listButton3.Size = new System.Drawing.Size(133, 26);
            this.listButton3.TabIndex = 2;
            this.listButton3.Text = "listButton3";
            // 
            // listButton2
            // 
            this.listButton2.Location = new System.Drawing.Point(59, 108);
            this.listButton2.MinimumSize = new System.Drawing.Size(4, 4);
            this.listButton2.Name = "listButton2";
            this.listButton2.Padding = new System.Windows.Forms.Padding(4);
            this.listButton2.Size = new System.Drawing.Size(133, 26);
            this.listButton2.TabIndex = 1;
            this.listButton2.Text = "listButton2";
            // 
            // listButton1
            // 
            this.listButton1.Location = new System.Drawing.Point(59, 76);
            this.listButton1.MinimumSize = new System.Drawing.Size(4, 4);
            this.listButton1.Name = "listButton1";
            this.listButton1.Padding = new System.Windows.Forms.Padding(4);
            this.listButton1.Size = new System.Drawing.Size(133, 26);
            this.listButton1.TabIndex = 0;
            this.listButton1.Text = "listButton1";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(388, 326);
            this.Controls.Add(this.listButton4);
            this.Controls.Add(this.listButton3);
            this.Controls.Add(this.listButton2);
            this.Controls.Add(this.listButton1);
            this.Name = "TestForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ListButton listButton1;
        private ListButton listButton2;
        private ListButton listButton3;
        private ListButton listButton4;



    }
}