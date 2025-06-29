using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    public GameObject ventousePrefab;
    //public GameObject cable;
    public Transform ventouseSpawn;
    public float ventouseVelocity = 30f;
    //private void FireVentouse()
    //{
    //    GameObject ventouse = Instantiate(ventousePrefab, ventouseSpawn);
    //}
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private float minDistance = 1f;
    private SpringJoint joint;

    private Rigidbody hitObject;
    
    private Weapon weapon;

    void Awake() {
        if (weapon == null)
        {
            weapon = GetComponent<Weapon>();
        }
    }

    void Update()
    {
        
        if (weapon.pickedUp == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGrapple();
            }

            if (hitObject != null)
            {

                hitObject.GetComponent<Collider>().isTrigger = true;
                if (Vector3.Distance(transform.position, hitObject.position) >= minDistance)
                {
                    hitObject.linearVelocity = (transform.position - hitObject.position) * 5;  
                }
                else
                {
                    hitObject.GetComponent<Collider>().isTrigger = false;
                    StopGrapple();
                }
            }
        }

            
        
            
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        
        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            Debug.Log(hit.transform.name);
            grapplePoint = hit.point;

            hitObject = hit.collider.GetComponent<Rigidbody>();
            
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        /*lr.positionCount = 0;
        Destroy(joint);*/
        hitObject.linearVelocity = Vector3.zero;
        hitObject = null;
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
