using Godot;
using System;

public partial class SettingPanel : Panel
{
	/// <summary>
	/// 关闭按钮
	/// </summary>
	[Export]
	public Button btnClose;
	// 音乐选择
	[Export]
	public CheckBox checkMusic;
	// 音乐音量
	[Export]
	public HSlider sliderMusicVolume;

	// 音效选择
	[Export]
	public CheckBox checkSound;
	// 音效音量
	[Export]
	public HSlider sliderSoundVolume;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.VisibilityChanged += _visibleChange;
		btnClose.Pressed += _on_btnClose_pressed;
		checkMusic.Toggled += _on_checkMusic_toggled;
		sliderMusicVolume.ValueChanged += _on_sliderMusicVolume_changed;
		checkSound.Toggled += _on_checkSound_toggled;
		sliderSoundVolume.ValueChanged += _on_sliderSoundVolume_changed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _visibleChange()
	{
		if (Visible)
		{
			checkMusic.ButtonPressed = AudioManager.MusicIsEnabled;
			sliderMusicVolume.Value = AudioManager.MusicVolume * 100;
			checkSound.ButtonPressed = AudioManager.SoundIsEnabled;
			sliderSoundVolume.Value = AudioManager.SoundVolume * 100;
		}
		else
		{

		}
	}

	private void _on_btnClose_pressed()
	{
		UICanvas.Instance.VisiblePanel(UICanvas.PanelType.SettingPanel, false);
	}
	private void _on_checkMusic_toggled(bool pressed)
	{
		AudioManager.MusicIsEnabled = pressed;
		AudioManager.Instance.UpdateMusic();
	}
	private void _on_sliderMusicVolume_changed(double value)
	{
		AudioManager.MusicVolume = (float)value / 100f;
		AudioManager.Instance.UpdateMusic();
	}
	private void _on_checkSound_toggled(bool pressed)
	{
		AudioManager.SoundIsEnabled = pressed;
	}
	private void _on_sliderSoundVolume_changed(double value)
	{
		AudioManager.SoundVolume = (float)value / 100f;
	}
}
