using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : GetFullComponent
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;
    [field: SerializeField] public AudioSource EffectSources { private set; get; }
    [field: SerializeField] public AudioSource AudioMusic { private set; get; }


    [field: SerializeField] List<Sound> Sounds = new List<Sound>();
    [SerializeField]
    private float _limitDistances;
    [Header("Audio")]
    public AudioMixer AudioMix;
    public Transform playerTransform;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError($"Just only  exist {this.gameObject.name} !!!!!");
        }
        else
        {
            _instance = this;

        }

    }
    private void Start()
    {
        //AudioMix.SetFloat(CONSTANT.SoundChanel, Mathf.Log10(SettingMenuSO.IsMuteSound) * 20);
        //AudioMix.SetFloat(CONSTANT.MusicChanel, Mathf.Log10(SettingMenuSO.IsMuteMusic) * 20);
        //EnableMussiEffect(SettingMenuSO.IsMuteMusic < 0);
        //EnableSoundEffect(SettingMenuSO.IsMuteSound < 0);
        //PlayMusicSound(SoundName.BGM);
    }
    #region HandleMethod


    public virtual bool PlayEffectSoundByVolume(SoundName soundName, float volume)
    {
        AudioClip audio = GetAudioClip(soundName);
        if (EffectSources.clip == audio && EffectSources.isPlaying)
            return false;

        EffectSources.clip = audio;
        EffectSources.PlayOneShot(audio, volume);
        return true;
    }

    public virtual void PlaySoundEffectByAudioVsDistances(AudioClip audioClip, Transform currentTransform)
    {

        EffectSources.PlayOneShot(audioClip, GetVolume(currentTransform, playerTransform));
    }
    public virtual void PlayerEffectSoundByDistances(SoundName soundName, Transform currentTransform)
    {
        PlayEffectSoundByVolume(soundName, GetVolume(currentTransform, playerTransform));
    }

    public void PlaySoundEffectByDistances(SoundName soundName, Transform currentTransform = null)
    {
        AudioClip audio = GetAudioClip(soundName);
        EffectSources.PlayOneShot(audio, GetVolume(currentTransform, playerTransform));
    }
    public float GetVolume(Transform currentTransform, Transform targetTransform)
    {

        float distances = (targetTransform.position - currentTransform.position).magnitude;
        if (distances >= _limitDistances)
        {
            return 0;
        }
        if (distances <= 30f)
        {
            return 1;
        }

        return 1 / distances;
    }

    public virtual void PlayMusicSound(SoundName soundName)
    {
        AudioClip clip = GetAudioClip(soundName);
        AudioMusic.clip = clip;
        AudioMusic.loop = true;
        AudioMusic.Play();
    }
    private AudioClip GetAudioClip(SoundName soundName)
    {
        AudioClip audio = null;
        for (int i = 0; i < Sounds.Count; i++)
        {
            if (Sounds[i].SoundName == soundName)
            {
                audio = Sounds[i].Clip;
            }
        }
        return audio;
    }
    #endregion
    public void StopMusic()
    {
        AudioMusic.Stop();
    }
    [ContextMenu("Get compos")]
    protected override void GetAllCompos()
    {
        base.GetAllCompos();
        int[] arr = System.Enum.GetValues(typeof(SoundName)) as int[];
        for (int i = 0; i < arr.Length; i++)
        {
            SoundName s = (SoundName)arr[i];
            string path = "SFX/" + s.ToString();
            AudioClip clip = Resources.Load<AudioClip>(path);
            Sound sound = new Sound(s, clip);
            Sounds.Add(sound);
        }
    }
    private void Reset()
    {

    }

    public void EnableSoundEffect(bool isMute)
    {
        EffectSources.mute = isMute;

    }
    public void EnableMussiEffect(bool isMute)
    {
        AudioMusic.mute = isMute;
    }
}

[System.Serializable]
public class Sound
{
    public SoundName SoundName;
    public AudioClip Clip;
    public bool IsLoop;
    public Sound(SoundName soundName, AudioClip clip)
    {
        SoundName = soundName;
        Clip = clip;
    }
}

public enum SoundName
{
    Footstep = 0,
    AmmoOut = 1,
    Gun_Machine_Gun_353 = 2,
    Gun_Machine_Gun_434 = 3,
    Gun_Machine_Gun_444 = 4,
    Player_Gun = 5,
    Reloading = 6,

}
