using UnityEngine;

public class SceneButtonHandler : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadSceneFromButton()
    {
        SceneLoader.LoadScene(sceneToLoad);
    }
}
