using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] private GameObject _cola, _soda, _donut, _desert;
    [SerializeField] private Payments _payments;

    private int myMoney;
    private bool isBuy;
    private bool[] isOpen;
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        if (myMoney >= _payments._unblockColaPay)
            Unblock(_cola, false);
        else
            Unblock(_cola, true);

        if (myMoney >= _payments._unblockDesertPay)
            Unblock(_desert, false);
        else
            Unblock(_desert, true);

        if (myMoney >= _payments._unblockSodaPay)
            Unblock(_soda, false);
        else
            Unblock(_soda, true);

        if (myMoney >= _payments._unblockDonutPay)
            Unblock(_donut, false);
        else
            Unblock(_donut, true);
    }

    private void Unblock(GameObject _unblock, bool block)
    {
        if (block)
        {
            _unblock.SetActive(true);
            isBuy = false;
        }
        else
        {
            _unblock.SetActive(false);
            isBuy = true;
        }
    }

    public void OnBuy(string name)
    {
        if (isBuy && PlayerPrefs.GetInt(name) != 1)
        {
            if (name == "cola")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockColaPay);
            else if (name == "soda")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockSodaPay);
            else if (name == "desert")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockDesertPay);
            else if (name == "donut")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockDonutPay);

            PlayerPrefs.SetInt(name, 1);
            PlayerPrefs.Save();
        }
    }
}
