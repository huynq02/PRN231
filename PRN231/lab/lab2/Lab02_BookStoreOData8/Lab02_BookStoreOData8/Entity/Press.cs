using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Press
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
