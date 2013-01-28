using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Simon.Configuration.Demo
{
    public class RegexStringWrapperValidator : ConfigurationValidatorBase
    {
        #region Constructors
        public RegexStringWrapperValidator(string regex) :
            this(regex, 0, 0x7fffffff)
        {
        }

        public RegexStringWrapperValidator(string regex, int minLength) :
            this(regex, minLength, 0x7fffffff)
        {
        }

        public RegexStringWrapperValidator(string regex, int minLength,
                                           int maxLength)
        {
            m_regexValidator = new RegexStringValidator(regex);
            m_stringValidator = new StringValidator(minLength, maxLength);
        }
        #endregion

        #region Fields
        private RegexStringValidator m_regexValidator;
        private StringValidator m_stringValidator;
        #endregion

        #region Overrides
        public override bool CanValidate(Type type)
        {
            return (type == typeof(string));
        }

        public override void Validate(object value)
        {
            m_stringValidator.Validate(value);
            m_regexValidator.Validate(value);
        }
        #endregion
    }

    public sealed class RegexStringWrapperValidatorAttribute : ConfigurationValidatorAttribute
    {
        #region Constructors
        public RegexStringWrapperValidatorAttribute(string regex)
        {
            m_regex = regex;
            m_minLength = 0;
            m_maxLength = 0x7fffffff;
        }
        #endregion

        #region Fields
        private string m_regex;
        private int m_minLength;
        private int m_maxLength;
        #endregion

        #region Properties
        public string Regex
        {
            get { return m_regex; }
        }

        public int MinLength
        {
            get
            {
                return m_minLength;
            }
            set
            {
                if (m_maxLength < value)
                {
                    throw new ArgumentOutOfRangeException("value",
                          "The upper range limit value must be greater" +
                          " than the lower range limit value.");
                }
                m_minLength = value;

            }
        }

        public int MaxLength
        {
            get
            {
                return m_maxLength;
            }
            set
            {
                if (m_minLength > value)
                {
                    throw new ArgumentOutOfRangeException("value",
                          "The upper range limit value must be greater " +
                          "than the lower range limit value.");
                }
                m_maxLength = value;
            }
        }

        public override ConfigurationValidatorBase ValidatorInstance
        {
            get
            {
                return new RegexStringWrapperValidator(m_regex, m_minLength, m_maxLength);
            }
        }
        #endregion
    }
}
