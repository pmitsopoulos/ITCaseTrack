using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCaseTrack.Domain.Entities
{
    public class Case : BaseDomainEntity
    {
        public string? ApplicationSystemId { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public string? ActionsTaken { get; set; }
        public string? SolutionComments { get; set; }
        public bool Closed { get; set; }
        public List<string>? Attachments { get; set; }
    }
}