using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private RandomOrder _randomOrder;
    [SerializeField] private AudioSource _buttonSFX;
    [SerializeField] private AudioSource _coffeeMakerSFX;
    [SerializeField] private AudioSource _poulSFX;

    public void OnButtonAnCoffeeMaker()
    {
        _buttonSFX.Play();
    }

    public void OnPoulButton()
    {
        _coffeeMakerSFX.Play();
        _poulSFX.Play();
    }
}
