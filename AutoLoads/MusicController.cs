using Godot;
using System;

public partial class MusicController : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AudioStreamPlayer MusicPlayer;
    Resource backgroundMusic = GD.Load("res://Assets/Audio01_LOOP_NOIR.ogg");
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var mMusicPlayer = GetNode("MusicPlayer");// as AudioStreamPlayer;
        //PlayMusic();
    }

    public void PlayMusic()
    {
        MusicPlayer.Play();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
