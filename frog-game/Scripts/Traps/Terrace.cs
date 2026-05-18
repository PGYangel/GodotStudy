using Godot;
using System;

public partial class Terrace : AnimatableBody2D
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
	public float MoveSpeed = 100.0f;
	/// <summary>
	/// 当前点位索引
	/// </summary>
	private int currentPointIndex = 0;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		Vector2 velocity = Vector2.Zero;
		if (points.Length > 0)
		{
			// 目标点位位置
			Vector2 targetPosition = points[currentPointIndex].GlobalPosition;
			// 计算移动方向和速度
			Vector2 direction = (targetPosition - GlobalPosition).Normalized();
			// 移动向量
			velocity = direction * MoveSpeed;

			// 判断是否到达目标点位
			if (GlobalPosition.DistanceTo(targetPosition) < ArriveDistance)
			{
				currentPointIndex++;
				// 到达最后一个点 → 回到第一个（循环巡逻）
				if (currentPointIndex >= points.Length)
					currentPointIndex = 0;
			}
		}
		this.Position += velocity * (float)delta;
	}

}
