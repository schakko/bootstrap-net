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
    using Aero;

    public static class GlassHelper
    {
        /// <summary>
        ///   Gibt an, ob Glass aktiviert ist
        /// </summary>
        public static bool GlassIsEnabled
        {
            get
            {
                if (!CanRenderGlass)
                {
                    return false;
                }

                return WinApi.DwmIsCompositionEnabled();
            }
        }

        /// <summary>
        ///   Gibt an, ob das OS Glass unterstützt
        /// </summary>
        public static bool CanRenderGlass
        {
            get { return (Environment.OSVersion.Version.Major >= 6); }
        }
    }
}