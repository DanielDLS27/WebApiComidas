using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiComidas.Entidades;
using WebApiComidas.Filtros;
using WebApiComidas.Services;

namespace WebApiComidas.Controllers
{
    [ApiController]
    [Route("api/comidas")] // api/comidas
    //[Authorize]
    public class ComidasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<ComidasController> logger;

        public ComidasController(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<ComidasController> logger)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            logger.LogInformation("Durante la ejecucion");
            return Ok(new
            {
                ComidasControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                ComidasControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                ComidasControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet] // api/comidas
        [HttpGet("listado")] // api/comidas/listado
        [HttpGet("/listado")] // listado
        //[ResponseCache(Duration =15)]
        //[Authorize]
        public async Task<ActionResult<List<Comida>>> Get()
        {
            // * Niveles de logs
            // Critical
            // Error
            // Warning
            // Information - Configuration actual
            // Debug
            // Trace
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de comidas");
            logger.LogWarning("Se obtiene el listado de comidas!");
            service.ejecutarJob();
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
                logger.LogError("No se encuentra la comida. ");
                return NotFound();
            }
            return comida;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Comida comida)
        {
            var existeLibroMismotitulo = await dbContext.Comidas.AnyAsync(x => x.Nombre == comida.Nombre);

            if (existeLibroMismotitulo)
            {
                return BadRequest("Ya existe una comida con el mismo nombre");
            }

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
