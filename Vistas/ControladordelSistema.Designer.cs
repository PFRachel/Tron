namespace JuegoTron
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblItems;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms
        //generado por windows forms
        

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.lblItems = new Label();
            this.lblItems.AutoSize = true;
            this.lblItems.Location = new Point(10, 10); // Posición en la pantalla
            this.lblItems.Size = new Size(200, 50);
            this.lblItems.Font = new Font("Arial", 12);
            this.lblItems.ForeColor = Color.White;
            this.lblItems.Text = "Ítems: Ninguno";
            this.Controls.Add(this.lblItems);
            this.ClientSize = new System.Drawing.Size(650, 650);
            this.Name = "Form1";
            this.Text = "Juego Tron";
            this.BackColor = System.Drawing.Color.Gray;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
        }

        #endregion
    }
}



