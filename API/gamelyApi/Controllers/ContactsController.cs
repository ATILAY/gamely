using gamelyApi.Data;
using gamelyApi.Models;
using gamelyApi.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gamelyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly GamelyDbContext dbContext;

        public ContactsController(GamelyDbContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllContacts() {
            var contacts = dbContext.Contacts.ToList();

            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult AddContact(AddContactRequestDTO request) {
            var domainModelContact = new Contact {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Favorite = request.Favorite,
            };

            dbContext.Contacts.Add(domainModelContact);
            dbContext.SaveChanges();

            return Ok(domainModelContact);
        }
    }
}
