using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSoda : MonoBehaviour
{
    [SerializeField] private GameObject _sodaAnBar;
    [SerializeField] private RandomOrder _randomOrder;
    [SerializeField] private GameContr _gameContr;

    private void OnMouseDown()
    {
        if (_randomOrder.isOrder)
        {
            _gameContr._myOrderObj = _sodaAnBar;
            _gameContr.ClientPay();

            _sodaAnBar.SetActive(true);

            _randomOrder.isOrder = false;
            _randomOrder.isDone = true;

            Invoke("EnableFalse", 1f);
        }
    }

    private void EnableFalse()
    {
        _sodaAnBar.SetActive(false);
    }
}