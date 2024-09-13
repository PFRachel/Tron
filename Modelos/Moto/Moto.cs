using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoTron
{
    public class MotoNodo
    {
        public Node? GridNode { get; set; } // El nodo del grid donde se encuentra esta parte de la moto
        public MotoNodo? Next { get; set; } // El siguiente nodo en la estela
        public Node? Position {get; set; }// El nodo donde esta esa parte de la moto
    }
    public class ListaEnlazadaMoto
    {
        public MotoNodo Head { get; private set; }
        public MotoNodo Tail { get; private set; }
        public int EstelaMaxima { get; private set; }
        public Direction CurrentDirection  { get; set; }
        /// 
        public Node? PosicionActual {get; private set;}//Posicion actual
        public int Direccion {get; set; } //0 arriba, 1 derecha, 2 abajo, 3 izquierda 
        public int Combustible { get;set; }
        public bool Vivo = true;
        public Color colorMoto = Color.Red;
        public Color colorEstelaMoto = Color.LightCoral;
        public ListaEnlazadaMoto()
        {
            this.EstelaMaxima = 4;
        }
        public void RecargarCombustible(int cantidad)
        {
            Combustible += cantidad;
            if (Combustible > 100)
            {
                Combustible = 100; // Limitar el combustible a 100
            }
        }
        // Método para añadir un nodo al inicio de la lista
        public void Add(Node gridNode)
        {
            // Si ya hay una cabeza, actualizamos su imagen a la de la estela
            if (Head != null)
            {
                Head.GridNode.CuadroImagen.BackColor = colorEstelaMoto;
            }

            MotoNodo newNodo = new MotoNodo { GridNode = gridNode, Position = gridNode };
            if (Head == null)
            {
                Head = Tail = newNodo;
                PosicionActual = gridNode;
            }
            else
            {
                newNodo.Next = Head;
                Head = newNodo;
            }

            // Actualizar la imagen del nuevo nodo al de la moto
            gridNode.CuadroImagen.BackColor = colorMoto;
        }
        public void MoverConCombustible(Node newGridNode)
        {
            if (Combustible > 0)
            {
                Move(newGridNode);
                Combustible -= 1; // Decrece el combustible
            }
            else
            {
                
                MessageBox.Show("Se acabó el combustible, ¡has perdido!");
            }
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
            PosicionActual = newGridNode;
        }
        public void AumentarEstela(int cantidad)
        {
            EstelaMaxima += cantidad; 
        }
        //obtenet el nodo siguiente del movimeinto actual 
         public Node GetsiguienteNodo()
        {
            switch (Direccion)
            {
                case 0: return PosicionActual?.Arriba;    
                case 1: return PosicionActual?.Derecha;  
                case 2: return PosicionActual?.Abajo;     
                case 3: return PosicionActual?.Izquierda; 
                default: return PosicionActual;
            }
        }

    }
}