using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

public partial class RankingPanel : Panel
{
	[Export]
	public Button btnClose;

	/// <summary>
	/// 排行榜列表容器
	/// </summary>
	[Export]
	public VBoxContainer VBoxRankList;

	/// <summary>
	/// 排行榜行预制体
	/// </summary>
	[Export]
	public PackedScene randRow;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnClose.Pressed += _on_btnClose_pressed;
		this.VisibilityChanged += _visible;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _visible()
	{
		if (this.Visible)
		{
			Dictionary root = DataManage.Instance.LoadRanking();
			Array rankingList;
			if (root == null)
			{
				root = new Dictionary();
				rankingList = new Array();
			}
			else
			{
				rankingList = root["Ranking"].AsGodotArray();
			}

			foreach (var item in rankingList)
			{
				Dictionary role = item.AsGodotDictionary();
				HBoxContainer randRowInstance = randRow.Instantiate<HBoxContainer>();
				randRowInstance.GetNode<Label>("Name").Text = role["Name"].AsString();
				randRowInstance.GetNode<Label>("Score").Text = role["Score"].AsInt32().ToString();
				// 时间格式化为 "yyyy-MM-dd"
				randRowInstance.GetNode<Label>("Date").Text = DateTime.Parse(role["Date"].AsString()).ToString("yyyy-MM-dd");
				VBoxRankList.AddChild(randRowInstance);
			}
		}
		else
		{
			// 隐藏时清空排行榜列表
			foreach (Node child in VBoxRankList.GetChildren())
			{
				// 判断子节点是否为分组RandRow的实例，如果是则删除
				if (child is HBoxContainer && child.IsInGroup("RankRow"))
				{
					child.QueueFree();
				}

			}
		}
	}

	private void _on_btnClose_pressed()
	{
		UICanvas.Instance.VisiblePanel(UICanvas.PanelType.RankingPanel, false);
	}
}
