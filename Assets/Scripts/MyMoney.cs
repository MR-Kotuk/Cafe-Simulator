using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyMoney", menuName = "ScrObj/MyMoney")]
public class MyMoney : ScriptableObject
{
    public int _range;
    public int _profit;
}
