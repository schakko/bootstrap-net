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
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public enum ValidationType
    {
        None,
        RegEx,
        Mask,
    }

    public enum DateTimeFormat
    {
        Custom,
        Time,
        BigTime,
        Date,
        DateTime,
        DateBigTime,
    }

    public enum TimeSpanFormat
    {
        Custom,
        Time,
        BigTime,
        DaysTime,
        DaysBigTime,
    }

    public enum TextFormat
    {
        Custom,
        Telefon,
        Email,
    }

    public class GenericBox<T> : MaskedTextBox
    {
        private char decimalSeperator;
        private string format;
        private bool hasDecimalSperator;
        private bool hasMinus;
        private string mask;
        private ParseFromValue parseFromValue;
        private ParseValue parseValue;
        private string previousContent;
        private int previousCursorIndex;
        private ValidationType validationType;

        #region Constructors

        public GenericBox()
            : this(null) {}

        public GenericBox(string mask)
            : this(mask, ValidationType.Mask) {}

        public GenericBox(string validation, ValidationType maskType)
            : this(validation, maskType, null) {}

        public GenericBox(string validation, ValidationType maskType, string format)
        {
            if (maskType == ValidationType.Mask)
            {
                Mask = validation;
            }
            else if (maskType == ValidationType.RegEx)
            {
                this.mask = validation;
            }
            Format = format;

            Initialize();
        }

        /// <summary>
        ///   Erzeugt eine neue Instanz der GenericBox-Klasse.
        ///   Dieser Konstruktor kann nur verwendet werden, wenn T vom Typ DateTime oder DateTime? ist.
        /// </summary>
        /// <param name = "format">Legt die Eingabemaske und Eingabeformat fest.</param>
        public GenericBox(DateTimeFormat format)
        {
            if (!(typeof (T) == typeof (DateTime) || typeof (T) == typeof (DateTime?)))
            {
                throw new Exception("This Constructor can only be used, when T is DateTime or Nullalbe<DateTime>.");
            }
            SetMaskAndFormat(format);
            Initialize();
        }

        /// <summary>
        ///   Erzeugt eine neu Instanz der GenericBox-Klasse.
        ///   Dieser Kostruktor kann nur verwendet werden, wenn T vom Typ TimeSpan oder TimeSpan? ist.
        /// </summary>
        /// <param name = "format">Legt die Eingabemaske und Eingabeformat fest.</param>
        public GenericBox(TimeSpanFormat format)
        {
            if (!(typeof (T) == typeof (TimeSpan) || typeof (T) == typeof (TimeSpan?)))
            {
                throw new Exception("This Constructor can only be used, when T is TimeSpan or Nullalbe<TimeSpan>.");
            }
            SetMaskAndFormat(format);
            Initialize();
        }

        /// <summary>
        ///   Erzeugt eine neu Instanz der GenericBox-Klasse.
        ///   Dieser Kostruktor kann nur verwendet werden, wenn T vom Typ String ist.
        /// </summary>
        /// <param name = "format">Legt die Eingabemaske und Eingabeformat fest.</param>
        public GenericBox(TextFormat format)
        {
            if (!(typeof (T) == typeof (string)))
            {
                throw new Exception("This Constructor can only be used, when T is String or Nullalbe<String>.");
            }
            SetMaskAndFormat(format);
            Initialize();
        }

        #endregion

        //[Browsable(false)]
        //public new string Text
        //{
        //    get { return  base.Text; }
        //    set { base.Text = value; }
        //}

        [Browsable(false)]
        public new string Text
        {
            get { return base.Text == null ? null : base.Text.Substring(BeginOffset, base.Text.Length - BeginOffset - EndOffset); }
            set
            {
                string beginn = base.Text.Substring(0, BeginOffset);
                string end = base.Text.Substring(base.Text.Length - EndOffset);
                base.Text = beginn + value + end;
            }
        }

        [Browsable(true)]
        public ushort BeginOffset { get; set; }

        [Browsable(true)]
        public ushort EndOffset { get; set; }

        //[Browsable(true)]
        //public string TextBeforeValue
        //{
        //    get { return textBeforeValue; }
        //    set
        //    {
        //        textBeforeValue = value;
        //        charsBeforeValue = value == null ? 0 : value.Length;
        //        Mask = mask;
        //    }
        //}

        //[Browsable(true)]
        //public string TextAfterValue
        //{
        //    get { return textAfterValue; }
        //    set
        //    {
        //        textAfterValue = value;
        //        charsAfterValue = value == null ? 0 : value.Length;
        //        Mask = mask;
        //    }
        //}


        public new string Mask
        {
            get { return this.mask; }
            set
            {
                this.mask = value;
                if (this.validationType == ValidationType.Mask)
                {
                    base.Mask = value;
                }
            }
        }

        //public new string Mask
        //{
        //    get { return mask; }
        //    set
        //    {
        //        mask = value;
        //        if (validationType == ValidationType.Mask)
        //        {
        //            base.Mask = value;
        //        }
        //    }
        //}

        [DefaultValue(typeof (ValidationType), "None")]
        public virtual ValidationType Validation
        {
            get { return this.validationType; }
            set
            {
                this.validationType = value;
                if (value == ValidationType.Mask)
                {
                    base.Mask = this.mask;
                }
                else
                {
                    base.Mask = null;
                }
                if (value == ValidationType.RegEx)
                {
                    KeyPress += OnKeyPressRegularExpression;
                    TextChanged += OnTextChangedRegularExpression;
                }
                else
                {
                    KeyPress -= OnKeyPressRegularExpression;
                    TextChanged -= OnTextChangedRegularExpression;
                }
            }
        }

        public virtual string Format
        {
            get { return this.format; }
            set { this.format = value; }
        }

        public T Value
        {
            get { return this.parseValue(Text); }
            set { Text = this.parseFromValue(value); }
        }

        private void Initialize()
        {
            var t = typeof (T);

            if (t == typeof (string))
            {
                this.parseValue = ParseString;
                this.parseFromValue = ParseFromString;
            }
            else if (t == typeof (char?) || t == typeof (char))
            {
                this.parseValue = ParseChar;
                this.parseFromValue = ParseFromChar;
            }
            else if (t == typeof (ushort?) || t == typeof (ushort))
            {
                this.parseValue = ParseUshort;
                this.parseFromValue = ParseFromUshort;
                KeyPress += OnKeyPressNaturalNumber;
            }
            else if (t == typeof (uint?) || t == typeof (uint))
            {
                this.parseValue = ParseUint;
                this.parseFromValue = ParseFromUint;
                KeyPress += OnKeyPressNaturalNumber;
            }
            else if (t == typeof (ulong?) || t == typeof (ulong))
            {
                this.parseValue = ParseUlong;
                this.parseFromValue = ParseFromUlong;
                KeyPress += OnKeyPressNaturalNumber;
            }
            else if (t == typeof (short?) || t == typeof (short))
            {
                this.parseValue = ParseUshort;
                this.parseFromValue = ParseFromUshort;
                KeyPress += OnKeyPressInteger;
                TextChanged += ObserveIntegerInput;
            }
            else if (t == typeof (int?) || t == typeof (int))
            {
                this.parseValue = ParseInt;
                this.parseFromValue = ParseFromInt;
                KeyPress += OnKeyPressInteger;
                TextChanged += ObserveIntegerInput;
            }
            else if (t == typeof (long?) || t == typeof (long))
            {
                this.parseValue = ParseLong;
                this.parseFromValue = ParseFromLong;
                KeyPress += OnKeyPressInteger;
                TextChanged += ObserveIntegerInput;
            }
            else if (t == typeof (float?) || t == typeof (float))
            {
                this.parseValue = ParseFloat;
                this.parseFromValue = ParseFromFloat;
                KeyPress += OnKeyPressRealNumber;
                TextChanged += ObserveRealNumberInput;
            }
            else if (t == typeof (double?) || t == typeof (double))
            {
                this.parseValue = ParseDouble;
                this.parseFromValue = ParseFromDouble;
                KeyPress += OnKeyPressRealNumber;
                TextChanged += ObserveRealNumberInput;
            }
            else if (t == typeof (decimal?) || t == typeof (decimal))
            {
                this.parseValue = ParseDecimal;
                this.parseFromValue = ParseFromDecimal;
                KeyPress += OnKeyPressRealNumber;
            }
            else if (t == typeof (DateTime?) || t == typeof (DateTime))
            {
                this.parseValue = ParseDateTime;
                this.parseFromValue = ParseFromDateTime;
            }
            else if (t == typeof (TimeSpan?) || t == typeof (TimeSpan))
            {
                this.parseValue = ParseTimeSpan;
                this.parseFromValue = ParseFromTimeSpan;
            }
            else
            {
                throw new Exception("Type not supportet");
            }

            this.decimalSeperator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToCharArray()[0];

            Value = default(T);
        }

        public void SetMaskAndFormat(TimeSpanFormat timeSpanFormat)
        {
            Validation = ValidationType.Mask;

            switch (timeSpanFormat)
            {
                case TimeSpanFormat.Time:
                    this.format = "";
                    this.mask = "00:00";
                    break;
                case TimeSpanFormat.BigTime:
                    this.mask = "00:00:00";
                    break;
                case TimeSpanFormat.DaysTime:
                    this.mask = "00.00:00";
                    break;
                case TimeSpanFormat.DaysBigTime:
                    this.mask = "00.00:00";
                    break;
            }
        }

        public void SetMaskAndFormat(DateTimeFormat dateTimeFormat)
        {
            Validation = ValidationType.Mask;
            switch (dateTimeFormat)
            {
                case DateTimeFormat.Time:
                    this.mask = "00:00";
                    this.format = "t";
                    break;
                case DateTimeFormat.BigTime:
                    this.mask = "00:00:00";
                    this.format = "T";
                    break;
                case DateTimeFormat.Date:
                    this.mask = "00/00/0000";
                    this.format = "d";
                    break;
                case DateTimeFormat.DateTime:
                    this.mask = "00/00/0000 00:00";
                    this.format = "g";
                    break;
                case DateTimeFormat.DateBigTime:
                    this.mask = "00/00/0000 00:00:00";
                    this.format = "G";
                    break;
            }
        }

        public void SetMaskAndFormat(TextFormat stringFormat)
        {
            Validation = ValidationType.RegEx;

            switch (stringFormat)
            {
                case TextFormat.Telefon:
                    this.format = null;
                    this.mask = @"^\+\d{1,2}(?:\s|)\d{1,5}(?:\s*)(\d+)((?:\s|-)([0-9a-zA-Z]+))$";
                    break;
                case TextFormat.Email:
                    this.format = null;
                    this.mask = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.(?:[A-Z]{2}|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum)$";
                    break;
            }
        }

        #region ParseValue

        private T ParseString(string value)
        {
            return (T) (object) value;
        }

        private T ParseChar(string value)
        {
            T result;
            try
            {
                result = (T) (object) char.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseUshort(string value)
        {
            try
            {
                return (T) (object) ushort.Parse(Text);
            }
            catch
            {
                return default(T);
            }
        }

        private T ParseUint(string value)
        {
            T result;
            try
            {
                result = (T) (object) uint.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseUlong(string value)
        {
            T result;
            try
            {
                result = (T) (object) ulong.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseShort(string value)
        {
            T result;
            try
            {
                result = (T) (object) short.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseInt(string value)
        {
            T result;
            try
            {
                result = (T) (object) int.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseLong(string value)
        {
            T result;
            try
            {
                result = (T) (object) long.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }


        private T ParseFloat(string value)
        {
            T result;
            try
            {
                result = (T) (object) float.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseDouble(string value)
        {
            T result;
            try
            {
                result = (T) (object) double.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseDecimal(string value)
        {
            T result;
            try
            {
                result = (T) (object) decimal.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseDateTime(string value)
        {
            T result;
            try
            {
                result = (T) (object) DateTime.Parse(Text, null, DateTimeStyles.NoCurrentDateDefault);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        private T ParseTimeSpan(string value)
        {
            T result;
            try
            {
                result = (T) (object) TimeSpan.Parse(Text);
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        #endregion

        #region ParseFromValue

        private string ParseFromString(T value)
        {
            return value == null ? null : value.ToString();
        }

        private string ParseFromChar(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromUshort(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromUint(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromUlong(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromShort(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromInt(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromLong(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromFloat(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromDouble(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromDecimal(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromDateTime(T value)
        {
            string result;
            try
            {
                result = ((DateTime) (object) value).ToString(Format);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        private string ParseFromTimeSpan(T value)
        {
            string result;
            try
            {
                result = value.ToString();
            }
            catch
            {
                result = null;
            }

            return result;
        }

        #endregion

        #region EventHandles

        protected override void OnValidated(EventArgs e)
        {
            Value = Value;
        }

        private void OnKeyPressNaturalNumber(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void OnKeyPressInteger(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar)
                          || char.IsControl(e.KeyChar)
                          || (e.KeyChar == '-' && !this.hasMinus && SelectionStart == 0));
        }

        private void OnKeyPressRealNumber(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar)
                          || char.IsControl(e.KeyChar)
                          || (e.KeyChar == '-' && !this.hasMinus && SelectionStart == 0)
                          || (e.KeyChar == this.decimalSeperator && !this.hasDecimalSperator));
        }

        private void ObserveRealNumberInput(object sender, EventArgs e)
        {
            this.hasDecimalSperator = Text.Contains(this.decimalSeperator);
            this.hasMinus = Text.Contains('-');
        }

        private void ObserveIntegerInput(object sender, EventArgs e)
        {
            this.hasMinus = Text.Contains('-');
        }


        private void OnKeyPressRegularExpression(object sender, KeyPressEventArgs e)
        {
            this.previousCursorIndex = SelectionStart;
        }

        private void OnTextChangedRegularExpression(object sender, EventArgs e)
        {
            if (Text.Length > 0 && this.mask != null)
            {
                var objNotNaturalPattern = new Regex(this.mask);
                var m = objNotNaturalPattern.Match(Text);
                if (m.Success)
                {
                    this.previousContent = Text;
                }
                else
                {
                    Text = this.previousContent;
                    SelectionStart = this.previousCursorIndex;
                }
            }
            else
            {
                this.previousContent = "";
            }
        }

        #endregion

        #region Nested type: ParseFromValue

        private delegate string ParseFromValue(T value);

        #endregion

        #region Nested type: ParseValue

        private delegate T ParseValue(string value);

        #endregion
    }
}