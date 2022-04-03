using StarterAssets;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] StarterAssetsInputs input;
    [SerializeField] float maxSway;
    [SerializeField] float swaySpeed;

    RectTransform rect;
    float currentZ = 0;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (input.move.x > 0 && currentZ > -maxSway)
        {
            currentZ = Mathf.Lerp(currentZ, -maxSway, Time.deltaTime * swaySpeed);
        }
        else if (input.move.x < 0 && currentZ < maxSway)
        {
            currentZ = Mathf.Lerp(currentZ, maxSway, Time.deltaTime * swaySpeed);
        }
        else
        {
            currentZ = Mathf.Lerp(currentZ, 0, Time.deltaTime * swaySpeed);
        }

        rect.rotation = Quaternion.Euler(0, 0, currentZ);
    }
}
