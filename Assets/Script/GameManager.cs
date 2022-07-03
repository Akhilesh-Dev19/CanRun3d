using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool MoveByTouch,StartTheGame;
    private Vector3 _mouseStartPos, PlayerStartPos;
    [SerializeField] public float Roadspeed, SwipeSpeed,Distance;
    [SerializeField] private GameObject Road;
    public static GameManager GameManagerInstance;
    public Camera mainCam;
    public List<Transform> Cans = new List<Transform>();
    private Rigidbody rb;
    private Collider _collider;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        GameManagerInstance = this;
        mainCam = Camera.main;
        Cans.Add(gameObject.transform);
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartTheGame = MoveByTouch = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false;
        }

        if (MoveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);

            float distance;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray, out distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                Vector3 desirePos = mousePos - _mouseStartPos;
                Vector3 move = PlayerStartPos + desirePos;

                move.x = Mathf.Clamp(move.x, -2.2f, 2.2f);
               move.z = -7f;

                var player = transform.position;

                player = new Vector3(Mathf.Lerp(player.x, move.x, Time.deltaTime * SwipeSpeed), player.y, player.z);

                transform.position = player;

         
            }





        }

        if (StartTheGame)
            Road.transform.Translate(Vector3.forward * (Roadspeed * -1 * Time.deltaTime));

        if(Cans.Count > 1)
        {

            for (int i = 1; i <Cans.Count; i++)
            {
                var FirstCan = Cans.ElementAt(i - 1);
                var SectCan = Cans.ElementAt(i);

                var DesireDistance = Vector3.Distance(SectCan.position, FirstCan.position);

                if(DesireDistance <= Distance)
                {
                    SectCan.position = new Vector3(Mathf.Lerp(SectCan.position.x, FirstCan.position.x, SwipeSpeed * Time.deltaTime)
                        , SectCan.position.y, Mathf.Lerp(SectCan.position.z, FirstCan.position.z +0.5f, SwipeSpeed * Time.deltaTime));
                }
            }
        }
    }

    
 
    private void LateUpdate()
    {
        if (StartTheGame)
            mainCam.transform.position = new Vector3(Mathf.Lerp(mainCam.transform.position.x,transform.position.x, (SwipeSpeed - 5f) * Time.deltaTime),
                mainCam.transform.position.y, mainCam.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blue"))
        {
           // other.transform.parent=null;
            other.transform.parent = Player.transform;
            other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
            //other.tag = gameObject.tag;
            Cans.Add(other.transform);

            if(Player.GetComponent<Rigidbody>().isKinematic == false)
            {
              
            }

            
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("path"))
        {
            rb.isKinematic = false;
            _collider.isTrigger = false;

            rb.velocity = new Vector3(0f, 7f, 0f);
            // Roadspeed = Roadspeed * 2;


        }
    }

    private void OnCollisionEnter(Collision collision)
        
    {
        if (collision. collider.CompareTag("path"))
        {
            rb.isKinematic = _collider.isTrigger = true;
          Roadspeed = 15f;

           
        }
    }
}
