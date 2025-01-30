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

    [SerializeField] private float eletronicAmount;
    [SerializeField] private float metalAmount;
    [SerializeField] private float prismAmount;
    [SerializeField] private float uraniumAmount;

    private bool isTurretSelected;
    private bool BuildMode; //Flag para verificar se está no modo de construcao
    private GameObject selectedTurret;
    private TurretData selectedTurretCosts;
    private Vector3 mouseposition;
    private bool canPlaceTurret;
    private bool canBuyTurret;

    private bool canUpgradeTurret;
    private bool canUpgradeLaser;

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
        canUpgradeTurret = false;
        canUpgradeLaser = false;
    }

    // Update is called once per frame
    void Update()
    {

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
                canUpgradeTurret = false;
                canUpgradeLaser = false;
                TooltipSystem.Hide();
            }
        }

        if (!BuildMode) //Daqui pra baixo somente build mode 
            return;

        if (canBuy(selectedTurretCosts))
            canBuyTurret = true;
        else
            canBuyTurret = false;

        if (Input.GetMouseButtonDown(1) && isTurretSelected || Input.GetMouseButtonDown(1) && canUpgradeLaser || 
            Input.GetMouseButtonDown(1) && canUpgradeTurret) //Cancela a construcao 
        {
            isTurretSelected = false;
            mouseMask.gameObject.SetActive(false);
            selectedTurretCosts = null;
            selectedTurret = null;
            canUpgradeTurret = false;
            canUpgradeLaser = false;
        }

        if (canUpgradeLaser || canUpgradeTurret)
            return;

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
        selectedTurret = turret;
        selectedTurretCosts = data;
        canUpgradeLaser = false;
        canUpgradeTurret = false;
        isTurretSelected = true;
        mouseMask.gameObject.SetActive(true);
        
        //Retornar o sprite da turret selecionada quando tiver o sprite todo
        mouseMask.gameObject.GetComponent<SpriteRenderer>().sprite = temporaryMask;


        mouseMask.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
    }

    public void enableUpgradeDefault(bool flag)
    {
        canUpgradeTurret = flag;
    }

    public void enableUpgradeLaser(bool flag)
    {
        canUpgradeLaser = flag;
    }

    public bool _canUpgradeDefault()
    {
        return canUpgradeTurret;
    }

    public bool _canUpgradeLaser()
    {
        return canUpgradeLaser;
    }

    public void CanPlaceTurret(bool b)
    {
        canPlaceTurret = b;
        if (b)
            mouseMask.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        else
            mouseMask.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.2f, 0.2f, 0.8f);
    }

    public bool isBuildMode()
    {
        return BuildMode;
    }
}
