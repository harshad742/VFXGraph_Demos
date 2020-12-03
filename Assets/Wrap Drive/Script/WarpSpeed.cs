using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WarpSpeed : MonoBehaviour
{
    public VisualEffect warpSpeedVFX;
    public MeshRenderer cylinder;
    public float rate = 0.02f;
    public float delay = 2.5f;

    private bool warpActive;

    // Start is called before the first frame update
    void Start()
    {
        warpSpeedVFX.Stop();
        warpSpeedVFX.SetFloat("WarpAmount", 0);

        cylinder.material.SetFloat("Active_", 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            warpActive = true;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            warpActive = false;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }

        // Vfx Effect Start
        IEnumerator ActivateParticles()
        {
            if(warpActive)
            {
               warpSpeedVFX.Play();

                float amount = warpSpeedVFX.GetFloat("WarpAmount");
                while(amount < 1 & warpActive)
                {
                    amount += rate;
                    warpSpeedVFX.SetFloat("WarpAmount", amount);
                    yield return new WaitForSeconds(0.1f);

                }
            }

            else
            {
                float amount = warpSpeedVFX.GetFloat("WarpAmount");
                while (amount > 0 & !warpActive)
                {
                    amount -= rate;
                    warpSpeedVFX.SetFloat("WarpAmount", amount);
                    yield return new WaitForSeconds(0.1f);

                    if(amount <= 0+rate)
                    {
                        amount = 0;
                        warpSpeedVFX.SetFloat("WarpAmount", amount);
                        warpSpeedVFX.Stop();

                    }

                }
               
            }

        }
    }        
         // Vfx Effect End
         
        // Shader Effect Start
        IEnumerator ActivateShader()
        {
            if(warpActive)
            {
                yield return new WaitForSeconds(delay);
                float amount = cylinder.material.GetFloat("Active_");
                while(amount < 1 & warpActive)
                {
                    amount += rate;
                    cylinder.material.SetFloat("Active_", amount);
                    yield return new WaitForSeconds(0.1f);

                }
            }

            else
            {
                float amount = cylinder.material.GetFloat("Active_");
                while (amount > 0 & !warpActive)
                {
                    amount -= rate;
                    cylinder.material.SetFloat("Active_", amount);
                    yield return new WaitForSeconds(0.1f);

                    if(amount <= 0+rate)
                    {
                        amount = 0;
                        cylinder.material.SetFloat("Active_", amount);

                    }

                }
               
            }

        }
    }
        // Shader Effect End

        

    

