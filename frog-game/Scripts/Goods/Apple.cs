using Godot;
using System;

public partial class Apple : Area2D
{
	[Export]
	public AnimatedSprite2D animatedSprite;
	[Export]
	public AudioStreamPlayer audioStreamPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += onAppleBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void onAppleBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			this.BodyEntered -= onAppleBodyEntered;
			animatedSprite.Play("Eat");
			AudioManager.Instance.PlaySound(audioStreamPlayer);
			animatedSprite.AnimationFinished += () =>
			{
				this.QueueFree();
			};
		}
	}
}
