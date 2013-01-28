using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;

namespace Simon.Configuration.Demo
{
    public class ValidationSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired=true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
        }

        [ConfigurationProperty("value", IsRequired=true)]
        [IntegerValidator(MinValue=0, MaxValue=100)]
        public int Value
        {
            get
            {
                return int.Parse(base["value"].ToString());
            }
        }

        [ConfigurationProperty("customValue", IsRequired = true)]
        [CallbackValidator(CallbackMethodName = "ModRangeValidatorCallback", Type = typeof(ValidationSection))]
        public int CustomValue
        {
            get
            {
                return int.Parse(base["customValue"].ToString());
            }
        }

        [ConfigurationProperty("url", DefaultValue="aaa", IsRequired=true)]
        [RegexStringWrapperValidator("a", MinLength=0, MaxLength=10)]//custom validator
        public string Url
        {
            get
            {
                return base["url"] as string;
            }
        }

        [ConfigurationProperty("myString", DefaultValue = "L: 5, W: 5, H: 5", IsRequired = true)]
        [TypeConverter(typeof(MyStructConverter))]//custom converter
        public MyStruct MyString
        {
            get { return base["myString"] as MyStruct; }
        }

        /// <summary>
        /// it can be used call back validator to call the method
        /// </summary>
        /// <param name="value"></param>
        public static void ModRangeValidatorCallback(object value)
        {
            int intVal = (int)value;
            if (intVal >= -100 && intVal <= 100)
            {
                if (intVal % 10 != 0)
                    throw new ArgumentException("The integer " +
                         "value is not a multiple of 10.");
            }
            else
            {
                throw new ArgumentException("The integer value is not" +
                                            " within the range -100 to 100");
            }
        }
    }
}
