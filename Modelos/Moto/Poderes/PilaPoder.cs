using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRONversion1.Modelos.Moto.Poderes
{

    public class Poder
    {
        public TipoPoder Tipo { get; set; }
        public int Cantidad { get; set; }

        public Poder(TipoPoder tipo, int cantidad)
        {
            Tipo = tipo;
            Cantidad = cantidad;
        }


    }
    public enum TipoPoder
    {
        Escudo,
        HiperVelocidad

    }
    public class PilaPoderes
    {
        private NodoPoder tope;  
        private int count;       

        public PilaPoderes()
        {
            tope = null;
            count = 0;
        }

        public void ApilarPoder(Poder poder)
        {
            NodoPoder nuevoNodo = new NodoPoder(poder);
            nuevoNodo.Next = tope; 
            tope = nuevoNodo;      
            count++;
        }

        public Poder DesapilarPoder()
        {
            if (tope != null) 
            {
                Poder poder = tope.Data; 
                tope = tope.Next; 
                count--;
                return poder;
            }
            return null; 
        }

        public int CantidadPoderes()
        {
            return count;
        }
        public void RotarPoder()
        {
            if (tope == null || tope.Next == null) 
                return;

            List<Poder> poderes = new List<Poder>();

            while (tope != null)
            {
                poderes.Add(DesapilarPoder());
            }

            Poder primerPoder = poderes[0];  
            poderes.RemoveAt(0);  
            poderes.Add(primerPoder); 

            for (int i = poderes.Count - 1; i >= 0; i--)
            {
                ApilarPoder(poderes[i]);
            }
        }

        public List<Poder> ObtenerPoderes()
        {
            List<Poder> poderes = new List<Poder>();
            NodoPoder actual = tope;

            while (actual != null) 
            {
                poderes.Add(actual.Data);
                actual = actual.Next;
            }

            return poderes;
        }
    }
}
