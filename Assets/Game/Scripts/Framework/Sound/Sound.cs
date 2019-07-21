using UnityEngine;
/// <summary>
/// 声音控制模板
/// </summary>
public class Sound : Singleton<Sound>
{
    public string ResourceDir = "Sounds";

    private AudioSource m_bgSource = null;      //背景音乐
    private AudioSource m_effectMusic = null;   //音效
    private AudioSource m_wordPronunciation = null;     //单词发音

    public WordLexicon wordLexcionOption;     //选择的词库发音

    protected override void Awake()
    {
        base.Awake();

        //添加声音组件
        m_bgSource = this.gameObject.AddComponent<AudioSource>();
        m_bgSource.playOnAwake = false;
        m_bgSource.loop = true;   //循环播放

        m_effectMusic = this.gameObject.AddComponent<AudioSource>();
        m_wordPronunciation = this.gameObject.AddComponent<AudioSource>();
       
    }

   

    /// <summary>
    /// 背景音乐大小
    /// </summary>
    public float BgVolume
    {
        get { return m_bgSource.volume; }
        set { m_bgSource.volume = value; }
    }

    /// <summary>
    /// 音效大小
    /// </summary>
    public float EffectMusicVolume
    {
        get { return  m_effectMusic.volume; }
        set { m_effectMusic.volume = value; }
    }

    public float WordPronunciarion
    {
        get { return m_wordPronunciation.volume; }
        set { m_wordPronunciation.volume = value; }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public void PlayBgMusic(string audioName)
    {
        //获得此时是否有正在播放的音乐
        string oldName = "";
        if (m_bgSource.clip != null)       
            oldName = m_bgSource.clip.name;       
        else       
            oldName = "";

        if (oldName != audioName)
        {
            //播放背景音乐
            string audioPath = "BgMusic/" + audioName;
            m_bgSource.clip = LoadClip(audioPath);
            m_bgSource.volume = 0.05f;
            m_bgSource.Play();
            
        }        
    }

    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    public void StopBgMusic()
    {
        m_bgSource.volume = 0;
        m_bgSource.Stop();
        m_bgSource.clip = null;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlayEffectMusic(string audioName)
    {
        string goldEffectPath = "EffectMusic/" + audioName;
        AudioClip clip = LoadClip(goldEffectPath);
        
        m_effectMusic.PlayOneShot(clip);
    }

    /// <summary>
    /// 启用音效
    /// </summary>
    public void PlayEffect()
    {
        EffectMusicVolume = 100;
    }

    /// <summary>
    /// 关闭音效
    /// </summary>
    public void StopEffect()
    {
        EffectMusicVolume = 0;
    }

    /// <summary>
    /// 播放单词发音
    /// </summary>
    public void PlayEnglishPronunciation(string aduioName)
    {
        //拼接英文发音资源路径
        string wordPath = "EnglishPronounce/" + wordLexcionOption.ToString() + "/" + aduioName;
        AudioClip clip = LoadClip(wordPath);

        m_wordPronunciation.PlayOneShot(clip);
    }

    /// <summary>
    /// 播放中文发音
    /// </summary>
    public void PlayChinesePronunciation(string aduioName)
    {
        string wordPath = "ChinesePronounce/" + wordLexcionOption.ToString() + "/" + aduioName;

        AudioClip clip = LoadClip(wordPath);

        m_wordPronunciation.PlayOneShot(clip);
    }

    /// <summary>
    /// 加载音频资源
    /// </summary>
    private AudioClip LoadClip(string audioName)
    {
        //确认路径
        string path = "";
        if (string.IsNullOrEmpty(ResourceDir))
        {
            path = audioName;
        }
        else
        {
            path = ResourceDir + "/" + audioName;
        }

        //根据路径获取资源
        AudioClip clip = Resources.Load<AudioClip>(path);
        return clip;
    }
}
