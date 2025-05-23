using UnivCtaCte.Domain.Entities;

namespace UnivCtaCte.Domain.Interfaces
{
    public interface ICtaCteRepository
    {
        Task<PagoMatricula?> ConsultarPagoMatriculaAsync(int estudianteId, int semestreId, int categoriaId);
        Task<List<EstadoCtaCte>> ConsultarEstadoCtaCteAsync(int estudianteId, int semestreId);
        Task<EstadoPension?> GenerarPensionPorMatriculaAsync(int estudianteId, int semestreId, int categoriaId, int creditos);
    }
}
