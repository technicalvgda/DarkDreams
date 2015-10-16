using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * NOTE:
 * TempRoomDoor has to start from 0 and name it with space 
 * Ex: TempRoomDoor 0
 * The Door has to be a child (to be connected to the room) of TempDoor and the name it without space
 * Ex: Door0
 * In the Hierchy :
 * Ex.
 * TempRoomDoor 0
 *     Platform
 *     Door0
*/

public class RandomMapGeneration : MonoBehaviour
{

    //Size of the map
    //each hallway is x
    //each level is y
    public int sizeOfMapX = 1;
    public int sizeOfMapY = 1;
    public int lengthOfRoom = 60;
    public int heightOfRoom = 27;
    //List of doorRoom and RegularRoom
    public Transform[] doorRoom;
    public Transform[] regRoom;
    public Transform[] extraRoom;

    private Stack<Transform> doorRoomUnvisited = new Stack<Transform>();
    private Stack<Transform> regRoomUnvisited = new Stack<Transform>();
    private Stack<Transform> extraRoomUnvisited = new Stack<Transform>();

    //DoorRoomEachFloor
    private List<int> doorRoomEachFloor = new List<int>(2);
    //number of door per floor is 2
    private const int NUMBER_OF_DOOR_PER_FLOOR = 2;

    //linkedDoor to keep track of the door to be connected on each floor
    private List<Transform> linkedDoor = new List<Transform>();


	

    // Use this for initialization
    void Awake()
    {
        initializeRooms();
        instantiateRooms();
        setLinkedDoorPrefab();
    }

    //shuffle door and reg room.
    void initializeRooms()
    {
        //shuffle doorRooms
        for (int i = 0; i < doorRoom.Length; i++)
        {
            int idx = Random.Range(i, doorRoom.Length);
            //swap elements
            Transform tmp = doorRoom[i];
            doorRoom[i] = doorRoom[idx];
            doorRoom[idx] = tmp;
        }
        //shuffle regRoom
        for (int i = 0; i < regRoom.Length; i++)
        {
            int idx = Random.Range(i, regRoom.Length);
            //swap elements
            Transform tmp = regRoom[i];
            regRoom[i] = regRoom[idx];
            regRoom[idx] = tmp;
        }
        for (int i = 0; i < doorRoom.Length; i++)
        {
            doorRoomUnvisited.Push(doorRoom[i]);
        }
        for (int i = 0; i < regRoom.Length; i++)
        {
            regRoomUnvisited.Push(regRoom[i]);
        }
        for (int i = 0; i < extraRoom.Length; i++)
        {
            extraRoomUnvisited.Push(extraRoom[i]);
        }
        if (doorRoom.Length < sizeOfMapX * NUMBER_OF_DOOR_PER_FLOOR)
        {
            Debug.Log("Door rooms size should be more than " + sizeOfMapX * NUMBER_OF_DOOR_PER_FLOOR);
        }
        if (regRoom.Length < (sizeOfMapX * sizeOfMapY) - (sizeOfMapX * NUMBER_OF_DOOR_PER_FLOOR))
        {
            Debug.Log("Regular rooms size should be more than " + ((sizeOfMapX * sizeOfMapY) - (sizeOfMapX * NUMBER_OF_DOOR_PER_FLOOR)));
        }
    }

    //Generate randomDoorRoom
    void generateRandomDoor()
    {
        int randomDoor = 0;
        int numberOfDoorEachFloor = 0;
        while (doorRoomEachFloor.Count < 2)
        {
            randomDoor = Random.Range(0, sizeOfMapX);
            if (!doorRoomEachFloor.Contains(randomDoor))
            {
                doorRoomEachFloor.Add(randomDoor);
                numberOfDoorEachFloor++;
            }
        }
    }

