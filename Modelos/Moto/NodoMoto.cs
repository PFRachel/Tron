using System.Drawing;
using System.Windows.Forms;

namespace JuegoTron
{
    public class NodoMoto
    {
        // Propiedad PictureBox para representar el segmento visualmente
        public PictureBox PictureBox { get; private set; }

        // Propiedad para enlazar con el siguiente segmento
        public NodoMoto? Siguiente { get; set; }

        // Constructor de la clase NodoMoto

        public NodoMoto(int x, int y, Color color)
        {
            // Inicializa el PictureBox
            PictureBox = new PictureBox
            {
                Width = 20, // Tamaño de la celda
                Height = 20,
                BackColor = color, // Color del segmento
                BorderStyle = BorderStyle.FixedSingle,
                Left = x,
                Top = y
            };
        }
    }
}
