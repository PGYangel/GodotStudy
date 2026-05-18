using Godot;
using System;

public partial class Monster : CharacterBody2D
{

	/// <summary>
	/// 移动点位
	/// </summary>
	[Export]
	public Node2D[] points;
	/// <summary>
	/// 到达点位的距离
	/// </summary>
	[Export]
	public float ArriveDistance = 20.0f;
	/// <summary>
	/// 移动速度
	/// </summary>
	[Export]
	public float MoveSpeed = 200.0f;
	/// <summary>
	/// 角色动画精灵
	/// </summary>
	[Export]
	public AnimatedSprite2D animatedSprite;
	/// <summary>
	/// 角色死亡音效
	/// </summary>
	[Export]
	public AudioStreamPlayer audioStreamPlayer;
	/// <summary>
	/// 死亡区域
	/// </summary>
	[Export]
	public Area2D deathArea;
	/// <summary>
	/// 怪物碰撞体
	/// </summary>
	[Export]
	public CollisionShape2D MonsterBody;
	/// <summary>
	/// 攻击区域
	/// </summary>
	[Export]
	public Area2D HitArea;

	/// <summary>
	/// 当前点位索引
	/// </summary>
	private int currentPointIndex = 0;

	private bool isDead = false;

	private bool _deathPlaying = false;



	public override void _Ready()
	{
		deathArea.BodyEntered += _on_DeathArea_body_entered;
		HitArea.BodyEntered += _on_HitArea_body_entered;
	}

	public override void _PhysicsProcess(double delta)
	{

		if (isDead)
		{
			// 死亡后停止移动
			Velocity = Vector2.Zero;
			return;
		}
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		// 目标点位位置

		if (points.Length > 0)
		{
			// 目标点位位置
			Vector2 targetPosition = points[currentPointIndex].GlobalPosition;
			// 计算移动方向和速度
			Vector2 direction = (targetPosition - GlobalPosition).Normalized();
			// 移动向量
			velocity = direction * MoveSpeed;

			// 根据方向调整玩家水平位置FlipH属性
			if (direction.X < 0)
			{
				animatedSprite.FlipH = true; // 向左移动，翻转精灵
			}
			else if (direction.X > 0)
			{
				animatedSprite.FlipH = false; // 向右移动，正常显示精灵
			}

			// 判断是否到达目标点位
			if (GlobalPosition.DistanceTo(targetPosition) < ArriveDistance)
			{
				currentPointIndex++;
				// 到达最后一个点 → 回到第一个（循环巡逻）
				if (currentPointIndex >= points.Length)
					currentPointIndex = 0;
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		AnimatedControl();
	}

	private void AnimatedControl()
	{

		if (isDead && _deathPlaying)
		{
			// 如果正在播放死亡动画，保持当前动画状态
			return;
		}
		if (isDead)
		{
			this._deathPlaying = true;
			// 播放死亡动画
			animatedSprite.Play("Dead");
			MonsterBody.Disabled = true; // 禁用碰撞体，防止角色死亡后仍然与玩家发生碰撞
		}
	}


	public void _on_DeathArea_body_entered(Node2D body)
	{
		if (body is Player player)
		{
			deathArea.BodyEntered -= _on_DeathArea_body_entered;
			HitArea.BodyEntered -= _on_HitArea_body_entered;
			AudioManager.Instance.PlaySound(audioStreamPlayer);
			isDead = true;
			DataManage.Instance.AddGrade(20);
			// 死亡动画播放完毕后删除怪物节点,怪物节点是父节点
			animatedSprite.AnimationFinished += () =>
			{
				this.GetParent().QueueFree();
			};
		}
	}

	public void _on_HitArea_body_entered(Node2D body)
	{
		if (body is Player player)
		{
			HitArea.BodyEntered -= _on_HitArea_body_entered;
			player.DeadPlayer();
		}
	}


}
