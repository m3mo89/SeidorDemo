using System;
using Microsoft.AspNetCore.Mvc;

using SeidorDemo.Models;

namespace SeidorDemo.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {

        private readonly IDocumentRepository _ItemRepository;

        public DocumentController(IDocumentRepository ItemRepository)
        {
            _ItemRepository = ItemRepository;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_ItemRepository.GetAll());
        }
    }
}
