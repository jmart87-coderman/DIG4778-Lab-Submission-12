using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public enum SortBy
    {
        ID,
        Value
    }

    public SortBy sortMode;
    public bool searchSortedID;
    public int targetID;
    public GameObject inventory; // The object thet holds the Items
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
            inventoryItems[i] = Instantiate(items, transform.position, Quaternion.identity, inventory.transform);
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

    public InventoryItem BinarySearchByID(int targetID)
    {
        int left = 0;
        int right = inventoryItems.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (inventoryItems[mid].ID == targetID)
            {
                Debug.Log("Item ID found: " + inventoryItems[mid] + "!");
                return inventoryItems[mid]; // Found the matching ID
            }

            if (inventoryItems[mid].ID < targetID)
            {
                left = mid + 1; // Target is in the right half
            }
            else
            {
                right = mid - 1; // Target is in the left half
            }
        }
        Debug.Log("Item ID not found.");
        return null; // ID not found
    }

    public void SortByID()
    {
        for (int i = 0; i < inventoryItems.Length - 1; i++)
        {
            for (int j = 0; j < inventoryItems.Length - i - 1; j++)
            {
                if (inventoryItems[j].ID > inventoryItems[j + 1].ID)
                {
                    Swap(inventoryItems, j, j + 1);
                }
            }
        }
    }

    public void Swap(InventoryItem[] itemsArray, int indexA, int indexB)
    {
        InventoryItem temp = itemsArray[indexA];
        itemsArray[indexA] = itemsArray[indexB];
        itemsArray[indexB] = temp;
    }

    public void InventorySorter()
    {
        switch (sortMode)
        {
            case SortBy.ID:
                SortByID();
                if (searchSortedID)
                {
                    BinarySearchByID(targetID);
                }
                break;

            case SortBy.Value:
                
                break;
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].transform.SetSiblingIndex(i);
        }
    }
}
