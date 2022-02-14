using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partical : MonoBehaviour
{
    private GameObject partical;
    void FireTest()
    {
        partical = ResoucesManager.Instance().LoadGameObject("Fire");
        //fire.transform.position = GameObject.Find("skeleton_king").transform.position;
        partical.transform.position = this.transform.position + this.transform.forward * 5.0f;
    }
    
    void Water()
    {
        partical = ResoucesManager.Instance().LoadGameObject("Water");
        partical.transform.position = this.transform.position + this.transform.forward * 5.0f;
    }

    void Power()
    {
        partical = ResoucesManager.Instance().LoadGameObject("Power");
        partical.transform.position = this.transform.position + this.transform.forward * 5.0f;
    }

    void LineUpOnce()
    {
        partical = ResoucesManager.Instance().LoadGameObject("LineUpOnce");
        partical.transform.position = this.transform.position;
    }

    void MagicRune1()
    {
        partical = ResoucesManager.Instance().LoadGameObject("MagicRune1");
        partical.transform.position = this.transform.position;
        
    }
}
