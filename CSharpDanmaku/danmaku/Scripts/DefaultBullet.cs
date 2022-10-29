using Godot;
using System;

public class DefaultBullet : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private float _bulletSpeed = 0;
    private float _bulletLifeSpan = 0;
    private float _lifeTime = 0;
    private float _bulletRadius = 0;
    private bool _collided = false;
    private Vector2 _playerPosition=Vector2.Zero;
    protected Vector2 _motionVector;


    public override void _Ready()
    {
        //_motionVector = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
        _motionVector = new Vector2(Mathf.Cos(Rotation)*_bulletSpeed, Mathf.Sin(Rotation)*_bulletSpeed);
    }

    public void Init(Vector2 position, float angle, float speed, float lifeSpan)
    {
        Position = position;
        Rotation = angle;
        _bulletSpeed = speed;
        _bulletLifeSpan = lifeSpan;
        
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        ExtraBehaviour(delta);
        Move(delta);
        if(_bulletLifeSpan!=0){
            Age(delta);
        }
        if(_playerPosition!=Vector2.Zero){
            CollisionDetection(_playerPosition);
        }
    }


    public virtual void ExtraBehaviour(float delta)
    {
    }

    public void Move(float delta){
        var newPos=new Vector2(Position.x+_motionVector.x*delta,Position.y+_motionVector.y*delta);
        Position=newPos;
    }
    public void Age(float delta){
        _lifeTime+=delta;
        if(_lifeTime>_bulletLifeSpan){
            QueueFree();
        }
    }

    public void CollisionDetection(Vector2 playerVector){
        if(Position.DistanceTo(playerVector)<=_bulletRadius){
            _collided=true;
            QueueFree();
        }
    }

    public void SetPlayerPosition(Vector2 playerVector){
        _playerPosition=playerVector;
    }
}
