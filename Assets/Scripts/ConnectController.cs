using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;


public class ConnectController : MonoBehaviour {
	TcpClient tcpClient = new TcpClient();
	NetworkStream ns;
	System.IO.MemoryStream ms;
    public InputField inputField;
    public GameObject ConnectArea;
    public GameObject Image;
    public GameObject mesField;
    public static string accesscode;

	public void ConnectStart(){
		try{
            tcpClient.Connect(inputField.text, 8080);
			ns = tcpClient.GetStream();
            inputField.text = "Successed";
            ConnectArea.SetActive(false);
            Image.SetActive(true);
            mesField.SetActive(true);
		}catch{
            inputField.text = "Failed";
		}
	}

    public void SendMes(string mes){
        if(tcpClient.Connected){
            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            byte[] sendBytes = enc.GetBytes(mes + '\n');
            ns.Write(sendBytes, 0, sendBytes.Length);
        }
    }

    void Update()
    {
        if (tcpClient.Connected)
        {
            if (tcpClient.Available > 0)
            {
                ms = new System.IO.MemoryStream();
                byte[] resBytes = new byte[256];
                int resSize = 0;
                resSize = ns.Read(resBytes, 0, resBytes.Length);
                if (resSize == 0)
                {
                    Debug.Log("サーバーが切断されました。");
                    ms.Close();
                    ns.Close();
                    tcpClient.Close();
                }
                ms.Write(resBytes, 0, resSize);
                System.Text.Encoding enc = System.Text.Encoding.UTF8;
                string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
                ms.Close();
                //メッセージごとに分割
                string[] lineArrays = resMsg.Split('|');
                foreach (string lineArray in lineArrays)
                {
                    if (lineArray != "")
                    {
                        //メッセージの要素を、「送信先、送信元、内容」もしくは「特殊コード、対象」に分割
                        string[] words = lineArray.Split(',');
                        switch(words[0]){
                            case "Start"://新規プレイヤーのログイン、アクセスコードの割り振り
                                accesscode = words[1];
                                SendMes("Register," + words[1] + "|");
                                break;
                            case "Register"://プレイヤーに新規プレイヤーのログインを伝達
                                FindObjectOfType<AllMesWindowCont>().UpdateAllMesChat("No." + words[1] + "さんが参加しました");
                                FindObjectOfType<ButtonSet>().AddButton("Room");
                                //新規プレイヤーに自分の存在を伝達
                                if(accesscode!=words[1]){
                                    FindObjectOfType<ButtonSet>().AddButton(words[1]);
                                    SendMes("Accept," + words[1] + "," + accesscode + "|");
                                }
                                break;
                            case "Accept":
                                if(words[1]==accesscode){
                                    FindObjectOfType<PriMesWindowCont>().UpdatePriMesChat("No."+words[2]+"さんが参加しているようです");
                                    FindObjectOfType<ButtonSet>().AddButton(words[2]);
                                }
                                break;
                            case "Room":
                                FindObjectOfType<AllMesWindowCont>().UpdateAllMesChat("No." + words[1] + ":" + words[2]);
                                break;
                            default:
                                if(words[0]==accesscode){
                                    FindObjectOfType<PriMesWindowCont>().UpdatePriMesChat("No." + words[1] + " to No." + accesscode + ":" + words[2]);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
