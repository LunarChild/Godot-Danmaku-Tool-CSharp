using Godot;
using System;
using System.Collections.Generic;
public class Main : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export(PropertyHint.File)]
    public string _emitterPath = "";
    private PackedScene _Emitter;

    [Export(PropertyHint.File)]
    public string _tabPath = "";
    private PackedScene _Tab;

    private int _emitterCount = 0;
    private EditableEmitter emitterEditing;
    private int _tabCount = 0;
    private Dictionary<Tab, EditableEmitter> _tabToEmitter = new Dictionary<Tab, EditableEmitter>();
    private Dictionary<EditableEmitter, Tab> _emitterToTab = new Dictionary<EditableEmitter, Tab>();
    private bool _repositionEmitter = false;
    private bool _rotatingEmitter = false;

    private Player _player;
    private SceneTree _mainTree;
    private TabContainer _editor;
    private Vector2 _screenSize;


    private string inputSpawn = "mouse_left";
    private string inputAdjust = "mouse_right";
    private string inputRotate = "rotate";

    public override void _Ready()
    {
        _player = FindNode("Player") as Player;
        _mainTree = GetTree();
        _editor = FindNode("UI").GetNode<TabContainer>("Editor");
        _screenSize = OS.GetScreenSize();
        if (!string.IsNullOrEmpty(_emitterPath))
        {
            _Emitter = ResourceLoader.Load<PackedScene>(_emitterPath);
        }
        if (!string.IsNullOrEmpty(_tabPath))
        {
            _Tab = ResourceLoader.Load<PackedScene>(_tabPath);
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(inputSpawn))
        {
            //TODO
            var emitter=SpwanEmitter();
            var tab=SpwanEditor(emitter);
            //var tab=;
        }
        if (@event.IsActionReleased(inputAdjust))
        {
            _repositionEmitter = false;
            _rotatingEmitter = false;
        }
        if (@event.IsActionReleased(inputRotate))
        {
            _rotatingEmitter = false;
        }
    }


    public EditableEmitter SpwanEmitter()
    {
        var emitter = _Emitter.Instance<EditableEmitter>();
        _emitterCount++;
        emitter.Init(GetGlobalMousePosition(), string.Format("Default_Emitter_{0}", _emitterCount));
        this.AddChild(emitter);
        emitterEditing = emitter;
        return emitter;
    }

    public Tab SpwanEditor(EditableEmitter emitter)
    {

        if (_emitterCount <= 1)
        {
            _editor.Visible = true;
        }
        Tab tab = _Tab.Instance<Tab>();
        _editor.AddChild(tab);
        if (_tabToEmitter.ContainsKey(tab))
        {
            _tabToEmitter[tab] = emitter;
        }
        else
        {
            _tabToEmitter.Add(tab, emitter);
        }

        if (_emitterToTab.ContainsKey(emitter))
        {
            _emitterToTab[emitter] = tab;
        }
        else
        {
            _emitterToTab.Add(emitter, tab);
        }

        tab.Init(_tabCount, emitter.Name, emitter.Position, emitter.FireRate, emitter.VolleySize, emitter.ArrayCount, emitter.BulletSpeed, emitter.BulletLifeSpan);
        _editor.CurrentTab = _tabCount;
        _tabCount++;
        return tab;
    }

    public void RepositionEmitter(float delta)
    {
        var x = Mathf.Lerp(emitterEditing.Position.x, GetGlobalMousePosition().x, 25 * delta);
        var y = Mathf.Lerp(emitterEditing.Position.y, GetGlobalMousePosition().y, 25 * delta);
        x = Mathf.Clamp(x, 0, _screenSize.x);
        y = Mathf.Clamp(y, 0, _screenSize.y);
        emitterEditing.Position = new Vector2(x, y);
        _emitterToTab[emitterEditing].SetPositionField(emitterEditing.Position);
    }

    public void RotateEmitter()
    {
        emitterEditing.SetAngle(GetGlobalMousePosition());
        _emitterToTab[emitterEditing].SetAngleField(emitterEditing.Rotation);
    }

    public void AdjustmentInput(EditableEmitter emitter, InputEvent @event)
    {
        if (@event.IsActionPressed(inputAdjust))
        {
            _repositionEmitter = true;
            emitterEditing = emitter;
            _editor.CurrentTab = int.Parse(_emitterToTab[emitter].Name);
            if (Input.IsActionPressed(inputRotate))
            {
                _repositionEmitter = false;
                _rotatingEmitter = true;
            }
        }
    }

    #region 更新字段
    public void UpdateName(Tab tab, string value)
    {
        _tabToEmitter[tab].Name = value;
    }

    public void UpdatePositionX(Tab tab, float value)
    {
        _tabToEmitter[tab].SetPositionX(value);
    }
    public void UpdatePositionY(Tab tab, float value)
    {
        _tabToEmitter[tab].SetPositionY(value);
    }
    public void UpdateAngle(Tab tab, float value)
    {
        _tabToEmitter[tab].SetAngle(value);
    }

    public void UpdateFireRate(Tab tab, float value)
    {
        _tabToEmitter[tab].FireRate = value;
    }
    public void UpdateClipSize(Tab tab, float value)
    {
        _tabToEmitter[tab].ClipSize = value;
    }
    public void UpdateReloadTime(Tab tab, float value)
    {
        _tabToEmitter[tab].ReloadTime = value;
    }
    public void UpdateAngularVelocity(Tab tab, float value)
    {
        _tabToEmitter[tab].AngularVelocity = value;
    }
    public void UpdateAngularAcceleration(Tab tab, float value)
    {
        _tabToEmitter[tab].AngularAcceleration = value;
    }
    public void UpdateMaxAngularVelocity(Tab tab, float value)
    {
        _tabToEmitter[tab].MaxAngularVelocity = value;
    }
    public void UpdateVolleySize(Tab tab, int value)
    {
        _tabToEmitter[tab].VolleySize = value;
    }
    public void UpdateSpreadAngle(Tab tab, float value)
    {
        _tabToEmitter[tab].SpreadAngle = value;
    }
    public void UpdateSpreadWidth(Tab tab, float value)
    {
        _tabToEmitter[tab].SpreadWidth = value;
    }
    public void UpdateArrayCount(Tab tab, int value)
    {
        _tabToEmitter[tab].ArrayCount = value;
    }
    public void UpdateArrayAngle(Tab tab, float value)
    {
        _tabToEmitter[tab].ArrayAngle = value;
    }
    public void UpdateAimEnable(Tab tab, bool value)
    {
        _tabToEmitter[tab].AimEnabled = value;
    }
    public void UpdateAimPause(Tab tab, int value)
    {
        _tabToEmitter[tab].AimPause = value;
    }
    public void UpdateAimOffset(Tab tab, float value)
    {
        _tabToEmitter[tab].AimOffset = value;
    }
    public void UpdateBullet(Tab tab, string value)
    {
        _tabToEmitter[tab].SetBullet(value);
    }
    public void UpdateBulletSpeed(Tab tab, float value)
    {
        _tabToEmitter[tab].BulletSpeed = value;
    }
    public void UpdateBulletLifeSpan(Tab tab, float value)
    {
        _tabToEmitter[tab].BulletLifeSpan = value;
    }
    #endregion


    public void LoadSelected(Tab tab,string path){
        var emitter=_tabToEmitter[tab];
        emitter.LoadEmitter(path);
        tab.SetNameField(emitter.Name);
        tab.SetPositionField(emitter.Position);
        tab.SetAngleField(emitter.Rotation);
        tab.SetFireRateField(emitter.FireRate);
        tab.SetClipSizeField(emitter.ClipSize);
        tab.SetReloadTimeField(emitter.ReloadTime);
        tab.SetAngularVelocityField(emitter.AngularVelocity);
        tab.SetAngularAccelerationField(emitter.AngularAcceleration);
        tab.SetMaxAngularVelocityField(emitter.MaxAngularVelocity);
        tab.SetVolleySizeField(emitter.VolleySize);
        tab.SetSpreadAngleField(emitter.SpreadAngle);
        tab.SetSpreadWidthField(emitter.SpreadWidth);
        tab.SetArrayCountField(emitter.ArrayCount);
        tab.SetArrayAngleField(emitter.ArrayAngle);
        tab.SetAimEnabledField(emitter.AimEnabled);
        tab.SetAimPauseField(emitter.AimPause);
        tab.SetAimOffsetField(emitter.AimOffset);
        tab.SetBulletSpeedField(emitter.BulletSpeed);
        tab.SetBulletLifeSpanField(emitter.BulletLifeSpan);
    }

    public void SaveFile(Tab tab,string path){
        _tabToEmitter[tab].Save(path);
    }

    public void DeleteEmitter(Tab tab){
        _tabToEmitter[tab].QueueFree();
        _editor.SetTabHidden(int.Parse(tab.Name),true);
    }
     // Called every frame. 'delta' is the elapsed time since the previous frame.
     public override void _Process(float delta)
     {
         if(_repositionEmitter){
            RepositionEmitter(delta);
         }
         if(_rotatingEmitter){
            RotateEmitter();
         }
         _mainTree.CallGroup("bullets","SetPlayerPosition",_player.Position);
     }


}
