using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoTron
{
    public class MotoNodo
    {
        public Node GridNode { get; set; } // El nodo del grid donde se encuentra esta parte de la moto
        public MotoNodo Next { get; set; } // El siguiente nodo en la estela
    }
    public class ListaEnlazadaMoto
    {
        public MotoNodo Head { get; private set; }
        public MotoNodo Tail { get; private set; }
        public int EstelaMaxima { get; private set; }

        public ListaEnlazadaMoto()
        {
            this.EstelaMaxima = 4;
        }

        // Método para añadir un nodo al inicio de la lista
        public void Add(Node gridNode)
        {
            // Si ya hay una cabeza, actualizamos su imagen a la de la estela
            if (Head != null)
            {
                Head.GridNode.CuadroImagen.BackColor = Color.White;
            }

            MotoNodo newNodo = new MotoNodo { GridNode = gridNode };
            if (Head == null)
            {
                Head = Tail = newNodo;
            }
            else
            {
                newNodo.Next = Head;
                Head = newNodo;
            }

            // Actualizar la imagen del nuevo nodo al de la moto
            gridNode.CuadroImagen.BackColor = Color.Violet;
        }

        // Método para mover la moto, manteniendo la longitud de la estela
        public void Move(Node newGridNode)
        {
            Add(newGridNode);

            // Mantener la longitud de la estela
            MotoNodo current = Head;
            int count = 1;
            while (current.Next != null)
            {
                if (count == EstelaMaxima)
                {
                    // Restaurar la imagen del último segmento a 'bloque'
                    current.Next.GridNode.CuadroImagen.BackColor = Color.Black;
                    current.Next = null;
                    Tail = current;
                    break;
                }
                current = current.Next;
                count++;
            }
        }
    }
}