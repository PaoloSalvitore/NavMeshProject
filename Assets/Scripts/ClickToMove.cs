using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{

    //This script/capsule is purely for you to roam in the world along with the other mindless AI's
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();
    [SerializeField] private float _speed = 20f;
    

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
       
    }

    void Update()
    {
        ClickMove(); 
    }

  

    void ClickMove()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                m_Agent.destination = m_HitInfo.point;
        }
    }

  
}
