using System.Drawing;//Manejar graficos

namespace JuegoTron
{
    public abstract class Nodo
    {
        public Nodo? Arriba { get; set; }
        public Nodo? Abajo { get; set; }
        public Nodo? Izquierda { get; set; }
        public Nodo? Derecha { get; set; }

        public abstract void Dibujar(Graphics g, int x, int y, int tamano);
    }

}
