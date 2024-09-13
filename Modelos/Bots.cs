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
            colorBot = Color.Blue;
            colorEstela = Color.LightBlue;
        }
        public void MoverBot()
        {

            Node siguienteNodo;
            do
            {
                CambiarDireccionAleatoria();
                siguienteNodo = GetsiguienteNodo();
            } while (!verificarNodo(siguienteNodo));
            
            
            //Mover bot 
            if (siguienteNodo != null)
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
    }

}