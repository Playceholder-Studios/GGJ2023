using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ingredient
{   
    public int id;
    public string ingredientName;
    public List<string> tags;
    public string description;
}