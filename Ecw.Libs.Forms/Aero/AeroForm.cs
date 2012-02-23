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
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AeroForm : Form
    {
        private Padding decorationMargin;
        private bool drawDecoration;

        public AeroForm()
        {
            BackColor = SystemColors.Window;
        }

        [Description("Gibt den inneren Margin an. 0 zum deaktivieren")]
        [Category("Layout")]
        [DefaultValue(typeof (Padding), "0;0;0;49")]
        public Padding DecorationMargin
        {
            get { return this.decorationMargin; }
            set
            {
                this.decorationMargin = value;
                this.drawDecoration = value.Bottom != 0 || value.Top != 0 || value.Left != 0 || value.Right != 0;

                // Refresh the form.
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (this.drawDecoration)
            {
                PaintDecoration(e.Graphics);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.drawDecoration)
            {
                var rect = new Rectangle(this.decorationMargin.Left, this.decorationMargin.Top, Width - this.decorationMargin.Right, Height - this.decorationMargin.Bottom);
                Invalidate(rect, false);
            }
        }

        protected void PaintDecoration(Graphics graphics)
        {
            // Paint the glass effect.
            Brush brush = null;
            Pen pen = null;

            try
            {
                var rectangles = new Rectangle[4];

                brush = new SolidBrush(SystemColors.Control);
                pen = new Pen(new SolidBrush(Color.FromArgb(223, 223, 223)));

                // Gibt die höhe des/der linken und rechten Rechteckes/Linie an
                int sideHeight = ClientRectangle.Height - this.decorationMargin.Top - this.decorationMargin.Bottom;
                int bottomY = ClientRectangle.Height - this.decorationMargin.Bottom;
                int rightX = ClientRectangle.Width - this.decorationMargin.Right;

                // Top
                rectangles[0] = new Rectangle(0, 0, ClientRectangle.Width, this.decorationMargin.Top);
                // Bottom
                rectangles[1] = new Rectangle(0, bottomY, ClientRectangle.Width, this.decorationMargin.Bottom);
                // Left
                rectangles[2] = new Rectangle(0, this.decorationMargin.Top, this.decorationMargin.Left, sideHeight);
                // Right
                rectangles[3] = new Rectangle(rightX, this.decorationMargin.Top, this.decorationMargin.Right, sideHeight);

                graphics.FillRectangles(brush, rectangles);

                if (bottomY > this.decorationMargin.Top && rightX > this.decorationMargin.Left)
                {
                    // Top-Left to Top-Right
                    if (this.decorationMargin.Top > 0)
                    {
                        graphics.DrawLine(pen, new Point(this.decorationMargin.Left, this.decorationMargin.Top), new Point(rightX, this.decorationMargin.Top));
                    }
                    // Top-Right To Bottom-Right
                    if (this.decorationMargin.Right > 0)
                    {
                        graphics.DrawLine(pen, new Point(rightX, this.decorationMargin.Top), new Point(rightX, bottomY));
                    }

                    // Bottom-Right To BottomLeft
                    if (this.decorationMargin.Bottom > 0)
                    {
                        graphics.DrawLine(pen, new Point(rightX, bottomY), new Point(this.decorationMargin.Left, bottomY));
                    }
                    // Bottom-Left To Top-Left
                    if (this.decorationMargin.Left > 0)
                    {
                        graphics.DrawLine(pen, new Point(this.decorationMargin.Left, bottomY), new Point(this.decorationMargin.Left, this.decorationMargin.Top));
                    }
                }
            }
            finally
            {
                if (brush != null)
                {
                    brush.Dispose();
                }
                if (pen != null)
                {
                    pen.Dispose();
                }
            }
        }
    }
}