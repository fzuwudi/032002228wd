using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class twojmp : MonoBehaviour
{

    public void OnCklick()
    {
        Debug.Log("跳转双人对战场景");
        SceneManager.LoadScene("GameIn");
        gamemain.m_CurrentMode = gamemain.E_ModeType.Player;
    }
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnCklick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
