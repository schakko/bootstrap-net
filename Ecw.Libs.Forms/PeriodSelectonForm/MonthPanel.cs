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
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Windows.Forms;
    using Utils;

    internal class MonthPanel : Panel
    {
        private const int width = 192;
        private const int height = 123;
        private const int clientY = height - 15*6;
        private const int clientX = width - 24*7;
        private const int textOffsetX = 21;
        private const int textOffsetY = 0;
        private const float rounding = 4.0f;
        private const float dayFontSize = 8.75f;

        private static readonly string[] dayNames;
        private static readonly FontFamily dayFontFamily = SystemFonts.DialogFont.FontFamily;
        private static readonly Font dayFont = new Font(dayFontFamily, dayFontSize);
        private static readonly Font captionFont = new Font(SystemFonts.DialogFont.FontFamily, 10, FontStyle.Regular);

        private static readonly Color markBorderColor = Color.FromArgb(120, 164, 218);
        private static readonly Color markTextColor = Color.Black;
        private static readonly Color markBackColor1 = Color.FromArgb(241, 247, 254);
        private static readonly Color markBackColor2 = Color.FromArgb(207, 228, 254);


        private static readonly Color cursorTextColor = Color.FromArgb(0, 102, 204);
        private static readonly Color cursorBorderColor = Color.FromArgb(185, 215, 252);
        private static readonly Color cursorBackColor1 = Color.FromArgb(253, 254, 255);
        private static readonly Color cursorBackColor2 = Color.FromArgb(243, 248, 255);

        private static readonly Color captionColor = Color.FromArgb(0, 51, 153);
        private static readonly Color weekColor = Color.Gray;

        private static readonly Pen cursorBorderPen = new Pen(cursorBorderColor);
        private static readonly Pen markBorderPen = new Pen(markBorderColor);

        private static readonly Brush cursorTextBrush = new SolidBrush(cursorTextColor);
        private static readonly Brush blackBrush = new SolidBrush(Color.Black);
        private static readonly Brush weekBrush = new SolidBrush(Color.Gray);
        private static readonly Brush cursorBackgroundBrush;
        private static readonly GraphicsPath cursorGraphicPath;

        private static readonly StringFormat dayStringFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
        private static readonly StringFormat center = new StringFormat {Alignment = StringAlignment.Center};

        private static readonly Bitmap classBackground = new Bitmap(width, height);

        private static readonly GraphicPathHelper graphicPathHelper = new GraphicPathHelper(clientX, clientY, rounding);
        private readonly int[] days = new int[42];

        private readonly int month;
        private readonly string monthName;
        private readonly int[] weeks = new int[6];
        private Position currentPosition = Position.Invalide;

        private int firstIndex;
        private int firstMarkedIndex = -1;
        private int lastIndex;
        private int lastMarkedIndex = -1;
        private Position lastPosition = Position.Invalide;
        private Bitmap objectBackground;
        private int year;


        static MonthPanel()
        {
            dayNames = new string[7];
            int firstDay = (int) CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 1;
            var dummyDate = new DateTime(2010, 8, firstDay);
            for (int i = 0; i < 7; i++)
            {
                dayNames[i] = dummyDate.ToString("ddd").Substring(0, 2);
                dummyDate = dummyDate.AddDays(1);
            }

            // Zeichnet auf eine weißes Bitmap die Elemente, die für alle Instanzen gleich sind 
            // und speichert das Ergebniss in der Variablen classBackground.
            classBackground = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(classBackground))
            {
                g.FillRectangle(new SolidBrush(SystemColors.Window), new Rectangle(0, 0, width, height));
                g.DrawLine(new Pen(Color.FromArgb(245, 245, 245)), clientX, clientY - 1, width - 1, clientY - 1);
                AddDayNames(g);
            }
        }

        public MonthPanel(int year, int month)
        {
            this.month = month;
            this.monthName = new DateTime(2000, month, 1).ToString("MMMM");
            Margin = new Padding(6);
            Width = width;
            Height = height;

            SetYear(year);
        }

        public void SetYear(int year)
        {
            this.year = year;

            var date = new DateTime(year, this.month, 1);


            // Das Feld für die Tage füllen
            int firstDayOfWeek = (int) date.DayOfWeek;

            // US auf das Deutsche-Format umstellen
            if (CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday)
            {
                if (firstDayOfWeek == 0)
                {
                    firstDayOfWeek = 6;
                }
                else
                {
                    firstDayOfWeek--;
                }
            }

            int lastDay = DateTime.DaysInMonth(year, this.month);

            int i = 0;
            int day = 1;
            while (i < firstDayOfWeek)
            {
                this.days[i++] = 0;
            }
            this.firstIndex = i;
            while (day <= lastDay)
            {
                this.days[i++] = day++;
            }
            this.lastIndex = i - 1;
            while (i < 42)
            {
                this.days[i++] = 0;
            }

            // Das Feld für die Kalenderwochen füllen
            //int week = DateUtils.GetGermanCalendarWeek(date).Week;
            var culture = CultureInfo.CurrentCulture;
            var weekRule = culture.DateTimeFormat.CalendarWeekRule;
            var firstDayRule = culture.DateTimeFormat.FirstDayOfWeek;
            int week = culture.Calendar.GetWeekOfYear(date, weekRule, firstDayRule);
            int lastWeek = culture.Calendar.GetWeekOfYear(new DateTime(year, this.month, lastDay), weekRule, firstDayRule);

            if (this.month == 1 && week > 1)
            {
                this.weeks[0] = week;
                week = 1;
                i = 1;
            }
            else
            {
                i = 0;
            }

            while (week <= lastWeek)
            {
                this.weeks[i++] = week++;
            }
            while (i < 6)
            {
                this.weeks[i++] = 0;
            }

            SetObjectBackground();
            SetBackground();
        }


        /// <summary>
        ///   Zeichnet auf die classBackground-Bitmap die Elemente, die sich bei den Monaten unterscheiden,
        ///   aber nicht neugezeichnet werden müssen, wenn sich die Auswahl geändernt hat. Das Ergebniss
        ///   wird in der objectBackground-Varialben gespeichert.
        /// </summary>
        private void SetObjectBackground()
        {
            this.objectBackground = new Bitmap(classBackground);
            using (var g = Graphics.FromImage(this.objectBackground))
            {
                AddWeeks(g);
                AddMonthName(g);
            }
        }

        /// <summary>
        ///   Zeichnet auf die objectBackground-Bitmap die Elemente, die neu gezeichnet werden müssen,
        ///   wenn sich die Auswahl ändert. Das Ergebnis wird als BackgroundImage des Panels gesetzt.
        /// </summary>
        private void SetBackground()
        {
            var back = new Bitmap(this.objectBackground);

            using (var g = Graphics.FromImage(back))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                if (this.firstMarkedIndex > -1 && this.lastMarkedIndex > -1)
                {
                    var path = graphicPathHelper.GetMark(this.firstMarkedIndex, this.lastMarkedIndex);
                    var bounds = new Rectangle(clientX, clientY, width - clientX, width - clientY);
                    var brush = new LinearGradientBrush(bounds, markBackColor1, markBackColor2, LinearGradientMode.Vertical);

                    g.FillPath(brush, path);
                    g.DrawPath(new Pen(markBorderColor), path);
                }
                AddDays(g);
            }

            BackgroundImage = back;
        }


        /// <summary>
        ///   Fügt der übergeben Grafik die Tage hinzu
        /// </summary>
        /// <param name = "g"></param>
        private void AddDays(Graphics g)
        {
            int y = clientY + textOffsetY;
            for (int i = 0; i < 6; i++)
            {
                int x = clientX + textOffsetX;
                for (int j = 0; j < 7; j++)
                {
                    int day = this.days[i*7 + j];
                    if (day > 0)
                    {
                        g.DrawString(day.ToString(), dayFont, blackBrush, x, y, dayStringFormat);
                    }
                    x += 24;
                }
                y += 15;
            }
        }

        /// <summary>
        ///   Fügt der übergebenen Grafik die Kurznamen der Wochentage hizu
        /// </summary>
        /// <param name = "g"></param>
        private static void AddDayNames(Graphics g)
        {
            int x = clientX + textOffsetX - 9;
            int y = clientY + textOffsetY - 15;
            foreach (string s in dayNames)
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawString(s, dayFont, blackBrush, x, y, center);
                x += 24;
            }
        }


        /// <summary>
        ///   Fügt der übergebenen Grafik Kalendarwochen hinzu
        /// </summary>
        /// <param name = "g"></param>
        private void AddWeeks(Graphics g)
        {
            int y = clientY + textOffsetY;
            for (int i = 0; i < 6; i++)
            {
                int week = this.weeks[i];
                if (week == 0)
                {
                    break;
                }
                g.DrawString(week.ToString(), dayFont, weekBrush, 21, y, dayStringFormat);
                y += 15;
            }
        }

        /// <summary>
        ///   Fügt der übergeben Grafik den Monatsnamen hinzu
        /// </summary>
        /// <param name = "g"></param>
        private void AddMonthName(Graphics g)
        {
            g.DrawString(this.monthName, captionFont, new SolidBrush(captionColor), Width/2, 0, center);
        }


        private void DrawCursor()
        {
            if (this.currentPosition.IsValid)
            {
                using (var g = CreateGraphics())
                {
                    if (this.currentPosition.IsCaption)
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        DrawMonthNameCursor(g);
                        g.DrawString(this.monthName, captionFont, cursorTextBrush, new Rectangle(0, 0, Width, 16), center);
                    }

                    else
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        DrawDayAndWeekCursor(g, this.currentPosition);
                    }
                }
            }
        }

        /// <summary>
        ///   Zeichnet einen Rahmen mit Hintergrund um eine Tag- oder Wochenzelle
        /// </summary>
        /// <param name = "g"></param>
        /// <param name = "pos"></param>
        private void DrawDayAndWeekCursor(Graphics g, Position pos)
        {
            int x = pos.Column*24 + clientX;
            int y = pos.Row*15 + clientY;
            var bounds = new Rectangle(x, y, 23, 14);
            var path = graphicPathHelper.GetRoundedRect(bounds);
            var brush = new LinearGradientBrush(bounds, cursorBackColor1, cursorBackColor2, LinearGradientMode.Vertical);
            g.FillPath(brush, path);
            g.DrawPath(cursorBorderPen, path);
            string s = (pos.IsWeek ? this.weeks[pos.Row] : this.days[pos.Index]).ToString();

            g.DrawString(s, dayFont, cursorTextBrush, x + textOffsetX, y + textOffsetY, dayStringFormat);
        }

        /// <summary>
        ///   Zeichnet eine Rahmen mit Hintergrund um den Monatsnamen
        /// </summary>
        /// <param name = "g"></param>
        private void DrawMonthNameCursor(Graphics g)
        {
            var bounds = new Rectangle(0, 0, Width - 1, 16);
            var path = graphicPathHelper.GetRoundedRect(bounds);
            var brush = new LinearGradientBrush(bounds, cursorBackColor1, cursorBackColor2, LinearGradientMode.Vertical);
            g.FillPath(brush, path);
            g.DrawPath(cursorBorderPen, path);
        }

        /// <summary>
        ///   Markiert alle Tage dieser Instanz die in dem übergeben Zeitrahmen liegen.
        /// </summary>
        /// <param name = "begin"></param>
        /// <param name = "end"></param>
        internal void MarkDays(DateTime begin, DateTime end)
        {
            var thisBegin = new DateTime(this.year, this.month, this.days[this.firstIndex]);
            var thisEnd = new DateTime(this.year, this.month, this.days[this.lastIndex]);
            var markBegin = default(DateTime);
            var markEnd = default(DateTime);


            if (begin <= thisBegin && thisBegin <= end)
            {
                markBegin = thisBegin;
            }
            if (begin <= thisEnd && thisEnd <= end)
            {
                markEnd = thisEnd;
            }

            if (thisBegin <= begin && begin <= thisEnd)
            {
                markBegin = begin;
            }
            if (thisBegin <= end && end <= thisEnd)
            {
                markEnd = end;
            }


            if (markBegin != default(DateTime) && markEnd != default(DateTime))
            {
                this.firstMarkedIndex = markBegin.Day - this.days[this.firstIndex] + this.firstIndex;
                this.lastMarkedIndex = markEnd.Day - this.days[this.firstIndex] + this.firstIndex;
            }
            else
            {
                this.firstMarkedIndex = -1;
                this.lastMarkedIndex = -1;
            }
            SetBackground();
        }


        private void InvalidatePosition(Position pos)
        {
            Rectangle rect;
            if (pos.IsCaption)
            {
                rect = new Rectangle(0, 0, Width, 17);
            }
            else
            {
                rect = new Rectangle(pos.Column*24 + clientX, pos.Row*15 + clientY, 24, 15);
            }
            Invalidate(rect);
        }


        private Position GetPosition(Point point)
        {
            // Der Mauszeiger ist oberhalb der Tage und Kalendarwochen 
            if (point.Y < clientY)
            {
                if (point.Y < 20)
                {
                    return Position.Caption;
                }
                else
                {
                    return Position.Invalide;
                }
            }
            // Der Mauszeiger ist auf den Kalendarwochen
            if (point.X < 24)
            {
                int row = (point.Y - clientY)/15;
                if (this.weeks[row] == 0)
                {
                    return Position.Invalide;
                }
                else
                {
                    return new Position(-1, row);
                }
            }

            // Der Mauszeiger ist auf den Tagen
            var pos = new Position((point.X - clientX)/24, (point.Y - clientY)/15);
            int index = pos.Index;
            if (this.firstIndex <= index && index <= this.lastIndex)
            {
                return pos;
            }
            return Position.Invalide;
        }

        #region Ereignisse

        public event PeriodSelectedEventHandler PeriodSelected;


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.lastPosition = this.currentPosition;
            this.currentPosition = GetPosition(e.Location);

            if (this.currentPosition != this.lastPosition)
            {
                InvalidatePosition(this.lastPosition);
                DrawCursor();
            }
        }

        /// <summary>
        ///   Wird ausgelöst wenn der Mauscursor das Panel verlässt.
        ///   Wenn die letzte Postion des Cursorsgültig war,
        ///   wird diese neu gezeichnet.
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.currentPosition.IsValid)
            {
                InvalidatePosition(this.currentPosition);
            }
            this.lastPosition = this.currentPosition = Position.Invalide;
        }

        /// <summary>
        ///   Wird ausgelöst, wenn jemand mit der Maus auf das Panel klickt.
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (PeriodSelected != null)
            {
                var pos = GetPosition(e.Location);
                if (pos.IsDay)
                {
                    PeriodSelected(this, new PeriodSelectedEventArgs(new DateTime(this.year, this.month, this.days[pos.Index])));
                }
                else if (pos.IsWeek)
                {
                    int week = this.weeks[pos.Row];
                    int year = week > 50 && this.month == 1 ? this.year - 1 : this.year;
                    var begin = DateHelper.FirstDateOfWeek(year, week);
                    var end = begin.AddDays(6);
                    PeriodSelected(this, new PeriodSelectedEventArgs(begin, end));
                }
                else if (pos.IsCaption)
                {
                    var begin = new DateTime(this.year, this.month, 1);
                    var end = begin.AddMonths(1).AddDays(-1);
                    PeriodSelected(this, new PeriodSelectedEventArgs(begin, end));
                }
            }
            Refresh();
            DrawCursor();
        }

        #endregion
    }
}