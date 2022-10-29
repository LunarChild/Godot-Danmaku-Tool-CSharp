using Godot;
using System;

public class Player : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // public Vector2 GetPosition()
    // {
    //     return Position;
    // }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Position=GetGlobalMousePosition();
    }
}
