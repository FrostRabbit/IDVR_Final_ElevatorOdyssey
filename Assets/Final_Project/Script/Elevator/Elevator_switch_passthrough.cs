using Meta.XR.MRUtilityKit;
using Meta.XR.MRUtilityKitSamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_switch_passthrough : MonoBehaviour
{
    /// <summary>
    /// modify version of KeyboardManager.cs
    /// </summary>

    #region Singleton 
    public static Elevator_switch_passthrough instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Elevator_switch_passthrough found!");
            // Destroy(gameObject);
            return;
        }
        else
        {
            // DontDestroyOnLoad(gameObject);
            instance = this;
        }
        switch_to_realworld();
    }

    #endregion
    [SerializeField]
    GameObject _prefab;
    public Animator floor_animator;
    public ParticleSystem particaleffect;
    [SerializeField]
    OVRPassthroughLayer _passthroughLayer;

    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.Log($"Detected new {trackable.TrackableType} with {trackable.name}");

        if (trackable.TrackableType != OVRAnchor.TrackableType.Keyboard)
        {
            // We only care about keyboards
            return;
        }

        // Instantiate the prefab
        var newGameObject = Instantiate(_prefab, trackable.transform);

        // Hook everything up
        var boundaryVisualizer = newGameObject.GetComponentInChildren<Bounded3DVisualizer>();
        if (boundaryVisualizer)
        {
            boundaryVisualizer.Initialize(_passthroughLayer, trackable);
        }
    }

    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        Debug.Log($"Removing GameObject '{trackable.name}'");
        Destroy(trackable.gameObject);
    }

    void Update()
    {
        if (_passthroughLayer && OVRInput.GetDown(OVRInput.RawButton.A)) // OVRInput.GetDown(OVRInput.RawButton.A)
        {
            _passthroughLayer.enabled = false;

            switch (_passthroughLayer.projectionSurfaceType)
            {
                case OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed:
                    {
                        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;
                        _passthroughLayer.overlayType = OVROverlay.OverlayType.Overlay;
                        Camera.main.clearFlags = CameraClearFlags.Skybox;
                        break;
                    }
                case OVRPassthroughLayer.ProjectionSurfaceType.UserDefined:
                    {
                        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed;
                        _passthroughLayer.overlayType = OVROverlay.OverlayType.Underlay;
                        Camera.main.clearFlags = CameraClearFlags.SolidColor;
                        break;
                    }
            }

            _passthroughLayer.enabled = true;
        }
    }

    public void switch_to_realworld()
    {
        // floor_animator.SetInteger("floor", -1);
        StartCoroutine(SwapSceneAfterTimes(3f, -1));
        // Toggle between full passthrough and surface-projected passthrough
        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed;
        _passthroughLayer.overlayType = OVROverlay.OverlayType.Underlay;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        FadeControl.instance.RecoverFadeScene();
        particaleffect.Stop();
        
        
    }

    public void switch_to_virtualworld(int button_id)
    {
        
        // Toggle between full passthrough and surface-projected passthrough
        /*set_sky_box();*/
        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed;
        _passthroughLayer.overlayType = OVROverlay.OverlayType.Underlay;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        FadeControl.instance.FadeScene(button_id);
        particaleffect.Play();
        floor_animator.SetInteger("floor", button_id);
        
    }

    public void set_sky_box()
    {
        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;
        _passthroughLayer.overlayType = OVROverlay.OverlayType.Overlay;
        Camera.main.clearFlags = CameraClearFlags.Skybox;
    }

    private IEnumerator SwapSceneAfterTimes(float times, int button_id)
    {
        yield return new WaitForSeconds(times);
        floor_animator.SetInteger("floor", button_id);
    }
}
