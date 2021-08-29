using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] GameObject massagebos;
    [SerializeField] LoadData loadData;
    [SerializeField] Text nameSprite;
    [SerializeField] private new Camera camera;

    private CameraController cameraController;

    private void Start()
    {
        cameraController = camera.GetComponent<CameraController>();
    }
    public void OpenSetting()
    {
        massagebos.SetActive(true);
        nameSprite.text = loadData.map.List[0].Id;
        cameraController.enabled = false;
    }
    public void CloseSetting()
    {
        massagebos.SetActive(false);
        cameraController.enabled = true;
    }
}
