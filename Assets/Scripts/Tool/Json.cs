using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Json : MonoBehaviour
{
    public static JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
    public void Awake()
    {
        jsonSettings.Converters.Add(new ScriptableObjectConverter<Product>());
        jsonSettings.TypeNameHandling = TypeNameHandling.All;
        AssetsLoader.jsonManager = this;
    }
   
}

public class ScriptableObjectConverter<T> : CustomCreationConverter<T> where T : ScriptableObject
{
    public override T Create(Type objectType)
    {
        return ScriptableObject.CreateInstance(objectType) as T;
    }
}

