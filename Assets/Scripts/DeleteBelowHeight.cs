using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBelowHeight : MonoBehaviour
{
  [SerializeField] int deleteHeight = -10;
    void Update()
    {
        if (transform.position.y < deleteHeight) {
          Destroy(gameObject);
        }
    }
}
