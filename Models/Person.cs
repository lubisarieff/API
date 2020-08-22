using API.Core;
using API.Core.Annotations;

namespace API.Models
{
    [BsonCollection("Person")]
    public class Person : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}