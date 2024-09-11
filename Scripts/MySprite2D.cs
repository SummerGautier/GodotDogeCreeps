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
        // rotate object orientation in space
        Rotation += _angularSpeed * (float)delta;

        // move object origin in direction of rotation and at magnitude of speed
        // Vector2.Up is forward relative to the object, it's modified by the new rotation to 
        // obtain the new direction.
        // how much towards that new direction we move is based on the delta fragment
        Vector2 velocity = Vector2.Up.Rotated(Rotation) * _speed;
        Position += velocity * (float)delta;
    }

    //_unhandled_input() -> callback best for events that don't occur every frame

    //Input singleton -> global object best for checking input every frame
}
