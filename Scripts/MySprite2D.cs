using Godot;
using System;

public partial class MySprite2D : Sprite2D
{
    // export variables

    // member variables
    private int _speed = 400;
    private float _angularSpeed = Mathf.Pi; // radians by default

    // functions
    public override void _Process(double delta) // delta is time elapsed since last frame
    {
        // ui_left / ui_right are defaulted to arrow keys and gamepad
        // this can be modified in the Input Map settings of Godot
        Int16 direction = 0;
        if (Input.IsActionPressed("ui_left"))
        {
            direction = -1;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            direction = 1;
        }

        // rotate object orientation in space
        Rotation += _angularSpeed * direction * (float)delta;

        // set local velocity dir/magnitude to no movement
        Vector2 velocity = Vector2.Zero;
        if (Input.IsActionPressed("ui_up"))
        {
            // Vector2.Up is global forward direction, align with this object's forward
            // direction by rotating the same direction that this object has rotated
            // velocity is towards that forward direction at the magnitude of _speed;
            velocity = Vector2.Up.Rotated(Rotation) * _speed;
        }

        // move towards target dir/mag encapsulated by velocity, but only move
        // partially to the goal vector. The partial amount is encapsulated by the delta
        Position += velocity * (float)delta;
    }

    //_unhandled_input() -> callback best for events that don't occur every frame

    //Input singleton -> global object best for checking input every frame
}
