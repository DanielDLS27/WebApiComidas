using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiComidas.Validaciones;

namespace WebApiComidas.Entidades
{
    public class Comida : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:10, ErrorMessage = "El campo {0} solo puede tener hasta 10 caracteres")]
       // [PrimeraLetraMayuscula]
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

        [NotMapped]
        public int menor { get; set; }
        [NotMapped]
        public int mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra de ser mayuscula",
                        new String[] { nameof(Nombre) });
                }
            }

            if (menor > mayor)
            {
                yield return new ValidationResult("El valor de menor no puede ser mas grande que el valor de mayor!",
                    new String[] { nameof(menor) });
            }

        }
    }
}
