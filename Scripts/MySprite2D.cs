using Godot;
using System;

public partial class MySprite2D : Sprite2D
{
    // export variables
    [Signal]
    public delegate void HealthDepletedEventHandler();

    // member variables
    private int _speed = 400;
    private float _angularSpeed = Mathf.Pi; // radians by default
    private int _health = 10;
    public int Health // this is used for signals, the _health is auto-set by init and causes infinite loop
    {
        get { return _health; }
        set
        {
            _health = value;
            if(_health < 0)
            {
                EmitSignal(SignalName.HealthDepleted);
                _health = 10;
            }
        }
    }
    // called on object initilization
    public override void _Ready()
    {
        _health = 10;
        Timer timer = GetNode<Timer>("MyTimer");//Looks for object of type with specified name
        timer.Timeout += OnTimerTimeout; // connect timer's timeout signal to function OnTimerTimeout
    }

    // called once a frame
    public override void _Process(double delta) // delta is time elapsed since last frame
    {
        Rotation += _angularSpeed * (float)delta;
        var velocity = Vector2.Up.Rotated(Rotation) * _speed;
        Position += velocity * (float)delta;
    }

    private void OnButtonPressed()
    {
        SetProcess(!IsProcessing()); // toggle on/off the process function to begin/stop
        Health--;
        GD.Print("health is: " + Health);
    }

    private void OnTimerTimeout()
    {
        Visible = !Visible;
    }
}
