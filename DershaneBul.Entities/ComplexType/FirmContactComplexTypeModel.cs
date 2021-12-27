using System;
using System.Collections.Generic;
using System.Text;
using DershaneBul.Entities.Abstract;
using DershaneBul.Entities.Enums;

namespace DershaneBul.Entities.ComplexType
{
    public class FirmContactComplexTypeModel : IBaseEntity
    {
        public Guid FirmId { get; set; } 
        public string ContactDescription { get; set; }
        public string ContactTypeDescription { get; set; }
        public int ContactTypeId { get; set; }
        public string Icon { get; set; }

        private EnumContactType _enumContactType;

        public EnumContactType EnumContactType
        {
            get => _enumContactType;
            set => _enumContactType = (EnumContactType)ContactTypeId;
        }  
    }
}
