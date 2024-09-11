using Godot;
using System;

public partial class RootNode : Node2D
{
	[Export(PropertyHint.Range, "0,10000,500,or_less")]
	private int _id;

    [Flags]
    public enum MyEnum
    {
        Fire = 1 << 1,
        Water = 1 << 2,
        Earth = 1 << 3,
        Wind = 1 << 4,

        FireAndWater = Fire | Water,
    }

    [Export(PropertyHint.Flags, "Fire,Water,Earth,Wind")]
    public int SpellElements { get; set; } = 0;

    [ExportGroup("Store Properties")]
    [Export]
    private string _store_name;

    [Export(PropertyHint.File, "*.png,*.jpg")]
	private string _background_image;

	[Export(PropertyHint.MultilineText)]
	private string history;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello from C# to Godot :)");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
