// --------------------------------------------------------------------------------------------------
// <copyright file="UploadService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.DTOs.Upload;
using FluentPOS.Shared.Infrastructure.Extensions;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class UploadService : IUploadService
    {
        public Task<string> UploadAsync(UploadRequest request)
        {
            if (request.Data == null)
            {
                return Task.FromResult(string.Empty);
            }

            string base64Data = Regex.Match(request.Data, "data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

            var streamData = new MemoryStream(Convert.FromBase64String(base64Data));
            if (streamData.Length > 0)
            {
                string folder = request.UploadType.ToDescriptionString();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    folder = folder.Replace(@"\", "/");
                }

                string folderName = Path.Combine("Files", folder);
                string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                bool exists = Directory.Exists(pathToSave);
                if (!exists)
                {
                    Directory.CreateDirectory(pathToSave);
                }

                string fileName = request.FileName.Trim('"');
                string fullPath = Path.Combine(pathToSave, fileName);
                string dbPath = Path.Combine(folderName, fileName);
                if (File.Exists(dbPath))
                {
                    dbPath = NextAvailableFilename(dbPath);
                    fullPath = NextAvailableFilename(fullPath);
                }

                using var stream = new FileStream(fullPath, FileMode.Create);
                streamData.CopyTo(stream);
                return Task.FromResult(dbPath);
            }
            else
            {
                return Task.FromResult(string.Empty);
            }
        }

        private static string numberPattern = " ({0})";

        private static string NextAvailableFilename(string path)
        {
            // Short-cut if already available
            if (!File.Exists(path))
            {
                return path;
            }

            // If path has extension then insert the number pattern just before the extension and return next filename
            if (Path.HasExtension(path))
            {
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), numberPattern));
            }

            // Otherwise just append the pattern to the path and return next filename
            return GetNextFilename(path + numberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);

            // if (tmp == pattern)
            // throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
            {
                return tmp; // short-circuit if no matches
            }

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                {
                    min = pivot;
                }
                else
                {
                    max = pivot;
                }
            }

            return string.Format(pattern, max);
        }
    }
}