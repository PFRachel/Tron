using JuegoTron.Modelos.Items;

public class NodoItem
{
    public Item Data { get; set; } // El �tem que contiene el nodo
    public NodoItem Next { get; set; } // El siguiente nodo en la cola

    public NodoItem(Item data)
    {
        Data = data;
        Next = null;
    }
}