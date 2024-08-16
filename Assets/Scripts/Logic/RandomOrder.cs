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

    [SerializeField] private List<string> _objName;

    [SerializeField] private GameObject _orderUI;
    [SerializeField] private GameObject _client;

    private int _orderNum;
    private readonly float _maxTime = 100;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MenuTutorials"))
        {
            PlayerPrefs.SetInt("soda", 2);
            PlayerPrefs.SetInt("donut", 2);
        }
        time = _maxTime;
        isTimes = true;

        for(int i = 0; i < _objOrder.Count; i++)
            AddObjectsToGame(_objOrder[i], _obj[i], _objName[i]);

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

        if (!PlayerPrefs.HasKey("Tutorial"))
            _orderNum = Random.Range(0, 1);
        else
            _orderNum = Random.Range(0, _orders.Count);

        _orders[_orderNum].SetActive(true);
        _orderUI.SetActive(true);

        _gameContr.OrderObj = _orders[_orderNum];

        isOrder = true;
    }

    private void AddObjectsToGame(GameObject objectOrder, GameObject objectAnBar, string name)
    {
        if (PlayerPrefs.GetInt(name) > 0)
        {
            AddObjectToList(_orders, objectOrder);
            AddObjectToList(_ordersObjects, objectAnBar);
        }
        else
        {
            RemoveObjectInList(_orders, objectOrder);
            RemoveObjectInList(_ordersObjects, objectAnBar);
        }
    }

    private void AddObjectToList(List<GameObject> list, GameObject addObject)
    {
        if (!list.Contains(addObject))
            list.Add(addObject);
    }
    private void RemoveObjectInList(List<GameObject> list,  GameObject removeObject)
    {
        if (list.Contains(removeObject))
            list.Remove(removeObject);
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
