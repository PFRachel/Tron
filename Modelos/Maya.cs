/*namespace TRONversion1;

public partial class Maya : Form
{
    public Maya()
    {
        InitializeComponent();
    }
}
*/
using System;
using System.Drawing;

namespace MatrizListaEnlazadaSimple
{
    public class Nodo
    {
        public char valor;
        public Nodo arriba;
        public Nodo abajo;
        public Nodo izquierda;
        public Nodo derecha;

        public Nodo(char valor)
        {
            this.valor = valor;
            arriba = null;
            abajo = null;
            izquierda = null;
            derecha = null;
        }
    }

    public class ListaEnlazadaSimple
    {
        public Nodo cabeza;

        public ListaEnlazadaSimple()
        {
            cabeza = null;
        }

        public void AgregarNodo(char valor, ref Nodo nodoArriba, ref Nodo nodoIzquierda, ref Nodo primerNodoFila)
        {
            Nodo nuevoNodo = new Nodo(valor);

            if (nodoArriba != null)
            {
                nuevoNodo.arriba = nodoArriba;
                nodoArriba.abajo = nuevoNodo;
                nodoArriba = nodoArriba.derecha;
            }

            if (nodoIzquierda != null)
            {
                nuevoNodo.izquierda = nodoIzquierda;
                nodoIzquierda.derecha = nuevoNodo;
            }

            if (primerNodoFila == null)
            {
                primerNodoFila = nuevoNodo;
            }

            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }

            nodoIzquierda = nuevoNodo;
        }

        public Bitmap ObtenerImagenMatriz(int tamanoCelda)
        {
            int filas = 30;
            int columnas = 30;
            Bitmap imagen = new Bitmap(columnas * tamanoCelda, filas * tamanoCelda);
            using (Graphics g = Graphics.FromImage(imagen))
            {
                g.Clear(Color.FromArgb(100,100,100));// GRIS oscuro
               
                Nodo filaActual = cabeza;
                for (int i = 0; i < filas; i++)
                {
                    Nodo columnaActual = filaActual;
                    for (int j = 0; j < columnas; j++)
                    {
                        g.DrawRectangle(Pens.Black, j * tamanoCelda, i * tamanoCelda, tamanoCelda, tamanoCelda);
                        g.DrawString(columnaActual.valor.ToString(), SystemFonts.DefaultFont, Brushes.Black, j * tamanoCelda + 5, i * tamanoCelda + 5);

                        columnaActual = columnaActual.derecha;
                    }
                    filaActual = filaActual.abajo;

                }
               
            }
            return imagen;
        }
    }
}
