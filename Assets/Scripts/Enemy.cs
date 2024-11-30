using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;
    [SerializeField] AudioClip destroyedSFX;
    [SerializeField] int hitPoints = 3;
    [SerializeField] int scoreValue = 10;

    Scoreboard scoreboard;
    AudioSource audioSource;

    private void Start()
    {
        scoreboard = FindFirstObjectByType<Scoreboard>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(destroyedSFX);
            }
            scoreboard.IncreaseScore(scoreValue);
            Instantiate(destroyedVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
