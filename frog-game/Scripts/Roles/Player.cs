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
	public bool GameOver = false;
	/// <summary>
	/// 玩家是否死亡
	/// </summary>
	public bool isDead = false;
	/// <summary>
	/// 死亡动画是否正在播放
	/// </summary>
	public bool _deadplaying = false;

	public override void _PhysicsProcess(double delta)
	{

		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		// Handle Jump.
		if (Input.IsActionJustPressed("myJump") && IsOnFloor() && !GameOver)
		{
			velocity.Y = JumpVelocity;
			AudioManager.Instance.PlaySound(audioStreamPlayer);
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero && !GameOver)
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
		if (isDead && _deadplaying) return;

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

		if (isDead && !_deadplaying)
		{
			animatedSprite.Play("Dead");
			_deadplaying = true;

			// 播放死亡音效
			audioStreamPlayer.Stream = GD.Load<AudioStream>("res://Assets/Music/Loss.mp3");
			AudioManager.Instance.PlaySound(audioStreamPlayer);
		}
	}

	/// <summary>
	///  玩家死亡逻辑
	/// </summary>
	public void DeadPlayer()
	{
		GameOver = true;
		isDead = true;
		// animatedSprite动画播放完毕
		animatedSprite.AnimationFinished += () =>
		{
			UICanvas.Instance.VisiblePanel(UICanvas.PanelType.EndPanel, true);
		};
	}

	/// <summary>
	/// 暂停玩家
	/// </summary>
	public void StopPlayer()
	{
		// Velocity = Vector2.Zero;
		// SetPhysicsProcess(false);
		GameOver = true;
	}
}
