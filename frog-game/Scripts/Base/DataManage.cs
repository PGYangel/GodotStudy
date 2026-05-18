using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;
using System.Linq;

public partial class DataManage : Node
{
	public static DataManage Instance { get; private set; }

	public static float Grade { get; set; } = 0f;

	// 排行榜文件路径
	private readonly string _rankingPath = "user://ranking.dat";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Instance == null)
		{
			Instance = this;
			// 这里可以添加一些初始化代码，例如加载数据等
		}
		else
		{
			QueueFree(); // 如果已经存在实例，销毁当前节点
		}
	}

	/// <summary>
	/// 重置游戏数据
	/// </summary>
	public void ResetData()
	{
		Grade = 0f;
		UICanvas.Instance.GetNode<GamePanel>("GamePanel").UpdateGrade();
	}

	/// <summary>
	///  添加分数
	/// </summary>
	/// <param name="value"></param>
	public void AddGrade(float value)
	{
		Grade += value;
		UICanvas.Instance.GetNode<GamePanel>("GamePanel").UpdateGrade();
	}

	/// <summary>
	/// 保存排行榜数据
	/// </summary>
	/// <param name="playerName">玩家名称</param>
	public void SaveRanking(string playerName = "Player")
	{
		try
		{
			// 根数据
			Dictionary root = LoadRanking();
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
			// 当前玩家数据
			Dictionary playerData = new Dictionary()
			{
				{"Name", playerName},
				{"Score", Grade},
				{"Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
			};
			rankingList.Add(playerData);
			// 排行榜列表根据Score排序，分数高的在前，如果分数相同则根据Date排序，时间早的在前
			var sortedList = rankingList.Select(r => r.AsGodotDictionary())
				.OrderByDescending(r => r["Score"].AsSingle())
				.ThenBy(r => DateTime.Parse(r["Date"].AsString()))
				.ToList();
			// 将sortedList转换回Godot Array
			rankingList = new Array();
			foreach (var item in sortedList)
			{
				rankingList.Add(item);
			}
			// 只保留前10名
			if (rankingList.Count > 10)
			{
				rankingList.Resize(10);
			}

			root["Ranking"] = rankingList;
			// 序列化为JSON字符串
			string json = Json.Stringify(root);
			// FileAccess写入文件
			using (FileAccess file = FileAccess.Open(_rankingPath, FileAccess.ModeFlags.Write))
			{
				file.StoreString(json);
			}
		}
		catch (Exception ex)
		{
			GD.PrintErr("保存排行榜数据失败: " + ex.Message);
		}
	}

	/// <summary>
	/// 加载排行榜数据
	/// </summary>
	/// <returns></returns>
	public Dictionary LoadRanking()
	{
		if (!FileAccess.FileExists(_rankingPath))
		{
			GD.Print("排行榜文件不存在!");
			return null;
		}
		// 读取文件
		string json = "";
		using (FileAccess file = FileAccess.Open(_rankingPath, FileAccess.ModeFlags.Read))
		{
			json = file.GetAsText();
		}
		// 解析json字符串
		Variant parsed = Json.ParseString(json);
		Dictionary root = parsed.AsGodotDictionary();
		return root;
	}

}
