using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnivCtaCte.Application.DTOs
{
    public class EstadoPensionResult
    {
        public int respuesta { get; set; }
        public string mensaje { get; set; } = "";

        public HttpStatusCode status { get; set; }
    }
}
