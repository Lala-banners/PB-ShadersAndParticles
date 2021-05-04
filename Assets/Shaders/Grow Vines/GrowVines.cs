using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowVines : MonoBehaviour
{
    public Animator animator;
    public List<MeshRenderer> growVinesMesh;
    public float growTime = 5;
    public float refreshRte = 0.05f;

    [Range(0, 1)] public float minGrowth;
    [Range(0, 1)] public float maxGrowth;

    private List<Material> growVinesMats = new List<Material>();
    private bool isFullyGrown;

    // Start is called before the first frame update
    void Start()
    {
        //Go through the mesh renderers in the vines
        for (int i = 0; i < growVinesMesh.Count; i++)
        {
            //Go through the materials in the vines
            for (int v = 0; v < growVinesMesh[i].materials.Length; v++)
            {
                //If the reference to the Grow float in the shader matches
                if (growVinesMesh[i].materials[v].HasProperty("_grow"))
                {
                    //Set the shader reference to minimum growth variable in the script
                    growVinesMesh[i].materials[v].SetFloat("_grow", minGrowth);

                    //Add this mesh renderer and material to the list
                    growVinesMats.Add(growVinesMesh[i].materials[v]);
                }
            }
        }
    }

    private void Update()
    {
        MagicGrow();
    }

    public void MagicGrow()
    {
        //Go through all materials in the vines
        for (int i = 0; i < growVinesMats.Count; i++)
        {
            //Play animation for grow attack
            animator.Play("Grow", 0);

            //Start Coroutine for growing the vines
            StartCoroutine(Grow(growVinesMats[i]));
        }
    }

    public IEnumerator Grow(Material mat)
    {
        float growthValue = mat.GetFloat("_grow");

        //If fully grown is not true then increase the grow variable
        if (!isFullyGrown)
        {
            //While growth value is less than the maximum
            while (growthValue < maxGrowth)
            {
                //Increment growth value by dividing the time the vine takes to grow by the refresh rate
                growthValue += 1 / (growTime / refreshRte);

                //Assign the shader grow value to the growth value in this IEnumerator
                mat.SetFloat("_grow", growthValue);

                yield return new WaitForSeconds(refreshRte);
            }
        }
        //Else shrink the grow variable
        else
        {
            //While growth value is greater than the maximum
            while (growthValue > maxGrowth)
            {
                //Decrease growth value by dividing the time the vine takes to grow by the refresh rate
                growthValue -= 1 / (growTime / refreshRte);

                //Assign the shader grow value to the growth value in this IEnumerator
                mat.SetFloat("_grow", growthValue);

                yield return new WaitForSeconds(refreshRte);
            }
        }

        //Check if the growth value is greater than or equal to the maximum
        if (growthValue >= maxGrowth)
        {
            //If yes, the vine is fully grown
            isFullyGrown = true;
        }
        else
        {
            //If not the vine is not fully grown
            isFullyGrown = false;
        }
    }
}
