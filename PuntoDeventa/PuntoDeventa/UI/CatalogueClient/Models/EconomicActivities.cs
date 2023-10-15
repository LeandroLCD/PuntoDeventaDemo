namespace PuntoDeventa.UI.CatalogueClient.Model
{
    public class EconomicActivities
    {
        public string Turn { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public bool IsMain { get; set; }

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
