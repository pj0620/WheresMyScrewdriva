using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
  [SerializeField] GameLogic logic;

  void Start()
  {
    logic = GameObject.FindGameObjectsWithTag("Logic")[0].GetComponent<GameLogic>();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Screwdriva") {
      logic.showPickupOption();
    }
  }
}
