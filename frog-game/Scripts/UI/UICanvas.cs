using Godot;
using System;

public partial class UICanvas : CanvasLayer
{
	public static UICanvas Instance { get; private set; }

	public enum PanelType
	{
		GamePanel,
		EndPanel,
		SettingPanel,
		QuitPanel,
		RankingPanel
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Instance != null && Instance != this)
		{
			QueueFree();
			return;
		}
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void HideAllPanels()
	{
		foreach (PanelType panelType in Enum.GetValues(typeof(PanelType)))
		{
			VisiblePanel(panelType, false);
		}
	}

	public void VisiblePanel(PanelType panelType, bool isVisible)
	{
		Panel panel = GetNode<Panel>(panelType.ToString());
		if (panel != null)
		{
			panel.Visible = isVisible;
		}
		else
		{
			GD.PrintErr($"没有找到名为 '{panelType}' 的面板节点。");
		}
	}
}
