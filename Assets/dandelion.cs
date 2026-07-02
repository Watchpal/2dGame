using UnityEngine;

public class Dandelion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D flowerRb;
    [SerializeField] private Transform stemVisual;
    [SerializeField] private Transform standCheck;
    [SerializeField]
    private Vector2 standCheckSize =
        new Vector2(0.8f, 0.15f);

    [SerializeField] private LayerMask playerLayer;

    private CharacterMovement playerOnTop;


    [Header("Carry")]
    [SerializeField] private float hoverHeight = 1.5f;
    [SerializeField] private float followSpeed = 15f;

    private Transform holder;
    private bool isCarried;
    private float timeSinceThrown = 9999999f;
    private float timeSinceFrozen=99999999f;
    private bool hasBeenFrozen=true;

    [Header("Visuals")]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite settlingSprite;
    [SerializeField] private Sprite platformSprite;
    [SerializeField] private Sprite floatSprite;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer stemSpriteRenderer;

    private Color flowerDefaultColor;
    private Color stemDefaultColor;


    private enum DandelionState
    {
        Idle,
        Float,
        Settling,
        Platform
    }
    private DandelionState state;

    private Vector3 startPosition;

    private void Start()
    {
        flowerRb.gameObject.layer =
    LayerMask.NameToLayer("Dandelion");
        startPosition = transform.position;
        flowerDefaultColor = spriteRenderer.color;
        stemDefaultColor = stemSpriteRenderer.color;
    }
    private void Update()
    {
        UpdateStemVisual();
        HandleGlideAfterThrown();
        UpdatePlatformSupport();
    }

    private void FixedUpdate()
    {
        if (!isCarried)
            return;

        if (holder == null)
            return;

        if (flowerRb.linearVelocityY<0f)
        {
            SetState(DandelionState.Float);
        }

        Vector2 targetPosition =
            (Vector2)holder.position +
            Vector2.up * hoverHeight;

        Vector2 newPosition =
            Vector2.Lerp(
                flowerRb.position,
                targetPosition,
                followSpeed * Time.fixedDeltaTime
            );

        flowerRb.MovePosition(newPosition);
        
        
    }

    private void UpdateStemVisual()
    {
        if (holder == null)
        {
            stemVisual.rotation =
            Quaternion.Euler(0, 0, 0);
            Vector2 offset = Vector2.down * 0.5f;

            stemVisual.position = flowerRb.position + offset;

            return;
        }

        stemVisual.gameObject.SetActive(true);

        Vector2 flowerPos =
            flowerRb.position;

        Vector2 handPos =
            holder.position;

        Vector2 dir =
            handPos - flowerPos;

        float length =
            dir.magnitude;

        stemVisual.position =
            (flowerPos + handPos) * 0.5f;

        float angle =
            Mathf.Atan2(
                dir.y,
                dir.x
            ) * Mathf.Rad2Deg;

        stemVisual.rotation =
            Quaternion.Euler(
                0f,
                0f,
                angle - 90f
            );

        Vector3 scale =
            stemVisual.localScale;

        scale.y = length;

        //stemVisual.localScale = scale;
    }
    private void UpdateVisualState()
    {
        switch (state)
        {
            case DandelionState.Idle:
                spriteRenderer.sprite = idleSprite;
                break;

            case DandelionState.Float:
                spriteRenderer.sprite = floatSprite;
                break;

            case DandelionState.Settling:
                spriteRenderer.sprite = settlingSprite;
                break;

            case DandelionState.Platform:
                spriteRenderer.sprite = platformSprite;
                break;
        }
    }
    private void SetState(DandelionState newState)
    {
        if (state == newState)
            return;

        state = newState;
        UpdateVisualState();
    }
    public void PickUp(Transform carryPoint)
    {
        SetHighlighted(false);
        holder = carryPoint;
        isCarried = true;

        flowerRb.linearVelocity = Vector2.zero;
        flowerRb.angularVelocity = 0f;
        SetLayerRecursively(gameObject, LayerMask.NameToLayer("CarriedDandelion"));
        SetState(DandelionState.Idle);
        timeSinceFrozen = 990009009f;
        timeSinceThrown = 999999999f;
        hasBeenFrozen = true;
    }

    public void Drop(Vector2 velocity)
    {
        holder = null;
        isCarried = false;
        flowerRb.linearVelocity = velocity;
        timeSinceThrown = 0f;
        hasBeenFrozen = false;
        SetLayerRecursively(gameObject, LayerMask.NameToLayer("Dandelion"));

    }

    private void HandleGlideAfterThrown()
    {
        if (holder != null)
            return;
        timeSinceThrown += Time.deltaTime;
        timeSinceFrozen += Time.deltaTime;
        flowerRb.linearVelocity *= 0.9965f;
        flowerRb.linearDamping = 0.2f;
        flowerRb.mass = 0.5f;
        if (!hasBeenFrozen && timeSinceThrown < 999999)
            flowerRb.gravityScale = 0f;
        SetState(DandelionState.Idle);
        flowerRb.bodyType = RigidbodyType2D.Dynamic;

        if (flowerRb.linearVelocity.magnitude > 0.2f)
            return;
        if (!hasBeenFrozen)
        {
            hasBeenFrozen = true;
            timeSinceFrozen = 0f;
        }


        if (timeSinceFrozen < 5)
        {
            flowerRb.bodyType = RigidbodyType2D.Kinematic;
            flowerRb.linearVelocity = Vector2.zero;
            flowerRb.linearDamping = 10f;
            flowerRb.mass = 20f;
            SetState(DandelionState.Platform);
        }
        else
        {
            flowerRb.linearDamping = 0.2f;
            flowerRb.mass = 0.5f;
            flowerRb.bodyType = RigidbodyType2D.Dynamic;
            flowerRb.gravityScale = 0.2f;
            SetState(DandelionState.Idle);
        }
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    private void UpdatePlatformSupport()
    {
        

        Collider2D hit =
            Physics2D.OverlapBox(
                standCheck.position,
                standCheckSize,
                0f,
                playerLayer
            );

        CharacterMovement currentPlayer = null;

        if (hit != null)
        {
            currentPlayer =
                hit.GetComponent<CharacterMovement>();
        }

        if (currentPlayer == playerOnTop)
            return;

        if (playerOnTop != null)
        {
            playerOnTop.ExitDandelionPlatform();
        }

        playerOnTop = currentPlayer;

        if (playerOnTop != null)
        {
            playerOnTop.EnterDandelionPlatform();
        }
    }

    public void Reset()
    {
        if (playerOnTop != null)
        {
            playerOnTop.ExitDandelionPlatform();
            playerOnTop = null;
        }
        timeSinceThrown = 99999999f;
        timeSinceFrozen = 99999999f;
        hasBeenFrozen = true;
        flowerRb.bodyType = RigidbodyType2D.Dynamic;
        flowerRb.position=startPosition;
    }

    public void SetHighlighted(bool highlighted)
    {
        Color targetColor =
            highlighted
            ? new Color(1f, 0.95f, 0.6f)
            : flowerDefaultColor;

        spriteRenderer.color = targetColor;

        stemSpriteRenderer.color =
            highlighted
            ? new Color(1f, 0.95f, 0.6f)
            : stemDefaultColor;
        flowerRb.transform.localScale = highlighted ? Vector3.one * 1.05f : Vector3.one * 1f;
    }

    private void OnDrawGizmosSelected()
    {
        if (standCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(
            standCheck.position,
            standCheckSize
        );
    }

    public bool IsCarried =>
        isCarried;

    public Vector2 Position =>
        flowerRb.position;
}