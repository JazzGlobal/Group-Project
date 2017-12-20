using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GroupProject
{
    /// <summary>
    /// Responsible for writing and reading XMLDocument objects.
    /// </summary>
    public static class Estimate
    {
        /// <summary>
        /// Writes an XML file that contains basic user informatino.
        /// </summary>
        /// <param name="firstName">Client's First Name</param>
        /// <param name="lastName">Client's Last Name</param>
        /// <param name="address">Client's Address</param>
        /// <param name="state">Client's State</param>
        /// <param name="city">Client's City</param>
        /// <param name="zipCode">Client's Zip Code</param>
        public static void WriteXml(string firstName, string lastName, string address, string state, string city, string zipCode, List<Tuple<string, string, string>> materialList, bool debug)
        {
            var curdir = Directory.GetCurrentDirectory();
            if (debug)
            {
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DebugTools");
            }
            XmlWriter xmlWriter = XmlWriter.Create(lastName + ".xml");
            xmlWriter.WriteStartElement("estimate");
            xmlWriter.WriteAttributeString("FirstName", firstName);
            xmlWriter.WriteAttributeString("LastName", lastName);
            xmlWriter.WriteAttributeString("Address", address);
            xmlWriter.WriteAttributeString("State", state);
            xmlWriter.WriteAttributeString("City", city);
            xmlWriter.WriteAttributeString("ZipCode", zipCode);

            xmlWriter.WriteStartElement("MaterialList");
            for (int i = 0; i < materialList.Count; i++)
            {
                xmlWriter.WriteStartElement("Item" + i);
                xmlWriter.WriteAttributeString("Name", materialList[i].Item1);
                xmlWriter.WriteAttributeString("Price", materialList[i].Item2);
                xmlWriter.WriteAttributeString("Quantity", materialList[i].Item3);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            Console.WriteLine("File was written successfully!");
            Console.ReadLine();

            Directory.SetCurrentDirectory(curdir);
        }
        /// <summary>
        /// Returns an XmlDocument object. 
        /// </summary>
        /// <param name="path">File path to the Xml Document.</param>
        /// <returns></returns>
        public static XmlDocument ReadXml(string path)
        {
            XmlDocument x = new XmlDocument();
            if (path == "")
            {
                path = "nolastname";
            }
            x.Load(path);
            return x;
        }
    }
}
    /// <summary>
    /// Used to transfer user data within the code from XML files.
    /// </summary>
    public class BuildingList
    {
        string firstname;
        string lastname;
        string address;
        string state;
        string city;
        string zipcode;
        List<Tuple<string, string, string>> materialList;

        public string FirstName
        {
            get { return firstname; }
        }
        public string LastName
        {
            get { return lastname; }
        }
        public string Address
        {
            get { return address; }
        }
        public string State
        {
            get { return state; }
        }
        public string City
        {
            get { return city; }
        }
        public string ZipCode
        {
            get { return zipcode; }
        }
        public static BuildingList CurrentBuildingList;
        public static XmlDocument XmlDoc;

        public List<Tuple<string, string, string>> MaterialList
        {
            get { return materialList; }
        }
        /// <summary>
        /// Creates a new building list object from an existing Xml Document. 
        /// </summary>
        /// <param name="xmlDoc"></param>
        public BuildingList(XmlDocument xmlDoc)
        {
            XmlNodeList estimate = xmlDoc.GetElementsByTagName("estimate");
            firstname = (estimate[0].Attributes.GetNamedItem("FirstName").Value);
            lastname = (estimate[0].Attributes.GetNamedItem("LastName").Value);
            address = (estimate[0].Attributes.GetNamedItem("Address").Value);
            state = (estimate[0].Attributes.GetNamedItem("State").Value);
            city = (estimate[0].Attributes.GetNamedItem("City").Value);
            zipcode = (estimate[0].Attributes.GetNamedItem("ZipCode").Value);

            materialList = new List<Tuple<string, string, string>>();
            for (int i = 0; i < xmlDoc.GetElementsByTagName("MaterialList")[0].ChildNodes.Count; i++)
            {
                XmlNodeList ml = xmlDoc.GetElementsByTagName("MaterialList");
                var name = ml[0].ChildNodes.Item(i).Attributes.GetNamedItem("Name").Value;
                var price = ml[0].ChildNodes.Item(i).Attributes.GetNamedItem("Price").Value;
                var quantity = ml[0].ChildNodes.Item(i).Attributes.GetNamedItem("Quantity").Value;

                materialList.Add(new Tuple<string, string, string>(name, price, quantity));
            }
            SetCurrentBuildingList();
            XmlDoc = xmlDoc;
        }
        public void SetCurrentBuildingList()
        {
            CurrentBuildingList = this;
        }
        public void AddItemToList(string name, string price, string units)
        {
            materialList.Add(new Tuple<string, string, string>(name, price, units));
        }
        public void RemoveItemFromList()
        {

        }
        /// <summary>
        /// Prints information gathered by Building List Object. 
        /// </summary>
        public void PrintInformation()
        {
            Console.WriteLine(firstname);
            Console.WriteLine(lastname);
            Console.WriteLine(address);
            Console.WriteLine(state);
            Console.WriteLine(city);
            Console.WriteLine(zipcode + "\n" + "\n");

            Console.WriteLine("Material List Items");
            foreach (Tuple<string, string, string> t in materialList)
            {
                Console.WriteLine("Name: " + t.Item1);
                Console.WriteLine("Price: " + t.Item2);
                Console.WriteLine("Amount: " + t.Item3);
            }
        }
    }

