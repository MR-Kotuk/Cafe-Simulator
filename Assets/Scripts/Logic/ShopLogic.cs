using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _isBuyChairsText;
    [SerializeField] private List<TMP_Text> _isBuyTablesText;
    [SerializeField] private List<TMP_Text> _isBuyColorsText;
    [SerializeField] private List<TMP_Text> _isBuyDecorText;
    [SerializeField] private List<TMP_Text> _isBuyLampText;
    [SerializeField] private List<TMP_Text> _isBuyEatText;
    [SerializeField] private TMP_Text[,] _isBuyText;

    [SerializeField] private List<GameObject> _reklamImageColor;

    [SerializeField] private List<string> _nameObj, _nameSelectObj, _eatName;

    [SerializeField] private GameObject[] _menu;

    [SerializeField] private Payments _payments;

    [SerializeField] private AudioSource _paySFX, _buttonSFX;

    [SerializeField] private int _payCustom;

    private int myMoney;
    private int _nextMenu;
    private int numPay;
    private readonly int _buyTrue = 1;

    private string _name;

    private void Start()
    {
        _nextMenu = 0;

        for (int i = 0; i < _menu.Length; i++)
            _menu[i].SetActive(false);
        _menu[_nextMenu].SetActive(true);

        for (int i = 0; i < _nameObj.Count; i++)
            PlayerPrefs.SetInt($"{_nameObj[i]} 0", _buyTrue);
    }
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        IsBuyText(_isBuyColorsText, 0);
        IsBuyText(_isBuyChairsText, 1);
        IsBuyText(_isBuyTablesText, 2);
        IsBuyText(_isBuyDecorText, 3);
        IsBuyText(_isBuyLampText, 4);
    }
    
    private void IsBuyText(List<TMP_Text> text, int num)
    {
        for (int i = 0; i < text.Count; i++)
        {
            if (PlayerPrefs.GetInt($"{_nameObj[num]} {i}") == _buyTrue)
            {
                if (PlayerPrefs.GetInt(_nameSelectObj[num]) == i)
                    text[i].text = "Исп.";
                else
                    text[i].text = "Куплено";
            }
        }
    }
    public void OnBuy(string name)
    {
        if (name == "cola")
            numPay = 0;
        else if (name == "soda")
            numPay = 1;
        else if (name == "donut")
            numPay = 2;
        else if (name == "desert")
            numPay = 3;

        if (PlayerPrefs.GetInt("MyMoney") >= _payments._unblockPay[numPay])
        {
            _paySFX.Play();

            PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockPay[numPay]);
            PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name) + Random.Range(2, 3));
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
            case "Lamp":
                BuyCustom(_nameObj[4], _nameSelectObj[4], num, _payCustom);
                break;
        }
    }

    private void BuyCustom(string nameObj, string nameSelectObj, int num, int _pay)
    {
        if (PlayerPrefs.GetInt($"{nameObj} {num}") == _buyTrue)
            PlayerPrefs.SetInt(nameSelectObj, num);
        else if (myMoney >= _pay)
        {
            _paySFX.Play();

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

    public void NextMenu(bool isNext)
    {
        _buttonSFX.Play();

        for (int i = 0; i < _menu.Length; i++)
            _menu[i].SetActive(false);

        if (isNext)
            _nextMenu++;
        else
            _nextMenu--;

        if (_nextMenu >= _menu.Length)
            _nextMenu = 0;
        else if (_nextMenu < 0)
            _nextMenu = _menu.Length - 1;

        _menu[_nextMenu].SetActive(true);
    }

    private void ShowReklam()
    {
        Debug.Log("Reklam");
        //Add Reklam Logic
    }
}
