using Godot;
using System;

public partial class BeginScene : Node2D
{
	[Export]
	public Button btnBegin;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnBegin.Pressed += _on_btnBegin_pressed;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_btnBegin_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/GameScene.tscn");
	}

	public void _on_btnQuit_pressed()
	{
		GetTree().Quit();
	}

}
