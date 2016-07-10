using UnityEngine;

public enum Pole
{
    North = 1,
    South = -1
}

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Magnet : MonoBehaviour
{
    public double Strength = 4000000;

    public Pole MagneticPole = Pole.North;

    [SerializeField]
    private Rigidbody2D rigidbody = null;
    
    void Start()
    {
        rigidbody.isKinematic = true;
        Physics2D.IgnoreLayerCollision((int)Layers.Magnets, (int)Layers.Platforms);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        GameObject collidingObject = c.gameObject;

        Magnet collidingMagnet = collidingObject.GetComponent<Magnet>();

        if (collidingMagnet != null) // If we're colliding with another magnet
        { 
            Vector2 radius = (collidingObject.transform.position - transform.position);

            double xForce = (SceneProperties.Permeability * Strength * collidingMagnet.Strength) / (4 * Mathf.PI * (radius.x * radius.x));
            double yForce = 0;//(SceneProperties.Permeability * Strength * collidingMagnet.Strength) / (4 * Mathf.PI * (radius.y * radius.y));
            // Via https://en.wikipedia.org/wiki/Force_between_magnets

            Vector2 force = new Vector2(1000f, -100f);//(float)xForce, (float)yForce);

            force = -force;
            rigidbody.AddForce(force);
            transform.forward = force;
            Debug.Log(force);
        }
    }
}
