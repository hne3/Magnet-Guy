using UnityEngine;

public class MagnetGuy : Magnet
{
    private void Start ()
    {
        Physics2D.IgnoreLayerCollision((int)Layers.MagnetGuy, (int)Layers.Platforms);
	}
}
