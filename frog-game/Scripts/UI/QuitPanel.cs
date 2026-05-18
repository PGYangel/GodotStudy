using Godot;
using System;

public partial class QuitPanel : Panel
{
	[Export]
	public Button btnYes;
	[Export]
	public Button btnNo;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnYes.Pressed += _on_btnYes_pressed;
		btnNo.Pressed += _on_btnNo_pressed;
		this.VisibilityChanged += visibleQuitPanel;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void visibleQuitPanel()
	{
		if (this.Visible)
		{
			GetTree().Paused = true;
		}
		else
		{
			GetTree().Paused = false;
		}
	}

	public void _on_btnYes_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/BeginScene.tscn");
	}
	public void _on_btnNo_pressed()
	{
		UICanvas.Instance.VisiblePanel(UICanvas.PanelType.QuitPanel, false);
	}
}
