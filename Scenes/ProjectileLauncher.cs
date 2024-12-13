using Godot;
using System;
using static Godot.SkeletonProfile;

public partial class ProjectileLauncher : Node2D
{
	
	[Export]
	public Node2D Aiming { get; private set; }

	[Export]
	public PackedScene ProjectileScene { get; private set; }

    [Export]
    public StringName ShootAction { get; private set; } = "shoot";

    [Export]
    public StringName ProjectilesParentGroup { get; private set; } = "ProjectilesParent";

    private Node projectilesParent;

    [Export]
    public float LaunchPower { get; set; } = 60000f;

    public Vector2 aimDirection;
    public int ammo = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        projectilesParent = GetTree().GetFirstNodeInGroup(ProjectilesParentGroup);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		aimDirection = GlobalPosition.DirectionTo(mousePosition);
		Aiming.Rotation = GetAngleTo(mousePosition);
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(ShootAction) && ammo >= 1)
        {
            ShootProjectile(ProjectileScene);
            ammo -= 1;
        }
    }

    public void ShootProjectile(PackedScene projectileToShoot)
    {
        Projectile projectileInstance = projectileToShoot.Instantiate<Projectile>();
        projectilesParent.AddChild(projectileInstance);
        projectileInstance.GlobalPosition = GlobalPosition;
        Vector2 launchVector = aimDirection * LaunchPower;
        projectileInstance.ApplyForce(launchVector);
    }
}
