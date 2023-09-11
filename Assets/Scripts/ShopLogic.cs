using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] private GameObject _cola, _soda, _donut, _desert;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private Payments _payments;

    private int myMoney;
    private bool isBuy;
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        isCanBuy(_payments._unblockColaPay, _cola);
        isCanBuy(_payments._unblockSodaPay, _soda);
        isCanBuy(_payments._unblockDonutPay, _donut);
        isCanBuy(_payments._unblockDesertPay, _desert);
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

    public void OnBuyColor(int numColor)
    {
        //Add Reklam Logic





        PlayerPrefs.SetInt("ColorWalls", numColor);
    }

    public void NextMenu()
    {
        for (int i = 0; i < _menu.Length; i++)
            _menu[i].SetActive(!_menu[i].activeInHierarchy);
    }

    private void isCanBuy(int how, GameObject eat)
    {
        if (myMoney >= how)
            Unblock(eat, false);
        else
            Unblock(eat, true);
    }
}
