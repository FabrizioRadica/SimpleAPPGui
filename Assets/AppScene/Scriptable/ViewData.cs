using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class view
{

    public string title;
    [TextArea(3, 10)]
    public string description;
    public Texture image;
    public GameObject viewPrefab;

}



[CreateAssetMenu(fileName = "ViewData", menuName = "SimpleApp-Gui/ViewData")]
public class ViewData : ScriptableObject
{
    public List<view> views = new();

}
