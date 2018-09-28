using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriMesWindowCont : MonoBehaviour {
    public Text[] texts = new Text[11];
    public void Start()
    {
        foreach (Text chattext in texts)
        {
            chattext.text = "";
        }
    }
    public void UpdatePriMesChat(string mes)
    {
        int i = 0;
        while (texts[i].text != "")//空きを確認
        {
            i++;
            if (i == 11)
            {
                break;
            }
        }
        if (i < 11)
        {
            texts[i].text = mes;
        }
        else//埋まっていたら詰める
        {
            for (int j = 0; j < 10; j++)
            {
                texts[j].text = texts[j + 1].text;
            }
            texts[10].text = mes;
        }
    }
}
