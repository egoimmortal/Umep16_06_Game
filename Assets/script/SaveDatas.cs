using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level;
    public float hp;
    public float[] position;
    private CharacterState player;

    public SaveData(CharacterState player)
    {
        level = player.Level;
        hp = player.Hp;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
