using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public AnimatedSprite2D animatedSprite;

	[Export]
	public AudioStreamPlayer audioStreamPlayer;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("myJump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			AudioManager.Instance.PlaySound(audioStreamPlayer);
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			if (direction.X < 0)
			{

				animatedSprite.FlipH = true;
			}
			else if (direction.X > 0)
			{
				animatedSprite.FlipH = false;
			}
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		AnimationControl();
	}
	public void AnimationControl()
	{
		if (!IsOnFloor())
		{
			animatedSprite.Play("Jump");
			if (Velocity.Y > 0)
			{
				animatedSprite.Play("Fall");
			}
		}
		else
		{
			if (Velocity.X == 0)
			{
				animatedSprite.Play("Idle");
			}
			else
			{
				animatedSprite.Play("Run");
			}
		}
	}
}
