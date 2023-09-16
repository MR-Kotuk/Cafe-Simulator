using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private RandomOrder _randomOrder;
    [SerializeField] private OnCoffee _onCoffee;
    [SerializeField] private AudioSource _buttonSFX;
    [SerializeField] private AudioSource _coffeeMakerSFX;
    [SerializeField] private AudioSource _poulSFX;

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
}
