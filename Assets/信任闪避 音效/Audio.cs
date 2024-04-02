using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;
    public AudioSource hitBall;
    public AudioSource throwBall;
    public AudioSource flyBall;
    public AudioSource behit;
    public AudioSource jump;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void HitBall()
    {
        hitBall.Play();
    }
    public void ThrowBall()
    {
        throwBall.Play();
    }
    public void Behit()
    {
        behit.Play();
    }
    public void Jump()
    {
        jump.Play();
    }
}
