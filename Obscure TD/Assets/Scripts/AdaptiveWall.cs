using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveWall : MonoBehaviour {
    [SerializeField]
    LayerMask avoid;
    [SerializeField]
    bool[] around; //forward, right, backward, left

    enum Direction { up, down, left, right };
	// Use this for initialization
	void Start () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, avoid))
        {
            hit.collider.gameObject.GetComponent<AdaptiveWall>().mutate(Direction.up);
            around[0] = true;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Infinity, avoid))
        {
            hit.collider.gameObject.GetComponent<AdaptiveWall>().mutate(Direction.down);
            around[2] = true;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, avoid))
        {
            hit.collider.gameObject.GetComponent<AdaptiveWall>().mutate(Direction.left);
            around[3] = true;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.left), out hit, Mathf.Infinity, avoid))
        {
            hit.collider.gameObject.GetComponent<AdaptiveWall>().mutate(Direction.right);
            around[1] = true;
        }
        interpret();
    }

    void mutate(Direction dir) {
        switch (dir) {
            case Direction.up: around[2] = true;                                  //This indicates that the object has been hit by a raycast from BELOW. The other game object has projected a ray UP
                break;
            case Direction.down: around[0] = true;
                break;
            case Direction.left: around[1] = true;
                break;
            case Direction.right: around[3] = true;
                break;
        }
        interpret();
    }

    void interpret() {
        if ((around[0] || around[1] || around[2] || around[3]))
        {
            if ((around[0] && around[2]) || (around[1] && around[3])) { // this indicates that it should be a square piece, as it has two vertically opposed walls on either side
                
                return;
            }
            if ((around[1] || around[3]) && (around[0] || around[2])) { // this indicates that is should be a corner, as it is not a square and has two walls on adjacent sides
                if (around[1])
                {
                    if (around[0])
                        print("corner should be on the bottom left: " + this.name); // This would suggest that the prefab.forward would be facing up
                    else
                        print("corner should be on the top left: " + this.name); // Facing right 90
                }
                else
                {
                    if (around[0])
                        print("corner should be on the bottom right: " + this.name); // Facing left 90
                    else
                        print("corner should be on the top right: " + this.name); // Facing right/left 180
                }
                    
                return;
            }
                                                                        // this indicates that it should terminate and is touching only 1 other wall
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
