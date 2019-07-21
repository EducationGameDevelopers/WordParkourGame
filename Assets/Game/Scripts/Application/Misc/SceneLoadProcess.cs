using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadProcess : MonoBehaviour
{
    public Image progressImg;
    private AsyncOperation async;
    public Text text;

    private int nextSceneIndex;

    private int curProgressVaule = 0;//计数器

    public int NextSceneIndex
    {
        get { return nextSceneIndex; }
        set { nextSceneIndex = value; }
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneIndex);//异步跳转到game场景
        async.allowSceneActivation = false;//当game场景加载到90%时，不让它直接跳转到game场景。
        yield return async;
    }

    public void StartSceneLoad()
    {
        StartCoroutine(LoadScene());
    }

    public void HideSceneLoad()
    {
        StopCoroutine(LoadScene());
        progressImg.fillAmount = 0;
        curProgressVaule = 0;
        async = null;
        gameObject.SetActive(false);
    }

    void Update()
    {

        if (async == null)
        {
            return;
        }

        int progressVaule = 0;

        if (async.progress < 0.9f)
        {
            progressVaule = (int)async.progress * 100;
        }
        else
        {
            progressVaule = 100;
        }

        if (curProgressVaule < progressVaule)
        {
            curProgressVaule++;
        }
        text.text = curProgressVaule + "%";
        progressImg.fillAmount = curProgressVaule / 100f;
        if (curProgressVaule == 100)
        {
            async.allowSceneActivation = true;
            
        }
    }
}
