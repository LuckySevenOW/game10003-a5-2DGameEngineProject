using Godot;
using System;

public partial class GameEngineProject : Node2D
{
    private float time = 0.0f;

    private Label timeLabel;

    public override void _Ready()
    {
        base._Ready();

        timeLabel = GetNode<Label>("Node2D/TimeLabel");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        time += (float)delta;

        timeLabel.Text = $"Time elapsed since start: {time:F1} s";
    }
}
