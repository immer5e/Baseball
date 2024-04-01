using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject BALL;
    public GameObject PC;
    public GameObject P1;
    public GameObject P2;

    Animator animator_BALL;
    Animator animator_PC;
    Animator animator_P1;
    Animator animator_P2;

    private bool randomBool;
    private bool Begin = false;
    private bool End = false;
    private bool Swap = true;
    private float timer;

    void Start()
    {
        // 获取Animator组件
        animator_BALL = BALL.GetComponent<Animator>();
        animator_PC = PC.GetComponent<Animator>();
        animator_P1 = P1.GetComponent<Animator>();
        animator_P2 = P2.GetComponent<Animator>();

        animator_P1.SetBool("Batter1", Swap);
        animator_P2.SetBool("Batter1", !Swap);

        animator_BALL.SetBool("Begin", Begin);
        animator_PC.SetBool("Begin", Begin);
        animator_P1.SetBool("Begin", Begin);
        animator_P2.SetBool("Begin", Begin);

        animator_BALL.SetBool("End", End);
        animator_PC.SetBool("End", End);
        animator_P1.SetBool("End", End);
        animator_P2.SetBool("End", End);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))                //按R初始化
        {
            Initialize();
        }

        if (timer > 2 && Begin == true)                  //3S倒计时，玩家可以开始操控
        {
            animator_PC.SetBool("Begin", Begin);
            animator_P1.SetBool("Begin", Begin);
            animator_P2.SetBool("Begin", Begin);
        }
        if (timer > 3 && Begin == true)                  //3.5S后发球
        {
            animator_BALL.SetBool("Begin", Begin);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Swap)
            {
                animator_P1.SetBool("Up", true);
                FightManager.fightManager.Log("Hit", 1);
            }
            else
            {
                animator_P2.SetBool("Up", true);
                FightManager.fightManager.Log("Dodge", 1);
            }
            StartCoroutine(ResetBoolP1(2F));
        }           //志安按下W

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Swap)
            {
                animator_P1.SetBool("Down", true);
                FightManager.fightManager.Log("Hit", 2);
            }
            else
            {
                animator_P2.SetBool("Down", true);
                FightManager.fightManager.Log("Dodge", 2);
            }
            StartCoroutine(ResetBoolP1(2F));
        }           //志安按下S

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Swap)
            {
                animator_P2.SetBool("Up", true);
                FightManager.fightManager.Log("Dodge", 1);
            }
            else
            {
                animator_P1.SetBool("Up", true);
                FightManager.fightManager.Log("Hit", 1);
            }
            StartCoroutine(ResetBoolP2(2F));
        }           //乔然按下W

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Swap)
            {
                animator_P2.SetBool("Down", true);
                FightManager.fightManager.Log("Dodge", 2);
            }
            else
            {
                animator_P1.SetBool("Down", true);
                FightManager.fightManager.Log("Hit", 2);
            }
            StartCoroutine(ResetBoolP2(2F));
        }           //乔然按下S

        if (timer > 5 && Begin == true)                  //公布答案
        {
            animator_PC.SetBool("End", true);
        }


        if (timer > 6 && Begin == true)
        {
            timer = 0;

            End = true;
            animator_BALL.SetBool("End", End);
            animator_P1.SetBool("End", End);
            animator_P2.SetBool("End", End);

            Begin = false;
            animator_BALL.SetBool("Begin", Begin);
            animator_PC.SetBool("Begin", Begin);
            animator_P1.SetBool("Begin", Begin);
            animator_P2.SetBool("Begin", Begin);

            animator_P2.SetBool("Hit", false);

            Swap = !Swap;
            animator_P1.SetBool("Batter1", Swap);            //交换顺序
            animator_P2.SetBool("Batter1", !Swap);
            //StartCoroutine(ResetSwap(3F));
            //Initialize();

            //结算回合
            FightManager.fightManager.GeneraResult();
        }                       //一轮结束的后处理
        Debug.Log(Swap);
    }

    void Initialize() //初始化回合
    {
        Begin = true;

        End = false;
        animator_BALL.SetBool("End", End);
        animator_PC.SetBool("End", End);
        animator_P1.SetBool("End", End);
        animator_P2.SetBool("End", End);

        timer = 0;

        randomBool = Random.Range(0, 2) == 0;           // 生成随机布尔值
        animator_BALL.SetBool("BALL_UP", randomBool);   //发球随机
        if (randomBool)
        {
            FightManager.fightManager.Log("Throw", 1);
        }
        else
        {
            FightManager.fightManager.Log("Throw", 2);
        }


        //Swap = !Swap;
        //animator_P1.SetBool("Batter1", Swap);            //交换顺序
        //animator_P2.SetBool("Batter1", !Swap);

    }

    IEnumerator ResetBoolP1(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Swap)
        {
            animator_P1.SetBool("Up", false);
            animator_P1.SetBool("Down", false);
        }
        else
        {
            animator_P2.SetBool("Up", false);
            animator_P2.SetBool("Down", false);
        }
    }       //重置志安WS

    IEnumerator ResetBoolP2(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Swap)
        {
            animator_P2.SetBool("Up", false);
            animator_P2.SetBool("Down", false);
        }
        else
        {
            animator_P1.SetBool("Up", false);
            animator_P1.SetBool("Down", false);
        }
    }       //重置乔安上下

    IEnumerator ResetSwap(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator_P1.SetBool("Batter1", Swap);            //交换顺序
        animator_P2.SetBool("Batter1", !Swap);
    }
}
