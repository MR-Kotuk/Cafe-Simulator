using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> _eat;

    [SerializeField] private List<TMP_Text> _isBuyChairsText;
    [SerializeField] private List<TMP_Text> _isBuyTablesText;
    [SerializeField] private List<TMP_Text> _isBuyColorsText;
    [SerializeField] private List<TMP_Text> _isBuyDecorText;
    [SerializeField] private List<TMP_Text> _isBuyEatText;
    [SerializeField] private List<GameObject> _reklamImageColor; 

    [SerializeField] private List<string> _nameObj, _nameSelectObj, _eatName;

    [SerializeField] private GameObject[] _menu;

    [SerializeField] private Payments _payments;

    [SerializeField] private int _payCustom;

    private int myMoney;
    private int _nextMenu = 0;
    private const int _buyTrue = 1;

    private string _name;

    private void Start()
    {
        _nextMenu = 0;
        for (int i = 0; i < _nameObj.Count; i++)
            PlayerPrefs.SetInt($"{_nameObj[i]} 0", _buyTrue);

    }
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        for (int i = 0; i < _eat.Count; i++)
        {
            if (PlayerPrefs.GetInt(_eatName[i]) == _buyTrue)
                _isBuyEatText[i].text = "Куплено";
         
            if (myMoney >= _payments._unblockPay[i] || PlayerPrefs.GetInt(_eatName[i]) == _buyTrue)
                _eat[i].SetActive(false);
            else
                _eat[i].SetActive(true);
        }

        IsBuyText(_isBuyColorsText, _nameObj[0], _nameSelectObj[0]);
        IsBuyText(_isBuyChairsText, _nameObj[1], _nameSelectObj[1]);
        IsBuyText(_isBuyTablesText, _nameObj[2], _nameSelectObj[2]);
        IsBuyText(_isBuyDecorText, _nameObj[3], _nameSelectObj[3]);

    }

    private void IsBuyText(List<TMP_Text> text, string name, string nameSelect)
    {
        for (int y = 0; y < text.Count; y++)
            IsBuyText(text[y], y, name, nameSelect);
    }
    private void IsBuyText(TMP_Text text, int num, string name, string nameSelect)
    {
        if (PlayerPrefs.GetInt($"{name} {num}") == _buyTrue)
        {
            if (PlayerPrefs.GetInt(nameSelect) == num)
                text.text = "Исп.";
            else
                text.text = "Куплено";
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
        switch (_name)
        {
            case "Color":
                BuyCustom(_nameObj[0], _nameSelectObj[0], num, _payCustom);
                break;
            case "Chair":
                BuyCustom(_nameObj[1], _nameSelectObj[1], num, _payCustom);
                break;
            case "Table":
                BuyCustom(_nameObj[2], _nameSelectObj[2], num, _payCustom);
                break;
            case "Decor":
                BuyCustom(_nameObj[3], _nameSelectObj[3], num, _payCustom);
                break;
        }
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

    public void TypeOfBuy(string name)
    {
        _name = name;
    }
    public void OnBuyReklam(int num)
    {
        if (PlayerPrefs.GetInt($"{_name} {num}") != _buyTrue)
        {
            ShowReklam();
            PlayerPrefs.SetInt($"{_name} {num}", _buyTrue);
        }

        for(int i = 0; i < _nameObj.Count; i++)
            if (_name == _nameObj[i])
                PlayerPrefs.SetInt(_nameSelectObj[i], num);
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
