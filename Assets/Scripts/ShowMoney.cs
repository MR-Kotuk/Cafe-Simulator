using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text _myMoneyText;

    private int myMoney, myProfit;
    private void Start()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");
        myProfit = PlayerPrefs.GetInt("Profit");
    }
    private void Update()
    {
        PlayerPrefs.SetInt("MyMoney", myMoney + myProfit);
        int cash = PlayerPrefs.GetInt("MyMoney");

        _myMoneyText.text = $"{cash}";
    }
}
