using System.IO.Compression;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Buffers.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ITCaseTrack.Application.Models;

namespace ITCaseTrack.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost]
        [Route("{objectid}")]
        public async Task PostFile([FromBody] List<FileToUpload> files, string ObjectId)
        {
            var path = Path.Combine("Data", ObjectId);
            
            if (files != null)
            { 
                Directory.CreateDirectory(path);
                foreach (var file in files)
                {
                    // var ln = file.Size;
                    var buffer = Convert.FromBase64String(file.FileInString);
                    // await file.OpenReadStream(1000000).ReadAsync(buffer);
                    var fullPath = Path.Combine(path,file.Name);
                    var filesInDirectory = Directory.EnumerateFiles(path).ToList();
                    if(!filesInDirectory.Exists(f => f == file.Name))
                    {
                        using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                        {
                            await fs.WriteAsync(buffer);
                        }
                    }
                }   
            }       
        }

        [HttpDelete]
        [Route("{objectId}/{fileName}")]
        public async Task DeleteFile(string ObjectId, string fileName)
        {
            var path = Path.Combine("Data",ObjectId);
            var filesInDirectory = Directory.GetFiles(path).ToList();
            System.IO.File.Delete($"{path}\\{fileName}");
        }

        [HttpGet]
        [Route("download/{objectId}/{fileName}")]
        [ActionName("FileDownload")]
        public async Task<ActionResult> DownloadFile(string ObjectId, string fileName)
        {
           var path = Path.Combine("Data",$"{ObjectId}/{fileName}");
           var file = await System.IO.File.ReadAllBytesAsync(path);
           var extention = $"application/{fileName.Substring(fileName.LastIndexOf("."))}";
           return File(file, extention);
        }

        [HttpGet]
        [Route("downloadAll/{objectid}")]
        public async Task<ActionResult> DownloadAllAttachedFiles(string ObjectId)
        {
           //TODO IMPLEMENT DOWNLOAD ALL IN ZIP FORM FUNCTIONALITY OR JUST USE THE DOWNLOAD FILE ENDPOINT FOR EACH FILE
            var path = Path.Combine("Data",ObjectId);

            var fPath = Path.Combine(path,$"attachments.zip");


            using (FileStream zipToOpen = new FileStream(fPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
            {
                foreach (var f in Directory.GetFiles(path).ToList().Where(x=> !Path.GetFileName(x).Equals("attachments.zip")))
                {
                    var entryName = Path.GetFileName(f);
                    var entry = archive.CreateEntry(entryName);
                    //entry.LastWriteTime = File.LastWriteTime(f);
                    using (var fs = new FileStream(f, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var stream = entry.Open())
                    {
                        fs.CopyTo(stream);
                    }
                }
            }

            // var items = Directory.GetFiles(path).ToList();

            // if(items.Contains(fPath))
            // {
            //     System.IO.File.Delete(fPath);
            // }

            // ZipFile.CreateFromDirectory(path,fPath);

            var file = await System.IO.File.ReadAllBytesAsync(fPath);

            var extention = "application/zip";

            return File(file, extention);
        }


        //TODO IMPLEMENT FILE PREVIEW IN FRONT END AND ENDPOINT ACCORDING TO THE FILE TYPES
        [HttpGet]
        [Route("{objectid}")]
        public async Task<List<string>> RetrieveAttachedFiles(string ObjectId)
        {
           var path = Path.Combine("Data", ObjectId);
           if(Directory.Exists(path))
           {
            var tempFilePaths = Directory.GetFiles(path).ToList();
            var files = new List<string>();
            foreach(var file in tempFilePaths)
                {
                    files.Add(Path.GetFileName(file));
                }
            return files;
           }
           return new List<string>();
        }
    }
}