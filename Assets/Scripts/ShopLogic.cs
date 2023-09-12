using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> _eat;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private Payments _payments;

    private int myMoney;
    private bool isBuy;
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        for (int i = 0; i < _eat.Count; i++)
            Unblock(_eat[i], _payments._unblockPay[i]);
    }

    private void Unblock(GameObject _unblock, int how)
    {
        if (myMoney >= how)
        {
            _unblock.SetActive(false);
            isBuy = true;
        }
        else
        {
            _unblock.SetActive(true);
            isBuy = false;
        }
    }

    public void OnBuy(string name)
    {
        if (isBuy && PlayerPrefs.GetInt(name) != 1)
        {
            if (name == "cola")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPay[0]);
            else if (name == "soda")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPay[1]);
            else if (name == "donut")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPay[2]);
            else if (name == "desert")
                PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPay[3]);

            PlayerPrefs.SetInt(name, 1);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyColor(int numColor)
    {
        if(myMoney >= _payments._unblockPayColor[numColor])
        {
            PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPayColor[numColor]);
            PlayerPrefs.SetInt("ColorWalls", numColor);
        }
    }
    public void OnBuyReklamColor(int numColor)
    {
        //Add Reklam Logic





        PlayerPrefs.SetInt("ColorWalls", numColor);
    }

    public void NextMenu()
    {
        for (int i = 0; i < _menu.Length; i++)
            _menu[i].SetActive(!_menu[i].activeInHierarchy);
    }
}
