using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MitcheelClaimService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        /// <summary>
        /// Thsi will save the cliams to database
        /// </summary>
        /// <param name="claimInputXml"></param>
        /// <returns></returns>
        public string AddClaimData(string claimInputXml)
        {
            ServiceUtility serviceUtility = new ServiceUtility(claimInputXml);

            if (serviceUtility.ValidateXml())
            {
                if (serviceUtility.AddClaim())
                {
                    return "Status: OK.";
                }
                else
                {
                    return "Status: Not Saved.";
                }
            }
            else
            {
                return "Status: Not Vlaid XML.";
            }
        }

        /// <summary>
        /// this will return the list of claims from database
        /// </summary>
        /// <returns></returns>
        public List<ModelMitchellCliams> GetClaims()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            return serviceUtility.GetClaims();
        }
    }
}
