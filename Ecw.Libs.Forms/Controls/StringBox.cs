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
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DefaultProperty("MaskAndFormat")]
    [ToolboxBitmap(typeof (TextBox))]
    public class StringBox : GenericBox<string>
    {
        private TextFormat maskAndFormat = TextFormat.Custom;

        public StringBox() {}


        public StringBox(TextFormat maskAndFormat)
        {
            MaskAndFormat = maskAndFormat;
        }

        [Browsable(true)]
        public TextFormat MaskAndFormat
        {
            get { return this.maskAndFormat; }
            set
            {
                //maskAndFormat = value;
                SetMaskAndFormat(value);
            }
        }

        public new string Mask
        {
            get { return base.Mask; }
            set
            {
                base.Mask = value;
                MaskAndFormat = TextFormat.Custom;
            }
        }

        public override string Format
        {
            get { return base.Format; }
            set
            {
                base.Format = value;
                MaskAndFormat = TextFormat.Custom;
            }
        }

        public override ValidationType Validation
        {
            get { return base.Validation; }
            set
            {
                base.Validation = value;
                if (value != ValidationType.RegEx)
                {
                    MaskAndFormat = TextFormat.Custom;
                }
            }
        }
    }
}