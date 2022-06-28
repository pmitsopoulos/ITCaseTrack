using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCaseTrack.Persistence.Models
{
    public class CaseStudyDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string Database { get; set; } = null!;
        public string CaseCollection { get; set; } = null!;
        public string ContactCollection { get; set; } = null!;
        public string SystemCollection { get; set; } = null!;
    }
}

