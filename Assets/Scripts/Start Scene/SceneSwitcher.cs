using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadStaticScene()
    {
        SceneManager.LoadScene("StaticMap");
    }

    public void LoadDynamicScene()
    {
        SceneManager.LoadScene("Game Template");
    }
}
