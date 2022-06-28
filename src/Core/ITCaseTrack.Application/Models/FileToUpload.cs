using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCaseTrack.Application.Models
{
    public class FileToUpload
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileInString { get; set; }
    }
}