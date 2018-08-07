using UnityEngine;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(AudioSource))]
	public class  MusicView : MonoBehaviour, IView
	{
		private const string MuteProp = "music_mute";
		
		private AudioSource m_audio;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_audio = GetComponent<AudioSource>();
			
			entity.isMusic = true;
			entity.AddMute(() => PlayerPrefs.GetInt(MuteProp) == 0);
			entity.AddMuteUpdate(mute =>
			{
				PlayerPrefs.SetInt(MuteProp, mute ? 0 : 1); 
				m_audio.mute = mute;
			});
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			
		}
	}
}
