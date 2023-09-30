using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomOrder : MonoBehaviour
{
    public bool isOrder = false;
    public bool isDone = false;
    public bool isGame = false;
    public bool isTimes;
    public float time;

    [SerializeField] private GameContr _gameContr;

    [SerializeField] private Image _timeBar;

    [SerializeField] private List<GameObject> _orders;
    [SerializeField] private List<GameObject> _ordersObjects;
    [SerializeField] private List<GameObject> _objOrder, _obj;
    [SerializeField] private List<GameObject> _chairsCustom;
    [SerializeField] private List<GameObject> _tablesCustom;

    [SerializeField] private GameObject _orderUI;
    [SerializeField] private GameObject _client;

    [SerializeField] private List<string> _objName;

    private int _orderNum;
    private readonly float _maxTime = 100;

    private void Start()
    {
        time = _maxTime;
        isTimes = true;

        for(int i = 0; i < _objOrder.Count; i++)
            AddObjectsToList(_objOrder[i], _obj[i], _objName[i]);

        for (int i = 0; i < _ordersObjects.Count; i++)
            _ordersObjects[i].SetActive(true);
    }

    private void Update()
    {
        if (isGame)
            CreateClients();

        if (!isOrder)
        {
            _orderUI.SetActive(false);
            _orders[_orderNum].SetActive(false);
        }
    }
    public void CreateOrder()
    {
        isTimes = true;
        StartCoroutine(TimeMake());

        _orderNum = Random.Range(0, _orders.Count);

        _orders[_orderNum].SetActive(true);
        _orderUI.SetActive(true);

        _gameContr.OrderObj = _orders[_orderNum];

        isOrder = true;
    }

    private void AddObjectsToList(GameObject objectOrder, GameObject objectAnBar, string name)
    {
        if (PlayerPrefs.GetInt(name) == 1)
        {
            _orders.Add(objectOrder);
            _ordersObjects.Add(objectAnBar);
        }
    }

    private void CreateClients()
    {
        isGame = false;
        GameObject _createdObject = Instantiate(_client);
        _createdObject.SetActive(true);
    }

    private IEnumerator TimeMake()
    {
        while (isTimes)
        {
            if (time >= 0)
            {
                yield return new WaitForSeconds(0.2f);
                time -= 0.4f;
                _timeBar.fillAmount = time / _maxTime;
            }
            else
                isTimes = false;
        }
    }
}
