using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public static class SortingLayerUtil
    {
        public static void SetSortingLayer(SpriteRenderer sr)
        {
            sr.sortingOrder += Mathf.RoundToInt(-sr.bounds.min.y * 1000);
        }
        
        public static void SetStaticSortingLayers(List<SpriteRenderer> sortingLayers)
        {
            float lowestY = sortingLayers.Min(sr => sr.bounds.min.y);

            foreach (var sr in sortingLayers)
                sr.sortingOrder += Mathf.RoundToInt(-lowestY * 1000);
        }

        public static void SetDynamicSortingLayers(List<SpriteRenderer> sortingLayers,
            Dictionary<SpriteRenderer, int> offsets)
        {
            float lowestY = sortingLayers.Min(sr => sr.bounds.min.y);
            
            foreach (var sr in sortingLayers)
            {
                var offset = offsets.ContainsKey(sr) ? offsets[sr] : 128;
                sr.sortingOrder = Mathf.RoundToInt(-lowestY * 1000 + offset);
            } 
        }
    }
}