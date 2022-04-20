using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrail : MonoBehaviour
{
    public int Gap = 10;
    public float TrailSpeed = 5;

    private Rigidbody rb;
    public GameObject TrailPrefab;

    public List<GameObject> TrailList = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    
    // Update is called once per frame
    void Update()
    {
        PositionsHistory.Insert(0, this.transform.position);
        
        int index = 0;
        if (rb.velocity.magnitude > 0) 
        {
            foreach (var trail in TrailList)
            {
            
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - trail.transform.position;
            trail.transform.position += moveDirection * TrailSpeed * Time.deltaTime;
            trail.transform.LookAt(point);
            index++;
            }
            
        }
    }

   void Grow()
   {
      GameObject trail = Instantiate(TrailPrefab, transform.position + (transform.forward*2), transform.rotation);
      TrailList.Add(trail);
   }

   private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp_Food"))
        {
            other.gameObject.SetActive(false);
            Grow();
            //count = count + 1;

            //SetCountText();
        }
    }
}
