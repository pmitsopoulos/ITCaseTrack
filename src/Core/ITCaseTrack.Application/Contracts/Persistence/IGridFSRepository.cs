using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ITCaseTrack.Application.Contracts.Persistence
{
    public interface IGridFSRepository
    {
        Task <IEnumerable<Stream>> RetrieveAttachments (List<ObjectId> ids);
        Task<ObjectId> Upload(Stream filestream, string fileName);
        Task<Stream> Download(ObjectId Id);
        Task Delete(ObjectId Id);
        Task<bool> Exists (ObjectId id);
        Task<IEnumerable<ObjectId>> AddAttachments(IEnumerable<IBrowserFile> files);
    }
}
