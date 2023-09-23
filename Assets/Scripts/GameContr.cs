﻿using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContr : MonoBehaviour
{
    public GameObject OrderObj;
    public GameObject MyOrderObj;
    public int ProbabilityTips;

    [SerializeField] private TMP_Text _myMoneyText;
    [SerializeField] private TMP_Text _tipsText;
    [SerializeField] private TMP_Text _workDays;

    [SerializeField] private Material[] _materials;
    [SerializeField] private List<GameObject> _chairs, _tables;

    [SerializeField] private GameObject _imageAngry;
    [SerializeField] private GameObject _walls, _ceiling;

    [SerializeField] private int _limitTips;

    [SerializeField] private Payments _payments;
    [SerializeField] private RandomOrder _randomOrder;

    private int _range;
    private int _profit;
    private int _howDays;
    private int _tips;

    private void Start()
    {
        int numColor = PlayerPrefs.GetInt("ColorWalls");
        int numChairs = PlayerPrefs.GetInt("Chairs");
        int numTables = PlayerPrefs.GetInt("Tables");

        NewColor(_materials[numColor]);
        NewCustomObj(_chairs, numChairs);
        NewCustomObj(_tables, numTables);

        _range = PlayerPrefs.GetInt("MyMoney");
        _howDays = PlayerPrefs.GetInt("HowDays");

        _profit = 0;
        _howDays++;

        WorkTime();
    }

    private void NewCustomObj(List<GameObject> _obj, int num)
    {
        for (int i = 0; i < _obj.Count; i++)
            _obj[i].SetActive(false);

        _obj[num].SetActive(true);
    }

    private void NewColor(Material material)
    {
        _walls.GetComponent<MeshRenderer>().material = material;
        _ceiling.GetComponent<MeshRenderer>().material = material;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            _profit += 100;

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();

            _howDays = 0;
            _range = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
            EndGame();

        _myMoneyText.text = $"{_range + _profit}";
        _workDays.text = _howDays + "";
    }

    private void EndGame()
    {
        SavePrefs();
        SceneManager.LoadScene("EndWorkDayScene");
    }

    public void ClientPay()
    {
        if (_randomOrder.isTimes)
        {
            if (OrderObj.gameObject.tag == MyOrderObj.gameObject.tag)
            {
                switch (OrderObj.gameObject.tag)
                {
                    case "cola":
                        _profit += _payments._payCola;
                        break;
                    case "soda":
                        _profit += _payments._paySoda;
                        break;
                    case "coffee":
                        _profit += _payments._payCoffee;
                        break;
                    case "coffee+":
                        _profit += _payments._payCoffeePlus;
                        break;
                    case "donut":
                        _profit += _payments._payDonut;
                        break;
                    case "desert":
                        _profit += _payments._payDesert;
                        break;
                }
                if (Random.Range(0, ProbabilityTips) == 0)
                {
                    _tips = Random.Range(5, _limitTips);

                    if (_tips > 0)
                    {
                        _tipsText.text = "+ " + _tips + " Чаевые";
                        Invoke("Tips", 3f);
                    }

                    _profit += _tips;
                }
                SavePrefs();
            }
            else
            {
                _imageAngry.SetActive(true);
                Invoke("AngryImage", 2f);
            }
        }
    }

    private void AngryImage()
    {
        _imageAngry.SetActive(false);
    }
    private void Tips()
    {
        _tipsText.text = " ";
    }

    private void WorkTime()
    {
        switch (_howDays)
        {
            case < 5:
                StartCoroutine(TimeToEnd(120f));
                break;
            case < 15:
                StartCoroutine(TimeToEnd(180f));
                break;
            case < 30:
                StartCoroutine(TimeToEnd(240f));
                break;
            case > 30:
                StartCoroutine(TimeToEnd(300f));
                break;
        }
    }

    private IEnumerator TimeToEnd(float wait)
    {
        yield return new WaitForSeconds(wait);
        EndGame();
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("MyMoney", _range);
        PlayerPrefs.SetInt("HowDays", _howDays);
        PlayerPrefs.SetInt("Profit", _profit);

        PlayerPrefs.Save();
    }
}
