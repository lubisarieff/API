using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMongoRepository<Person> _peopleRepository;
      
        public WeatherForecastController(IMongoRepository<Person> peopleRepository)
        {
             _peopleRepository = peopleRepository;
              var person = new Person()
             {
                FirstName = "John",
                LastName = "Doe"
             };
             _peopleRepository.InsertOne(person);
        }

         [HttpGet]
         public IEnumerable<string> GetPeopleData()
         {
             var people = _peopleRepository.FilterBy(
                 filter => filter.FirstName != "test",
                 projection => projection.FirstName
        );
        return people;
        }
    }
}
