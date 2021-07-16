using System.Collections.Generic;
using UnityEngine;

public class SpawnerUITanks : MonoBehaviour
{
    [SerializeField] private GameObject _uiElement;
    [SerializeField] private Transform _parent;
    
    [SerializeField]private List<GameObject> _uiElements = new List<GameObject>();

    private void Start()
    {
        SpawnUITank();
    }

    private void SpawnUITank()
    {
        for (int i = 0; i < Game.Instance.MaxSpawnTanks; i++)
        {
            _uiElements.Add(Instantiate(_uiElement, _parent));
        }
    }

    public void RemoveUITankElement()
    {
        if (_uiElements.Count > 0)
        {
            _uiElements[_uiElements.Count - 1].SetActive(false);
            _uiElements.RemoveAt(_uiElements.Count - 1);
        }
    }
}
