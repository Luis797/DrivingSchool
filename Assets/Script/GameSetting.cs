using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSetting : ScriptableObject
{
   
    [SerializeField]
    private float MusicLevel = 0.0f;
    [SerializeField]

    private float SoundFxLevel = 0.0f;

    ///<summary>
    ///Set the music level as the value provided
    ///</summary>
    public void SetMusicLevel(float Lvl)
    {
        MusicLevel = Lvl;
    }
   
    
    ///<summary>
    ///Get the current music level 
    ///</summary>
    public float GetMusicLevel()
    {
        return MusicLevel;
    }
    
     ///<summary>
    ///Set the SFX level as the value provided
    ///</summary>
    public void SetSoundFxLevel(float Lvl)
    {
        SoundFxLevel = Lvl;
    }

     ///<summary>
    ///Get the current SFX level 
    ///</summary>
    public float GetSoundFxLevel()
    {
        return SoundFxLevel;
    }

   
}
