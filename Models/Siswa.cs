using API.Core;
using API.Core.Annotations;

namespace API.Models
{
    [BsonCollection("siswa")]
    public class Siswa : Document
    {
        public string Nama { get; set; }
        public string Alamat { get; set; }
    }
}