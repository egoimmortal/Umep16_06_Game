using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    /// <summary>
    /// 畫面變黑
    /// </summary>
    public static void FadeIn()
    {
        TotalStatic.BlackFade_image.CrossFadeAlpha(1, 0.5f, false);
    }
    /// <summary>
    /// 畫面變透明
    /// </summary>
    public static void FadeOut()
    {
        TotalStatic.BlackFade_image.CrossFadeAlpha(0, 2f, false);
    }

    /// <summary>
    /// 戰鬥失敗顯示出來
    /// </summary>
    public static void GameOverFadeIn()
    {
        TotalStatic.GameOver_image.CrossFadeAlpha(1, 0.5f, false);
    }
    public static void GameOverFadeOut()
    {
        TotalStatic.GameOver_image.CrossFadeAlpha(0, 2f, false);
    }
}