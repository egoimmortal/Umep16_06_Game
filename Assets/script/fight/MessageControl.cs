using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageControl : MonoBehaviour
{
    public static bool message_clear;
    public Text message;

    void Update()
    {
        if(EventControl.fight_message != null)
        {
            GetComponent<Text>().text = "" + EventControl.fight_message;
            EventControl.fight_message = null;
        }

        if (message_clear)
        {
            GetComponent<Text>().text = "";
            message_clear = false;
        }
    }
}
