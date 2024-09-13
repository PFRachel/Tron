using System.Collections.Generic;

namespace JuegoTron.Modelos.Items
{
    public class Item
    {
        public TipoItem Tipo { get; set; }
        public int Cantidad { get; set; } 
        public PictureBox CuadroItem { get; set; } 

        public Item(TipoItem tipo, Point location)
        {
            Tipo = tipo;
            CuadroItem = new PictureBox
            {
                Width = 50,
                Height = 50,
                Location = location,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = ObtenerColorPorTipo(tipo) 
            };
        }

        private Color ObtenerColorPorTipo(TipoItem tipo)
        {
            switch (tipo)
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
    }

    public enum TipoItem
    {
        Combustible,
        CrecimientoEstela,
        Bomba
    }

    public class ColaItems
    {
        private NodoItem frente; 
        private NodoItem final;  
        private int count;       

        public ColaItems()
        {
            frente = null;
            final = null;
            count = 0;
        }

        public void RecogerItem(Item item)
        {
            NodoItem nuevoNodo = new NodoItem(item);

            if (final == null) 
            {
                frente = nuevoNodo;
                final = nuevoNodo;
            }
            else
            {
                final.Next = nuevoNodo; 
                final = nuevoNodo;
            }

            count++;
        }

        public Item AplicarSiguienteItem()
        {
            if (frente != null) 
            {
                Item item = frente.Data;
                frente = frente.Next;

                if (frente == null) 
                {
                    final = null;
                }

                count--;
                return item;
            }

            return null; 
        }

        public void RotarItems()
        {
            if (frente != null && frente.Next != null) 
            {
                NodoItem primerItem = frente; 
                frente = frente.Next; 
                final.Next = primerItem; 
                final = primerItem;
                final.Next = null; 
            }
        }

        public List<Item> ObtenerItems()
        {
            List<Item> items = new List<Item>();
            NodoItem actual = frente;

            while (actual != null) 
            {
                items.Add(actual.Data);
                actual = actual.Next;
            }

            return items;
        }

        public int Count()
        {
            return count;
        }
    }

}

