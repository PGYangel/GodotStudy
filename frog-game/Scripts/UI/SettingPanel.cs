using Godot;
using System;

public partial class SettingPanel : Panel
{
	/// <summary>
	/// 关闭按钮
	/// </summary>
	[Export]
	public Button btnClose;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnClose.Pressed += _on_btnClose_pressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_btnClose_pressed()
	{
		UICanvas.Instance.VisiblePanel(UICanvas.PanelType.SettingPanel, false);
	}
}
