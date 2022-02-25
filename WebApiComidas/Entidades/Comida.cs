using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiComidas.Entidades
{
    public class Comida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:10, ErrorMessage = "El campo {0} solo puede tener hasta 10 caracteres")]
        public string Nombre { get; set; }

        [Range(10,100, ErrorMessage = "El campo precio no esta dentro del rango")]
        [NotMapped]
        public int Precio { get; set; }

        [NotMapped]
        [CreditCard]
        public string Tarjeta { get; set; }

        [NotMapped]
        [Url]
        public string Url { get; set; }

        public List<Restaurante> restaurantes { get; set; }
    }
}
