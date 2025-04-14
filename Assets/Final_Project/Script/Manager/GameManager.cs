using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton 
    public static GameManager instance;
    public int button_id = 0; // floor
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found!");
            // Destroy(gameObject);
            return;
        }
        else
        {
            // DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("WWWWW");
            if (!GameDataManager.instance.is_open)
            {
                Elevator_switch_passthrough.instance.switch_to_virtualworld(button_id);
                button_id += 1;
                button_id = button_id % 7;
            }
           
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            GameDataManager.instance.is_open = !GameDataManager.instance.is_open;
            if (GameDataManager.instance.is_open)
            {
                Elevator_switch_passthrough.instance.switch_to_realworld();
            }
        }
    }
}
