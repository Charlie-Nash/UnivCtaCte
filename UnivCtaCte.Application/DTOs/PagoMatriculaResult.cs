using System.Net;

namespace UnivCtaCte.Application.DTOs
{
    public class PagoMatriculaResult
    {
        public int respuesta { get; set; }
        public string mensaje { get; set; } = "";

        public HttpStatusCode status { get; set; }
    }
}
