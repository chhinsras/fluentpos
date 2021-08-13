// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterBrandCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands
{
    public class RegisterBrandCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Detail { get; set; }

        public UploadRequest UploadRequest { get; set; }
    }
}