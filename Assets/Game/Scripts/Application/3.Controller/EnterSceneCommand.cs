using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnterSceneCommand : Controller
{
    public override void Execute(object data)
    {
        //注册视图
        SceneArgs e = data as SceneArgs;
        Game.Instance.HideSceneLoadProcess();

        switch (e.SceneIndex)
        {
            case 0://Init

                break;

            case 1://Start
                RegisterView(GameObject.Find("UIStart").GetComponent<UIStart>());
                RegisterView(GameObject.Find("UIStart/UISelectLexicon").GetComponent<UISelectLexcion>());
                //玩家角色生成
                Game.Instance.a_ObjectPool.ResourcesDir = "Prefabs";
                Game.Instance.a_ObjectPool.Spawn("Player", Vector3.zero);
               
                break;

            case 2://Select                
                break;

            case 3://Level
                RegisterView(GameObject.Find("GameScene").GetComponent<Spawner>());

                Transform canvas = GameObject.Find("Canvas").transform;
                RegisterView(canvas.Find("UIWin").GetComponent<UIWin>());
                RegisterView(canvas.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(canvas.Find("UICountDown").GetComponent<UICountDown>());
                RegisterView(canvas.Find("UIAnswer").GetComponent<UIAnswer>());
                Game.Instance.a_StaticData.InitPlayerSpeed();
                Game.Instance.a_GameData.InitAllData();
                break;

            case 4://Complete
                RegisterView(GameObject.Find("UIComplete").GetComponent<UIComplete>());
                break;

            case 5:
                
                break;
            default:
                break;
        }
    }
}

