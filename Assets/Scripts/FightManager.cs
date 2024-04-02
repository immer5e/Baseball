using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager fightManager;
    public enum ThrowStyle
    {
        none = 0,
        up,
        down
    }
    public enum HitStyle
    {
        none = 0,
        up,
        down
    }
    public enum DodgeStyle
    {
        none = 0,
        up,
        down
    }
    [Serializable]
    public class RoundScore
    {
        public bool _type;
        public ThrowStyle throwRc;
        public HitStyle hitRc;
        public DodgeStyle dodgeRc;
        public int _player1;
        public int _player2;
    }
    [Header("UI Log")]
    public TextMeshProUGUI round_GUI;
    public TextMeshProUGUI total1_GUI;
    public TextMeshProUGUI total2_GUI;
    public TextMeshProUGUI log1_GUI;
    public TextMeshProUGUI log2_GUI;
    public TextMeshProUGUI log3_GUI;
    public List<RoundScore> _scoreRecord = new();
    public int _currentRound;
    public int _score1;
    public int _score2;
    private void Awake()
    {
        _score1 = 0;
        _score2 = 0;
        _currentRound = 0;
        if (fightManager)
        {
            Destroy(gameObject);
        }
        else
        {
            fightManager = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateUI()
    {
        round_GUI.text = $"第{_currentRound + 1}回合";
        int score1 = 0;
        foreach (var R in _scoreRecord)
        {
            score1 += R._player1;
        }
        total1_GUI.text = $"player1\t: {score1}分";
        int score2 = 0;
        foreach (var R in _scoreRecord)
        {
            score2 += R._player2;
        }
        total2_GUI.text = $"player2\t: {score2}分";
        log1_GUI.text = log2_GUI.text;
        log2_GUI.text = log3_GUI.text;
        string str0;
        string str1;
        string str2;
        RoundScore round = _scoreRecord[_currentRound];
        //球
        str0 = round.throwRc == (ThrowStyle)1 ? "上" :
        "下";
        if (round._type)
        {
            //击球者
            str2 = round.hitRc == (HitStyle)0 ? "中" :
            round.hitRc == (HitStyle)1 ? "击上" :
            "击下";
            //躲球者
            str1 = round.dodgeRc == (DodgeStyle)0 ? "中" :
            round.dodgeRc == (DodgeStyle)1 ? "躲上" :
            "躲下";
        }
        else
        {
            //击球者
            str1 = round.hitRc == (HitStyle)0 ? "中" :
            round.hitRc == (HitStyle)1 ? "击上" :
            "击下";
            //躲球者
            str2 = round.dodgeRc == (DodgeStyle)0 ? "中" :
            round.dodgeRc == (DodgeStyle)1 ? "躲上" :
            "躲下";
        }
        log3_GUI.text = $"回合{_currentRound + 1}：球-" + str0 + " P1-" + str1 + " P2-" + str2;
    }
    public void Log(string name, int rc)
    {
        RoundScore round = _scoreRecord[_currentRound];
        if (name == "Throw")
        {
            if (round.throwRc == 0)
                round.throwRc = (ThrowStyle)rc;
            else
                return;
        }
        else if (name == "Hit")
        {
            if (round.hitRc == 0)
                round.hitRc = (HitStyle)rc;
            else
                return;
        }
        else if (name == "Dodge")
        {
            if (round.dodgeRc == 0)
                round.dodgeRc = (DodgeStyle)rc;
            else
                return;
        }
        else
        {
            Debug.Log("传入了意外的参数");
        }
    }
    public void GeneraResult()
    {
        RoundScore round = _scoreRecord[_currentRound];
        int throwRc = (int)round.throwRc;
        int hitRc = (int)round.hitRc;
        int dodgeRc = (int)round.dodgeRc;
        if (throwRc == hitRc)
        {
            if (dodgeRc != 0)
            {
                Score(2, 0);
                //不信任
            }
            else
            {
                Score(4, 4);
                //完美信任
            }
        }
        else
        {
            if (dodgeRc == 0)
            {
                //呆瓜
                if (_currentRound <= 3)
                    Score(-2, -2);
                else
                    Score(-2, -4);
            }
            else if (throwRc == dodgeRc)
            {
                //无效闪避
                if (_currentRound <= 3)
                    Score(-2, -2);
                else
                    Score(0, -2);
            }
            else
            {
                //精彩闪避
                Score(0, 5);
            }
        }
        UpdateUI();
        _currentRound++;

        void Score(int hiter, int dodger)
        {
            if (!round._type)
            {
                round._player1 = hiter;
                round._player2 = dodger;
            }
            else
            {
                round._player1 = dodger;
                round._player2 = hiter;
            }
        }
    }
}
