using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject MainCharacter;//主要操控角色
    public GameObject MiniMap;//小地圖
    public bool fighting;

    void Start()
    {
        fighting = false;
        ResoucesManager rm = new ResoucesManager();
    }

    void Update()
    {
        //Cursor.visible = true;//滑鼠游標顯示
        //Cursor.visible = false;//滑鼠游標隱藏
        //Cursor.lockState = CursorLockMode.Locked;//滑鼠鎖定中間後隱藏
        //Cursor.lockState = CursorLockMode.None;//滑鼠顯示

        //if (Input.GetKeyDown(KeyCode.G))//攝影機鎖定跟著主角動
        //    TotalStatic.bCameraMove = false;
        //if (Input.GetKeyDown(KeyCode.M))//攝影機解除鎖定，可以用滑鼠操控攝影機左右
        //    TotalStatic.bCameraMove = true;

        MiniMap.transform.position = new Vector3(
            MainCharacter.transform.position.x,
            40,
            MainCharacter.transform.position.z);
    }
}