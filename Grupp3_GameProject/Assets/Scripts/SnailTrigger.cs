using UnityEngine;

//Author: Molly R�le
public class SnailTrigger : MonoBehaviour
{
    [SerializeField]
    private SnailBoss snailBoss;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            snailBoss.SetAwake(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            snailBoss.SetAwake(false);
        }
    }
}
