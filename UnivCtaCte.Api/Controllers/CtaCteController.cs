using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UnivCtaCte.Api.Helpers;
using UnivCtaCte.Application.DTOs;
using UnivCtaCte.Application.UseCases;
using UnivCtaCte.Domain.Entities;

namespace UnivCtaCte.Api.Controllers
{
    [ApiController]
    [Route("api/v1/estudiante/ctacte")]

    public class CtaCteController : ControllerBase
    {
        private readonly CtaCteService _ctaCteService;
        private readonly AppAuth _apptAuth;

        public CtaCteController(CtaCteService ctaCteService, AppAuth appAuth)
        {
            _ctaCteService = ctaCteService;
            _apptAuth = appAuth;
        }

        [HttpGet("pago-matricula/{estudiante_id}/{semestre_id}/{categoria_id}")]
        public async Task<IActionResult> ConsultarPagoMatriculaAsync(int estudiante_id, int semestre_id, int categoria_id)
        {
            try
            {
                if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
                {
                    return Unauthorized(new { respuesta = -401, status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
                }

                PagoMatriculaRequest request = new();

                request.estudiante_id = estudiante_id;
                request.semestre_id = semestre_id;
                request.categoria_id = categoria_id;

                if (request.estudiante_id == 0) { return BadRequest(new { respuesta = -400, status = HttpStatusCode.BadRequest, mensaje = "FALTA: Código de estudiante" }); }
                if (request.semestre_id == 0) { return BadRequest(new { respuesta = -400, status = HttpStatusCode.BadRequest, mensaje = "FALTA: Código de semestre" }); }
                if (request.categoria_id == 0) { return BadRequest(new { respuesta = -400, status = HttpStatusCode.BadRequest, mensaje = "FALTA: Código de categoría" }); }

                var result = await _ctaCteService.ConsultarPagoMatriculaAsync(request);

                if (result == null)
                {
                    return NotFound(new { respuesta = -404, status = HttpStatusCode.NotFound, mensaje = "No se encontró información acerca de la cta cte del estudiante" });
                }

                PagoMatriculaResult pagoMatriculaResult = new();

                pagoMatriculaResult.respuesta = result.respuesta;
                pagoMatriculaResult.mensaje = result.mensaje;
                pagoMatriculaResult.status = HttpStatusCode.OK;

                return Ok(pagoMatriculaResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    respuesta = -500,
                    status = HttpStatusCode.InternalServerError,
                    mensaje = ex.Message
                });
            }
        }

        [HttpGet("estado/{estudiante_id}/{semestre_id}")]
        public async Task<ActionResult<List<EstadoCtaCte>>> ConsultarEstadoCtaCteAsync(int estudiante_id, int semestre_id)
        {
            if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
            }

            EstadoCtaCteRequest request = new();

            request.estudiante_id = estudiante_id;
            request.semestre_id = semestre_id;

            var lstEstadoCtaCte = await _ctaCteService.ConsultarEstadoCtaCteAsync(request);

            if (lstEstadoCtaCte == null || lstEstadoCtaCte.Count == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, mensaje = "No se pudo obtener información acerca de la Cta. Cte. del estudiante" });
            }

            return Ok(lstEstadoCtaCte);
        }

        [HttpPost("generar-pension")]
        public async Task<IActionResult> GenerarPensionPorMatriculaAsync([FromBody] EstadoPensionRequest request)
        {
            try
            {
                if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
                {
                    return Unauthorized(new { respuesta = -401, status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
                }
                
                if (request.estudiante_id == 0) { return BadRequest(new { respuesta = -400, status = HttpStatusCode.BadRequest, mensaje = "FALTA: Código de estudiante" }); }
                if (request.semestre_id == 0) { return BadRequest(new { respuesta = -400, status = HttpStatusCode.BadRequest, mensaje = "FALTA: Código de semestre" }); }
                if (request.categoria_id == 0) { return BadRequest(new { respuesta = -400, status = HttpStatusCode.BadRequest, mensaje = "FALTA: Código de categoría" }); }
                
                var result = await _ctaCteService.GenerarPensionPorMatriculaAsync(request);

                if (result == null)
                {
                    return NotFound(new { respuesta = -404, status = HttpStatusCode.NotFound, mensaje = "No se pudo generar la pensión por concepto de matrícula del estudiante" });
                }

                EstadoPensionResult estadoPensionResult = new();

                estadoPensionResult.respuesta = result.respuesta;
                estadoPensionResult.mensaje = result.mensaje;
                estadoPensionResult.status = HttpStatusCode.OK;

                return Ok(estadoPensionResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    respuesta = -500,
                    status = HttpStatusCode.InternalServerError,
                    mensaje = ex.Message
                });
            }
        }
    }
}
