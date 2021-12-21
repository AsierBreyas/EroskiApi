using System.ComponentModel.DataAnnotations;
namespace EroskiApi.Models{
    public class Departamento{
        [Key]
        public string nombre {get; set;}
        public int numero {get; set;}
    }
}