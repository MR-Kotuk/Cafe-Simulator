using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCoffee : MonoBehaviour
{
    [SerializeField] private GameObject _coffeeInMaker;
    [SerializeField] private GameObject _coffeeOnBar;
    [SerializeField] private GameObject _coffeeOnBarPlus;
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _animMilk;
    [SerializeField] private RandomOrder _randomOrder;
    [SerializeField] private GameContr _gameContr;


    public bool isInMaker = false;
    private bool isMilk = false;
    private bool isPoulCoffee = false;

    private void OnMouseDown()
    {
        if (_randomOrder.isOrder)
        {
            _randomOrder.isOrder = false;
            isInMaker = true;

            _coffeeInMaker.SetActive(true);
        }   
    }

    public void OnButtonCoffeeMaker()
    {
        if (isInMaker)
        {
            isPoulCoffee = true;

            if (isMilk)
                _animMilk.SetBool("isPoul", true);
            _anim.SetBool("isPoul", true);

            Invoke("PoulIsFalse", 5f);
        }
    }

    public void AddMilk()
    {
        if(isInMaker && !isPoulCoffee)
            isMilk = true;
    }

    public void IsDone()
    {
        if (!isInMaker && isPoulCoffee)
        {
            _coffeeInMaker.SetActive(false);
            _coffeeOnBar.SetActive(true);

            if (isMilk)
                _gameContr.MyOrderObj = _coffeeOnBarPlus;
            else
                _gameContr.MyOrderObj = _coffeeOnBar;

            _gameContr.ClientPay();

            _randomOrder.isDone = true;

            Invoke("EnableFalse", 1f);
        }
    }

    private void PoulIsFalse()
    {
        isInMaker = false;

        _anim.SetBool("isPoul", false);
        _animMilk.SetBool("isPoul", false);
    }

    private void EnableFalse()
    {
        _coffeeOnBar.SetActive(false);

        _randomOrder.isDone = false;
        _randomOrder.isOrder = false;

        isPoulCoffee = false;
        isMilk = false;
    }
}
