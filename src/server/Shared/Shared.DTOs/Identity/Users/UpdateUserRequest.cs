// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateUserRequest.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace FluentPOS.Shared.DTOs.Identity.Users
{
    public class UpdateUserRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        // [Required]
        // [EmailAddress]
        // public string Email { get; set; }

        // [Required]
        // [MinLength(6)]
        // public string UserName { get; set; }

        [MinLength(6)]
        public string CurrentPassword { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
    }
}