using Godot;
using Godot.Collections;
using System.Collections.Generic;

public class Card : Node2D
{
    bool isSelected = false;
    Array rest_nodes;
    Array target_nodes;
    Vector2? rest_point;
    DropZone currentDropZone;

    Sprite Sprite { get; set; }

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
        Sprite = GetNode("Sprite") as Sprite;

        switch (Value.ToLower())
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
                Sprite.Frame = 9;
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
        var positionX = this.GlobalPosition.x;
        var positionY = this.GlobalPosition.y;
        var newPosition = new Vector2(positionX, positionY).Normalized();
        var explosion = explosionScene.Instance() as Explosion;
        explosion.Position = this.Position;
        this.GetParent().AddChild(explosion);
        explosion._Ready();
        explosion.Connect("ExplosionComplete", this, "onExplosionComplete");
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
    public override void _PhysicsProcess(float delta)
    {
        if (isSelected)
        {
            var lerpX = Mathf.Lerp(this.GlobalPosition.x, this.GetGlobalMousePosition().x, 25 * delta);
            var lerpY = Mathf.Lerp(this.GlobalPosition.y, this.GetGlobalMousePosition().y, 25 * delta);
            if (lerpX == 0 || lerpY == 0)
                GD.Print("Gotcha!");


            this.GlobalPosition = new Vector2(lerpX, lerpY);


        }
        else
        {
            //GD.Print("Not Selected");

            var lerpX = Mathf.Lerp(this.GlobalPosition.x, rest_point?.x ?? 0, 10 * delta);
            var lerpY = Mathf.Lerp(this.GlobalPosition.y, rest_point?.y ?? 0, 10 * delta);
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

            if (me.ButtonIndex == (int)ButtonList.Left && !me.IsPressed())
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

                            
                            Sprite.Visible = false;
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
