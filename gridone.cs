using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gridone : MonoBehaviour
{
    public int row;
    public int line;
    public int numself;

    public gamemain main;
    public shaizi1 shaizi;
    public void OnCklick()
    {
        Debug.Log("���̸��Ӹ�ֵ");
        if (numself == 0)
        { 
            if(gamemain.m_CurrentMode == gamemain.E_ModeType.AI)
            {
                //���ģʽ�������ȡ��� ��ҵĲ��� �� ���Ե� 
                //������� ���� 0 ����AI
                if (transform.parent.name == "BoardA" && main.count == 1 && shaizi.state == true)
                {
                    GameObject.Find("Round").GetComponent<Text>().text ="���Իغ�";
                    main.boarda[row, line] = shaizi.value;
                    main.count =-1;
                    main.Do(main.boardb, shaizi.value, row);
                    shaizi.state = false;
                    float resulta = main.GetScore(main.boarda);
                    float resultb = main.GetScore(main.boardb);
                    GameObject.Find("Ascore").GetComponent<Text>().text = "���A�÷� :" + resulta;
                    GameObject.Find("Bscore").GetComponent<Text>().text = "���B�÷� :" + resultb;
                    if (main.Full(main.boarda))
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
                        Invoke("LoadScene", 3);
                    }
                    main.changeAistate(gamemain.E_AIstate.NULL);
                }
                
            }
            else if(gamemain.m_CurrentMode == gamemain.E_ModeType.Player)
            {
                Debug.Log("name:" + transform.parent.name);
                Debug.Log("countL" + main.count);
                Debug.Log("shaizi.state" + shaizi.state);
                //��ֵ
                if (transform.parent.name == "BoardA" && main.count == 1 && shaizi.state == true)
                {
                    GameObject.Find("Round").GetComponent<Text>().text = "���B�غ�";
                    main.boarda[row, line] = shaizi.value;
                    main.count = 0;
                    main.Do(main.boardb, shaizi.value, row);
                    shaizi.state = false;

                    float resulta = main.GetScore(main.boarda);
                    float resultb = main.GetScore(main.boardb);
                    GameObject.Find("Ascore").GetComponent<Text>().text = "���A�÷� :" + resulta;
                    GameObject.Find("Bscore").GetComponent<Text>().text = "���B�÷� :" + resultb;
                    if (main.Full(main.boarda))
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
                            GameObject.Find("Round").GetComponent<Text>().text = "��Ϸ���������B��" + resultb + "�ֻ�ʤ���ٹ�5����ת������";
                        }

                        Invoke("LoadScene", 3);
                    }

                }
                else if (transform.parent.name == "BoardB" && main.count == 0 && shaizi.state == true)
                {
                    GameObject.Find("Round").GetComponent<Text>().text = "���A�غ�";
                    main.boardb[row, line] = shaizi.value;
                    main.count = 1;
                    main.Do(main.boarda, shaizi.value, row);
                    shaizi.state = false;
                    float resulta = main.GetScore(main.boarda);
                    float resultb = main.GetScore(main.boardb);
                    GameObject.Find("Bscore").GetComponent<Text>().text = "���B�÷� :" + resultb;
                    GameObject.Find("Ascore").GetComponent<Text>().text = "���A�÷� :" + resulta;
                    if (main.Full(main.boardb))
                    {
                        if (resulta > resultb)
                        {
                            GameObject.Find("Round").GetComponent<Text>().text = "��Ϸ���������A��" + resulta + "�ֻ�ʤ���ٹ�5����ת������";
                        }
                        else if (resulta == resultb)
                        {
                            GameObject.Find("Round").GetComponent<Text>().text = "��Ϸ��������"+ resulta + "��ƽ�֡� �ٹ�5����ת������";
                        }
                        else
                        {
                            GameObject.Find("Round").GetComponent<Text>().text = "��Ϸ���������B��" + resultb + "�ֻ�ʤ���ٹ�5����ת������";
                        }

                        Invoke("LoadScene", 3);
                    }
                }
            }
            
        }
        else
        {

        }

    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnCklick);

        main = GameObject.Find("Main").GetComponent<gamemain>();  
        shaizi = GameObject.Find("Dice").GetComponent<shaizi1>();
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name == "BoardA")
        {
            numself =  GameObject.Find("Main").GetComponent<gamemain>().boarda[row, line];
        }
        else if (transform.parent.name == "BoardB")
        {
            numself = GameObject.Find("Main").GetComponent<gamemain>().boardb[row, line];
        }
        transform.GetChild(0).GetComponent<Text>().text = numself.ToString();  
    }
}
