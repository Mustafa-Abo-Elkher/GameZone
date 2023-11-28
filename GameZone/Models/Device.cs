namespace GameZone.Models
{
    public class Device: BaseEntity
    {
        [MaxLength(50)]
        public String Icon { get; set; } = String.Empty;

    }
}
