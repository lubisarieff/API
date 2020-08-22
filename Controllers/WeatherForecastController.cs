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
    [Route("/api/person")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMongoRepository<Person> _peopleRepository;
      
        public WeatherForecastController(IMongoRepository<Person> peopleRepository)
        {
             _peopleRepository = peopleRepository;
        }
        
        [HttpGet]
        public List<Person> GetAll()
        {
            return _peopleRepository.GetAll();
        }
    }
}
