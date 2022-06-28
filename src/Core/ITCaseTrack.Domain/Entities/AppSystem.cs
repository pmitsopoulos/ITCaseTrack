using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCaseTrack.Domain.Entities
{
    public class AppSystem : BaseDomainEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ContactId { get; set; }
    }
}