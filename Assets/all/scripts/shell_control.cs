using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_control : MonoBehaviour
{
    public ParticleSystem shell_explosion;
    public float explosion_radius;
    public float explosion_force;
    public LayerMask explosion_layer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] tanks_collider = Physics.OverlapSphere(transform.position, explosion_radius, explosion_layer);
        for(int i=0;i<tanks_collider.Length;i++)
        {
            var tanks_rigidbody = tanks_collider[i].gameObject.GetComponent<Rigidbody>();
            if (tanks_rigidbody != null) 
            {
                tanks_rigidbody.AddExplosionForce(explosion_force, this.transform.position, explosion_radius);
            }        
        }
        shell_explosion.transform.parent = null;
        if(shell_explosion)
        {
            shell_explosion.Play();
            Destroy(shell_explosion.gameObject, shell_explosion.main.duration);
        }
        Destroy(this.gameObject);
    }
}
