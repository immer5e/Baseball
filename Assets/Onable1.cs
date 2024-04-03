using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Onable1 : MonoBehaviour
{
    public Manager manager;
    private void OnEnable()
    {
        manager.Initialize();
    }
}
