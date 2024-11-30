using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int numberOfMusicPlayers = FindObjectsByType<MusicPlayer>(FindObjectsSortMode.None).Length;
        if(numberOfMusicPlayers > 1)
        {
            Destroy(this.gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }

}
