using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using JuegoTron.Modelos.Items;
using TRONversion1.Modelos.Moto.Poderes;
using Timer = System.Windows.Forms.Timer;

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
        private MallaListaEnlazada MallaListaEnlazada;

        private Direction currentDirection;
        private ColaItems colaItems = new ColaItems(); // Cola de ítems para la moto
        private bool escudoActivo = false; 
        private Timer escudoTimer;
        private Timer hiperVelocidadTimer;
        System.Windows.Forms.Timer moveTimer;
        private List<Bot> bots; // Lista para almacenar los bots
        System.Windows.Forms.Timer botMoveTimer;
        private List<Item> listaDeItems = new List<Item>();
        private Label lblCombustible;
        private int celdasRecorridas;
        private Label lblPoderActivo;
        private PilaPoderes pilaPoderes = new PilaPoderes();
        private Label lblPoderes;
        private List<Tuple<Poder, PictureBox>> listaDePoderes = new List<Tuple<Poder, PictureBox>>();
        private Timer itemTimer;
        
        

        public Form1()
        {
            try
            {
                InitializeComponent();
                InitializarMallaListaEnlazada();
                bots = new List<Bot>();
                InicializarBots(4);
                GenerarPoderesAleatorios(4);
                GenerarItemsAlatorios(5);

                // Temporizadores para mover la moto y los bots
                moveTimer = new System.Windows.Forms.Timer();
                moveTimer.Interval = 200;
                moveTimer.Tick += MoveTimer_Tick;
                moveTimer.Start();

                this.lblPoderes = new Label();
                this.lblPoderes.AutoSize = true;
                this.lblPoderes.Location = new Point(10, 80);
                this.lblPoderes.Size = new Size(200, 50);
                this.lblPoderes.Font = new Font("Arial", 12);
                this.lblPoderes.ForeColor = Color.White;
                this.lblPoderes.Text = "Poderes: Ninguno";
                this.Controls.Add(lblPoderes);
                lblPoderes.BringToFront();

                this.lblPoderActivo = new Label();
                this.lblPoderActivo.AutoSize = true;
                this.lblPoderActivo.Location = new Point(10, 110);
                this.lblPoderActivo.Size = new Size(200, 50);
                this.lblPoderActivo.Font = new Font("Arial", 12);
                this.lblPoderActivo.ForeColor = Color.White;
                this.lblPoderActivo.BackColor = Color.Green;
                this.lblPoderActivo.Text = "";
                this.Controls.Add(lblPoderActivo);
                lblPoderActivo.BringToFront();

                this.lblCombustible = new Label();
                this.lblCombustible.AutoSize = true;
                this.lblCombustible.Location = new Point(10, 50); // Posición en la pantalla, debajo de lblItems
                this.lblCombustible.Size = new Size(200, 50);
                this.lblCombustible.Font = new Font("Arial", 12);
                this.lblCombustible.ForeColor = Color.White;
                this.lblCombustible.Text = "Combustible: 100";
                this.Controls.Add(lblCombustible);
                lblCombustible.BringToFront();

                celdasRecorridas = 0;

                MallaListaEnlazada.Moto.Combustible = 100;
                MallaListaEnlazada.Moto.CurrentDirection = Direction.Derecha;

                botMoveTimer = new System.Windows.Forms.Timer();
                botMoveTimer.Interval = 250;
                botMoveTimer.Tick += BotMoveTimer_Tick;
                botMoveTimer.Start();

                this.Paint += Form1_Paint;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error al inicializar el formulario: {ex.Message}", "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BotMoveTimer_Tick(object sender,EventArgs e)
        {
            try
            {
                MoverBots();
            }
            catch(Exception ex)
            {
                 MessageBox.Show($"Error al mover los bots: {ex.Message}", "Error en Movimiento de Bots", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            MoverBots();
        }
        private void MoverBots()
        {
            for(int i = bots.Count -1; i>0; i--)
            {
                Bot bot = bots[i];
                try
                {
                    VerificarColisiones(bot);
                    VerificarColisionConItems(bot);
                    if(bot.Vivo)
                    {
                        bot.MoverBot();
                    }
                    else
                    {
                        bots.RemoveAt(i);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al mover un bot: {ex.Message}", "Error en Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bot.Vivo = false; // Marcar el bot como muerto en caso de error
                    bots.RemoveAt(i); // Elimina el bot de la lista
                    
                }
            }
            Invalidate(); // Redibujar la pantalla
        }
        private bool VerificarColisiones(ListaEnlazadaMoto moto)
        {
          try
            {
                if (!moto.Vivo || escudoActivo) return; 

                MotoNodo cabeza = moto.Head;
                Node posicionActual = cabeza.Position;

                if (posicionActual == null ||
                    (posicionActual.Arriba == null && moto.CurrentDirection == Direction.Arriba) ||
                    (posicionActual.Abajo == null && moto.CurrentDirection == Direction.Abajo) ||
                    (posicionActual.Izquierda == null && moto.CurrentDirection == Direction.Izquierda) ||
                    (posicionActual.Derecha == null && moto.CurrentDirection == Direction.Derecha))
                {
                    moto.Vivo = false;
                    DesaperecerMoto(moto);
                    return;
                }

                if (VerificarColisionConEstela(MallaListaEnlazada.Moto, posicionActual))
                {
                    moto.Vivo = false;
                    DesaperecerMoto(moto);

                    return;
                }

                foreach (Bot bot in bots)
                {
                    if (VerificarColisionConEstela(bot, posicionActual))
                    {
                        moto.Vivo = false;
                        DesaperecerMoto(moto);

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar colisiones: {ex.Message}", "Error de Colisión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                moto.Vivo = false; 
            }
        }
        private void DesaperecerMoto(ListaEnlazadaMoto moto)
        {
            MotoNodo actualNodo = moto.Head;

            while (actualNodo != null)
            {
                
                actualNodo.GridNode.CuadroImagen.BackColor = Color.Black;
                actualNodo = actualNodo.Next;
            }
            GenerarPoderesAleatorios(2);
            GenerarItemsAleatorios(1);
            this.Invalidate();
        }
        private bool VerificarColisionConEstela(ListaEnlazadaMoto moto, Node posicionActual)
        {
            MotoNodo actualNodo = moto.Head.Next;

            while (actualNodo != null)
            {
                if (actualNodo.Position == posicionActual)
                {
                    return true; 
                }
                actualNodo = actualNodo.Next;
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
            if (MallaListaEnlazada.Moto.Combustible > 0)
            {
                VerificarColisiones(MallaListaEnlazada.Moto);
                if (MallaListaEnlazada.Moto.Vivo)
                {
                    MallaListaEnlazada.MoverMoto(MallaListaEnlazada.Moto.CurrentDirection);
                    VerificarColisionConItems(MallaListaEnlazada.Moto);
                    VerificarColisionConPoderes();
                    celdasRecorridas++;

                    if (celdasRecorridas >= 5)
                    {
                        celdasRecorridas = 0;
                        MallaListaEnlazada.Moto.Combustible -= 1;
                        lblCombustible.Text = $"Combustible: {MallaListaEnlazada.Moto.Combustible}";
                    }

                    if (MallaListaEnlazada.Moto.Combustible == 0)
                    {
                        MessageBox.Show("Se acabó el combustible, ¡has perdido!");
                        moveTimer.Stop(); // Detener el movimiento de la moto
                        botMoveTimer.Stop(); // Detener el movimiento de los bots
                    }

                    Invalidate(); // Redibujar el formulario
                }
                else
                {
                    moveTimer.Stop();
                    botMoveTimer.Stop();
                    MessageBox.Show("Has colisionado, FIN DEL JUEGO");
                }
            }
        }
        private void RecargarCombustible(int cantidad, ListaEnlazadaMoto moto)
        {
            MallaListaEnlazada.Moto.Combustible += cantidad;

            if (MallaListaEnlazada.Moto.Combustible > 100)
            {
                MallaListaEnlazada.Moto.Combustible = 100;
            }

            lblCombustible.Text = $"Combustible: {MallaListaEnlazada.Moto.Combustible}";
        }
        private void DestruirMoto(ListaEnlazadaMoto moto)
        {
            if (moto is Bot bot)
            {
                // Si la moto es un bot, elimínalo de la lista de bots
                DesaperecerMoto(moto);
                bots.Remove(bot);
            }
            else if (moto == MallaListaEnlazada.Moto)
            {
                // Si es la moto principal del jugador, detén el juego
                moveTimer.Stop(); // Detener el movimiento de la moto
                botMoveTimer.Stop(); // Detener el movimiento de los bots
                MessageBox.Show("La moto ha sido destruida.");
            }
        }
        private void VerificarColisionConItems(ListaEnlazadaMoto moto)
        {
            Node posicionMoto = moto.Head.GridNode;

            foreach (var item in listaDeItems.ToList()) // Iterar sobre una copia de la lista
            {
                if (item.CuadroItem.Bounds.IntersectsWith(posicionMoto.CuadroImagen.Bounds))
                {
                    AplicarPoderItem(item, moto); // Aplicar el poder del ítem

                    this.Controls.Remove(item.CuadroItem); // Eliminar el ítem de la pantalla
                    listaDeItems.Remove(item); // Eliminar el ítem de la lista
                }
            }
        }
        private void AplicarPoderItem(Item item, ListaEnlazadaMoto moto)
        {
            switch (item.Tipo)
            {
                case TipoItem.Combustible:
                    RecargarCombustible(100, moto); 
                    break;

                case TipoItem.CrecimientoEstela:
                    AumentarEstela(1, moto);  
                    break;

                case TipoItem.Bomba:
                    DestruirMoto(moto); 
                    break;
                default:
                    MessageBox.Show("¡Poder desconocido!");
                    break;
            }
        }
        private void AumentarEstela(int cantidad, ListaEnlazadaMoto moto)
        {
            MallaListaEnlazada.Moto.AumentarEstela(cantidad); 
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (MallaListaEnlazada.Moto.CurrentDirection != Direction.Derecha) MallaListaEnlazada.Moto.CurrentDirection = Direction.Izquierda;
                    break;
                case Keys.Right:
                    if (MallaListaEnlazada.Moto.CurrentDirection != Direction.Izquierda) MallaListaEnlazada.Moto.CurrentDirection = Direction.Derecha;
                    break;
                case Keys.Up:
                    if (MallaListaEnlazada.Moto.CurrentDirection != Direction.Abajo) MallaListaEnlazada.Moto.CurrentDirection = Direction.Arriba;
                    break;
                case Keys.Down:
                    if (MallaListaEnlazada.Moto.CurrentDirection != Direction.Arriba) MallaListaEnlazada.Moto.CurrentDirection = Direction.Abajo;
                    break;
                case Keys.E:
                    
                        AplicarPoder(); 
                    
                    break;
                case Keys.Q:
                    RotarPoder();
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
        private void ActivarEscudo()
        {
            if (!escudoActivo)
            {
                escudoActivo = true;
                lblPoderActivo.Text = "Escudo activo";
                escudoTimer = new Timer();
                escudoTimer.Interval = 10000; 
                escudoTimer.Tick += (s, e) => DesactivarEscudo();
                escudoTimer.Start();
            }
        }
        private void DesactivarEscudo()
        {
            escudoActivo = false;
            escudoTimer.Stop();
            lblPoderActivo.Text = "";

        }
        private void ActivarHiperVelocidad(int duracion)
        {

            moveTimer.Interval = 30;
            lblPoderActivo.Text = "HiperVelocidad activada";
            hiperVelocidadTimer = new Timer();
            hiperVelocidadTimer.Interval = duracion * 1000; 
            hiperVelocidadTimer.Tick += (s, e) => DesactivarHiperVelocidad();
            hiperVelocidadTimer.Start();

        }
        private void DesactivarHiperVelocidad()
        {
            lblPoderActivo.Text = "";
            moveTimer.Interval = 200; 
            hiperVelocidadTimer.Stop();
        }

        private void ApilarPoder(Poder poder)
        {
            pilaPoderes.ApilarPoder(poder);
            ActualizarEtiquetaPoderes();
        }
        private void AplicarPoder()
        {
            Poder poder = pilaPoderes.DesapilarPoder();
            if (poder != null)
            {
                switch (poder.Tipo)
                {
                    case TipoPoder.Escudo:
                        ActivarEscudo();
                        break;
                    case TipoPoder.HiperVelocidad:
                        ActivarHiperVelocidad(3);
                        break;
                }
                ActualizarEtiquetaPoderes();
            }
            
        }
        private void ActualizarEtiquetaPoderes()
        {
            List<Poder> poderes = pilaPoderes.ObtenerPoderes();
            if (poderes.Count > 0)
            {
                string textoPoderes = "Poderes: ";
                foreach (var poder in poderes)
                {
                    textoPoderes += poder.Tipo.ToString() + " ";
                }
                lblPoderes.Text = textoPoderes;
            }
            else
            {
                lblPoderes.Text = "Poderes: Ninguno";
            }
        }
        private void GenerarItemsAleatorios(int cantidadItems)
        {
            Random random = new Random();

            for (int i = 0; i < cantidadItems; i++)
            {
                int filaAleatoria = random.Next(0, Cabezafila);
                int columnaAleatoria = random.Next(0, CabezaColumna);

                Node nodoAleatorio = MallaListaEnlazada.ObtenerNodoEn(filaAleatoria, columnaAleatoria);
                Point posicionItem = nodoAleatorio.CuadroImagen.Location;

                TipoItem tipoItemAleatorio = (TipoItem)random.Next(0, 3); 
                Item nuevoItem = new Item(tipoItemAleatorio, posicionItem);

                PictureBox cuadroItem = new PictureBox
                {
                    Width = 25,
                    Height = 25,
                    Location = posicionItem,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = ObtenerColorPorTipo(nuevoItem.Tipo) 
                };

                this.Controls.Add(cuadroItem);
                cuadroItem.BringToFront(); 

                nuevoItem.CuadroItem = cuadroItem;

                listaDeItems.Add(nuevoItem);
            }

            this.Invalidate(); 
        }

        private Color ObtenerColorPorTipo(TipoItem tipoItem)
        {
            switch (tipoItem)
            {
                case TipoItem.Combustible:
                    return Color.Green;
                case TipoItem.CrecimientoEstela:
                    return Color.Blue;
                case TipoItem.Bomba:
                    return Color.Red;
                default:
                    return Color.Gray;
            }
        }
        private void RotarPoder()
        {
            pilaPoderes.RotarPoder(); 
            ActualizarEtiquetaPoderes();
        }
        private void GenerarPoderesAleatorios(int cantidadPoderes)
        {
            Random random = new Random();

            for (int i = 0; i < cantidadPoderes; i++)
            {
                int filaAleatoria = random.Next(0, Cabezafila);
                int columnaAleatoria = random.Next(0, CabezaColumna);

                Node nodoAleatorio = MallaListaEnlazada.ObtenerNodoEn(filaAleatoria, columnaAleatoria);
                Point posicionPoder = nodoAleatorio.CuadroImagen.Location;

                TipoPoder tipoPoderAleatorio = (TipoPoder)random.Next(0, 2); 
                Poder nuevoPoder = new Poder(tipoPoderAleatorio, 100); 

                PictureBox cuadroPoder = new PictureBox
                {
                    Width = 50,
                    Height = 50,
                    Location = posicionPoder,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = ObtenerColorPorTipoPoder(tipoPoderAleatorio) 
                };

                this.Controls.Add(cuadroPoder);
                cuadroPoder.BringToFront(); 

                listaDePoderes.Add(new Tuple<Poder, PictureBox>(nuevoPoder, cuadroPoder));
            }

            this.Invalidate();
        }

        // Método para asignar colores según el tipo de poder
        private Color ObtenerColorPorTipoPoder(TipoPoder tipoPoder)
        {
            switch (tipoPoder)
            {
               
                case TipoPoder.Escudo:
                    return Color.Blue;
                case TipoPoder.HiperVelocidad:
                    return Color.Yellow;
                default:
                    return Color.Gray;
            }
        }
        private void VerificarColisionConPoderes()
        {
            Node posicionMoto = MallaListaEnlazada.Moto.Head.GridNode;

            foreach (var poderInfo in listaDePoderes.ToList()) 
            {
                Poder poder = poderInfo.Item1; 
                PictureBox cuadroPoder = poderInfo.Item2; 

                if (cuadroPoder.Bounds.IntersectsWith(posicionMoto.CuadroImagen.Bounds))
                {
                    ApilarPoder(poder);

                    this.Controls.Remove(cuadroPoder);
                    listaDePoderes.Remove(poderInfo); 
                }
            }
        }

    }    
}
