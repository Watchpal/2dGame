using UnityEngine;
using UnityEngine.Windows.Speech;

public class door : MonoBehaviour
{
    [SerializeField] private Collider2D physicalCollider;
    private SpriteRenderer SpriteRenderer;
    [SerializeField] private Sprite closed;
    [SerializeField] private Sprite open;
   

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        physicalCollider.enabled = true;
        SpriteRenderer.sprite=closed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerKey player = collision.GetComponent<PlayerKey>();
            if (player != null&&player.hasKey)
            {
                player.UseKey();
                physicalCollider.enabled = false;
                SpriteRenderer.sprite = open;
                Debug.Log("Door opened");
            }
        }
    }
    
}
