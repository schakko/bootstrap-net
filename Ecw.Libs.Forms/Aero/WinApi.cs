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
namespace Ecw.Libs.Forms.Aero
{
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Static utility class that holds the native methods for the glass effect.
    /// </summary>
    internal static class WinApi
    {
        internal const int WmNchittest = 0x84;
        internal const int HtClient = 1;
        internal const int HtCaption = 2;

        internal const int WmDwmCompositionChanged = 0x031E;
        internal const int WmDwmCrenderingChanged = 0x031F;
        internal const int WmDwmColorizationColorChanged = 0x0320;
        internal const int WmDwmWindowMaximizedChange = 0x0321;
        internal const int WmStyleChanged = 0x7D;
        internal const int WmStyleChanging = 0x7C;

        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern bool DwmIsCompositionEnabled();
    }
}