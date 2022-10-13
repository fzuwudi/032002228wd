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
        Debug.Log("棋盘格子赋值");
        if (numself == 0)
        { 
            if(gamemain.m_CurrentMode == gamemain.E_ModeType.AI)
            {
                //这个模式下面可以取完成 玩家的操作 和 电脑的 
                //定义玩家 定义 0 定义AI
                if (transform.parent.name == "BoardA" && main.count == 1 && shaizi.state == true)
                {
                    GameObject.Find("Round").GetComponent<Text>().text ="电脑回合";
                    main.boarda[row, line] = shaizi.value;
                    main.count =-1;
                    main.Do(main.boardb, shaizi.value, row);
                    shaizi.state = false;
                    float resulta = main.GetScore(main.boarda);
                    float resultb = main.GetScore(main.boardb);
                    GameObject.Find("Ascore").GetComponent<Text>().text = "玩家A得分 :" + resulta;
                    GameObject.Find("Bscore").GetComponent<Text>().text = "玩家B得分 :" + resultb;
                    if (main.Full(main.boarda))
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
                //赋值
                if (transform.parent.name == "BoardA" && main.count == 1 && shaizi.state == true)
                {
                    GameObject.Find("Round").GetComponent<Text>().text = "玩家B回合";
                    main.boarda[row, line] = shaizi.value;
                    main.count = 0;
                    main.Do(main.boardb, shaizi.value, row);
                    shaizi.state = false;

                    float resulta = main.GetScore(main.boarda);
                    float resultb = main.GetScore(main.boardb);
                    GameObject.Find("Ascore").GetComponent<Text>().text = "玩家A得分 :" + resulta;
                    GameObject.Find("Bscore").GetComponent<Text>().text = "玩家B得分 :" + resultb;
                    if (main.Full(main.boarda))
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
                            GameObject.Find("Round").GetComponent<Text>().text = "游戏结束，玩家B以" + resultb + "分获胜。再过5秒跳转主界面";
                        }

                        Invoke("LoadScene", 3);
                    }

                }
                else if (transform.parent.name == "BoardB" && main.count == 0 && shaizi.state == true)
                {
                    GameObject.Find("Round").GetComponent<Text>().text = "玩家A回合";
                    main.boardb[row, line] = shaizi.value;
                    main.count = 1;
                    main.Do(main.boarda, shaizi.value, row);
                    shaizi.state = false;
                    float resulta = main.GetScore(main.boarda);
                    float resultb = main.GetScore(main.boardb);
                    GameObject.Find("Bscore").GetComponent<Text>().text = "玩家B得分 :" + resultb;
                    GameObject.Find("Ascore").GetComponent<Text>().text = "玩家A得分 :" + resulta;
                    if (main.Full(main.boardb))
                    {
                        if (resulta > resultb)
                        {
                            GameObject.Find("Round").GetComponent<Text>().text = "游戏结束，玩家A以" + resulta + "分获胜。再过5秒跳转主界面";
                        }
                        else if (resulta == resultb)
                        {
                            GameObject.Find("Round").GetComponent<Text>().text = "游戏结束，以"+ resulta + "分平局。 再过5秒跳转主界面";
                        }
                        else
                        {
                            GameObject.Find("Round").GetComponent<Text>().text = "游戏结束，玩家B以" + resultb + "分获胜。再过5秒跳转主界面";
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
