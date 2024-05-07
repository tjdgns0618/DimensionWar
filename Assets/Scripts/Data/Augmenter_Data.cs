using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="Augmenter",menuName = "Scriptble Object/Augmenter_Data")]
public class Augmenter_Data : ScriptableObject
{
    public int number;

    [Header("# Main Info")]
    public string Augmenter_Name;

    [TextArea]
    public string Augmenter_Desc = "0";

    [Header("# Image")]
    public Sprite image;

    [Header("# Ctrl_Num")]
    public int num;

    void gostring()
    {
        Augmenter_Desc = num.ToString();
    }
}
