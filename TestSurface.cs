using Godot;

public partial class TestSurface : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public string MatchingValue {get;set;}

    Sprite2D Sprite2D{get;set;}
    AnimationTree AnimationTree { get; set; }
    AnimationNodeStateMachinePlayback StateMachine {get;set;}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Set Sprite
        Sprite2D = GetNode("Sprite2D") as Sprite2D;
        GD.Print($"Other matching value: {MatchingValue}");

        int frameIndex = 19; // Default frame index

        switch (MatchingValue.ToLower())
        {
            case "a":
                frameIndex = 15;
                break;
            case "b":
                frameIndex = 11;
                break;
            case "c":
                frameIndex = 7;
                break;
            case "d":
                frameIndex = 3;
                break;
            case "e":
                frameIndex = 26;
                break;
            case "f":
                frameIndex = 22;
                break;
            case "g":
                frameIndex = 18;
                break;
            case "h":
                frameIndex = 14;
                break;
            case "i":
                frameIndex = 10;
                break;
            case "j":
                frameIndex = 6;
                break;
            case "k":
                frameIndex = 2;
                break;
            case "l":
                frameIndex = 23;
                break;
            case "m":
                frameIndex = 21;
                break;
            case "n":
                frameIndex = 17;
                break;
            case "o":
                frameIndex = 13;
                break;
            case "p":
                frameIndex = 9;
                break;
            case "q":
                frameIndex = 5;
                break;
            case "r":
                frameIndex = 1;
                break;
            case "s":
                frameIndex = 24;
                break;
            case "t":
                frameIndex = 20;
                break;
            case "u":
                frameIndex = 16;
                break;
            case "v":
                frameIndex = 12;
                break;
            case "w":
                frameIndex = 8;
                break;
            case "x":
                frameIndex = 4;
                break;
            case "y":
                frameIndex = 0;
                break;
            case "z":
                frameIndex = 25;
                break;
        }

        // Ensure the frame index is within bounds
        int totalFrames = Sprite2D.Hframes * Sprite2D.Vframes;
        if (frameIndex >= 0 && frameIndex < totalFrames)
        {
            Sprite2D.Frame = frameIndex;
            GD.Print($"Frame index {frameIndex} set successfully.");
        }
        else
        {
            GD.PrintErr($"Frame index {frameIndex} is out of bounds. Total frames: {totalFrames}");
        }
        base._Ready();

    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
