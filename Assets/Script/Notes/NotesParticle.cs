
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesParticle : MonoBehaviour
{
  [SerializeField] GameObject ParticleSystemPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NormalNotes")
        {
            Instantiate(ParticleSystemPrefab, transform.position, Quaternion.identity);
            
        }
    }
}