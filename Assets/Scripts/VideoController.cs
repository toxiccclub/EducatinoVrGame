using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoCanvas;

    public void PlayVideo()
    {
        videoCanvas.SetActive(true);
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Pause();
       //videoCanvas.SetActive(false);
    }
}
