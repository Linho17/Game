using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plates : MonoBehaviour
{
    [SerializeField] private PlateFloor[] plates;
    private PlateFloor finishZone;
    public void SetPlates(PlateFloor finishPlate)
    {
        foreach (var plate in plates)
        {
            plate.SetPlateFloor(PlateState.Floor);
        }
        finishPlate.SetPlateFloor(PlateState.FinishZone);
    }


}
