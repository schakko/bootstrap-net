// <copyright file="" company="EDV Consulting Wohlers GmbH">
// 	Copyright (C) 2012 EDV Consulting Wohlers GmbH
// 	
// 	This library is free software; you can redistribute it and/or
// 	modify it under the terms of the GNU Lesser General Public
// 	License as published by the Free Software Foundation; either
// 	version 3 of the License, or (at your option) any later version.
// 
// 	This library is distributed in the hope that it will be useful,
// 	but WITHOUT ANY WARRANTY; without even the implied warranty of
// 	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// 	Lesser General Public License for more details.
// 
// 	You should have received a copy of the GNU Lesser General Public
// 	License along with this library. If not, see http://www.gnu.org/licenses/. 
// </copyright>
// <author>Daniel Vogelsang</author>
namespace Ecw.Libs.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class InputBox<T> : Form
    {
        private readonly GenericBox<T> inputText;

        public InputBox(string infoText)
        {
            InitializeComponent();


            this.inputText = new GenericBox<T>();
            Controls.Add(this.inputText);
            this.infoLabel.Text = infoText;
            this.inputText.Location = new Point(11, 70);
            this.inputText.Name = "inputText";
            this.inputText.Size = new Size(229, 20);
            this.inputText.TabIndex = 2;
        }


        public T Value
        {
            get { return this.inputText.Value; }
            set { this.inputText.Value = value; }
        }


        private void acceptButton_Click(object sender, EventArgs e)
        {
            //T t = new T();
            //Value = inputText.Text;
            //int i;

            if (Value.Equals(default(T)))
            {
                MessageBox.Show("Wert nicht innerhalb des akzeptieren Wertebereichs.", "Eingabedialog", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void InputForm_Shown(object sender, EventArgs e)
        {
            this.inputText.Focus();
        }
    }
}