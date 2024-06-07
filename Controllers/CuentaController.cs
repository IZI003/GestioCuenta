using Comunes.Config;
using Comunes.Respuesta;
using Cuenta.Modelos;
using Cuenta.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Cuenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuenta CuentaServices;

        public CuentaController(ICuenta CuentaServices)
        { 
            this.CuentaServices = CuentaServices;
        }
        // GET: api/<CuentaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<CuentaController>/saldo/5
        [HttpGet("saldo/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new RespuestaApi<SalidaSaldo>();
            var salida = await  CuentaServices.ObtenerSaldoIdCuenta(id);

            if (!salida.RespuestaBD.Ok)
            {
                response.Resultado.AgregarError(GestionErrores.C_Men_100, 403, mensaje: GestionErrores.C_Cod_100, codigoInterno: GestionErrores.C_Cod_100);

                return response.ObtenerResult();
            }
            response.Resultado.Datos = salida;
            return Ok(response);
        }

        [HttpGet("saldo-total/{idusuario}")]
        public async Task<IActionResult> GetSaldoUsuario(int idusuario)
        {
            var response = new RespuestaApi<SalidaSaldoTotal>();
            var salida = await CuentaServices.ObtenerSaldoIdUsuario(idusuario);

            if (!salida.RespuestaBD.Ok)
            {
                response.Resultado.AgregarError(GestionErrores.C_Men_100, 403, mensaje: GestionErrores.C_Cod_100, codigoInterno: GestionErrores.C_Cod_100);

                return response.ObtenerResult();
            }
            response.Resultado.Datos = salida;
            return Ok(response);
        }
        // POST api/<CuentaController>
        [HttpPost]
        public async Task<IActionResult> CrearCuenta([FromBody] int idusuario)
        {
            var response = new RespuestaApi<RespuestaBD>();

            var salida = await CuentaServices.CrearCuenta(idusuario);

            if (!salida.Ok)
            {
                response.Resultado.AgregarError(GestionErrores.C_Men_101, 400, mensaje: salida.Mensaje, codigoInterno: GestionErrores.C_Cod_101);

                return response.ObtenerResult();
            }

            response.Resultado.Datos = salida;

            return Ok(response);
        }

        // PUT api/<CuentaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
