using UnityEngine;

public class ExitSceneCommand : Controller
{
    public override void Execute(object data)
    {
        //对象池回收所有对象
        Game.Instance.a_ObjectPool.UnspawnAll();

        SceneArgs e = data as SceneArgs;
        if (e.SceneIndex == 3)
        {            
            GameModel gm = GetModel<GameModel>();
            //保存单词信息数据
            gm.SaveWordListToJson();
        }
        Time.timeScale = 1;
    }
}

