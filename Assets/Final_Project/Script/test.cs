using Meta.XR.EnvironmentDepth;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        // OVRScene.RequestSpaceSetup();
        // Debug.Log("################# EnvironmentDepthManager" + g.GetComponent<EnvironmentDepthManager>().MaskBias);
        g.GetComponent<PokeInteractable>().WhenStateChanged += Test_WhenStateChanged;
    }

    private void Test_WhenStateChanged(InteractableStateChangeArgs obj)
    {
        Debug.Log("################### change state: "+ obj.NewState); // InteractableState
        
        // if(obj.NewState == InteractableState.Normal) Debug.Log("################### Normal");
    }

    // Update is called once per frame
    void Update()
    {
        /*foreach (var item in g.GetComponent<PokeInteractable>().Interactors)
        {
            Debug.Log("################# item.State: " + item.State);  // 可以感測到按下去的 item
        }*/
    }
}
