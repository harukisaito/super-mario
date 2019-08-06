using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

	[SerializeField] Sound[] sounds;

	public static AudioManager instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.loop = s.loop;
		}
	}

	void Start()
	{
		Play(GameController.instance.Music);
	}
	
	
	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
	}
	
	public void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Stop();
	}

	public void Pause(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Pause();
	}

	public void UnPause(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.UnPause();
	}

	public void StopAllCurrentSounds()
	{
		foreach(Sound s in sounds)
		{
			if(s.source.isPlaying)
			{
				s.source.Stop();
			}
		}
	}

	public void MuteAllSounds()
	{
		foreach(Sound s in sounds)
		{
			s.source.mute = true;
		}
	}

	public void UnMuteAllSounds()
	{
		foreach(Sound s in sounds)
		{
			s.source.mute = false;
		}
	}

	public void PauseAllSounds()
	{
		foreach(Sound sound in sounds)
		{
			sound.source.Pause();
		}
		Sound s = Array.Find(sounds, sound => sound.name == "Pause");
		s.source.Play();
	}

	public void UnPauseAllSounds()
	{
		foreach(Sound s in sounds)
		{
			s.source.UnPause();
		}
	}
}
