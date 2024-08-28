using System;
using System.Windows.Forms;
using System.Drawing;
//dibujar la maya 
namespace JuegoTron
{
    public partial class Form1 : Form
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

        public Form1()
        {
            //InitializeComponent();
            InitializeComponent();
            InitializarMatrizListaEnlazada();
                        //InitializeLinkedListGrid();
            this.Controls.AddRange(MatrizListaEnlazada.Matriz.Cast<Node>().Select(n => n.CuadroImagen).ToArray());

            currentDirection = Direction.Derecha; // Direcci�n inicial

            // Configurar el temporizador para mover la moto autom�ticamente
            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = 200; // Intervalo de movimiento en milisegundos
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Dibuja la malla cuando se pinta la ventana
            //malla.Dibujar(e.Graphics);
        }


        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            MoveMoto();
        }

        // Mover la moto en la direcci�n actual
        private void MoveMoto()
        {
            MatrizListaEnlazada.MoverMoto(currentDirection);
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
                    if (currentDirection != Direction.Arriba)
                        currentDirection = Direction.Abajo;
                    break;
                case Keys.Down:
                    if (currentDirection != Direction.Arriba)
                        currentDirection = Direction.Abajo;
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        

        // Cambiar la direcci�n a la izquierda

        private void InitializarMatrizListaEnlazada()
        {

    
            MatrizListaEnlazada = new MatrizListaEnlazada(Cabezafila, CabezaColumna, sizeCuadroImagen);
  
            // Agregar cada PictureBox al formulario
            foreach (var node in MatrizListaEnlazada.Matriz)
            {
                this.Controls.Add(node.CuadroImagen);
            }
        }
    }

    }

    
}
