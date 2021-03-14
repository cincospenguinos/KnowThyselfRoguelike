using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public AudioSource IncidentalSoundSource;
    private List<AudioClip> _audioClips;
    private double _nextSoundTime;

    void Start() {
        _nextSoundTime = Time.timeAsDouble + Random.Range(10, 50);
        _audioClips = new List<AudioClip>(new AudioClip[] {
            (AudioClip) Resources.Load("Sounds/dark_one0"),
            (AudioClip) Resources.Load("Sounds/dark_one1"),
            (AudioClip) Resources.Load("Sounds/stutter0"),
            (AudioClip) Resources.Load("Sounds/stutter1"),
            (AudioClip) Resources.Load("Sounds/stutter2"),
            (AudioClip) Resources.Load("Sounds/nosce_te_ipsem"),
        });

        IncidentalSoundSource.volume = 0.0f;
        IncidentalSoundSource.PlayOneShot(_audioClips[5]);
        StartCoroutine(FadeMixerGroup.StartFade(IncidentalSoundSource, 8f, 0.125f));
    }

    void Update() {
        if (_nextSoundTime < Time.timeAsDouble) {
            AudioClip clip = _audioClips.GetRandom();

            // If we are playing Nosce Te Ipsem, we will want to treat the volume in certain ways and
            // wait a while before we play anything else
            if (_audioClips.IndexOf(clip) == _audioClips.Count - 1) {
                IncidentalSoundSource.volume = 0.0f;
                IncidentalSoundSource.PlayOneShot(clip);
                StartCoroutine(FadeMixerGroup.StartFade(IncidentalSoundSource, 8f, 0.125f));
                _nextSoundTime = Time.timeAsDouble + 180;
            } else {
                IncidentalSoundSource.volume = 0.20f;
                IncidentalSoundSource.PlayOneShot(clip);
                _nextSoundTime = Time.timeAsDouble + Random.Range(10, 50);
            }
        }
    }
}
