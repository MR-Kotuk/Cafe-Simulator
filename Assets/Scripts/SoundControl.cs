using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private RandomOrder _randomOrder;
    [SerializeField] private OnCoffee _onCoffee;

    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _buttonSFX;
    [SerializeField] private AudioSource _coffeeMakerSFX;
    [SerializeField] private AudioSource _poulSFX;
    [SerializeField] private AudioSource _timerSFX;

    private bool isTime = true;
    public void OnButtonAnCoffeeMaker()
    {
        if(_onCoffee.isInMaker)
            _buttonSFX.Play();
    }

    public void OnPoulButton()
    {
        if (_onCoffee.isInMaker)
        {
            _coffeeMakerSFX.Play();
            _poulSFX.Play();
        }
    }

    private void Update()
    {
        if (_randomOrder.time <= 8f && isTime)
        {
            isTime = false;
            _timerSFX.Play();
            _anim.SetBool("isWork", true);
        }
        else if(_randomOrder.time <= 0 || _randomOrder.time > 25)
            _anim.SetBool("isWork", false);

        if (_randomOrder.time <= 0.5f)
            _timerSFX.Pause();
    }
}
