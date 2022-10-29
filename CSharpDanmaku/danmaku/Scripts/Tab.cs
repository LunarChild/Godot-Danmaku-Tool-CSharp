using Godot;
using System;

public class Tab : Tabs
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private int Id = 0;

    private Main _controller;


    public override void _Ready()
    {
        _controller = GetTree().Root.GetChild<Main>(0);
        base._Ready();
        //_controller
    }

    public void Init(int idx, string emitterName, Vector2 position, float fireRate, int volleySize, int arrayCount, float bulletSpeed, float bulletLifeSpan)
    {
        this.Name = idx.ToString();
        SetNameField(emitterName);
        SetPositionField(position);
        SetFireRateField(fireRate);
        SetVolleySizeField(volleySize);
        SetArrayCountField(arrayCount);
        SetBulletSpeedField(bulletSpeed);
        SetBulletLifeSpanField(bulletLifeSpan);
    }

    #region 信号槽
    public void OnSetName(string value)
    {
        _controller.UpdateName(this,value);
    }
    public void OnSetX(float value){
        _controller.UpdatePositionX(this,value);
    }
    public void OnSetY(float value){
        _controller.UpdatePositionY(this,value);
    }
    public void OnSetAngle(float value){
        _controller.UpdateAngle(this,value);
    }
    public void OnSetFireRate(float value){
        _controller.UpdateFireRate(this,value);
    }
    public void OnSetClipSize(float value){
        _controller.UpdateClipSize(this,value);
    }
    public void OnSetReloadTime(float value){
        _controller.UpdateReloadTime(this,value);
    }
    public void OnSetAngularVelocity(float value){
        _controller.UpdateAngularVelocity(this,Mathf.Deg2Rad(value) );
    }
    public void OnSetAngularAcceleration(float value){
        _controller.UpdateAngularAcceleration(this,Mathf.Deg2Rad(value) );
    }
    public void OnSetMaxAngularVelocity(float value){
        _controller.UpdateMaxAngularVelocity(this,Mathf.Deg2Rad(value) );
    }
    public void OnSetVolleySize(int value){
        _controller.UpdateVolleySize(this,value);
    }
    public void OnSetSpreadAngle(float value){
        _controller.UpdateSpreadAngle(this,Mathf.Deg2Rad(value) );
    }
    public void OnSetSpreadWidth(float value){
        _controller.UpdateSpreadWidth(this,value);
    }
    public void OnSetArrayCount(int value){
        _controller.UpdateArrayCount(this,value);
    }
    public void OnSetArrayAngle(float value){
        _controller.UpdateArrayAngle(this,Mathf.Deg2Rad(value) );
    }
    public void OnSetAimEnabled(bool value){
        _controller.UpdateAimEnable(this,value);
    }
    public void OnSetAimPause(int value){
        _controller.UpdateAimPause(this,value);
    }
    public void OnSetAimOffset(float value){
        _controller.UpdateAimOffset(this,Mathf.Deg2Rad(value) );
    }
    public void OnSetBulletSpeed(float value){
        _controller.UpdateBulletSpeed(this,value);
    }
    public void OnSetBulletLifeSpan(float value){
        _controller.UpdateBulletLifeSpan(this,value);
    }

    public void OnLoadEmitter(){
        GetNode<AcceptDialog>("LoadWarning").PopupCentered();
    }
    public void OnAcceptLoadWarning(){
        GetNode<FileDialog>("LoadDialog").PopupCentered();
    }
    public void OnLoadSelected(string path){
        _controller.LoadSelected(this,path);
    }
    public void OnLoadBullet(){
        GetNode<FileDialog>("LoadBulletDialog").PopupCentered();
    }
    public void OnBulletSelected(string path){
        _controller.UpdateBullet(this,path);
    }
    public void OnSaveEmitter(){
        GetNode<FileDialog>("SaveDialog").PopupCentered();
    }
    public void OnSavePathSelected(string path){
        _controller.SaveFile(this,path);
    }
    public void OnDelete(){
        GetNode<AcceptDialog>("DeleteWarning").PopupCentered();
    }
    public void OnAcceptDeleteWarning(){
        _controller.DeleteEmitter(this);
    }
    #endregion
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    #region 更新UI字段
    public void SetNameField(string value)
    {
        GetNode<LineEdit>("Menu/Name_Input").Text = value;
    }
    public void SetPositionField(Vector2 pos)
    {
        GetNode<SpinBox>("Menu/HBoxContainer3/X_Input").GetLineEdit().Text = pos.x.ToString();
        GetNode<SpinBox>("Menu/HBoxContainer4/Y_Input").GetLineEdit().Text = pos.y.ToString();
        GetNode<SpinBox>("Menu/HBoxContainer3/X_Input").Apply();
        GetNode<SpinBox>("Menu/HBoxContainer4/Y_Input").Apply();
    }
    public void SetAngleField(float value)
    {
        GetNode<SpinBox>("Menu/Angle_Input").GetLineEdit().Text = (Mathf.Rad2Deg(value)).ToString();
        GetNode<SpinBox>("Menu/Angle_Input").Apply();
    }
    public void SetFireRateField(float value)
    {
        GetNode<SpinBox>("Menu/FireRate_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/FireRate_Input").Apply();
    }
    public void SetClipSizeField(float value)
    {
        GetNode<SpinBox>("Menu/ClipSize_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/ClipSize_Input").Apply();
    }
    public void SetReloadTimeField(float value)
    {
        GetNode<SpinBox>("Menu/ReloadTime_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/ReloadTime_Input").Apply();
    }
    public void SetAngularVelocityField(float value)
    {
        GetNode<SpinBox>("Menu/AngularVelocity_Input").GetLineEdit().Text = Mathf.Rad2Deg(value).ToString();
        GetNode<SpinBox>("Menu/AngularVelocity_Input").Apply();
    }
    public void SetAngularAccelerationField(float value)
    {
        GetNode<SpinBox>("Menu/AngularAcceleration_Input").GetLineEdit().Text = Mathf.Rad2Deg(value).ToString();
        GetNode<SpinBox>("Menu/AngularAcceleration_Input").Apply();
    }
    public void SetMaxAngularVelocityField(float value)
    {
        GetNode<SpinBox>("Menu/MaxAngularVelocity_Input").GetLineEdit().Text = Mathf.Rad2Deg(value).ToString();
        GetNode<SpinBox>("Menu/MaxAngularVelocity_Input").Apply();
    }
    public void SetVolleySizeField(int value)
    {
        GetNode<SpinBox>("Menu/VolleySize_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/VolleySize_Input").Apply();
    }
    public void SetSpreadAngleField(float value)
    {
        GetNode<SpinBox>("Menu/SpreadAngle_Input").GetLineEdit().Text = Mathf.Rad2Deg(value).ToString();
        GetNode<SpinBox>("Menu/SpreadAngle_Input").Apply();
    }
    public void SetSpreadWidthField(float value)
    {
        GetNode<SpinBox>("Menu/SpreadWidth_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/SpreadWidth_Input").Apply();
    }

    public void SetArrayCountField(int value)
    {
        GetNode<SpinBox>("Menu/ArrayCount_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/ArrayCount_Input").Apply();
    }
    public void SetArrayAngleField(float value)
    {
        GetNode<SpinBox>("Menu/ArrayAngle_Input").GetLineEdit().Text = Mathf.Rad2Deg(value).ToString();
        GetNode<SpinBox>("Menu/ArrayAngle_Input").Apply();
    }
    public void SetAimEnabledField(bool value)
    {
        GetNode<Button>("Menu/AimEnabled_Input").Pressed = value;
    }
    public void SetAimPauseField(float value)
    {
        GetNode<SpinBox>("Menu/AimPause_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/AimPause_Input").Apply();
    }
    public void SetAimOffsetField(float value)
    {
        GetNode<SpinBox>("Menu/AimOffset_Input").GetLineEdit().Text = Mathf.Rad2Deg(value).ToString();
        GetNode<SpinBox>("Menu/AimOffset_Input").Apply();
    }
    public void SetBulletSpeedField(float value)
    {
        GetNode<SpinBox>("Menu/BulletSpeed_Input").GetLineEdit().Text = value.ToString();
        GetNode<SpinBox>("Menu/BulletSpeed_Input").Apply();
    }
    public void SetBulletLifeSpanField(float value)
    {
        GetNode<SpinBox>("Menu/BulletLifespan_Input").GetLineEdit().Text = (value).ToString();
        GetNode<SpinBox>("Menu/BulletLifespan_Input").Apply();
    }
    #endregion

}
