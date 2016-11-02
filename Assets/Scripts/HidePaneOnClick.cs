using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HidePaneOnClick : MonoBehaviour {
    public GameObject PaneToHide;
    private bool IsVisible;

    void Start()
    {
        IsVisible = false;
    }

    void OnSelect()
    {
        IsVisible = !IsVisible;
        PaneToHide.SetActive(IsVisible);
        if (IsVisible)
        {
            gameObject.GetComponentInChildren<Text>().text = "Hide";
        } else
        {
            gameObject.GetComponentInChildren<Text>().text = "Show";
        }
    }

    public bool IsTargetObjectHiding()
    {
        return IsVisible;
    }
}
