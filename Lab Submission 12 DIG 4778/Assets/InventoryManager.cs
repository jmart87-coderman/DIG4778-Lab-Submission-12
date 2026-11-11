using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem items;
    public InventoryItem[] inventoryItems;
    public int listSize;
    public string[] itemNames;

    public string itemSearch; // make a public string you can quickly type items into
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryItems = new InventoryItem[listSize];
        generateItems();

        InventoryItem foundItem = LinearSearchByName(itemSearch);

        if (foundItem != null)
        {
            Debug.Log("Item found: " + foundItem.Name + "!");
        }
        else
        {
            Debug.Log("Item not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateItems() // creates a list of random items with random values and IDs
    {
        for (int i = 0; i < listSize; i++)
        {
            inventoryItems[i] = Instantiate(items);
            inventoryItems[i].Name = itemNames[Random.Range(0, itemNames.Length)]; // make the name a random selection from the itemNames array
            inventoryItems[i].Value = Random.Range(1, 100);
            inventoryItems[i].ID = Random.Range(1, 10000);
        }
    }

    public InventoryItem LinearSearchByName(string itemName) // linear search that goes looking through each item in the inventory trying to match the name
    {
        for (int i = 0; i < listSize; i++) // checks each item one by one
        {
            if (inventoryItems[i].Name == itemName) // if the names match with a name on the list
            {
                return inventoryItems[i]; // return the entire item
            }
        }



        return null; // return null if not
    }
}
