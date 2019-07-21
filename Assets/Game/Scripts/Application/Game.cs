using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
[RequireComponent(typeof(GameData))]
public class Game : ApplicationBase<Game>
{
    //全局访问点
    public ObjectPool a_ObjectPool = null;  //对象池
    public Sound a_Sound = null;    //声音
    public StaticData a_StaticData = null;   //静态数据
    public GameData a_GameData = null;      //征战数据

    private SceneLoadProcess m_SceneLoadProcess;

    public bool IsOnPlay = false;    //是否正在进行游戏

    /// <summary>
    /// 加载场景
    /// </summary>
    public void LoadScene(int sceneLevel)
    {
        SceneArgs e = new SceneArgs();
        //获取当前场景的索引
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;

        //退出该场景事件发出
        SendEvent(Consts.E_ExitScene, e);

        //加载进度显示场景，进入目标场景
        ShowSceneLoadProcess(sceneLevel);
    }

    /// <summary>
    /// 当加载对应场景完成后
    /// </summary>
    void OnLevelWasLoaded(int sceneLevel)
    {
        SceneArgs e = new SceneArgs();
        e.SceneIndex = sceneLevel;

        //发送进入该场景事件，UICountDown脚本接受并处理
        SendEvent(Consts.E_EnterScene, e);
    }

    void Start()
    {
        
        Screen.SetResolution(720, 1280, false);
        //该脚本所在物品不会随场景加载而销毁
        GameObject.DontDestroyOnLoad(this.gameObject);

        //全局变量赋值       
        a_ObjectPool = ObjectPool.Instance;
        a_Sound = Sound.Instance;
        a_StaticData = StaticData.Instance;
        a_GameData = GameData.Instance;

        m_SceneLoadProcess = transform.Find("GameCanvas/UISceneLoadProcess").GetComponent<SceneLoadProcess>();
        HideSceneLoadProcess();

        //注册开始命令
        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));
        //进入游戏（发送命令），StartUpCommand接受处理该事件
        SendEvent(Consts.E_StartUp);
    }

    public void ShowSceneLoadProcess(int nextSceneIndex)
    {
        m_SceneLoadProcess.gameObject.SetActive(true);
        m_SceneLoadProcess.NextSceneIndex = nextSceneIndex;
        m_SceneLoadProcess.StartSceneLoad();
    }

    public void HideSceneLoadProcess()
    {
        m_SceneLoadProcess.HideSceneLoad();
    }

    public void GameSendEvent(string eventName,object data = null)
    {
        SendEvent(eventName, data);
    }
}
