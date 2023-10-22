namespace PuntoDeventa.UI.CatalogueClient.Model
{
    public class BranchOffices
    {
        public int Code { get; set; }

        public string Commune { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is int other)
            {
                return Code == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
