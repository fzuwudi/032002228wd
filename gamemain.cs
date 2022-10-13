using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemain : MonoBehaviour
{
    //棋盘格子抽象为两个3×3数组
    public int[,] boarda = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    public int[,] boardb = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    // Start is called before the first frame update
    //count用于表示本轮应该是哪一方出棋，
    //在双人模式中：玩家A的count为1，玩家B的count为0
    //在AI模式中：玩家A的count为1，电脑玩家的count为-1.
    public int count;

    //模式类型 
    public enum E_ModeType
    {
        AI,     //人机模式
        Player  //双人模式
    };

    //当前模式
    public static E_ModeType m_CurrentMode;

    //AI行为状态
    public enum E_AIstate
    {
        Throw,//投
        Place, //放
        NULL //空状态，起始默认状态
    };

    //主要算法函数AImothod
    public void AImethod(int value)
    {
        int j=0;
        float addai=0;
        float decpa=0;
        int maxnum=0;  //最大重复次数，默认初始值为0，即默认某行中不存在与value重复的值
        int num = 0;   //某行中value出现的次数，用于暂存
        int maxline=0; //最大重复次数对应的行
        int addline=0; //自己得分最大化策略对应的行数
        int decline=0; //对方得分最小化策略对应的行数
        int ischange = 0; //该变量用于避免一种情况，举例：当前棋盘布局为：6，2，2；0，0，0；0，0，0
                          //当前value值为6或者2，分析可知，无法找到符合自己分数最大化策略的位置，
                          //此时，默认的maxline为0，而0行全满，就无法对棋盘赋值，这样就出bug了。
        int flag1 = 0;
        int flag2 = 0;
        //寻找增加自己得分最佳位置（自己得分最大化策略）
        for (int i = 0; i < boardb.GetLength(0); i++)
        {
            if( boardb[i,0]!=0 && boardb[i, 1] != 0&& boardb[i, 2] != 0)//一行全满
            {
                continue;
            }
            for (j = 0; j < boardb.GetLength(1); j++)
            {
                if (boardb[i, j] == value)
                {
                    num++;
                }
            }
            if (num > maxnum)
            {
                maxnum = num;
                maxline = i;
                ischange = 1;//将“是否发生变化”变量ischange的值置为1，意为寻找过程中存在最佳点的可能出现点
            }
            num = 0;
        }
        //以下强转float操作主要是因为Pow函数的局限性，同时也是针对寻找过程中有找到可能为最佳点的点
        float num1=(float)maxnum;
        float num2=(float)(maxnum+1);
        float result1 = (float)(value) * Mathf.Pow(num2, 2.0f);
        float result2 = (float)(value) * Mathf.Pow(num1, 2.0f);
        addai = result1 - result2;
        addline = maxline;
        //针对没有找到最佳点的情况，则找一行，需要满足该行不满
        Debug.Log("add是否找到最佳点" + ischange);
        if (ischange == 0)
        {
            for (int i = 0; i < boardb.GetLength(0); i++)
            {
                if (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0)
                {
                    flag1 = 1;
                    addline = i;
                    //空3个的行优先级最高
                }
            }
            if (flag1 == 0)//3空的没找到，只能来找2空的
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0))
                    {
                        flag2 = 1;
                        addline = i;
                        //空2个的其次
                    }
                }
            }
            if (flag2 == 0 && flag1==0)//2空的没找到，只能来找1空的
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] != 0))
                    {
                        addline = i;
                        //空1个的最后
                    }
                }

            }
            flag1 = 0;
            flag2 = 0;
        }
        Debug.Log("addline值为" + addline);
        //每一轮结束后，无论ischange的值是多少，都应该将其赋值为原来的值，否则会影响后面游戏的正常进行
        ischange = 0;

        //寻找减少玩家A分数最佳位置（对方得分最小化策略）
        maxnum = 0;//重置
        //在这边无需对计数变量num再重置，因为在70行，可以看到，每次计数完都会重置num。同理于ischange。
        //同样，在此也存在着一种情况，就是A的棋盘是1，0，0；2，1，0；3，0，0；
                                    //电脑棋盘是4，3，3；5，6，0；1，0，0；
                                    //如果此时，电脑的骰子点数为4，分析可知，没有对方得分最小化的最佳位置
                                    //而maxline得默认值是0，但第0行又满了，就有会发生无法对棋盘赋值的问题，需要处理。
        for (int i = 0; i < boardb.GetLength(0); i++)
        {
            if (boardb[i, 0] != 0 && boardb[i, 1] != 0 && boardb[i, 2] != 0)//一行全满
            {
                continue;
            }
            for (j = 0; j < boarda.GetLength(1); j++)
            {
                if (boarda[i, j] == value)
                {
                    num++;
                }
            }
            if (num > maxnum)
            {
                maxnum = num;
                maxline = i;
                ischange = 1;
            }
            num = 0;
        }
        num1 = (float)maxnum;
        result2 = (float)(value) * Mathf.Pow(num1, 2.0f);
        decpa = result2;
        decline = maxline;
        Debug.Log("dec是否找到最佳点" + ischange);
        if (ischange == 0)
        {
            for (int i = 0; i < boardb.GetLength(0); i++)
            {
                if (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0)
                {
                    flag1 = 1;
                    decline = i;
                    //空3个的行优先级最高
                }
            }
            if (flag1 == 0)//3空的没找到，只能来找2空的
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0))
                    {
                        flag2 = 1;
                        decline = i;
                        //空2个的其次
                    }
                }
            }
            if (flag2 == 0&& flag1==0)//2空的没找到，只能来找1空的
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] != 0))
                    {
                        decline = i;
                        //空1个的最后
                    }
                }
                
            }
            flag1 = 0;
            flag2 = 0;
        }
        Debug.Log("decline值为" + decline);
        ischange = 0;

        if (addai > decpa)
        {
            //置自己得分最大化的位置为骰子点数
            for (int k = 0; k < boardb.GetLength(1); k++)
            {
                if (boardb[addline, k] == 0)
                {
                    boardb[addline, k] = value;
                    break;
                    //不马上退出的话就会出错
                }
            }
            //赋值过后，应该立刻执行消除操作
            Do(boarda, value, addline);
        }
        else
        {
            //置对方得分最小化的位置为骰子点数
            for (int k = 0; k < boardb.GetLength(1); k++)
            {
                Debug.Log("decline:" + decline);
                Debug.Log("k:" + k);

                if (boardb[decline, k] == 0)
                {
                    boardb[decline, k] = value;
                    break;
                }
            }
            Do(boarda, value, decline);
        }
    }

    //AI当前状态
    public E_AIstate m_CurrentAiState = E_AIstate.NULL;

    void Start()
    {
        count = 1;
        //不论是双人模式，还是AI模式，都是玩家A先出，所以count默认值为1。
    }
    
    //对AI的状态做出变化所对应的函数
    public void changeAistate(E_AIstate _state)
    { 
        m_CurrentAiState = _state;
        switch(m_CurrentAiState)
        {
            case E_AIstate.Throw:
                //投色子
                break;
        }
    }
    //消除函数
    public void Do(int[,] _board ,int _value,int hang)
    {
        for(int i=0;i<_board.GetLength(1);i++)
        {
            if (_board[hang, i] == _value)
            {
                _board[hang, i] = 0;
            }
        }
    }

    //获取分数
    public float GetScore(int[,] _board)
    {
        float sumnum = 0;//总分
        int[] arr = new int[7] { 0, 0, 0, 0, 0, 0, 0 };//用一个长度为7的数组，数组下标和骰子点数相对应
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                arr[_board[i, j]]++;
            }//内层循环做一次，相当于对一行进行统计
            for (int k = 1; k < arr.Length; k++)
            {
                if (arr[k] > 0)
                {
                    float di = (float)k;
                    float mi = (float)(arr[k]);
                    sumnum += di * Mathf.Pow(mi, 2);
                }
            }
            //对下一行统计数量之前，需要对数组清空，否则会混乱
            for (int k = 0; k < arr.Length; k++)
            {
                arr[k] = 0;
            }
        }
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = 0;
        }
        return sumnum;
    }
    
    //判断棋盘是否全满的函数
    public bool Full(int[,] _board)
    {
        int flag = 0;
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (_board[i, j] == 0)
                {
                    flag = 1;
                }
            }
        }
        if (flag == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //添加1.
    public void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }

    //由于每一帧的帧间隔时间是很短的，要让电脑在帧间隔时间内扔骰子，放骰子，在视觉上非常快，所以需要延时
    float m_time;
    // Update is called once per frame
    void Update()
    {
        if(m_CurrentMode == E_ModeType.AI)
        //进入的前提之一：AI模式
        {
            if(count == -1)
            //前提之二：电脑玩家轮次
            {
                switch(m_CurrentAiState)
                {
                    case E_AIstate.Throw:
                        m_time += Time.deltaTime;
                        if(m_time>4)
                        {
                            //直接操作B棋盘
                            AImethod(GameObject.Find("Dice").GetComponent<shaizi1>().value);
                            float resulta = GetScore(boarda);
                            float resultb = GetScore(boardb);
                            GameObject.Find("Ascore").GetComponent<Text>().text = "玩家A得分 :" + resulta;
                            GameObject.Find("Bscore").GetComponent<Text>().text = "电脑得分 :" + resultb;
                            if(Full(boardb) == true)
                            {
                                if (resulta > resultb)
                                {
                                    GameObject.Find("Round").GetComponent<Text>().text = "游戏结束，玩家A以" + resulta + "分获胜。再过5秒跳转主界面";
                                }
                                else if (resulta == resultb)
                                {
                                    GameObject.Find("Round").GetComponent<Text>().text = "游戏结束，以" + resulta + "分平局。 再过5秒跳转主界面";
                                }
                                else
                                {
                                    GameObject.Find("Round").GetComponent<Text>().text = "游戏结束，电脑玩家以" + resultb + "分获胜。再过5秒跳转主界面";
                                }
                                Invoke("LoadScene", 5);
                            }
                            else
                            {
                                //修改回合改成玩家回合
                                count = 1;
                                GameObject.Find("Round").GetComponent<Text>().text = "玩家A回合";
                                m_CurrentAiState = E_AIstate.NULL;
                            }
                        }
                       
                        break; 
                    case E_AIstate.NULL:
                        m_time += Time.deltaTime;
                        if (m_time > 3)
                        {
                            m_time = 0;
                            GameObject.Find("Dice").GetComponent<shaizi1>().ThrowDice();
                            GameObject.Find("Dice").GetComponent<shaizi1>().state = false;
                            changeAistate(E_AIstate.Throw);
                        }
                         break;
                }
            }
        }
    }
}
