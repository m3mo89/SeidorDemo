using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeidorDemo.Models
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetAll();
    }
}
