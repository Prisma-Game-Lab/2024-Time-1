using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildModeManager : MonoBehaviour
{
    public static BuildModeManager Instance { get; private set; }

    [Header("References")]
    public TextMeshProUGUI ResourcePanel;
    public GameObject turretBuyPanel;


    [SerializeField]
    private float resourceX;
    private bool isTurretSelected;
    private bool BuildMode; //Flag para verificar se está no modo de construcao
    private GameObject selectedTurret;
    private float selectedTurretCost;
    private Vector3 mouseposition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita múltiplas instâncias
        }
    }


    private void Start()
    {
        resourceX = 0.0f;
        isTurretSelected = false;
        BuildMode = false;
        selectedTurret = null;
        selectedTurretCost = float.PositiveInfinity;
    }

    // Update is called once per frame
    void Update()
    {
        ResourcePanel.text = resourceX.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildMode = !BuildMode;

            if (BuildMode) //Esta no modo de construcao
            {
                turretBuyPanel.SetActive(true); //Ativa opcao de compra
            }
            else
            {
                turretBuyPanel.SetActive(false); //Desativa opcao de compra
            }
        }

        if(Input.GetMouseButtonDown(0) && isTurretSelected)
        {

            if (Camera.main == null)
            {
                Debug.LogError("Nenhuma câmera principal encontrada.");
                return;
            }

            // Verifique se o objeto 'selectedTurret' foi atribuído corretamente
            if (selectedTurret == null)
            {
                Debug.LogError("Nenhuma torre selecionada para instanciar.");
                return;
            }

            mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseposition.z = 2.0f;
            Instantiate(selectedTurret, mouseposition, Quaternion.identity);
            resourceX -= selectedTurretCost;
            selectedTurretCost = float.PositiveInfinity;
            isTurretSelected=false;
            selectedTurret = null;
        }
    }

    public float getResourceX()
    {
        return resourceX;
    }
    public void increaseResourceX(float n)
    {
        resourceX += n;
    }

    public void decreaseResourceX(float n)
    {
        if(resourceX-n >= 0)
        {
            resourceX -= n;
        }
    }

    public void setSelectedTurret(GameObject turret, float cost)
    {
        selectedTurret = turret;
        selectedTurretCost = cost;
        isTurretSelected = true;
    }

}
