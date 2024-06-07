using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{

    public Material material;
    LaserBeam beam;

    // Start is called before the first frame update
    void Start()
    {
        beam = new LaserBeam(material);
    }

    // Update is called once per frame
    void Update()
    {
        beam.StartRay(gameObject.transform.position, gameObject.transform.right);
    }
}
