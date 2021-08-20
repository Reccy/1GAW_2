using UnityEngine;

[SelectionBase]
public class Pellet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Temp code!
        if (collision.gameObject.CompareTag("PachMan"))
        {
            Destroy(gameObject);
        }
    }
}
