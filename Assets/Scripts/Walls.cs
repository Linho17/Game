using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] List<PlateFloor> walls;

    public void SetWalls()
    {
        foreach (var wall in walls)
        {
            wall.SetPlateFloor(PlateState.Wall);
        }
    }

    public void AddPlane(PlateFloor plate)
    {
        walls.Add(plate);
    }
}
