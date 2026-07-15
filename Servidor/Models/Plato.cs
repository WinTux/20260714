using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class Plato
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
