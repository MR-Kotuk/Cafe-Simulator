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
    private int _nextMenu = 0;
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        for (int i = 0; i < _eat.Count; i++)
        {
            if (myMoney >= _payments._unblockPay[i])
                _eat[i].SetActive(false);
            else
                _eat[i].SetActive(true);
        }

    }

    public void OnBuy(string name)
    {
        if (PlayerPrefs.GetInt(name) != _buyTrue)
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
            //Add Reklam Logic




            PlayerPrefs.SetInt($"Color {numColor}", _buyTrue);
        }


        PlayerPrefs.SetInt("ColorWalls", numColor);
    }

    public void NextMenu()
    {
        for (int i = 0; i < _menu.Length; i++)
            _menu[i].SetActive(false);

        _nextMenu++;

        if (_nextMenu >= _menu.Length)
            _nextMenu = 0;

        _menu[_nextMenu].SetActive(true);
    }
}
