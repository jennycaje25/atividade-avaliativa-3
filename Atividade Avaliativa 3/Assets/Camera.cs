using UnityEngine;

public class Camera : MonoBehaviour
{ 
   public GameObject player;

   public GameObject box;
   static BoxCollider2D boxZone;
    
   
    
    void Start()
    {
     boxZone = box.GetComponent<BoxCollider2D>();
     player = GameObject.FindGameObjectWithTag("Player");    
     
    }

    void Update()
    {

 
    Vector2 center = boxZone.transform.TransformPoint(boxZone.offset);

        if ( 
               UnityEngine.Camera.main.transform.position.x >  (center.x - (boxZone.size.x / 2))
               &&
               UnityEngine.Camera.main.transform.position.x <  (center.x + (boxZone.size.x / 2))
            )
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, 
                player.transform.position.y,
                gameObject.transform.position.z);

            float halfHeight =   UnityEngine.Camera.main.orthographicSize;
            float halfWidth =   UnityEngine.Camera.main.aspect * halfHeight;
            
            if (player.transform.position.x - halfWidth < (center.x - boxZone.size.x / 2))
            {
                transform.position = new Vector3( center.x - boxZone.size.x / 2 + halfWidth , transform.position.y, transform.position.z);
            } 
            
            if (player.transform.position.x + halfWidth > (center.x + boxZone.size.x / 2))
            {
                transform.position = new Vector3( center.x + boxZone.size.x / 2 - halfWidth, transform.position.y, transform.position.z);
            }
            
            
            if (player.transform.position.y - halfHeight < (center.y - boxZone.size.y / 2))
            {
                transform.position = new Vector3(  transform.position.x, center.y - boxZone.size.y / 2 + halfHeight, transform.position.z);
            } 
            
            if (player.transform.position.y + halfHeight > (center.y + boxZone.size.y / 2))
            {
                transform.position = new Vector3(  transform.position.x, center.y + boxZone.size.y / 2 - halfHeight, transform.position.z);
            }
        }
        
    }
}