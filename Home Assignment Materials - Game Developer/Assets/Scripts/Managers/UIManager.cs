using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    [SerializeField] private List<UIScreen> screens;
    [SerializeField] private ToastFeedback toastFeedbackPrefab;
    private ToastFeedback currentToast;
    private Stack<UIScreen> screenStack = new Stack<UIScreen>();
    public UIScreen CurrentScreen => screenStack.Count > 0 ? screenStack.Peek() : null;

    private void Start()
    {
        foreach (UIScreen screen in screens)
        {
            screen.OnShowEvent += HandleScreenShown;
            screen.OnHideEvent += HandleScreenHidden;
        }
        screenStack.Push(GetScreen<LoadoutScreen>());
    }

    private void HandleScreenShown(UIScreen screen)
    {
        if (screenStack.Count == 0 || screenStack.Peek() != screen)
        {
            screenStack.Push(screen);
        }
    }

    private void HandleScreenHidden(UIScreen screen)
    {
        if (screenStack.Contains(screen))
        {
            screenStack = new Stack<UIScreen>(screenStack.Where(s => s != screen).Reverse());
        }
    }

    public UIScreen GetScreen<T>()
    {
        return screens.Find(screen => screen is T);
    }

    public void Toast(string message)
    {
        if (currentToast != null)
        {
            Destroy(currentToast.gameObject);
        }
        Transform parentTransform = null;
        if (CurrentScreen == null)
        {
            parentTransform = GetScreen<LoadoutScreen>().transform;
        }
        else
        {
            parentTransform = CurrentScreen.transform;
        }
        ToastFeedback toast = Instantiate(toastFeedbackPrefab, parentTransform);
        toast.Show(message);
    }
}
