using UnityEngine;

public class TP : MonoBehaviour
{
    public GameObject player;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            FadeInOut.FadeIn();
            //Invoke("ToScene2", 2f);
            TimeLineM.bBossGO = true;
            NPCtest.Instance().MissionP.SetActive(false);
        }
    }
    void ToScene2()
    {
        Debug.Log("fuck");
        MenuClick.MiniMap.SetActive(false);
        player.transform.position = new Vector3(120, -32f, 348);
        FadeInOut.FadeOut();   
    }
}
