using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : MonoBehaviour
{
    public static Shit Instance;
    public GameObject IntroObj1;
    public GameObject IntroObj2;
    public GameObject Dialog2;
    public GameObject Dialog3;
    public GameObject Dialog4;
    public void OpenIntroObj1()
    {
        IntroObj1.SetActive(true);
    }
    public void OpenIntroObj2()
    {
        IntroObj2.SetActive(true);
    }
    public void OpenDialog2()
    {
        Dialog2.SetActive(true);
    }
    public void OpenDialog3()
    {
        Dialog3.SetActive(true);
    }
    public void OpenDialog4()
    {
        Dialog4.SetActive(true);
    }
}
