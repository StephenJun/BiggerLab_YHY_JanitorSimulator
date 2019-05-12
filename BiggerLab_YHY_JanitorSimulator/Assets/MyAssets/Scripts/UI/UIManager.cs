using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    public Dictionary<Type, UIScreen> screenDic;
    private Stack<UIScreen> screenStacks;
    private Transform uiRoot;

    public void Init()
    {
        screenDic = new Dictionary<Type, UIScreen>();
        screenStacks = new Stack<UIScreen>();
        uiRoot = GameObject.Find("Canvas").transform;
        UIScreen[] screens = ResourceLoader.Instance.LoadAll<UIScreen>("UIScreens");
        for (int i = 0; i < screens.Length; i++)
        {
            screenDic.Add(screens[i].GetType(), screens[i]);
        }
    }

    //多态， 泛型， where用于约束泛型
    public T Push<T>(object[] data, bool hidePrevious = true) where T : UIScreen
    {
        return (T)Push(typeof(T), data,  hidePrevious);
    }

    //打开一个新的窗口，并选择是否叠在前一个窗口上
    public UIScreen Push(Type screenType, object[] data, bool hidePrevious = true)
    {
        if (!screenDic.ContainsKey(screenType))
        {
            Debug.LogWarning("You are trying to open a screen not included in the folder");
            return null;
        }

        if (hidePrevious && screenStacks.Count > 0)
        {
            screenStacks.Peek().gameObject.SetActive(false);
            screenStacks.Peek().OnHide();
        }
        UIScreen screenToPush = UnityEngine.Object.Instantiate(screenDic[screenType]);
        screenToPush.OnInit(data);
        screenToPush.OnShow();
        screenToPush.gameObject.name = screenType.Name;
        screenToPush.transform.SetParent(uiRoot);
        screenStacks.Push(screenToPush);
        return screenToPush;
    }

    //关闭当前打开的窗口，并打开前一个窗口
    public UIScreen Pop(bool destroy = true)
    {
        UIScreen screenToPop = screenStacks.Pop();
        if (destroy)
        {
            UnityEngine.Object.Destroy(screenToPop.gameObject);
        }
        else
        {
            screenToPop.gameObject.SetActive(false);
        }
        screenStacks.Peek().gameObject.SetActive(true);
        screenStacks.Peek().OnShow();
        return screenToPop;
    }
}
