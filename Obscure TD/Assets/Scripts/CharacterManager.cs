using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    [System.Serializable]
    public struct Possible {
        public Tower tower;
        public int weight;//higher weight means more frequent
        public float occurence;
    }

    public List<Combiner> recipes = new List<Combiner>();

    public List<Possible> towers = new List<Possible>();
    public GameObject towerBase;
    public SpawnedTowers Inventory;

    float totalWeight;

    private void OnValidate()
    {
        totalWeight = 0;
        foreach (Possible t in towers)
            totalWeight += t.weight;
        Inventory = this.GetComponent<SpawnedTowers>();
    }

    private void Awake()
    {
        OnValidate();
    }

    private void Update()
    {
        if (RoundManager.Phase == 0 && RoundManager.TowersToBuild > 0) //this means that we can build
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                build();
                RoundManager.TowersToBuild -= 1;
                if (RoundManager.TowersToBuild == 0)
                {
                    RoundManager.Phase = 1; //now we are in the tower selection phase
                    foreach (Combiner c in recipes) {
                        if (c.CanCraft(Inventory)) {
                            print("You can craft a: " + c.name);
                        }
                    }
                    print("You can no longer craft");
                }
            }
        } else if (RoundManager.Phase == 1)
        {
            //now we can select towers and view their stats
        } else if (RoundManager.Phase == 2)
        {
            //enemies spawn and we deal with that through the enemy manager
        }
        
    }
    // Use this for initialization
    void build() {
     // Generate a random position in the list.
      float pick = Random.value * totalWeight;
      int chosenIndex = 0;
      float cumulativeWeight = towers[0].weight;
      // Step through the list until we've accumulated more weight than this.
      // The length check is for safety in case rounding errors accumulate.
      while(pick > cumulativeWeight && chosenIndex < towers.Count - 1)
      {
         chosenIndex++;
         cumulativeWeight += towers[chosenIndex].weight;
      }
        // Spawn the chosen item.
        GameObject tower =  Instantiate(towers[chosenIndex].tower.prefab, Vector3.up, transform.rotation);
        Tower SOTower = Instantiate(towers[chosenIndex].tower);
        tower.GetComponent<InGameTower>().tower = SOTower;
        Inventory.AddTower(tower.GetComponent<InGameTower>().tower);

        print("Built a: " + towers[chosenIndex].tower.name + " with a " + towers[chosenIndex].weight + " in " + totalWeight + " chance.");
   }
	
}
