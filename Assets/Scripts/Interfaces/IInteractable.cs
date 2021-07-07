using UnityEngine;


//[RequireComponent(typeof(MeshRenderer))]
public abstract class IInteractable : MonoBehaviour
{
    //Shader m_startShader;

    //public MeshRenderer MeshRenderer { get; set; }

    //void Start()
    //{
    //    MeshRenderer = GetComponent<MeshRenderer>();
    //    m_startShader = MeshRenderer.material.shader;
    //}

    //public void ResetShader()
    //{
    //    SetShader(m_startShader);
    //}

    //public void SetShader(Shader shader)
    //{
    //    for (int i = 0; i < MeshRenderer.materials.Length; i++)
    //    {
    //        MeshRenderer.materials[i].shader = shader;
    //    }
    //}

    public abstract void Interact();
}
