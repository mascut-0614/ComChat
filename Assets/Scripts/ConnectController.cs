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

	public void ConnectStart(string iptext){
		try{
			tcpClient.Connect(iptext, 8080);
			ns = tcpClient.GetStream();
            inputField.text = "Successed";
            ConnectArea.gameObject.SetActive(false);
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
                string[] lineArrays = resMsg.Split('|');
                foreach (string lineArray in lineArrays)
                {
                    if (lineArray != "")
                    {
                        //FindObjectOfType<ChatScreen>().UpdateChat(lineArray);
                    }
                }
            }
        }
    }
}
