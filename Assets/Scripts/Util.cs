using System;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static List<T> ImportJson<T>(string path)
    { 
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Wrapper<T>>(textAsset.text).Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}