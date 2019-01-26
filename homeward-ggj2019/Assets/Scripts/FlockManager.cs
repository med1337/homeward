﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour    
{
    [Header("References")]
    [SerializeField] public GameObject leader;
    [SerializeField] List<GameObject> flockMembers;
    [SerializeField] Camera main_cam;

    [Header("Parameters")]
    [SerializeField] float avoidanceStrength = 1.0f;
    [SerializeField] float avoidanceDistance = 2.0f;

    [SerializeField] float seekStrength = 1.5f;

    [SerializeField] int flockMaxSize = 20;

    [SerializeField] List<Vector3> positionOffsets;

    [SerializeField] float distanceDelayStrength = 50.0f;

    [SerializeField] float flyingHeight = 10.0f;

    private float delayTimer = 0.0f;
    private float delayTimerMax = 3.0f;
    private bool delayOn = false;

    public static FlockManager Instance = null;


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)

            //if not, set instance to this
            Instance = this;

        //If instance already exists and it's not this:
        else if (Instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

       
    }

    // Start is called before the first frame update
    void Start()
    {
        SetOffsetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        Avoid();

        if (delayOn)
        {
            if (delayTimer < delayTimerMax)
            {
                delayTimer += Time.deltaTime;
            }
            else
            {
                delayTimer = 0.0f;
                delayOn = false;
            }
        }
        else
        {
            Seek();
        }
    }

    void SetOffsetPositions()
    {
        float xOffset = avoidanceDistance;
        float zOffset = avoidanceDistance;

        for (int i = 0; i < flockMaxSize; i++)
        {
            Vector3 offset = leader.transform.position;

            offset.x += xOffset;
            offset.z += zOffset;

            offset.y = flyingHeight;

            if (!(i % 2 == 0))
            {
                offset.x *= -1;
                zOffset += avoidanceDistance;
                xOffset += avoidanceDistance;
            }
            positionOffsets.Add(offset);
        }
    }

    void Avoid()
    {
        foreach(GameObject birds in flockMembers)
        {
            foreach (GameObject bird in flockMembers)
            {
                if (bird != birds)
                {
                    if (Vector3.Distance(birds.transform.position, bird.transform.position) < avoidanceDistance)
                    {
                        Vector3 targetPos = birds.transform.position - bird.transform.position;
                        targetPos.Normalize();
                        birds.GetComponent<Rigidbody>().AddForce(targetPos * avoidanceStrength);
                    }
                }
            }
        }
    }

    void Seek()
    {
        if (leader)
        {
            UpdateOffsetPositions();

            for (int j = 0; j < flockMembers.Count; j++)
            {
                if (Vector3.Distance(flockMembers[j].transform.position, positionOffsets[j]) > 0.5f)
                {
                    Vector3 targetPos = positionOffsets[j] - flockMembers[j].transform.position;

                    float speed = seekStrength;

                    speed *= Random.Range(0.2f, 1.8f);

                    speed *= ((distanceDelayStrength / Vector3.Distance(flockMembers[j].transform.position, leader.transform.position)) / 4);

                    flockMembers[j].GetComponent<Rigidbody>().AddForce(targetPos * speed);
                }
            }

            
            Vector3 leaderTargetPos = leader.transform.position;
            leaderTargetPos.y = flyingHeight;

            if (!((leader.transform.position.y < flyingHeight + 5) && (leader.transform.position.y > flyingHeight + 5)))
            {
                if (leader.transform.position.y < flyingHeight)
                {
                    leader.GetComponent<Rigidbody>().AddForce(transform.up * (seekStrength * 2));
                }
                else
                {
                    leader.GetComponent<Rigidbody>().AddForce(-transform.up * (seekStrength * 2));
                }
            }
        }
    }

    void UpdateOffsetPositions()
    {
        float xOffset = avoidanceDistance;
        float zOffset = avoidanceDistance;

        for (int i = 0; i < flockMembers.Count; i++)
        {
            Vector3 offset = leader.transform.position;            

            if (!(i % 2 == 0))
            {
                offset.x += xOffset;
                offset.z -= zOffset;

                zOffset += avoidanceDistance;
                xOffset += avoidanceDistance;
            }
            else
            {
                offset.x -= xOffset;
                offset.z -= zOffset;
            }

            offset.y = leader.transform.position.y;

            positionOffsets[i] = offset;
        }
    }

    public void RemoveFlockMember(GameObject _bird)
    {
        for (int i = 0; i < flockMembers.Count; i++)
        {
            if (flockMembers[i] == _bird)
            {
                flockMembers.RemoveAt(i);
            }
        }
    }    

    public void UpdateLeader()
    {
        delayOn = true;

        flockMembers[0].GetComponent<BirbController>().enabled = true;
        flockMembers[0].GetComponent<FlockMember>().SetIsLeader(true);

        main_cam.GetComponent<LazyCamera>().UpdateTarget(flockMembers[0]);

        flockMembers.RemoveAt(0);        
    }

    public void SetFlyingHeight(float _height)
    {
        flyingHeight = _height;
    }
}
