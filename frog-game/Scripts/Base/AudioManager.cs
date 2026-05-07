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
        if (!MusicIsEnabled || MusicAudioPlayer == null) return;
        MusicAudioPlayer.Stream = ResourceLoader.Load<AudioStream>(path);
        MusicAudioPlayer.VolumeDb = Mathf.LinearToDb(MusicVolume);
        MusicAudioPlayer.Play();
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
