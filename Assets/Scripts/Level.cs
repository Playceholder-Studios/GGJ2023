using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{   
    public int id;
    public int characterID;
    public int correctDrinkId;
    public List<Dialogue> ordering;
    public List<Dialogue> success;
    public List<Dialogue> failure;
}