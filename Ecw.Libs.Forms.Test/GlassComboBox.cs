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
namespace Ecw.Libs.Forms.Test
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class GlassComboBox : ComboBox
    {
        public GlassComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnPrint(PaintEventArgs e)
        {
            // base.OnPrint(e);
        }

        protected override void OnFormatStringChanged(EventArgs e)
        {
            // base.OnFormatStringChanged(e);
        }

        protected override void OnTextUpdate(EventArgs e)
        {
            base.OnTextUpdate(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }


        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //base.OnDrawItem(e);   

            e.DrawBackground();
            e.DrawFocusRectangle();
            TextRenderer.DrawText(e.Graphics, Items[e.Index].ToString(), e.Font, new Point(e.Bounds.X, e.Bounds.Y + 1), e.ForeColor, TextFormatFlags.NoPrefix);
        }
    }
}