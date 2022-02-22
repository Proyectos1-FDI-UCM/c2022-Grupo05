using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private BoxCollider2D cameraBox;
    private Transform player;
    private Transform _myTransform;
    // Start is called before the first frame update
    void Start()
    {
        cameraBox = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

  void FollowPlayer ()
    {
        if (GameObject.Find("Boundary"))
        {
           
            _myTransform.position =

            Vector3.Lerp(_myTransform.position, new Vector3(Mathf.Clamp(player.position.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                               Mathf.Clamp(player.position.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2), _myTransform.position.z), 3 * Time.deltaTime); 
          
        }
    }
}
