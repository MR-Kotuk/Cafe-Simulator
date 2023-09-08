using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Payments", menuName = "ScrObj/Payments")]
public class Payments : ScriptableObject
{
    public int _payCola, _paySoda, _payCoffee, _payCoffeePlus;
    public int _payDesert, _payDonut;

    public int _unblockColaPay, _unblockSodaPay, _unblockDesertPay, _unblockDonutPay;
}
