using Godot;
using System;

public partial class Explosion : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private CpuParticles2D particles;

    [Signal]
    public delegate void ExplosionCompleteEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        particles = GetNode<CpuParticles2D>("CPUParticles2D");
        particles.Emitting = true;
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!particles.Emitting)
        {
            this.EmitSignal(nameof(ExplosionCompleteEventHandler));
            QueueFree();
        }
    }
}
