using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FadeControl : MonoBehaviour
{
    #region Singleton 
    public static FadeControl instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of FadeControl found!");
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

    public FadeScene[] fadeScene;
    private FadeScene _fadeScene;
    public int pre_button_id = 0;
    public Elevator_change_skybox change_skybox;
    // Start is called before the first frame update
    void Start()
    {

        _fadeScene = fadeScene[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _fadeScene.Fade(true); // 按下 F 鍵觸發淡出
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
           _fadeScene.Fade(false); // 按下 G 鍵觸發淡入
        }
    }
    public void FadeScene(int button_id)
    {
        serSceneIndex(button_id);
        Debug.Log("@@@@@@@@@###########button_id: " + button_id);
        _fadeScene.Fade(false);
        if (button_id == 6)
        {
            change_skybox.switch_to_f6();
        }
        pre_button_id = button_id;
    }
    public void serSceneIndex(int button_id)
    {
        _fadeScene = fadeScene[button_id];
    }
    public void RecoverFadeScene()
    {
        _fadeScene = fadeScene[pre_button_id];
        _fadeScene.Fade(true);
        // _fadeScene.RecoverMaterial();
    }

}
