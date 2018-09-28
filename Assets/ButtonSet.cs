using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSet : MonoBehaviour {
    public void AddButton(string Username){
        transform.Find(Username).gameObject.SetActive(true);
    }
}
