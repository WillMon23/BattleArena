using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    class Player : Entity
    {
        private Item[] _items;
        private Item _currentItem;

        public override float AttackPower 
        {
            get
            {
                if (_currentItem.Type == ItemType.ATTACK)
                    return base.AttackPower + CurrentItem.StatBoost;
                return base.AttackPower;
            }
            
        }

        public override float DefensePower
        {
            get
            {
                if (_currentItem.Type == ItemType.DEFENSE)
                    return base.DefensePower + CurrentItem.StatBoost;
                return base.DefensePower;
            }

        }
        public Item CurrentItem { get { return _currentItem; } }

        public Player(string name, float health, float attactPower, float defensePower, Item[] items) : base(name, health, attactPower, defensePower)
        {
            _items = items;
            _currentItem.Name = "Nothing";
        }

        /// <summary>
        /// Sets the item at the given index to be the current item
        /// </summary>
        /// <param name="index">The Index is outside the bounds of the array</param>
        /// <returns>False if the index is outside the bounds of the array</returns>
        public bool TryEquipItem(int index)
        {
            //If the index is out of bounds...
            if (index > _items.Length || index < 0)
                //...return false
                return false;

            //Set the current item to be the arry at the given index
            _currentItem = _items[index];

            return true;
        }
        public string[] GetItemNames()
        {
            string[] itemName = new string[_items.Length];

            for(int i = 0; i < _items.Length; i++)
                itemName[i] = _items[i].Name;

            return itemName;
        }

        /// <summary>
        /// Set the current Item to be Nothing
        /// </summary>
        /// <returns>If called it will Unequip if there is somthing to un-equip</returns>
        public bool TryToRemoveCurrentItm()
        {
            //Checks to see if there is NOTHING equiped. . .
            if (CurrentItem.Name == "Nothing")
                //... If there is nothing then they cant un-equip anything 
                return false;

            _currentItem = new Item();
            _currentItem.Name = "Nothing";

            return true;

        }
    }
}
