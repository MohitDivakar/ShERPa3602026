using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{


    public class Address
    {
        public string apartment_address { get; set; }
        public string street_address1 { get; set; }
        public string street_address2 { get; set; }
        public string landmark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public ContactDetails contact_details { get; set; }
    }

    public class ContactDetails
    {
        public string name { get; set; }
        public string phone_number { get; set; }
    }

    public class DeliveryInstructions
    {
        public List<InstructionsList> instructions_list { get; set; }
    }

    public class DropDetails
    {
        public Address address { get; set; }
    }

    public class InstructionsList
    {
        public string type { get; set; }
        public string description { get; set; }
    }

    public class PickupDetails
    {
        public Address address { get; set; }
    }

    public class PorterClass
    {
        public string request_id { get; set; }
        public DeliveryInstructions delivery_instructions { get; set; }
        public PickupDetails pickup_details { get; set; }
        public DropDetails drop_details { get; set; }
    }



    public class EstimatedFareDetails
    {
        public string currency { get; set; }
        public int minor_amount { get; set; }
    }

    public class PorterSuccess
    {
        public string request_id { get; set; }
        public string order_id { get; set; }
        public long estimated_pickup_time { get; set; }
        public EstimatedFareDetails estimated_fare_details { get; set; }
        public string tracking_url { get; set; }
    }

    public class PorterError
    {

        public string type { get; set; }

        public string message { get; set; }

    }

}