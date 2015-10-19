using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using MitcheelDBEntity;
using Newtonsoft.Json;

namespace MitcheelClaimService
{
    public class ServiceUtility
    {
        /// <summary>
        /// input as xml string
        /// </summary>
        private string ClaimInputXml { get; set; }

        public ServiceUtility()
        {
            ClaimInputXml = null;
        }
        public ServiceUtility(string claimInputXml)
        {
            ClaimInputXml = claimInputXml;
        }

        /// <summary>
        /// Add clims to database
        /// </summary>
        /// <returns></returns>
        public bool AddClaim()
        {
            XNamespace ns = "http://www.mitchell.com/examples/claim";

            try
            {
                var claimXdoc = XDocument.Parse(ClaimInputXml);

                using (var context = new MitchellEntities())
                {
                    var StatusCodes = from status in context.StatusCodes
                                      select status;

                    var CauseOfLossCode = from causeOfLossCode in context.CauseOfLossCodes
                                          select causeOfLossCode;

                    //we can make the hardcoding configurable
                    MitchellClaim mitchellClaim = (from r in claimXdoc.Descendants(ns + "MitchellClaim")
                                                   let Status = r.Element(ns + "Status").Value
                                                   select new MitchellClaim()
                                                   {
                                                       claimNumber = Convert.ToString(r.Element(ns + "ClaimNumber").Value),
                                                       claimantFirstName = Convert.ToString(r.Element(ns + "ClaimantFirstName").Value),
                                                       caimantLastName = Convert.ToString(r.Element(ns + "ClaimantLastName").Value),
                                                       status = (from a in StatusCodes
                                                                 where a.status == Status
                                                                 select a.id).FirstOrDefault(),
                                                       lossdate = DateTime.Parse(Convert.ToString(r.Element(ns + "LossDate").Value)),

                                                   }).FirstOrDefault();

                    context.MitchellClaims.Add(mitchellClaim);


                    LossInfo lossInfo = (from r in claimXdoc.Descendants(ns + "MitchellClaim").Descendants(ns + "LossInfo")
                                         let CauseOfLoss = r.Element(ns + "CauseOfLoss").Value
                                         select new LossInfo()
                                         {
                                             MitchellClaim = mitchellClaim,
                                             iCauseOfLoss = (from a in CauseOfLossCode
                                                             where a.cause == CauseOfLoss
                                                             select a.id).FirstOrDefault(),
                                             LossDescription = Convert.ToString(r.Element(ns + "LossDescription").Value),
                                             ReportedDate = DateTime.Parse(Convert.ToString(r.Element(ns + "ReportedDate").Value))

                                         }).FirstOrDefault();


                    context.LossInfoes.Add(lossInfo);

                    //if there is multiple vehicals then we can make it as list item.
                    Vehicle vehicles = (from r in claimXdoc.Descendants(ns + "MitchellClaim").Descendants(ns + "Vehicles").Descendants(ns + "VehicleDetails")
                                        select new Vehicle()
                                        {
                                            MitchellClaim = mitchellClaim,
                                            Vin = Convert.ToString(r.Element(ns + "Vin").Value),
                                            ModelYear = Convert.ToInt32(r.Element(ns + "ModelYear").Value),
                                            MakeDescription = Convert.ToString(r.Element(ns + "MakeDescription").Value),
                                            ModelDescription = Convert.ToString(r.Element(ns + "ModelDescription").Value),
                                            EngineDescription = Convert.ToString(r.Element(ns + "EngineDescription").Value),
                                            ExteriorColor = Convert.ToString(r.Element(ns + "ExteriorColor").Value),
                                            LicPlate = Convert.ToString(r.Element(ns + "LicPlate").Value),
                                            LicPlateState = Convert.ToString(r.Element(ns + "LicPlateState").Value),
                                            LicPlateExpDate = DateTime.Parse(r.Element(ns + "LicPlateExpDate").Value),
                                            DamageDescription = Convert.ToString(r.Element(ns + "DamageDescription").Value),
                                        }).FirstOrDefault();

                    context.Vehicles.Add(vehicles);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //log exceptionin log file
                //throw ex;
                return false;
            }
            
            return true;
        }

        public List<ModelMitchellCliams> GetClaims()
        {
            List<ModelMitchellCliams> listModelMitchellCliamses=new  List<ModelMitchellCliams>();
            try
            {
                using (var context = new MitchellEntities())
                {
                   
                    DbSet<MitchellClaim> mitchellClaims = context.MitchellClaims;

                    listModelMitchellCliamses = (from claim in mitchellClaims
                        select new ModelMitchellCliams()
                        {
                            caimantLastName = claim.caimantLastName,
                            claimNumber = claim.claimNumber,
                            claimantFirstName = claim.claimantFirstName,
                            status = claim.status,
                            lossdate = claim.lossdate,
                            AssignedAdjusterID = claim.AssignedAdjusterID,
                            Status = claim.StatusCode.status
                            //we can fill rest of the fields of model from db context and return it.
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                //log the excpetion in log file or database tables.
                //return "";
            }
            return listModelMitchellCliamses;
        }

        /// <summary>
        /// Validate the InComming XML against XSD file. If it faills then return the false
        /// </summary>
        /// <returns></returns>
        public bool ValidateXml()
        {
            bool blnValidate = false;
            try
            {
                var xDocument = XDocument.Parse(ClaimInputXml);
               
                XmlSchemaSet schemas1 = new XmlSchemaSet();
                schemas1.Add("http://www.mitchell.com/examples/claim", XmlReader.Create(new StringReader(Constants.XSDClaim)));

                xDocument.Validate(schemas1, (o, e) =>
                {
                    blnValidate = true;
                });

                return blnValidate;
            }
            catch (Exception Ex)
            {
                //Add logs to log file
                return blnValidate;
            }
        }
    }
}