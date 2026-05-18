using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] frames;

    [Header("Settings")]
    public float lifeTime = 0.3f;
    public float moveSpeed = 1.4f;
    public float fadePower = 1.4f;

    private float _timer;
    private Vector2 _velocity;
    private Color _startColor;
    private float _startScale;
    
    private void Update()
    {
        if (frames == null || frames.Length == 0) return;
        ParticleAnim();
    }

    private void ParticleAnim()
    {
        _timer += Time.deltaTime;

        float t = _timer / lifeTime;
        t = Mathf.Clamp01(t);

        int frameIndex = Mathf.FloorToInt(t * frames.Length * 0.95f);
        frameIndex = Mathf.Clamp(frameIndex, 0, frames.Length - 1);

        spriteRenderer.sprite = frames[frameIndex];

        float alpha = Mathf.Pow(1f - t, fadePower);
        spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, alpha);

        transform.position += (Vector3)_velocity * Time.deltaTime;
        _velocity *= 0.93f;
    }
    
    public void Play(Color color, float angleDeg, float scale)
    {
        _timer = 0f;

        _startColor = color;
        _startScale = scale;

        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        transform.localScale = new Vector3(_startScale, _startScale, 1f);

        float angleRad = angleDeg * Mathf.Deg2Rad;
        _velocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * moveSpeed;

        spriteRenderer.color = _startColor;

        Destroy(gameObject, lifeTime);
    }
}
