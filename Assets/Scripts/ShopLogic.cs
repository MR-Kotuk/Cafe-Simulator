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
    private void Start()
    {
        PlayerPrefs.SetInt("Color 0", 1);
        PlayerPrefs.SetInt("Table 0", 1);
        PlayerPrefs.SetInt("Chair 0", 1);
    }
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

    public void OnBuyCustom(int num)
    {
        if(num >= 10)
            BuyCustom("Table", "Tables", num - 10, 150);
        else
            BuyCustom("Chair", "Chairs", num, 150);
    }

    public void OnBuyColor(int numColor)
    {
        BuyCustom("Color", "ColorWalls", numColor, 150);
    }

    private void BuyCustom(string nameObj, string nameSelectObj, int num, int _pay)
    {
        if (PlayerPrefs.GetInt($"{nameObj} {num}") == _buyTrue)
            PlayerPrefs.SetInt(nameSelectObj, num);
        else if (myMoney >= _pay)
        {
            PlayerPrefs.SetInt("MyMoney", myMoney -= _pay);
            PlayerPrefs.SetInt($"{nameObj} {num}", _buyTrue);
            PlayerPrefs.SetInt(nameSelectObj, num);
        }
    }

    private string _name;
    public void TypeOfReklamBuy(string name)
    {
        _name = name;
    }
    public void OnBuyReklam(int num)
    {
        Debug.Log(_name);
        Debug.Log(num);
        if (num >= 10)
            num -= 10;
        if (PlayerPrefs.GetInt($"{_name} {num}") != _buyTrue)
        {
            ShowReklam();
            PlayerPrefs.SetInt($"{_name} {num}", _buyTrue);
        }

        if (_name == "Color")
            PlayerPrefs.SetInt("ColorWalls", num);
        else if (_name == "Chair")
            PlayerPrefs.SetInt("Chairs", num);
        else if (_name == "Table")
            PlayerPrefs.SetInt("Tables", num);
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

    private void ShowReklam()
    {
        Debug.Log("Reklam");
        //Add Reklam Logic
    }
}
