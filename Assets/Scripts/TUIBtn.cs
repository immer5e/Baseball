using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TUIBtn : MonoBehaviour
{
    public GameObject Canvas1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void RuleDisplay()
    {
        Time.timeScale = 0f;
        Canvas1.SetActive(true);
        Canvas1.GetComponent<TapCanvas>().Set(0);
    }
}
