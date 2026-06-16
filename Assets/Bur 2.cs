using UnityEngine;

public class NewEmptyCSharpScript
{
    public class Player : MonoBehaviour

    {
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.CompareTag("riktiga buren"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }

}
