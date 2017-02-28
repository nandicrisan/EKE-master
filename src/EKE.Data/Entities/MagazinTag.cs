namespace EKE.Data.Entities
{
    public class MagazinTag
    {
        public int MagazinId { get; set; }
        public Magazine Magazin { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
