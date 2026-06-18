using UnityEngine;

public class Dandelion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D flowerRb;
    [SerializeField] private Rigidbody2D stemTipRb;
    [SerializeField] private Transform stemVisual;

    [Header("Ground Follow")]
    [SerializeField] private float followStrength = 20f;
    [SerializeField] private float hoverHeight = 1.5f;

    private Transform holder;

    private bool followGround;

    public Rigidbody2D StemTipRb => stemTipRb;

    private void Update()
    {
        UpdateStemVisual();
    }

    private void FixedUpdate()
    {
        if (!followGround)
            return;

        if (holder == null)
            return;

        Vector2 target =
            (Vector2)holder.position +
            Vector2.up * hoverHeight;

        Vector2 offset =
            target -
            flowerRb.position;

        flowerRb.AddForce(
            offset *
            followStrength
        );
    }

    private void UpdateStemVisual()
    {
        Vector2 flowerPos =
            flowerRb.position;

        Vector2 stemTipPos =
            stemTipRb.position;

        Vector2 dir =
            stemTipPos -
            flowerPos;

        float length =
            dir.magnitude;

        stemVisual.position =
            (flowerPos + stemTipPos) * 0.5f;

        stemVisual.rotation =
            Quaternion.Euler(
                0,
                0,
                Mathf.Atan2(
                    dir.y,
                    dir.x
                ) * Mathf.Rad2Deg - 90f
            );

        Vector3 scale =
            stemVisual.localScale;

        scale.y = length;

        stemVisual.localScale =
            scale;
    }

    public void PickUp(Transform carryPoint)
    {
        holder = carryPoint;
        followGround = true;
    }

    public void Drop()
    {
        holder = null;
        followGround = false;
    }

    public void SetGroundFollow(bool value)
    {
        followGround = value;
    }
}