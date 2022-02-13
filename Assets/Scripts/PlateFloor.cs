using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlateState
{
    Floor,
    FinishZone,
    DeathZone,
    Wall,
}

public enum NavArea
{
    Walkable,
    NotWalkable
}
public class PlateFloor : MonoBehaviour
{
    [SerializeField] private PlateState plateState;
    
    [SerializeField] private float normalPosition;
    [SerializeField] private float wallPosition;

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Material wallMaterial;
    [SerializeField] private Material deathZoneMaterial;
    [SerializeField] private Material finishZoneMaterial;

    [SerializeField] private NavMeshModifier modifier;

    public PlateState GetPlateState() => plateState;
    public void SetPlateFloor(PlateState _plateState)
    {
        plateState = _plateState;
        transform.position = new Vector3(transform.position.x, normalPosition, transform.position.z);
        renderer.material = wallMaterial;
        modifier.area = (int)NavArea.Walkable;

        switch (plateState)
        {
            case PlateState.Floor:
                
                break;
            case PlateState.FinishZone:
                renderer.material = finishZoneMaterial;
                break;
            case PlateState.DeathZone:
                renderer.material = deathZoneMaterial;
                break;
            case PlateState.Wall:
                transform.position = new Vector3(transform.position.x, wallPosition, transform.position.z);
                modifier.area = (int)NavArea.NotWalkable;
                break;
        }
    }

    
}
