using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door
{
    public GameObject doorObj;
    public int x, y;
    public bool connected = false;

    public Door(GameObject door)
    {
        doorObj = door;
        UpdateDoorPos();  
    }

    public void UpdateDoorPos()
    {
        x = Mathf.FloorToInt(doorObj.transform.position.x);
        y = Mathf.FloorToInt(doorObj.transform.position.z); // note unity uses z where you expect y
    }

    public Vector3 AdjacentPosFloat()
    {
        return doorObj.transform.position + doorObj.transform.forward;
    }

    public Vector2Int AdjacentPosInt()
    {
        //int adj_x, adj_y;
        Vector3 adj_pos = AdjacentPosFloat();
        int adj_x = Mathf.FloorToInt(adj_pos.x);
        int adj_y = Mathf.FloorToInt(adj_pos.z); // note unity uses z where you expect y        
        return new Vector2Int(adj_x, adj_y);
    }
}

public class Room : MonoBehaviour
{
    public List<Door> doors = new List<Door>();
    public int width, length;
    public int left, top, right, bottom;
    public int bearing = 0;

    void FindDoors()
        // populate door list
    {
        foreach (Transform t in transform)
        {
            if (t.name == "Doorway")
            {
                doors.Add(new Door(t.gameObject));
            }
        }
    }

    public void UpdateCornersCoords()
    {
        Renderer rend = GetComponent<Renderer>();
        right = Mathf.RoundToInt(rend.bounds.max.x);
        left = Mathf.RoundToInt(rend.bounds.min.x);
        top = Mathf.RoundToInt(rend.bounds.max.z);
        bottom = Mathf.RoundToInt(rend.bounds.min.z);
    }

    void UpdateAllDoorPos()
    {
        foreach (Door door in doors)
        {
            door.UpdateDoorPos();
        }
    }

    public void AlignDoors(Door this_door, Door target_door)
    {
        RotateAround(this_door, Mathf.RoundToInt(target_door.doorObj.transform.rotation.eulerAngles.y) + 180);
        
        MoveDoorTo(this_door, target_door.AdjacentPosFloat());
    }

    public void Snap()
    {   
        // snap top left to integer grid
        transform.position = new Vector3((float)left + (float)length / 2, 0, (float)top - (float)width / 2);
        UpdateCornersCoords();
        UpdateAllDoorPos();
    }

    public void ConnectDoors(Door this_door, Door target_door)
    {
        this_door.connected = true;
        target_door.connected = true;
    }

    void MoveDoorTo(Door door, Vector3 pos)
    {
        Debug.Log("room being moved to " + pos);
        float offset_x = pos.x - door.doorObj.transform.position.x;
        float offset_y = pos.z - door.doorObj.transform.position.z;

        transform.position += new Vector3(offset_x, 0, offset_y);
        UpdateCornersCoords();
        UpdateAllDoorPos();
    }

    void RotateAround(Door door, int angle)
    {
        transform.RotateAround(door.doorObj.transform.position, Vector3.up, angle);
        UpdateCornersCoords();
        UpdateAllDoorPos();
    }

    void Awake()
    {
        Collider m_Collider = GetComponent<Collider>();
        width = (int) m_Collider.bounds.size.z;
        length = (int) m_Collider.bounds.size.x;

        UpdateCornersCoords();
        FindDoors();
    }

}
