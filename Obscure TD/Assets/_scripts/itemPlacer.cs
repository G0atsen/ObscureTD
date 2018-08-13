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
        if (Input.GetMouseButtonDown(0))
        {
            print("stareted");
            RaycastHit hitInfo;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            print("click");
            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }
            print(hitInfo.point);
            tilesCreated += 1;
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
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
