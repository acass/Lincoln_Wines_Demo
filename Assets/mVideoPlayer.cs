using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class mVideoPlayer : MonoBehaviour {

    public Renderer mVideoImage;

    public VideoClip intVideo, wBorn, wStart, wVicePres;

    VideoPlayer videoPlayer;
    VideoSource videoSource;
    AudioSource audioSource;

    void Start() {

        StartCoroutine(playVideo(intVideo));
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += EndReached;
    }

   public void PlayThisVideo(string mString) {

        switch (mString) {

            case "wBorn":
                StartCoroutine(playVideo(wBorn));
                break;
            case "wStart":
                StartCoroutine(playVideo(wStart));
                break;
            case "wVicePres":
                StartCoroutine(playVideo(wVicePres));
                break;
            default:
                print("No Video Played");
                break;
        }
    }

    void EndReached(VideoPlayer videoPlayer) {
        
        Debug.Log("Video is Done!!!");
    }

   public IEnumerator playVideo(VideoClip videoToPlay) {


        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        videoPlayer.source = VideoSource.VideoClip;

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return waitTime;
            break;
        }

        Debug.Log("Done Preparing Video");

        mVideoImage.material.mainTexture = videoPlayer.texture;

        videoPlayer.Play();

        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying) {
            
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }

}