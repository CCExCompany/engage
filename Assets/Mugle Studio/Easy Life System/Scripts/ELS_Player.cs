/////////////////////////////////////////////////////////////////////////////////////////////
//
//	ELS_Player.cs
//	© Artem Goldov (Mugle Studio). All Rights Reserved.
//	http://www.mugle.ru
//
//	Description: "Easy Life System" - the simplest solution to create and control game life.
//
/////////////////////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ELS_Player : MonoBehaviour
{
    public enum GUIType //GUI Types
    {
        GUIBar,
        OnlyText
    }

    public enum DeathType //If no more lifes
    {
        RestartTheLevel,
        Respawn
    }


    //Info
    [Header("Easy Life System v1.0", order = 1)]

    public GUIType GUILifeMode = GUIType.GUIBar;
    public DeathType IfNoMoreLifes = DeathType.RestartTheLevel;
    [Space(15)]

    [Header("Settings:", order = 2)]
    [Header("Max Life - 100", order = 2)]
    private float MaxLife = 100; // Don't change this value
    private float CurrentLifeGUI = 1; //Current life GUI progress
    
    [Header("GUI Bar:", order = 2)]
    public Texture2D Life_EmptyTexture = null; //Life Emty Texture
    public Texture2D Life_FullTexture = null; //Life Full Texture
    public Vector2 Life_GUI_Pos = new Vector2(50, 100);  //Life GUI Position on screen
    public Vector2 Life_GUI_Size = new Vector2(120, 22); //Life GUI Size on screen

    [Header("If selected only text:", order = 2)]
    public string LifeText = "Life: "; //Text

    [Header("Fire:", order = 1)]
    public float FireDamage = 5; //Fire Damage
    public AudioClip FireSound = null; // Fire Sound. (I recommend a loop sound)
    public float FireVolume = 1.0f; //Volume 0.0f - 1.0f

    [Header("Hit:", order = 1)]
    public float HitDamage = 5; //Hit Damage
    public AudioClip HitSound = null; //Hit Sound
    public float HitVolume = 1.0f; //Volume 0.0f - 1.0f

    [Header("First AID:", order = 1)]
    public float FirstAidValue = 35;
    public AudioClip FirstAidSound = null; //First AID Sound
    public float FirstAidVolume = 1.0f; //Volume 0.0f - 1.0f

    [Header("First AID Infinite:", order = 1)]
    public float FirstAidInfValue = 10;
    public AudioClip FirstAidInfSound = null; //First AID Infinite Sound (I recommend a loop sound)
    public float FirstAidInfVolume = 1.0f; //Volume 0.0f - 1.0f

    [Space(15)]
    private float Timer = 1; //Time delay for fire damage
    private float TimeSmoothControl = 1; //Fix for fire damage

    private Vector3 RespawnPos; //Re Spawn position
    private int Respawnfix = 0;

    AudioSource PlayerAudio;
    
    void Start()
    {
        PlayerAudio = GetComponent<AudioSource>();
        RespawnPos = transform.position;
    }


    void OnGUI()
    {

        switch (GUILifeMode)
        {
        case GUIType.GUIBar: 
         //Life GUI Bar
        GUI.BeginGroup(new Rect(Life_GUI_Pos.x, Life_GUI_Pos.y, Life_GUI_Size.x, Life_GUI_Size.y));
        GUI.Box(new Rect(0, 0, Life_GUI_Size.x, Life_GUI_Size.y), Life_EmptyTexture);
        GUI.BeginGroup(new Rect(0, 0, Life_GUI_Size.x * CurrentLifeGUI, Life_GUI_Size.y));
        GUI.Box(new Rect(0, 0, Life_GUI_Size.x, Life_GUI_Size.y), Life_FullTexture);
        GUI.EndGroup();
        GUI.EndGroup();
        //Life GUI Bar END
       break;

       case GUIType.OnlyText:
        //Life GUI - Only Text
        GUI.BeginGroup(new Rect(Life_GUI_Pos.x, Life_GUI_Pos.y, Life_GUI_Size.x, Life_GUI_Size.y));
        GUI.Box(new Rect(0, 0, Life_GUI_Size.x, Life_GUI_Size.y), LifeText + MaxLife.ToString());
        GUI.EndGroup();
        //Life GUI - Only Text END
        break;
        }

    }

    void Update()
    {
        //GUI Fix
        if (MaxLife == 100) { CurrentLifeGUI = 1.0f; }
        if (MaxLife >= 90 && MaxLife <= 99) { CurrentLifeGUI = 0.9f; }
        if (MaxLife >= 80 && MaxLife <= 90) { CurrentLifeGUI = 0.8f; }
        if (MaxLife >= 70 && MaxLife <= 80) { CurrentLifeGUI = 0.7f; }
        if (MaxLife >= 60 && MaxLife <= 70) { CurrentLifeGUI = 0.6f; }
        if (MaxLife >= 50 && MaxLife <= 60) { CurrentLifeGUI = 0.5f; }
        if (MaxLife >= 40 && MaxLife <= 50) { CurrentLifeGUI = 0.4f; }
        if (MaxLife >= 30 && MaxLife <= 40) { CurrentLifeGUI = 0.3f; }
        if (MaxLife >= 20 && MaxLife <= 30) { CurrentLifeGUI = 0.2f; }
        if (MaxLife >= 10 && MaxLife <= 20) { CurrentLifeGUI = 0.1f; }
        if (MaxLife >= 0  && MaxLife <= 10) { CurrentLifeGUI = 0.1f; }
        if (MaxLife == 0 ) { CurrentLifeGUI = 0.0f;}

        //Fix for MaxLife
        if (MaxLife < 0) { MaxLife = 0; }
        if (MaxLife > 100) { MaxLife = 100; }

        //If no more lifes
        switch (IfNoMoreLifes)
        {
            case DeathType.RestartTheLevel:
                //Restart the level
                if (MaxLife <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

                //Restart the level END
                break;

            case DeathType.Respawn:
                //Re spawn
                if (MaxLife <= 0)
                {
                    Respawnfix = 1;
                    transform.position = RespawnPos;
                    MaxLife = 100;                    
                }
                //Re spawn END
                break;
        }
    }
    
    IEnumerator Fire_Control() // Fire damage
    {
        yield return new WaitForSeconds(Timer);
        if (TimeSmoothControl >= 1 && TimeSmoothControl <= 2)
        {
            if (FireDamage >= MaxLife && Respawnfix == 0)
            {
                MaxLife -= MaxLife;
            }
            else
            {
                if (Respawnfix == 0)
                {
                    MaxLife -= FireDamage;
                }
            }
        }
        TimeSmoothControl = Random.Range(0.0f, 20.0f);
    }

    public void Fire_Sound() // Fire damage sound
    {
        if (PlayerAudio.isPlaying == false)
        {
            PlayerAudio.PlayOneShot(FireSound, FireVolume);
        }
    }

    public void Hit() // Hit damage
    {
        MaxLife -= HitDamage;
        PlayerAudio.PlayOneShot(HitSound, HitVolume);
    }

    public void FirstAID() // Health ++
    {
        MaxLife += FirstAidValue;
        PlayerAudio.PlayOneShot(FirstAidSound, FirstAidVolume);
    }

    public void FirstAIDInf() // Health infinity ++
    {
        MaxLife += FirstAidInfValue;

        if (PlayerAudio.isPlaying == false)
        { 
        PlayerAudio.PlayOneShot(FirstAidInfSound, FirstAidInfVolume);
        }
    }

    void OnTriggerStay(Collider PlayerCollision) // Collision by tag
    {
        if (PlayerCollision.tag == "Fire" && MaxLife > 0)
        {
            StartCoroutine(Fire_Control());
            Fire_Sound();
            Respawnfix = 0;
        }

        if (PlayerCollision.tag == "Hit")
        {
            Hit();
            Destroy(PlayerCollision.gameObject);
        }

        if (PlayerCollision.tag == "FirstAID")
        {
            FirstAID();
            Destroy(PlayerCollision.gameObject);
        }
        
        if (PlayerCollision.tag == "FirstAIDInf")
        {
            FirstAIDInf();            
        }
    }

    void OnTriggerExit(Collider PlayerCollision) // Fix for Fire and FirstAIDInf
    {
        if (PlayerCollision.tag == "Fire" || PlayerCollision.tag == "FirstAIDInf")
        {
            PlayerAudio.Stop();
            Respawnfix = 1;
        }
    }
}
