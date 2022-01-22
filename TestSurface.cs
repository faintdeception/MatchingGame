using Godot;
using System;

public class TestSurface : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public string MatchingValue {get;set;}

    Sprite Sprite{get;set;}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set Sprite
        Sprite = GetNode("Sprite") as Sprite;

        switch (MatchingValue.ToLower())
        {
            case "a":
            Sprite.Frame = 15;
            break;
            case "b":
            Sprite.Frame = 11;
            break;
            case "c":
            Sprite.Frame = 7;
            break;
            case "d":
            Sprite.Frame = 3;
            break;
            case "e":
            Sprite.Frame = 26;
            break;
            case "f":
            Sprite.Frame = 22;
            break;
            case "g":
            Sprite.Frame = 18;
            break;
            case "h":
            Sprite.Frame = 14;
            break;
            case "i":
            Sprite.Frame = 10;
            break;
            case "j":
            Sprite.Frame = 6;
            break;
            case "k":
            Sprite.Frame = 2;
            break;
            case "l":
            Sprite.Frame = 23;
            break;
            case "m":
            Sprite.Frame = 21;
            break;
            case "n":
            Sprite.Frame = 17;
            break;
            case "o":
            Sprite.Frame = 13;
            break;
            case "p":
            Sprite.Frame = 3;
            break;
            case "q":
            Sprite.Frame = 5;
            break;
            case "r":
            Sprite.Frame = 1;
            break;
            case "s":
            Sprite.Frame = 24;
            break;
            case "t":
            Sprite.Frame = 20;
            break;
            case "u":
            Sprite.Frame = 16;
            break;
            case "v":
            Sprite.Frame = 12;
            break;
            case "w":
            Sprite.Frame = 8;
            break;
            case "x":
            Sprite.Frame = 4;
            break;
            case "y":
            Sprite.Frame = 0;
            break;
            case "z":
            Sprite.Frame = 25;
            break;
            default:
            Sprite.Frame = 19;
            break;
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
