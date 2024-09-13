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

        private void InitializarMoto(int Filas,int Columnas)
        {
            Moto = new ListaEnlazadaMoto(); 

            int nodoCentral = ObtenerNodoCentral(Filas,Columnas);
            int nodoActual = nodoCentral.Izquierda.Izquierda;
            
            Moto.Add(nodoActual);
            Moto.Add(nodoActual.Derecha); // Primer segmento de la estela (cola)
            Moto.Add(nodoActual.Derecha.Derecha); // Segundo segmento de la estela
            Moto.Add(nodoActual.Derecha.Derecha.Derecha); // Cabeza de la moto
        }
        
        private Node ObtenerNodoCentral(int Filas, int Columnas)
        {
            Node nodoActual = NodoInicial;
            int filaCentral = Filas / 2;
            int columnaCentral = Columnas / 2;


            for (int i = 0; i < filaCentral; i++)
            {
                nodoActual = nodoActual.Abajo;
            }

            for (int i = 0; i < columnaCentral; i++)
            {
                nodoActual = nodoActual.Derecha;
            }

            return nodoActual;
        }

        private int ObtenerCantidadNodos()
        {
            Node nodoActual = NodoInicial;
            int count = 0;
            while (nodoActual != null)
            {
                Node nodoFila = nodoActual;
                while (nodoFila != null)
                {
                    count++;
                    nodoFila = nodoFila.Derecha;
                }

                nodoActual = nodoActual.Abajo;
            }

            return count;
        }

        public void MoverMoto(Direction direccion)
        {
            Node currentNode = Moto.Head.GridNode;
            Node? nodoSiguiente = null;

            switch (direccion)
            {
                case Direction.Arriba:
                    nodoSiguiente = currentNode.Arriba;
                    break;
                case Direction.Abajo:
                    nodoSiguiente = currentNode.Abajo;
                    break;
                case Direction.Izquierda:
                    nodoSiguiente = currentNode.Izquierda;
                    break;
                case Direction.Derecha:
                    nodoSiguiente = currentNode.Derecha;
                    break;
            }

            if (nodoSiguiente != null) //&& !EsColision(nodoSiguiente)
            {
                Moto.Move(nodoSiguiente);
                //return false;
            }
        }
        public Node ObtenerNodoEn(int fila,int columna)
        {
            Node nodoActual = NodoInicial;
            for(int i= 0; i< fila && nodoActual != null; i++)
            {
                nodoActual = nodoActual.Abajo;

            }
            for(int j= 0; j< columna && nodoActual != null; j++)
            {
                nodoActual = nodoActual.Derecha;  
            }
            return nodoActual;
        }

    }
}
