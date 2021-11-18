using UnityEngine;

//Author: Molly R�le
public class SnailBoss : MonoBehaviour, IKillable
{
    [SerializeField] 
    private State[] states;

    [SerializeField] 
    private BoxCollider awakeTrigger;

    [SerializeField] 
    private GameObject shell;

    [SerializeField] 
    private GameObject body;

    [SerializeField] 
    private bool awake = false;

    [SerializeField] 
    private bool vulnerable = false;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform mouth;

    [SerializeField]
    private Transform [] chargePoints;

    [SerializeField]
    private GameObject rotationTarget;

    private Health health;
    private Rigidbody rb;
    private StateMachine stateMachine;
    private bool isDead = false;
    private float posX;
    private float posY;
    private float posZ;

    private void Awake()
    {
        stateMachine = new StateMachine(this, states);
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        stateMachine.RunUpdate();

        //F�r att dra ner o upp HP:t och kunna testa om den byter state vid r�tt HP
        if(Input.GetKeyDown(KeyCode.V))
        {
            DecreaseHealth(10);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            health.InitializeHealth();
            //Debug.Log("Health reset to 500");
        }

    }

    public GameObject GetBody()
    {
        return body;
    }
    public bool IsVulnerable()
    {
        return vulnerable;
    }

    public void SetVulnerable(bool isVulnerable)
    {
        vulnerable = isVulnerable;
    }

    public bool IsAwake()
    {
        return awake;
    }
    public void SetAwake(bool isAwake)
    {
        awake = isAwake;
    }
    public bool BossIsDead()
    {
        return isDead;
    }
    public float GetBossPosX()
    {
        return posX;
    }
    public float GetBossPosY()
    {
        return posY;
    }
    public float GetBossPosZ()
    {
        return posZ;
    }
    public Transform GetPlayerTransform()
    {
        return player;
    }

    public Transform GetMouthTransform()
    {
        return mouth;
    }

    public Transform[] GetChargePoints()
    {
        return chargePoints;
    }

    public GameObject GetRotationTarget()
    {
        return rotationTarget;
    }

    public void UpdatePosition(Vector3 pos)
    {
        rb.MovePosition(pos);
    }

    public float GetHealth()
    {
        return health.GetCurrentHealth();
    }

    public void DecreaseHealth(float damage)
    {
        //Boss in invulnerable in SleepingState and RollAtkState
        if (vulnerable)
        {
            health.DecreaseHealth(damage);
            //Debug.Log("Boss taking damage, new HP is: " + GetHealth());
        }
        
    }

    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
