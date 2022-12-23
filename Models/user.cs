using System.ComponentModel.DataAnnotations;

namespace consumeapi.Models
{
    public class user
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
