using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectStages : MonoBehaviour {

	
	void Start () {
		
	}


    /// <summary>
    /// 隐藏关卡卡片选择界面
    /// </summary>
    public void HideSelectStage()
    {
        this.gameObject.SetActive(false);
    }
}
