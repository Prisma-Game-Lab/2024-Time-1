using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BuildModeManager : MonoBehaviour
{
    public static BuildModeManager Instance { get; private set; }

    [Header("References")]
    public TextMeshProUGUI ResourcePanel;
    public GameObject turretBuyPanel;
    public GameObject mouseMask;
    public GameObject minimap;
    public Sprite temporaryMask;



    [SerializeField]
    private float resourceX;
    private bool isTurretSelected;
    private bool BuildMode; //Flag para verificar se está no modo de construcao
    private GameObject selectedTurret;
    private float selectedTurretCost;
    private Vector3 mouseposition;
    private bool canPlaceTurret;
    private bool canBuyTurret;

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
        canPlaceTurret = true;
        canBuyTurret = false;
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
                minimap.SetActive(false);
            }
            else
            {
                turretBuyPanel.SetActive(false); //Desativa opcao de compra
                mouseMask.SetActive(false);
                minimap.SetActive(true);
                isTurretSelected = false;
                selectedTurretCost = float.PositiveInfinity;
                selectedTurret = null;
            }
        }

        if (!BuildMode) //Daqui pra baixo somente build mode 
            return;

        if (selectedTurretCost > resourceX)
            canBuyTurret = false;
        else
            canBuyTurret = true;

        if (Input.GetMouseButtonDown(1) && isTurretSelected) //Cancela a construcao 
        {
            isTurretSelected = false;
            mouseMask.gameObject.SetActive(false);
            selectedTurretCost = float.PositiveInfinity;
            selectedTurret = null;
        }
            

        if(Input.GetMouseButtonDown(0) && isTurretSelected) //Tenta colocar torreta
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
               
            if(canPlaceTurret && canBuyTurret)
            {
                mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseposition.z = 2.0f;
                Instantiate(selectedTurret, mouseposition, Quaternion.identity);
                resourceX -= selectedTurretCost;
                if (selectedTurretCost > resourceX)
                    canPlaceTurret = false;
            }
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
        mouseMask.gameObject.SetActive(true);
        
        //mouseMask.gameObject.GetComponent<SpriteRenderer>().sprite = turret.gameObject.GetComponent<SpriteRenderer>().sprite;
        //Retornar o sprite da turret selecionada quando tiver o sprite todo
        mouseMask.gameObject.GetComponent<SpriteRenderer>().sprite = temporaryMask;


        mouseMask.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
    }

    public void CanPlaceTurret(bool b)
    {
        canPlaceTurret = b;
        if (b)
            mouseMask.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        else
            mouseMask.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.2f, 0.2f, 0.8f);
    }
}
