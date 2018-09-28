using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messeger : MonoBehaviour {
    public InputField mesfield;
    public void SendButton(){
        if(transform.name!="Room"){
            FindObjectOfType<PriMesWindowCont>().UpdatePriMesChat("No." + ConnectController.accesscode + " to No." + transform.name + ":" + mesfield.text);
        }
        FindObjectOfType<ConnectController>().SendMes(transform.name + "," +ConnectController.accesscode+","+ mesfield.text+"|");
        mesfield.text = "";
    }
}
