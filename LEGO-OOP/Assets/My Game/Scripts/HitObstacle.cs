using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.LEGO.Minifig;
using Unity.VisualScripting;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            transform.position = new Vector3(-41, 41, 1204);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WALL")
        {
            GetComponent<WALKAUTOWOW>().enabled = false;
            GetComponent<MinifigController>().enabled = true;
            this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
            camera.GetComponent<CinemachineFreeLook>().m_StandbyUpdate = CinemachineVirtualCameraBase.StandbyUpdateMode.RoundRobin;
            camera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
            camera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
        }
    }
}
