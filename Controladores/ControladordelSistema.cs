using System;
using System.Windows.Forms;
using System.Drawing;
//dibujar la maya 
namespace JuegoTron
{
    public partial class Form1 : Form
    {
        private Malla malla;

        public Form1()
        {
            InitializeComponent();
            malla = new Malla(); // Crea la malla de 30x30.
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Malla malla = new Malla();
            malla.Dibujar(e.Graphics); // Dibuja la malla cuando se pinta la ventana.

        }
    }
}
