using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    public float rate = 1;
    public int maximum = 0;
    public int group = 1;
    int total = 0;
    public GameObject foodObj;
    // Start is called before the first frame update
    public void Start()
    {
     
            for(int i = 0; i < group; i++)
            {
                GameObject instance = Instantiate(foodObj);
                instance.transform.position = new Vector3(Random.Range(-40, 40), 0.5f, Random.Range(-40, 40));
                total++;
            }
       
    }
}

   