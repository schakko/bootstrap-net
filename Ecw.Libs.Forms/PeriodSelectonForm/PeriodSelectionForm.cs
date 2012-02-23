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

    public partial class PeriodSelectionForm : GlassForm
    {
        private readonly PeriodSelectedEventArgs[] beginEnd = new PeriodSelectedEventArgs[2];
        private readonly MonthPanel[] months = new MonthPanel[12];
        private int counter;
        private bool dateSelectedBefore;
        private int year;

        public PeriodSelectionForm(int year)
        {
            InitializeComponent();
            this.year = year;
            this.yearButton.Text = year.ToString();
            for (int i = 0; i < 12; i++)
            {
                this.months[i] = new MonthPanel(year, i + 1);
                this.months[i].PeriodSelected += PeriodSelected;
            }
            this.flowLayoutPanel1.Controls.AddRange(this.months);
        }

        public int Year
        {
            get { return this.year; }
            set
            {
                this.yearButton.Text = value.ToString();
                this.year = value;
                //yearOnlyCalendar1.Year = value;
                for (int i = 0; i < 12; i++)
                {
                    this.months[i].SetYear(value);
                }

                if (Begin.HasValue)
                {
                    MarkDays(Begin.Value, End.Value);
                }
            }
        }

        public DateTime? Begin
        {
            get
            {
                if (this.beginEnd[0] == null || this.beginEnd[1] == null)
                {
                    return null;
                }
                else
                {
                    return this.beginEnd[0].Begin < this.beginEnd[1].Begin ? this.beginEnd[0].Begin : this.beginEnd[1].Begin;
                }
            }
        }

        public DateTime? End
        {
            get
            {
                if (this.beginEnd[0] == null || this.beginEnd[1] == null)
                {
                    return null;
                }
                else
                {
                    return this.beginEnd[0].End < this.beginEnd[1].End ? this.beginEnd[1].End : this.beginEnd[0].End;
                }
            }
        }

        private void PeriodSelected(object sender, PeriodSelectedEventArgs e)
        {
            if (this.dateSelectedBefore)
            {
                this.beginEnd[this.counter++%2] = e;
            }
            else
            {
                this.beginEnd[0] = e;
                this.beginEnd[1] = e;
                this.dateSelectedBefore = true;
            }
            MarkDays(Begin.Value, End.Value);
        }


        public void MarkDays(DateTime begin, DateTime end)
        {
            foreach (var month in this.months)
            {
                month.MarkDays(begin, end);
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            Year++;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Year--;
        }

        private string ToRoman(int i)
        {
            switch (i)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "VI";
                default:
                    return "V";
            }
        }

        private void yearButton_Click(object sender, EventArgs e)
        {
            PeriodSelected(this, new PeriodSelectedEventArgs(new DateTime(this.year, 1, 1), new DateTime(this.year, 12, 31)));
        }
    }
}