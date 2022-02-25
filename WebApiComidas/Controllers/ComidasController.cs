using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiComidas.Entidades;

namespace WebApiComidas.Controllers
{
    [ApiController]
    [Route("api/comidas")] // api/comidas
    public class ComidasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ComidasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet] // api/comidas
        [HttpGet("listado")] // api/comidas/listado
        [HttpGet("/listado")] // listado
        public async Task<ActionResult<List<Comida>>> Get()
        {
            return await dbContext.Comidas.Include(x => x.restaurantes).ToListAsync();
        }

        [HttpGet("primera")] //api/comidas/primera
        public async Task<ActionResult<Comida>> PrimerComida([FromHeader] int valor, [FromQuery] string comida, [FromQuery] int comidaId)
        {
            return await dbContext.Comidas.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/{param=cuajitos}")]
        public ActionResult<Comida> Get(int id, string param)
        {
            var comida = dbContext.Comidas.FirstOrDefault(x => x.Id == id);

            if(comida == null)
            {
                return NotFound();
            }
            return comida;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Comida>> Get([FromRoute]string nombre)
        {
            var comida = await dbContext.Comidas.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (comida == null)
            {
                return NotFound();
            }
            return comida;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Comida comida)
        {
            dbContext.Add(comida);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        
        public async Task<ActionResult> Put(Comida comida, int id)
        {
            if(comida.Id != id)
            {
                return BadRequest("El id del alumno no coincide con el establecido en la url");
            }

            dbContext.Update(comida);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Comidas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Comida()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }  
    }
}
