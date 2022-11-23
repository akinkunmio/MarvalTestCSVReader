using MarvalTestCSVReader.Models.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models.Core
{
    public class PersonRow : ValidatedRow
    {
        private readonly PersonRowConfig _config;
        const int INDEX_OF_IDENTITY = 0;
        const int INDEX_OF_FIRST_NAME = 1;
        const int INDEX_OF_SURNAME = 2;
        const int INDEX_OF_AGE = 3;
        const int INDEX_OF_SEX = 4;
        const int INDEX_OF_MOBILE = 5;
        const int INDEX_OF_ACTIVE = 6;

        public PersonRow() { }

        public PersonRow(Row row, PersonRowConfig config)
        {
            _config = config;

            this.Row = row.Index;

            SetupFields(row.Columns);
        }

        private void SetupFields(List<Column> columns)
        {
            var errors = new List<string>();

            Identity = GetColumnValue(columns, INDEX_OF_IDENTITY, "").Trim();
            FirstName = GetColumnValue(columns, INDEX_OF_FIRST_NAME, "").Trim();
            Surname = GetColumnValue(columns, INDEX_OF_SURNAME, "").Trim();

            var ageSupplied = GetColumnValue(columns, INDEX_OF_AGE, "").Trim();

            if (_config.IsAgeRequired && string.IsNullOrWhiteSpace(ageSupplied))
                errors.Add($"{nameof(Age)} not specified");

            if (!string.IsNullOrWhiteSpace(ageSupplied)) 
            { 
                if (!(int.TryParse(ageSupplied, out int _age)) && ageSupplied.Length > 4)
                {
                    errors.Add($"{nameof(Age)} must be a valid age. Provided age: {ageSupplied} is invalid");
                }
                Age = ageSupplied;
            }

            var sexSupplied = GetColumnValue(columns, INDEX_OF_SEX, "").Trim();

            if (_config.IsSexRequired && string.IsNullOrWhiteSpace(sexSupplied))
                errors.Add($"{nameof(Sex)} not specified");

            if (!string.IsNullOrWhiteSpace(sexSupplied))
            {
                if ( !(sexSupplied.Equals("m", StringComparison.InvariantCultureIgnoreCase)
                    || sexSupplied.Equals("f", StringComparison.InvariantCultureIgnoreCase)
                    || sexSupplied.Equals("female", StringComparison.InvariantCultureIgnoreCase)
                    || sexSupplied.Equals("male", StringComparison.InvariantCultureIgnoreCase)))
                {
                    errors.Add($"{nameof(Sex)} must be either 'Male', 'Female', 'M' or 'F'. Provided sex: {sexSupplied} is invalid");
                }

                Sex = sexSupplied;
            }

            var providedMobile = GetColumnValue(columns, INDEX_OF_MOBILE, "");

            if (_config.IsPhoneNumberRequired && string.IsNullOrWhiteSpace(providedMobile))
                errors.Add($"{nameof(Mobile)} not specified");

            if (!string.IsNullOrWhiteSpace(providedMobile))
            {
                if (!decimal.TryParse(providedMobile, out decimal _mobile))
                {
                    errors.Add($"{nameof(Mobile)} must be number. Provided mobile number: {providedMobile} is invalid");
                }
                Mobile = providedMobile;
            }

            var suppliedActive = GetColumnValue(columns, INDEX_OF_ACTIVE, "");

            if (_config.IsActiveRequired && string.IsNullOrWhiteSpace(suppliedActive))
                errors.Add($"{nameof(Active)} not specified");

            if (!string.IsNullOrWhiteSpace(suppliedActive))
            {
                if (!bool.TryParse(suppliedActive, out bool active))
                {
                    errors.Add($"{nameof(Active)} must be either 'TRUE' or 'FALSE'.");
                }
                Active = suppliedActive;
            }

            if (string.IsNullOrWhiteSpace(FirstName))
                errors.Add($"{nameof(FirstName)} not specified");
            if (FirstName.Count() < 2)
                errors.Add($"{nameof(FirstName)} not valid");
            if (string.IsNullOrWhiteSpace(Surname))
                errors.Add($"{nameof(Surname)} not specified");
            if (Surname.Count() < 2)
                errors.Add($"{nameof(Surname)} not valid");

            IsValid = errors.Count == 0;
            if (!IsValid) ErrorMessages = errors;
            Person = new Person
            {
                Identity = int.Parse(Identity),
                Active = Active ?? "",
                Age = Age ?? "",
                FirstName = FirstName ?? "",
                Surname = Surname ?? "",
                IsValid = IsValid.ToString() ?? "",
                Mobile = Mobile ?? "",
                Sex = Sex ?? "",
            };
        }

        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public string Active { get; set; }

        public Person Person { get; set; }

    }

}
