using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_button : MonoBehaviour
{
    private PokeInteractable _elevatorbutton;
    public Animator _animator;
    public int button_id = 1; // floor
  
    // Start is called before the first frame update
    void Start()
    {
        _elevatorbutton = GetComponent<PokeInteractable>();
        _elevatorbutton.WhenStateChanged += WhenButtonStateChanged;
    }
    private void WhenButtonStateChanged(InteractableStateChangeArgs obj)
    {
        // Debug.Log("################### change state: " + obj.NewState + ", id: " + button_id); // InteractableState

        if (obj.NewState == InteractableState.Select)
        {
            // Change floor
            Debug.Log("################### Selected" + ", id: " + button_id);
            if(!GameDataManager.instance.is_open) Elevator_switch_passthrough.instance.switch_to_virtualworld(button_id);  // switch to real world
        }
    }
}
