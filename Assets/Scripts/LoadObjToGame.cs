using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObjToGame : MonoBehaviour
{

    [SerializeField] private Material[] _materials;

    [SerializeField] private List<GameObject> _chairs, _tables, _decor, _lamp;

    [SerializeField] private GameObject _walls, _ceiling;

    private void Update()
    {
        NewColor(_materials[PlayerPrefs.GetInt("ColorWalls")]);
        NewCustomObj(_chairs, PlayerPrefs.GetInt("Chairs"));
        NewCustomObj(_tables, PlayerPrefs.GetInt("Tables"));
        NewCustomObj(_decor, PlayerPrefs.GetInt("Decors"));
        NewCustomObj(_lamp, PlayerPrefs.GetInt("Lamps"));
    }

    private void NewColor(Material material)
    {
        _walls.GetComponent<MeshRenderer>().material = material;
        _ceiling.GetComponent<MeshRenderer>().material = material;
    }

    private void NewCustomObj(List<GameObject> _obj, int num)
    {
        for (int i = 0; i < _obj.Count; i++)
            _obj[i].SetActive(false);

        _obj[num].SetActive(true);
    }
}
