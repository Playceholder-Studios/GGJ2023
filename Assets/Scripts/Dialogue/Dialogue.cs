using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{   
    public bool isPlayer;
    public string content;
    public List<Keyword> keywords;
}

[Serializable]
public class Keyword
{
    public string word;
    public string context;
}