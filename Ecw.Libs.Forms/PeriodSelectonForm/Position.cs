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
    internal struct Position
    {
        private int column;
        private int row;

        public Position(int index)
        {
            this.column = index%7;
            this.row = index/7;
        }

        public Position(int column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Column
        {
            get { return this.column; }
            set { this.column = value; }
        }

        public int Index
        {
            get { return this.row*7 + this.column; }
        }

        public bool IsWeek
        {
            get { return this.column == -1 && this.row >= 0; }
        }

        public bool IsDay
        {
            get { return this.column >= 0; }
        }

        public bool IsValid
        {
            get { return this != Invalide; }
        }

        public bool IsCaption
        {
            get { return this == Caption; }
        }

        public static Position Invalide
        {
            get { return new Position(-1, -1); }
        }

        public static Position Caption
        {
            get { return new Position(-2, -2); }
        }


        public static bool operator !=(Position a, Position b)
        {
            return a.row != b.row || a.column != b.column;
        }

        public static bool operator ==(Position a, Position b)
        {
            return a.row == b.row && a.column == b.column;
        }

        public override int GetHashCode()
        {
            return Index;
        }

        public override bool Equals(object obj)
        {
            return obj is Position && ((Position) obj) == this;
        }
    }
}