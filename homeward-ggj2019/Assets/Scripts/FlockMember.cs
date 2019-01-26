using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMember : MonoBehaviour
{
    [SerializeField] FlockManager manager;
    private Animator anim;
    private Rigidbody rigid;

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
        //if (rigid.velocity.magnitude > 15.0f)
        {
            float speed = rigid.velocity.magnitude / 10.0f;
            
            if (speed > 1.1f)
            {
                speed *= 5.0f;
            }

            float animSpeed = speed;
            //float animSpeed = 1.0f;
            //animSpeed += (rigid.velocity.magnitude * rigid.velocity.magnitude) * 0.05f;
            anim.speed = animSpeed;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!col.transform.GetComponent<FlockMember>())
        {
            manager.RemoveFlockMember(this.gameObject);
            rigid.useGravity = true;
            Destroy(this.gameObject, 5);
        }
    }

    public void SetManager(FlockManager _manager)
    {
        manager = _manager;
    }
}
