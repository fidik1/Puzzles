using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadGame(int scene) => SceneManager.LoadScene(scene);
}
