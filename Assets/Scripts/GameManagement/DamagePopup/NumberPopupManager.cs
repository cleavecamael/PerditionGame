using UnityEngine;

public class NumberPopupManager : MonoBehaviour
{
    [Header("Player satisfaction")]
    [SerializeField] private int satisfactionMultiplier;
    [Header("Damage number prefabs")]
    [SerializeField] private GameObject damageEnemyNumberPrefab;
    [SerializeField] private GameObject damagePlayerNumberPrefab;
    [SerializeField] private GameObject healPlayerNumberPrefab;

    [Header("Game info")]
    [SerializeField] private Vector3Position playerPosition;
    private int sortingOrder;

    public void CreateForEnemy(Vector3 pos, int damage)
    {
        if (damage > 0)
        {
            GameObject damagePopup = Instantiate(damageEnemyNumberPrefab, pos, Quaternion.identity, transform);
            NumberPopupController controller = damagePopup.GetComponent<NumberPopupController>();
            controller.Setup(damage*satisfactionMultiplier, sortingOrder++);
        }
    }

    public void CreateDamageForPlayer(int damage)
    {
        if (damage > 0)
        {
            GameObject damagePopup = Instantiate(damagePlayerNumberPrefab, playerPosition.pos, Quaternion.identity, transform);
            NumberPopupController controller = damagePopup.GetComponent<NumberPopupController>();
            controller.Setup(damage*satisfactionMultiplier, sortingOrder++);
        }
    }

    public void CreateHealForPlayer(int heal)
    {
        if (heal > 0)
        {
            GameObject damagePopup = Instantiate(healPlayerNumberPrefab, playerPosition.pos, Quaternion.identity, transform);
            NumberPopupController controller = damagePopup.GetComponent<NumberPopupController>();
            controller.Setup(heal*satisfactionMultiplier, sortingOrder++);
        }
    }
}
