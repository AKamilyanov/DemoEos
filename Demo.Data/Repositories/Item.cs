using System;
using System.Collections.Generic;
using Demo.Core.Entities;
using Newtonsoft.Json;

namespace Demo.Data.Repositories
{
    /// <summary>
    /// Class implements IItem interface for SqlServer and DTO
    /// </summary>
    public class Item : IItem
    {
        public Guid ItemId { get; set; }

        public string Title { get; set; }

        public int MaxValue { get; set; }

        [JsonIgnore]
        public Guid? ParentId { get; set; }
        
        [JsonIgnore]
        public virtual IItem Parent
        {
            get => _parent;
            set 
            {
                if (value == null) return;

                _parent = value;
                ParentId = value.ItemId; // required for bulk insert
            }
        }
        private IItem _parent;

        [JsonIgnore]
        public int Value { get; set; }

        [JsonIgnore]
        public virtual ICollection<IItem> Childs { get; set; }
    }
}
