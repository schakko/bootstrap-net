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
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;
    using Aero;

    /// <summary>
    ///   Class that provides an extended winform that supports Vista's glass.
    /// </summary>
    /// <remarks>
    ///   This class provides a winform that supports the new Vista's glass features. Use the 
    ///   <see cref = "GlassMargin">GlassMargin</see> to set the margin for the glass in the form. 
    ///   By setting -1 the whole form is "glassed".
    /// </remarks>
    public class GlassForm : AeroForm
    {
        private bool fullWindow;
        private Color glassColor;

        private Padding glassMargin;
        private Rectangle[] glassRectangles;
        private WindowMode windowMode;

        /// <summary>
        ///   Specifies the interior glass margin of a control. Set -1 to full window glass. Requires Windows Vista or higher.
        /// </summary>
        [Description("Specifies the interior glass margin of a control. Set -1 to full window glass. Requires Windows Vista or higher.")]
        [Category("Layout")]
        public Padding GlassMargin
        {
            get { return this.glassMargin; }
            set
            {
                this.glassMargin = value;
                // Check if we are in full window mode.
                this.fullWindow = (this.glassMargin.Bottom == -1 && this.glassMargin.Top == -1 && this.glassMargin.Left == -1 && this.glassMargin.Right == -1);
                if (this.fullWindow)
                {
                    DecorationMargin = new Padding(0);
                }
                // Refresh the form.
                Invalidate();
            }
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        private void SetWindowMode()
        {
            if (Environment.OSVersion.Version.Major > 5)
            {
                if (WinApi.DwmIsCompositionEnabled())
                {
                    if (DesignMode)
                    {
                        this.glassColor = SystemColors.GradientActiveCaption;
                        this.windowMode = WindowMode.Basic;
                    }
                    else
                    {
                        TransparencyKey = Color.FromArgb(254, 84, 253);
                        this.glassColor = Color.FromArgb(243, 244, 245);
                        //glassColor = Color.Black;

                        var margins = new MARGINS(GlassMargin);
                        var hWnd = Handle;
                        DwmExtendFrameIntoClientArea(hWnd, ref margins);

                        this.windowMode = WindowMode.Aero;
                    }
                }
                else if (VisualStyleInformation.IsEnabledByUser)
                {
                    this.glassColor = SystemColors.GradientActiveCaption;
                    this.windowMode = WindowMode.Basic;
                }
                else
                {
                    this.glassColor = SystemColors.Control;
                    this.windowMode = WindowMode.Classic;
                }
            }
            else
            {
                this.glassColor = SystemColors.Control;
                this.windowMode = WindowMode.Classic;
            }

            Activated -= GlassForm_Activated;
            Deactivate -= GlassForm_Deactivate;

            // Wenn das aktuelle Design das Basis-Design ist,
            // müssen die Ereignisse Activated und Deactivate abgefangen 
            // werden, damit die „Glass-Farbe“ der Ramenfarbe angepasset werden kann
            if (this.windowMode == WindowMode.Basic)
            {
                Activated += GlassForm_Activated;
                Deactivate += GlassForm_Deactivate;
            }
        }

        private void GlassForm_Deactivate(object sender, EventArgs e)
        {
            this.glassColor = SystemColors.GradientInactiveCaption;
            Invalidate();
        }

        private void GlassForm_Activated(object sender, EventArgs e)
        {
            this.glassColor = SystemColors.GradientActiveCaption;
            Invalidate();
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!this.fullWindow)
            {
                PaintInnerRectangle(e.Graphics);
            }
            PaintDecoration(e.Graphics);
            PaintGlass(e.Graphics);
        }


        private void PaintInnerRectangle(Graphics graphics)
        {
            var deco = DecorationMargin;
            var glass = GlassMargin;
            Rectangle rectangle;

            int top = Math.Max(deco.Top, glass.Top);
            int bottom = Math.Max(deco.Bottom, glass.Bottom);
            int left = Math.Max(deco.Left, glass.Left);
            int right = Math.Max(deco.Right, glass.Right);
            int width = ClientRectangle.Width - left - right;
            int height = ClientRectangle.Height - top - bottom;

            rectangle = new Rectangle(left, top, width, height);

            using (var brush = new SolidBrush(BackColor))
            {
                graphics.FillRectangle(brush, rectangle);
            }
        }

        private void PaintGlass(Graphics graphics)
        {
            Brush brush = null;

            try
            {
                this.glassRectangles = new Rectangle[4];

                if (this.windowMode == WindowMode.Aero)
                {
                    // Wenn der TransparencyKey nicht an dieser Stelle auf einen einen anderen Wert
                    // als auf dem beim Laden der Form gesetzt wird, erscheint für einen kurzen Augenblick
                    // ein Balken in der Farbe mit der man das „Glas“ malt.
                    // Alternativ kann der TransparencyKey unangetastet bleiben, dann muss man das „Glas“
                    // mit Color.Black malen. Diese Lösung hat den Nachteil, dass auch schwarze Schrift
                    // transparent wird, wenn nicht UseCompatibleTextRendering auf true gesetzt wurde.
                    TransparencyKey = this.glassColor;
                    //glassColor = TransparencyKey = Color.Black;
                }

                brush = new SolidBrush(this.glassColor);

                if (this.fullWindow)
                {
                    graphics.FillRectangle(brush, ClientRectangle);
                }
                else
                {
                    // Gibt die höhe des/der linken und rechten Rechteckes/Linie an
                    int sideHeight = ClientRectangle.Height - this.glassMargin.Top - this.glassMargin.Bottom;
                    int bottomY = ClientRectangle.Height - this.glassMargin.Bottom;
                    int rightX = ClientRectangle.Width - this.glassMargin.Right;
                    // Top
                    this.glassRectangles[0] = new Rectangle(0, 0, ClientRectangle.Width, this.glassMargin.Top);
                    // Bottom
                    this.glassRectangles[1] = new Rectangle(0, bottomY, ClientRectangle.Width, this.glassMargin.Bottom);
                    // Left
                    this.glassRectangles[2] = new Rectangle(0, this.glassMargin.Top, this.glassMargin.Left, sideHeight);
                    // Right
                    this.glassRectangles[3] = new Rectangle(rightX, this.glassMargin.Top, this.glassMargin.Right, sideHeight);

                    graphics.FillRectangles(brush, this.glassRectangles);
                }
            }
            finally
            {
                if (brush != null)
                {
                    brush.Dispose();
                }
            }
        }

        #region Nested type: MARGINS

        /// <summary>
        ///   Struct that specifies the margin of the glass.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MARGINS
        {
            public MARGINS(Padding t)
            {
                this.Left = t.Left;
                this.Right = t.Right;
                this.Top = t.Top;
                this.Bottom = t.Bottom;
            }

            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        #endregion

        #region Overriden members

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetWindowMode();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InvalidateGlass();
        }


        private void InvalidateGlass()
        {
            var rect = new Rectangle(this.glassMargin.Left, this.glassMargin.Top, ClientRectangle.Width - this.glassMargin.Right, Height - this.glassMargin.Bottom);
            Invalidate(rect, false);
        }


        /// <summary>
        ///   Fängt Systemnachrichten ab.
        /// </summary>
        /// <param name = "msg"></param>
        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);

            if (msg.Msg == WinApi.WmDwmCompositionChanged)
            {
                SetWindowMode();
                Refresh();
            }

            // Return if glass is not enabled.
            if (this.windowMode == WindowMode.Classic)
            {
                return;
            }

            if (msg.Msg == WinApi.WmNchittest && msg.Result.ToInt32() == WinApi.HtClient)
            {
                // Get the screen coordinates.
                int lParam = msg.LParam.ToInt32();
                int x = (lParam << 16) >> 16;
                int y = lParam >> 16;
                var p = PointToClient(new Point(x, y));

                // Check out if we are on glass.
                if (this.fullWindow || this.glassRectangles[0].Contains(p) || this.glassRectangles[1].Contains(p) || this.glassRectangles[2].Contains(p) || this.glassRectangles[3].Contains(p))
                {
                    // Signalize that the caption was clicked.
                    msg.Result = new IntPtr(WinApi.HtCaption);
                }
            }
        }

        #endregion

        #region Nested type: WindowMode

        private enum WindowMode
        {
            Classic = 0,
            Basic = 1,
            Aero = 2
        }

        #endregion
    }
}