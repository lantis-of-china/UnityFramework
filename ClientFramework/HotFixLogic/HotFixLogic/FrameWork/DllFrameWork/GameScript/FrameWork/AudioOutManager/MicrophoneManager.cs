using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class MicrophoneManager
{
    /// <summary>
    /// 标识当前是否播放中
    /// </summary>
	public bool curPlay;
    /// <summary>
    /// 等待播放的语音队列
    /// </summary>
	public List<object> taskList = new List<object>();
    /// <summary>
    /// 添加一个语音播放任务到队列
    /// </summary>
    /// <param name="recordUrlPath"></param>
    /// <param name="userId"></param>
    /// <param name="time"></param>
    /// <param name="AudioMsgFishCallFun"></param>
	public void AddTask(string recordUrlPath, int userId, float time, AudioMsgFinishCall AudioMsgFishCallFun)
    {
        object[] task = new object[] { recordUrlPath, userId, time, AudioMsgFishCallFun };
        taskList.Add(task);
        PlayTask();
    }

    /// <summary>
    /// 从队列中获取下一个播放任务 
    /// </summary>
    /// <returns></returns>
	public object GetNextTask()
    {
        if (taskList.Count > 0)
        {
            object task = taskList[0];
            taskList.RemoveAt(0);
            return task;
        }

        return null;
    }

    /// <summary>
    /// 清理播放任务和停止播放
    /// </summary>
	public void Clear()
    {
        curPlay = false;
        taskList.Clear();
        IEnumeratorManager.Instance.StopGroupAll("RecordPlayAudio");
    }

    /// <summary>
    /// 播放队列中的语音任务
    /// </summary>
	public void PlayTask()
    {
        if (!curPlay)
        {
            object nextTask = GetNextTask();

            if (nextTask != null)
            {
                curPlay = true;
                object[] taskParamar = (object[])nextTask;
                DownLoadRecord((string)taskParamar[0], (int)taskParamar[1], (float)taskParamar[2], (AudioMsgFinishCall)taskParamar[3]);
            }
        }
    }

    public void RecordPlayAudio(float time)
    {
        IEnumeratorManager.Instance.StartCoroutine(IERecordPlayAudio(time), "RecordPlayAudio");
    }

    public IEnumerator IERecordPlayAudio(float time)
    {
        curPlay = false;

        yield return new IEnumeratorManager.WaitForSeconds(time);

        PlayTask();
    }

    /// <summary>
    /// 语音消息播放委托定义
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="time"></param>
    /// <param name="downLoadFilePath"></param>
    public delegate void AudioMsgFinishCall(int userId, float time, string downLoadFilePath);
    /// <summary>
    /// 播放源
    /// </summary>
    AudioSource audioSource = null;
    /// <summary>
    /// 单个clip的时间长度
    /// </summary>
    private const int audioClipLength = 6;
    /// <summary>
    /// 当前clip录音的长度
    /// </summary>
    private float audioClipTime = 0.0f;
    /// <summary>
    /// 设备名字
    /// </summary>
    private string driviceName = "";
    /// <summary>
    /// 存储所有的音效片段
    /// </summary>
    private List<AudioClip> audioClipList = new List<AudioClip>();
    /// <summary>
    /// 麦克风状态是否打开
    /// </summary>
    private bool openMicro = false;
    /// <summary>
    /// 采样长度 代表一秒钟的float数量
    /// 44100
    /// 22050
    /// 2205
    /// </summary>
    public int sampleLength = 11025;
    /// <summary>
    /// 音量
    /// </summary>
    public float microVosice = 3.5f;
    /// <summary>
    /// 上传完成调用
    /// </summary>
    public Action<string, string> uploadCallSucess;
    /// <summary>
    /// 标识当前录音是否需要上传
    /// </summary>
    public bool needSend = true;

    /// <summary>
    /// 实例化
    /// </summary>
    public void Init()
    {
        audioSource = FrameWorkDrvice.AudioOutManagerInstance.GetMicrophoneAudioSource();
        audioSource.mute = false;
        CheckHasMicrophone();
    }

    /// <summary>
    /// 检测是否有可用麦克风
    /// </summary>
    /// <returns></returns>
    public bool CheckHasMicrophone()
    {
        if (Microphone.devices != null && Microphone.devices.Length > 0)
        {
            Debug.Log("Microphone 检测到有效的麦克风 数量 " + Microphone.devices.Length);
            driviceName = Microphone.devices[0];
        }
        else
        {
            Debug.LogError("Microphone 没有检测到有效的麦克风 数量 " + Microphone.devices.Length);
            return false;
        }


        if (string.IsNullOrEmpty(driviceName))
        {
            Debug.LogError("Microphone 没有检测到有效的麦克风 数量 " + Microphone.devices.Length);

            return false;
        }

        return true;
    }

    /// <summary>
    /// 关闭录音
    /// </summary>
	public void ClearMicroEnd()
    {
        needSend = false;
        EndMicro(null);
    }

    /// <summary>
    /// 开始录音
    /// </summary>
    public void StarMicro()
    {
        if (CheckHasMicrophone())
        {
            needSend = true;
            openMicro = true;
            audioClipList.Clear();
            audioClipTime = audioClipLength;
            FrameWorkDrvice.AudioOutManagerInstance.ActiveBackAudioAndSampeAudio(false);
        }
    }

    /// <summary>
    /// 结束录音
    /// </summary>
    public void EndMicro(Action<string, string> callB)
    {
        openMicro = false;
        audioClipTime = audioClipLength;
        int position = Microphone.GetPosition(null);
        Microphone.End(driviceName);
        float length = position / sampleLength;
        FrameWorkDrvice.AudioOutManagerInstance.ActiveBackAudioAndSampeAudio(true);

        if (length > 1.0f && needSend)
        {
            UploadMicro();
        }
    }

    public void UploadMicro()
    {
        var datas = GetClipDatas();

        if (datas != null && datas.Length > 0)
        {
            string filePath = string.Format("{0}/{1}.wav", Application.persistentDataPath, DateTime.Now.ToFileTime());
            var bytes = CSTools.FloatArrayToByteArray(datas);
            File.WriteAllBytes(filePath, bytes);
            var oldSize = bytes.Length;
            bytes = CompressEncryption.CompressEncryptionData(bytes);
            Debug.Log($"newSize:{bytes.Length} oldSize:{oldSize}");
            var jsonMap = new Dictionary<string, string>()
            {
                { "type","sound"},
                { "suffix","wav"},
                { "base64String",Convert.ToBase64String(bytes)},
            };

            var jsonString = LitJson.JsonMapper.ToJson(jsonMap);

            HttpTools.PostHttpData("http://lantis.club:56985", System.Text.Encoding.UTF8.GetBytes(jsonString), (json) =>
            {
                UpLoadFileCallBack(filePath, json);
            });
        }
    }

    public void UpLoadFileCallBack(string filePath, string json)
    {
        DebugLoger.Log("开始语音上传回调!");
        LitJson.JsonData jsdata = LitJson.JsonMapper.ToObject(json);

        if (jsdata != null && int.Parse(jsdata["result"].ToString()) == 0)
        {
            var url = jsdata["url"].ToString();

            if (uploadCallSucess != null)
            {
                uploadCallSucess(filePath, url);
            }
        }
        else
        {
            UINameSpace.UITipMessage.PlayMessage("语音上传失败!");
        }
    }

    /// <summary>
    /// 下载播放语音
    /// </summary>
    /// <param name="recordUrlPath"></param>
    public void DownLoadRecord(string recordUrlPath, int userId, float time, AudioMsgFinishCall AudioMsgFishCallFun)
    {
        DebugLoger.Log("下载地址:" + recordUrlPath);
        string DownLoadfilePath = string.Format("{0}/{1}.wav", Application.persistentDataPath, DateTime.Now.ToFileTime());

        HttpTools.GetHttpData(recordUrlPath, (byte[] bytes) =>
        {
            if (bytes != null && bytes.Length > 0)
            {
                bytes = CompressEncryption.UnCompressDecompressData(bytes);
                File.WriteAllBytes(DownLoadfilePath, bytes);
                AudioMsgFishCallFun(userId, time, DownLoadfilePath);
            }
        });
    }


    /// <summary>
    /// 打开录音
    /// </summary>
    /// <param name="_audioClipLength"></param>
    /// <returns></returns>
    public AudioClip OpenMicro(int _audioClipLength)
    {
        Microphone.End(null);
        AudioClip ac = Microphone.Start(driviceName, true, _audioClipLength, sampleLength);
        return ac;
    }

    /// <summary>
    /// 获取全部语音片段整合的语音数据
    /// </summary>
    public float[] GetAllClipData()
    {
        List<float[]> bufDataList = GetMicroData();

        if (bufDataList.Count != 0)
        {
            int floatLength = 0;

            for (int l = 0; l < bufDataList.Count; ++l)
            {
                float[] buf = bufDataList[l];

                floatLength += buf.Length;
            }

            int index = 0;
            float[] sampleBuf = new float[floatLength];

            for (int l = 0; l < bufDataList.Count; ++l)
            {
                float[] buf = bufDataList[l];
                buf.CopyTo(sampleBuf, index);
                index += buf.Length;
            }

            return sampleBuf;
        }

        return null;
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    public float[] GetClipDatas()
    {
        var datas = GetAllClipData();

        if (datas != null)
        {
            CSTools.SetFloatVosice(datas, microVosice);
        }

        return datas;
    }

    /// <summary>
    /// 播放声音
    /// </summary>
    public void PlayRecordAudio()
    {
        float[] bufDataList = GetAllClipData();

        if (bufDataList != null)
        {
            PlayClipData(bufDataList);
        }
    }

    /// <summary>
    /// 在Update中进行录音检测
    /// </summary>
    public void Update()
    {
        if (openMicro && audioClipTime >= audioClipLength)
        {
            audioClipTime = 0.0f;

            AudioClip aC = OpenMicro(audioClipLength);

            audioClipList.Add(aC);
        }
        else if (openMicro)
        {
            audioClipTime += Time.deltaTime;
        }
    }


    /// <summary>
    /// 获取对话数据
    /// </summary>
    public List<float[]> GetMicroData()
    {

        List<float[]> audioBufList = new List<float[]>();

        if (audioClipList != null && audioClipList.Count > 0)
        {
            for (int loop = 0; loop < audioClipList.Count; loop++)
            {
                AudioClip ac = audioClipList[loop];
                float[] audioBuf = GetMicroData(ac);

                if (audioBuf != null && audioBuf.Length > 0)
                {
                    audioBufList.Add(audioBuf);
                }
            }
        }

        return audioBufList;
    }


    /// <summary>
    /// 对吼数据转到字节
    /// </summary>
    /// <param name="_ac"></param>
    /// <returns></returns>
    public float[] GetMicroData(AudioClip _ac)
    {
        if (_ac == null)
        {
            DebugLoger.Log("GetClipData audio.clip is null");
            return null;
        }

        float[] samples = new float[_ac.samples];
        _ac.GetData(samples, 0);

        return samples;
    }

    /// <summary>
    /// 播放丫丫语音
    /// </summary>
    /// <param name="recordPath"></param>
    public void PlayAudio(string recordPath)
    {
        PlayClipDataFromPath(recordPath);
    }

    /// <summary>
    /// 播放对话数据
    /// </summary>
    /// <param name="_playData"></param>
    public void PlayClipData(float[] _playData)
    {
        if (_playData == null)
        {
            Debug.LogError("play data is null");
            return;
        }

        audioSource.Stop();

        if (audioSource.clip == null)
        {
            audioSource.clip = AudioClip.Create("playRecordClip", _playData.Length, 1, sampleLength, false);
            audioSource.spatialBlend = 1.0f;
        }
        else
        {
            AudioClip acDes = audioSource.clip;
            audioSource.clip = null;
            UnityEngine.Object.Destroy(acDes);

            audioSource.clip = AudioClip.Create("playRecordClip", _playData.Length, 1, sampleLength, false);
            audioSource.spatialBlend = 1.0f;
        }

        audioSource.clip.SetData(_playData, 0);
        audioSource.PlayOneShot(audioSource.clip);
    }


    /// <summary>
    /// 播放对话数据
    /// </summary>
    /// <param name="_playData"></param>
    public void PlayClipDataFromPath(string _playPath)
    {
        byte[] _playData = null;

        if (!System.IO.File.Exists(_playPath))
        {
            return;
        }

        using (System.IO.FileStream fs = System.IO.File.Open(_playPath, System.IO.FileMode.Open))
        {
            _playData = new byte[fs.Length];
            fs.Read(_playData, 0, _playData.Length);
        }

        if (_playData == null)
        {
            Debug.LogError("play data is null");
            return;
        }

        audioSource.Stop();

        if (audioSource.clip == null)
        {
            audioSource.clip = AudioClip.Create("playRecordClip", _playData.Length, 1, sampleLength, false);
            audioSource.spatialBlend = 1.0f;
        }
        else
        {
            AudioClip acDes = audioSource.clip;
            audioSource.clip = null;
            UnityEngine.Object.Destroy(acDes);

            audioSource.clip = AudioClip.Create("playRecordClip", _playData.Length, 1, sampleLength, false);
            audioSource.spatialBlend = 1.0f;
        }

        audioSource.clip.SetData(CSTools.ByteArrayToFloatArray(_playData), 0);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
