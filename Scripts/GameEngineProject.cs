using Godot;
using System;

public partial class GameEngineProject : Node2D
{
    
    ProjectileLauncher launcher = new ProjectileLauncher();

    public Label ammoCount;

    public override void _Ready()
    {
        base._Ready();

        //Assigns ammoCount the AmmoCount label from the 2d_game_engine_project scene.
        ammoCount = GetNode<Label>("AmmoCount/AmmoCount");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        //Draws the text on the ammoCount label, and (should) change the text to reflect the current ammo count (0-3).
        ammoCount.Text = $"Missiles Remaining: {launcher.ammo}";
    }
}
