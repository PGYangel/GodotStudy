using Godot;
using System;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    #region 音乐变量
    /// <summary>
    /// 音乐是否启用
    /// </summary>
    public static bool MusicIsEnabled { get; set; } = true;
    /// <summary>
    /// 音乐音量
    /// </summary>
    public static float MusicVolume { get; set; } = 0.5f;
    /// <summary>
    /// 音乐播放器
    /// </summary>
    public static AudioStreamPlayer MusicAudioPlayer => Instance.GetNode<AudioStreamPlayer>("/root/BgAudioPlayer");
    #endregion

    #region 音效变量
    /// <summary>
    /// 音效是否启用
    /// </summary>
    public static bool SoundIsEnabled { get; set; } = true;
    /// <summary>
    /// 音效音量
    /// </summary>
    public static float SoundVolume { get; set; } = 0.5f;
    #endregion

    public override void _Ready()
    {
        var config = new ConfigFile();
        var err = config.Load("user://settings.cfg");
        if (err == Error.Ok)
        {
            MusicIsEnabled = (bool)config.GetValue("Audio", "MusicIsEnabled", true);
            MusicVolume = (float)config.GetValue("Audio", "MusicVolume", 0.5f);
            SoundIsEnabled = (bool)config.GetValue("Audio", "SoundIsEnabled", true);
            SoundVolume = (float)config.GetValue("Audio", "SoundVolume", 0.5f);
        }
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            QueueFree(); // Destroy duplicate instances
        }
    }
    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="path">音乐资源</param>
    public void PlayMusic(string path)
    {
        if (!MusicIsEnabled || MusicAudioPlayer == null)
        {
            PauseMusic();
            return;
        }
        MusicAudioPlayer.Stream = ResourceLoader.Load<AudioStream>(path);
        MusicAudioPlayer.VolumeDb = Mathf.LinearToDb(MusicVolume);
        MusicAudioPlayer.Play();
    }
    /// <summary>
    /// 更新音乐状态
    /// </summary>
    public void UpdateMusic()
    {
        if (!MusicIsEnabled || MusicAudioPlayer == null)
        {
            PauseMusic();
            return;
        }
        MusicAudioPlayer.VolumeDb = Mathf.LinearToDb(MusicVolume);
        if (!MusicAudioPlayer.Playing)
        {
            MusicAudioPlayer.Play();
        }

    }
    /// <summary>
    /// 暂停音乐
    /// </summary>
    public void PauseMusic()
    {
        if (MusicAudioPlayer == null) return;
        MusicAudioPlayer.Stop();
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="soundPlayer">音效播放器</param>
    public void PlaySound(AudioStreamPlayer soundPlayer)
    {
        if (!SoundIsEnabled || soundPlayer == null) return;
        soundPlayer.VolumeDb = Mathf.LinearToDb(SoundVolume);
        soundPlayer.Play();
    }
}
