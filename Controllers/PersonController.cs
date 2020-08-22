using System.Collections.Generic;
using API.Core;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[api/person]")]
    public class PersonController : ControllerBase
    {
        private IMongoRepository<Person> _personRepository;

        public PersonController(IMongoRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }  

        [HttpGet]
        public ICollection<Person> GetAll()
        {
            return _personRepository.GetAll();
        }

        [HttpGet("{id : length(24)}", Name = "GetPerson")]
        public Person GetPersonById(string id)
        {
            return _personRepository.FindById(id);
        }

        [HttpPost]
        public void InsertPerson(Person person)
        {
            _personRepository.InsertOne(person);
        }      
    }
}