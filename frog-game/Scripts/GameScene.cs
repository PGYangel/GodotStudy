using Godot;
using System;

public partial class GameScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// 设置背景音乐
		AudioManager.Instance.PlayMusic("res://Assets/Music/Gameing.mp3");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_btnQuit_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/BeginScene.tscn");
	}
}
