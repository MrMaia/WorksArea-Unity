// Copyright (c) 2024 Synty Studios Limited. All rights reserved.
// Otimizado para Unity 6.3

using System.Collections.Generic;
using System.Linq; // Necessário para converter Array para List, se mantivermos a Lista.
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
    /// <summary>
    ///     Oscillates the value of a list of sliders.
    ///     Refactored to comply with Unity 6 standards.
    /// </summary>
    public class SampleOscillateSliders : MonoBehaviour
    {
        [Header("References")]
        // DICA: Em projetos reais, prefira Array se o tamanho for fixo, evita overhead de List.
        // Mantive List para compatibilidade com o script original.
        public List<Slider> sliders;

        [Header("Parameters")]
        public bool autoGetSliders = true;
        public float speed = 1f;
        public float offset = 0.5f;

        private void Reset()
        {
            // O Reset roda apenas no Editor quando o componente é adicionado ou resetado.
            FindSliders();
        }

        private void Start()
        {
            if (autoGetSliders)
            {
                FindSliders();
            }
        }

        private void Update()
        {
            // Cache de Time.time para evitar chamadas repetidas na propriedade (micro-otimização)
            float currentTime = Time.time;
            
            // Usar 'for' tradicional é mais performático que 'foreach' em Lists no Unity
            for (int i = 0; i < sliders.Count; i++)
            {
                if (sliders[i] != null) // Safety Check: previne erros se um slider for destruído
                {
                    sliders[i].value = (Mathf.Sin((currentTime * speed) + (i * offset)) * 0.5f) + 0.5f;
                }
            }
        }

        /// <summary>
        /// Método centralizado para buscar os sliders.
        /// Evita duplicação de lógica entre Start e Reset.
        /// </summary>
        private void FindSliders()
        {
            // UNITY 6 CHANGE:
            // FindObjectsSortMode.None: Muito mais rápido, não ordena os objetos.
            // FindObjectsSortMode.InstanceID: Comportamento legado (lento).
            var foundSliders = FindObjectsByType<Slider>(FindObjectsSortMode.None);
            
            sliders = foundSliders.ToList();
        }
    }
}