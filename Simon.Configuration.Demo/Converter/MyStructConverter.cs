using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Globalization;

namespace Simon.Configuration.Demo
{
    public class MyStruct
    {
        public int Length;
        public int Width;
        public int Height;

        public override string ToString()
        {
            return "L: " + Length + ", W: " + Width + ", H: " + Height;
        }
    }

    public sealed class MyStructConverter : ConfigurationConverterBase
    {
        public MyStructConverter() { }

        /// <summary>Converts a string to a MyStruct.</summary>
        /// <returns>A new MyStruct value.</returns>
        /// <param name="ctx" />The <see 
        /// cref="T:System.ComponentModel.ITypeDescriptorContext">
        /// </see> object used for type conversions.</param />
        /// <param name="ci" />The <see 
        /// cref="T:System.Globalization.CultureInfo">
        /// </see> object used during conversion.</param />
        /// <param name="data" />The <see cref="T:System.String">
        /// </see> object to convert.</param />
        public override object ConvertFrom(ITypeDescriptorContext ctx,
                                           CultureInfo ci, object data)
        {
            string dataStr = ((string)data).ToLower();
            string[] values = dataStr.Split(',');
            if (values.Length == 3)
            {
                try
                {
                    MyStruct myStruct = new MyStruct();
                    foreach (string value in values)
                    {
                        string[] varval = value.Split(':');
                        switch (varval[0])
                        {
                            case "l":
                                myStruct.Length = Convert.ToInt32(varval[1]); break;
                            case "w":
                                myStruct.Width = Convert.ToInt32(varval[1]); break;
                            case "h":
                                myStruct.Height = Convert.ToInt32(varval[1]); break;

                        }
                    }
                    return myStruct;
                }
                catch
                {
                    throw new ArgumentException("The string contained invalid data.");
                }
            }

            throw new ArgumentException("The string did not contain all three, " +
                                        "or contained more than three, values.");
        }

        /// <summary>Converts a MyStruct to a string value.</summary>
        /// <returns>The string representing the value 
        ///           parameter.</returns>
        /// <param name="ctx" />The <see 
        /// cref="T:System.ComponentModel.ITypeDescriptorContext">
        /// </see> object used for type conversions.</param />
        /// <param name="ci" />The <see 
        /// cref="T:System.Globalization.CultureInfo">
        /// </see> object used during conversion.</param />
        /// <param name="data" />The <see cref="T:System.String">
        /// </see> object to convert.</param />
        /// <param name="type" />The type to convert to.</param />
        public override object ConvertTo(ITypeDescriptorContext ctx,
               CultureInfo ci, object value, Type type)
        {
            return value;
        }

    }
}
