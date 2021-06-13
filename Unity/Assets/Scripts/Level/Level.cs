using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public GameObject RoomTemplate;

    [SerializeField, Range(3, 10)]
    public int width = 5;
    [SerializeField, Range(3, 10)]
    public int height = 5;

    public bool fadeAll = false;
    bool prevFadeAll = false;

    // TODO -- remove debug elements
    public GameObject S;
    public GameObject E;

    public GameObject cam;

    Room[,] RoomList;

    public GameObject PlayerTemplate;
    GameObject player;

    bool ValidOption(Room[,] rooms, RoomPosition pos, List<RoomPosition> excluded, bool checkVisited=true)
    {
        bool inside = pos.x > -1 && pos.x < rooms.GetLength(0) && pos.y > -1 && pos.y < rooms.GetLength(1);
        if (!inside) return false;
        bool exclusion = excluded.Contains(pos);
        bool visited = checkVisited ? rooms[pos.x, pos.y].visited : !rooms[pos.x, pos.y].visited;
        return !exclusion && !visited;
    }

    List<Door.Position> GenOptions(Room[,] rooms, RoomPosition currentPos, List<RoomPosition> excluded, bool checkVisited=true)
    {
        List<Door.Position> options = new List<Door.Position>();

        RoomPosition up = currentPos.Up();
        RoomPosition down = currentPos.Down();
        RoomPosition left = currentPos.Left();
        RoomPosition right = currentPos.Right();

        if (ValidOption(rooms, up, excluded, checkVisited)) options.Add(Door.Position.UP);
        if (ValidOption(rooms, down, excluded, checkVisited)) options.Add(Door.Position.DOWN);
        if (ValidOption(rooms, left, excluded, checkVisited)) options.Add(Door.Position.LEFT);
        if (ValidOption(rooms, right, excluded, checkVisited)) options.Add(Door.Position.RIGHT);

        return options;
    }

    void InitRooms(Room[,] rooms)
    {
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                // // Instantiate(rooms[i,j]);
                // Room tempr;
                // rooms[i,j] = tempr;
                // Instantiate(rooms[i,j]);
                rooms[i,j] = gameObject.AddComponent<Room>();
                rooms[i,j].player = player;
                rooms[i,j].cam = cam;
            }
        }
    }

    // void CleanRooms(Room[,] rooms)
    // {
    //     for (int i = 0; i < rooms.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < rooms.GetLength(1); j++)
    //         {
    //             Destroy(rooms[i,j]);
    //         }
    //     }
    // }
    void CleanRooms()
    {
        Component[] r = GetComponents(typeof(Room));
        foreach (Component room in r)
        {
            Destroy(room);
        }
    }
    
    (Room[,], RoomPosition, int, List<RoomPosition>) DepthSearch(RoomPosition start, List<RoomPosition> excluded, int max=-1)
    {
        Room[,] rooms = new Room[width, height];
        InitRooms(rooms);

        List<RoomPosition> visitedCells = new List<RoomPosition>();

        int furthest = 0;
        int all_visited = max == -1 ? width * height - excluded.Count : max - 1;
        RoomPosition furthestPos = start;
        RoomPosition currentPos = start;
        int numVisited = 0;
        int numDead = 0;

        int stackptr = 0;
        RoomPosition[] stack = new RoomPosition[width * height];
        for (int i = 0; i < stack.Length; i++)
        {
            stack[i] = new RoomPosition(-1, -1);
        }

        while (numVisited < all_visited + 1)
        {
            if (stackptr > furthest)
            {
                furthest = stackptr;
                furthestPos = currentPos;
            }

            List<Door.Position> options = GenOptions(rooms, currentPos, excluded);
            if (options.Count == 0)
            {
                if (!rooms[currentPos.x, currentPos.y].visited)
                {
                    rooms[currentPos.x, currentPos.y].visited = true;
                    rooms[currentPos.x, currentPos.y].distance = stackptr;
                    rooms[currentPos.x, currentPos.y].dead_end = true;
                    visitedCells.Add(currentPos);
                    numVisited += 1;
                    numDead += 1;
                }

                if (stackptr > 0)
                {
                    stackptr -= 1;
                    currentPos = stack[stackptr];
                }
                else
                {
                    // Depth-search failed by getting itself stuck (numdead = -1)
                    return (rooms, furthestPos, -1, visitedCells);
                }
            }
            else
            {
                rooms[currentPos.x, currentPos.y].visited = true;
                rooms[currentPos.x, currentPos.y].distance = stackptr;
                visitedCells.Add(currentPos);
                numVisited++;
                stack[stackptr++] = currentPos;
                int newposidx = (int) Random.Range(0, options.Count);
                rooms[currentPos.x, currentPos.y].doorList.Add(options[newposidx]);
                RoomPosition newpos = new RoomPosition(0, 0);
                switch (options[newposidx])
                {
                    case Door.Position.UP:
                        newpos = currentPos.Up();
                        rooms[newpos.x, newpos.y].doorList.Add(Door.Position.DOWN);
                        break;
                    case Door.Position.DOWN:
                        newpos = currentPos.Down();
                        rooms[newpos.x, newpos.y].doorList.Add(Door.Position.UP);
                        break;
                    case Door.Position.LEFT:
                        newpos = currentPos.Left();
                        rooms[newpos.x, newpos.y].doorList.Add(Door.Position.RIGHT);
                        break;
                    case Door.Position.RIGHT:
                        newpos = currentPos.Right();
                        rooms[newpos.x, newpos.y].doorList.Add(Door.Position.LEFT);
                        break;
                }
                currentPos = newpos;
            }

        }
        // fallback cause I'm bad
        if (!rooms[currentPos.x, currentPos.y].visited)
        {
            rooms[currentPos.x, currentPos.y].visited = true;
            rooms[currentPos.x, currentPos.y].distance = stackptr;
            rooms[currentPos.x, currentPos.y].dead_end = true;
            numVisited++;
            numDead++;
            if (++stackptr > furthest)
            {
                furthestPos = currentPos;
            }
        }

        rooms[furthestPos.x, furthestPos.y].key_item = true;

        return (rooms, furthestPos, numDead, visitedCells);
    }

    (Door.Position, RoomPosition, int) BestKeyDoor(List<Door.Position> options, Room[,] rooms, RoomPosition start)
    {
        int smallest = 10000;
        RoomPosition pos = new RoomPosition(0, 0);
        Door.Position direction = Door.Position.UP;
        foreach (Door.Position p in options)
        {
            switch (p)
            {
                case Door.Position.UP:
                    pos = start.Up();
                    break;
                case Door.Position.DOWN:
                    pos = start.Down();
                    break;
                case Door.Position.LEFT:
                    pos = start.Left();
                    break;
                case Door.Position.RIGHT:
                    pos = start.Right();
                    break;
            }
            int distance = rooms[pos.x, pos.y].distance;
            if (distance < smallest && distance != 0)
            {
                smallest = distance;
                direction = p;
            }
        }
        return (direction, pos, smallest);
    }

    void GenerateLevel(int maxAttempts=10)
    {
        int attempts = 1;
        float[] scores = new float[maxAttempts];
        List<Room[,]> generated = new List<Room[,]>();
        RoomPosition[] starts = new RoomPosition[maxAttempts];
        RoomPosition[] ends = new RoomPosition[maxAttempts];

        for (int i = 0; i < maxAttempts; i++)
        {
            Room[,] combined = new Room[width,height];
            // InitRooms(combined);
            RoomPosition endFinalPosition;
            List<RoomPosition> endVisited;
            RoomPosition mazeStart;
            RoomPosition mazeEnd;
            int numDead = 0;
            // also bad but whatever
            while (true)
            {
                RoomPosition end = new RoomPosition((int) Random.Range(0, width), (int) Random.Range(0, height));
                RoomPosition start = new RoomPosition((int) Random.Range(0, width), (int) Random.Range(0, height));
                while (start == end)
                {
                    start.x = (int) Random.Range(0, width);
                    start.y = (int) Random.Range(0, height);
                }

                List<RoomPosition> endExcluded = new List<RoomPosition>();
                endExcluded.Add(start);

                (Room[,] endRooms, RoomPosition efp, int eDead, List<RoomPosition> evisited) = DepthSearch(end, endExcluded, Random.Range(2, 5));
                if (eDead == -1) 
                {
                    attempts++;
                    continue;
                }
                (Room[,] nRooms, RoomPosition nfp, int nDead, List<RoomPosition> nVis) = DepthSearch(start, evisited);
                if (eDead != -1) 
                {
                    endFinalPosition = efp;
                    endVisited = evisited;
                    numDead = nDead;
                    mazeStart = start;
                    mazeEnd = end;
                    // Adding the configured rooms together
                    for (int k = 0; k < height; k++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (endRooms[j,k].visited)
                            {
                                combined[j,k] = endRooms[j,k];
                            }
                            else
                            {
                                combined[j,k] = nRooms[j,k];
                            }
                        }
                    }
                    break;
                }
            }

            // connecting the two regions with a key door
            List<Door.Position> options = GenOptions(combined, endFinalPosition, endVisited, false);
            RoomPosition fromr = endFinalPosition;
            if (options.Count == 0)
            {
                int vispos = endVisited.Count - 1;
                while (options.Count == 0)
                {
                    options = GenOptions(combined, endVisited[vispos], endVisited, false);
                    fromr = endVisited[vispos--];
                }
            }

            // locked door!
            (Door.Position bestPos, RoomPosition otherpos, int smallest) = BestKeyDoor(options, combined, fromr);
            combined[fromr.x, fromr.y].doorList.Add(Door.Lock(bestPos)); // this needs massive improvement
            if (combined[fromr.x, fromr.y].doorList.Contains(bestPos)) {
                combined[fromr.x, fromr.y].doorList.Remove(bestPos);
            }
            // combined[otherpos.x, otherpos.y].doorList.Add(Door.Lock(Door.Opposite(bestPos)));
            // Debug.Log(fromr);

            combined[mazeStart.x,mazeStart.y].type = "start";
            combined[mazeEnd.x,mazeEnd.y].type = "end";

            int[] scoretable = {0, 10, 15, 20, 10, 5};
            int smscore = 0;
            if (smallest < scoretable.Length)
            {
                smscore = scoretable[smallest];
            }

            float score = numDead * 7.5f + smscore;
            scores[i] = score;
            generated.Add(combined);
            starts[i] = mazeStart;
            ends[i] = mazeEnd;
        }

        float largest = 0;
        int idx = 0;
        for (int i = 0; i < maxAttempts; i++)
        {
            if (scores[i] > largest)
            {
                largest = scores[i];
                idx = i;
            }
        }

        // RoomList = generated[idx];
        Transform parent = transform.GetChild(0);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // NOTE -- temporary for testing, needs to be replaced by real
                // room selection code
                GameObject temproom = Instantiate(RoomTemplate);
                temproom.transform.SetParent(parent);
                temproom.transform.position = new Vector3(x * 17, -y * 11, 0);
                temproom.GetComponent<Room>().cam = cam;
                temproom.GetComponent<Room>().player = player;
                GameObject doorcontainer = temproom.GetComponent<Room>().Doors;
                List<Door.Position> doorlist = generated[idx][x, y].doorList;

                if (generated[idx][x, y].type == "start")
                {
                    // temproom.transform.GetChild(temproom.transform.childCount - 1).GetComponent<SpriteRenderer>().color = Color.gray;
                    S.transform.position = temproom.transform.position;
                    player.transform.position = temproom.transform.position;
                }

                if (generated[idx][x, y].type == "end")
                {
                    // temproom.transform.GetChild(temproom.transform.childCount - 1).GetComponent<SpriteRenderer>().color = Color.black;
                    E.transform.position = temproom.transform.position;
                }

                if (generated[idx][x, y].dead_end)
                {
                    temproom.GetComponent<Room>().EnableChest(true);
                }
                else
                {
                    temproom.GetComponent<Room>().EnableChest(false);
                }

                foreach (Transform d in doorcontainer.transform)
                {
                    Door.Position temppos = d.GetComponent<Door>().position;
                    if (doorlist.Contains(Door.Lock(temppos)))
                    {
                        // d.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
                        d.GetComponent<Door>().position = Door.Lock(d.GetComponent<Door>().position);
                        continue;
                    }
                    if (!doorlist.Contains(temppos))
                    {
                        d.gameObject.SetActive(false);
                    }
                }

                // temproom.GetComponent<Room>().FadeIn();

            }
        }

        // foreach (Room[,] r in generated)
        // {
        //     CleanRooms(r);
        // }

    }

    void Start()
    {
        player = Instantiate(PlayerTemplate);
        GenerateLevel();
        CleanRooms();
    }

    
    void Update()
    {
        if (fadeAll != prevFadeAll)
        {
            prevFadeAll = fadeAll;
            foreach (Transform t in transform.GetChild(0))
            {
                if (fadeAll)
                    t.gameObject.GetComponent<Room>().FadeIn();
                else
                    t.gameObject.GetComponent<Room>().FadeOut();
            }
        }
    }
}
