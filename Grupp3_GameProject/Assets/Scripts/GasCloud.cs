using UnityEngine;


public class GasCloud : MonoBehaviour
{   
    [SerializeField]
    private float damage = 20;

    [SerializeField]
    private float timerValue = 2;

    private float timer;

    private void Awake()
    {
        timer = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                other.GetComponent<Health>().DecreaseHealth(damage);
                timer = timerValue;
            }
        }

    }
}
