using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
    [SerializeField] private Text _myMoneyText;

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