    //set linkedDoorPrefab
    //Precondition:The name of each room needs to have a space between name and number and Door has to be named Door and number
    //without a space.
    //Ex: TempRoomDoor 1 and Door1
    //Ex: TempRoomDoor 5 and Door5
    void setLinkedDoorPrefab()
    {

        for (int i = 0; i < linkedDoor.Count; i++)
        {
            //Skip the first door since it will be connected to the basement
            if (i == 0)
            {
                string s1 = linkedDoor[i].name;
                string[] doorNumber1 = s1.Split(' ');

                Transform gameObjLast = GameObject.Find("Door" + doorNumber1[1]).transform;
                gameObjLast.GetComponent<TeleportDoors>().exit = GameObject.Find("BasementDoor").transform;
				//gameObjLast.GetComponent<TeleportDoors>().numberOfTimes++;
				Destroy (gameObjLast.GetComponent("TeleportDoors"));

                Transform gameObjBaseDoor = GameObject.Find("BasementDoor").transform;
                gameObjBaseDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber1[1]).transform;


                i++;
            }
            //if it is the last room, skip the last door since it will be connected to the rooftopRoom
            if (i == linkedDoor.Count - 1)
            {
                string s = linkedDoor[i].name;
                string[] lastDoor = s.Split(' ');

                Transform gameObjectLast = GameObject.Find("Door" + lastDoor[1]).transform;
                gameObjectLast.GetComponent<TeleportDoors>().exit = GameObject.Find("AtticDoor").transform;


                Transform gameObjAtticDoor = GameObject.Find("AtticDoor").transform;
                gameObjAtticDoor.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + lastDoor[1]).transform;
				Destroy (gameObjAtticDoor.GetComponent("TeleportDoors"));
                break;

            }
            else
            {
                //get the door number from linkedDoor
                string s1 = linkedDoor[i].name;
                string s2 = linkedDoor[i + 1].name;
                string[] doorNumber1 = s1.Split(' ');
                string[] doorNumber2 = s2.Split(' ');

                //Connecting the door
                Transform gameObjLast = GameObject.Find("Door" + doorNumber2[1]).transform;
                gameObjLast.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber1[1]).transform;


                Transform gameObject2 = GameObject.Find("Door" + doorNumber1[1]).transform;
                gameObject2.GetComponent<TeleportDoors>().exit = GameObject.Find("Door" + doorNumber2[1]).transform;

                i++;

            }

        }
        for (int i = 0; i < linkedDoor.Count; i++)
        {
            Debug.Log("This is the linkedDoor of " + linkedDoor[i]);
        }

    }


    //Assume that each door and regular room has the same s    ize.
    void instantiateRooms()
    {
        //level is needed to keep track of each floor.
        int level = 0;
        Vector3 sizeOfPrefab = new Vector3(0, 0, 0);

        //the basement
        Transform basement = extraRoomUnvisited.Pop();
        Instantiate(basement, new Vector3(120, -27, 0), transform.rotation);

        //the attic
        Transform attic = extraRoomUnvisited.Pop();
        Instantiate(attic, new Vector3(120, 135, 0), transform.rotation);

        for (int j = 0; j < sizeOfMapY; j++)
        {
            generateRandomDoor();
            for (int i = 0; i < sizeOfMapX; i++)
            {
                if (doorRoomEachFloor.Contains(i))
                {
                    //Get each room out of the stack doorRoomUnvisited
                    Transform doorR = doorRoomUnvisited.Pop();
                    //Add door to linkedDoor to link each door and only 2 doors are connected
                    linkedDoor.Add(doorR);
                    Instantiate(doorR, transform.position + sizeOfPrefab, transform.rotation);
                    //Size/Gap between each hallway (length of rooms)
                    sizeOfPrefab.x += lengthOfRoom;
                }
                else
                {
                    Transform regR = regRoomUnvisited.Pop();
                    Instantiate(regR, transform.position + sizeOfPrefab, transform.rotation);

                    //Size/Gap between each hallway  (length of rooms)
                    sizeOfPrefab.x += lengthOfRoom;
                }
            }
            //Clear the doorRoomEachFloor
            doorRoomEachFloor.Clear();
            sizeOfPrefab.x = 0;
            sizeOfPrefab.y = 0;
            sizeOfPrefab.z = 0;
            level++;

            //Size/Gap between each level (height of rooms)
            sizeOfPrefab.y += heightOfRoom;
            sizeOfPrefab.y *= level;
        }
    }
}