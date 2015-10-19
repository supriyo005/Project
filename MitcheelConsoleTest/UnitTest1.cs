using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitcheelConsoleTest.MitcheelServiceReference;

namespace MitcheelConsoleTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MitcheelServiceReference.Service1Client client = new MitcheelServiceReference.Service1Client();

            //Save the cliam data to database using WCF service
            XDocument doc = XDocument.Load("Request.xml");
            string returnString = client.AddClaimData(doc.ToString());

            //get the claims from database
            ModelMitchellCliams[] modelMitchellCliams = client.GetClaims();

        }
    }
}
