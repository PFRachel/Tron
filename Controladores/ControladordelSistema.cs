using System;
using System.Windows.Forms;
using System.Drawing;
//dibujar la maya 
namespace JuegoTron
{
        public enum Direction
    {
        Arriba,
        Abajo, 
        Izquierda, 
        Derecha
    }
    public partial class Form1 : Form
    {
        private const int Cabezafila = 22;
        private const int CabezaColumna = 22; // Tama�o del grid NxN
                                             // Tama�o del grid NxN
        private const int sizeCuadroImagen = 25; // Tama�o de cada PictureBox
        private MatrizListaEnlazada MatrizListaEnlazada;

        private Direction currentDirection;
        System.Windows.Forms.Timer moveTimer;
        private List<Bot> bots;
        System.Windows.Forms.Timer botMoveTimer;
        

        public Form1()
        {
            //InitializeComponent();
            InitializeComponent();
            //InitializeComponen;t();
            InitializarMatrizListaEnlazada();
                    

            currentDirection = Direction.Derecha; // Direcci�n inicial
            bots = new List<Bot>();
            InicializarBots(4);
            // Configurar el temporizador para mover la moto autom�ticamente
            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = 200; // Intervalo de movimiento en milisegundos
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();
            //Temporizador para los bots 
            botMoveTimer = new System.Windows.Forms.Timer();
            botMoveTimer.Interval = 250; 
            botMoveTimer.Tick += BotMoveTimer_Tick;
            botMoveTimer.Start();

            this.Paint += Form1_Paint;
        }
        private void BotMoveTimer_Tick(object sender,EventArgs e)
        {
            MoverBots();
        }
        private void MoverBots()
        {
            for(int i = bots.Count -1; i>0; i--)
            {
                Bot bot = bots[i];
                if (!VerificarColisiones(bot))
                {
                    bot.MoverBot();
                }
                else
                {
                    bots.RemoveAt(i);
                }
            }
            Invalidate();
        }
        private bool VerificarColisiones(ListaEnlazadaMoto moto)
        {
            MotoNodo cabeza = moto.Head;
            Node posicionActual = cabeza.Position;

            if (posicionActual == null || posicionActual.Arriba == null && currentDirection == Direction.Arriba ||
                posicionActual.Abajo == null && currentDirection == Direction.Abajo ||
                posicionActual.Izquierda == null && currentDirection == Direction.Izquierda ||
                posicionActual.Derecha == null && currentDirection == Direction.Derecha)
            {
                return true; // Colisión con los bordes
            }
            
            foreach(Bot bot in bots)
            {
                MotoNodo temp = bot.Head;
                while(temp != null)
                {
                    if(temp.Position == posicionActual)
                    {
                        return true;
                    }
                    temp = temp.Next;
                }
            }
            MotoNodo nodo = moto.Head.Next;
            while(nodo!= null)
            {
                if(nodo.Position == posicionActual)
                {
                    return true;
                }
                nodo = nodo.Next;
            }
            return false;

        }
        private void InicializarBots(int cantidad)
        {
            for( int i = 0; i < cantidad; i++)
            {
                Bot nuevoBot = new Bot();
                Node posicionInicial = ObtenerPosicionInicialBot(i);
                nuevoBot.Add(posicionInicial);
                bots.Add(nuevoBot);

            }
        }
        private Node ObtenerPosicionInicialBot(int botIndex)
        {
            switch(botIndex)
            {
                case 0: return MallaListaEnlazada.ObtenerNodoEn(0,0);//derecha
                case 1: return MallaListaEnlazada.ObtenerNodoEn(0,Cabezafila -1);//izquierda
                case 2: return MallaListaEnlazada.ObtenerNodoEn(CabezaColumna-1,0);//derecha abajo
                case 3: return MallaListaEnlazada.ObtenerNodoEn(CabezaColumna-1,Cabezafila-1);
                default: return MallaListaEnlazada.ObtenerNodoEn(0,0);

            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Dibuja la malla cuando se pinta la ventana
            DibujarMalla(e.Graphics);
        }
        private void DibujarMalla(Graphics g)
        {
            Node nodoActual = MallaListaEnlazada.NodoInicial;
            while(nodoActual != null)
            {
                Node nodoFila = nodoActual;
                while (nodoFila != null)
                {
                    g.DrawRectangle(Pens.Gray,nodoFila.CuadroImagen.Bounds);
                    nodoFila = nodoFila.Derecha;

                }
                nodoActual = nodoActual.Abajo;
            }
        }


        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            MoveMoto();
        }

        // Mover la moto en la direcci�n actual
        private void MoveMoto()
        {
            if (!VerificarColisiones(MallaListaEnlazada.Moto))
            {
                    
                MallaListaEnlazada.MoverMoto(currentDirection);
                Invalidate();
            }
            else
            {
                moveTimer.Stop();
                botMoveTimer.Stop();
                MessageBox.Show("Has colisionado, FIN DEL JUEGO");

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (currentDirection != Direction.Derecha)
                        currentDirection = Direction.Izquierda;
                    break;
                case Keys.Right:
                    if (currentDirection != Direction.Izquierda)
                        currentDirection = Direction.Derecha;
                    break;
                case Keys.Up:
                    if (currentDirection != Direction.Abajo)
                        currentDirection = Direction.Arriba;
                    break;
                case Keys.Down:
                    if (currentDirection != Direction.Arriba)
                        currentDirection = Direction.Abajo;
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        

        // Cambiar la direcci�n a la izquierda

          private void InitializarMallaListaEnlazada()
        {

    
            MallaListaEnlazada = new MallaListaEnlazada(Cabezafila, CabezaColumna, sizeCuadroImagen);
            Node nodoActual = MallaListaEnlazada.NodoInicial;
            while (nodoActual != null)
            {
                Node nodoFila = nodoActual;
                while (nodoFila != null)
                {
                    this.Controls.Add(nodoFila.CuadroImagen);
                    nodoFila = nodoFila.Derecha;
                }
                nodoActual = nodoActual.Abajo;
           
            }

        }
    }

    

    
}
