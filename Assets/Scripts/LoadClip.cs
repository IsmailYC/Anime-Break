using UnityEngine;
using System.Collections;

public class LoadClip : MonoBehaviour {
	public string clip;
	public AudioSource player;

	public void Load()
	{
		AudioClip temp = Resources.Load(clip) as AudioClip;
        if(player.clip == null)
        {
            player.clip = temp;
            player.Play();
        }
        else
        {
            if(player.clip.name==clip)
            {
                if (player.isPlaying)
                    player.Pause();
                else
                    player.UnPause();
            }
            else
            {
                player.clip = temp;
                player.Play();
            }
                
        }
	}
}
