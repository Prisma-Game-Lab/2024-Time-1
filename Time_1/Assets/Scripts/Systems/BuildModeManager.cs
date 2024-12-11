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
    private float eletronicAmount;
    private float metalAmount;
    private float prismAmount;
    private float uraniumAmount;

    private bool isTurretSelected;
    private bool BuildMode; //Flag para verificar se está no modo de construcao
    private GameObject selectedTurret;
    private TurretData selectedTurretCosts;
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
        prismAmount = 0.0f;
        uraniumAmount = 0.0f;
        eletronicAmount = 0.0f;
        metalAmount = 0.0f;
        isTurretSelected = false;
        BuildMode = false;
        selectedTurret = null;
        canPlaceTurret = true;
        canBuyTurret = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ResourcePanel.text = resourceX.ToString();

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
                selectedTurretCosts = null;
                selectedTurret = null;
            }
        }

        if (!BuildMode) //Daqui pra baixo somente build mode 
            return;

        if (canBuy(selectedTurretCosts))
            canBuyTurret = true;
        else
            canBuyTurret = false;

        if (Input.GetMouseButtonDown(1) && isTurretSelected) //Cancela a construcao 
        {
            isTurretSelected = false;
            mouseMask.gameObject.SetActive(false);
            selectedTurretCosts = null;
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
                discountAmounts(selectedTurretCosts);
                if (!canBuy(selectedTurretCosts))
                    canPlaceTurret = false;
            }
        }
    }

    public bool canBuy(TurretData data)
    {
        if(data == null)
            return false;
        if(data.requiredEletronic > eletronicAmount)
            return false;
        if (data.requiredMetal > metalAmount)
            return false;
        if (data.requiredPrism > prismAmount)
            return false;
        if (data.requiredUranium > uraniumAmount)
            return false;

        return true;    
    }

    public void discountAmounts(TurretData data)
    {
        if (data != null)
        {
            eletronicAmount -= data.requiredEletronic;
            metalAmount -= data.requiredMetal;
            prismAmount -= data.requiredPrism;
            uraniumAmount -= data.requiredUranium;
        }
    }

    public float getResourceEletronic()
    {
        return eletronicAmount;
    }

    public float getResourceMetal()
    {
        return metalAmount;
    }

    public float getResourcePrism()
    {
        return prismAmount;
    }

    public float getResourceUranium()
    {
        return uraniumAmount;
    }

    public void increaseResources(ResourceData data)
    {
        if (data.eletronicAmount > 0)
            eletronicAmount++;
        else if(data.metalAmount > 0)
            metalAmount++;
        else if(data.prismAmount > 0)
            prismAmount++;
        else if(data.uraniumAmount > 0)
            uraniumAmount++;
    }

    public void setSelectedTurret(GameObject turret, TurretData data)
    {
        Debug.Log("SelectedTurret");
        selectedTurret = turret;
        selectedTurretCosts = data;
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
