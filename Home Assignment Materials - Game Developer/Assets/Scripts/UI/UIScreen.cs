using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    public System.Action<UIScreen> OnShowEvent;
    public System.Action<UIScreen> OnHideEvent;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        OnShow();
        OnShowEvent?.Invoke(this);
    }

    public virtual void Hide()
    {
        OnHide();
        gameObject.SetActive(false);
        OnHideEvent?.Invoke(this);
    }

    public abstract void OnShow();
    public abstract void OnHide();

}
