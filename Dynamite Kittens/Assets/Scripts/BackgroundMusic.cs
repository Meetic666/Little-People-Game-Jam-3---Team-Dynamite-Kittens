using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	private static BackgroundMusic instance = null;
	public static BackgroundMusic Instance {
		get { return instance; }
	}

	public AudioClip m_BackgroundMusic;

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;

			audio.clip = m_BackgroundMusic;
			audio.Play();
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
