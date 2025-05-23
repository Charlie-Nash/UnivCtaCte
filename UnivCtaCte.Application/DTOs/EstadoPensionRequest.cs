using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivCtaCte.Application.DTOs
{
    public class EstadoPensionRequest
    {
        public int estudiante_id { get; set; }
        public int semestre_id { get; set; }
        public int categoria_id { get; set; }
        public int creditos { get; set; }
    }
}
