using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MitcheelClaimService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string AddClaimData(string inputXml);

        [OperationContract]
        List<ModelMitchellCliams> GetClaims();

    }

    //we can put the below model classes into a model DLL and add the reference to this project

    /// <summary>
    /// This is the data contract that will be exposed to client
    /// </summary>
    [DataContract]
    public class ModelMitchellCliams
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string claimNumber { get; set; }

        [DataMember]
        public string claimantFirstName { get; set; }

        [DataMember]
        public string caimantLastName { get; set; }

        [DataMember]
        public Nullable<byte> status { get; set; }

        [DataMember]
        public Nullable<System.DateTime> lossdate { get; set; }

        [DataMember]
        public Nullable<int> AssignedAdjusterID { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public ICollection<ModelLossInfo> LossInfoes { get; set; }

        [DataMember]
        public ICollection<ModelVehicle> Vehicles { get; set; }
    }

    public class ModelStatusCode
    {
        public byte id { get; set; }
        public string status { get; set; }
    }

    public class ModelLossInfo
    {
        public long id { get; set; }
        public Nullable<int> iCauseOfLoss { get; set; }
        public Nullable<System.DateTime> ReportedDate { get; set; }
        public string LossDescription { get; set; }

    }

    public class ModelVehicle
    {
        public long id { get; set; }
        public Nullable<int> ModelYear { get; set; }
        public string MakeDescription { get; set; }
        public string ModelDescription { get; set; }
        public string EngineDescription { get; set; }
        public string ExteriorColor { get; set; }
        public string Vin { get; set; }
        public string LicPlate { get; set; }
        public string LicPlateState { get; set; }
        public Nullable<System.DateTime> LicPlateExpDate { get; set; }
        public string DamageDescription { get; set; }
        public Nullable<int> Mileage { get; set; }
    }
}
