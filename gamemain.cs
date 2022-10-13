using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemain : MonoBehaviour
{
    //���̸��ӳ���Ϊ����3��3����
    public int[,] boarda = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    public int[,] boardb = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    // Start is called before the first frame update
    //count���ڱ�ʾ����Ӧ������һ�����壬
    //��˫��ģʽ�У����A��countΪ1�����B��countΪ0
    //��AIģʽ�У����A��countΪ1��������ҵ�countΪ-1.
    public int count;

    //ģʽ���� 
    public enum E_ModeType
    {
        AI,     //�˻�ģʽ
        Player  //˫��ģʽ
    };

    //��ǰģʽ
    public static E_ModeType m_CurrentMode;

    //AI��Ϊ״̬
    public enum E_AIstate
    {
        Throw,//Ͷ
        Place, //��
        NULL //��״̬����ʼĬ��״̬
    };

    //��Ҫ�㷨����AImothod
    public void AImethod(int value)
    {
        int j=0;
        float addai=0;
        float decpa=0;
        int maxnum=0;  //����ظ�������Ĭ�ϳ�ʼֵΪ0����Ĭ��ĳ���в�������value�ظ���ֵ
        int num = 0;   //ĳ����value���ֵĴ����������ݴ�
        int maxline=0; //����ظ�������Ӧ����
        int addline=0; //�Լ��÷���󻯲��Զ�Ӧ������
        int decline=0; //�Է��÷���С�����Զ�Ӧ������
        int ischange = 0; //�ñ������ڱ���һ���������������ǰ���̲���Ϊ��6��2��2��0��0��0��0��0��0
                          //��ǰvalueֵΪ6����2��������֪���޷��ҵ������Լ�������󻯲��Ե�λ�ã�
                          //��ʱ��Ĭ�ϵ�maxlineΪ0����0��ȫ�������޷������̸�ֵ�������ͳ�bug�ˡ�
        int flag1 = 0;
        int flag2 = 0;
        //Ѱ�������Լ��÷����λ�ã��Լ��÷���󻯲��ԣ�
        for (int i = 0; i < boardb.GetLength(0); i++)
        {
            if( boardb[i,0]!=0 && boardb[i, 1] != 0&& boardb[i, 2] != 0)//һ��ȫ��
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
                ischange = 1;//�����Ƿ����仯������ischange��ֵ��Ϊ1����ΪѰ�ҹ����д�����ѵ�Ŀ��ܳ��ֵ�
            }
            num = 0;
        }
        //����ǿתfloat������Ҫ����ΪPow�����ľ����ԣ�ͬʱҲ�����Ѱ�ҹ��������ҵ�����Ϊ��ѵ�ĵ�
        float num1=(float)maxnum;
        float num2=(float)(maxnum+1);
        float result1 = (float)(value) * Mathf.Pow(num2, 2.0f);
        float result2 = (float)(value) * Mathf.Pow(num1, 2.0f);
        addai = result1 - result2;
        addline = maxline;
        //���û���ҵ���ѵ�����������һ�У���Ҫ������в���
        Debug.Log("add�Ƿ��ҵ���ѵ�" + ischange);
        if (ischange == 0)
        {
            for (int i = 0; i < boardb.GetLength(0); i++)
            {
                if (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0)
                {
                    flag1 = 1;
                    addline = i;
                    //��3���������ȼ����
                }
            }
            if (flag1 == 0)//3�յ�û�ҵ���ֻ������2�յ�
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0))
                    {
                        flag2 = 1;
                        addline = i;
                        //��2�������
                    }
                }
            }
            if (flag2 == 0 && flag1==0)//2�յ�û�ҵ���ֻ������1�յ�
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] != 0))
                    {
                        addline = i;
                        //��1�������
                    }
                }

            }
            flag1 = 0;
            flag2 = 0;
        }
        Debug.Log("addlineֵΪ" + addline);
        //ÿһ�ֽ���������ischange��ֵ�Ƕ��٣���Ӧ�ý��丳ֵΪԭ����ֵ�������Ӱ�������Ϸ����������
        ischange = 0;

        //Ѱ�Ҽ������A�������λ�ã��Է��÷���С�����ԣ�
        maxnum = 0;//����
        //���������Լ�������num�����ã���Ϊ��70�У����Կ�����ÿ�μ����궼������num��ͬ����ischange��
        //ͬ�����ڴ�Ҳ������һ�����������A��������1��0��0��2��1��0��3��0��0��
                                    //����������4��3��3��5��6��0��1��0��0��
                                    //�����ʱ�����Ե����ӵ���Ϊ4��������֪��û�жԷ��÷���С�������λ��
                                    //��maxline��Ĭ��ֵ��0������0�������ˣ����лᷢ���޷������̸�ֵ�����⣬��Ҫ����
        for (int i = 0; i < boardb.GetLength(0); i++)
        {
            if (boardb[i, 0] != 0 && boardb[i, 1] != 0 && boardb[i, 2] != 0)//һ��ȫ��
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
        Debug.Log("dec�Ƿ��ҵ���ѵ�" + ischange);
        if (ischange == 0)
        {
            for (int i = 0; i < boardb.GetLength(0); i++)
            {
                if (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0)
                {
                    flag1 = 1;
                    decline = i;
                    //��3���������ȼ����
                }
            }
            if (flag1 == 0)//3�յ�û�ҵ���ֻ������2�յ�
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] == 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0))
                    {
                        flag2 = 1;
                        decline = i;
                        //��2�������
                    }
                }
            }
            if (flag2 == 0&& flag1==0)//2�յ�û�ҵ���ֻ������1�յ�
            {
                for (int i = 0; i < boardb.GetLength(0); i++)
                {
                    if ((boardb[i, 0] != 0 && boardb[i, 1] != 0 && boardb[i, 2] == 0) || (boardb[i, 0] != 0 && boardb[i, 1] == 0 && boardb[i, 2] != 0) || (boardb[i, 0] == 0 && boardb[i, 1] != 0 && boardb[i, 2] != 0))
                    {
                        decline = i;
                        //��1�������
                    }
                }
                
            }
            flag1 = 0;
            flag2 = 0;
        }
        Debug.Log("declineֵΪ" + decline);
        ischange = 0;

        if (addai > decpa)
        {
            //���Լ��÷���󻯵�λ��Ϊ���ӵ���
            for (int k = 0; k < boardb.GetLength(1); k++)
            {
                if (boardb[addline, k] == 0)
                {
                    boardb[addline, k] = value;
                    break;
                    //�������˳��Ļ��ͻ����
                }
            }
            //��ֵ����Ӧ������ִ����������
            Do(boarda, value, addline);
        }
        else
        {
            //�öԷ��÷���С����λ��Ϊ���ӵ���
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

    //AI��ǰ״̬
    public E_AIstate m_CurrentAiState = E_AIstate.NULL;

    void Start()
    {
        count = 1;
        //������˫��ģʽ������AIģʽ���������A�ȳ�������countĬ��ֵΪ1��
    }
    
    //��AI��״̬�����仯����Ӧ�ĺ���
    public void changeAistate(E_AIstate _state)
    { 
        m_CurrentAiState = _state;
        switch(m_CurrentAiState)
        {
            case E_AIstate.Throw:
                //Ͷɫ��
                break;
        }
    }
    //��������
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

    //��ȡ����
    public float GetScore(int[,] _board)
    {
        float sumnum = 0;//�ܷ�
        int[] arr = new int[7] { 0, 0, 0, 0, 0, 0, 0 };//��һ������Ϊ7�����飬�����±�����ӵ������Ӧ
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                arr[_board[i, j]]++;
            }//�ڲ�ѭ����һ�Σ��൱�ڶ�һ�н���ͳ��
            for (int k = 1; k < arr.Length; k++)
            {
                if (arr[k] > 0)
                {
                    float di = (float)k;
                    float mi = (float)(arr[k]);
                    sumnum += di * Mathf.Pow(mi, 2);
                }
            }
            //����һ��ͳ������֮ǰ����Ҫ��������գ���������
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
    
    //�ж������Ƿ�ȫ���ĺ���
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

    //���1.
    public void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }

    //����ÿһ֡��֡���ʱ���Ǻ̵ܶģ�Ҫ�õ�����֡���ʱ���������ӣ������ӣ����Ӿ��Ϸǳ��죬������Ҫ��ʱ
    float m_time;
    // Update is called once per frame
    void Update()
    {
        if(m_CurrentMode == E_ModeType.AI)
        //�����ǰ��֮һ��AIģʽ
        {
            if(count == -1)
            //ǰ��֮������������ִ�
            {
                switch(m_CurrentAiState)
                {
                    case E_AIstate.Throw:
                        m_time += Time.deltaTime;
                        if(m_time>4)
                        {
                            //ֱ�Ӳ���B����
                            AImethod(GameObject.Find("Dice").GetComponent<shaizi1>().value);
                            float resulta = GetScore(boarda);
                            float resultb = GetScore(boardb);
                            GameObject.Find("Ascore").GetComponent<Text>().text = "���A�÷� :" + resulta;
                            GameObject.Find("Bscore").GetComponent<Text>().text = "���Ե÷� :" + resultb;
                            if(Full(boardb) == true)
                            {
                                if (resulta > resultb)
                                {
                                    GameObject.Find("Round").GetComponent<Text>().text = "��Ϸ���������A��" + resulta + "�ֻ�ʤ���ٹ�5����ת������";
                                }
                                else if (resulta == resultb)
                                {
                                    GameObject.Find("Round").GetComponent<Text>().text = "��Ϸ��������" + resulta + "��ƽ�֡� �ٹ�5����ת������";
                                }
                                else
                                {
                                    GameObject.Find("Round").GetComponent<Text>().text = "��Ϸ���������������" + resultb + "�ֻ�ʤ���ٹ�5����ת������";
                                }
                                Invoke("LoadScene", 5);
                            }
                            else
                            {
                                //�޸Ļغϸĳ���һغ�
                                count = 1;
                                GameObject.Find("Round").GetComponent<Text>().text = "���A�غ�";
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
