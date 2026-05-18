using Godot;
using System;

public partial class BeginScene : Node2D
{
	/// <summary>
	/// 开始按钮
	/// </summary>
	[Export]
	public Button btnBegin;
	/// <summary>
	/// 设置按钮
	/// </summary>
	[Export]
	public Button btnSetting;
	/// <summary>
	/// 排行榜按钮
	/// </summary>
	[Export]
	public Button btnRanking;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UICanvas.Instance.HideAllPanels();
		// 设置背景音乐
		AudioManager.Instance.PlayMusic("res://Assets/Music/Begin.mp3");

		btnBegin.Pressed += _on_btnBegin_pressed;
		btnSetting.Pressed += _on_btnSetting_pressed;
		btnRanking.Pressed += _on_btnRanking_pressed;

		btnBegin.MouseEntered += _on_btnHover_MouseEntered;
		btnSetting.MouseEntered += _on_btnHover_MouseEntered;
		btnRanking.MouseEntered += _on_btnHover_MouseEntered;
	}

	/// <summary>
	/// 开始游戏
	/// </summary>
	public void _on_btnBegin_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/GameScene.tscn");
	}

	/// <summary>
	/// 设置界面
	/// </summary>
	public void _on_btnSetting_pressed()
	{
		UICanvas.Instance.VisiblePanel(UICanvas.PanelType.SettingPanel, true);
	}

	/// <summary>
	/// 排行榜界面
	/// </summary>
	public void _on_btnRanking_pressed()
	{
		UICanvas.Instance.VisiblePanel(UICanvas.PanelType.RankingPanel, true);
	}

	/// <summary>
	/// 退出游戏
	/// </summary>
	public void _on_btnQuit_pressed()
	{
		GetTree().Quit();
	}

	/// <summary>
	/// 按钮悬停音效
	/// </summary>
	public void _on_btnHover_MouseEntered()
	{
		AudioStreamPlayer player = GetNode<AudioStreamPlayer>("btnHoverAudioStreamPlayer");
		AudioManager.Instance.PlaySound(player);
	}

}
