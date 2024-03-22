using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CSharpClasses.Interfaces
{
    public interface IDropableScriptableObject : IBaseScriptableObject
    {
        public float DropProbability { get; }
        public Sprite MenuIcon { get; } 
    }
}
