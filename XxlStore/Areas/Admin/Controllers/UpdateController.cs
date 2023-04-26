﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using XxlStore.Models;

namespace XxlStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UpdateController : XxlController
    {

        [HttpPost]
        public IActionResult BaseUpdate()
        {
            Console.WriteLine("Start Base Update");
            SingleFileModel model = new SingleFileModel();
            return View("Index", model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            SingleFileModel model = new SingleFileModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);

                string fileName = model.FileName + fileInfo.Extension;

                Console.WriteLine(fileInfo.Extension);

                //if (fileInfo.Extension != ".csv") {
                //    Console.WriteLine("Не csv!!!");
                //}

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
                model.IsSuccess = true;
                model.Message = "File upload successfully";

                //if (IsFileValid(model.File)) {

                //} else {
                //    model.IsSuccess = false;
                //    model.Message = "Fuck you!";
                //}

                Data.ImportCSV(fileName);

                Data.LoadObjects();
            }
            return View("Index", model);
        }

        public static bool IsFileValid(IFormFile file)
        {
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var signatures = _fileSignatures.Values.SelectMany(x => x).ToList();  // flatten all signatures to single list
                var headerBytes = reader.ReadBytes(_fileSignatures.Max(m => m.Value.Max(n => n.Length)));
                bool result = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
                return result;
            }
        }

        private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            },
            { ".jpeg2000", new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A } } },

            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            },
            { ".zip", new List<byte[]> //also docx, xlsx, pptx, ...
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
                    new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                    new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                    new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                    new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
                }
            },

            { ".pdf", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
            { ".z", new List<byte[]>
                {
                    new byte[] { 0x1F, 0x9D },
                    new byte[] { 0x1F, 0xA0 }
                }
            },
            { ".tar", new List<byte[]>
                {
                    new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72, 0x00, 0x30 , 0x30 },
                    new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72, 0x20, 0x20 , 0x00 },
                }
            },
            { ".tar.z", new List<byte[]>
                {
                    new byte[] { 0x1F, 0x9D },
                    new byte[] { 0x1F, 0xA0 }
                }
            },
            { ".tif", new List<byte[]>
                {
                    new byte[] { 0x49, 0x49, 0x2A, 0x00 },
                    new byte[] { 0x4D, 0x4D, 0x00, 0x2A }
                }
            },
            { ".tiff", new List<byte[]>
                {
                    new byte[] { 0x49, 0x49, 0x2A, 0x00 },
                    new byte[] { 0x4D, 0x4D, 0x00, 0x2A }
                }
            },
            { ".rar", new List<byte[]>
                {
                    new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07 , 0x00 },
                    new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07 , 0x01, 0x00 },
                }
            },
            { ".7z", new List<byte[]>
                {
                    new byte[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27 , 0x1C },
                }
            },
            { ".txt", new List<byte[]>
                {
                    new byte[] { 0xEF, 0xBB , 0xBF },
                    new byte[] { 0xFF, 0xFE},
                    new byte[] { 0xFE, 0xFF },
                    new byte[] { 0x00, 0x00, 0xFE, 0xFF },
                }
            },
            { ".mp3", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xFB },
                    new byte[] { 0xFF, 0xF3},
                    new byte[] { 0xFF, 0xF2},
                    new byte[] { 0x49, 0x44, 0x43},
                }
            },
        };


    }
}
