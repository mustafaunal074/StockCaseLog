using StockCaseLog.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCaseLog.Domain.Entities
{
    public class ChangeLog :BaseEntity
    {
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public int PrimaryKeyValue { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChanged { get; set; }

        public EnumState State { get; set; }

    }

    public enum EnumState
    {
        Update =1,
        Delete =2,
        Added =3
    }
}
