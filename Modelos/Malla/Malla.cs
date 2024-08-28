using System.Collections.Generic;//listas genericas
using System.Windows.Forms;//con controladores de formularios


namespace JuegoTron // nombre del espacio de trabajo
{
    
    public class MatrizListaEnlazada //clase de la matriz 
    {
        private int sizeFilas;//tamaño de filas 
        private int sizeColumnas;//tamaño de columnas
        private int sizeCuadroImagen;//tamaño de cada Picturebox

        public Node[,] Matriz { get;  set; }//matriz de nodos, como guia
        public ListaEnlazadaMoto Moto { get; private set; }//

        public MatrizListaEnlazada(int sizeFilas, int sizeColumnas, int sizeCuadroImagen)
        {
            this.sizeFilas = sizeFilas;
            this.sizeColumnas = sizeColumnas;
            this.sizeCuadroImagen = sizeCuadroImagen;
            Matriz = new Node[sizeFilas, sizeColumnas];
            InitializarMatriz();
            InitializarMoto();
            
        }

        private void InitializarMatriz()
        {
            for (int fila = 0; fila < sizeFilas; fila++)
            {
                for (int columna = 0; columna < sizeColumnas; columna++)
                {
                    // Crear un nuevo nodo
                    Node node = new Node
                    {
                        CuadroImagen = new PictureBox
                        {
                            Width = sizeCuadroImagen,
                            Height = sizeCuadroImagen,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.Black,
                            Location = new Point(columna * sizeCuadroImagen, fila * sizeCuadroImagen)
                        }
                    };

                    // Añadir el nodo a la matriz
                    Matriz[fila, columna] = node;

                    // Conectar con nodos vecinos si existen
                    if (fila > 0)
                    {
                        node.Arriba = Matriz[fila - 1, columna];
                        Matriz[fila - 1, columna].Abajo = node;
                    }
                    if (columna > 0)
                    {
                        node.Izquierda = Matriz[fila, columna - 1];
                        Matriz[fila, columna - 1].Derecha = node;
                    }
                }
            }
        }

        private void InitializarMoto()
        {
            Moto = new ListaEnlazadaMoto(); // Tamaño inicial de la estela

            int Cabezafila = sizeFilas / 2;
            int CabezaColumna = sizeColumnas / 2;

            // Agregar los segmentos de la estela en orden, comenzando desde la cola
            Moto.Add(Matriz[Cabezafila, CabezaColumna - 3]);
            Moto.Add(Matriz[Cabezafila, CabezaColumna - 2]); // Primer segmento de la estela (cola)
            Moto.Add(Matriz[Cabezafila, CabezaColumna - 1]); // Segundo segmento de la estela
            Moto.Add(Matriz[Cabezafila, CabezaColumna]);     // Cabeza de la moto
        }

        public void MoverMoto(Direction direccion)
        {
            Node currentNode = Moto.Head.GridNode;
            Node? nextNode = null;

            switch (direccion)
            {
                case Direction.Arriba:
                    nextNode = currentNode.Arriba;
                    break;
                case Direction.Abajo:
                    nextNode = currentNode.Abajo;
                    break;
                case Direction.Izquierda:
                    nextNode = currentNode.Izquierda;
                    break;
                case Direction.Derecha:
                    nextNode = currentNode.Derecha;
                    break;
            }

            if (nextNode != null)
            {
                Moto.Move(nextNode);
            }
        }
    }
}
