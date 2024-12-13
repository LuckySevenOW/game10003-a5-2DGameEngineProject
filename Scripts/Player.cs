using Godot;
using System;

public partial class Player : Node2D
{
	[Export]
	private float Speed = 400;

	[Export]
	private PackedScene Prefab;

	[Export]
	private Node2D PrefabParent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float moveX = Input.GetAxis("left", "right") * Speed * (float)delta;
		Translate(new Vector2(moveX, 0));

		if (Input.IsActionJustPressed("shoot"))
		{
			Node2D projectile = Prefab.Instantiate<Node2D>();
			PrefabParent.AddChild(projectile);
            projectile.GlobalPosition = this.GlobalPosition;

        }
	}
}
