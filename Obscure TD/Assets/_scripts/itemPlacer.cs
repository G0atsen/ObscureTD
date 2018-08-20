using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPlacer : MonoBehaviour {

    private grid grid;
     public Camera cam;
    public GameObject tilePrefab;
    public int tilesCreated;


    private void Awake()
    {
        grid = FindObjectOfType<grid>();
    }

    private void Update()
    {
        /* Removed to test player controller
         * 
         * 
         * if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }
            tilesCreated += 1;
        }*/
    }

    public void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        var clone = Instantiate(tilePrefab, finalPosition,Quaternion.identity);
        clone.name = "tile" + tilesCreated;
       // GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
	}
	
	
}
