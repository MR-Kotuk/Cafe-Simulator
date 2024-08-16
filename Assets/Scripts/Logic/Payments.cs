using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Payments", menuName = "ScrObj/Payments")]
public class Payments : ScriptableObject
{
    public int _payCola, _paySoda, _payCoffee, _payCoffeePlus;
    public int _payDesert, _payDonut;

    public List<int> _unblockPay;
    public List<int> _unblockPayColor;
}
