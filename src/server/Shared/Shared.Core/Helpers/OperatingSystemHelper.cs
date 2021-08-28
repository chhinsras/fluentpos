// --------------------------------------------------------------------------------------------------
// <copyright file="OperatingSystemHelper.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace FluentPOS.Shared.Core.Helpers
{
    /// <summary>
    /// OperatingSystemHelper.
    /// </summary>
    public static class OperatingSystemHelper
    {
         /// <summary>
         /// GetOperatingSystem.
         /// </summary>
        public static OSPlatform GetOperatingSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }

            throw new Exception("Cannot determine operating system!");
        }
    }
}