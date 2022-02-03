using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class AgentControl : MonoBehaviour
{
    [SerializeField]
        
    public AgentStats myStats = new AgentStats();
    
    public NavMeshAgent agent;

    private Collider foundFoodCollider = null;
    [SerializeField]
    //  public Transform closest = null;

    private Vector3 targetPosition;
    [SerializeField]

    private Vector3 homeLocation;
    

   


     void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();   
        homeLocation = this.gameObject.transform.position;
        updateAgentSpeed();
        //closest = this.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        starvationCheck();
        if (myStats.atHome == false){ 
             myStats.energy -= energyBurn() * Time.deltaTime;
             if (myStats.foodEaten >= 2)
            {
                goHome();

            }
            else
            {
                if (foundFoodCollider == null)
                {
                    findFood();
                }
                if ((foundFoodCollider == null && myStats.wandering == false) || this.gameObject.transform.position == targetPosition)
                {
                    Wander();
                }
            }

            goToTarget();

        }
    }
    float energyBurn()
    {
        if (myStats.atHome == false)
            return myStats.speed * myStats.speed + myStats.vision;
        else
            return 0;

    }

    public void updateAgentSpeed()
    {
        agent.speed = myStats.speed;
    }

    void starvationCheck()
    {
    if(myStats.energy <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void goHome()
    {
        targetPosition = homeLocation;

        if(this.transform.position.x == homeLocation.x && this.transform.position.z == homeLocation.z)
        {
            myStats.atHome = true;
        }
    }
    void findFood()
    {
        
       Collider[] foundfood = Physics.OverlapSphere(this.gameObject.transform.position, myStats.vision);
        foundfood = foundfood.OrderBy((objectToTest) => Vector3.Distance(this.transform.position,objectToTest.transform.position)).ToArray();
        foreach (Collider collider in foundfood)
        {
            if (collider.gameObject.CompareTag("Food"))
            {
                foundFoodCollider = collider;
                collider.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                Debug.Log("Food Found");

                break;
            }
        }
        if (foundFoodCollider != null){
        
            targetPosition = foundFoodCollider.transform.position;
            myStats.wandering = false;
        }


        

    }

    void Wander()
    {
        myStats.wandering = true;
        Vector3 wanderDirection = Random.insideUnitSphere * myStats.vision;

        wanderDirection += this.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(wanderDirection, out navHit, myStats.vision, -1);
        targetPosition = navHit.position;

    }

    void goToTarget()
    {
        agent.SetDestination(targetPosition);
    }
    


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {

            Destroy(other.gameObject, .5f);
            myStats.foodEaten++;


        }
    }



}

