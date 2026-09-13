using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class PlayerInfoSerialization : MonoBehaviour
{
    private PlayerAgent agent;
    private string savePath =
#if UNITY_EDITOR
        "D:/info/playerInfo.txt";
#else
        System.IO.Path.Combine(Application.streamingAssetsPath, "playerInfo.txt");
#endif
    private void Awake()
    {
        Quit.SerializationAction += Serialize;
        agent = GetComponent<PlayerAgent>();
        DeSerialize();
    }
    private void Serialize()
    {
        string playerJson = JsonConvert.SerializeObject(agent.Property, agent.Property.GetType(),Json.jsonSettings);
        File.WriteAllText(savePath, playerJson);
    }

    private void DeSerialize()
    {
        string playerJson = File.ReadAllText(savePath);
        var obj = JsonConvert.DeserializeObject(playerJson,agent.Property.GetType(),Json.jsonSettings);
        agent.InitProperty(obj);
    }
}
