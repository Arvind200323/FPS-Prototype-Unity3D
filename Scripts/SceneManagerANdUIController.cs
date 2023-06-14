using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerANdUIController : MonoBehaviour
{
    public void PLay(){
        SceneManager.LoadScene(1);
    }

    public void Quitt(){
        Application.Quit();
    }
}
