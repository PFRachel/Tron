using System.Drawing;

namespace JuegoTron
{
    public class Malla
    {
        private const int Tamano = 30;
        private const int TamanoCelda = 20;
        public Nodo[,] Nodos { get; private set; }

        public Malla()
        {
            Nodos = new Nodo[Tamano, Tamano];
            CrearMalla();
        }

        private void CrearMalla()
        {
            for (int i = 0; i < Tamano; i++)
            {
                for (int j = 0; j < Tamano; j++)
                {
                    Nodos[i, j] = new NodoConcreto();
                }
            }

            for (int i = 0; i < Tamano; i++)
            {
                for (int j = 0; j < Tamano; j++)
                {
                    if (i > 0) Nodos[i, j].Arriba = Nodos[i - 1, j];
                    if (i < Tamano - 1) Nodos[i, j].Abajo = Nodos[i + 1, j];
                    if (j > 0) Nodos[i, j].Izquierda = Nodos[i, j - 1];
                    if (j < Tamano - 1) Nodos[i, j].Derecha = Nodos[i, j + 1];
                }
            }
        }

        public void Dibujar(Graphics g)
        {
            for (int i = 0; i < Tamano; i++)
            {
                for (int j = 0; j < Tamano; j++)
                {
                    Nodos[i, j].Dibujar(g, j * TamanoCelda, i * TamanoCelda, TamanoCelda);
                }
            }
        }
    }
}
