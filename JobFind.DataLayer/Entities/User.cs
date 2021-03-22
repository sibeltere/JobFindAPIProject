using System.ComponentModel.DataAnnotations.Schema;

namespace JobFind.DataLayer.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CV CV { get; set; }
    }
}
