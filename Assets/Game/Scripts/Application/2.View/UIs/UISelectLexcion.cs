using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectLexcion : View {

    //选择相应词库的开关组件
    private Toggle toggle_Primary;
    private Toggle toggle_CET4;
    private Toggle toggle_CET6;
    private Toggle toggle_Postgraduate;

    private Button btn_OK;     //确认选择的词库按钮

    private WordLexicon wordLexicon = WordLexicon.None;

    public override string Name
    {
        get
        {
            return Consts.V_UISelectLexcion;
        }
    }

    public override void RegisterAttentionEvent()
    {
        this.AttentionEventList.Add(Consts.E_EnterScene);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e = data as SceneArgs;
                //当用户已选择词库时，进入到该场景不再显示选择词库界面
                if (StaticData.Instance.WordLexiconOption != WordLexicon.None && e.SceneIndex == 1)
                {
                    HideSelf();
                }

                if (e.SceneIndex == 1)
                {
                    switch (StaticData.Instance.WordLexiconOption)
                    {
                        case WordLexicon.None:
                            ShowSelf();
                            break;

                        case WordLexicon.Primary:
                            toggle_Primary.isOn = true;
                            break;

                        case WordLexicon.CET4:
                            toggle_CET4.isOn = true;
                            break;

                        case WordLexicon.CET6:
                            toggle_CET6.isOn = true;
                            break;

                        case WordLexicon.Postgraduate:
                            toggle_Postgraduate.isOn = true;
                            break;

                        default:
                            
                            break;
                    }
                }

                break;

            default:
                break;
        }
    }

    
    void Awake ()
    {

        toggle_Primary = transform.Find("SelectPrimary/IsSelect").GetComponent<Toggle>();
        toggle_CET4 = transform.Find("SelectCET4/IsSelect").GetComponent<Toggle>();
        toggle_CET6 = transform.Find("SelectCET6/IsSelect").GetComponent<Toggle>();
        toggle_Postgraduate = transform.Find("SelectPostgraduate/IsSelect").GetComponent<Toggle>();

        btn_OK = transform.Find("BtnOK").GetComponent<Button>();
        btn_OK.onClick.AddListener(OnSelectOK);

        toggle_Primary.onValueChanged.AddListener(isOn => OnSelectPrimary(isOn));
        toggle_CET4.onValueChanged.AddListener(isOn => OnSelectCTE4(isOn));
        toggle_CET6.onValueChanged.AddListener(isOn => OnSelectCTE6(isOn));
        toggle_Postgraduate.onValueChanged.AddListener(isOn => OnSelectPostgraduate(isOn));
    }

    
    public void OnSelectPrimary(bool isOn)
    {
        wordLexicon = WordLexicon.Primary;
        toggle_Primary.isOn = isOn;
    }

    public void OnSelectCTE4(bool isOn)
    {
        wordLexicon = WordLexicon.CET4;
        toggle_CET4.isOn = isOn;
    }

    public void OnSelectCTE6(bool isOn)
    {
        wordLexicon = WordLexicon.CET6;
        toggle_CET6.isOn = isOn;
    }

    public void OnSelectPostgraduate(bool isOn)
    {

        wordLexicon = WordLexicon.Postgraduate;
        toggle_Postgraduate.isOn = isOn;
    }

    /// <summary>
    /// 确认选择词库
    /// </summary>
    void OnSelectOK()
    {
        if (wordLexicon == WordLexicon.None)
        {
            wordLexicon = WordLexicon.Primary;
        }

        //数据类根据选择的词库初始化字典和相关资源
        StaticData.Instance.SelectWordLexcion(wordLexicon);

        Sound.Instance.wordLexcionOption = wordLexicon;
        //发送选择词库选项，UIStart脚本接受并处理
        SendEvent(Consts.E_WordLexcionOption);
        HideSelf();
    }

    public void ShowSelf()
    {
        gameObject.SetActive(true);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

}
