using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GlowHighLight : MonoBehaviour
{
    private Dictionary<Renderer, Material[]> glowMaterialDictionary;
    private Dictionary<Renderer, Material[]> originalMaterialDictionary;
    private Dictionary<Color, Material> cachedGlowMaterials;
    private Dictionary<Renderer, Material[]> glowExtraMaterialDictionary;
 

    public Material glowMaterial;
    public Material extraGlowMaterial;

    private bool isGlowing = false;

    private void Awake()
    {
        glowMaterialDictionary = new Dictionary<Renderer, Material[]>();
        originalMaterialDictionary = new Dictionary<Renderer, Material[]>();
        cachedGlowMaterials = new Dictionary<Color, Material>();
        glowExtraMaterialDictionary = new Dictionary<Renderer, Material[]>();
        PrepareMaterialDictionaries();
    }

    private void PrepareMaterialDictionaries()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] originalMaterials = renderer.materials;
            originalMaterialDictionary.Add(renderer, originalMaterials);
            Material[] newMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material mat = null;
                if (cachedGlowMaterials.TryGetValue(originalMaterials[i].color, out mat) == false)
                {
                    mat = new Material(glowMaterial);
                    mat.color = originalMaterials[i].color;
                }

                newMaterials[i] = mat;
            }
            
            Material[] newExtraMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material mat = null;
                if (cachedGlowMaterials.TryGetValue(originalMaterials[i].color, out mat) == false)
                {
                    mat = new Material(extraGlowMaterial);
                    mat.color = originalMaterials[i].color;
                }

                newMaterials[i] = mat;
            }

            glowMaterialDictionary.Add(renderer, newMaterials);
            glowExtraMaterialDictionary.Add(renderer, newExtraMaterials);

        }
    }

    public void ToggleGlow()
    {
        if (isGlowing == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = glowMaterialDictionary[renderer];
            }  
        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = originalMaterialDictionary[renderer];
            }
        }
        isGlowing = !isGlowing;
    }

    public void ToggleExtraGlow()
    {
        foreach (Renderer renderer in originalMaterialDictionary.Keys)
        {
            renderer.materials = glowExtraMaterialDictionary[renderer];
        }

        isGlowing = true;
    }

    public void ToggleGlow(bool state)
    {
        if (isGlowing == state)
            return;
        isGlowing = !state;
        ToggleGlow();
    }
    
}