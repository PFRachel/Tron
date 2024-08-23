using System.Drawing;//Manejar graficos

namespace JuegoTron
{
    
    public class NodoConcreto : Nodo
    {
        public bool EstaOcupado { get; set; }

        public override void Dibujar(Graphics g, int x, int y, int tamano)
        {
            // Dibujar el fondo gris
            using (Brush brush = new SolidBrush(Color.Black))//Gray or Indigo MidnightBlue
            {
                g.FillRectangle(brush, x, y, tamano, tamano);
            }

            // (Opcional) Dibujar el borde de la celda en negro
            using (Pen pen = new Pen(Color.RoyalBlue)) //RoyalBlue MediumBlue
            {
                g.DrawRectangle(pen, x, y, tamano, tamano);
            }
        }
    }
}