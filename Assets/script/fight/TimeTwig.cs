using UnityEngine;

public class TimeTwig
{
    /// <summy>
    /// 該script附加在DemoScene中的FightScript上
    /// script說明 : 
    ///     戰鬥開始時取得角色1角色2怪物1怪物2的資料
    ///     判斷有存活的角色或怪物持續增加時間
    ///     任何一個角色或怪物的時間到達或超過時間表上限的時候判斷行動回合
    ///     判斷角色或怪物的時間到達或超過時間表上限的話就將該角色或怪物加入行動回合，以角色1角色2怪物1怪物2為順序，只進行一次判斷
    /// </summy>

    /// <summary>
    /// 公有變數
    /// </summary>
    //現在的行動回合
    public static string now_turn;
    //時間表開關與行動表開關
    public static bool turn_switch;
    /// <summary>
    /// 私有變數
    /// </summary>
    //行動條時間上限
    private float time_over = 100;

    public void TimeJudge()
    {
        #region 有存活的角色或怪物持續增加時間，任何一個到達或超過100的時間條會使所有時間暫停並開啟判斷時間條
        if (TotalStatic.Time_switch)
        {
            foreach (var value in TotalStatic.StateList)
            {
                if (value.fTime >= time_over)
                {
                    turn_switch = true;
                    TotalStatic.Time_switch = false;
                }
                else
                {
                    AddTime(value);
                }
            }
        }
        #endregion

        #region 有人的行動條到達上限和現在行動者為空時，執行一次判斷行動者是誰
        if (turn_switch)
        {
            foreach (var value in TotalStatic.StateList)
            {
                if (value.fTime >= time_over)
                {
                    MoveTurn(value.name);
                }
            }
        }
        #endregion
    }
    /// <summary>
    /// 增加時間的判斷
    /// </summary>
    /// <param name="now_name"></param>
    /// <param name="time"></param>
    private void AddTime(CharacterState objState)
    {
        if (objState.Hp > 0 && objState.fTime < time_over)
            objState.fTime += objState.iSpeed * Time.deltaTime * 2;
    }
    /// <summary>
    /// 到達上限的角色進入行動回合
    /// </summary>
    private void MoveTurn(string now_name)
    {
        now_turn = now_name;
        turn_switch = false;
    }
}