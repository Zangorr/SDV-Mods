using System;
using StardewValley;

namespace CJBItemSpawner.Framework.ItemData
{
    /// <summary>A game item with metadata.</summary>
    /// <remarks>This is copied from the SMAPI source code and should be kept in sync with it.</remarks>
    internal class SearchableItem
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The <see cref="ItemDataDefinition.Identifier"/> value for the item type.</summary>
        public string Type { get; }

        /// <summary>A sample item instance.</summary>
        public Item Item { get; }

        /// <summary>Create an item instance.</summary>
        public Func<Item> CreateItem { get; }

        /// <summary>The unqualified item ID.</summary>
        public string ID { get; }

        /// <summary>The qualified item ID.</summary>
        public string QualifiedItemID { get; }

        /// <summary>The item's default name.</summary>
        public string Name => this.Item.Name;

        /// <summary>The item's display name for the current language.</summary>
        public string DisplayName => this.Item.DisplayName;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="type">The item type.</param>
        /// <param name="id">The unqualified item ID.</param>
        /// <param name="createItem">Create an item instance.</param>
        public SearchableItem(string type, string id, Func<SearchableItem, Item> createItem)
        {
            this.Type = type;
            this.ID = id;
            this.QualifiedItemID = this.Type + this.ID;
            this.CreateItem = () => createItem(this);
            this.Item = createItem(this);
        }

        /// <summary>Construct an instance.</summary>
        /// <param name="item">The item metadata to copy.</param>
        public SearchableItem(SearchableItem item)
        {
            this.Type = item.Type;
            this.ID = item.ID;
            this.CreateItem = item.CreateItem;
            this.Item = item.Item;
        }

        /// <summary>Get whether the item name contains a case-insensitive substring.</summary>
        /// <param name="substring">The substring to find.</param>
        public bool NameContains(string substring)
        {
            return
                this.Name.IndexOf(substring, StringComparison.OrdinalIgnoreCase) != -1
                || this.DisplayName.IndexOf(substring, StringComparison.OrdinalIgnoreCase) != -1;
        }

        /// <summary>Get whether the item name is exactly equal to a case-insensitive string.</summary>
        /// <param name="name">The substring to find.</param>
        public bool NameEquivalentTo(string name)
        {
            return
                this.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                || this.DisplayName.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
