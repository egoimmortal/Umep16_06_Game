using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    /// <summary>
    /// 該script附加在Assets/Prefabs/的所有角色身上
    /// script說明 : 
    ///     一進入遊戲就先取得各自的血條
    ///     每到回合的時候去計算血條的增減
    /// </summary>

    /// <summary>
    /// 私有靜態變數
    /// </summary>
    private static Image hp_bar, mp_bar;
    private static float max_hp, now_hp, max_mp, now_mp;
    private static Text t_now_hp, t_now_mp, LV1, LV2;

    #region 在MoveToTarget中的傷害判斷裡使用(血條增減判斷)
    public static void BloodVariety(GameObject Player)
    {
        if (Player.name == "Character1")
        {
            hp_bar = GameObject.Find("P1HP").GetComponent<Image>();
            mp_bar = GameObject.Find("P1MP").GetComponent<Image>();
            t_now_hp = GameObject.Find("P1HPText").GetComponent<Text>();
            t_now_mp = GameObject.Find("P1MPText").GetComponent<Text>();
        }
        else
        {
            hp_bar = GameObject.Find("P2HP").GetComponent<Image>();
            mp_bar = GameObject.Find("P2MP").GetComponent<Image>();
            t_now_hp = GameObject.Find("P2HPText").GetComponent<Text>();
            t_now_mp = GameObject.Find("P2MPText").GetComponent<Text>();
        }

        max_hp = Player.GetComponent<CharacterState>().MaxHp;
        max_mp = Player.GetComponent<CharacterState>().MaxMp;

        now_hp = Player.GetComponent<CharacterState>().Hp;
        now_mp = Player.GetComponent<CharacterState>().Mp;
        hp_bar.fillAmount = now_hp / max_hp;
        mp_bar.fillAmount = now_mp / max_mp;

        t_now_hp.text = now_hp.ToString();
        t_now_mp.text = now_mp.ToString();
    }
    #endregion
}
