using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRONversion1.Modelos.Moto.Poderes
{
    public class NodoPoder
    {
        public Poder Data { get; set; } 
        public NodoPoder Next { get; set; } 

        public NodoPoder(Poder data)
        {
            Data = data;
            Next = null;
        }
    }
}
