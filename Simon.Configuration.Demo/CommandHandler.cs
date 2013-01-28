using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Simon.Configuration.Demo
{
    /// <summary>
    /// IConfigurationSectionHandler is a old method which is used in .net 1.1
    /// </summary>
    public class CommandHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            XmlElement node = section as XmlElement;
            List<Company> companies = new List<Company>();
            foreach (XmlElement xe in node.ChildNodes)
            {
                Company company = new Company
                {
                    Name = xe.GetAttribute("name"),
                    DoorNo = xe.GetAttribute("doorNo"),
                    Street = xe.GetAttribute("street"),
                    City = xe.GetAttribute("city"),
                    PostalCode = Int64.Parse(xe.GetAttribute("postalCode")),
                    Country = xe.GetAttribute("country"),
                };
                companies.Add(company);
            }

            return companies;
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public Int64 PostalCode { get; set; }
        public string Country { get; set; }
    }
}
