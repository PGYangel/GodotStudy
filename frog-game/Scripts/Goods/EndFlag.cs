using Godot;
using System;

public partial class EndFlag : Area2D
{
	[Export]
	public AudioStreamPlayer audioStreamPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += onEndFlagBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void onEndFlagBodyEntered(Node2D body)
	{
		this.BodyEntered -= onEndFlagBodyEntered;
		if (body is Player player)
		{
			AudioManager.Instance.PlaySound(audioStreamPlayer);
			player.StopPlayer();
			UICanvas.Instance.VisiblePanel(UICanvas.PanelType.EndPanel, true);
		}
	}
}
