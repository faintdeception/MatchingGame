using Godot;
using System;

public partial class DropZone : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    public bool IsOccupied {get;set;}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Draw()
    {
        DrawCircle(Vector2.Zero, 75, new Color("blanchedalmond"));
        GD.Print("Drawn");
    }

    public void Select()
    {
        SelfModulate = new Color("webmaroon");
        IsOccupied = true;
        //GD.Print("Selected");
    }

    public void Deselect()
    {
        SelfModulate = new Color("white");
        IsOccupied = false;
        DrawCircle(Vector2.Zero, 9000, new Color("orange"));
        GD.Print("Deselected");
    }
}
