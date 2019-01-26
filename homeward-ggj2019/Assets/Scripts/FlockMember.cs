using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMember : MonoBehaviour
{
    [SerializeField] FlockManager manager;
    private Animator anim;
    private Rigidbody rigid;

    [SerializeField] bool isLeader = false;
    [SerializeField] float movementSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        AnimationSpeedUpdate();

        rigid.AddForce(-transform.forward * movementSpeed);
    }

    void AnimationSpeedUpdate()
    {
        float speed = rigid.velocity.magnitude / 10.0f;

        if (speed > 1.1f)
        {
            speed *= 5.0f;
        }

        float animSpeed = speed;
        anim.speed = animSpeed;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!col.transform.GetComponent<FlockMember>())
        {
            BirbController controller = GetComponent<BirbController>();

            if (controller.enabled)
            {
                controller.enabled = false;
                //manager.UpdateLeader();
            }

            manager.RemoveFlockMember(this.gameObject);            

            rigid.useGravity = true;
            Destroy(this.gameObject, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLeader)
        {
            //todo: collision layer
            Debug.Log(other.gameObject.name);
            var comp = other.GetComponent<FoliageSpawner>();
            if (comp)
            {
                comp.DestroyTile(transform);
            }
        }
    }

    public void SetManager(FlockManager _manager)
    {
        manager = _manager;
    }

    public void SetIsLeader(bool _ldr)
    {
        isLeader = _ldr;
    }
}
