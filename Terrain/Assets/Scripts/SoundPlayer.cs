using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip walkClip;
    [SerializeField] AudioClip shootClip;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Run() 
    {
        audioSource.PlayOneShot(runClip);
    }

    public void Walk() 
    {
        audioSource.PlayOneShot(walkClip);
    }

    public void Shoot() 
    {
        audioSource.PlayOneShot(shootClip);
    }

}
