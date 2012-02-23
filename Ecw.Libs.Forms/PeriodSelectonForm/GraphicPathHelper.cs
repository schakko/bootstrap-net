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
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class GraphicPathHelper
    {
        private readonly int clientX;
        private readonly int clientY;
        private readonly float rounding;

        internal GraphicPathHelper(int offsetX, int offsetY, float rounding)
        {
            this.clientX = offsetX;
            this.clientY = offsetY;
            this.rounding = rounding;
        }

        private void AddTopLeftCorner(GraphicsPath path, int column, int row)
        {
            path.AddArc(column*24 + this.clientX, row*15 + this.clientY, this.rounding, this.rounding, 180, 90);
        }

        private void AddTopRightCorner(GraphicsPath path, int column, int row)
        {
            path.AddArc(column*24 + this.clientX + 23 - this.rounding, row*15 + this.clientY, this.rounding, this.rounding, 270, 90);
        }

        private void AddBottomRightCorner(GraphicsPath path, int column, int row)
        {
            path.AddArc(column*24 + this.clientX + 23 - this.rounding, row*15 + this.clientY + 14 - this.rounding, this.rounding, this.rounding, 0, 90);
        }

        private void AddBottomLeftCorner(GraphicsPath path, int column, int row)
        {
            path.AddArc(column*24 + this.clientX, row*15 + this.clientY + 14 - this.rounding, this.rounding, this.rounding, 90, 90);
        }

        private void AddInnerTopLeftCorner(GraphicsPath path, int column, int row)
        {
            path.AddArc(column*24 + this.clientX + 23 - this.rounding - 1, row*15 + this.clientY + 14 - this.rounding - 1, this.rounding + 2, this.rounding + 2, 90, -90);
        }

        private void AddInnerBottomRightCorner(GraphicsPath path, int column, int row)
        {
            path.AddArc(column*24 + this.clientX - 1, row*15 + this.clientY - 1, this.rounding + 2, this.rounding + 2, -90, -90);
        }

        internal GraphicsPath GetMark(int firstIndex, int lastIndex)
        {
            var path = new GraphicsPath();

            int top = firstIndex/7;
            int left = firstIndex%7;
            int bottom = lastIndex/7;
            int right = lastIndex%7;

            bool multiLine = bottom > top;
            bool splitted = left > right && bottom - top == 1;

            // Top-Left
            AddTopLeftCorner(path, left, top);

            // Top-Right
            AddTopRightCorner(path, multiLine ? 6 : right, top);

            // Bottom-Right   
            if (multiLine && right != 6)
            {
                AddBottomRightCorner(path, 6, bottom - 1);

                if (splitted)
                {
                    AddBottomLeftCorner(path, left, bottom - 1);
                    path.CloseFigure();
                }
                else
                {
                    AddInnerBottomRightCorner(path, right + 1, bottom);
                }
            }
            AddBottomRightCorner(path, right, bottom);

            // Bottom-Left     
            AddBottomLeftCorner(path, multiLine ? 0 : left, bottom);

            if (multiLine && left != 0)
            {
                AddTopLeftCorner(path, 0, top + 1);

                if (splitted)
                {
                    AddTopRightCorner(path, right, top + 1);
                }
                else
                {
                    AddInnerTopLeftCorner(path, left - 1, top);
                }
            }

            path.CloseFigure();

            return path;
        }

        internal GraphicsPath GetRoundedRect(RectangleF rect)
        {
            var path = new GraphicsPath();

            var arc = new RectangleF(rect.X, rect.Y, this.rounding, this.rounding);
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - this.rounding;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - this.rounding;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}