using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_detect_open_close : MonoBehaviour
{
    public Transform closet_point;  // from ray cast
    public float min_distance = 1;
    private Transform cam;
    private float now_distance;
    private bool is_open = false;
    private GameDataManager gdm;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        gdm = GameDataManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        now_distance = (cam.position - closet_point.position).magnitude;
        if(!gdm.is_open && now_distance > min_distance)
        {
            gdm.is_open = true;
            Elevator_switch_passthrough.instance.switch_to_realworld();
            Debug.Log("################ door open, now_distance: " + now_distance);
        }
        if(gdm.is_open && now_distance < min_distance)
        {
            gdm.is_open = false;
            Debug.Log("################ door close, now_distance: " + now_distance);
        }
    }
}
