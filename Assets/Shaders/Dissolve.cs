using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private float fff = 0.0f;
    private bool bDissolve;
    private Renderer _renderer;
    private Material _material;
    private MaterialPropertyBlock _propBlock;
    private CharacterState State;

    void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
        _material = GetComponent<Renderer>().material;
        State = GetComponent<CharacterState>();
    }

    private void Update()
    {
        #region
        if (bDissolve)
        {
            fff += 1f * Time.fixedDeltaTime * 0.5f;
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_Threshold", fff);
            _renderer.SetPropertyBlock(_propBlock);
        }
        else
        {
            fff = 0;
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_Threshold", fff);
            _renderer.SetPropertyBlock(_propBlock);
            _renderer.material.shader = Shader.Find("Toon/Lit");
        }

        if (fff >= 1)
        {
            ObjectPool.Intance.CollectObject(transform.parent.gameObject, 0);
            bDissolve = false;
        }
        #endregion

        //_material.SetFloat("", 1f);
    }

    public void dissolve()
    {
        _renderer.material.shader = Shader.Find("Custom/Dissolve");
        bDissolve = true;
    }
}
