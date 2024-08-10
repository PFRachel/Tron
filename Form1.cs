using System;
using System.Windows.Forms;

namespace MatrizListaEnlazadaSimple
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MostrarMatriz();
        }

        private void MostrarMatriz()
        {
            ListaEnlazadaSimple matriz = new ListaEnlazadaSimple();
            Nodo nodoArriba = null;
            Nodo nodoIzquierda = null;
            Nodo primerNodoFila = null;

            for (int i = 0; i < 31; i++)
            {
                nodoIzquierda = null;
                if (i > 0)
                {
                    nodoArriba = primerNodoFila;
                    primerNodoFila = null;
                }

                for (int j = 0; j < 31; j++)
                {
                    matriz.AgregarNodo(' ', ref nodoArriba, ref nodoIzquierda, ref primerNodoFila);
                }
            }

            pictureBox.Image = matriz.ObtenerImagenMatriz(23); // Tamaño de cada celda en píxeles
        }
    }
}
