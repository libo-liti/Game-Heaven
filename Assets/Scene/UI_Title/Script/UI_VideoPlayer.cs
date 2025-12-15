using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class UI_VideoPlayer : MonoBehaviour // 영상에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    public UI_GameSelect gameSelect;
    [SerializeField] string VideoFileName;

    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        PlayVideo();
        StartCoroutine(StopVideoAfterDelay(0.1f));
    }

    private IEnumerator<object> StopVideoAfterDelay(float delay)  // IEnumerator 제네릭 타입을 object로 지정
    {
        yield return new WaitForSeconds(delay);
        videoPlayer.Stop();
    }

    private void Update()
    {
        if (gameSelect != null)
        {
            if ((gameSelect.GameSelect == 0 && VideoFileName == "Bullet_Scene 5sec.mp4") ||
                (gameSelect.GameSelect == 1 && VideoFileName == "Hand_Scene 5sec.mp4") ||
                (gameSelect.GameSelect == 2 && VideoFileName == "AS_Stone 5sec.mp4") ||
                (gameSelect.GameSelect == 3 && VideoFileName == "War_Scene 5sec.mp4"))
            {
                if (!videoPlayer.isPlaying)
                {
                    PlayVideo();
                }
            }
            else
            {
                if (videoPlayer.isPlaying)
                    videoPlayer.Stop();
            }
        }

        // gameSelect가 null이면서 VideoFileName이 "Title_Video.mp4"인 경우에만 비디오 재생
        if (gameSelect == null && VideoFileName == "Title_Video.mp4")
        {
            if (!videoPlayer.isPlaying)
            {
                PlayVideo();
            }
        }
    }

    public void PlayVideo()
    {
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, VideoFileName);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }
}
