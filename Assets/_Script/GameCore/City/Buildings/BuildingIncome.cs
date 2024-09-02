using System;
using System.Runtime.InteropServices;

namespace _Script.GameCore.City.Buildings
{
    public class BuildingIncome
    {
        private float _incomeDelta;
        private int _baseIncome;
        public ResourceType _resourceType;


        public BuildingIncome(ResourceType resource, int baseIncome, float incomeDelta)
        { 
            _resourceType = resource;
            _baseIncome = baseIncome;
            _incomeDelta = incomeDelta;
        }

        public int GetIncome(int level)
        {
            return (int)Math.Ceiling(_baseIncome + _incomeDelta * level);
        }
    }
}