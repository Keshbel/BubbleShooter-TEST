using UnityEngine;

public class RabbitAnim : MonoBehaviour
{
    // Update is called once per frame
    public void Idle()
    {
        GetComponent<Animation>().Play("rabbit_idle");
    }
}
