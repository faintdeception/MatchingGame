using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class Card : Node2D
{
    bool isSelected = false;
    Array<Node> rest_nodes;
    Array<Node> target_nodes;
    Vector2? rest_point;
    DropZone currentDropZone;

    Sprite2D Sprite2D { get; set; }

    private PackedScene explosionScene;

    private Timer spawnTimer;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rest_nodes = GetTree().GetNodesInGroup("zone");
        target_nodes = GetTree().GetNodesInGroup("test_surface");
        //Find one that isn't occupied.
        foreach (var point in rest_nodes)
        {
            currentDropZone = point as DropZone;
            if (currentDropZone != null && !currentDropZone.IsOccupied)
            {
                rest_point = currentDropZone?.GlobalPosition;
                currentDropZone.Select();
                break;
            }
        }

        //Set Sprite
        Sprite2D = GetNode("Sprite2D") as Sprite2D;

        switch (Value.ToLower())
        {
            case "a":
                Sprite2D.Frame = 15;
                break;
            case "b":
                Sprite2D.Frame = 11;
                break;
            case "c":
                Sprite2D.Frame = 7;
                break;
            case "d":
                Sprite2D.Frame = 3;
                break;
            case "e":
                Sprite2D.Frame = 26;
                break;
            case "f":
                Sprite2D.Frame = 22;
                break;
            case "g":
                Sprite2D.Frame = 18;
                break;
            case "h":
                Sprite2D.Frame = 14;
                break;
            case "i":
                Sprite2D.Frame = 10;
                break;
            case "j":
                Sprite2D.Frame = 6;
                break;
            case "k":
                Sprite2D.Frame = 2;
                break;
            case "l":
                Sprite2D.Frame = 23;
                break;
            case "m":
                Sprite2D.Frame = 21;
                break;
            case "n":
                Sprite2D.Frame = 17;
                break;
            case "o":
                Sprite2D.Frame = 13;
                break;
            case "p":
                Sprite2D.Frame = 9;
                break;
            case "q":
                Sprite2D.Frame = 5;
                break;
            case "r":
                Sprite2D.Frame = 1;
                break;
            case "s":
                Sprite2D.Frame = 24;
                break;
            case "t":
                Sprite2D.Frame = 20;
                break;
            case "u":
                Sprite2D.Frame = 16;
                break;
            case "v":
                Sprite2D.Frame = 12;
                break;
            case "w":
                Sprite2D.Frame = 8;
                break;
            case "x":
                Sprite2D.Frame = 4;
                break;
            case "y":
                Sprite2D.Frame = 0;
                break;
            case "z":
                Sprite2D.Frame = 25;
                break;
            default:
                Sprite2D.Frame = 19;
                break;
        }



        //spawnTimer = GetNode<Timer>("SpawnTimer");
		//spawnTimer.Connect("timeout", this, "onSpawnTimeout");
        explosionScene = ResourceLoader.Load<PackedScene>("res://Explosion.tscn");
        //SpawnLasers();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void onExplosionComplete()
    {
        GetParent().GetTree().ReloadCurrentScene();
    }

    public void SpawnLasers()
    {
        spawnTimer.WaitTime = 3;
        spawnTimer.OneShot = false;
        spawnTimer.Start();
    }

    private void Explode()
    {
        Sprite2D.Visible = false;
        var positionX = this.GlobalPosition.X;
        var positionY = this.GlobalPosition.Y;
        var newPosition = new Vector2(positionX, positionY).Normalized();
        var explosion = explosionScene.Instantiate() as Explosion;
        explosion.Position = this.Position;
        this.GetParent().AddChild(explosion);
        explosion._Ready();
        explosion.Connect("ExplosionComplete", new Callable(this, "onExplosionComplete"));
    }

    public void _on_Area2D_input_event(Node n, InputEvent e, int idx)
    {
        var me = e as InputEventMouseButton;
        if (e.IsActionPressed("click"))
        {
            //if(me.IsPressed())
            GD.Print("Clicked!");
            isSelected = true;
            currentDropZone.Deselect();
            //}
        }
        else
        {
            //GD.Print("Other");
        }
    }

    // public override void _on
    
    [Export]
    public string Value { get; set; }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (isSelected)
        {
            var lerpX = (float)Mathf.Lerp(this.GlobalPosition.X, this.GetGlobalMousePosition().X, 25 * delta);
            var lerpY = (float)Mathf.Lerp(this.GlobalPosition.Y, this.GetGlobalMousePosition().Y, 25 * delta);
            if (lerpX == 0 || lerpY == 0)
                GD.Print("Gotcha!");


            this.GlobalPosition = new Vector2(lerpX, lerpY);


        }
        else
        {
            //GD.Print("Not Selected");

            var lerpX = (float)Mathf.Lerp(this.GlobalPosition.X, rest_point?.X ?? 0, 10 * delta);
            var lerpY = (float)Mathf.Lerp(this.GlobalPosition.Y, rest_point?.Y ?? 0, 10 * delta);
            if (this.GlobalPosition.DistanceTo(new Vector2(lerpX, lerpY)) == 0)
            {
                this.currentDropZone.Select();
            }

            this.GlobalPosition = new Vector2(lerpX, lerpY);
        }
    }

    public override void _Input(InputEvent e)
    {
        var me = e as InputEventMouseButton;        
        if (me != null)
        {   
            if (me.ButtonIndex == MouseButton.Left && !me.IsPressed())
            {
                GD.Print("Released");
                isSelected = false;
                var shortest_distance = 75f;

                foreach (DropZone child in target_nodes)
                {
                    var distance = GlobalPosition.DistanceTo(child.GlobalPosition);
                    if ((distance < shortest_distance) && !child.IsOccupied)
                    {
                        var matchingValue = child.GetParent<TestSurface>().MatchingValue;

                        if (Value == matchingValue)
                        {
                            child.Select();
                            rest_point = child.GlobalPosition;
                            shortest_distance = distance;

                            //Explode the card.
                            Explode();

                            //When the explosion completes, reload the scene.


                        }
                    }
                }

                foreach (DropZone child in rest_nodes)
                {
                    var distance = GlobalPosition.DistanceTo(child.GlobalPosition);
                    if ((distance < shortest_distance) && !child.IsOccupied)
                    {
                        child.Select();
                        rest_point = child.GlobalPosition;
                        shortest_distance = distance;

                    }
                }
            }
        }
    }
}
