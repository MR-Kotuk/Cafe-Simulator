using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] _cola, _soda, _donut, _desert;
    [SerializeField] private Payments _payments;

    private int myMoney;
    private void Update()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney");

        if (myMoney >= _payments._unblockColaPay)
            Unblock(_cola);
        if (myMoney >= _payments._unblockDesertPay)
            Unblock(_desert);
        if (myMoney >= _payments._unblockSodaPay)
            Unblock(_soda);
        if (myMoney >= _payments._unblockDonutPay)
            Unblock(_donut);
    }

    private void Unblock(GameObject[] _unblock)
    {
        _unblock[1].SetActive(false);
        _unblock[0].SetActive(true);
    }

    public void OnBuy(string name)
    {
        Debug.Log(name);
        if(name == "cola")
            PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockColaPay);
        else if (name == "soda")
            PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockSodaPay);
        else if (name == "desert")
            PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockDesertPay);
        else if (name == "donut")
            PlayerPrefs.SetInt("MyMoney", myMoney -= _payments._unblockDonutPay);

        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("MyMoney"));
    }
}
