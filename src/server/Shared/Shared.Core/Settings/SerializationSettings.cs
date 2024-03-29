﻿// --------------------------------------------------------------------------------------------------
// <copyright file="SerializationSettings.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace FluentPOS.Shared.Core.Settings
{
    public class SerializationSettings
    {
        public bool UseSystemTextJson { get; set; }

        public bool UseNewtonsoftJson { get; set; }
    }
}