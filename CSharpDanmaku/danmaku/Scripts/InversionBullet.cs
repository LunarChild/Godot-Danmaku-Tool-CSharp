using Godot;
using System;

public class InversionBullet : DefaultBullet
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private float _inversionTime=0f;
    private float inversionTrigger;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        inversionTrigger=3f;
        base._Ready();
    }

    public override void ExtraBehaviour(float delta)
    {
        Invert(delta);
        base.ExtraBehaviour(delta);
    }
    public void Invert(float delta){
        _inversionTime+=delta;
        if(_inversionTime>=inversionTrigger){
            _motionVector*=-1;
            _inversionTime=0;
        }
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
