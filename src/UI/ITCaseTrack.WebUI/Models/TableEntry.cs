using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCaseTrack.WebUI.Models
{
    public class TableEntry<T>
    {
        public bool Revealed { get; set; }
        public T Entity { get; set; }
    }
}