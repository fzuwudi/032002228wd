using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shaizi1 : MonoBehaviour
{
    public int value;
    public bool state;
    void Start()
    {
        Debug.Log("zzzzz");
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ThrowDice);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowDice()
    {
        if(state == false)
        {
            Debug.Log(11111);
            value = UnityEngine.Random.Range(1, 7);
            transform.GetChild(0).GetComponent<Text>().text = value.ToString();
            state = true;
        }
        
    }
}
