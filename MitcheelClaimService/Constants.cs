﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MitcheelClaimService
{
    public static class Constants
    {
        public static string XSDClaim = @"<?xml version='1.0' encoding='UTF-8'?>
                          <xs:schema targetNamespace='http://www.mitchell.com/examples/claim' 
                          xmlns:mxs='http://www.mitchell.com/examples/claim' 
                          xmlns:xs='http://www.w3.org/2001/XMLSchema' elementFormDefault='qualified'>

      <xs:element name='MitchellClaim' type='mxs:MitchellClaimType'/>
	
	  <xs:complexType name='MitchellClaimType'>
		  <xs:sequence>
			  <xs:element name='ClaimNumber' type='xs:string'/>
			  <xs:element name='ClaimantFirstName' type='xs:string' minOccurs='0'/>
			  <xs:element name='ClaimantLastName' type='xs:string' minOccurs='0'/>
			  <xs:element name='Status' type='mxs:StatusCode' minOccurs='0'/>
			  <xs:element name='LossDate' type='xs:dateTime' minOccurs='0'/>
			  <xs:element name='LossInfo' type='mxs:LossInfoType' minOccurs='0'/>
			  <xs:element name='AssignedAdjusterID' type='xs:long'  minOccurs='0'/>
			  <xs:element name='Vehicles' type='mxs:VehicleListType' minOccurs='0'/>
		  </xs:sequence>
	  </xs:complexType>


      <xs:simpleType name='CauseOfLossCode'>
		  <xs:restriction base='xs:string'>
			  <xs:enumeration value='Collision'/>
			  <xs:enumeration value='Explosion'/>
			  <xs:enumeration value='Fire'/>
			  <xs:enumeration value='Hail'/>
			  <xs:enumeration value='Mechanical Breakdown'/>
			  <xs:enumeration value='Other'/>
		  </xs:restriction>
	  </xs:simpleType>
	
	  <xs:simpleType name='StatusCode'>
		  <xs:restriction base='xs:string'>
			  <xs:enumeration value='OPEN'/>
			  <xs:enumeration value='CLOSED'/>
		  </xs:restriction>
	  </xs:simpleType>

	  <xs:complexType name='LossInfoType'>
		  <xs:sequence>
			  <xs:element name='CauseOfLoss' type='mxs:CauseOfLossCode' minOccurs='0'/>
			  <xs:element name='ReportedDate' type='xs:dateTime' minOccurs='0'/>
			  <xs:element name='LossDescription' type='xs:string' minOccurs='0'/>
		  </xs:sequence>
	  </xs:complexType> 
	
	  <xs:complexType name='VehicleListType'>
		  <xs:sequence>
			  <xs:element name='VehicleDetails' type='mxs:VehicleInfoType' minOccurs='1' maxOccurs='unbounded'/>
		  </xs:sequence>
	  </xs:complexType>

	  <xs:complexType name='VehicleInfoType'>
		  <xs:sequence>
			  <xs:element name='ModelYear' type='xs:int'/>
			  <xs:element name='MakeDescription' type='xs:string' minOccurs='0'/>
			  <xs:element name='ModelDescription' type='xs:string' minOccurs='0'/>
			  <xs:element name='EngineDescription' type='xs:string' minOccurs='0'/>
			  <xs:element name='ExteriorColor' type='xs:string' minOccurs='0'/>
        	  <xs:element name='Vin' type='xs:string' minOccurs='0'/>
			  <xs:element name='LicPlate' type='xs:string' minOccurs='0'/>
			  <xs:element name='LicPlateState' type='xs:string' minOccurs='0'/>
			  <xs:element name='LicPlateExpDate' type='xs:date' minOccurs='0'/>
			  <xs:element name='DamageDescription' type='xs:string' minOccurs='0'/>
			  <xs:element name='Mileage' type='xs:int' minOccurs='0'/>
		  </xs:sequence>
	  </xs:complexType>

  </xs:schema>
";
    }
}