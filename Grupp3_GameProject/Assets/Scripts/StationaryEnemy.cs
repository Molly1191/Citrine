using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Molly R�le

public class StationaryEnemy : MonoBehaviour, IKillable
{
    [SerializeField]
    private float maxGasCloudSize;

    [SerializeField]
    private bool inRange = false;

    [Header("Particles")]
    [SerializeField]
    private ParticleSystem deathParticles;

    [SerializeField]
    private ParticleSystem gasCloudParticles;

    [Header("Sound")]
    [SerializeField]
    private AudioClip dyingSound;

    [SerializeField]
    private AudioClip gasCloudSound;

    [Header("AttackCooldown")]
    [SerializeField, Min(0)] 
    private float timerValue = 1.0f;
    private float timer;

    new private MeshRenderer renderer;
    private bool isDead = false;
    private float posX;
    private float posY;
    private float posZ;
    private Vector3 originalLocalScale;
    private Transform gasCloudTransform;
    private SphereCollider inRangeCollider;
    private SphereCollider gasCloudCollider;

    private void Awake()
    {
        timer = timerValue;
        renderer = gameObject.GetComponent<MeshRenderer>();
        inRangeCollider = GetComponent<SphereCollider>();
        originalLocalScale = transform.GetChild(0).localScale;
        gasCloudTransform = transform.GetChild(0);
    }

    void Update()
    {
        if (inRange)
        {
            SpreadGas();
        }
        else
        {
            RemoveGas();
        }

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }

    public float GetStationaryEnemyPosX()
    {
        return posX;
    }
    public float GetStationaryEnemyPosY()
    {
        return posY;
    }
    public float GetStationaryEnemyPosZ()
    {
        return posZ;
    }

    public bool StationaryEnemyIsDead()
    {
        return isDead;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //SpreadGas();
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Cancel sound
        CancelSound();

        //RemoveGas();
        inRange = false;
    }

    private void SpreadGas()
    {
        float gasGrowthFactor = 5f;
        if (gasCloudTransform.localScale.x < maxGasCloudSize && gasCloudTransform.localScale.y < maxGasCloudSize && gasCloudTransform.localScale.z < maxGasCloudSize)
        {
            gasCloudTransform.localScale += (Vector3.one * gasGrowthFactor * Time.deltaTime);

            //Timer
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                EventCallbacks.EventHelper.CreateSoundEvent(gameObject, gasCloudSound);
                EventCallbacks.EventHelper.CreateParticleEvent(gameObject, gasCloudParticles);
                timer = timerValue;
            }

        }
        else
        {
            gasCloudTransform.localScale = Vector3.one * maxGasCloudSize;
        }
    }

    private void RemoveGas()
    {
        //Shrink collider back to its original scale
        gasCloudTransform.localScale = originalLocalScale;
    }

    private void CancelSound()
    {
        EventCallbacks.EventHelper.CreateSoundEvent(gameObject, gasCloudSound, true);
    }

    public void Die()
    {
        CancelSound();
        EventCallbacks.EventHelper.CreateParticleEvent(gameObject, deathParticles);
        EventCallbacks.EventHelper.CreateSoundEvent(gameObject, dyingSound);

        inRange = false;
        isDead = true;
        EventCallbacks.EventHelper.CreateDeathEvent(gameObject);
    }
}

