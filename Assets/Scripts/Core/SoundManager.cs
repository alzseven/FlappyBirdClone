using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<SoundManager>();
                    if (_instance == null)
                    {
                        var go = new GameObject("@SoundManager");
                        _instance = go.AddComponent<SoundManager>();
                    }
                }
                return _instance;
            }
        }

        private static SoundManager _instance;

        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioSource _sfxSource;
        //TODO: Separate
        [SerializeField] private List<AudioClip> _clips;
        
        private void Awake()
        {
            //TODO:
            // if (_instance == null) _instance = this;
            // else Destroy(gameObject);
        }

        public void PlayBackGroundMusic(string audioName)
        {
            if (_clips.Find(clip => clip.name == audioName))
            {
                PlayBackGroundMusic(_clips.Find(clip => clip.name == audioName));
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        
        //TODO:
        public void PlayBackGroundMusic(AudioClip clipToPlay)
        {
            _bgmSource.loop = true;
            _bgmSource.clip = clipToPlay;
            _bgmSource.Play();
        }
        
        public void PlaySfx(string audioName)
        {
            if (_clips.Find(clip => clip.name == audioName))
            {
                PlaySfx(_clips.Find(clip => clip.name == audioName));
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        
        //TODO:
        public void PlaySfx(AudioClip clipToPlay)
        {
            _sfxSource.PlayOneShot(clipToPlay);
        }
    }
}