using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiComidas.Entidades;

namespace WebApiComidas.Controllers
{
    [ApiController]
    [Route("api/restaurantes")]
    public class RestaurantesController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public RestaurantesController (ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Restaurante>>> GetAll()
        {
            return await dbContext.Restaurantes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Restaurante>> GetById(int id)
        {
            return await dbContext.Restaurantes.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Restaurante restaurante)
        {
            var existeComida = await dbContext.Comidas.AnyAsync(x => x.Id == restaurante.ComidaId);

            if (!existeComida)
            {
                return BadRequest($"No existe la comida con el id: {restaurante.ComidaId}");
            }

            dbContext.Add(restaurante);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Restaurante restaurante, int id)
        {
            var exist = await dbContext.Restaurantes.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El restaurante especificado no existe. ");
            }

            if(restaurante.Id != id)
            {
                return BadRequest("El id del restaurante no coincide con el establecido en la url. ");
            }

            dbContext.Update(restaurante);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Restaurantes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado. ");
            }

            //var validateRelation = await dbContext.ComidaRestaurante.AnyAsync;

            dbContext.Remove(new Restaurante {Id = id});
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
