using System.Collections.Generic;

namespace PuntoDeventa.UI.CatalogueClient.Model
{
    public class Client
    {
        public string Id { get; set; }

        public string Rut { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string CommuneName { get; set; }

        public string RouteId { get; set;}

        public string Phone { get; set; }

        public List<BranchOffices> BranchOffices { get; set; }

        public List<EconomicActivities> EconomicActivities { get; set;}
    }
}
