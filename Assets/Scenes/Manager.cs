using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    private bool isBall = true;
    private bool randomBool;
    private bool Begin = false;
    private bool End = false;
    private bool Swap = true;
    private float timer;
    private bool hitBall = false;
    private bool throwBall = false;
    private bool behit = false;
    private bool jump = false;
    public GameObject Dialog3;
    public GameObject onable1;
    private void OnEnable()
    {

    }
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

        if (timer > 2 && Begin == true)                  //2S倒计时,PC开始动画
        {
            animator_PC.SetBool("Begin", Begin);
            //animator_P1.SetBool("Begin", Begin);
            //animator_P2.SetBool("Begin", Begin);
        }

        if (timer > 3 && Begin == true)                  //3S后发球，玩家可以开始操控
        {
            if (throwBall)
            {
                Audior.Instance.ThrowBall();
                throwBall = false;
            }
            animator_BALL.SetBool("Begin", Begin);
            animator_P1.SetBool("Begin", Begin);
            animator_P2.SetBool("Begin", Begin);
        }

        if (timer > 3.5 && Begin == true && Swap == true && (randomBool == true && animator_P1.GetBool("Up") == true || randomBool == false && animator_P1.GetBool("Down") == true))
        {
            BALL.SetActive(false);
            isBall = false;
            if (hitBall)
            {
                Audior.Instance.HitBall();
                hitBall = false;
            }
            Debug.Log("Hit");           //男在前
        }

        if (timer > 3.5 && Begin == true && Swap == false && (randomBool == true && animator_P1.GetBool("Up") == true || randomBool == false && animator_P1.GetBool("Down") == true))
        {
            if (hitBall)
            {
                Audior.Instance.HitBall();
                hitBall = false;
            }
            BALL.SetActive(false);
            isBall = false;
            Debug.Log("Hit");           //女在前
        }


        if (timer > 4 && Begin == true && Swap == true && isBall == true && (randomBool == true && animator_P2.GetBool("Down") == false || randomBool == false && animator_P2.GetBool("Up") == false))
        {
            if (behit)
            {
                Audior.Instance.Behit();
                behit = false;
            }
            animator_P2.SetBool("Hit", true); //女在后
        }

        if (timer > 4 && Swap == false && Begin == true && isBall == true && (randomBool == true && animator_P2.GetBool("Down") == false || randomBool == false && animator_P2.GetBool("Up") == false))
        {
            if (behit)
            {
                Audior.Instance.Behit();
                behit = false;
            }
            animator_P2.SetBool("Hit", true); //男在后
        }


        if (Input.GetKeyDown(KeyCode.W) && animator_P2.GetBool("Hit") == false && (Swap == true && animator_P1.GetBool("Down") == false || Swap == false && animator_P2.GetBool("Down") == false))
        {
            if (Swap)
            {
                animator_P1.SetBool("Up", true);
                FightManager.fightManager.Log("Hit", 1);
            }
            else
            {
                if (jump)
                {
                    Audior.Instance.Jump();
                    jump = false;
                }
                animator_P2.SetBool("Up", true);
                FightManager.fightManager.Log("Dodge", 1);
            }
            StartCoroutine(ResetBoolP1(1F));
        }           //志安按下W

        if (Input.GetKeyDown(KeyCode.S) && animator_P2.GetBool("Hit") == false && (Swap == true && animator_P1.GetBool("Up") == false || Swap == false && animator_P2.GetBool("Up") == false))
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
            StartCoroutine(ResetBoolP1(1F));
        }           //志安按下S

        if (Input.GetKeyDown(KeyCode.UpArrow) && animator_P2.GetBool("Hit") == false && (Swap == true && animator_P2.GetBool("Down") == false || Swap == false && animator_P1.GetBool("Down") == false))
        {
            if (Swap)
            {
                animator_P2.SetBool("Up", true);
                FightManager.fightManager.Log("Dodge", 1);
            }
            else
            {
                if (jump)
                {
                    Audior.Instance.Jump();
                    jump = false;
                }
                animator_P1.SetBool("Up", true);
                FightManager.fightManager.Log("Hit", 1);
            }
            StartCoroutine(ResetBoolP2(1F));
        }           //乔然按下W

        if (Input.GetKeyDown(KeyCode.DownArrow) && animator_P2.GetBool("Hit") == false && (Swap == true && animator_P2.GetBool("Up") == false || Swap == false && animator_P1.GetBool("Up") == false))
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
            StartCoroutine(ResetBoolP2(1F));
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

            //结算回合
            FightManager.fightManager.GeneraResult();

            if (FightManager.fightManager._currentRound == 12)
            {
                //游戏结束
                FightManager.fightManager.Fin();
            }
            else if (FightManager.fightManager._currentRound != 5)
            {
                StartCoroutine(IEInit());
            }
            else
            {
                onable1.SetActive(false);
                Dialog3.SetActive(true);
            }
            /*Initialize();*/
        }                       //一轮结束的后处理
    }

    IEnumerator IEInit()
    {
        yield return new WaitForSeconds(4f);
        Initialize();
    }

    public void Initialize() //初始化回合
    {
        hitBall = true;
        throwBall = true;
        behit = true;
        jump = true;


        isBall = true;
        BALL.SetActive(true);
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
