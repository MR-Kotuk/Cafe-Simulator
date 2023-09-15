using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> _eat;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private Payments _payments;

    private int myMoney;
    private int _buyTrue = 1;
    private bool isBuy;
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        for (int i = 0; i < _eat.Count; i++)
        {
            if (myMoney >= _payments._unblockPay[i])
                Unblock(_eat[i], true);
            else
                Unblock(_eat[i], false);
        }

    }

    private void Unblock(GameObject _block, bool isValue)
    {
        _block.SetActive(!isValue);
        isBuy = isValue;
    }

    public void OnBuy(string name)
    {
        if (isBuy && PlayerPrefs.GetInt(name) != _buyTrue)
        {
            for(int i = 0; i < _eat.Count; i++)
                if(name == _eat[i].tag)
                    PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPay[i]);

            PlayerPrefs.SetInt(name, _buyTrue);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyColor(int numColor)
    {
        if(PlayerPrefs.GetInt($"Color {numColor}") == _buyTrue)
            PlayerPrefs.SetInt("ColorWalls", numColor);
        else if (myMoney >= _payments._unblockPayColor[numColor])
        {
            PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPayColor[numColor]);
            PlayerPrefs.SetInt($"Color {numColor}", _buyTrue);
            PlayerPrefs.SetInt("ColorWalls", numColor);
        }
    }
    public void OnBuyReklamColor(int numColor)
    {
        if (PlayerPrefs.GetInt($"Color {numColor}") != _buyTrue)
        {
            Debug.Log("Reklam");

            //Add Reklam Logic




            PlayerPrefs.SetInt($"Color {numColor}", _buyTrue);
        }


        PlayerPrefs.SetInt("ColorWalls", numColor);
    }

    public void NextMenu()
    {
        for (int i = 0; i < _menu.Length; i++)
            _menu[i].SetActive(!_menu[i].activeInHierarchy);
    }
}
