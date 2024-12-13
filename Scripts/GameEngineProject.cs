using Godot;
using System;

public partial class GameEngineProject : Node2D
{
    ProjectileLauncher launcher = new ProjectileLauncher();

    public Label ammoCount;

    public override void _Ready()
    {
        base._Ready();

        ammoCount = GetNode<Label>("AmmoCount/AmmoCount");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        ammoCount.Text = $"Missiles Remaining: {launcher.ammo}";
    }
}
