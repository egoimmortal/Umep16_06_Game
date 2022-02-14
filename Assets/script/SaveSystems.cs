using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystems : MonoBehaviour
{
    private CharacterState character_state;
    private HappenedEvent happenedEvent = new HappenedEvent();
    private string SaveStr;
    private string LoadJson;

    private static SaveSystems m_instance;
    public static SaveSystems Instance()
    {
        return m_instance;
    }

    private void Awake()
    {
        m_instance = this;
    }

    public void SaveData(GameObject character)
    {
        character_state = character.GetComponent<CharacterState>();
        character_state.vPos = character.transform.position;
        character_state.ItemList = new List<int>
        {
            TotalStatic.iRedWater_item,
            TotalStatic.iBlueWater_item,
            TotalStatic.iRevive_item
        };
        SaveStr = JsonUtility.ToJson(character_state);
        StreamWriter file = new StreamWriter(Path.Combine(Application.streamingAssetsPath, character.name + ".json"));
        file.Write(SaveStr);
        file.Close();
    }

    public void SaveHappenedEvent()
    {
        happenedEvent.MainMenuButton = true;
        happenedEvent.MiniMap = true;
        happenedEvent.bStartTL = TimeLineM.bStartTL;
        happenedEvent.bUnityChanTL = TimeLineM.bUnityChanTL;
        happenedEvent.bGateTL = TimeLineM.bGateTL;
        happenedEvent.bBossTL = TimeLineM.bBossTL;
        happenedEvent.UChan = TimeLineM.UChan.activeSelf;
        happenedEvent.Msr1 = FirstMusroom.Msr1.activeSelf;
        happenedEvent.Msr2 = FirstMusroom.Msr2.activeSelf;
        happenedEvent.rMsr1 = FirstMusroom.rMsr1.activeSelf;
        happenedEvent.rMsr2 = FirstMusroom.rMsr2.activeSelf;

        SaveStr = JsonUtility.ToJson(happenedEvent);
        StreamWriter file = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "Happened.json"));
        file.Write(SaveStr);
        file.Close();
    }

    public void LoadData(GameObject character)
    {
        LoadFile(character.name);
        TemState loaddata;
        loaddata = JsonUtility.FromJson<TemState>(LoadJson);

        character_state = character.GetComponent<CharacterState>();
        character_state.Level = loaddata.Level;
        character_state.Hp = loaddata.Hp;
        character_state.MaxHp = loaddata.MaxHp;
        character_state.Mp = loaddata.Mp;
        character_state.MaxMp = loaddata.MaxMp;
        character_state.Exp = loaddata.Exp;
        character_state.MaxExp = loaddata.MaxExp;
        character_state.Atk = loaddata.Atk;
        character_state.Matk = loaddata.Matk;
        character_state.Def = loaddata.Def;
        character_state.Mdef = loaddata.Mdef;
        character_state.iSpeed = loaddata.iSpeed;
        character_state.Dodge = loaddata.Dodge;
        character_state.Hit = loaddata.Hit;
        character_state.Critical = loaddata.Critical;
        character.transform.position = new Vector3(loaddata.vPos.x, loaddata.vPos.y, loaddata.vPos.z);
        character_state.ItemList = loaddata.ItemList;
    }

    private void LoadFile(string name)
    {
        name += ".json";
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, name));
        LoadJson = file.ReadToEnd();
        file.Close();
    }

    public void LoadHappenedEvent()
    {
        ///讀取動畫事件的檔案
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Happened.json"));
        LoadJson = file.ReadToEnd();
        file.Close();
        HappenedEvent bHappened;
        bHappened = JsonUtility.FromJson<HappenedEvent>(LoadJson);
        ///設定動畫事件是否播放過
        MenuClick.MainMenuButton.SetActive(bHappened.MainMenuButton);
        MenuClick.MiniMap.SetActive(bHappened.MiniMap);
        TimeLineM.bStartTL = bHappened.bStartTL;
        TimeLineM.bUnityChanTL = bHappened.bUnityChanTL;
        TimeLineM.bGateTL = bHappened.bGateTL;
        TimeLineM.bBossTL = bHappened.bBossTL;
        TimeLineM.UChan.SetActive(bHappened.UChan);
        FirstMusroom.Msr1.SetActive(bHappened.Msr1);
        FirstMusroom.Msr2.SetActive(bHappened.Msr2);
        FirstMusroom.rMsr1.SetActive(bHappened.rMsr1);
        FirstMusroom.rMsr2.SetActive(bHappened.rMsr2);
    }
}
