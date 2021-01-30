using UnityEngine;
using System.Collections;

public class AudioOutManager
{
    /// <summary>
    /// 声音组件所在根节点
    /// </summary>
    public GameObject audioObject;
    /// <summary>
    /// 音效
    /// </summary>
    public AudioSource audioSource;
    /// <summary>
    /// 背景音
    /// </summary>
    public AudioSource backAudioSource;
    /// <summary>
    /// 背景音
    /// </summary>
    public AudioSource backAudioSource2;
    /// <summary>
    /// 语音
    /// </summary>
    public AudioSource microphoneAudioSource;
    /// <summary>
    /// 声音获取器
    /// </summary>
    public AudioListener audioListener;
    /// <summary>
    /// 播放声音
    /// </summary>
    /// <param name="soundPark"></param>
    /// <param name="soundName"></param>
    public void PlaySound(string soundPark, string soundName, float volice = 1.0f)
    {
        AudioSource curAudio = GetAudioSource();
        curAudio.volume = volice;
        AssetsParkManager.PlaySound(soundPark, curAudio, soundName);
    }
    /// <summary>
    /// 播放背景音
    /// </summary>
    /// <param name="soundPark"></param>
    /// <param name="soundName"></param>
    public void PlayBackSound(string soundPark, string soundName, bool loop)
    {
        AudioSource curAudio = GetBackAudioSource();

        AssetsParkManager.PlayBGMSound(soundPark, curAudio, soundName, loop);
    }

    /// <summary>
    /// 播放背景音
    /// </summary>
    /// <param name="soundPark"></param>
    /// <param name="soundName"></param>
    public void PlayBackSound2(string soundPark, string soundName, bool loop)
    {
        AudioSource curAudio = GetBackAudioSource2();

        AssetsParkManager.PlayBGMSound(soundPark, curAudio, soundName, loop);
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBackSound()
    {
        AudioSource curAudio = GetBackAudioSource();

        curAudio.volume = 0;

    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBackSound2()
    {
        AudioSource curAudio = GetBackAudioSource2();

        curAudio.volume = 0;

    }

    /// <summary>
    /// 开启和屏蔽背景音
    /// </summary>
    /// <param name="isActive"></param>
    public void ActiveBackAudioAndSampeAudio(bool isActive)
    {
        if (audioSource != null)
        {
            audioSource.enabled = isActive;
        }

        if (backAudioSource != null)
        {
            backAudioSource.enabled = isActive;
        }

        if (backAudioSource2 != null)
        {
            backAudioSource2.enabled = isActive;
        }
    }

    public AudioSource GetAudioSource()
    {
        if (audioSource == null)
        {
            if (audioObject == null)
            {
                audioObject = new GameObject("AudioSourceRoot");

                audioObject.transform.SetParent(LSharpEntryGame.Instance.gameDontDestroy.transform);
            }

            audioSource = audioObject.AddComponent<AudioSource>();

            if (audioListener == null)
            {
                audioListener = audioObject.AddComponent<AudioListener>();
            }
        }

        return audioSource;
    }

    public AudioSource GetBackAudioSource()
    {
        if (backAudioSource == null)
        {
            if (audioObject == null)
            {
                audioObject = new GameObject("AudioSourceRoot");
                audioObject.transform.SetParent(LSharpEntryGame.Instance.gameDontDestroy.transform);
            }

            backAudioSource = audioObject.AddComponent<AudioSource>();

            if (audioListener == null)
            {
                audioListener = audioObject.AddComponent<AudioListener>();
            }
        }

        return backAudioSource;
    }

    public AudioSource GetBackAudioSource2()
    {
        if (backAudioSource2 == null)
        {
            if (audioObject == null)
            {
                audioObject = new GameObject("AudioSourceRoot");
                audioObject.transform.SetParent(LSharpEntryGame.Instance.gameDontDestroy.transform);
            }

            backAudioSource2 = audioObject.AddComponent<AudioSource>();

            if (audioListener == null)
            {
                audioListener = audioObject.AddComponent<AudioListener>();
            }
        }

        return backAudioSource2;
    }

    public AudioSource GetMicrophoneAudioSource()
    {
        if (microphoneAudioSource == null)
        {
            if (audioObject == null)
            {
                audioObject = new GameObject("AudioSourceRoot");
                audioObject.transform.SetParent(LSharpEntryGame.Instance.gameDontDestroy.transform);
            }

            microphoneAudioSource = audioObject.AddComponent<AudioSource>();

            if (audioListener == null)
            {
                audioListener = audioObject.AddComponent<AudioListener>();
            }
        }

        return microphoneAudioSource;
    }


    public void SetSoundVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }

    }

    public void SetBackGroundSoundVolume(float value)
    {
        if (backAudioSource != null)
        {
            backAudioSource.volume = value;
        }

        if (backAudioSource2 != null)
        {
            backAudioSource2.volume = value;
        }

    }

    public static void SetSoundValue()
    {
        PlayerPrefs.SetFloat("soundVolume", 1);
        PlayerPrefs.SetFloat("backgroundVolume", 1);
    }

    public static void GetSoundValueSaveValue()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            //GoableData.UIGameSettingData.soundValue = PlayerPrefs.GetFloat("soundVolume");
        }

        if (PlayerPrefs.HasKey("backgroundVolume"))
        {
            //GoableData.UIGameSettingData.backgroundSoundValue = PlayerPrefs.GetFloat("backgroundVolume");
        }
    }
}
