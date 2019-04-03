using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 使用方法：

    ///将这个脚本挂载在GameObject上

    ///将canvas中的panel拖拽到挂载该脚本的GameObject中的Inspector

    ///使用时UIManager.Instance.ShowPanel("panel");即可

 /*拓展：1.该方法只能处理单流程的情况，如果是同时显示多个panel时，可以给panel进行分类，不同类别用不同的栈进行管理起来。

           2.如果是大量的panel切换，不要使用SetActive()，会产生大量的GC Alloc 。*/
    /// </summary>
    public GameObject[] UIPanel;
    public GameObject menuPanel;
    private GameObject errorObj;
    private static GameObject thisGameObejct;
    private int UIPanelSumSize;
    public static Stack<string> panelStack = new Stack<string>();
    // private UIManager() { }
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = thisGameObejct.GetComponent<UIManager>();
            }
            return _instance;

        }
    }

    void Awake()
    {
        thisGameObejct = gameObject;
        UIPanelSumSize = UIPanel.Length;

    }
    void Start()
    {
     
        HideAllPaenl();
        ShowPanel("MainPanel");//初始化界面
    }
    public void ShowPanel(string panelName)
    {
        //todo 这里的循环如果是针对一个页面进一个页面出，是没必要的，如果是复杂的情况下需要另行考虑，比如说某几个页面相互关联，需要同时显示或者隐藏
        for (int j = 0; panelStack.Count > j; j++)
        {
            var str = panelStack.Pop();
            var hidePanel = FindObj(str);
            if (hidePanel == null)
                return;
            hidePanel.SetActive(false);
        }

       
        var showPanel = FindObj(panelName);
        if (showPanel == null)
        {
            Debug.Log("没找都panel");
            return;


        }
     
        showPanel.SetActive(true);
    
        panelStack.Push(panelName);

    }
    private GameObject FindObj(string objName)
    {
        for (int i = 0; UIPanelSumSize > i; i++)
        {
            if (UIPanel[i].name == objName)
                return UIPanel[i];
            else
                continue;
        }
        return errorObj;
    }
    public void HideAllPaenl()
    {
        if (UIPanelSumSize > 0)
        {
            foreach (var item in UIPanel)
            {
                item.SetActive(false);
            }
        }
      
    }
}
