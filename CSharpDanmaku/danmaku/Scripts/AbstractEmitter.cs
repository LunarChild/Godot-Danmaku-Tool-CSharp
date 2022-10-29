using Godot;
using System;
using System.Collections.Generic;


public class AbstractEmitter : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private PackedScene _bullet = ResourceLoader.Load<PackedScene>("res://danmaku/Scenes/ProvidedBullets/Bullet.tscn");

    private float _fireRate = 0.2f;
    private float _clipSize = 0f;
    private float _reloadTime = 0f;

    private float _angularVelocity = 0f;
    private float _angularAcceleration = 0f;
    private float _maxAngularVelocity = 0f;

    private int _volleySize = 1;
    private float _spreadAngle = 0f;
    private float _spreadWidth = 0f;

    private int _arrayCount = 1;
    private float _arrayAngle = 0;

    private bool _aimEnabled = false;
    private int _aimPause = 0;
    private float _aimOffset = 0f;

    protected string _bulletAddress = "res://danmaku/Scenes/ProvidedBullets/Bullet.tscn";
    private float _bulletSpeed = 100f;
    private float _bulletLifeSpan = 0f;


    protected Player player;
    protected Main controller; //Main class
    protected float aimTimer = 0f;
    protected float shotTimer = 0f;
    protected float reloadTimer = 0f;
    protected int shotCount = 0;

    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float ClipSize { get => _clipSize; set => _clipSize = value; }
    public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    public float AngularVelocity { get => _angularVelocity; set => _angularVelocity = value; }
    public float AngularAcceleration { get => _angularAcceleration; set => _angularAcceleration = value; }
    public float MaxAngularVelocity { get => _maxAngularVelocity; set => _maxAngularVelocity = value; }
    public int VolleySize { get => _volleySize; set => _volleySize = value; }
    public float SpreadAngle { get => _spreadAngle; set => _spreadAngle = value; }
    public float SpreadWidth { get => _spreadWidth; set => _spreadWidth = value; }
    public int ArrayCount { get => _arrayCount; set => _arrayCount = value; }
    public float ArrayAngle { get => _arrayAngle; set => _arrayAngle = value; }
    public bool AimEnabled { get => _aimEnabled; set => _aimEnabled = value; }
    public int AimPause { get => _aimPause; set => _aimPause = value; }
    public float AimOffset { get => _aimOffset; set => _aimOffset = value; }
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
    public float BulletLifeSpan { get => _bulletLifeSpan; set => _bulletLifeSpan = value; }

    public override void _Ready()
    {
        player = GetParent().FindNode("Player") as Player;
        controller = GetTree().Root.GetChild<Main>(0);
    }


    #region 虚函数
    public virtual void Move()
    {

    }
    public virtual void BoundHandler()
    {

    }
    #endregion

    #region Helper
    public void Aim(float delta, Vector2 playerPosition)
    {
        aimTimer += delta;
        if (aimTimer >= AimPause)
        {
            LookAt(playerPosition);
            Rotation += AimOffset;
            aimTimer = 0;
        }
    }

    public void Rotating(float delta)
    {
        if (!(AngularVelocity == 0 && AngularAcceleration == 0))
        {
            Rotation += AccelerateRotation(delta);
            if (RotationDegrees >= 360 || RotationDegrees <= -360)
            {
                RotationDegrees = 0;
            }
        }
    }

    public float AccelerateRotation(float delta)
    {
        AngularVelocity += AngularAcceleration * delta;
        if (Mathf.Abs(AngularVelocity) > Mathf.Abs(MaxAngularVelocity) && MaxAngularVelocity != 0)
        {
            AngularAcceleration *= -1;
        }
        return AngularVelocity;
    }

    public bool CoolDown(float delta)
    {
        shotTimer += delta;
        if (shotTimer >= FireRate)
        {
            shotTimer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Reload(float delta)
    {
        reloadTimer -= delta;
        if (reloadTimer <= 0)
        {
            return true;
        }
        else { return false; }
    }

    public void Shoot()
    {
        for (int array = 0; array < ArrayCount; array++)
        {
            var currentAngle = Rotation + (array * ArrayAngle);
            IList<DefaultBullet> childBullets = new List<DefaultBullet>();
            childBullets = InstanceBullets(childBullets, currentAngle);
            childBullets = PositionBullets(childBullets, currentAngle);
            childBullets = RotateBullets(childBullets);
            foreach (var item in childBullets)
            {
                controller.AddChild(item);
            }
            //List<>
        }
    }

    public IList<DefaultBullet> InstanceBullets(IList<DefaultBullet> childBullets, float angle)
    {
        for (int i = 0; i < VolleySize; i++)
        {
            var item = _bullet.Instance<DefaultBullet>();
            item.Init(Position, angle, BulletSpeed, BulletLifeSpan);
            item.AddToGroup("bullets");
            childBullets.Add(item);
        }
        return childBullets;
    }

    public IList<DefaultBullet> PositionBullets(IList<DefaultBullet> childBullets, float angle)
    {
        if (VolleySize > 1)
        {
            float spread = (SpreadWidth / 2) * -1;
            float spreadIncrement = SpreadWidth / (VolleySize - 1);
            float adjustedAngle = angle + Mathf.Deg2Rad(90);
            for (int i = 0; i < childBullets.Count; i++)
            {
                Vector2 newPos = new Vector2(spread * Mathf.Cos(adjustedAngle), spread * Mathf.Sin(adjustedAngle));
                childBullets[i].Translate(newPos);
                spread += spreadIncrement;
            }
        }
        return childBullets;
    }
    public IList<DefaultBullet> RotateBullets(IList<DefaultBullet> childBullets)
    {
        if (VolleySize > 1)
        {
            float spreadAngleIncrement = SpreadAngle / (VolleySize - 1);
            float currAngle = (SpreadAngle / 2) * -1;
            for (int i = 0; i < childBullets.Count; i++)
            {
                childBullets[i].Rotation += currAngle;
                currAngle += spreadAngleIncrement;
            }
        }
        return childBullets;
    }
    #endregion

    #region A
    public void ClipManagement()
    {
        shotCount += 1;
        if (ClipSize != 0 && shotCount >= ClipSize)
        {
            shotCount = 0;
            reloadTimer = ReloadTime;
        }
    }

    public void LoadEmitter(string fileName)
    {
        var file = new Godot.File();
        if (file.FileExists(fileName))
        {
            file.Open(fileName, File.ModeFlags.Read);
            while (file.GetPosition() < file.GetLen())
            {
                var saveData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(file.GetLine()).Result);
                //var newScore = saveData["Score"].ToString();
                Name = saveData["Name"].ToString();
                string[] V2Array = saveData["Position"].ToString().Replace("(", "").Replace(")", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var pos = new Vector2(float.Parse(V2Array[0]), float.Parse(V2Array[1]));
                Position = pos;
                Rotation = (float)(saveData["Rotation"]);
                FireRate = (float)(saveData["FireRate"]);
                ClipSize = (float)(saveData["ClipSize"]);
                ReloadTime = (float)(saveData["ReloadTime"]);
                AngularVelocity = ((float)(saveData["AngularVelocity"]));
                AngularAcceleration = ((float)(saveData["AngularAcceleration"]));
                MaxAngularVelocity = ((float)(saveData["MaxAngularVelocity"]));
                VolleySize = Convert.ToInt32(saveData["VolleySize"]);
                SpreadAngle = ((float)(saveData["SpreadAngle"]));
                SpreadWidth = (float)(saveData["SpreadWidth"]);
                ArrayCount = Convert.ToInt32(saveData["ArrayCount"]);
                ArrayAngle = ((float)(saveData["ArrayAngle"]));
                AimEnabled = (bool)(saveData["AimEnabled"]);
                AimPause = Convert.ToInt32(saveData["AimPause"]);
                AimOffset = ((float)(saveData["AimOffset"]));
                _bulletAddress = (saveData["_bulletAddress"].ToString());
                BulletSpeed = (float)(saveData["_bulletSpeed"]);
                BulletLifeSpan = (float)(saveData["_bulletLifeSpan"]);
            }
        }
    }

    #endregion

    #region Setter

    public void SetPositionX(float x)
    {
        Position = new Vector2(x, Position.y);
    }
    public void SetPositionY(float y)
    {
        Position = new Vector2(Position.x, y);
    }
    public void SetAngle(float value)
    {
        Rotation = Mathf.Deg2Rad(value);
    }
    public void SetAngle(Vector2 value)
    {
        LookAt(value);
    }
    public void SetBullet(string path)
    {
        _bulletAddress = path;
        _bullet = ResourceLoader.Load<PackedScene>(path);
    }
    #endregion
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Move();
        BoundHandler();
        if (AimEnabled)
        {
            Aim(delta, player.Position);
        }
        else
        {
            Rotating(delta);
        }

        if (CoolDown(delta) && Reload(delta))
        {
            Shoot();
            ClipManagement();
        }
    }
}
