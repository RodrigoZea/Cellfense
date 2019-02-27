using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public static NavMeshBaker Instance { get; private set; }

    [SerializeField]
    public List<NavMeshSurface> navMeshSurfaces = new List<NavMeshSurface>();

    // Cuando inicia, queremos que el cuadrado inicial sea caminable.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        Bake();
    }

    // Por el momento, no sera util el update. Pues no queremos que se actualice el navmesh cada frame, seria muy costoso.
    void Update()
    {
        
    }

    // Este metodo sera util cuando agreguemos mas y mas planos donde se pueda caminar
    public void Bake()
    {
        for (int i = 0; i < navMeshSurfaces.Count; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }
}
