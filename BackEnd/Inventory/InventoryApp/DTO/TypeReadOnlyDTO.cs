using System.ComponentModel.DataAnnotations;

namespace InventoryApp.DTO
{
    public class TypeReadOnlyDTO
    {
        public int TypeId { get; set; }

        public string TypeDescription { get; set; } = null!;
    }
}
