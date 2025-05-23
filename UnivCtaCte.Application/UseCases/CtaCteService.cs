using UnivCtaCte.Application.DTOs;
using UnivCtaCte.Domain.Entities;
using UnivCtaCte.Domain.Interfaces;

namespace UnivCtaCte.Application.UseCases
{
    public class CtaCteService
    {
        private readonly ICtaCteRepository _ctaCteRepository;

        public CtaCteService(ICtaCteRepository ctaCteRepository)
        {
            _ctaCteRepository = ctaCteRepository;
        }

        public async Task<PagoMatricula?> ConsultarPagoMatriculaAsync(PagoMatriculaRequest pagoMatriculaRequest)
        {
            return await _ctaCteRepository.ConsultarPagoMatriculaAsync(pagoMatriculaRequest.estudiante_id, pagoMatriculaRequest.semestre_id, pagoMatriculaRequest.categoria_id);
        }

        public async Task<List<EstadoCtaCte>> ConsultarEstadoCtaCteAsync(EstadoCtaCteRequest estadoCtaCteRequest)
        {
            return await _ctaCteRepository.ConsultarEstadoCtaCteAsync(estadoCtaCteRequest.estudiante_id, estadoCtaCteRequest.semestre_id);
        }

        public async Task<EstadoPension?> GenerarPensionPorMatriculaAsync(EstadoPensionRequest estadoPensionRequest) 
        {
            return await _ctaCteRepository.GenerarPensionPorMatriculaAsync(estadoPensionRequest.estudiante_id, estadoPensionRequest.semestre_id, estadoPensionRequest.categoria_id, estadoPensionRequest.creditos);
        }
    }
}
