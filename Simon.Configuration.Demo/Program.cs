using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections;

namespace Simon.Configuration.Demo
{
    class Program
    {
        /// <summary>
        /// reference:
        /// http://www.codeproject.com/Articles/16466/Unraveling-the-Mysteries-of-NET-2-0-Configuration
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //GetSimpleSection();

            //GetProviderSection();

            //GetSingleSection();

            //GetSingleElementSection();

            //GetMultipleElementSection();

            //ModifyMultipleElementSection();

            //GetMultipleElementSection();

            //GetSectionGroup();

            //GetValidationSection();

            //GetCommandHandler();

            GetCustomizedLocationConfiguration();

            Console.ReadKey();
        }

        static void GetCustomizedLocationConfiguration()
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap 
            { 
                ExeConfigFilename = "CustomizedConfigurationFile.config"
            };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            Console.Write(config.AppSettings.Settings["a"].Value);
        }

        static void GetSimpleSection()
        {
            NameValueCollection nvSection = ConfigurationManager.GetSection("simpleConfig/nvSection") as NameValueCollection;
            Console.WriteLine(nvSection.Count.ToString());

            Hashtable dicSection = ConfigurationManager.GetSection("simpleConfig/dicSection") as Hashtable;
            Console.WriteLine(dicSection.Count.ToString());

            Hashtable stSection = ConfigurationManager.GetSection("simpleConfig/stSection") as Hashtable;
            Console.WriteLine(stSection.Count.ToString());
        }

        static void GetMultipleElementSection()
        {
            MultipleElementSection meSection = ConfigurationManager.GetSection("MultipleElementSection") as MultipleElementSection;
            Console.WriteLine(meSection.Name);
            Console.WriteLine(meSection.Commands.Count.ToString());
        }

        static void ModifyMultipleElementSection()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            MultipleElementSection section = config.GetSection("MultipleElementSection") as MultipleElementSection;
            MultipleElement element = new MultipleElement
            {
                Name = "Export",
                Provider = "DocAve.SPMigration.Export",
                Type = "DocAve.SPMigration"
            };
            section.Commands.Add(element);
            ConfigurationManager.RefreshSection("MultipleElementSection");
            config.Save();
        }

        static void GetProviderSection()
        {
            ProviderSection providerSection = ConfigurationManager.GetSection("Providers") as ProviderSection;
            Console.WriteLine(providerSection.Name);
            Console.WriteLine(providerSection.Providers.Count.ToString());
        }

        static void GetSingleSection()
        {
            SingleSection singleSection = ConfigurationManager.GetSection("SingleSection") as SingleSection;
            Console.WriteLine(singleSection.Name);
            Console.WriteLine(singleSection.Type);
        }

        static void GetSingleElementSection()
        {
            SingleElementSection singleElement = ConfigurationManager.GetSection("SingleElementSection") as SingleElementSection;
            Console.WriteLine(singleElement.SingleTag.Name);
            Console.WriteLine(singleElement.SingleTag.Value);
        }

        static void GetSectionGroup()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            SectionGroup group = config.GetSectionGroup("section.group") as SectionGroup;
            Console.WriteLine(group.First.SingleTag.Name);
            Console.WriteLine(group.Second.Name);
        }

        static void GetValidationSection()
        {
            try
            {
                ValidationSection vs = ConfigurationManager.GetSection("ValidationSection") as ValidationSection;
                Console.WriteLine(vs.Name);
                Console.WriteLine(vs.Value);
                Console.WriteLine(vs.CustomValue);
                Console.WriteLine(vs.Url);
                Console.WriteLine(vs.MyString.ToString());
            }
            catch (ConfigurationErrorsException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void GetCommandHandler()
        {
            List<Company> companies = ConfigurationManager.GetSection("companies/company") as List<Company>;
            Console.WriteLine(companies.Count.ToString());
        }
    }
}
