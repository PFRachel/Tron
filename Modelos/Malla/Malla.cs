using System.Collections.Generic;//listas genericas
using System.Windows.Forms;//con controladores de formularios


namespace JuegoTron // nombre del espacio de trabajo
{
    
    public class MallaListaEnlazada //clase de la malla 
    {
        private int sizeCuadroImagen;//tamaño de cada Picturebox
        public Node NodoInicial { get; private set; }// nodo incial de listas enlazdas 
        public ListaEnlazadaMoto Moto { get; private set; }//lista enlazada de la moto
        public MallaListaEnlazada(int Filas, int Columnas, int sizeCuadroImagen)
        {//clase de la malla
            this.sizeCuadroImagen = sizeCuadroImagen;
          
            InitializarMalla(Filas,Columnas);
            InitializarMoto(Filas,Columnas);
            
        }

        private void InitializarMalla(int Filas,int Columnas)
        {
            Node nodoArriba = null;
            Node nodoAnteriorColumna = null;
            for (int fila = 0; fila < Filas; fila++)
            {
                Node nodoAnteriorFila = null;
                for (int columna = 0; columna < Columnas; columna++)
                {
                    if (columna == 0)
                    {
                        nodoArriba = nodoAnteriorColumna;
                    }
                    // Crear un nuevo nodo
                    Node nodoActual = new Node
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
                    // conexion con los nodos
                    if(nodoAnteriorFila != null)
                    {
                        nodoAnteriorFila.Derecha = nodoActual;
                        nodoActual.Izquierda = nodoAnteriorFila;
                    }
                    if (nodoArriba != null)
                    {
                        nodoArriba.Abajo = nodoActual;
                        nodoActual.Arriba = nodoArriba;
                        nodoArriba = nodoArriba.Derecha;
                    }
                    nodoAnteriorFila = nodoActual;
                    if (fila ==0 && columna ==0)
                    {
                        NodoInicial = nodoActual;
                    }
                    if (columna == 0)
                    {
                        nodoAnteriorColumna = nodoActual;
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
