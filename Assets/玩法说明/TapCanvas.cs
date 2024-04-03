using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TapCanvas : MonoBehaviour
{
    public List<Sprite> imageList = new();
    public Image showImage;
    public int index;
    public GameObject NextObj;
    private void Awake()
    {
        index = 0;
    }
    private void Start()
    {
        index = 0;
        UpdateShowImage();
    }
    [ContextMenu("看b见")]
    public void InVisible()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        gameObject.SetActive(false);
    }
    [ContextMenu("看见")]
    public void Visible()
    {
        gameObject.SetActive(true);
        index = 0;
        UpdateShowImage();
    }
    public void UpdateShowImage()
    {
        showImage.sprite = imageList[index];
    }
    public void Set(int n)
    {
        index = n;
        UpdateShowImage();
    }
    public void Next()
    {
        index = Mathf.Clamp(++index, 0, imageList.Count);
        if (index == imageList.Count)
        {
            InVisible();
            if (NextObj != null)
                NextObj.SetActive(true);
        }
        else
        {
            UpdateShowImage();
        }

    }
    public void Previous()
    {
        index = Mathf.Clamp(--index, 0, imageList.Count - 1);
        UpdateShowImage();
    }
}
