using Godot;
using System;

public class DropZone : Node2D
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
        DrawCircle(Vector2.Zero, 75, Color.ColorN("blanchedalmond"));
    }

    public void Select()
    {
        // foreach (var child in GetTree().GetNodesInGroup("zone"))
        // {
        //     Deselect(child as Node2D);
            
        // }
        Modulate = Color.ColorN("webmaroon");
        IsOccupied = true;
    }

    public void Deselect()
    {
        Modulate = Color.ColorN("white");
        IsOccupied = false;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
