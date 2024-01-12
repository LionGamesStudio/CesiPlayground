using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities.Sound
{
    [RequireComponent(typeof(AudioSource))]
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

        public void OnBeforeDestroy(Action lastAction)
        {
            // GameObject become invisible
            if (GetComponent<Renderer>() != null)
                GetComponent<Renderer>().enabled = false;

            // Disable all children
            DisableChildren(transform);


            // Play the sound
            sourceAudio.Play();

            // Wait for the sound to finish
            StartCoroutine(WaitForSound(lastAction));
        }

        IEnumerator WaitForSound(Action lastAction)
        {
            // Wait for the sound to finish
            yield return new WaitForSeconds(sourceAudio.clip.length);

            lastAction();

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
}
