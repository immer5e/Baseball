using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void Rule()
    {

    }
    public void Maker()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
