namespace WebApiComidas.Entidades
{
    public class Restaurante
    {
        public int Id { get; set; }

        public string Nombre { get; set;}

        public string Direccion { get; set; }

        public int ComidaId { get; set; }

        public Comida Comida { get; set; }
    }
}
