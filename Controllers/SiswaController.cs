using System.Collections.Generic;
using API.Core;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/siswa")]
    public class SiswaController : ControllerBase
    {
        private readonly IMongoRepository<Siswa> _siswaRepository;
     
        public SiswaController(IMongoRepository<Siswa> siswaRepository)
        {
            _siswaRepository = siswaRepository;
        }

        [HttpGet]   
        public IEnumerable<Siswa> Get()
        {
            return _siswaRepository.GetAll();
        }

        [HttpPost]
        public void RegisterSiswa(Siswa siswa)
        {
            _siswaRepository.InsertOne(siswa);
        }

        [HttpPut("{id}")]
        public void Update(string id, Siswa siswa)
        {
            _siswaRepository.ReplaceOne(id, siswa);
        }
    }    
}