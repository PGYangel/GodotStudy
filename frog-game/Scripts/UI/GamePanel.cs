using Godot;
using System;

public partial class GamePanel : Panel
{
	[Export]
	public Label labGrade;

	[Export]
	public Button btnQuit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnQuit.Pressed += _on_btnQuit_pressed;
		UpdateGrade();
	}

	public void UpdateGrade()
	{
		labGrade.Text = DataManage.Grade.ToString();
	}

	public void _on_btnQuit_pressed()
	{
		UICanvas.Instance.VisiblePanel(UICanvas.PanelType.QuitPanel, true);
	}
}
