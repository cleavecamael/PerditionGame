using UnityEngine;

public class UltimateManager : MonoBehaviour
{

    public PlayerStats playerVariables;
    public GameObject missile;
    public UltiConstants ultiConstants;
    public BoolGameEvent ultimateChargeEvent;

    public void OnChargePickup(int i)
    {
        // Debug.Log("UltimateChargePickup");
        ultiConstants.charge = true;
        ultimateChargeEvent.Raise(ultiConstants);
    }


    public void OnActivate(Vector2 mousePosition)
    {
        Debug.Log("Ulti cast");
        if (ultiConstants)
        {

            // Debug.Log("called");
            Camera mainCamera = Camera.main;
            float distanceFromCamera = Mathf.Abs(mainCamera.transform.position.z); // Distance from the camera to the viewport
            float halfFOV = mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad; // Half of the camera's field of view in radians
            float topY = distanceFromCamera * Mathf.Tan(halfFOV);
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));
            Vector3 topViewportPosition = new Vector3(0.5f, 1.0f, 10.0f); // 0.5f for the center of the viewport
            Vector3 topWorldPosition = mainCamera.ViewportToWorldPoint(topViewportPosition);
            Vector2 start = new Vector2(targetPosition.x, topWorldPosition.y);
            GameObject missileInstance = Instantiate(missile, start, Quaternion.identity);
            Missile missileClass = missileInstance.GetComponent<Missile>();
            missileClass.TargetPosition = targetPosition;
            ultiConstants.charge = false;
            ultimateChargeEvent.Raise(ultiConstants.charge);
        }
    }

}