using UnityEngine;

//Author: Molly R�le

namespace EventCallbacks
{
    public class SoundListener : MonoBehaviour
    {
        [SerializeField]
        private AudioSource AudioSource;
        private void OnEnable() => EventSystem<SoundEvent>.RegisterListener(PlaySound);
        private void OnDisable() => EventSystem<SoundEvent>.UnRegisterListener(PlaySound);
        private void PlaySound(SoundEvent eve)
        {
            if (eve.isCancel)
            {
                AudioSource.clip = eve.UnitSound;
                AudioSource.Stop();
            }
            else
            {
                if (AudioSource.clip != eve.UnitSound || AudioSource.isPlaying == false)
                {
                    AudioSource.clip = eve.UnitSound;
                    AudioSource.PlayOneShot(eve.UnitSound);
                }
            }
        }
    }
}