using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }
	private int _score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<HUD>("HUD").ShowGameOver();

		// play death music
		GetNode<AudioStreamPlayer2D>("Music").Stop();
		GetNode<AudioStreamPlayer2D>("DeathSFX").Play();
	}

	public void NewGame()
	{
		// reset player
		Player player = GetNode<Player>("Player");
		Marker2D startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		// reset mobs
		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);//call queufree on every members of mobs group

        // reset score and timer
        _score = 0;
        GetNode<Timer>("StartTimer").Start();

		// update HUD
        HUD hud = GetNode<HUD>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");

		// play game music
		GetNode<AudioStreamPlayer2D>("Music").Play();
    }

	private void OnScoreTimerTimeout()
	{
		_score++;
        GetNode<HUD>("HUD").UpdateScore(_score);
    }

	private void OnStartTimerTimeout()
	{
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

	private void OnMobTimerTimeout()
	{
		Mob mob = MobScene.Instantiate<Mob>();

		PathFollow2D mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf(); // get random point along the path;

		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2; // mob direction perpendicular to path direction

		mob.Position = mobSpawnLocation.Position;

		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4); // radians 0 to 360 equivalent
		mob.Rotation = direction;

		Vector2 velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		AddChild(mob);
	}
}
