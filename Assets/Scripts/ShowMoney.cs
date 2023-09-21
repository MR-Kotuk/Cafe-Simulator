using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text _myMoneyText;

    private void Start()
    {
        int myMoney;
        myMoney = PlayerPrefs.GetInt("MyMoney") + PlayerPrefs.GetInt("Profit");

        PlayerPrefs.SetInt("MyMoney", myMoney);
    }
    private void Update()
    {
        _myMoneyText.text = $"{PlayerPrefs.GetInt("MyMoney")}";
    }
}
