using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getout : MonoBehaviour
{
    public void OnCklick()
    {
        Debug.Log("�˳���Ϸ");
        Application.Quit();
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
