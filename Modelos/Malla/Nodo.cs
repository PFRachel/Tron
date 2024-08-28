using System.Drawing;//Manejar graficos

namespace JuegoTron // espacio nombre del juego
{
    public class Node// clase nodo
    {
        public PictureBox CuadroImagen{get; set;} //cuadro donde se dibuja

        public Node Arriba{get; set;}// Nodo conectado de arriba
        public Node Abajo {get; set;}//Nodo conectado abajo

        public Node Izquierda{get; set;}//Nodo conectado derecha
        public Node Derecha{get; set;}//Nodo conectado derecha

    }

}
