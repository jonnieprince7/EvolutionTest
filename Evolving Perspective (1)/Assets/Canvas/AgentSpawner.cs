
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public int critterCount = 1;
    public int dayTime = 100;
    public GameObject foodObj;
    public int foodcount;
    public float cycleStart = 0;
    public GameObject critter;
    int edge;


    // Start is called before the first frame update

    public void spawnFood(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject instance = Instantiate(foodObj);
            instance.transform.position = new Vector3(Random.Range(-40, 40), 0.5f, Random.Range(-40, 40));
        }
    }




    void Start()
    {
        spawnFood(foodcount);
        for(int i = 0; i < critterCount; i++)
        {
            spawn();

        }
    
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Data.time = dayTime - (Time.time - cycleStart);
    }

    //spawn everything function
    public void spawn()
    {
        GameObject newAgent = Instantiate(critter);
        edge = Random.Range(0, 4);
        if(edge == 0)
            newAgent.transform.position = new Vector3(40, 1f, Random.Range(-40, 40));
        else if (edge == 1)
            newAgent.transform.position = new Vector3(-40, 1f, Random.Range(-40, 40));
        else if (edge == 2)   
            newAgent.transform.position = new Vector3(Random.Range(-40, 40), 1f, 40);  
        else
            newAgent.transform.position = new Vector3(Random.Range(40, -40), 1f, -40);
      
    }


}
