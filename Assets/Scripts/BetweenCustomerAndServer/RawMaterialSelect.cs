using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialSelect : MonoBehaviour, ISelect
{
    private Material m_Material;
    public string shaderThicknessName;
    public float thickness = 0.0056f;


    private void Awake()
    {
        m_Material = GetComponent<MeshRenderer>()?.material;
        Unselect();
    }
    public void Select()
    {
        m_Material.SetFloat(shaderThicknessName,thickness);
    }

    public void Unselect()
    {
        m_Material.SetFloat(shaderThicknessName, 0f);
    }
}
