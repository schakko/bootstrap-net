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
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ListButton : Control
    {
        private const float rounding = 4;

        private static readonly StringFormat center = new StringFormat {Alignment = StringAlignment.Near};

        private readonly ColorSet defaultSet = new ColorSet
        {
            Border = Color.White,
            OuterTop = Color.White,
            OuterBottom = Color.White,
            InnerTop = Color.White,
            InnerBottom = Color.White,
        };

        private readonly ColorSet downSet = new ColorSet
        {
            Border = Color.FromArgb(unchecked((int) 0xFF7DA2CE)),
            InnerTop = Color.FromArgb(unchecked((int) 0xFFDDECFD)),
            InnerBottom = Color.FromArgb(unchecked((int) 0xFFC2DCFD)),
            OuterTop = Color.FromArgb(unchecked((int) 0xFFEBF4FD)),
            OuterBottom = Color.FromArgb(unchecked((int) 0xFFDBEAFD)),
        };


        private readonly ColorSet focusSet = new ColorSet
        {
            Border = Color.FromArgb(unchecked((int) 0xFFDADADA)),
            OuterTop = Color.FromArgb(unchecked((int) 0xFFFBFBFC)),
            OuterBottom = Color.FromArgb(unchecked((int) 0xFFF1F1F1)),
            InnerTop = Color.FromArgb(unchecked((int) 0xFFF9F9F9)),
            InnerBottom = Color.FromArgb(unchecked((int) 0xFFE6E6E6)),
        };

        private readonly ColorSet hoverSet = new ColorSet
        {
            Border = Color.FromArgb(unchecked((int) 0xFFB9D7FC)),
            InnerTop = Color.FromArgb(unchecked((int) 0xFFFCFDFF)),
            InnerBottom = Color.FromArgb(unchecked((int) 0xFFEDF5FF)),
            OuterTop = Color.FromArgb(unchecked((int) 0xFFFDFEFF)),
            OuterBottom = Color.FromArgb(unchecked((int) 0xFFF3F8FF)),
        };

        private Bitmap backgroundBitmap;
        private bool clicked;
        private Rectangle contentRectangle;
        private Bitmap downBitmap;
        private Bitmap focusBitmap;
        private Bitmap hoverBitmap;
        private bool hovered;
        private GraphicsPath innerPath;
        private Rectangle innerRectangle;
        private GraphicsPath outerPath;
        private Rectangle outerRectangle;


        //private ColorSet hoverSet = new ColorSet
        //{
        //    Border = Color.Black,
        //    OuterTop = Color.Yellow,
        //    OuterBottom = Color.Red,
        //    InnerTop = Color.LightBlue,
        //    InnerBottom = Color.Blue,
        //};


        public ListButton()
        {
            MinimumSize = new Size(4, 4);
            Padding = new Padding(4);
        }


        //[Browsable(true)]
        //[Category("Appearance")]
        //public ContentAlignment TextAlignment { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bitmap;

            if (this.clicked || Focused && this.hovered)
            {
                bitmap = this.downBitmap;
            }
            else if (this.hovered)
            {
                bitmap = this.hoverBitmap;
            }
            else if (Focused)
            {
                bitmap = this.focusBitmap;
            }
            else
            {
                bitmap = this.backgroundBitmap;
            }

            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);

            using (var textBrush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, textBrush, GetInnerRectangle(this.innerRectangle, Padding));
            }
        }


        private void SetBitmaps()
        {
            this.backgroundBitmap = GetBitmap(this.defaultSet);
            this.downBitmap = GetBitmap(this.downSet);
            this.hoverBitmap = GetBitmap(this.hoverSet);
            this.focusBitmap = GetBitmap(this.focusSet);
        }

        private Bitmap GetBitmap(ColorSet set)
        {
            var result = new Bitmap(Width, Height);
            Brush outerBrush = null;
            Brush innerBrush = null;
            Pen borderPen = null;
            Graphics g = null;

            try
            {
                g = Graphics.FromImage(result);
                g.SmoothingMode = SmoothingMode.HighQuality;

                outerBrush = new LinearGradientBrush(this.outerRectangle, set.OuterTop, set.OuterBottom, LinearGradientMode.Vertical);
                innerBrush = new LinearGradientBrush(this.innerRectangle, set.InnerTop, set.InnerBottom, LinearGradientMode.Vertical);
                borderPen = new Pen(set.Border);

                g.FillPath(outerBrush, this.outerPath);
                g.DrawPath(borderPen, this.outerPath);
                g.FillPath(innerBrush, this.innerPath);
                //g.DrawString( Text, Font,textBrush, (float)5, (float)(Height - Font.Height) / 2, center);             
            }
            finally
            {
                if (outerBrush != null)
                {
                    outerBrush.Dispose();
                }
                if (innerBrush != null)
                {
                    innerBrush.Dispose();
                }
                if (borderPen != null)
                {
                    borderPen.Dispose();
                }
                if (g != null)
                {
                    g.Dispose();
                }
            }
            return result;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.outerRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            this.innerRectangle = new Rectangle(1, 1, Width - 3, Height - 3);


            this.outerPath = GetRoundedRect(this.outerRectangle);
            this.innerPath = GetRoundedRect(this.innerRectangle);


            SetBitmaps();
            Invalidate();
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            Invalidate();
        }

        private Rectangle GetInnerRectangle(Rectangle rect, Padding pad)
        {
            return new Rectangle(rect.X + pad.Left, rect.Y + pad.Top, rect.Width - pad.Left - pad.Right, rect.Height - pad.Top - pad.Bottom);
        }


        private static GraphicsPath GetRoundedRect(Rectangle rect)
        {
            var path = new GraphicsPath();

            var arc = new RectangleF(rect.X, rect.Y, rounding, rounding);
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - rounding;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - rounding;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            SetBitmaps();
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.hovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.hovered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            this.clicked = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.clicked = false;
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                OnClick(new EventArgs());
            }
            Invalidate();
        }


        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        #region Nested type: ColorSet

        private struct ColorSet
        {
            public Color Border;
            public Color InnerBottom;
            public Color InnerTop;
            public Color OuterBottom;
            public Color OuterTop;
        }

        #endregion
    }
}