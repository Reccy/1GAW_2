using UnityEngine;

[SelectionBase]
public class PowerPellet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        // Temp code!
        if (collision.gameObject.CompareTag("PachMan"))
        {
            Destroy(gameObject);
        }
    }
}
