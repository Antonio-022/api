using API_SAADS_CORE_61.DTOs;
using API_SAADS_CORE_61.Helpers;
using API_SAADS_CORE_61.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;

namespace API_SAADS_CORE_61.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccesosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRepositorio _repsositorio;
        private const int moduloId = 1300;// colocar el modulo correspodiente

    
        public AccesosController(IRepositorio repositorio, IWebHostEnvironment env)
        {
            _env = env;
            _repsositorio = repositorio;
        }

        //var respuestaHTTP = await _repsositorio.Post<PagoGenerarDTO, PagoGenerarResp>("RegistrarPago", consulta);
        //var res = respuestaHTTP.Response;


        [HttpGet("RenovarToken")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<UserToken>> RenovarToken()
        {
            try
            {
                var respuestaHTTP = await _repsositorio.Get<UserToken>("Funcionarios/RenovarToken");
                var res = respuestaHTTP.Response;

                if (respuestaHTTP.Error)
                {
                    return BadRequest(respuestaHTTP.HttpResponseMessage.ToString());
                }

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Funcionario")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<FuncionarioDTO>> Get()
        {

            try
            {
                var respuestaHTTP = await _repsositorio.Get<FuncionarioDTO>("Funcionarios");
                var res = respuestaHTTP.Response;

                if (respuestaHTTP.Error)
                {
                    return BadRequest(respuestaHTTP.HttpResponseMessage.ToString());
                }

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Tareas/{interfaceId:int}")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<List<TareaDTO>>> GetTareas(int interfaceId)
        {

            try
            {
                var respuestaHTTP = await _repsositorio.Get<TareaDTO>($"Funcionarios/Tareas/{interfaceId}");
                var res = respuestaHTTP.Response;

                if (respuestaHTTP.Error)
                {
                    return BadRequest(respuestaHTTP.HttpResponseMessage.ToString());
                }

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }

   
       /* [HttpGet("QR")]
        [AllowAnonymous]
        public async Task<ActionResult<byte[]>> getQR(string texto)
        {
            try
            {
                return QR.GenerarQR(texto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }*/

        [HttpPost, Route("QRwithLogo")]
        public ActionResult<string> QRwithLogo( QrDto Imagen)
        {
            byte[] bytes = Convert.FromBase64String(Imagen.ImagenB64);
            return QR.GenerarQRWithUploadImage(Imagen.Contenido, bytes);

        }
        [HttpPost, Route("QRContact")]
        public ActionResult<string> QRContact(QrDto Imagen)
        {
            byte[] bytes = Convert.FromBase64String(Imagen.ImagenB64);
            return QR.GenerarQRContact(Imagen.Contenido, bytes);

        }

   

       

        [HttpGet("Interfaces")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<List<InterfaceDTO>>> getIterfaces()
        {

            try
            {
                var respuestaHTTP = await _repsositorio.Get<List<InterfaceDTO>>($"Funcionarios/Interfaces/{moduloId}");
                var res = respuestaHTTP.Response;

                if (respuestaHTTP.Error)
                {
                    return BadRequest(respuestaHTTP.HttpResponseMessage.ToString());
                }

                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
    }
}
