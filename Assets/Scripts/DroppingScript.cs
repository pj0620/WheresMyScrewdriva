using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingScript : MonoBehaviour
{
  [SerializeField] GameObject leftWaypoint;
  [SerializeField] GameObject rightWaypoint;
  [SerializeField] GameObject[] tools;

  [SerializeField] float spawnRate = 2;
  private float timer = 0;

  void Update()
  {
      var leftPoint = leftWaypoint.transform.position;
      var rightPoint = rightWaypoint.transform.position;


      if (timer < spawnRate) {
        timer += Time.deltaTime;
      } else {
        var tool = tools[Random.Range(0, tools.Length)];


        var rotation = new Vector3(90,0,0);
        if (tool.name == "Saw" || tool.name == "Drill") { rotation = new Vector3(90,90,0); }

        var createdObject = Instantiate(
          tool, 
          leftPoint + (rightPoint - leftPoint) * Random.Range(0f,1f), 
          Quaternion.Euler(rotation)
        );
        timer = 0;
      }
  }
}
