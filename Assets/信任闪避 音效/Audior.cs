using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audior : MonoBehaviour
{
    public static Audior Instance;
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
    [ContextMenu("打球")]
    public void HitBall()
    {
        hitBall.Play();
    }
    [ContextMenu("扔球")]
    public void ThrowBall()
    {
        throwBall.Play();
    }
    [ContextMenu("被球击中")]
    public void Behit()
    {
        behit.Play();
    }
    [ContextMenu("跳跃")]
    public void Jump()
    {
        jump.Play();
    }
}
