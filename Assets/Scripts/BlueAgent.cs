using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class BlueAgent : MonoBehaviour
{
  
    #region Waypoint finder
    public int objective = 0; //used to determine which waypoint we are heading for
    public GameObject objective0; //game object of the first objective we will pathfind to
    public GameObject objective1; //game object of the second objective we will pathfind to
    public GameObject objectiveFinish; //game object of the last objective we will pathfind to before reseting
    #endregion

    #region NavMesh Variables
    NavMeshAgent m_Agent;
    [SerializeField] private float _speed = 8f; //the default speed
    [SerializeField] private float _grassSpeed = 6f; //speed while on green platform
    [SerializeField] private float _redSpeed = 8f; //speed while on red platform
    [SerializeField] private float _blueSpeed = 12f; //speed while on blue platform
    #endregion


    #region Animator
    [SerializeField] private float _animationStop = 0.01f;
    [SerializeField] private Animator _anim;
    #endregion



    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>(); //tells unity who is our agent (script is attached to them)
        GetComponentInChildren<Animator>(); //Tells unity to grab the attached animator and we will control it through this script
    }

    void Update()
    {
        TargetLocation(); //runs the target location method which will use switch cases to determine where the AI is going
        ChangeAreaSpeed(); //runs the area speed method which will change how fast we are going depending on the platform.
        UpdateAnimator(); //controls the animator
    }

    private void UpdateAnimator()
    {
        if (m_Agent.velocity.magnitude < _animationStop)
        {
            _anim.SetBool("isWalking", false);
        }

        else
        {
            _anim.SetBool("isWalking", true);
        }

    }



    void ChangeAreaSpeed()
    {
        NavMeshHit navHit; //checks what the navmesh is currently hitting

        m_Agent.SamplePathPosition(-1, 0.0f, out navHit);

        int GrassMask = 1 << NavMesh.GetAreaFromName("Tall Grass"); //declares variables for the 3 types of Navmesh
        int RedMask = 1 << NavMesh.GetAreaFromName("Red");
        int BlueMask = 1 << NavMesh.GetAreaFromName("Blue");

        if (navHit.mask == GrassMask) //checks to see if we are on any of these masks, if so change our speed to the approriate speed
        {
            m_Agent.speed = _grassSpeed;
            Debug.Log("Green speed");
        }

        else if (navHit.mask == RedMask)
        {
            m_Agent.speed = _redSpeed;
            Debug.Log("Red speed");
        }

        else if (navHit.mask == BlueMask)
        {
            m_Agent.speed = _blueSpeed;
            Debug.Log("Blue Speed");
        }


        else
        {
            m_Agent.speed = _speed;
        }

    }

    void TargetLocation()
    {
        //uses switch case it looks what is the value of objective preform the task
        switch (objective)
        {
            case 0:

                m_Agent.SetDestination(objective0.transform.position); //sets destination to the 0 objective
                if (Vector3.Distance(objective0.transform.position, m_Agent.transform.position) < 0.1f) //if we are at the same locationg then move on to the next objective
                {

                    objective = 1;


                    break;
                }
                break;

            case 1:

                m_Agent.SetDestination(objective1.transform.position);
                if (m_Agent.transform.position == objective1.transform.position)
                {
                    objective = 2;
                }
                break;

            case 2:

                m_Agent.SetDestination(objectiveFinish.transform.position);
                if (m_Agent.transform.position == objectiveFinish.transform.position)
                {
                    objective = 0;
                }

                break;


        }



    }



}
