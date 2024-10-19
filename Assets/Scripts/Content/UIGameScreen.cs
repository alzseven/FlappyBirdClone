using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Object = UnityEngine.Object;

namespace Content
{
    public class UIGameScreen : UIBaseScreen
    {
        //TODO: 
        private Dictionary<Type, Dictionary<string, UnityEngine.Object>> _objects = new();

        private void Awake()
        {
            foreach (var graphic in GetComponentsInChildren<MaskableGraphic>())
            {
                if(_objects.TryGetValue(graphic.GetType(), out var typeDict))
                    typeDict.Add(graphic.gameObject.name, graphic);
                else
                    _objects.Add(graphic.GetType(), new Dictionary<string, Object>{ {graphic.gameObject.name, graphic} });
            }
        }

        public override T Get<T>(string nameToFind)
        {
            if (_objects.TryGetValue(typeof(T), out var objects))
            {
                if (objects.TryGetValue(nameToFind, out var res))
                    return res as T;
                
                throw new Exception($"No {typeof(T)} named - {nameToFind}");
            }
            throw new Exception($"Invalid Type : {typeof(T)}");
        }

        public override void EnableObject<T>(string nameToFind)
        {
            var go = Get<T>(nameToFind) as MaskableGraphic;
            if(go) go.gameObject.SetActive(true);
        }
        
        public override void ClearAll()
        {
            foreach (var typedObjects in _objects.Values)
            {
                foreach (var obj in typedObjects.Values)
                {
                    var go = obj as MaskableGraphic;
                    if (go != null) go.gameObject.SetActive(false);
                }
            }
        }
    }
}