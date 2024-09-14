using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace JuegoTron 
{
    public class Bot: ListaEnlazadaMoto
    {
        private static Random random = new Random();
        private Color colorBot;
        private Color colorEstela;
        public Bot() : base()
        {
            colorBot = Color.Yellow;
            colorEstela = Color.LightYellow;
        }
        public void MoverBot()
        {
            Node siguienteNodo = null;

            bool movimientoValido = false;
            int intentos = 0;

            while (!movimientoValido /*&& intentos < 4*/)
            {
                CambiarDireccionAleatoria();
                siguienteNodo = GetsiguienteNodo();

                if (siguienteNodo != null && verificarNodo(siguienteNodo) && !EsBorde(siguienteNodo))
                {
                    movimientoValido = true;
                }

                //intentos++;
            }

            if (movimientoValido && siguienteNodo != null)
            {
                Move(siguienteNodo);
                PintarBot(); 
            }
        }


        public bool verificarNodo(Node node)
        {
            MotoNodo temp = Head;
            while (temp != null)
            {
                if (temp.Position == node)
                {
                    return false;
                }

                temp = temp.Next;
            }
            return true;
        }

        private void CambiarDireccionAleatoria()
        {
            Direccion = random.Next(0,4);
        }
        private void PintarBot()
        {
            MotoNodo temp = Head;
            temp.GridNode.CuadroImagen.BackColor = colorBot;
            temp = temp.Next;
            while(temp != null)
            {
                temp.GridNode.CuadroImagen.BackColor = colorEstela;
                temp = temp.Next;
                
            }

        }
        private bool EsBorde(Node nodo)
        {
            // Verificar si el nodo es un borde en cualquier dirección
            if (nodo.Arriba == null || nodo.Abajo == null || nodo.Izquierda == null || nodo.Derecha == null)
            {
                return true; // Es un borde
            }

            return false; // No es un borde
        }
    }

}