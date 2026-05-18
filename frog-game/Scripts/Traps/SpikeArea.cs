using Godot;
using System;

public partial class SpikeArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += _on_SpikeArea_body_entered;
	}

	public void _on_SpikeArea_body_entered(Node2D body)
	{
		if (body is Player player)
		{
			player.DeadPlayer();
		}
	}
}
