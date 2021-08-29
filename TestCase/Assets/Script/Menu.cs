using UnityEngine;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject settingButton;
    [SerializeField] private GameObject menuMessageBos;
    [SerializeField] private LoadData loadData;

    public void OpenNormalMap()
    {
        loadData.LoadDataNormalMap();
        settingButton.SetActive(true);
        menuMessageBos.SetActive(false);
    }

    public void OpenHardMap()
    {
        loadData.LoadDataHardMap();
        settingButton.SetActive(true);
        menuMessageBos.SetActive(false);
    }
}
