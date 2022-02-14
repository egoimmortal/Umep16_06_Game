using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMusroom : MonoBehaviour
{
    public static GameObject Msr1, Msr2;
    public static GameObject rMsr1, rMsr2; //戰鬥後死亡香菇
    public static GameObject ExMark, ExMark2; //驚嘆號

    void Start()
    {
        ExMark = ResoucesManager.Instance().LoadGameObject("ExMark");//取得驚嘆號
        ExMark.SetActive(false);
        ExMark2 = ResoucesManager.Instance().LoadGameObject("ExMark");
        ExMark2.SetActive(false);
        
        Msr1 = GameObject.Find("mmm1");
        Msr2 = GameObject.Find("mmm2");
        rMsr1 = GameObject.Find("diemr1");
        rMsr2 = GameObject.Find("diemr2");
        rMsr1.SetActive(false);
        rMsr2.SetActive(false);
    }

    void Update()
    {
        Vector3 vec = this.transform.position - TotalStatic.AcqChan.transform.position;
        float dist = vec.magnitude;
        if (dist < 12)//距離內 進戰鬥
        {
            Msr1.transform.LookAt(TotalStatic.AcqChan.transform);
            Msr2.transform.LookAt(TotalStatic.AcqChan.transform);

            ExMark.SetActive(true); //驚嘆號
            Vector3 msr1 = Msr1.transform.position;
            ExMark.transform.position = new Vector3(msr1.x, msr1.y + 3, msr1.z);

            ExMark2.SetActive(true);//驚嘆號
            Vector3 msr2 = Msr2.transform.position;
            ExMark2.transform.position = new Vector3(msr2.x, msr2.y + 3, msr2.z);

            StartCoroutine(FirstFight());
            Destroy(ExMark, 3);//驚嘆號destroy
            Destroy(ExMark2, 3);

            Invoke("ShowDeadMsr", 1.5f);
        }
        else
        {
            ExMark.SetActive(false);
            ExMark2.SetActive(false);
        }
    }

    IEnumerator FirstFight()
    {
        yield return new WaitForSeconds(1f);
        NewBehaviour.attacking = true;
        NewBehaviour.monster_type = "FirstFight";
    }
    void ShowDeadMsr()
    {
        Msr1.SetActive(false);//原本香菇消失
        Msr2.SetActive(false);
        rMsr1.SetActive(true);//屍體香菇出現
        rMsr2.SetActive(true);
    }
}
