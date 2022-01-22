using Godot;
using System;

public class Explosion : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private CPUParticles2D particles;

    [Signal]
    public delegate void ExplosionComplete();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        particles = GetNode<CPUParticles2D>("CPUParticles2D");
        particles.Emitting = true;
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (!particles.Emitting)
        {
            this.EmitSignal(nameof(ExplosionComplete));
            QueueFree();
        }
    }
}
