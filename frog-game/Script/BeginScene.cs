using Godot;
using System;

public partial class BeginScene : Node2D
{
	// 开始游戏
	[Export]
	public Button btnBegin;
	// 排行榜
	[Export]
	public Button btnRanking;
	// 设置
	[Export]
	public Button btnSetting;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnBegin.Pressed += ClickBtnBegin;
	}

	// 点击开始游戏
	public void ClickBtnBegin()
	{
		GetTree().ChangeSceneToFile("res://Scenes/GameScene.tscn");
	}

	// 点击退出游戏
	public void ClickBtnQuit()
	{
		GetTree().Quit();
	}

}
