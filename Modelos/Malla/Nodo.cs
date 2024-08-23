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

    public class NodoConcreto : Nodo
    {
        public bool EstaOcupado { get; set; }

        public override void Dibujar(Graphics g, int x, int y, int tamano)
        {
            // Dibujar el fondo gris
            using (Brush brush = new SolidBrush(Color.MidnightBlue))//Gray or Indigo
            {
                g.FillRectangle(brush, x, y, tamano, tamano);
            }

            // (Opcional) Dibujar el borde de la celda en negro
            using (Pen pen = new Pen(Color.Black))
            {
                g.DrawRectangle(pen, x, y, tamano, tamano);
            }
        }
    }
}
