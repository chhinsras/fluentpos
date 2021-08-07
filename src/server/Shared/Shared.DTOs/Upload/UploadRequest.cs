// <copyright file="UploadRequest.cs" company="Fluentpos">
// --------------------------------------------------------------------------------------------------
// Copyright (c) Fluentpos. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------
// </copyright>

namespace FluentPOS.Shared.DTOs.Upload
{
    public class UploadRequest
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public UploadType UploadType { get; set; }

        public byte[] Data { get; set; }
    }
}