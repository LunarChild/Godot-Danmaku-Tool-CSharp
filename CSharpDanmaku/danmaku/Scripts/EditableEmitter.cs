using Godot;
using System;
using System.Collections.Generic;
public class EditableEmitter : AbstractEmitter
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private Vector2 _screenSize=OS.GetScreenSize();

    public override void _Ready()
    {
        base._Ready();
    }

    public void Save(string filePath){
        var file=new Godot.File();
        file.Open(filePath, File.ModeFlags.Write);
        Dictionary<string,object> saveData=new Dictionary<string, object>();
        saveData.Add("Name",Name);
        saveData.Add("Position",Position);
        saveData.Add("Rotation",Rotation);
        saveData.Add("FireRate",FireRate);
        saveData.Add("ClipSize",ClipSize);
        saveData.Add("ReloadTime",ReloadTime);
        saveData.Add("AngularVelocity",AngularVelocity);
        saveData.Add("AngularAcceleration",AngularAcceleration);
        saveData.Add("MaxAngularVelocity",MaxAngularVelocity);
        saveData.Add("VolleySize",VolleySize);
        saveData.Add("SpreadAngle",SpreadAngle);
        saveData.Add("SpreadWidth",SpreadWidth);
        saveData.Add("ArrayCount",ArrayCount);
        saveData.Add("ArrayAngle",ArrayAngle);
        saveData.Add("AimEnabled",AimEnabled);
        saveData.Add("AimPause",AimPause);
        saveData.Add("AimOffset",AimOffset);
        saveData.Add("_bulletAddress",_bulletAddress);
        saveData.Add("_bulletSpeed",BulletSpeed);
        saveData.Add("_bulletLifeSpan",BulletLifeSpan);
        file.StoreLine(JSON.Print(saveData));
        file.Close();
    }

    public void Init(Vector2 pos,string name){
        Position=pos;
        Name=name;
    }

    public void OnInputEvent(Node viewport,InputEvent inputEvent,int shape_idx){
        controller.AdjustmentInput(this,inputEvent);
    }
    public override void BoundHandler(){
        var x=Mathf.Wrap(Position.x,-1,_screenSize.x+1);
        var y=Mathf.Wrap(Position.y,-1,_screenSize.y+1);
        Position=new Vector2(x,y);
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
