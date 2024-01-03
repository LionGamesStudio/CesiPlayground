using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyPlaySound : MonoBehaviour
{
    // The sound to play when the object is destroyed
    public AudioClip destructionSound;

    // The audio source to play the sound on
    private AudioSource sourceAudio;

    void Start()
    {
        sourceAudio = gameObject.AddComponent<AudioSource>();
        sourceAudio.clip = destructionSound;

        // Remove the start delay
        sourceAudio.playOnAwake = false;
    }

    public void OnBeforeDestroy()
    {
        // GameObject become invisible
        if (GetComponent<Renderer>() != null)
            GetComponent<Renderer>().enabled = false;

        // Disable all children
        DisableChildren(transform);
       

        // Play the sound
        sourceAudio.Play();

        // Wait for the sound to finish
        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        // Wait for the sound to finish
        yield return new WaitForSeconds(sourceAudio.clip.length);

        // Destroy the object
        Destroy(gameObject);

    }

    // Recursively disable all children
    void DisableChildren(Transform t)
    {
        foreach (Transform child in t)
        {
            child.gameObject.SetActive(false);
            if (child.childCount > 0)
                DisableChildren(child);
        }
    }

}
