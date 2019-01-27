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

    [SerializeField] GameObject deathExplosionPrefab;
    public AudioClip[] BirbSounds;
    public AudioClip[] BirbDeathSounds; 
    private bool invunerable = false;

    private float quackTime;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        quackTime = Random.Range(1, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimationSpeedUpdate();

        rigid.AddForce(-transform.forward * movementSpeed);

        if (Time.time > quackTime)
        {
            AudioManager.instance.PlaySingle(BirbSounds[Random.Range(0, BirbSounds.Length)]);
            quackTime = Time.time + Random.Range(1, 10);
        }
    }

    void AnimationSpeedUpdate()
    {
        if (manager != null)
        {
            if (gameObject != manager.leader)
            {
                transform.rotation = manager.leader.transform.rotation;

            }
        }       

        float speed = rigid.velocity.magnitude / 10.0f;

        if (speed > 1)
        {
            speed *= 5.0f;
        }

        float animSpeed = speed;
        anim.speed = animSpeed;
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (!invunerable)
        {
            if (col.transform.GetComponent<Wind>())
            {
                manager.EnableBoost();
                Destroy(col.gameObject);
                return;
            }

            if (!col.transform.GetComponent<FlockMember>())
            {
                AudioManager.instance.PlaySingle(BirbDeathSounds[1]);
                AudioManager.instance.PlaySingle(BirbSounds[Random.Range(0, BirbSounds.Length)]);
                AudioManager.instance.PlaySingle(BirbDeathSounds[0]);

                Instantiate(deathExplosionPrefab, transform.position, transform.rotation);

                BirdController controller = GetComponent<BirdController>();

                if (controller.enabled)
                {
                    controller.enabled = false;
                    manager.UpdateLeader();
                }

                if (manager)
                {
                    manager.RemoveFlockMember(this.gameObject);
                }

                Destroy(this.gameObject);            
            }
            else
            {
                FlockMember brd = col.transform.GetComponent<FlockMember>();
                if (!brd.GetManager())
                {
                    brd.AddToFlock(manager);
                }
            }
        }
    }
    public void SetManager(FlockManager _manager)
    {
        manager = _manager;
    }
    public FlockManager GetManager()
    {
        return manager;
    }
    public void SetIsLeader(bool _ldr)
    {
        isLeader = _ldr;
    }
    public float GetSpeed()
    {
        return movementSpeed;
    }
    public void SetSpeed(float _spd)
    {
        movementSpeed = _spd;
    }
    public void AddToFlock(FlockManager _manager)
    {
        manager = _manager;
        manager.AddNewFlockMember(this.gameObject);

        if (transform.Find("Sparkles"))
        {
            Destroy(transform.Find("Sparkles").gameObject);
        }
    }
    public void SetInvunerable(bool _in)
    {
        invunerable = _in;
    }
}
