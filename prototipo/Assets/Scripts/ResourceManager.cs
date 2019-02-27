using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    //Cuanta "sangre" tiene el jugador, estos son los puntos para poder crear estructuras, entrenar infanteria, etc..
    int bloodPts;
    //Cuantas estructuras hay. Mientras mas se tengan, mas celulas se podran alocar.
    public int structCount;
    //Limite maximo de unidades
    public int maxLimit;
    //Cuenta actual de unidades
    public int currCount;
    public Text points;
    public Image wavebar;
    public int currentTime = 0;

    //Singleton
    public static ResourceManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //Solo podemos tener un ResourceManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        wavebar.fillAmount = 0;
        addPts(10);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 3;
        wavebar.fillAmount = currentTime;
    }

    void addPts(int bPts) {
        bloodPts += bPts;
        points.text = bloodPts.ToString();
    }
    void subPts(int bPts) {
        bloodPts -= bPts;
    }
}
