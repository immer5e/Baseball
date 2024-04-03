using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shrink : MonoBehaviour
{
    public Animator _animator;
    public TextMeshProUGUI _textGUI;
    public Image ImageObj;

    public void SetText(string text)
    {
        _textGUI.text = text;
    }

    [ContextMenu("缩小")]
    public void DoShrink()
    {
        _animator.Play("Shrink", 0, 0f);
    }
    [ContextMenu("看b见")]
    public void InVisible()
    {
        _textGUI.enabled = false;
        ImageObj.enabled = false;
    }
    [ContextMenu("看见")]
    public void Visible()
    {
        _textGUI.enabled = true;
        ImageObj.enabled = true;
    }
}
