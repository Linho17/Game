using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private PlateFloor plateFloorPrefab;
    [SerializeField] private Transform parentPlate;

    [SerializeField] private Walls walls;
    private PlateFloor[,] plates;

    [SerializeField] private int horizontalLength;
    [SerializeField] private int verticalLength;


    private float chanceWall = 0.1f;
    private float chanceDeath = 0.1f;

    private void Awake()
    {
        CreatePlates();
        CreateWalls();

    }
    private float step => plateFloorPrefab.transform.localScale.x;
    public void GenerateField(out Transform startPlatePosition, out Transform finishPlatePosition)
    {
        
        for (int i = 0; i < horizontalLength; i++)
        {
            for (int j = 0; j < verticalLength; j++)
            {
                
                float chance = Random.Range(0, 1f);

                if (chance <= chanceWall)
                {
                    plates[i, j].SetPlateFloor(PlateState.Wall);
                }
                else if (chance <= chanceWall + chanceDeath)
                {
                    plates[i, j].SetPlateFloor(PlateState.DeathZone);
                }
                else
                {
                    plates[i, j].SetPlateFloor(PlateState.Floor);
                }
                
            }
        }
        plates[horizontalLength - 1, verticalLength - 1].SetPlateFloor(PlateState.FinishZone);
        plates[0, 0].SetPlateFloor(PlateState.Floor);

        startPlatePosition = plates[0, 0].transform;
        finishPlatePosition = plates[horizontalLength - 1, verticalLength - 1].transform;
    }

    private void CreatePlates()
    {
        plates = new PlateFloor[horizontalLength, verticalLength];
        for (int i = 0; i < horizontalLength; i++)
        {
            for (int j = 0; j < verticalLength; j++)
            {
                PlateFloor tempPlate = Instantiate(plateFloorPrefab, new Vector3(i * step, 0, j * step), Quaternion.identity, parentPlate);
                tempPlate.name = i + "_" + j + "_plate";
                float chance = Random.Range(0, 1f);


                if (chance <= chanceWall)
                {
                    tempPlate.SetPlateFloor(PlateState.Wall);
                }
                else if (chance <= chanceWall + chanceDeath)
                {
                    tempPlate.SetPlateFloor(PlateState.DeathZone);
                }
                else
                {
                    tempPlate.SetPlateFloor(PlateState.Floor);
                }
                plates[i, j] = tempPlate;

            }
        }
    }

    private void CreateWalls()
    {
        for (int i = -1; i < horizontalLength + 1; i++)
        {
            for (int j = -1; j < verticalLength + 1; j++)
            {
                if (i == -1 || i == horizontalLength || j == -1 || j == verticalLength)
                {
                    PlateFloor tempPlate = Instantiate(plateFloorPrefab, new Vector3(i * step, 0, j * step), Quaternion.identity, walls.transform);
                    tempPlate.name = i + "_" + j + "_walls";
                    walls.AddPlane(tempPlate);
                }
            }
        }

        walls.SetWalls();
    }
}
