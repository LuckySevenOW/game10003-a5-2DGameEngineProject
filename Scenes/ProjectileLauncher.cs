using Godot;
using System;
using static Godot.SkeletonProfile;

public partial class ProjectileLauncher : Node2D
{
	//All of these get; private set; setters allow each of these to be read, but not to be written to, so that these don't get messed up by other things elsewhere.

	[Export]
	public Node2D Aiming { get; private set; }

    //Loads the packed scene ProjectileScene for firing the projectiles later. 
	[Export]
	public PackedScene ProjectileScene { get; private set; }

    //Sets the shoot action to "shoot", which is also set up as an action in Godot, using the spacebar to fire. 
    [Export]
    public StringName ShootAction { get; private set; } = "shoot";

    //Sets the parent group for the projectiles when they spawn to keep things more organized. 
    [Export]
    public StringName ProjectilesParentGroup { get; private set; } = "ProjectilesParent";

    private Node projectilesParent;

    //Sets launch power, which determines how much force is applied to the projectile, and lets it be set in the Godot inspector. 
    [Export]
    public float launchPower { get; set; } = 60000f;

    public Vector2 aimDirection;

    // Ammo count is 3. I considered adding more, but I think having 4 or more would make it a bit too easy to just topple everything every time. 
    public int ammo = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Assigns the parent for the projectiles directly here instead of in the inspector. 
        projectilesParent = GetTree().GetFirstNodeInGroup(ProjectilesParentGroup);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        //Gets the mouse position and rotates the launcher to point at it, which provides a visual indicator for aiming the missile. 
		Vector2 mousePosition = GetGlobalMousePosition();
		aimDirection = GlobalPosition.DirectionTo(mousePosition);
		Aiming.Rotation = GetAngleTo(mousePosition);
	}

    public override void _Input(InputEvent @event)
    {
        //If the shoot action (spacebar) is pressed, and you have ammo (ammo >=1), it will fire a projectile, and deduct one from your ammo count. 
        if (Input.IsActionJustPressed(ShootAction) && ammo >= 1)
        {
            ShootProjectile(ProjectileScene);
            ammo -= 1;
        }
    }

    public void ShootProjectile(PackedScene projectileToShoot)
    {
        //Instantiates new projectiles, adds them to projectilesParent, and then applies force to them, propelling them toward where you are aiming. 
        Projectile projectileInstance = projectileToShoot.Instantiate<Projectile>();
        projectilesParent.AddChild(projectileInstance);
        projectileInstance.GlobalPosition = GlobalPosition;
        Vector2 launchVector = aimDirection * launchPower;
        projectileInstance.ApplyForce(launchVector);
    }
}
