using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
  [SerializeField] GameObject screwdriver;
  [SerializeField] GameObject outOfMapWaypoint;
  [SerializeField] GameObject leftMainMenuWaypoint;
  [SerializeField] GameObject rightMainMenuWaypoint;
  [SerializeField] GameObject[] toolsWithPhysics;
  [SerializeField] float mainMenuSpawnRate = 2;
  [SerializeField] bool inMainMenu = true;
  [SerializeField] Camera mainMenuCamera;
  [SerializeField] GameObject mainMenuObject;
  [SerializeField] GameObject pickScrewdrivaObject;
  [SerializeField] Camera firstPlayerCamera;
  [SerializeField] GameObject firstPlayerController;
  [SerializeField] GameObject winWaypoint;
  [SerializeField] GameObject winMessage;

  private float mainMenuTimer = 0;
  private bool nearScrewdriva = false;
  void Start()
  {
    GameObject[] toolWaypoints = GameObject.FindGameObjectsWithTag("ToolWaypoint");
    GameObject[] sourceToolObjects = GameObject.FindGameObjectsWithTag("Tool");

    // randomly place tools at waypoints keeping track of where tools went
    var rng = new System.Random();
    Shuffle(sourceToolObjects, rng);
    for (int i=0; i < toolWaypoints.Length; i++) {
      sourceToolObjects[i].transform.position = toolWaypoints[i].transform.position;
    }

    // Randomly replace one tool with screwdriva
    int screwdrivaIdx = Random.Range(0, toolWaypoints.Length);
    screwdriver.transform.position = toolWaypoints[screwdrivaIdx].transform.position;
    sourceToolObjects[screwdrivaIdx].SetActive(false);

    gotoMainMenu();
  }

  public void startGame() {
    inMainMenu = false;

    mainMenuCamera.enabled = false;
    mainMenuObject.SetActive(false);

    firstPlayerCamera.enabled = true;
    firstPlayerController.SetActive(true);
  }

  public void gotoMainMenu() {
    inMainMenu = true;

    mainMenuCamera.enabled = true;
    mainMenuObject.SetActive(true);

    firstPlayerCamera.enabled = false;
    firstPlayerController.SetActive(false);
  }

  public void showPickupOption(bool nearScrewdrivaIn) {
    Debug.Log("pickup item");
    if (inMainMenu) { return; }
    pickScrewdrivaObject.SetActive(nearScrewdrivaIn);
    nearScrewdriva = nearScrewdrivaIn;
  }


  public void Shuffle<T>(IList<T> list, System.Random rnd)
  {
      for(var i=list.Count; i > 0; i--)
          this.Swap(list, 0, rnd.Next(0, i));
  }
  public void Swap<T>(IList<T> list, int i, int j)
  {
      var temp = list[i];
      list[i] = list[j];
      list[j] = temp;
  }

  private void UpdateMainMenu() {
    var leftPoint = this.leftMainMenuWaypoint.transform.position;
    var rightPoint = this.rightMainMenuWaypoint.transform.position;

    if (this.mainMenuTimer < this.mainMenuSpawnRate) {
      this.mainMenuTimer += Time.deltaTime;
    } else {
      var tool = this.toolsWithPhysics[Random.Range(0, this.toolsWithPhysics.Length)];
      var rotation = new Vector3(90,0,0);
      if (tool.name == "Saw" || tool.name == "Drill") { rotation = new Vector3(90,90,0); }

      var createdObject = Instantiate(
        tool, 
        leftPoint + (rightPoint - leftPoint) * Random.Range(0f,1f), 
        Quaternion.Euler(rotation)
      );
      this.mainMenuTimer = 0;
    }
  }

  void Update() {
    if (inMainMenu) {
      UpdateMainMenu();
      return;
    } 
    
    if (Input.GetKeyDown(KeyCode.E) && nearScrewdriva) {
      showPickupOption(false);
      firstPlayerController.transform.position = winWaypoint.transform.position;
      firstPlayerController.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
      winMessage.SetActive(true);
    }
  }
}
