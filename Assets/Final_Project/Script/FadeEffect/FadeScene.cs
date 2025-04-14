using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScene : MonoBehaviour
{
 [SerializeField]
    private float _fadeDuration = 1.0f; // 淡化持續時間
    //public Material _material; // 材質
    private Material[] originalMaterial;  // 原始材質

    private bool _isFadingOut = false; // 是否正在淡出

    public Material[] material; // 材質

    public float alphaUp = 0.8f;

    public float alphaDown = 0.0f;

    public void Awake()
    {
        
        originalMaterial = new Material[material.Length];
        for (int i = 0; i < material.Length; i++)
        {
            originalMaterial[i] = new Material(material[i]);
        }

        SetTransparentMode();
        for (int i = 0; i < material.Length; i++) {
            Color color = material[i].color;
            color.a = 0;
            material[i].color = color;
        }
        //Fade(false);
        
        /*for (int i = 0; i < material.Length; i++)
        {
            Color color = material[i].color;
            color.a = 0;
            material[i].color = color;
        }
        SetOpaqueMode();*/


    }
    private void Start()
    {


    }
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F))
        // {
           
        //     GetComponent<FadeScene>().Fade(true); // 按下 F 鍵觸發淡出
        // }
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     GetComponent<FadeScene>().Fade(false); // 按下 G 鍵觸發淡入
            
        // }
    }
    void OnEnable()
    {
        
        Fade(false);
        Debug.Log("@@@@@@@@@@@@@############ + name: " + transform.name);
        // Debug.Log("@@@@@@@@@@@@@@@@@@@@@@ P rintOnEnable: script was enabled");
    }
    private void OnDisable()
    {
        // 恢復共享材質的原始屬性
        for (int i = 0; i < material.Length; i++)
        {
            material[i].CopyPropertiesFromMaterial(originalMaterial[i]);
        }
    }

    public void Fade(bool fadeOut)
    {
        if(fadeOut == false)
        {
            for (int i = 0; i < material.Length; i++)
            {
                Color color = material[i].color;
                color.a = 0;
                material[i].color = color;
            }
        }
        SetTransparentMode();
        if (fadeOut && _isFadingOut)
            return;
        if (!fadeOut && !_isFadingOut)
            return;
        _isFadingOut = fadeOut;

        StopAllCoroutines();
        string val = _isFadingOut ? "OUT" : "IN";
        Debug.Log($"Starting fade {val} coroutine");
        StartCoroutine(PlayEffect(fadeOut));
    }

    public void RecoverMaterial()
    {
        for (int i = 0; i < material.Length; i++)
        {
            material[i].CopyPropertiesFromMaterial(originalMaterial[i]);
        }
    }

    private IEnumerator PlayEffect(bool fadeOut)
    {
        float[] startAlphas = new float[material.Length];
        float endAlpha = fadeOut ? alphaDown : alphaUp; // 淡出至透明，淡入至不透明

        for (int i = 0; i < material.Length; i++)
        {
            startAlphas[i] = material[i].color.a; // 獲取當前透明度
        }

        float elapsedTime = 0;
        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            for (int i = 0; i < material.Length; i++)
            {
                Color color = material[i].color;
                color.a = Mathf.Lerp(startAlphas[i], endAlpha, elapsedTime / _fadeDuration); // 平滑過渡 alpha
                material[i].color = color; // 設置材質顏色
            }
            yield return null; // 等待下一幀
        }

        // 確保最終透明度準確
        for (int i = 0; i < material.Length; i++)
        {
            Color color = material[i].color;
            color.a = endAlpha;
            material[i].color = color;
        }

        if (endAlpha == 1.0f)
        {
            SetOpaqueMode();
            Elevator_switch_passthrough.instance.set_sky_box();
        }


        // if (startColor.a == 0.0f){
        //     // disable the object
        //     gameObject.SetActive(false);
        // }
    }

    private void SetTransparentMode()
    {
        foreach (var mat in material)
        {
            mat.SetFloat("_Mode", 3); // 設置為 Transparent 模式
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0); // 禁用深度寫入
            mat.renderQueue = 3000; // 設置渲染隊列為透明隊列
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
    }

    private void SetOpaqueMode()
    {
        foreach (var mat in material)
        {
            mat.SetFloat("_Mode", 0); // 設置為 Opaque 模式
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1); // 啟用深度寫入
            mat.renderQueue = -1; // 重置為默認渲染隊列
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
    }
}
