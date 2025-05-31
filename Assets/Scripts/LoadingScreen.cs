using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    //public Slider progressBar; // Можно оставить null, если не нужен

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoader.nextScene);

        yield return new WaitForSeconds(10f);
        while (!operation.isDone)
        {
           /* if (progressBar != null)
                progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);*/

            yield return new WaitForSeconds(10f);
        }
    }
}
