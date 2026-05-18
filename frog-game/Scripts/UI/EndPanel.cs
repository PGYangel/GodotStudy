using Godot;
using System;

public partial class EndPanel : Panel
{
	[Export]
	public Label labGrade;
	[Export]
	public TextEdit txtName;

	[Export]
	public Button btnQuit;

	/// <summary>
	/// 玩家名字最大长度
	/// </summary>
	private readonly int _maxLengthName = 5;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		labGrade.Text = DataManage.Grade.ToString();
		btnQuit.Pressed += _on_btnQuit_pressed;
		this.VisibilityChanged += _visible;
		txtName.TextChanged += OnTextChanged;
	}

	private void _visible()
	{
		if (this.Visible)
		{
			labGrade.Text = DataManage.Grade.ToString();
			txtName.Text = "";
			btnQuit.Disabled = true;
		}
	}

	// 文本变化时触发
	private void OnTextChanged()
	{
		if (txtName.Text.Length == 0)
			btnQuit.Disabled = true;
		else
			btnQuit.Disabled = false;

		// 如果当前文本长度 > 最大长度 → 截断！
		if (txtName.Text.Length > _maxLengthName)
		{
			// 只保留前 maxLength 个字符
			txtName.Text = txtName.Text.Substring(0, _maxLengthName);
		}
	}

	public void _on_btnQuit_pressed()
	{
		string playerName = txtName.Text.Trim();
		if (string.IsNullOrEmpty(playerName))
		{
			GD.PrintErr("请输入玩家姓名!");
			return;
		}
		// 保存排行榜数据
		DataManage.Instance.SaveRanking(playerName);
		GetTree().ChangeSceneToFile("res://Scenes/BeginScene.tscn");
	}
}
