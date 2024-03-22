using System.Collections.Generic;

namespace Assets.Scripts.CSharpClasses.Inventory
{
    public class GunInventoryBasedOnLevelSystem : InventoryBase<DropableGunScriptableObject>
    {
        private LevelSystem m_levelSystem;

        public GunInventoryBasedOnLevelSystem(LevelSystem levelSystem) : base()
        {
            m_levelSystem = levelSystem;
        }

        public GunInventoryBasedOnLevelSystem(LevelSystem levelSystem, IEnumerable<DropableGunScriptableObject> items) : base(items)
        {
            m_levelSystem = levelSystem;
        }

        public void Configurate(LevelSystem levelSystem)
        {
            m_levelSystem = levelSystem;
        }

        public void Configurate(LevelSystem levelSystem, IEnumerable<DropableGunScriptableObject> items)
        {
            Configurate(levelSystem);
            AddRange(items);
            SetDefaultKey();
        }

        public override bool TryAddItem(DropableGunScriptableObject item)
        {
            if (item.MinLevel > m_levelSystem.CurrentLevel)
            {
                return false;
            }
            return base.TryAddItem(item);
        }
    }
}
