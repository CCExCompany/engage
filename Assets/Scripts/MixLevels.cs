using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

    public AudioMixer MasterMixer;

    public void SetSfxLvl(float sfxLvl)
    {
        MasterMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLvl (float musicLvl)
    {
        MasterMixer.SetFloat("MusicVol", musicLvl);
    }

    public void SetMasterLvl(float masterLvl)
    {
        MasterMixer.SetFloat("MasterVol", masterLvl);
    }
}
