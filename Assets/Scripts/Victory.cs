using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private CountPlacedPuzzles _countPlacedPuzzles;
    [SerializeField] private GameObject _panelWin;

    private void Start() => _countPlacedPuzzles.AllPuzzlesPlaced += Win;

    private void Win() => _panelWin.SetActive(true);

    public void Restart() => SceneManager.LoadScene(0);
}
