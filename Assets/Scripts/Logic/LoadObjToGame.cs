using System.Collections.Generic;
using UnityEngine;

public class LoadObjToGame : MonoBehaviour
{

    [SerializeField] private Material[] _materials;

    [SerializeField] private List<GameObject> _chairs, _tables, _decor, _lamp;

    [SerializeField] private GameObject _walls, _ceiling;

    private void Awake()
    {
        NewColor(_materials[PlayerPrefs.GetInt("ColorWalls")]);
        NewCustomObj(_chairs, "Chairs");
        NewCustomObj(_tables, "Tables");
        NewCustomObj(_decor, "Decors");
        NewCustomObj(_lamp, "Lamps");
    }

    private void NewColor(Material material)
    {
        _walls.GetComponent<MeshRenderer>().material = material;
        _ceiling.GetComponent<MeshRenderer>().material = material;
    }

    private void NewCustomObj(List<GameObject> _obj, string name)
    {
        for (int i = 0; i < _obj.Count; i++)
            _obj[i].SetActive(false);

        _obj[PlayerPrefs.GetInt(name)].SetActive(true);
    }
}
